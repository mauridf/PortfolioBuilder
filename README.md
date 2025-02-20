# PortfolioBuilder API

A **PortfolioBuilder API** é uma aplicação backend desenvolvida em .NET 8 com MongoDB, projetada para ajudar usuários a montar um portfólio profissional e gerar currículos no formato ATS (Applicant Tracking System). A API permite o cadastro de informações pessoais, experiências profissionais, competências, conquistas, idiomas e instruções, além de fornecer um endpoint para gerar um currículo completo em formato JSON.

## Funcionalidades

- **Cadastro de Pessoas**: Registro de informações pessoais, como nome, CPF, endereço, telefone, e-mail e redes sociais.
- **Gerenciamento de Instruções**: Cadastro de graus de instrução (ensino médio, graduação, pós-graduação, etc.).
- **Experiências Profissionais**: Registro de experiências profissionais, incluindo empresa, cargo, período e atividades realizadas.
- **Competências**: Cadastro de competências e conhecimentos (ex: Java, Angular, SQL).
- **Conquistas**: Registro de cursos, certificações e outras conquistas.
- **Idiomas**: Cadastro de idiomas e níveis de proficiência.
- **Geração de Currículo**: Endpoint para montar um currículo completo em formato JSON, combinando todas as informações cadastradas.

## Tecnologias Utilizadas

- **.NET 8**: Framework para desenvolvimento da API.
- **MongoDB**: Banco de dados NoSQL para armazenamento das informações.
- **AutoMapper**: Biblioteca para mapeamento de objetos entre DTOs e models.
- **Swagger**: Documentação interativa da API.

## Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (local ou MongoDB Atlas)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

### Passos para Configuração

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/PortfolioBuilder.git
   cd PortfolioBuilder
Configure o MongoDB:

Crie um banco de dados no MongoDB (local ou no MongoDB Atlas).

Atualize a connection string no arquivo appsettings.json:

json
Copy
{
  "ConnectionStrings": {
    "MongoDB": "sua_connection_string_aqui"
  }
}
Restauração de Pacotes:

Restaure os pacotes NuGet:

bash
Copy
dotnet restore
Executar a Aplicação:

Execute a aplicação:

bash
Copy
dotnet run
A API estará disponível em https://localhost:5001.

Acessar o Swagger:

Abra o navegador e acesse https://localhost:5001/swagger para visualizar e testar os endpoints da API.

Endpoints da API
Pessoas
GET /api/pessoa/Listar: Lista todas as pessoas cadastradas.

GET /api/pessoa/BuscarPorId/{id}: Busca uma pessoa pelo ID.

POST /api/pessoa/Registrar: Cadastra uma nova pessoa.

PUT /api/pessoa/Atualizar/{id}: Atualiza os dados de uma pessoa.

DELETE /api/pessoa/Remover/{id}: Remove uma pessoa e todas as entidades relacionadas.

Instruções
GET /api/instrucao/PorPessoa/{pessoaId}: Lista as instruções de uma pessoa.

POST /api/instrucao: Cadastra uma nova instrução.

PUT /api/instrucao/{id}: Atualiza uma instrução.

DELETE /api/instrucao/RemoverInstrucao/{id}: Remove uma instrução.

Experiências Profissionais
GET /api/experiencia: Lista todas as experiências profissionais.

GET /api/experiencia/PorPessoa/{pessoaId}: Lista as experiências de uma pessoa.

POST /api/experiencia: Cadastra uma nova experiência profissional.

PUT /api/experiencia/{id}: Atualiza uma experiência profissional.

DELETE /api/experiencia/RemoverExperiencia/{id}: Remove uma experiência profissional.

Competências
GET /api/competencia/PorPessoa/{pessoaId}: Lista as competências de uma pessoa.

POST /api/competencia: Cadastra uma nova competência.

PUT /api/competencia/{id}: Atualiza uma competência.

DELETE /api/competencia/{id}: Remove uma competência.

Conquistas
GET /api/conquista/PorPessoa/{pessoaId}: Lista as conquistas de uma pessoa.

POST /api/conquista: Cadastra uma nova conquista.

PUT /api/conquista/{id}: Atualiza uma conquista.

DELETE /api/conquista/{id}: Remove uma conquista.

Idiomas
GET /api/idioma/PorPessoa/{pessoaId}: Lista os idiomas de uma pessoa.

POST /api/idioma: Cadastra um novo idioma.

PUT /api/idioma/{id}: Atualiza um idioma.

DELETE /api/idioma/{id}: Remove um idioma.

Currículo
GET /api/pessoa/MontarCurriculo/{id}: Retorna um JSON com todas as informações da pessoa para montar o currículo.

Exemplo de Uso
Gerar Currículo
Para gerar o currículo de uma pessoa, faça uma requisição GET para o endpoint:

Copy
GET /api/pessoa/MontarCurriculo/{id}
Exemplo de resposta:

json
Copy
{
  "pessoa": {
    "nome": "Maurício Dias de Carvalho Oliveira",
    "cpf": "793.311.571-34",
    "endereco": {
      "rua": "Av. Aurora Rassi - Quadra 53 Lote",
      "numero": "19",
      "cidade": "Guapó",
      "estado": "GO",
      "cep": "75.350-000"
    },
    "telefone": "+55 (61) 99398-3844",
    "email": "mauridf@gmail.com",
    "redesSociais": [
      {
        "nomeRedeSocial": "LinkedIn",
        "link": "https://www.linkedin.com/in/mdcoliveira/"
      }
    ]
  },
  "instrucoes": [
    {
      "nivel": "Graduação",
      "instituicao": "Universidade Exemplo",
      "mesAnoInicio": "03/2015",
      "mesAnoTermino": "12/2019",
      "nomeCurso": "Ciência da Computação"
    }
  ],
  "competencias": [
    {
      "tipo": "Backend",
      "nome": "Java"
    }
  ],
  "experienciasProfissionais": [
    {
      "nomeEmpresa": "Empresa Exemplo",
      "cargo": "Desenvolvedor Backend",
      "mesAnoInicio": "01/2020",
      "mesAnoTermino": "09/2023",
      "atividades": "Desenvolvimento de APIs RESTful.",
      "projetos": "Migração para microserviços.",
      "competencias": [
        {
          "tipo": "Backend",
          "nome": "Java"
        }
      ]
    }
  ],
  "conquistas": [
    {
      "nome": "Certificação AWS Solutions Architect"
    }
  ],
  "idiomas": [
    {
      "nome": "Inglês",
      "nivel": "Avançado"
    }
  ]
}
Contribuição
Contribuições são bem-vindas! Siga os passos abaixo:

Faça um fork do projeto.

Crie uma branch para sua feature (git checkout -b feature/nova-feature).

Commit suas mudanças (git commit -m 'Adicionando nova feature').

Push para a branch (git push origin feature/nova-feature).

Abra um Pull Request.

Licença
Este projeto está licenciado sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

Copy

### **Link para Download**

Você pode baixar o arquivo README.md clicando no link abaixo:

[Download README.md](https://files.fm/u/8z5v3q9v4z)
O link não está funcionando, pode gerar novamente para mim por favor? Pois o conteudo veio todo desconfigurado ai não tive nem como copiar e colar
Peço desculpas pelo inconveniente! Vou disponibilizar o conteúdo do README.md diretamente aqui para que você possa copiar e colar. Se precisar de um arquivo para download, você pode copiar o conteúdo abaixo, colar em um arquivo .md no seu computador e salvá-lo como README.md.

Conteúdo do README.md
markdown
Copy
# PortfolioBuilder API

A **PortfolioBuilder API** é uma aplicação backend desenvolvida em .NET 8 com MongoDB, projetada para ajudar usuários a montar um portfólio profissional e gerar currículos no formato ATS (Applicant Tracking System). A API permite o cadastro de informações pessoais, experiências profissionais, competências, conquistas, idiomas e instruções, além de fornecer um endpoint para gerar um currículo completo em formato JSON.

## Funcionalidades

- **Cadastro de Pessoas**: Registro de informações pessoais, como nome, CPF, endereço, telefone, e-mail e redes sociais.
- **Gerenciamento de Instruções**: Cadastro de graus de instrução (ensino médio, graduação, pós-graduação, etc.).
- **Experiências Profissionais**: Registro de experiências profissionais, incluindo empresa, cargo, período e atividades realizadas.
- **Competências**: Cadastro de competências e conhecimentos (ex: Java, Angular, SQL).
- **Conquistas**: Registro de cursos, certificações e outras conquistas.
- **Idiomas**: Cadastro de idiomas e níveis de proficiência.
- **Geração de Currículo**: Endpoint para montar um currículo completo em formato JSON, combinando todas as informações cadastradas.

## Tecnologias Utilizadas

- **.NET 8**: Framework para desenvolvimento da API.
- **MongoDB**: Banco de dados NoSQL para armazenamento das informações.
- **AutoMapper**: Biblioteca para mapeamento de objetos entre DTOs e models.
- **Swagger**: Documentação interativa da API.

## Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (local ou MongoDB Atlas)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

### Passos para Configuração

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/PortfolioBuilder.git
   cd PortfolioBuilder
Configure o MongoDB:

Crie um banco de dados no MongoDB (local ou no MongoDB Atlas).

Atualize a connection string no arquivo appsettings.json:

json
Copy
{
  "ConnectionStrings": {
    "MongoDB": "sua_connection_string_aqui"
  }
}
Restauração de Pacotes:

Restaure os pacotes NuGet:

bash
Copy
dotnet restore
Executar a Aplicação:

Execute a aplicação:

bash
Copy
dotnet run
A API estará disponível em https://localhost:5001.

Acessar o Swagger:

Abra o navegador e acesse https://localhost:5001/swagger para visualizar e testar os endpoints da API.

Endpoints da API
Pessoas
GET /api/pessoa/Listar: Lista todas as pessoas cadastradas.

GET /api/pessoa/BuscarPorId/{id}: Busca uma pessoa pelo ID.

POST /api/pessoa/Registrar: Cadastra uma nova pessoa.

PUT /api/pessoa/Atualizar/{id}: Atualiza os dados de uma pessoa.

DELETE /api/pessoa/Remover/{id}: Remove uma pessoa e todas as entidades relacionadas.

Instruções
GET /api/instrucao/PorPessoa/{pessoaId}: Lista as instruções de uma pessoa.

POST /api/instrucao: Cadastra uma nova instrução.

PUT /api/instrucao/{id}: Atualiza uma instrução.

DELETE /api/instrucao/RemoverInstrucao/{id}: Remove uma instrução.

Experiências Profissionais
GET /api/experiencia: Lista todas as experiências profissionais.

GET /api/experiencia/PorPessoa/{pessoaId}: Lista as experiências de uma pessoa.

POST /api/experiencia: Cadastra uma nova experiência profissional.

PUT /api/experiencia/{id}: Atualiza uma experiência profissional.

DELETE /api/experiencia/RemoverExperiencia/{id}: Remove uma experiência profissional.

Competências
GET /api/competencia/PorPessoa/{pessoaId}: Lista as competências de uma pessoa.

POST /api/competencia: Cadastra uma nova competência.

PUT /api/competencia/{id}: Atualiza uma competência.

DELETE /api/competencia/{id}: Remove uma competência.

Conquistas
GET /api/conquista/PorPessoa/{pessoaId}: Lista as conquistas de uma pessoa.

POST /api/conquista: Cadastra uma nova conquista.

PUT /api/conquista/{id}: Atualiza uma conquista.

DELETE /api/conquista/{id}: Remove uma conquista.

Idiomas
GET /api/idioma/PorPessoa/{pessoaId}: Lista os idiomas de uma pessoa.

POST /api/idioma: Cadastra um novo idioma.

PUT /api/idioma/{id}: Atualiza um idioma.

DELETE /api/idioma/{id}: Remove um idioma.

Currículo
GET /api/pessoa/MontarCurriculo/{id}: Retorna um JSON com todas as informações da pessoa para montar o currículo.

Contribuição
Contribuições são bem-vindas! Siga os passos abaixo:

Faça um fork do projeto.

Crie uma branch para sua feature (git checkout -b feature/nova-feature).

Commit suas mudanças (git commit -m 'Adicionando nova feature').

Push para a branch (git push origin feature/nova-feature).

Abra um Pull Request.

Licença
Este projeto está licenciado sob a licença MIT. 