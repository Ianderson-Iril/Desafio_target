-- Seed gerado automaticamente
-- Tabela: estoque
INSERT INTO estoque (ID, NomeProduto, quantidade) VALUES (1, 'Caneta Azul', 1236.0);
INSERT INTO estoque (ID, NomeProduto, quantidade) VALUES (2, 'Caderno Universitário', 75.0);
INSERT INTO estoque (ID, NomeProduto, quantidade) VALUES (3, 'Borracha Branca', 200.0);
INSERT INTO estoque (ID, NomeProduto, quantidade) VALUES (4, 'Lápis Preto HB', 320.0);
INSERT INTO estoque (ID, NomeProduto, quantidade) VALUES (5, 'Marcador de Texto Amarelo', 658.0);

-- Tabela: MovimentoEstoque
INSERT INTO MovimentoEstoque (id, tipoMovimento, quantidade, CodItem) VALUES (1, 'Saida', 150.0, 1);
INSERT INTO MovimentoEstoque (id, tipoMovimento, quantidade, CodItem) VALUES (2, 'Entrada', 1236.0, 1);
INSERT INTO MovimentoEstoque (id, tipoMovimento, quantidade, CodItem) VALUES (3, 'Entrada', 132.0, 5);
INSERT INTO MovimentoEstoque (id, tipoMovimento, quantidade, CodItem) VALUES (4, 'Entrada', 436.0, 5);
