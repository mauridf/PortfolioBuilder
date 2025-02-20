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
public class ExperienciaProfissionalController : ControllerBase
{
    private readonly MongoContext _context;
    private readonly IMapper _mapper;

    public ExperienciaProfissionalController(MongoContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Get()
    {
        var experiencias = await _context.Experiencias.Find(_ => true).ToListAsync();
        return Ok(experiencias);
    }

    [HttpGet("PorPessoa/{pessoaId}")]
    public async Task<IActionResult> GetByPessoa(string pessoaId)
    {
        // Verifica se o ID da pessoa é válido
        if (!ObjectId.TryParse(pessoaId, out _))
        {
            return BadRequest("ID da pessoa inválido.");
        }

        // Busca as experiências profissionais da pessoa
        var experiencias = await _context.Experiencias
            .Find(e => e.PessoaId == pessoaId)
            .ToListAsync();

        return Ok(experiencias);
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Create([FromBody] ExperienciaProfissionalDTO experienciaDTO)
    {
        var experiencia = new ExperienciaProfissional
        {
            PessoaId = experienciaDTO.PessoaId,
            NomeEmpresa = experienciaDTO.NomeEmpresa,
            Cargo = experienciaDTO.Cargo,
            MesAnoInicio = experienciaDTO.MesAnoInicio,
            MesAnoTermino = experienciaDTO.MesAnoTermino,
            Atividades = experienciaDTO.Atividades,
            Projetos = experienciaDTO.Projetos,
            CompetenciasIds = experienciaDTO.CompetenciasIds,
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        await _context.Experiencias.InsertOneAsync(experiencia);
        return CreatedAtAction(nameof(Get), new { id = experiencia.Id }, experiencia);
    }

    [HttpDelete("Remover/{id}")]
    public async Task<IActionResult> DeleteExperiencia(string id)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a experiência profissional pelo ID
        var experiencia = await _context.Experiencias
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (experiencia == null)
        {
            return NotFound("Experiência profissional não encontrada.");
        }

        // Remove a experiência profissional
        await _context.Experiencias.DeleteOneAsync(e => e.Id == id);

        // Retorna uma mensagem de sucesso
        return Ok(new { message = "Experiência profissional deletada com sucesso." });
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] ExperienciaProfissionalDTO experienciaDTO)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a experiência profissional pelo ID
        var experienciaExistente = await _context.Experiencias
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (experienciaExistente == null)
        {
            return NotFound("Experiência profissional não encontrada.");
        }

        // Atualiza os campos
        experienciaExistente.NomeEmpresa = experienciaDTO.NomeEmpresa;
        experienciaExistente.Cargo = experienciaDTO.Cargo;
        experienciaExistente.MesAnoInicio = experienciaDTO.MesAnoInicio;
        experienciaExistente.MesAnoTermino = experienciaDTO.MesAnoTermino;
        experienciaExistente.Atividades = experienciaDTO.Atividades;
        experienciaExistente.Projetos = experienciaDTO.Projetos;
        experienciaExistente.CompetenciasIds = experienciaDTO.CompetenciasIds;
        experienciaExistente.DataAtualizacao = DateTime.UtcNow;

        // Atualiza no banco de dados
        await _context.Experiencias.ReplaceOneAsync(e => e.Id == id, experienciaExistente);

        return Ok(experienciaExistente);
    }
}
