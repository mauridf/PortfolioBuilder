using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PortfolioBuilder.Models;

public class Idioma
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string PessoaId { get; set; } 

    public string Nome { get; set; } // Ex: Inglês, Espanhol
    public string Nivel { get; set; } // Ex: Básico, Intermediário, Avançado

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}