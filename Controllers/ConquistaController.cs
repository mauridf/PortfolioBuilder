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
public class ConquistaController : ControllerBase
{
    private readonly MongoContext _context;
    private readonly IMapper _mapper;

    public ConquistaController(MongoContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Get()
    {
        var conquistas = await _context.Conquistas.Find(_ => true).ToListAsync();
        return Ok(conquistas);
    }

    [HttpGet("PorPessoa/{pessoaId}")]
    public async Task<IActionResult> GetByPessoa(string pessoaId)
    {
        // Verifica se o ID da pessoa é válido
        if (!ObjectId.TryParse(pessoaId, out _))
        {
            return BadRequest("ID da pessoa inválido.");
        }

        // Busca as conquistas da pessoa
        var conquistas = await _context.Conquistas
            .Find(c => c.PessoaId == pessoaId)
            .ToListAsync();

        return Ok(conquistas);
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Create([FromBody] ConquistaDTO conquistaDTO)
    {
        var conquista = _mapper.Map<Conquista>(conquistaDTO);
        await _context.Conquistas.InsertOneAsync(conquista);
        return CreatedAtAction(nameof(Get), new { id = conquista.Id }, conquista);
    }

    [HttpDelete("Remover/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a conquista pelo ID
        var conquista = await _context.Conquistas.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (conquista == null)
        {
            return NotFound("Conquista não encontrada.");
        }

        // Remove a conquista
        await _context.Conquistas.DeleteOneAsync(i => i.Id == id);

        // Retorna uma mensagem de sucesso
        return Ok(new { message = "Conquista deletada com sucesso." });
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] ConquistaDTO conquistaDTO)
    {
        var conquistaExistente = await _context.Conquistas.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (conquistaExistente == null)
        {
            return NotFound("Competência não encontrada.");
        }

        conquistaExistente.PessoaId = conquistaDTO.PessoaId;
        conquistaExistente.Nome = conquistaDTO.Nome;
        conquistaExistente.MesAno = conquistaDTO.MesAno;
        conquistaExistente.DataAtualizacao = DateTime.UtcNow;

        await _context.Conquistas.ReplaceOneAsync(c => c.Id == id, conquistaExistente);
        return Ok(conquistaExistente);
    }
}
