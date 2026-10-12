using JogoApi.Models;

namespace JogoApi.Repositories;

public interface IJogoRepository
{
    Task<List<Jogo>> ListarAsync();
    Task<Jogo?> ObterPorIdAsync(string id);
    Task<Jogo> CriarAsync(Jogo jogo);
    Task<bool> AtualizarAsync(string id, Jogo jogo);
    Task<bool> RemoverAsync(string id);
    Task<List<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo);
    Task<List<RelatorioEstoqueDto>> RelatorioEstoqueAsync();
}