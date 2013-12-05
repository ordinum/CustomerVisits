namespace CVisits.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregaAccionaVisita : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visita", "AccionID", c => c.Int());
            AddForeignKey("dbo.Visita", "AccionID", "dbo.Accion", "AccionID");
            CreateIndex("dbo.Visita", "AccionID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Visita", new[] { "AccionID" });
            DropForeignKey("dbo.Visita", "AccionID", "dbo.Accion");
            DropColumn("dbo.Visita", "AccionID");
        }
    }
}
