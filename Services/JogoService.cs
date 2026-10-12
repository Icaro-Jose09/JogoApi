using JogoApi.Models;
using JogoApi.Repositories;

namespace JogoApi.Services;

public class JogoService
{
    private readonly IJogoRepository _repo;

    public JogoService(IJogoRepository repo) => _repo = repo;

    public Task<List<Jogo>> ListarAsync() => _repo.ListarAsync();
    public Task<Jogo?> ObterPorIdAsync(string id) => _repo.ObterPorIdAsync(id);
    public Task<Jogo> CriarAsync(Jogo jogo) => _repo.CriarAsync(jogo);
    public Task<bool> AtualizarAsync(string id, Jogo jogo) => _repo.AtualizarAsync(id, jogo);
    public Task<bool> RemoverAsync(string id) => _repo.RemoverAsync(id);
    public Task<List<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo) =>
        _repo.BuscarAsync(plataforma, precoMaximo);
    public Task<List<RelatorioEstoqueDto>> RelatorioEstoqueAsync() => _repo.RelatorioEstoqueAsync();
}