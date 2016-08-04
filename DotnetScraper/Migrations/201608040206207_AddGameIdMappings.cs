namespace DotnetScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGameIdMappings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameIdMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.String(),
                        GameHash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameIdMappings");
        }
    }
}
