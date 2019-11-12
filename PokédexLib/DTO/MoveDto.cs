namespace PokédexLib.DTO
{
    public class MoveDto
    {
        public MoveInfo move { get; set; }
        public VersionGroupDetailsDto[] version_group_details { get; set; }

        public class MoveInfo
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }

}
