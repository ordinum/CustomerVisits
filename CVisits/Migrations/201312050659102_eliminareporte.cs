namespace CVisits.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eliminareporte : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accion", "ReporteID", "dbo.Reporte");
            DropForeignKey("dbo.Reporte", "TipoVisitaID", "dbo.TipoVisita");
            DropForeignKey("dbo.Reporte", "Cliente_ClienteID", "dbo.Cliente");
            DropForeignKey("dbo.Reporte", "VisitaID", "dbo.Visita");
            DropForeignKey("dbo.Reporte", "MaquinaEquipoID", "dbo.MaquinaEquipo");
            DropForeignKey("dbo.Reporte", "PaisID", "dbo.Pais");
            DropForeignKey("dbo.Reporte", "UserId", "dbo.UserProfile");
            DropIndex("dbo.Accion", new[] { "ReporteID" });
            DropIndex("dbo.Reporte", new[] { "TipoVisitaID" });
            DropIndex("dbo.Reporte", new[] { "Cliente_ClienteID" });
            DropIndex("dbo.Reporte", new[] { "VisitaID" });
            DropIndex("dbo.Reporte", new[] { "MaquinaEquipoID" });
            DropIndex("dbo.Reporte", new[] { "PaisID" });
            DropIndex("dbo.Reporte", new[] { "UserId" });
            DropColumn("dbo.Accion", "ReporteID");
            DropTable("dbo.Reporte");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ReporteID);
            
            AddColumn("dbo.Accion", "ReporteID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reporte", "UserId");
            CreateIndex("dbo.Reporte", "PaisID");
            CreateIndex("dbo.Reporte", "MaquinaEquipoID");
            CreateIndex("dbo.Reporte", "VisitaID");
            CreateIndex("dbo.Reporte", "Cliente_ClienteID");
            CreateIndex("dbo.Reporte", "TipoVisitaID");
            CreateIndex("dbo.Accion", "ReporteID");
            AddForeignKey("dbo.Reporte", "UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Reporte", "PaisID", "dbo.Pais", "PaisID");
            AddForeignKey("dbo.Reporte", "MaquinaEquipoID", "dbo.MaquinaEquipo", "MaquinaEquipoID");
            AddForeignKey("dbo.Reporte", "VisitaID", "dbo.Visita", "VisitaID");
            AddForeignKey("dbo.Reporte", "Cliente_ClienteID", "dbo.Cliente", "ClienteID");
            AddForeignKey("dbo.Reporte", "TipoVisitaID", "dbo.TipoVisita", "TipoVisitaID");
            AddForeignKey("dbo.Accion", "ReporteID", "dbo.Reporte", "ReporteID");
        }
    }
}
