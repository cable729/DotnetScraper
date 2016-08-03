using System;
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

    public class Game
    {
        public int GameId { get; set; }
        public string PlatformId { get; set; }
        public DateTime GameCreation { get; set; }
        public TimeSpan GameDuration { get; set; }
        public int QueueId { get; set; }
        public int MapId { get; set; }
        public int SeasonId { get; set; }
        public string GameVersion { get; set; }
        public string GameMode { get; set; }
        public string GameType { get; set; }
        // Teams
        // Participants
        // ParticipantIdentities
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

        public async Task<Game> GetGame(string matchId, string matchHash)
        {
            //https://acs.leagueoflegends.com/v1/stats/game/TRLH1/{GAME_ID}?gameHash=GAME_HASH}
            var request = new RestRequest($"stats/game/TRLH1/{matchId}");
            request.AddQueryParameter("gameHash", matchHash);
            return await Execute<Game>("https://acs.leagueoflegends.com/v1/", request);
        }
    }
}