using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PortfolioBuilder.Data;

namespace PortfolioBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculoController : ControllerBase
    {
        private readonly MongoContext _context;
        private readonly IMapper _mapper;

        public CurriculoController(MongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("MontarCurriculo/{id}")]
        public async Task<IActionResult> MontarCurriculo(string id)
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

            // Busca as instruções da pessoa
            var instrucoes = await _context.Instrucoes
                .Find(i => i.PessoaId == id)
                .Project(i => new
                {
                    i.Nivel,
                    i.Instituicao,
                    i.MesAnoInicio,
                    i.MesAnoTermino,
                    i.NomeCurso
                })
                .ToListAsync();

            // Busca as competências da pessoa
            var competencias = await _context.Competencias
                .Find(c => c.PessoaId == id)
                .Project(c => new
                {
                    c.Id,
                    c.Tipo,
                    c.Nome
                })
                .ToListAsync();

            // Busca as experiências profissionais da pessoa
            var experiencias = await _context.Experiencias
                .Find(e => e.PessoaId == id)
                .Project(e => new
                {
                    e.NomeEmpresa,
                    e.Cargo,
                    e.MesAnoInicio,
                    e.MesAnoTermino,
                    e.Atividades,
                    e.Projetos,
                    Competencias = e.CompetenciasIds.Select(compId => competencias.FirstOrDefault(c => c.Id == compId))
                })
                .ToListAsync();

            // Busca as conquistas da pessoa
            var conquistas = await _context.Conquistas
                .Find(c => c.PessoaId == id)
                .Project(c => new
                {
                    c.Nome
                })
                .ToListAsync();

            // Busca os idiomas da pessoa
            var idiomas = await _context.Idiomas
                .Find(i => i.PessoaId == id)
                .Project(i => new
                {
                    i.Nome,
                    i.Nivel
                })
                .ToListAsync();

            // Monta o JSON final
            var curriculo = new
            {
                Pessoa = new
                {
                    pessoa.Nome,
                    pessoa.CPF,
                    pessoa.Endereco,
                    pessoa.Telefone,
                    pessoa.Email,
                    pessoa.RedesSociais
                },
                Instrucoes = instrucoes,
                Competencias = competencias,
                ExperienciasProfissionais = experiencias,
                Conquistas = conquistas,
                Idiomas = idiomas
            };

            return Ok(curriculo);
        }
    }
}
