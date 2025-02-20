namespace PortfolioBuilder.DTOs;

public class InstrucaoDTO
{
    public string PessoaId { get; set; } // Referência à Pessoa
    public string Nivel { get; set; } // Nível de instrução (Ensino Médio, Graduação, etc.)
    public string MesAnoInicio { get; set; } // Mês e ano de início
    public string MesAnoTermino { get; set; } // Mês e ano de término (pode ser nulo se ainda estiver em andamento)
    public string Instituicao { get; set; } // Nome da instituição de ensino
    public string NomeCurso { get; set; } // Nome do curso
}