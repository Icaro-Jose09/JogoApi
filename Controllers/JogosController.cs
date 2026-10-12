using JogoApi.Models;
using JogoApi.Services;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace JogoApi.Controllers;

[ApiController]
[Route("api/jogos")]
public class JogosController : ControllerBase
{
    private readonly JogoService _service;

    public JogosController(JogoService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Jogo jogo)
    {
        var criado = await _service.CriarAsync(jogo);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
    }

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _service.ListarAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest("Id inválido.");
        var jogo = await _service.ObterPorIdAsync(id);
        return jogo is null ? NotFound() : Ok(jogo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, [FromBody] Jogo jogo)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest("Id inválido.");
        var ok = await _service.AtualizarAsync(id, jogo);
        return ok ? Ok(jogo) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest("Id inválido.");
        var ok = await _service.RemoverAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("busca")]
    public async Task<IActionResult> Buscar([FromQuery] string plataforma, [FromQuery] decimal precoMaximo)
    {
        if (string.IsNullOrWhiteSpace(plataforma)) return BadRequest("Informe a plataforma.");
        return Ok(await _service.BuscarAsync(plataforma, precoMaximo));
    }

    [HttpGet("relatorio-estoque")]
    public async Task<IActionResult> RelatorioEstoque() =>
        Ok(await _service.RelatorioEstoqueAsync());
}