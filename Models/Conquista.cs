using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PortfolioBuilder.Models;

public class Conquista
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PessoaId { get; set; } 

    public string Nome { get; set; } // Ex: Certificação AWS, Curso de Angular
    public string MesAno { get; set; } // Mês e ano da conquista

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}
