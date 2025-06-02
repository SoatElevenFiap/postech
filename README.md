# FastFoodSystem - MVP - FASE 01
Projeto acadêmico desenvolvido para a disciplina de Arquitetura de Software (FIAP - Pós-graduação), com foco em modelagem baseada em DDD, Event Storming e arquitetura hexagonal.

## Visão Geral

Este sistema simula o fluxo completo de atendimento de um restaurante fast food, desde a identificação do cliente, montagem do pedido, pagamento, acompanhamento da preparação, até a entrega.

---

## Objetivos da Fase 01

- [x] Aplicar **Event Storming completo** com base nos conceitos da aula 6.
- [x] Utilizar **linguagem ubíqua**.
- [x] Implementar arquitetura **Hexagonal (Ports & Adapters)**.
- [x] Criar as **APIs obrigatórias**.
- [x] Disponibilizar o Swagger para testes.
- [x] Disponibilizar o sistema com **Docker** (App + Banco).

---

## Fluxos Modelados

### 1. Realização do pedido e pagamento
- Identificação (CPF, cadastro ou anônimo)
- Montagem de combo (lanche, acompanhamento, bebida, sobremesa)
- Envio do pedido via fake checkout
- Pagamento via QRCode (Mercado Pago - mock)

### 2. Preparação e entrega do pedido
- Pedido é enviado à cozinha
- Cliente acompanha status: Recebido → Em preparação → Pronto → Finalizado
- Cliente retira o pedido
- Sistema registra a finalização

---

## Arquitetura

- **Monolito modular**
- **Arquitetura Hexagonal (Ports and Adapters)**
- **Domain-Driven Design (DDD)**
  - Event Storming
  - Entidades, Objetos de Valor, Agregados
  - Linguagem Ubíqua

> **🎯 Event Storming:** Acesse nossa modelagem completa do domínio em [FastFood Event Storming](https://app.eraser.io/workspace/zO0ZqV5uHMeMpAL2bUdc)

### **Estrutura de Camadas:**

#### **Core (Núcleo da Aplicação)**
- **`Domain`** - Entidades de negócio, regras de domínio, contratos e interfaces do domínio
- **`Application`** - Casos de uso, portas de entrada e saída (Ports)

#### **Adapters (Adaptadores)**
- **`Application`** - Serviços, DTOs, mapeadores e validações
- **`Infrastructure`** - Repositórios, configurações de banco, modelagem EF
- **`API`** - Controllers, configurações, autenticação/autorização (Driving Adapters)

### **Fluxo da Arquitetura Hexagonal:**
```
API (Driving Adapter) → Core.Application (Ports) → Domain (Business Logic) → Infrastructure (Driven Adapter)
```

---

## Tecnologias

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- C#
- ASP.NET Core Web API
- Entity Framework Core
- Swagger (Swashbuckle)
- Docker + Docker Compose
- Banco de Dados: PostgreSQL
---

## APIs Disponíveis

### Autenticação
| Rota                                | Método | Descrição                               |
|-------------------------------------|--------|-----------------------------------------|
| `/api/Auth`                         | POST   | Login com usuário e senha               |
| `/api/Atendimento/token/anonimo`    | GET    | Token anônimo (sem auth)                |
| `/api/Atendimento/token/porCpf/{cpf}` | GET  | Token por CPF (sem auth)                |

### Usuários/Clientes
| Rota                                | Método | Descrição                               |
|-------------------------------------|--------|-----------------------------------------|
| `/api/Usuario/Cliente`              | POST   | Cadastro de cliente                     |
| `/api/Usuario/Cliente`              | PUT    | Atualizar cliente                       |
| `/api/Usuario/Cliente/PorCpf/{cpf}` | GET    | Identificação via CPF                   |
| `/api/Usuario/Administrador`        | POST   | Criar administrador                     |
| `/api/Usuario/Administrador`        | PUT    | Atualizar administrador                 |
| `/api/Usuario/Password`             | PUT    | Alterar senha                           |

### Categorias
| Rota                                | Método | Descrição                               |
|-------------------------------------|--------|-----------------------------------------|
| `/api/Categoria`                    | GET    | Listar categorias                       |
| `/api/Categoria/{id}`               | GET    | Obter categoria por ID                  |
| `/api/Categoria`                    | POST   | Criar nova categoria                    |
| `/api/Categoria/{id}`               | PUT    | Editar categoria                        |
| `/api/Categoria/{id}`               | DELETE | Desativar categoria                     |
| `/api/Categoria/{id}/reativar`      | POST   | Reativar categoria                      |

### Produtos
| Rota                                | Método | Descrição                               |
|-------------------------------------|--------|-----------------------------------------|
| `/api/Produto`                      | GET    | Listar produtos                         |
| `/api/Produto/{id}`                 | GET    | Obter produto por ID                    |
| `/api/Produto`                      | POST   | Criar novo produto                      |
| `/api/Produto/{id}`                 | PUT    | Editar produto                          |
| `/api/Produto/{id}`                 | DELETE | Desativar produto                       |
| `/api/Produto/{id}/reativar`        | POST   | Reativar produto                        |
| `/api/Produto/{id}/imagem`          | POST   | Upload de imagem                        |
| `/api/Produto/{id}/imagem`          | DELETE | Remover imagem                          |

### Pedidos
| Rota                                      | Método | Descrição                               |
|-------------------------------------------|--------|-----------------------------------------|
| `/api/Pedido`                             | POST   | Criar pedido                            |
| `/api/Pedido`                             | GET    | Listar pedidos (admin)                  |
| `/api/Pedido/{id}`                        | GET    | Obter pedido por ID                     |
| `/api/Pedido/{id}`                        | PUT    | Atualizar pedido                        |
| `/api/Pedido/{id}/pagar`                  | POST   | Processar pagamento                     |
| `/api/Pedido/{id}/iniciar-preparacao`     | POST   | Iniciar preparação (admin)              |
| `/api/Pedido/{id}/finalizar-preparacao`   | POST   | Finalizar preparação (admin)            |
| `/api/Pedido/{id}/finalizar`              | POST   | Finalizar pedido (admin)                |
| `/api/Pedido/{id}/cancelar`               | POST   | Cancelar pedido                         |

> **Swagger disponível em:** `http://localhost:5000/swagger`

---

## Docker

### Estrutura de Containerização

O projeto utiliza uma arquitetura multi-container com:

#### **Dockerfile**
Arquivo principal para build da aplicação .NET:
- **Stage 1 (build-env):** Compilação e publicação da aplicação
- **Stage 2 (migrator):** Preparação do ambiente para migrações EF Core
- **Stage 3 (final):** Runtime otimizado com ASP.NET Core

```dockerfile
# Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# Restore de dependências
# Compilação e publicação

# Ambiente para migrações
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrator
# Instalação do dotnet-ef tool

# Runtime final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# Apenas os binários necessários para execução
```

#### **Docker Compose (docker-compose.yml)**
Orquestração de 3 serviços:

1. **`db`** - PostgreSQL 16
   - Banco de dados principal
   - Volume persistente para dados
   - Healthcheck automático
   - Rede interna `FastFood`

2. **`migrator`** - Aplicação de Migrações
   - Executa `dotnet ef database update`
   - Depende do serviço `db` estar saudável
   - Container temporário (executa e finaliza)

3. **`app`** - API FastFood
   - Aplicação .NET principal
   - Porta 8080 exposta
   - Depende de `db` (saudável) e `migrator` (concluído)

### Executar com Docker Compose:

```bash
# Executar com arquivo .env padrão
docker-compose up --build

# Executar com arquivo .env específico
docker-compose --env-file .env.development up --build

# Executar em background (detached)
docker-compose up -d --build

# Parar todos os serviços
docker-compose down
```

### Variáveis de Ambiente
Configure no arquivo `.env` ou `.env.development`:

```env
# PostgreSQL
POSTGRES_HOST=db
POSTGRES_PORT=5432
POSTGRES_USER=admin
POSTGRES_PASSWORD=admin123
POSTGRES_DB=fastfood_db
POSTGRES_CONNECTION_STRING=Host=db;Port=5432;Database=fastfood_db;Username=admin;Password=admin123;

# ASP.NET Core
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080
```

### Acessos após Docker Compose:
- **API:** http://localhost:8080
- **Swagger:** http://localhost:8080/swagger
- **PostgreSQL:** localhost:5432

### Comandos Docker Úteis:

```bash
# Ver logs de um serviço específico
docker-compose logs app
docker-compose logs db

# Executar comandos dentro do container
docker-compose exec app bash
docker-compose exec db psql -U admin -d fastfood_db

# Rebuild apenas um serviço
docker-compose build app
docker-compose up app

# Verificar status dos serviços
docker-compose ps
```

> **📖 Guia Completo de Desenvolvimento:** Para instruções detalhadas de configuração do ambiente, banco de dados, migrações e solução de problemas, consulte o [Guia de Ambiente de Desenvolvimento](docs/ambiente-desenvolvimento.md).

### Integrantes do Grupo:
- Adriano Torini
- Andre Luiz
- Dhyogo Americo
- Filipe Braga
- Kauan Kajitani