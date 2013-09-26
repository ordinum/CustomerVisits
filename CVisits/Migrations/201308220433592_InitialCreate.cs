namespace CVisits.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accion",
                c => new
                    {
                        AccionID = c.Int(nullable: false, identity: true),
                        FechaAccion = c.DateTime(nullable: false),
                        Descripcion = c.String(nullable: false),
                        ReporteID = c.Int(nullable: false),
                        AccionResponsableID = c.Int(nullable: false),
                        AccionEstadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccionID)
                .ForeignKey("dbo.Reporte", t => t.ReporteID)
                .ForeignKey("dbo.AccionResponsable", t => t.AccionResponsableID)
                .ForeignKey("dbo.AccionEstado", t => t.AccionEstadoID)
                .Index(t => t.ReporteID)
                .Index(t => t.AccionResponsableID)
                .Index(t => t.AccionEstadoID);
            
            CreateTable(
                "dbo.Reporte",
                c => new
                    {
                        ReporteID = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaTermino = c.DateTime(nullable: false),
                        BussinesPotential = c.Int(nullable: false),
                        CFC = c.String(),
                        Descripcion = c.String(nullable: false),
                        VisitaID = c.Int(nullable: false),
                        TipoVisitaID = c.Int(nullable: false),
                        MaquinaEquipoID = c.Int(nullable: false),
                        PaisID = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Cliente_ClienteID = c.Int(),
                    })
                .PrimaryKey(t => t.ReporteID)
                .ForeignKey("dbo.TipoVisita", t => t.TipoVisitaID)
                .ForeignKey("dbo.Cliente", t => t.Cliente_ClienteID)
                .ForeignKey("dbo.Visita", t => t.VisitaID)
                .ForeignKey("dbo.MaquinaEquipo", t => t.MaquinaEquipoID)
                .ForeignKey("dbo.Pais", t => t.PaisID)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.TipoVisitaID)
                .Index(t => t.Cliente_ClienteID)
                .Index(t => t.VisitaID)
                .Index(t => t.MaquinaEquipoID)
                .Index(t => t.PaisID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Visita",
                c => new
                    {
                        VisitaID = c.Int(nullable: false, identity: true),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaPlanificada = c.DateTime(nullable: false),
                        Descripcion = c.String(nullable: false),
                        EstadoVisitaID = c.Int(nullable: false),
                        LineaProductoID = c.Int(nullable: false),
                        TipoVisitaID = c.Int(nullable: false),
                        ClienteID = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisitaID)
                .ForeignKey("dbo.EstadoVisita", t => t.EstadoVisitaID)
                .ForeignKey("dbo.LineaProducto", t => t.LineaProductoID)
                .ForeignKey("dbo.TipoVisita", t => t.TipoVisitaID)
                .ForeignKey("dbo.Cliente", t => t.ClienteID)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.EstadoVisitaID)
                .Index(t => t.LineaProductoID)
                .Index(t => t.TipoVisitaID)
                .Index(t => t.ClienteID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EstadoVisita",
                c => new
                    {
                        EstadoVisitaID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EstadoVisitaID);
            
            CreateTable(
                "dbo.LineaProducto",
                c => new
                    {
                        LineaProductoID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LineaProductoID);
            
            CreateTable(
                "dbo.TipoVisita",
                c => new
                    {
                        TipoVisitaID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TipoVisitaID);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.ClienteContacto",
                c => new
                    {
                        ClienteContactoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefono = c.String(),
                        ClienteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteContactoID)
                .ForeignKey("dbo.Cliente", t => t.ClienteID)
                .Index(t => t.ClienteID);
            
            CreateTable(
                "dbo.ClienteFaena",
                c => new
                    {
                        ClienteFaenaID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        ClienteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteFaenaID)
                .ForeignKey("dbo.Cliente", t => t.ClienteID)
                .Index(t => t.ClienteID);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.MaquinaEquipo",
                c => new
                    {
                        MaquinaEquipoID = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaquinaEquipoID);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        PaisID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PaisID);
            
            CreateTable(
                "dbo.AccionResponsable",
                c => new
                    {
                        AccionResponsableID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.AccionResponsableID);
            
            CreateTable(
                "dbo.AccionEstado",
                c => new
                    {
                        AccionEstadoID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccionEstadoID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ClienteFaena", new[] { "ClienteID" });
            DropIndex("dbo.ClienteContacto", new[] { "ClienteID" });
            DropIndex("dbo.Visita", new[] { "UserId" });
            DropIndex("dbo.Visita", new[] { "ClienteID" });
            DropIndex("dbo.Visita", new[] { "TipoVisitaID" });
            DropIndex("dbo.Visita", new[] { "LineaProductoID" });
            DropIndex("dbo.Visita", new[] { "EstadoVisitaID" });
            DropIndex("dbo.Reporte", new[] { "UserId" });
            DropIndex("dbo.Reporte", new[] { "PaisID" });
            DropIndex("dbo.Reporte", new[] { "MaquinaEquipoID" });
            DropIndex("dbo.Reporte", new[] { "VisitaID" });
            DropIndex("dbo.Reporte", new[] { "Cliente_ClienteID" });
            DropIndex("dbo.Reporte", new[] { "TipoVisitaID" });
            DropIndex("dbo.Accion", new[] { "AccionEstadoID" });
            DropIndex("dbo.Accion", new[] { "AccionResponsableID" });
            DropIndex("dbo.Accion", new[] { "ReporteID" });
            DropForeignKey("dbo.ClienteFaena", "ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.ClienteContacto", "ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.Visita", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Visita", "ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.Visita", "TipoVisitaID", "dbo.TipoVisita");
            DropForeignKey("dbo.Visita", "LineaProductoID", "dbo.LineaProducto");
            DropForeignKey("dbo.Visita", "EstadoVisitaID", "dbo.EstadoVisita");
            DropForeignKey("dbo.Reporte", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Reporte", "PaisID", "dbo.Pais");
            DropForeignKey("dbo.Reporte", "MaquinaEquipoID", "dbo.MaquinaEquipo");
            DropForeignKey("dbo.Reporte", "VisitaID", "dbo.Visita");
            DropForeignKey("dbo.Reporte", "Cliente_ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.Reporte", "TipoVisitaID", "dbo.TipoVisita");
            DropForeignKey("dbo.Accion", "AccionEstadoID", "dbo.AccionEstado");
            DropForeignKey("dbo.Accion", "AccionResponsableID", "dbo.AccionResponsable");
            DropForeignKey("dbo.Accion", "ReporteID", "dbo.Reporte");
            DropTable("dbo.AccionEstado");
            DropTable("dbo.AccionResponsable");
            DropTable("dbo.Pais");
            DropTable("dbo.MaquinaEquipo");
            DropTable("dbo.UserProfile");
            DropTable("dbo.ClienteFaena");
            DropTable("dbo.ClienteContacto");
            DropTable("dbo.Cliente");
            DropTable("dbo.TipoVisita");
            DropTable("dbo.LineaProducto");
            DropTable("dbo.EstadoVisita");
            DropTable("dbo.Visita");
            DropTable("dbo.Reporte");
            DropTable("dbo.Accion");
        }
    }
}
