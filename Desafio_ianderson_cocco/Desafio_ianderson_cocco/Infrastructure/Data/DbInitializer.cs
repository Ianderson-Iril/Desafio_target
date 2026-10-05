using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using Desafio_ianderson_cocco.Entities;
using Desafio_ianderson_cocco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Desafio_ianderson_cocco.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task InicializarBancoAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // Caminho do arquivo do banco 
            var dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "desafio_estoque.db");
            var dbDirectory = Path.GetDirectoryName(dbFilePath) ?? AppDomain.CurrentDomain.BaseDirectory;

            //  Gera script do modelo e grava ao lado do arquivo do banco
            try
            {
                var createScript = context.Database.GenerateCreateScript();
                var schemaFile = Path.Combine(dbDirectory, "desafio_estoque_schema.sql");
                await File.WriteAllTextAsync(schemaFile, createScript, Encoding.UTF8);
            }
            catch
            {
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!await context.estoque.AnyAsync())
            {
                string caminhoEstoque = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json", "estoque.json");
                if (File.Exists(caminhoEstoque))
                {
                    string jsonString = await File.ReadAllTextAsync(caminhoEstoque);
                    List<estoque>? listaEstoque = null;
                    try
                    {
                        listaEstoque = JsonSerializer.Deserialize<List<estoque>>(jsonString, options);
                    }
                    catch (JsonException) { }

                    if (listaEstoque == null)
                    {
                        try
                        {
                            using var doc = JsonDocument.Parse(jsonString);
                            var root = doc.RootElement;
                            JsonElement? arr = null;
                            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("estoque", out var prop) && prop.ValueKind == JsonValueKind.Array)
                                arr = prop;
                            else if (root.ValueKind == JsonValueKind.Array)
                                arr = root;
                            else
                            {
                                foreach (var p in root.EnumerateObject())
                                {
                                    if (p.Value.ValueKind == JsonValueKind.Array)
                                    {
                                        arr = p.Value;
                                        break;
                                    }
                                }
                            }

                            if (arr != null)
                            {
                                listaEstoque = new List<estoque>();
                                foreach (var item in arr.Value.EnumerateArray())
                                {
                                    var nome = string.Empty;
                                    var id = 0;
                                    decimal qtd = 0m;

                                    if (item.ValueKind == JsonValueKind.Object)
                                    {
                                        if (item.TryGetProperty("descricaoProduto", out var desc) && desc.ValueKind == JsonValueKind.String)
                                            nome = desc.GetString() ?? string.Empty;
                                        else if (item.TryGetProperty("NomeProduto", out var nomeP) && nomeP.ValueKind == JsonValueKind.String)
                                            nome = nomeP.GetString() ?? string.Empty;
                                        else if (item.TryGetProperty("descricao", out var desc2) && desc2.ValueKind == JsonValueKind.String)
                                            nome = desc2.GetString() ?? string.Empty;

                                        if (item.TryGetProperty("codigoProduto", out var cod) && cod.ValueKind == JsonValueKind.Number && cod.TryGetInt32(out var codInt))
                                            id = codInt;
                                        else if (item.TryGetProperty("ID", out var idp) && idp.ValueKind == JsonValueKind.Number && idp.TryGetInt32(out var idInt))
                                            id = idInt;

                                        if (item.TryGetProperty("estoque", out var q) && q.ValueKind == JsonValueKind.Number && q.TryGetDecimal(out var qDec))
                                            qtd = qDec;
                                        else if (item.TryGetProperty("quantidade", out var q2) && q2.ValueKind == JsonValueKind.Number && q2.TryGetDecimal(out var qDec2))
                                            qtd = qDec2;
                                    }

                                    if (string.IsNullOrWhiteSpace(nome))
                                        continue;

                                    listaEstoque.Add(new estoque
                                    {
                                        ID = id,
                                        NomeProduto = nome.Trim(),
                                        quantidade = qtd
                                    });
                                }
                            }
                        }
                        catch { }
                    }

                    if (listaEstoque != null && listaEstoque.Count > 0)
                    {
                        var finalList = listaEstoque
                                        .Where(x => !string.IsNullOrWhiteSpace(x.NomeProduto))
                                        .Select(x => new estoque
                                        {
                                            NomeProduto = x.NomeProduto.Trim(),
                                            quantidade = x.quantidade
                                        })
                                        .ToList();

                        if (finalList.Count > 0)
                            await context.estoque.AddRangeAsync(finalList);
                    }
                }
            }

            if (!await context.MovimentoEstoque.AnyAsync())
            {
                string caminhoMovimentos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json", "movimentos.json");
                if (File.Exists(caminhoMovimentos))
                {
                    string jsonString = await File.ReadAllTextAsync(caminhoMovimentos);

                    List<MovimentoEstoque>? listaMovimentos = null;
                    try
                    {
                        listaMovimentos = JsonSerializer.Deserialize<List<MovimentoEstoque>>(jsonString, options);
                    }
                    catch (JsonException) { }

                    if (listaMovimentos == null)
                    {
                        try
                        {
                            using var doc = JsonDocument.Parse(jsonString);
                            var root = doc.RootElement;
                            JsonElement? arr = null;
                            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("movimentos", out var prop) && prop.ValueKind == JsonValueKind.Array)
                                arr = prop;
                            else if (root.ValueKind == JsonValueKind.Array)
                                arr = root;
                            else
                            {
                                foreach (var p in root.EnumerateObject())
                                {
                                    if (p.Value.ValueKind == JsonValueKind.Array)
                                    {
                                        arr = p.Value;
                                        break;
                                    }
                                }
                            }

                            if (arr != null)
                            {
                                listaMovimentos = new List<MovimentoEstoque>();
                                foreach (var item in arr.Value.EnumerateArray())
                                {
                                    var tipo = string.Empty;
                                    var id = 0;
                                    decimal qtd = 0m;
                                    int codItem = 0;

                                    if (item.ValueKind == JsonValueKind.Object)
                                    {
                                        if (item.TryGetProperty("tipoMovimento", out var t) && t.ValueKind == JsonValueKind.String)
                                            tipo = t.GetString() ?? string.Empty;
                                        else if (item.TryGetProperty("tipo", out var t2) && t2.ValueKind == JsonValueKind.String)
                                            tipo = t2.GetString() ?? string.Empty;

                                        if (item.TryGetProperty("id", out var idp) && idp.ValueKind == JsonValueKind.Number && idp.TryGetInt32(out var idInt))
                                            id = idInt;

                                        if (item.TryGetProperty("quantidade", out var q) && q.ValueKind == JsonValueKind.Number && q.TryGetDecimal(out var qDec))
                                            qtd = qDec;
                                        else if (item.TryGetProperty("estoque", out var q2) && q2.ValueKind == JsonValueKind.Number && q2.TryGetDecimal(out var qDec2))
                                            qtd = qDec2;

                                        if (item.TryGetProperty("CodItem", out var ci) && ci.ValueKind == JsonValueKind.Number && ci.TryGetInt32(out var ciInt))
                                            codItem = ciInt;
                                        else if (item.TryGetProperty("codItem", out var ci2) && ci2.ValueKind == JsonValueKind.Number && ci2.TryGetInt32(out var ciInt2))
                                            codItem = ciInt2;
                                        else if (item.TryGetProperty("codigoProduto", out var cp) && cp.ValueKind == JsonValueKind.Number && cp.TryGetInt32(out var cpInt))
                                            codItem = cpInt;
                                    }

                                    if (string.IsNullOrWhiteSpace(tipo))
                                        continue;

                                    listaMovimentos.Add(new MovimentoEstoque
                                    {
                                        id = id,
                                        tipoMovimento = tipo.Trim(),
                                        quantidade = qtd,
                                        CodItem = codItem
                                    });
                                }
                            }
                        }
                        catch { }
                    }

                    if (listaMovimentos != null && listaMovimentos.Count > 0)
                    {
                        var finalList = listaMovimentos
                                        .Where(x => !string.IsNullOrWhiteSpace(x.tipoMovimento))
                                        .Select(x => new MovimentoEstoque
                                        {
                                            tipoMovimento = x.tipoMovimento.Trim(),
                                            quantidade = x.quantidade,
                                            CodItem = x.CodItem
                                        })
                                        .ToList();

                        if (finalList.Count > 0)
                            await context.MovimentoEstoque.AddRangeAsync(finalList);
                    }
                }
            }

            await context.SaveChangesAsync();

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("-- Seed gerado automaticamente");
                sb.AppendLine("-- Tabela: estoque");
                var estoques = await context.estoque.AsNoTracking().OrderBy(e => e.ID).ToListAsync();
                foreach (var e in estoques)
                {
                    var nomeEscaped = e.NomeProduto.Replace("'", "''");
                    sb.AppendLine($"INSERT INTO estoque (ID, NomeProduto, quantidade) VALUES ({e.ID}, '{nomeEscaped}', {e.quantidade.ToString(CultureInfo.InvariantCulture)});");
                }

                sb.AppendLine();
                sb.AppendLine("-- Tabela: MovimentoEstoque");
                var movimentos = await context.MovimentoEstoque.AsNoTracking().OrderBy(m => m.id).ToListAsync();
                foreach (var m in movimentos)
                {
                    var tipoEscaped = m.tipoMovimento.Replace("'", "''");
                    sb.AppendLine($"INSERT INTO MovimentoEstoque (id, tipoMovimento, quantidade, CodItem) VALUES ({m.id}, '{tipoEscaped}', {m.quantidade.ToString(CultureInfo.InvariantCulture)}, {m.CodItem});");
                }

                var seedFile = Path.Combine(dbDirectory, "desafio_estoque_seed.sql");
                await File.WriteAllTextAsync(seedFile, sb.ToString(), Encoding.UTF8);
            }
            catch
            {
            }
        }
    }
}