using System;

namespace DotnetScraper
{
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
}