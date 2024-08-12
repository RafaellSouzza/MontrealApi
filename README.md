# MontrealApi - README

## Visão Geral

A **MontrealApi** é uma API RESTful desenvolvida em .NET Core 8.0 com o objetivo de gerenciar o cadastro de pessoas e usuários, incluindo funcionalidades de autenticação e autorização. Este projeto foi desenvolvido como parte de uma etapa técnica de avaliação, seguindo requisitos específicos descritos no documento de escopo fornecido.

## Arquitetura

A arquitetura do projeto segue o padrão de camadas, separando as responsabilidades em diferentes projetos e camadas dentro da solução. As principais camadas são:

1. **Camada de Apresentação (Controllers)**: Gerencia as requisições HTTP e coordena a lógica de resposta, implementando os endpoints RESTful. Esta camada é responsável por receber as requisições, invocar os serviços apropriados e devolver as respostas ao cliente.

2. **Camada de Serviços (Services)**: Contém a lógica de negócios da aplicação. Os serviços realizam operações necessárias sobre os dados e coordenam a comunicação entre os repositórios e os controllers.

3. **Camada de Repositórios (Repositories)**: Responsável pela persistência e acesso aos dados. A comunicação com o banco de dados é feita através do Entity Framework Core, que fornece mapeamento objeto-relacional (ORM).

4. **Camada de Modelos (Models)**: Define as entidades do domínio e os modelos de dados utilizados pela aplicação. Também inclui as definições das regras de validação.

5. **Camada de Configuração e Segurança**: Implementa a configuração de autenticação e autorização, gerenciando as roles de usuários e a geração de tokens JWT.

## Funcionalidades

### Pessoas
- **Criação**: Cadastro de novas pessoas com validações de nome, sobrenome, CPF, data de nascimento, sexo e foto.
- **Leitura**: Recuperação de dados das pessoas cadastradas, sem incluir as fotos.
- **Atualização**: Atualização dos dados de uma pessoa, mantendo o histórico das fotos e definindo a mais recente como principal.
- **Deleção**: Exclusão de registros de pessoas.
- **Pesquisa Paginada**: Busca de pessoas por nome, CPF, data de nascimento e sexo, com suporte à paginação.
- **Foto da Pessoa**: Recuperação da foto mais recente associada à pessoa.

### Usuários
- **Criação**: Cadastro de novos usuários com validações de nome de usuário, senha e role.
- **Leitura**: Recuperação de dados dos usuários cadastrados.
- **Atualização**: Atualização dos dados de um usuário.
- **Deleção**: Exclusão de registros de usuários.

### Autenticação e Autorização
- **Autenticação**: Implementação de autenticação utilizando JWT (JSON Web Token).
- **Autorização**: Configuração de roles (`Admin` e `User`) para controle de acesso. A role `Admin` possui acesso completo à API, enquanto a role `User` tem apenas permissões de leitura.

## Endpoints

A API segue os padrões REST e inclui os seguintes endpoints principais:

### Pessoa
- `POST /api/pessoa`: Cria uma nova pessoa.
- `GET /api/pessoa`: Recupera todas as pessoas (sem fotos).
- `GET /api/pessoa/{id}`: Recupera uma pessoa específica pelo ID.
- `PUT /api/pessoa/{id}`: Atualiza os dados de uma pessoa específica.
- `DELETE /api/pessoa/{id}`: Deleta uma pessoa específica.
- `GET /api/pessoa/paginado`: Realiza uma pesquisa paginada de pessoas.
- `GET /api/pessoa/{id}/foto`: Recupera a foto mais recente de uma pessoa.

### Usuário
- `POST /api/usuario`: Cria um novo usuário.
- `GET /api/usuario`: Recupera todos os usuários.
- `GET /api/usuario/{id}`: Recupera um usuário específico pelo ID.
- `PUT /api/usuario/{id}`: Atualiza os dados de um usuário específico.
- `DELETE /api/usuario/{id}`: Deleta um usuário específico.

### Autenticação
- `POST /api/auth/login`: Realiza a autenticação e retorna o token JWT.

## Modelagem e Persistência de Dados

Todos os modelos de dados foram definidos em entidades que são persistidas em um banco de dados relacional utilizando o Entity Framework Core. O banco de dados escolhido para este projeto foi o **SQL Server**, rodando em um container Docker, garantindo fácil configuração e isolamento do ambiente de desenvolvimento.

## Testes Unitários

O projeto inclui testes unitários para garantir a confiabilidade e a integridade das funcionalidades implementadas. Os testes foram desenvolvidos utilizando frameworks de testes compatíveis com .NET 8, assegurando que cada unidade de código funcione corretamente e de forma isolada.

## Considerações Finais

O projeto foi desenvolvido seguindo as melhores práticas de arquitetura e programação, com foco na separação de responsabilidades e na escalabilidade. A utilização de camadas bem definidas facilita a manutenção e a evolução da aplicação.