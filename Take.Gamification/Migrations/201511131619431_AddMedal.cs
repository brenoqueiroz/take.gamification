namespace Take.Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMedal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMedals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MedalId = c.Int(nullable: false),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medals", t => t.MedalId, cascadeDelete: true)
                .ForeignKey("dbo.UserAccounts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MedalId);
            
            CreateTable(
                "dbo.Medals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMedals", "UserId", "dbo.UserAccounts");
            DropForeignKey("dbo.UserMedals", "MedalId", "dbo.Medals");
            DropIndex("dbo.UserMedals", new[] { "MedalId" });
            DropIndex("dbo.UserMedals", new[] { "UserId" });
            DropTable("dbo.Medals");
            DropTable("dbo.UserMedals");
        }
    }
}
