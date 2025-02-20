using MongoDB.Driver;
using PortfolioBuilder.Models;

namespace PortfolioBuilder.Data;

public class MongoContext
{
    private readonly IMongoDatabase _database;
    public IMongoClient Client { get; } // Propriedade pública para o MongoClient

    public MongoContext(IConfiguration configuration)
    {
        Client = new MongoClient(configuration.GetConnectionString("MongoDB")); // Inicializa o Client
        _database = Client.GetDatabase("PortfolioBuilder"); // Nome do banco de dados
    }

    public IMongoCollection<Pessoa> Pessoas => _database.GetCollection<Pessoa>("Pessoas");
    public IMongoCollection<Instrucao> Instrucoes => _database.GetCollection<Instrucao>("Instrucoes");
    public IMongoCollection<ExperienciaProfissional> Experiencias => _database.GetCollection<ExperienciaProfissional>("Experiencias");
    public IMongoCollection<Competencia> Competencias => _database.GetCollection<Competencia>("Competencias");
    public IMongoCollection<Conquista> Conquistas => _database.GetCollection<Conquista>("Conquistas");
    public IMongoCollection<Idioma> Idiomas => _database.GetCollection<Idioma>("Idiomas");
}