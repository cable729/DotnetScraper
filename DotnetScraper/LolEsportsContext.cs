using System.Data.Entity;

namespace DotnetScraper
{
    public class LolEsportsContext : DbContext
    {
        public DbSet<ScheduleItem> ScheduleItems { get; set; }
    }
}