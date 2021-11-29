using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UStart.Domain.Entities;
using UStart.Infrastructure.Helpers;

namespace UStart.Infrastructure.Context
{

    public class UStartContext : DbContext
    {

        public UStartContext(DbContextOptions<UStartContext> options)
         : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<FormaPagamento> FormasPagamentos { get; set; } 
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<OrcamentoItem> OrcamentosItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidosItens { get; set; }
        public DbSet<GrupoProduto> GrupoProdutos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        // exemplo para EF
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<BuyerOrder>()
        //         .ToTable("buyerorders")
        //         .HasKey(x => new { x.OrganizationId, x.DistributionChannelId, x.Identity, x.OrderItemId });


        //     EntityFrameworkPostgresHelper.MapPropertiesLowerCase<BuyerOrder>(modelBuilder);


        //     base.OnModelCreating(modelBuilder);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormaPagamento>(entity =>
            {
                entity.Property(forma => forma.Dias)
                .HasColumnType("jsonb");                
            });

            modelBuilder.Entity<Orcamento>(entity =>
            {
                entity
                    .HasMany(or => or.Itens)
                    .WithOne(item => item.Orcamento)
                    .HasForeignKey(item => item.OrcamentoId);
                                        
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity
                    .HasMany(ped => ped.Itens)
                    .WithOne(item => item.Pedido)
                    .HasForeignKey(item => item.PedidoId);                                        
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}

