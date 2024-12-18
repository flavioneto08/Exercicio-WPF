using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Exercicio_WPF.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<Produto_GrupoModel> Produto_Grupo { get; set; }
        public DbSet<VendaModel> Venda { get; set; }
        public DbSet<VendaProdutoModel> Venda_Produto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeia ProdutoModel para a tabela "produto"
            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("produto"); // Define o nome da tabela no banco de dados

                entity.Property(p => p.CodBarra)
                      .IsRequired(false); // Permite que CodBarra seja nullable
            });

            // Mapeia ProdutoGrupoModel para a tabela "produto_grupo"
            modelBuilder.Entity<Produto_GrupoModel>()
                .ToTable("produto_grupo");

            // Configuração do relacionamento entre Produto e ProdutoGrupo
            modelBuilder.Entity<ProdutoModel>()
                .HasOne(p => p.Produto_Grupo)          // Produto tem um Produto_Grupo
                .WithMany(g => g.Produto)            // Produto_Grupo tem muitos Produtos
                .HasForeignKey(p => p.CodGrupo)       // Define a Foreign Key como CodGrupo
                .HasConstraintName("fk_produtoCodGrupo"); // Nome opcional da constraint

            base.OnModelCreating(modelBuilder);
        }
    }
}
