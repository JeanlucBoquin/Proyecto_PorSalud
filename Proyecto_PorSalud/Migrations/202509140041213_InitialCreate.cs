namespace Proyecto_PorSalud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identificacion = c.String(nullable: false, maxLength: 20),
                        NombreCompleto = c.String(nullable: false, maxLength: 150),
                        Direccion = c.String(maxLength: 250),
                        Telefono = c.String(maxLength: 20),
                        Celular = c.String(maxLength: 20),
                        Correo = c.String(maxLength: 120),
                        Sexo = c.String(nullable: false, maxLength: 1),
                        Activo = c.Boolean(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 200),
                        RutaRelativa = c.String(maxLength: 260),
                        ContentType = c.String(maxLength: 128),
                        Length = c.Long(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documentoes", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Documentoes", new[] { "ClienteId" });
            DropTable("dbo.Documentoes");
            DropTable("dbo.Clientes");
        }
    }
}
