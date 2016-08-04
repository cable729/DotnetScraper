namespace DotnetScraper
{
    public class ScheduleItem : PersistentObject
    {
        public string RiotGivenId { get; set; }
        public string Match { get; set; }
        public string Tournament { get; set; }

        public static ScheduleItem FromResponse(ScheduleItemResponse response)
        {
            return new ScheduleItem
            {
                RiotGivenId = response.Id,
                Match = response.Match,
                Tournament = response.Tournament
            };
        }
    }
}