# Tecnologias utilizadas
Para o projeto foi utilizado dotnet 8 para desenvolvimento das apis, docker para subir os containers necessários para rabbit e mssql.
Para realizar a comunicação entre os serviços foi utilizando o rabbitmq como message broker, para comunicar quando houvesse um novo lançamento e o saldo precisasse ser atualizado.
Uma parte de validação de healthcheck foi utilizado com reffit apenas para compreender se o serviço estava online para seguir com lançamentos ou retornar um 503, service unavailable.
Também para realizar a troca de mensagens request e response foi utilizado MediatR e Automapper para mapeamento dos objetos.


![dotnet](https://img.shields.io/badge/dotnet-8.0-blue)
![rabbit](https://img.shields.io/badge/rabbit-3.0-blue)
![mssql](https://img.shields.io/badge/mssql-2022-blue)

![alt text](/docs/architecture.png)

# Setup 
Para subir o projeto é necessário rodar o comando do docker-compose up -d na pasta raiz do projeto.
```
docker-compose up -d    
```

depois disso é necessário rodar as migrations referentes ao projeto de ms-lancamentos e ms-saldo. Para isso é necessário rodar o comando abaixo na pasta raiz do projeto.

Caso não tenha o dotnet ef instalado, é necessário instalar o pacote globalmente, por meio do comando a seguir:
```
dotnet tool install --global dotnet-ef
```

1. Migrations Lancamentos
```
dotnet ef database update --project Lancamentos.Infrastructure/Lancamentos.Infrastructure.csproj --startup-project Lancamentos.Api/Lancamentos.Api.csproj --context Lancamentos.Infrastructure.LancamentosDbContext --configuration Debug 20240909130847_Initial
```

2. Migrations Lancamentos
```
dotnet ef database update --project Saldo.Infrastructure/Saldo.Infrastructure.csproj --startup-project Saldo.Api/Saldo.Api.csproj --context Saldo.Infrastructure.SaldoDbContext --configuration Debug 20240909212824_Initial
```

# Subir o Projeto

Para niciar o projeto de Lancamentos.Api e Saldo.Api basta abrir 2 terminais ou configurar pelo visual studio ou vs code o multiple projects statup nas configurações.

1. Para rodar cada um via terminal basta rodar o comando a seguir:

#### Build solution
``` 
dotnet build
```

#### Lancamentos.Api:
```
dotnet run --project Lancamentos.Api/Lancamentos.Api.csproj --launch-profile https
```

#### Saldo.Api:
```
dotnet run --project Saldo.Api/Saldo.Api.csproj --launch-profile https
```

2. Após isso basta acessar o browser e navegar pelo swagger de cada aplicação para relizar as chamadas de inserir lançamento ou visualizar saldo consolidado.
O projeto de lançamento esta mapeado para se comunicar via https com o saldo, mas se preferir usar http, mas alterar a porta no appsettings.Development.json o "SaldoUrl".

Seguem Exemplos dentro da pasta Postman na raiz do projeto. Os arquivos podem ser importados no postman, e uma vez setado o environment, é possível rodar os testes.

#### Testes

Para rodar os testes basta rodar o comando a seguir na pasta raiz do projeto.
```
dotnet run tests
```