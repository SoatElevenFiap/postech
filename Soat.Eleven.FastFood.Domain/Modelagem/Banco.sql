-- Tabela de perfis de usuário (admin, usuario comum)
CREATE TABLE Perfis (
    Id INT PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL UNIQUE
);

-- Preenchimento dos perfis
INSERT INTO Perfis (PerfilId, Nome)
VALUES 
    (1, 'administrador'),
    (2, 'usuario');

-- Usuários do sistema (administrador, usuário comum)
CREATE TABLE UsuarioSistema (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(255) NOT NULL,
    PerfilId INT NOT NULL,
    CriadoEm DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (PerfilId) REFERENCES Perfis(PerfilId)
);

-- Clientes que fazem pedidos
CREATE TABLE Cliente (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UsuarioId UNIQUEIDENTIFIER,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Telefone NVARCHAR(20),
    CriadoEm DATETIME NOT NULL DEFAULT GETDATE()
);


-- Categorias de produtos (Lanches, Bebidas, Sobremesas etc.)
CREATE TABLE CategoriaProduto (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100) NOT NULL UNIQUE,
    Descricao NVARCHAR(255)
);

-- Produtos (cadastrados pelos usuários do sistema)
CREATE TABLE Produto (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100) NOT NULL,
    SKU NVARCHAR(12) NOT NULL,
    Descricao NVARCHAR(255),
    Preco DECIMAL(10,2) NOT NULL,
    CategoriaId UNIQUEIDENTIFIER NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    CriadoEm DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CategoriaId) REFERENCES CategoriasProduto(CategoriaId)
);

-- Pedidos feitos pelos clientes
CREATE TABLE Pedido (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClienteId UNIQUEIDENTIFIER NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    SenhaPedido NVARCHAR(12) NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    Subtotal DECIMAL(10,2) NOT NULL,
    Desconto DECIMAL(10,2) NOT NULL DEFAULT 0,
    Total DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

-- Comandas associadas aos clientes
CREATE TABLE Comanda (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClienteId UNIQUEIDENTIFIER NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CriadaEm DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

-- Itens do pedido (vinculados ao produto)
CREATE TABLE ItemPedido (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PedidoId UNIQUEIDENTIFIER NOT NULL,
    ProdutoId UNIQUEIDENTIFIER NOT NULL,
    Quantidade INT NOT NULL,
    PrecoComDesconto DECIMAL(10,2) NULL;
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(PedidoId),
    FOREIGN KEY (ProdutoId) REFERENCES Produtos(ProdutoId)
);

CREATE TABLE LogPedido (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PedidoId UNIQUEIDENTIFIER NOT NULL,
    StatusAtual NVARCHAR(50) NOT NULL,
    AlteradoPor UNIQUEIDENTIFIER,
    IP NVARCHAR(45), -- IPv6 compatível
    DataAlteracao DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(PedidoId),
    FOREIGN KEY (AlteradoPor) REFERENCES UsuariosSistema(UsuarioId)
);


-- Criação da tabela DescontosProduto
CREATE TABLE DescontoProduto (
    Nome NVARCHAR(50) NOT NULL UNIQUE,
    DescontoProdutoId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProdutoId UNIQUEIDENTIFIER NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,  -- porcentagem
    DataInicio DATETIME NOT NULL,
    DataFim DATETIME NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    CriadoEm DATETIME NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (ProdutoId) REFERENCES Produtos(ProdutoId),
    FOREIGN KEY (TipoDescontoId) REFERENCES TiposDesconto(TipoDescontoId)
);


