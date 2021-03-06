using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Produtos;
using DevIO.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DevIO.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar")
                .HasMaxLength(100));     /* dessa forma eu não precisaria me preocupar com o tamanho no mapeamento das entidades */

            modelBuilder.Configurations.Add(new FornecedorConfig());
            modelBuilder.Configurations.Add(new EnderecoConfig());
            modelBuilder.Configurations.Add(new ProdutoConfig());
        }
    }
}
