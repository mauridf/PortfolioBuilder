using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PortfolioBuilder.Models;

public class Pessoa
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Nome { get; set; }
    public string CPF { get; set; }
    public Endereco Endereco { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public List<RedeSocial> RedesSociais { get; set; } = new List<RedeSocial>();
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}

public class Endereco
{
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
}

public class RedeSocial
{
    public string NomeRedeSocial { get; set; }
    public string Link { get; set; }
}
