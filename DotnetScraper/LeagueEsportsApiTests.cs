using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var sb = new StringBuilder();
            foreach (var item in scheduleItems.ScheduleItems.Where(s => s.Match != null && s.Tournament != null))
            {
                var matches = await api.GetMatches(item.Tournament, item.Match);
                if (matches?.GameIdMappings == null)
                {
                    //_testOutputHelper.WriteLine($"No matches found for match {item.Match} and tournament {item.Tournament}");
                    continue;
                } 
                matches.GameIdMappings
                    .Select(g => $"{g?.Id}, {g?.GameHash}")
                    .ToList()
                    .ForEach(_testOutputHelper.WriteLine);
            }
        }

        private async Task<IList<GameIdMapping>> GetGameIdMappings()
        {
            var api = new LeagueEsportsApi();

            var scheduleItems = await api.GetScheduleItems(2);
            var scheduleItemsWithGames = scheduleItems.ScheduleItems
                .Where(s => s.Match != null && s.Tournament != null);

            var gameMappings = new List<GameIdMapping>();
            foreach (var item in scheduleItemsWithGames)
            {
                var matches = await api.GetMatches(item.Tournament, item.Match);
                if (matches?.GameIdMappings == null)
                {
                    _testOutputHelper.WriteLine($"No matches found for match {item.Match} and tournament {item.Tournament}");
                    continue;
                }
                gameMappings.AddRange(matches.GameIdMappings);
            }
            return gameMappings;
        }

        [Fact]
        public async Task FetchGames()
        {
            var mappings = await GetGameIdMappings();

            //var api = new LeagueEsportsApi();
            var sb = new StringBuilder();
            foreach (var mapping in mappings)
            {
                sb.AppendLine($"{mapping.Id}, {mapping.GameHash}");
                //var game = await api.GetGame(mapping.Id, mapping.GameHash);
                //Console.WriteLine(game.GameId);
            }
            var str = sb.ToString();
            Console.Write(str);
            Console.WriteLine("done");
        }

        [Fact]
        public void TestSelectFromDatabase()
        {
            using (var context = new LolEsportsContext())
            {
                var scheduleItems = context.ScheduleItems.ToList();
            }
        }
    }
}