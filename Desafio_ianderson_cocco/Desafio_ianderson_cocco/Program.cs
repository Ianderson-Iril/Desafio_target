using Desafio_ianderson_cocco.Infrastructure.Data;
using Desafio_ianderson_cocco.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace Desafio_ianderson_cocco
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=desafio_estoque.db");

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                DbInitializer.InicializarBancoAsync(context).GetAwaiter().GetResult();

                string caminhoProdutos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json", "produtos.json");
                if (File.Exists(caminhoProdutos))
                {
                    try
                    {
                        string jsonString = File.ReadAllText(caminhoProdutos);
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var produtosDoArquivo = JsonSerializer.Deserialize<List<estoque>>(jsonString, options);

                        if (produtosDoArquivo != null && produtosDoArquivo.Count > 0)
                        {
                            foreach (var produto in produtosDoArquivo)
                            {
                                var nome = (produto.NomeProduto ?? string.Empty).Trim();
                                if (string.IsNullOrEmpty(nome))
                                    continue;

                                bool existe = context.estoque.Any(e => e.NomeProduto.ToLower() == nome.ToLower());

                                if (!existe)
                                {
                                    var novo = new estoque
                                    {
                                        NomeProduto = nome,
                                        quantidade = produto.quantidade
                                    };
                                    context.estoque.Add(novo);
                                }
                            }

                            if (context.ChangeTracker.HasChanges())
                            {
                                context.SaveChanges();
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }

            Application.Run(new frm_Principal());
        }
    }
}