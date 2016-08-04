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

            using (var context = new LolEsportsContext())
            {
                foreach (var item in result.ScheduleItems)
                {
                    if (!context.ScheduleItems.Any(i => i.RiotGivenId == item.Id))
                        context.ScheduleItems.Add(ScheduleItem.FromResponse(item));
                }
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task FetchMatches()
        {
            var api = new LeagueEsportsApi();

            using (var context = new LolEsportsContext())
            {
                foreach (var item in context.ScheduleItems.Where(s => s.Match != null && s.Tournament != null))
                {
                    var matches = await api.GetMatches(item.Tournament, item.Match);
                    if (matches?.GameIdMappings == null) continue;
                    foreach (var gameIdMapping in matches.GameIdMappings)
                    {
                        if (!context.GameIdMappings.Any(
                            g => g.GameId == gameIdMapping.Id && g.GameHash == gameIdMapping.GameHash))
                            context.GameIdMappings.Add(GameIdMapping.FromResponse(gameIdMapping));
                    }
                }
                context.SaveChanges();
            }
        }

        private async Task<IList<GameIdMappingResponse>> GetGameIdMappings()
        {
            var api = new LeagueEsportsApi();

            var scheduleItems = await api.GetScheduleItems(2);
            var scheduleItemsWithGames = scheduleItems.ScheduleItems
                .Where(s => s.Match != null && s.Tournament != null);

            var gameMappings = new List<GameIdMappingResponse>();
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