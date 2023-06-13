namespace Csharp_Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamplayer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        JoinedDate = c.DateTime(nullable: false),
                        ReleaseDate = c.DateTime(),
                        JoinedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players");
            DropIndex("dbo.TeamPlayers", new[] { "TeamId" });
            DropIndex("dbo.TeamPlayers", new[] { "PlayerId" });
            DropTable("dbo.TeamPlayers");
        }
    }
}
