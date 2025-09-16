namespace Proyecto_PorSalud.Migrations
{
    using Proyecto_PorSalud.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Proyecto_PorSalud.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Proyecto_PorSalud.Models.AppDbContext ctx)
        {
            if (!ctx.Clientes.Any())
            {
                var rnd = new Random();
                var sexos = new[] { "M", "F" };

                for (int i = 1; i <= 2200; i++)
                {
                    ctx.Clientes.Add(new Cliente
                    {
                        Identificacion = $"ID{i:000000}",
                        NombreCompleto = $"Cliente {i}",
                        Direccion = $"Calle {i} # {rnd.Next(1, 200)}",
                        Telefono = $"22{rnd.Next(100000, 999999)}",
                        Celular = $"99{rnd.Next(100000, 999999)}",
                        Correo = $"cliente{i}@mail.com",
                        Sexo = sexos[rnd.Next(0, 2)],
                        Activo = rnd.Next(0, 2) == 1
                    });
                }
                ctx.SaveChanges();
            }
        }
    }
}
