namespace CVisits.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class corrigecardinalidadAccionVisita : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visita", "AccionID", "dbo.Accion");
            DropIndex("dbo.Visita", new[] { "AccionID" });
            RenameColumn(table: "dbo.Accion", name: "AccionID", newName: "VisitaID");
            DropPrimaryKey("dbo.Accion", new[] { "AccionID" });            
            AddForeignKey("dbo.Accion", "VisitaID", "dbo.Visita", "VisitaID");
            CreateIndex("dbo.Accion", "VisitaID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Accion", new[] { "VisitaID" });
            DropForeignKey("dbo.Accion", "VisitaID", "dbo.Visita");
            DropPrimaryKey("dbo.Accion", new[] { "AccionID" });
            AddPrimaryKey("dbo.Accion", "AccionID");
            RenameColumn(table: "dbo.Accion", name: "VisitaID", newName: "AccionID");
            CreateIndex("dbo.Visita", "AccionID");
            AddForeignKey("dbo.Visita", "AccionID", "dbo.Accion", "AccionID");
        }
    }
}
