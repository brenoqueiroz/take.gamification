namespace Take.Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mdedalssadsd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medals", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medals", "Type");
        }
    }
}
