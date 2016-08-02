using System.Collections.Generic;
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
            return await Execute<ScheduleItemsResponse>("http://api.lolesports.com/api/v1/", request);
        }
    }
}