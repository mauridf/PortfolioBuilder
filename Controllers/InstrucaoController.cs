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
public class InstrucaoController : ControllerBase
{
    private readonly MongoContext _context;
    private readonly IMapper _mapper;

    public InstrucaoController(MongoContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Get()
    {
        var instrucoes = await _context.Instrucoes.Find(_ => true).ToListAsync();
        return Ok(instrucoes);
    }

    [HttpGet("PorPessoa/{pessoaId}")]
    public async Task<IActionResult> GetByPessoa(string pessoaId)
    {
        // Verifica se o ID da pessoa é válido
        if (!ObjectId.TryParse(pessoaId, out _))
        {
            return BadRequest("ID da pessoa inválido.");
        }

        // Busca as instruções da pessoa
        var instrucoes = await _context.Instrucoes
            .Find(i => i.PessoaId == pessoaId)
            .ToListAsync();

        return Ok(instrucoes);
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Create([FromBody] InstrucaoDTO instrucaoDTO)
    {
        var instrucao = _mapper.Map<Instrucao>(instrucaoDTO);
        await _context.Instrucoes.InsertOneAsync(instrucao);
        return CreatedAtAction(nameof(Get), new { id = instrucao.Id }, instrucao);
    }

    [HttpDelete("Remover/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a instrução pelo ID
        var instrucao = await _context.Instrucoes.Find(i => i.Id == id).FirstOrDefaultAsync();
        if (instrucao == null)
        {
            return NotFound("Instrução não encontrada.");
        }

        // Remove a instrução
        await _context.Instrucoes.DeleteOneAsync(i => i.Id == id);

        // Retorna uma mensagem de sucesso
        return Ok(new { message = "Instrução deletada com sucesso." });
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] InstrucaoDTO instrucaoDTO)
    {
        var instrucaoExistente = await _context.Instrucoes.Find(i => i.Id == id).FirstOrDefaultAsync();
        if (instrucaoExistente == null)
        {
            return NotFound("Instrução não encontrada.");
        }

        instrucaoExistente.Nivel = instrucaoDTO.Nivel;
        instrucaoExistente.MesAnoInicio = instrucaoDTO.MesAnoInicio;
        instrucaoExistente.MesAnoTermino = instrucaoDTO.MesAnoTermino;
        instrucaoExistente.Instituicao = instrucaoDTO.Instituicao;
        instrucaoExistente.NomeCurso = instrucaoDTO.NomeCurso;
        instrucaoExistente.DataAtualizacao = DateTime.UtcNow;

        await _context.Instrucoes.ReplaceOneAsync(i => i.Id == id, instrucaoExistente);
        return Ok(instrucaoExistente);
    }
}
