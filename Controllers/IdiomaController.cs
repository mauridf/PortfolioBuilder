using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PortfolioBuilder.Data;
using PortfolioBuilder.DTOs;
using PortfolioBuilder.Models;

namespace PortfolioBuilder.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdiomaController : ControllerBase
{
    private readonly MongoContext _context;
    private readonly IMapper _mapper;

    public IdiomaController(MongoContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Get()
    {
        var idiomas = await _context.Idiomas.Find(_ => true).ToListAsync();
        return Ok(idiomas);
    }

    [HttpGet("PorPessoa/{pessoaId}")]
    public async Task<IActionResult> GetByPessoa(string pessoaId)
    {
        // Verifica se o ID da pessoa é válido
        if (!ObjectId.TryParse(pessoaId, out _))
        {
            return BadRequest("ID da pessoa inválido.");
        }

        // Busca os idiomas da pessoa
        var idiomas = await _context.Idiomas
            .Find(i => i.PessoaId == pessoaId)
            .ToListAsync();

        return Ok(idiomas);
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Create([FromBody] IdiomaDTO idiomaDTO)
    {
        var idioma = _mapper.Map<Idioma>(idiomaDTO);
        await _context.Idiomas.InsertOneAsync(idioma);
        return CreatedAtAction(nameof(Get), new { id = idioma.Id }, idioma);
    }

    [HttpDelete("Remover/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a idioma pelo ID
        var idioma = await _context.Idiomas.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (idioma == null)
        {
            return NotFound("Idioma não encontrado.");
        }

        // Remove a idioma
        await _context.Idiomas.DeleteOneAsync(i => i.Id == id);

        // Retorna uma mensagem de sucesso
        return Ok(new { message = "Idioma deletado com sucesso." });
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] IdiomaDTO idiomaDTO)
    {
        var idiomaExistente = await _context.Idiomas.Find(i => i.Id == id).FirstOrDefaultAsync();
        if (idiomaExistente == null)
        {
            return NotFound("Idioma não encontrado.");
        }

        idiomaExistente.PessoaId = idiomaDTO.PessoaId;
        idiomaExistente.Nome = idiomaDTO.Nome;
        idiomaExistente.Nivel = idiomaDTO.Nivel;
        idiomaExistente.DataAtualizacao = DateTime.UtcNow;

        await _context.Idiomas.ReplaceOneAsync(i => i.Id == id, idiomaExistente);
        return Ok(idiomaExistente);
    }
}
