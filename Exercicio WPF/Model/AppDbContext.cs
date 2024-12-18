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
            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("produto");

                entity.Property(p => p.CodBarra)
                      .IsRequired(false);
            });

            modelBuilder.Entity<Produto_GrupoModel>()
                .ToTable("produto_grupo");

            modelBuilder.Entity<ProdutoModel>()
                .HasOne(p => p.Produto_Grupo)        
                .WithMany(g => g.Produto)            
                .HasForeignKey(p => p.CodGrupo)      
                .HasConstraintName("fk_produtoCodGrupo"); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
