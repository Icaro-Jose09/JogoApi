# JogoApi - Catálogo de Jogos (.NET + MongoDB)

Web API em .NET para gerenciar um catálogo de jogos de videogame, com persistência em MongoDB.

## Tecnologias
- .NET 9 (Web API)
- MongoDB.Driver
- Swagger

## Arquitetura
Controller > JogoService > IJogoRepository > MongoDB
Configuração via Options Pattern (`IOptions<MongoDbSettings>`).

## Como rodar
1. Subir o MongoDB com Docker:
```bash
   docker run -d -p 27017:27017 --name mongo-local mongo
```
2. Rodar a API:
```bash
   dotnet run
```
3. Abrir o Swagger: http://localhost:5115/swagger

A string de conexão fica em `appsettings.json` (`MongoDbSettings`).

## Endpoints
| Método | Rota | Descrição | Status |
|---|---|---|---|
| POST | /api/jogos | Cadastra jogo | 201, 400 |
| GET | /api/jogos | Lista jogos | 200 |
| GET | /api/jogos/{id} | Busca por id | 200, 400, 404 |
| PUT | /api/jogos/{id} | Atualiza jogo | 200, 400, 404 |
| DELETE | /api/jogos/{id} | Remove jogo | 204, 400, 404 |
| GET | /api/jogos/busca?plataforma=&precoMaximo= | Filtra por plataforma e preço máximo | 200 |
| GET | /api/jogos/relatorio-estoque | Relatório por plataforma (títulos e valor do inventário) | 200 |


## Alunos 
| Icaro J. Santos| --| 562403|
| Iago Liziero| -- | 564063|

