using AutoMapper;
using PortfolioBuilder.DTOs;
using PortfolioBuilder.Models;

namespace PortfolioBuilder.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PessoaDTO, Pessoa>();
        CreateMap<EnderecoDTO, Endereco>();
        CreateMap<RedeSocialDTO, RedeSocial>();
        CreateMap<InstrucaoDTO, Instrucao>();
        CreateMap<CompetenciaDTO, Competencia>();
        CreateMap<ConquistaDTO, Conquista>();
        CreateMap<IdiomaDTO, Idioma>();
        CreateMap<ExperienciaProfissionalDTO, ExperienciaProfissional>();
    }
}
