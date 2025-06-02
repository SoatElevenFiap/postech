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
- **Camadas:**
  - `Domain`
  - `Application`
  - `Infrastructure`
  - `API` (Apresentação)

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

### Executar com Docker Compose:

```bash
docker-compose up --build
docker-compose --env-file .env.development up --build
```

### Integrantes do Grupo:
- Adriano Torini
- Andre Luiz
- Dhyogo Americo
- Filipe Braga
- Kauan Kajitani