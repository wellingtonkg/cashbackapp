CashbackApp - API
=================
Objetivo: Disponibilizar API REST contendo as especificações abaixo:

- Consultar o catálogo de discos de forma paginada, filtrando por gênero e ordenando de forma crescente pelo nome do disco;
- Consultar o disco pelo seu identificador;
- Consultar todas as vendas efetuadas de forma paginada, filtrando pelo range de datas (inicial e final) da venda e ordenando de forma decrescente pela data da venda;
- Consultar uma venda pelo seu identificador;
- Registrar uma nova venda de discos calculando o valor total de cashback.
- Integrar com a API do SPotify para alimenar a coleção de Discos.

Como rodar a aplicação
=======================
- Utilizar a versão 2.1 do SDK (https://dot.net/core)
- Restaurar os pacotes
- Executar a aplicação através do visual studio e após iniciar será redirecionado para o Swagger, ou acesse a url: http://localhost:14275/swagger/index.html

Ferramentas de CI
=================
- Travis (Para executar Tests)

Tecnologias Utilizadas
======================
.NET Core 2.1
ASP.NET WebApi Core
Entity Framework Core 2.1
Entity Framework Core 2.1 (InMemory)
Swagger

Arquitetura
===========
- Dependency Injection
- Domain Driven Design (Camadas and Domain Model Pattern)
- Repository
- Services
- Abstract 
- TDD

Soluções Externas
=================
- Spotify API
- Spotify Credentials (appsettings.json)


[![Build Status](https://travis-ci.org/wellingtonkg/cashbackapp.svg?branch=master)](https://travis-ci.org/AaronLenoir/flaclibsharp)