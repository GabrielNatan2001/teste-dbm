# Projeto CRUD - Teste Técnico .NET

## Descrição do Projeto
Este projeto é uma aplicação CRUD (Create, Read, Update, Delete) desenvolvida como parte de um teste técnico para a vaga de Desenvolvedor .NET Pleno. A aplicação gerencia uma entidade "Produto" e foi construída seguindo os princípios de SOLID, com uma arquitetura em camadas e boas práticas de desenvolvimento.

## Tecnologias Utilizadas
- **Backend**: ASP.NET Core (Web API)
- **ORM**: Entity Framework Core
- **Migrações**: FluentMigrator
- **Validações**: FluentValidation
- **Testes**: xUnit
- **Containerização**: Docker

## Pré-requisitos
- .NET 8.0 SDK
- Docker
- Git

## Links das imagens no docker hub
 - https://hub.docker.com/repository/docker/gabrielnatan2001/testetecnicodbm-api/general
 - https://hub.docker.com/repository/docker/gabrielnatan2001/testetecnicodbm-mvc/general

## Como Configurar e Executar Localmente
1. Clone o repositório:
    git clone https://github.com/GabrielNatan2001/teste-dbm.git
2. Restaure as dependências:
    dotnet restore
3. Execute os testes:
    dotnet test
4. Com o docker instalado na maquina rode o comando abaixo na raiz do projeto para iniciar as aplicações:
    docker-compose up -d

