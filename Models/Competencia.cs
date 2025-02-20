using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PortfolioBuilder.Models;

public class Competencia
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PessoaId { get; set; }

    public string Nome { get; set; } // Ex: Java, Angular, SQL
    public string Tipo { get; set; } // Ex: Backend, Frontend, Banco de Dados

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}