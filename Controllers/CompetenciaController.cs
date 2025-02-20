using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PortfolioBuilder.Data;
using PortfolioBuilder.DTOs;
using PortfolioBuilder.Models;

namespace PortfolioBuilder.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompetenciaController : ControllerBase
{
    private readonly MongoContext _context;
    private readonly IMapper _mapper;

    public CompetenciaController(MongoContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Get()
    {
        var competencias = await _context.Competencias.Find(_ => true).ToListAsync();
        return Ok(competencias);
    }

    [HttpGet("PorPessoa/{pessoaId}")]
    public async Task<IActionResult> GetByPessoa(string pessoaId)
    {
        // Verifica se o ID da pessoa é válido
        if (!ObjectId.TryParse(pessoaId, out _))
        {
            return BadRequest("ID da pessoa inválido.");
        }

        // Busca as competencias da pessoa
        var competencias = await _context.Competencias
            .Find(c => c.PessoaId == pessoaId)
            .ToListAsync();

        return Ok(competencias);
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Create([FromBody] CompetenciaDTO competenciaDTO)
    {
        var competencia = _mapper.Map<Competencia>(competenciaDTO);
        await _context.Competencias.InsertOneAsync(competencia);
        return CreatedAtAction(nameof(Get), new { id = competencia.Id }, competencia);
    }

    [HttpDelete("Remover/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a competencia pelo ID
        var competencia = await _context.Competencias.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (competencia == null)
        {
            return NotFound("Competência não encontrada.");
        }

        // Remove a competencia
        await _context.Competencias.DeleteOneAsync(i => i.Id == id);

        // Retorna uma mensagem de sucesso
        return Ok(new { message = "Competência deletada com sucesso." });
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] CompetenciaDTO competenciaDTO)
    {
        var competenciaExistente = await _context.Competencias.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (competenciaExistente == null)
        {
            return NotFound("Competência não encontrada.");
        }

        competenciaExistente.PessoaId = competenciaDTO.PessoaId;
        competenciaExistente.Nome = competenciaDTO.Nome;
        competenciaExistente.Tipo = competenciaDTO.Tipo;
        competenciaExistente.DataAtualizacao = DateTime.UtcNow;

        await _context.Competencias.ReplaceOneAsync(c => c.Id == id, competenciaExistente);
        return Ok(competenciaExistente);
    }
}
