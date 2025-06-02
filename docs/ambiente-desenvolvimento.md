# Configuração do Ambiente de Desenvolvimento

Este documento contém as instruções para configurar o ambiente de desenvolvimento do projeto FastFood System.

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [PostgreSQL](https://www.postgresql.org/download/) (opcional, pode usar via Docker)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

## Configuração do Banco de Dados

### Opção 1: PostgreSQL via Docker (Recomendado)

Execute o comando abaixo para criar e iniciar o container PostgreSQL:

```bash
docker run --name postgres \
  -e POSTGRES_USER=admin \
  -e POSTGRES_PASSWORD=admin123 \
  -e POSTGRES_DB=meubanco \
  -p 5432:5432 \
  -v pgdata:/var/lib/postgresql/data \
  -d postgres:16
```

### Opção 2: PostgreSQL Local

Se preferir instalar o PostgreSQL localmente:

1. Instale o PostgreSQL 16 ou superior
2. Crie um banco de dados chamado `meubanco`
3. Configure as credenciais conforme o arquivo de configuração

## Configuração da String de Conexão

No arquivo `appsettings.Development.json`, configure a string de conexão:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=meubanco;Username=admin;Password=admin123;"
  }
}
```

## Configuração do Entity Framework

### Aplicar Migrações Existentes

Para aplicar as migrações existentes ao banco de dados:

```bash
dotnet ef database update \
  --project Soat.Eleven.FastFood.Infra/Soat.Eleven.FastFood.Infra.csproj \
  --startup-project Soat.Eleven.FastFood.Api/Soat.Eleven.FastFood.Api.csproj \
  --context AppDbContext
```

### Criar Nova Migração

Quando necessário criar uma nova migração:

```bash
dotnet ef migrations add [NomeDaMigracao] \
  --project Soat.Eleven.FastFood.Infra/Soat.Eleven.FastFood.Infra.csproj \
  --startup-project Soat.Eleven.FastFood.Api/Soat.Eleven.FastFood.Api.csproj \
  --context AppDbContext
```

**Exemplos de nomes de migração:**
- `AdicionarTabelaPedidos`
- `AtualizarCamposProduto`
- `CorrigirRelacionamentoPagamento`

## Executando a Aplicação

### Via Visual Studio
1. Abra a solução no Visual Studio
2. Configure o projeto `Soat.Eleven.FastFood.Api` como projeto de inicialização
3. Pressione F5 ou clique em "Start"

### Via Linha de Comando
```bash
cd src/Soat.Eleven.FastFood.Api
dotnet run
```

### Via Docker Compose
```bash
docker-compose up --build
```

## Acessando a Aplicação

- **API:** http://localhost:5000
- **Swagger:** http://localhost:5000/swagger
- **HTTPS:** https://localhost:5001

## Dados Padrão do Sistema

### Usuário Administrador
- **Email:** `sistema@fastfood.com`
- **Senha:** `Senha@123`

### Banco de Dados
- **Host:** localhost
- **Porta:** 5432
- **Database:** meubanco
- **Usuário:** admin
- **Senha:** admin123

## Comandos Úteis

### Docker
```bash
# Parar o container PostgreSQL
docker stop postgres

# Iniciar o container PostgreSQL
docker start postgres

# Remover o container PostgreSQL
docker rm postgres

# Ver logs do container
docker logs postgres
```

### Entity Framework
```bash
# Verificar migrações pendentes
dotnet ef migrations list --project Soat.Eleven.FastFood.Infra/Soat.Eleven.FastFood.Infra.csproj --startup-project Soat.Eleven.FastFood.Api/Soat.Eleven.FastFood.Api.csproj --context AppDbContext

# Reverter migração
dotnet ef database update [NomeMigracaoAnterior] --project Soat.Eleven.FastFood.Infra/Soat.Eleven.FastFood.Infra.csproj --startup-project Soat.Eleven.FastFood.Api/Soat.Eleven.FastFood.Api.csproj --context AppDbContext

# Remover última migração
dotnet ef migrations remove --project Soat.Eleven.FastFood.Infra/Soat.Eleven.FastFood.Infra.csproj --startup-project Soat.Eleven.FastFood.Api/Soat.Eleven.FastFood.Api.csproj --context AppDbContext
```

## Solução de Problemas

### Erro de Conexão com Banco
1. Verifique se o container PostgreSQL está executando: `docker ps`
2. Teste a conexão: `docker exec -it postgres psql -U admin -d meubanco`
3. Verifique a string de conexão no `appsettings.json`

### Erro de Migração
1. Verifique se o banco está acessível
2. Execute `dotnet ef database drop` e `dotnet ef database update` para recriar
3. Certifique-se de estar na pasta raiz do projeto

### Porta em Uso
Se a porta 5000 estiver em uso, altere no arquivo `launchSettings.json`:
```json
"applicationUrl": "https://localhost:5001;http://localhost:5000"
```

## Estrutura do Projeto

```
src/
├── Soat.Eleven.FastFood.Api/          # Camada de Apresentação
├── Soat.Eleven.FastFood.Application/  # Camada de Aplicação
├── Soat.Eleven.FastFood.Domain/       # Camada de Domínio
├── Soat.Eleven.FastFood.Infra/        # Camada de Infraestrutura
└── Soat.Eleven.FastFood.Tests/        # Testes
```

## Próximos Passos

1. Configure o ambiente seguindo este guia
2. Execute as migrações
3. Teste os endpoints via Swagger
4. Explore a documentação da API
