using Desafio_ianderson_cocco.Entities;
using Desafio_ianderson_cocco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desafio_ianderson_cocco
{
    public partial class frm_Principal : Form
    {
        public frm_Principal()
        {
            InitializeComponent();

            this.Load += frm_Principal_Load;
        }

        private async void frm_Principal_Load(object? sender, EventArgs e)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlite("Data Source=desafio_estoque.db");

                using var context = new AppDbContext(optionsBuilder.Options);

                var produtos = await context.Set<estoque>()
                                           .AsNoTracking()
                                           .OrderBy(p => p.NomeProduto)
                                           .ToListAsync();

                if (produtos == null || produtos.Count == 0)
                {
                    MessageBox.Show("Nenhum produto encontrado no banco de dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbProduto.DataSource = null;
                    return;
                }

                cbProduto.DisplayMember = nameof(estoque.NomeProduto);
                cbProduto.ValueMember = nameof(estoque.ID);
                cbProduto.DataSource = produtos;

                if (cbProduto.Items.Count > 0)
                    cbProduto_SelectedIndexChanged(cbProduto, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalcularComissao_Click(object sender, EventArgs e)
        {
            try
            {
                var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json", "comissao.json");
                if (!File.Exists(jsonPath))
                {
                    MessageBox.Show($"Arquivo não encontrado: {jsonPath}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var json = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var root = JsonSerializer.Deserialize<ComissaoRoot>(json, options);

                if (root?.vendas == null || root.vendas.Count == 0)
                {
                    MessageBox.Show("Nenhuma venda encontrada no arquivo JSON.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var ci = CultureInfo.GetCultureInfo("pt-BR");
                var sb = new StringBuilder();
                sb.AppendLine("Comissões por vendedor:");
                sb.AppendLine(new string('-', 50));

                decimal totalVendasGeral = 0m;
                decimal totalComissaoGeral = 0m;

                var grupos = root.vendas.GroupBy(v => v.vendedor);
                foreach (var grupo in grupos.OrderBy(g => g.Key))
                {
                    var vendas = grupo.ToList();
                    var totalVendas = vendas.Sum(v => v.valor);
                    var totalComissao = vendas.Sum(v => CalcularComissao(v.valor));

                    totalVendasGeral += totalVendas;
                    totalComissaoGeral += totalComissao;

                    sb.AppendLine($"{grupo.Key}:");
                    sb.AppendLine($"  Quantidade de vendas: {vendas.Count}");
                    sb.AppendLine($"  Total de vendas: {totalVendas.ToString("C", ci)}");
                    sb.AppendLine($"  Comissão total: {totalComissao.ToString("C", ci)}");
                    sb.AppendLine();
                }

                sb.AppendLine(new string('-', 50));
                sb.AppendLine("Totais gerais:");
                sb.AppendLine($"  Total de vendas: {totalVendasGeral.ToString("C", ci)}");
                sb.AppendLine($"  Comissão total: {totalComissaoGeral.ToString("C", ci)}");

                MessageBox.Show(sb.ToString(), "Resultado das Comissões", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao calcular comissões: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static decimal CalcularComissao(decimal valor)
        {
            if (valor < 100m) return 0m;
            if (valor < 500m) return Math.Round(valor * 0.01m, 2);
            return Math.Round(valor * 0.05m, 2);
        }

        private class ComissaoRoot
        {
            public List<Venda> vendas { get; set; } = new();
        }

        private class Venda
        {
            public string vendedor { get; set; } = string.Empty;
            public decimal valor { get; set; }
        }

        private async void btnInserir_Click(object sender, EventArgs e)
        {
            if (cbProduto.SelectedItem is not estoque produtoSelecionado)
            {
                MessageBox.Show("Selecione um produto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var culture = CultureInfo.GetCultureInfo("pt-BR");
            if (!decimal.TryParse(txbQtd.Text.Trim(), NumberStyles.Number, culture, out var qtd) || qtd <= 0m)
            {
                MessageBox.Show("Informe uma quantidade válida maior que zero.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cblTipoMovimento.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione o tipo de movimento (Entrada ou Saida).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tipo = cblTipoMovimento.CheckedItems[0].ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(tipo))
            {
                MessageBox.Show("Tipo de movimento inválido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlite("Data Source=desafio_estoque.db");

                using var context = new AppDbContext(optionsBuilder.Options);

                var produto = await context.Set<estoque>().FirstOrDefaultAsync(p => p.ID == produtoSelecionado.ID);
                if (produto == null)
                {
                    MessageBox.Show("Produto não encontrado no banco.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal novoEstoque = produto.quantidade;
                if (tipo.Equals("Entrada", StringComparison.OrdinalIgnoreCase))
                {
                    novoEstoque = produto.quantidade + qtd;
                }
                else if (tipo.Equals("Saida", StringComparison.OrdinalIgnoreCase) || tipo.Equals("Saída", StringComparison.OrdinalIgnoreCase))
                {
                    if (produto.quantidade < qtd)
                    {
                        MessageBox.Show("Estoque insuficiente para realizar a saída.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    novoEstoque = produto.quantidade - qtd;
                }
                else
                {
                    MessageBox.Show("Tipo de movimento desconhecido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                produto.quantidade = novoEstoque;
                context.Set<MovimentoEstoque>().Add(new MovimentoEstoque
                {
                    tipoMovimento = tipo,
                    quantidade = qtd,
                    CodItem = produto.ID
                });

                await context.SaveChangesAsync();

                txtEstoque.Text = novoEstoque.ToString("N2", culture);

                var linha = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {produto.NomeProduto} (Cod:{produto.ID}) - {tipo} - Qtd: {qtd.ToString("N2", culture)} - Estoque: {novoEstoque.ToString("N2", culture)}";
                if (!string.IsNullOrEmpty(txtMemoriaRegistro.Text))
                    txtMemoriaRegistro.AppendText(Environment.NewLine);
                txtMemoriaRegistro.AppendText(linha);

                txbQtd.Clear();
                for (int i = 0; i < cblTipoMovimento.Items.Count; i++)
                    cblTipoMovimento.SetItemChecked(i, false);

                if (cbProduto.DataSource is List<estoque> lista)
                {
                    var item = lista.FirstOrDefault(x => x.ID == produto.ID);
                    if (item != null) item.quantidade = produto.quantidade;
                    var idx = cbProduto.SelectedIndex;
                    cbProduto.DisplayMember = "";
                    cbProduto.DisplayMember = nameof(estoque.NomeProduto);
                    cbProduto.SelectedIndex = idx;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar movimento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cbProduto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                if (cbProduto.SelectedItem is not estoque selecionado)
                {
                    txtEstoque.Text = string.Empty;
                    return;
                }

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlite("Data Source=desafio_estoque.db");

                using var context = new AppDbContext(optionsBuilder.Options);

                var produto = await context.Set<estoque>().AsNoTracking().FirstOrDefaultAsync(p => p.ID == selecionado.ID);
                if (produto != null)
                {
                    txtEstoque.Text = produto.quantidade.ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
                }
                else
                {
                    txtEstoque.Text = string.Empty;
                }
            }
            catch
            {
                txtEstoque.Text = string.Empty;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                var culture = CultureInfo.GetCultureInfo("pt-BR");

                // Ler valor base
                if (string.IsNullOrWhiteSpace(txtValorTomado.Text) || !decimal.TryParse(txtValorTomado.Text.Trim(), NumberStyles.Number, culture, out var valor) || valor <= 0m)
                {
                    MessageBox.Show("Informe um valor válido (use vírgula).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJuros.Text = string.Empty;
                    return;
                }

                var vencimento = dpVencimento.Value.Date;
                var hoje = DateTime.Today;
                var dias = (hoje - vencimento).Days;

                if (dias <= 0)
                {
                    txtJuros.Text = 0m.ToString("C", culture);
                    MessageBox.Show("Sem juros: data de vencimento maior que a data atual.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                const decimal taxaDiaria = 0.025m; // 2,5% ao dia

                var juros = Math.Round(valor * taxaDiaria * dias, 2, MidpointRounding.AwayFromZero);

                txtJuros.Text = juros.ToString("C", culture);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao calcular juros: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
