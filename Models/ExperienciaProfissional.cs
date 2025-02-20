using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PortfolioBuilder.Models;

public class ExperienciaProfissional
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PessoaId { get; set; } 

    public string NomeEmpresa { get; set; }
    public string Cargo { get; set; }
    public string MesAnoInicio { get; set; }
    public string MesAnoTermino { get; set; }
    public string Atividades { get; set; }
    public string Projetos { get; set; }

    // Lista de IDs das competências relacionadas a essa experiência
    public List<string> CompetenciasIds { get; set; } = new List<string>();

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}
