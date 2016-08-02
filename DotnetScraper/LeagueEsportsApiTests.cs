using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DotnetScraper
{
    public class LeagueEsportsApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public LeagueEsportsApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task FetchScheduleItems()
        {
            var api = new LeagueEsportsApi();

            var result = await api.GetScheduleItems(2);

            result.ScheduleItems
                .Select(s => $"{s.Match}, {s.Tournament}")
                .ToList()
                .ForEach(_testOutputHelper.WriteLine);
        }

        [Fact]
        public async Task FetchMatches()
        {
            var api = new LeagueEsportsApi();

            var scheduleItems = await api.GetScheduleItems(2);
            foreach (var item in scheduleItems.ScheduleItems.Where(s => s.Match != null && s.Tournament != null))
            {
                var matches = await api.GetMatches(item.Tournament, item.Match);
                if (matches?.GameIdMappings == null)
                {
                    _testOutputHelper.WriteLine($"No matches found for match {item.Match} and tournament {item.Tournament}");
                    continue;
                } 
                matches.GameIdMappings
                    .Select(g => $"{g?.Id}, {g?.GameHash}")
                    .ToList()
                    .ForEach(_testOutputHelper.WriteLine);
            }
        }


    }
}