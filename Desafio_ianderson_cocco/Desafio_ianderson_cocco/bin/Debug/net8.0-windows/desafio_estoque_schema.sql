CREATE TABLE "estoque" (
    "ID" INTEGER NOT NULL CONSTRAINT "PK_estoque" PRIMARY KEY AUTOINCREMENT,
    "NomeProduto" TEXT NOT NULL,
    "quantidade" TEXT NOT NULL
);


CREATE TABLE "MovimentoEstoque" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_MovimentoEstoque" PRIMARY KEY AUTOINCREMENT,
    "tipoMovimento" TEXT NOT NULL,
    "quantidade" TEXT NOT NULL,
    "CodItem" INTEGER NOT NULL
);


