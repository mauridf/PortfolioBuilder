namespace PortfolioBuilder.DTOs;

public class PessoaDTO
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public EnderecoDTO Endereco { get; set; } // Usando EnderecoDTO
    public string Telefone { get; set; }
    public string Email { get; set; }
    public List<RedeSocialDTO> RedesSociais { get; set; } = new List<RedeSocialDTO>(); // Usando RedeSocialDTO
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}

public class EnderecoDTO
{
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }
}

public class RedeSocialDTO
{
    public string NomeRedeSocial { get; set; }
    public string Link { get; set; }
}