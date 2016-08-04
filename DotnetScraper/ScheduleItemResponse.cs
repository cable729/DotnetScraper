using RestSharp.Deserializers;

namespace DotnetScraper
{
    public class ScheduleItemResponse
    {
        public string Id { get; set; }
        public string Match { get; set; }
        public string Tournament { get; set; }
    }
}