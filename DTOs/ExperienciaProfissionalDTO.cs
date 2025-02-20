namespace PortfolioBuilder.DTOs;

public class ExperienciaProfissionalDTO
{
    public string PessoaId { get; set; } // Referência à Pessoa
    public string NomeEmpresa { get; set; } // Nome da empresa
    public string Cargo { get; set; } // Cargo exercido
    public string MesAnoInicio { get; set; } // Mês e ano de início
    public string MesAnoTermino { get; set; } // Mês e ano de término (pode ser nulo se ainda estiver em andamento)
    public string Atividades { get; set; } // Descrição das atividades exercidas
    public string Projetos { get; set; } // Descrição dos projetos (opcional)
    public List<string> CompetenciasIds { get; set; } = new List<string>(); // IDs das competências relacionadas
}
