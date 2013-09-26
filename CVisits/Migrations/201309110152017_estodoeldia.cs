namespace CVisits.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estodoeldia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visita", "EsTodoElDia", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visita", "EsTodoElDia");
        }
    }
}
