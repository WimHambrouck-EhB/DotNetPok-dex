namespace PokédexLib.DTO
{
    public class AbilityDTO
    {
        public AbilityInfo ability { get; set; }
        public bool is_hidden { get; set; }
        public int slot { get; set; }

        public class AbilityInfo
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }

}
