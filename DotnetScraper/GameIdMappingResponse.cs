namespace DotnetScraper
{
    public class GameIdMappingResponse
    {
        public string Id { get; set; }
        public string GameHash { get; set; }
    }

    public class GameIdMapping : PersistentObject
    {
        public string GameId { get; set; }
        public string GameHash { get; set; }

        public static GameIdMapping FromResponse(GameIdMappingResponse response)
        {
            return new GameIdMapping
            {
                GameId = response.Id,
                GameHash = response.GameHash
            };
        }
    }
}