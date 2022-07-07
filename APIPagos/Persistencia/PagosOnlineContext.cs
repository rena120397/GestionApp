using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistencia
{
    public class PagosOnlineContext : IdentityDbContext<Usuario>
    {
        public PagosOnlineContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>()
            .HasOne(a => a.pago)
            .WithOne(a => a.Categoria)
            .HasForeignKey<Pago>(c => c.id_categoria);
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Documento> Documento { get; set; }  
        public DbSet<Pago> Pago { get; set; }


    }
}
