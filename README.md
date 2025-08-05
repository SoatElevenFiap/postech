# FastFoodSystem - MVP - FASE 01
Projeto acadêmico desenvolvido para a disciplina de Arquitetura de Software (FIAP - Pós-graduação), com foco em modelagem baseada em DDD, Event Storming e arquitetura hexagonal.

## Visão Geral

Este sistema simula o fluxo completo de atendimento de um restaurante fast food, desde a identificação do cliente, montagem do pedido, pagamento, acompanhamento da preparação, até a entrega.

---

## Objetivos da Fase 01

- [x] Aplicar **Event Storming completo** com base nos conceitos da aula 6.
- [x] Utilizar **linguagem ubíqua**.
- [x] Implementar arquitetura **Clean Architeture**.
- [x] Criar as **APIs obrigatórias**.
- [x] Disponibilizar o Swagger para testes.
- [x] Disponibilizar o sistema com **Kubernetes** (KIND + Docker).
- [x] Implementar **auto-scaling** com HPA (Horizontal Pod Autoscaler).
- [x] Configurar **Ingress** para acesso externo com domínio personalizado.

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
- **Clean Architeture**
- **Domain-Driven Design (DDD)**
  - Event Storming
  - Entidades, Objetos de Valor, Agregados
  - Linguagem Ubíqua

> **🎯 Event Storming:** Acesse nossa modelagem completa do domínio em [FastFood Event Storming](https://app.eraser.io/workspace/zO0ZqV5uHMeMpAL2bUdc)

**Diagrama de Infraestrutura do projeto (Junto ao Event Storm, canto inferior direito), considerando um Cluster Kubernetes:**

https://app.eraser.io/workspace/zO0ZqV5uHMeMpAL2bUdc

### **Estrutura de Camadas:**

#### **Camadas do Projeto**

- **API (`Soat.Eleven.FastFood.Api/`)**  
   Camada de apresentação, responsável pelos endpoints REST, autenticação/autorização, middlewares e configuração da Web API. Borda da aplicação.

- **Application (`Soat.Eleven.FastFood.Application/`)**  
   Implementa os controllers da Clean Architeture,faz a ponte entre a API e o core.

- **Core/Domain (`Soat.Eleven.FastFood.Core/`)**  
   Núcleo da aplicação,contendo Use Cases,Entidade,VOs, Enums, Interfaces de Gateways, Data Sources, Services

- **Infraestrutura (`Soat.Eleven.FastFood.Infra/`)**  
   Implementação concreta dos repositórios, gateways externos, persistência de dados (Entity Framework), integrações e configurações específicas de infraestrutura.

- **Testes (`Soat.Eleven.FastFood.Tests/`)**  
   Projeto dedicado a testes unitários e de integração das principais camadas.

> A estrutura segue os princípios de Clean Architecture, promovendo separação de responsabilidades, testabilidade e flexibilidade para evolução do sistema.

### **Fluxo da Arquitetura Hexagonal:**
```
API Rest → Controlles → Core (Business Logic) → Infrastructure (Driven Adapters)
```
---

## Tecnologias

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- C#
- ASP.NET Core Web API
- Entity Framework Core
- Swagger (Swashbuckle)
- **Kubernetes** (KIND - Kubernetes in Docker)
- **Docker** + **Docker Compose**
- **NGINX Ingress Controller**
- **PostgreSQL** (Banco de Dados)
- **Metrics Server** + **HPA** (Auto-scaling)
- **Persistent Volumes** (Armazenamento)

---

## Infraestrutura Kubernetes

### Componentes do Cluster

O projeto roda em um cluster Kubernetes local usando **KIND** com os seguintes componentes:

#### **📦 Namespace e Configurações**
- **`fastfood-namespace.yaml`** - Isola recursos da aplicação
- **`config-map.yaml`** - Variáveis de ambiente não-sensíveis
- **`secret.yaml`** - Credenciais do banco (usuário/senha)

#### **🗄️ Banco de Dados**
- **`db.yaml`** - Deploy do PostgreSQL
- **`db-service.yaml`** - Serviço que expõe o banco
- **`db-pvc.yaml`** - Volume persistente para dados

#### **🔄 Migrations**
- **`migrator-job.yaml`** - Job que executa Entity Framework migrations

#### **🚀 Aplicação**
- **`fastfood.yaml`** - Deploy da API .NET
- **`fastfood-service.yaml`** - Serviço interno (ClusterIP)

#### **🌐 Acesso Externo**
- **`fastfood-ingress-80.yaml`** - NGINX Ingress Controller
- **`fastfood-ingress.yaml`** - Regras de roteamento HTTP

#### **📈 Auto-scaling**
- **`metrics-server-kind.yaml`** - Coleta métricas de CPU/memória
- **`fastfood-hpa.yaml`** - Auto-scaling baseado em métricas

#### **⚙️ Configuração KIND**
- **`kind-config.yaml`** - Cluster local com port mapping

### Arquitetura da solução rodando dentro do AKS
```
Localhost(Client) (fastfood:80/443)
   │
   ▼
+-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    
| NGINX Ingress     |───▶| FastFood Service  |───▶| FastFood Pod(s)   |───▶| DB Service        |───▶| PostgreSQL Pod    |───▶| Persistent Volume 
+-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    
                             │
                             ▼
                    +-------------------+
                    |      HPA          |
                    | (Auto-scaling)    |
                    +-------------------+
```
### Arquitetura de recursos
```
+-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+
| KIND Cluster      | -> | Namespace         | -> | ConfigMap/Secret  | -> | PostgreSQL Deploy | -> | DB Service        | -> | Migrator Job      | -> | FastFood Deploy   | -> | Metrics Server    |
+-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+    +-------------------+

```

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

### Pagamento
| Rota                                      | Método | Descrição                               |
|-------------------------------------------|--------|-----------------------------------------|
| `/api/StatusPagamento`                             | GET    | Consulta Status do Pagamento   |

### Webhook
| Rota                                      | Método | Descrição                               |
|-------------------------------------------|--------|-----------------------------------------|
| `/Webhook/Pagamento/MercadoPago`          | Post    | Webhook para confirmação de pagamento  |



> **Swagger disponível em:** `http://fastfood/swagger` (após configurar hosts)

---

## Deploy e Execução (KIND)

[Veja o arquivo`/manifesto/README.md`](manifesto/README.md)


## Arquivos e Estrutura do Projeto

### **🎯 Estrutura Geral**
```
📁 postech/
├── 📁 src/                          # Código-fonte .NET
│   ├── 📄 Soat.Eleven.FastFood.sln     # Solution principal
│   ├── 📁 Soat.Eleven.FastFood.Api/    # Camada Web API 
│   ├── 📁 Soat.Eleven.FastFood.Application/ # Casos de Uso (Application)
│   ├── 📁 Soat.Eleven.FastFood.Core/   # Entidades e Regras (Domain)
│   └── 📁 Soat.Eleven.FastFood.Infra/  # Infraestrutura (Driven Adapters)
├── 📁 manifesto/                    # Manifests Kubernetes
├── 📁 docs/                         # Documentação
├── 📁 Soat.Eleven.FastFood.Tests/   # Testes unitários
├── 📄 docker-compose.yml           # Orquestração Docker Compose
├── 📄 Dockerfile                   # Build da aplicação
└── 📄 README.md                    # Este arquivo
```

#### **Camadas Detalhadas:**

1. **Domain (Core)** - `Soat.Eleven.FastFood.Core/`
   - **Entities:** Cliente, Produto, Pedido, Categoria
   - **Value Objects:** CPF, Email, Preco
   - **Enums:** StatusPedido, StatusPagamento, TipoPagamento
   - **Ports (Interfaces):** Contratos para repositórios e gateways
   - **Business Rules:** Regras de negócio puras

2. **Application** - `Soat.Eleven.FastFood.Application/`
   - **Controllers:** Orquestração dos casos de uso
   - **UseCases:** Lógica de aplicação específica
   - **DTOs:** Objetos de transferência de dados
   - **Presenters:** Formatação de dados para apresentação

3. **Adapters** - `Soat.Eleven.FastFood.Api/` + `Soat.Eleven.FastFood.Infra/`
   - **WebApi:** Endpoints REST, middleware, configurações
   - **Infrastructure:** Repositórios, gateways externos, persistence
   - **Data Sources:** Implementações concretas dos ports

### **🗂️ Kubernetes Manifests (`manifesto/`)**

| Arquivo | Função | Descrição |
|---------|--------|-----------|
| `fastfood-namespace.yaml` | **Namespace** | Isolamento lógico do ambiente |
| `secret.yaml` |  **Secrets** | Credenciais sensíveis (passwords, keys) |
| `config-map.yaml` |  **ConfigMap** | Configurações não-sensíveis da aplicação |
| `db-pvc.yaml` |  **PersistentVolume** | Armazenamento persistente do PostgreSQL |
| `db.yaml` |  **Database** | Deploy do PostgreSQL com volumes |
| `db-service.yaml` |  **DB Service** | Exposição interna do banco |
| `migrator-job.yaml` |  **Job** | Execução única das migrações EF Core |
| `deploy.yaml` |  **Deployment** | Deploy da aplicação .NET |
| `fastfood-service.yaml` |  **App Service** | Exposição interna da aplicação |
| `fastfood-ingress.yaml` | **Ingress (443)** | Acesso externo HTTPS |
| `fastfood-ingress-80.yaml` | **Ingress (80)** | Acesso externo HTTP |
| `metrics-server-kind.yaml` | **Metrics Server** | Coleta de métricas para HPA |
| `fastfood-hpa.yaml` | **HPA** | Auto-scaling baseado em CPU |
| `kind-config.yaml` | **KIND Config** | Configuração do cluster local |

## Licença

Este projeto é desenvolvido para fins educacionais como parte do curso de Arquitetura de Software da FIAP/Alura.

### Integrantes do Grupo:
- Adriano Torini
- Andre Luiz
- Dhyogo Americo
- Filipe Braga
- Kauan Kajitani
