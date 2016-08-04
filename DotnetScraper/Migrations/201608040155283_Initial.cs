namespace DotnetScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RiotGivenId = c.String(),
                        Match = c.String(),
                        Tournament = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScheduleItems");
        }
    }
}
