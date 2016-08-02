using System;
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
    }
}