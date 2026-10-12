using JogoApi.Models;
using JogoApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JogoApi.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly IMongoCollection<Jogo> _jogos;

    public JogoRepository(IMongoDatabase database, IOptions<MongoDbSettings> options)
    {
        _jogos = database.GetCollection<Jogo>(options.Value.CollectionName);
    }

    public async Task<List<Jogo>> ListarAsync() =>
        await _jogos.Find(_ => true).ToListAsync();

    public async Task<Jogo?> ObterPorIdAsync(string id) =>
        await _jogos.Find(j => j.Id == id).FirstOrDefaultAsync();

    public async Task<Jogo> CriarAsync(Jogo jogo)
    {
        jogo.Id = null; // o Mongo gera o ObjectId
        await _jogos.InsertOneAsync(jogo);
        return jogo;
    }

    public async Task<bool> AtualizarAsync(string id, Jogo jogo)
    {
        jogo.Id = id;
        var result = await _jogos.ReplaceOneAsync(j => j.Id == id, jogo);
        return result.MatchedCount > 0;
    }

    public async Task<bool> RemoverAsync(string id)
    {
        var result = await _jogos.DeleteOneAsync(j => j.Id == id);
        return result.DeletedCount > 0;
    }

    // DESAFIO 1: Builders<Jogo>.Filter.And(...)
    public async Task<List<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo)
    {
        var filtro = Builders<Jogo>.Filter.And(
            Builders<Jogo>.Filter.Eq(j => j.Plataforma, plataforma),
            Builders<Jogo>.Filter.Lte(j => j.Preco, precoMaximo)
        );
        return await _jogos.Find(filtro).ToListAsync();
    }

    // DESAFIO 2: pipeline de agregação
    public async Task<List<RelatorioEstoqueDto>> RelatorioEstoqueAsync()
    {
        var docs = await _jogos.Aggregate()
            .Group(
                j => j.Plataforma,
                g => new
                {
                    Plataforma = g.Key,
                    Quantidade = g.Count(),
                    Valor = g.Sum(j => j.Preco * j.Estoque)
                })
            .SortBy(r => r.Plataforma)
            .ToListAsync();

        return docs.Select(r => new RelatorioEstoqueDto
        {
            Plataforma = r.Plataforma,
            QuantidadeTitulos = r.Quantidade,
            ValorTotalInventario = r.Valor
        }).ToList();
    }
}