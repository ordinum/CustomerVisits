namespace CVisits.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregaboolnotificacliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visita", "NotificaCliente", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visita", "NotificaCliente");
        }
    }
}
