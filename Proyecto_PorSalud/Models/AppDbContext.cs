using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_PorSalud.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDb") { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Documento> Documentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Documentos)
                .WithRequired(d => d.Cliente)
                .HasForeignKey(d => d.ClienteId);

            base.OnModelCreating(modelBuilder);
        }
    }
}