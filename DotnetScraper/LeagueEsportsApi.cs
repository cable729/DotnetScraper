using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace DotnetScraper
{
    public class ScheduleItem
    {
        public string Match { get; set; }
        public string Tournament { get; set; }
    }

    public class ScheduleItemsResponse
    {
        public List<ScheduleItem> ScheduleItems { get; set; }
    }

    public class GameIdMapping
    {
        public string Id { get; set; }
        public string GameHash { get; set; }
    }

    public class MatchDetailsResponse
    {
        public List<GameIdMapping> GameIdMappings { get; set; }
    }

    public class LeagueEsportsApi
    {
        private static async Task<T> Execute<T>(string baseUrl, IRestRequest request)
        {
            var client = new RestClient(baseUrl);

            var result = await client.ExecuteTaskAsync<T>(request);
            return result.Data;
        }

        public async Task<ScheduleItemsResponse> GetScheduleItems(int leagueId)
        {
            var request = new RestRequest("scheduleItems", Method.GET);
            request.AddQueryParameter("leagueId", leagueId.ToString());

            var result = await Execute<ScheduleItemsResponse>("http://api.lolesports.com/api/v1/", request);
            if (result?.ScheduleItems == null)
            {
                
            }
            if (result.ScheduleItems.Any(s => s.Match == null || s.Tournament == null))
            {
                var nulls = result.ScheduleItems.Where(s => s.Match == null || s.Tournament == null);
            }
            return result;
        }

        public async Task<MatchDetailsResponse> GetMatches(string tournamentId, string matchId)
        {
            var request = new RestRequest("highlanderMatchDetails");
            request.AddQueryParameter("tournamentId", tournamentId);
            request.AddQueryParameter("matchId", matchId);
            return await Execute<MatchDetailsResponse>("http://api.lolesports.com/api/v2/", request);
        }
    }
}