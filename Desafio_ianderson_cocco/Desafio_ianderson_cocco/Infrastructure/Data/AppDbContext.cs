using Desafio_ianderson_cocco.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_ianderson_cocco.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
            public DbSet<estoque> estoque { get; set; } = null!;
        public DbSet<MovimentoEstoque> MovimentoEstoque { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        
            modelBuilder.Entity<estoque>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.NomeProduto).IsRequired().HasMaxLength(150);
                entity.Property(e => e.quantidade).HasColumnType("TEXT");
            });

        
            modelBuilder.Entity<MovimentoEstoque>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.tipoMovimento).IsRequired().HasMaxLength(50);
                entity.Property(e => e.quantidade).HasColumnType("TEXT");

        
                entity.Property(e => e.CodItem)
                      .HasColumnName("CodItem")
                      .HasColumnType("INTEGER")
                      .IsRequired(true);
            });
        }
    }
}
