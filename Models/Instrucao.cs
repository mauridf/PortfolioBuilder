using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PortfolioBuilder.Models;

public class Instrucao
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PessoaId { get; set; }

    public string Nivel { get; set; }
    public string MesAnoInicio { get; set; }
    public string MesAnoTermino { get; set; }
    public string Instituicao { get; set; }
    public string NomeCurso { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}
