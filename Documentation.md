# Documentação do Projeto CRUD - Teste Técnico .NET

## Estrutura do Projeto
O projeto `TesteTecnicoDBM` segue uma arquitetura em camadas, separando responsabilidades e aderindo aos princípios de SOLID. A estrutura é organizada da seguinte forma:

- **Backend/WebAPI**:
  - Camada de apresentação e Web API.
  - Contém os controladores REST para operações CRUD da entidade `Produto`.
- **Backend/Application**:
  - Contém a lógica de aplicação, serviços e orquestração entre as camadas.
  - Inclui validações implementadas com FluentValidation.
- **Backend/Domain**:
  - Define a entidade `Produto` e as regras de negócio.
- **Backend/Infraestructure**:
  - Gerencia o acesso ao banco de dados com Entity Framework Core e o padrão Repository.
  - Contém as migrações de banco de dados utilizando FluentMigrator.
- **Frontend/mvc**:
  - Interface MVC básica para interação com a Web API.
  - Permite cadastro, listagem, atualização e exclusão de produtos.
- **Shared/Communication**:
  - Componentes compartilhados para comunicação entre camadas, como DTOs.
- **Shared/Exceptions**:
  - Exceções personalizadas para tratamento de erros consistentes.

## Responsabilidades das Camadas
- **Backend/WebAPI**: Expõe endpoints REST (ex.: `POST /api/produtos`, `GET /api/produtos/{id}`) e gerencia requisições HTTP.
- **Backend/Application**: Centraliza a lógica de negócio, como validações e chamadas ao repositório.
- **Backend/Domain**: Define a entidade `Produto` (campos: `Id`, `Nome`, `Descricao`, `Preco`, `DataCadastro`).
- **Backend/Infraestructure**: Abstrai o acesso ao banco com EF Core e aplica migrações com FluentMigrator.
- **Frontend/mvc**: Interface de usuário para interação com a API, consumindo endpoints de forma desacoplada.
- **Shared**: Fornece utilitários reutilizáveis, como DTOs e exceções customizadas.

## Escolha de Tecnologias e Padrões
- **ASP.NET Core**: Escolhido por sua performance, suporte a APIs REST e integração nativa com .NET.
- **Entity Framework Core**: ORM robusto para mapeamento objeto-relacional, facilitando o CRUD.
- **FluentMigrator**: Permite controle granular e versionamento das migrações de banco de dados.
- **FluentValidation**: Simplifica a validação declarativa e reutilizável (ex.: `Nome` obrigatório, `Preco` > 0).
- **xUnit**: Framework de testes leve e amplamente adotado na comunidade .NET.
- **SOLID**: Aplicado para garantir separação de responsabilidades (SRP), extensibilidade (OCP) e injeção de dependências (DIP).
- **Repository Pattern**: Usado em `Infraestructure` para abstrair o acesso a dados e facilitar testes.
- **Bogus**: Usado para criar os builder de dtos e entitdades randomicas

## Desafios Encontrados e Soluções
1. **Dockerização**:
   - **Desafio**: Gerar as imagens e subir um container com as duas aplicaçãoes se comunicando.
   - **Solução**: Realizado um estudo de como funciona o docker e como poderia ser feito a comunicação das imagens.

## Plano de Testes
Os testes unitários foram implementados no projeto `TesteTecnicoDBM.Tests` (ou similar) com xUnit e cobrem os seguintes cenários:

1. **Validações (FluentValidation)**:
   - `Nome` é obrigatório e não pode exceder 100 caracteres.
   - `Nome` não pode ser duplicado (verificação simulada no repositório).
   - `Preco` deve ser maior que zero e não nulo.
2. **Operações CRUD (Repository e Service)**:
   - **Create**: Testa a inserção de um produto válido e falha com dados inválidos.
   - **Read**: Verifica a recuperação de um produto por ID e listagem de todos os produtos.
   - **Update**: Confirma a atualização de um produto existente e falha com ID inválido.
   - **Delete**: Testa a exclusão de um produto e falha com ID inexistente.

Os testes utilizam mocks para isolar dependências externas (ex.: banco de dados) e garantir que a lógica de negócio esteja funcionando corretamente.