namespace PokédexLib.DTO
{
    public class StatsDto
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
        public StatInfo stat { get; set; }
        public class StatInfo
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }



}
