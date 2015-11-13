namespace Take.Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMerits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerUserId = c.Int(),
                        TargetUserId = c.Int(nullable: false),
                        MeritId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merits", t => t.MeritId, cascadeDelete: true)
                .ForeignKey("dbo.UserAccounts", t => t.OwnerUserId)
                .ForeignKey("dbo.UserAccounts", t => t.TargetUserId, cascadeDelete: true)
                .Index(t => t.OwnerUserId)
                .Index(t => t.TargetUserId)
                .Index(t => t.MeritId);
            
            CreateTable(
                "dbo.Merits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMerits", "TargetUserId", "dbo.UserAccounts");
            DropForeignKey("dbo.UserMerits", "OwnerUserId", "dbo.UserAccounts");
            DropForeignKey("dbo.UserMerits", "MeritId", "dbo.Merits");
            DropIndex("dbo.UserMerits", new[] { "MeritId" });
            DropIndex("dbo.UserMerits", new[] { "TargetUserId" });
            DropIndex("dbo.UserMerits", new[] { "OwnerUserId" });
            DropTable("dbo.Merits");
            DropTable("dbo.UserMerits");
        }
    }
}
