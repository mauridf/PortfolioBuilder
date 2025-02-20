using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PortfolioBuilder.Data;
using PortfolioBuilder.DTOs;
using PortfolioBuilder.Models;

namespace PortfolioBuilder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly MongoContext _context;
    private readonly IMapper _mapper;

    public PessoaController(MongoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> Get()
    {
        var pessoas = await _context.Pessoas.Find(_ => true).ToListAsync();
        return Ok(pessoas);
    }

    [HttpGet("BuscarPorId/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a pessoa pelo ID
        var pessoa = await _context.Pessoas.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada.");
        }

        // Retorna a pessoa encontrada
        return Ok(pessoa);
    }

    [HttpPost("Registrar")]
    public async Task<IActionResult> Create([FromBody] PessoaDTO pessoaDTO)
    {
        var pessoa = _mapper.Map<Pessoa>(pessoaDTO);
        await _context.Pessoas.InsertOneAsync(pessoa);
        return CreatedAtAction(nameof(Get), new { id = pessoa.Id }, pessoa);
    }

    [HttpDelete("Remover/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a pessoa pelo ID
        var pessoa = await _context.Pessoas.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (pessoa == null)
        {
            return NotFound("Pessoa não encontrada.");
        }

        // Remove a pessoa e todas as entidades relacionadas
        using (var session = await _context.Client.StartSessionAsync())
        {
            session.StartTransaction();

            try
            {
                // Remove a pessoa
                await _context.Pessoas.DeleteOneAsync(session, p => p.Id == id);

                // Remove as competências relacionadas
                await _context.Competencias.DeleteManyAsync(session, c => c.PessoaId == id);

                // Remove as conquistas relacionadas
                await _context.Conquistas.DeleteManyAsync(session, c => c.PessoaId == id);

                // Remove as experiências profissionais relacionadas
                await _context.Experiencias.DeleteManyAsync(session, e => e.PessoaId == id);

                // Remove os idiomas relacionados
                await _context.Idiomas.DeleteManyAsync(session, i => i.PessoaId == id);

                // Remove as instruções relacionadas
                await _context.Instrucoes.DeleteManyAsync(session, i => i.PessoaId == id);

                // Commit da transação
                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // Rollback em caso de erro
                await session.AbortTransactionAsync();
                return StatusCode(500, $"Erro ao deletar pessoa e entidades relacionadas: {ex.Message}");
            }
        }

        // Retorna uma mensagem de sucesso
        return Ok(new { message = "Pessoa e entidades relacionadas foram deletadas com sucesso." });
    }

    [HttpPut("Atualizar/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] PessoaDTO pessoaDTO)
    {
        // Verifica se o ID é válido
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest("ID inválido.");
        }

        // Busca a pessoa pelo ID
        var pessoaExistente = await _context.Pessoas.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (pessoaExistente == null)
        {
            return NotFound("Pessoa não encontrada.");
        }

        // Mapeia o DTO para a model existente
        pessoaExistente.Nome = pessoaDTO.Nome;
        pessoaExistente.CPF = pessoaDTO.CPF;
        pessoaExistente.Endereco = new Endereco
        {
            Rua = pessoaDTO.Endereco.Rua,
            Numero = pessoaDTO.Endereco.Numero,
            Cidade = pessoaDTO.Endereco.Cidade,
            Estado = pessoaDTO.Endereco.Estado,
            CEP = pessoaDTO.Endereco.CEP
        };
        pessoaExistente.Telefone = pessoaDTO.Telefone;
        pessoaExistente.Email = pessoaDTO.Email;
        pessoaExistente.RedesSociais = pessoaDTO.RedesSociais.Select(rs => new RedeSocial
        {
            NomeRedeSocial = rs.NomeRedeSocial,
            Link = rs.Link
        }).ToList();
        pessoaExistente.DataAtualizacao = DateTime.UtcNow;

        // Atualiza a pessoa no banco de dados
        await _context.Pessoas.ReplaceOneAsync(p => p.Id == id, pessoaExistente);

        return Ok(pessoaExistente); // Retorna a pessoa atualizada
    }
}
