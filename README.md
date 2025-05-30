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
- Banco de Dados: [definir: PostgreSQL / SQL Server / etc.]

---

## APIs Disponíveis

| Rota                                | Descrição                               |
|-------------------------------------|------------------------------------------|
| `POST /api/clientes`                | Cadastro de cliente                      |
| `GET /api/clientes/cpf/{cpf}`       | Identificação via CPF                    |
| `POST /api/produtos`                | Criar novo produto                       |
| `PUT /api/produtos/{id}`            | Editar produto                           |
| `DELETE /api/produtos/{id}`         | Remover produto                          |
| `GET /api/produtos/categoria/{cat}` | Buscar produtos por categoria            |
| `POST /api/pedidos/checkout`        | Fake checkout (enviar para a fila)       |
| `GET /api/pedidos`                  | Listar pedidos em andamento              |

> Swagger disponível em: `http://localhost:5000/swagger`

---

## Docker

### Executar com Docker Compose:

```bash
docker-compose up --build
docker-compose --env-file .env.development up --build
```

### Integrantes do Gruop:
- Adriano Torini
- Andre Luiz
- Dhyogo Americo
- Filipe Braga
- Kauan Kajitani