## AspnetCore Application Microservices:

## Environment:

* dotnet version in file `global.json`
* Visual Code/Studio
* Docker Desktop

## How to run project

Run command for build:
```Powershell
dotnet build
```

Go to folder contain file `docker-compose`

1. Using docker-compose
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
```

## Application URLS - LOCAL Environment (Docker Container):
- Product API: http://localhost:6002/api/products

## Docker Application URLS - LOCAL Environment (Docker Container):
- Portainer: http://localhost:9000 - username: admin; pass: 123Pa$$word!
- kibana: http://localhost:5601 - username: elastic; pass: admin
- RabbitMQ: http://localhost:15672 - username: guest; pass: guest

2. Using Visual Studio
- Open microservices.sln - `microservices.sln`
- Run Compound to start multi project:
---
## Application URLS - DEVELOPMENT Environment:
- Product API: http://localhost:5002/api/products
---
## Application URLS - PRODUCTION Environment:

---
## Packages References:

## Install Environment:

- https://dotnet.microsoft.com/download/dotnet/6.0
- https://visualstudio.microsoft.com/

## References URLS:

## Docker Commands:

- docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

## Useful Commands:

- ASPNETCORE_ENVIRONMENT=Production dotnet ef database update
- dotnet watch run --environment "Development"
- dotnet restore