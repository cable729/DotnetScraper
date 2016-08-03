using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace DotnetScraper
{
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

        public async Task<Game> GetGame(string matchId, string matchHash)
        {
            //https://acs.leagueoflegends.com/v1/stats/game/TRLH1/{GAME_ID}?gameHash=GAME_HASH}
            var request = new RestRequest($"stats/game/TRLH1/{matchId}");
            request.AddQueryParameter("gameHash", matchHash);
            return await Execute<Game>("https://acs.leagueoflegends.com/v1/", request);
        }
    }
}