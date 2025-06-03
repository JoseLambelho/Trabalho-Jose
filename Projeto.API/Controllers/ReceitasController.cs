// Controller de Receita
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Modelo;
using Projeto.Servicos;
using System.Threading.Tasks;
using System.Collections.Generic;


[ApiController]
[Route("api/[controller]")]
public class ReceitasController : ControllerBase
{
    private readonly IReceitaServico _servico;

    public ReceitasController(IReceitaServico servico)
    {
        _servico = servico;
    }

    [HttpGet]
    public async Task<ActionResult<List<Receita>>> Get()
    {
        var receitas = await _servico.ListarTodasAsync();
        return Ok(receitas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Receita>> Get(int id)
    {
        var receita = await _servico.ObterPorIdAsync(id);
        if (receita == null) return NotFound();
        return Ok(receita);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Receita>> Post([FromBody] Receita receita)
    {
        var nova = await _servico.CriarAsync(receita);
        return CreatedAtAction(nameof(Get), new { id = nova.Id }, nova);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Receita receita)
    {
        if (id != receita.Id) return BadRequest();
        await _servico.AtualizarAsync(receita);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _servico.RemoverAsync(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
