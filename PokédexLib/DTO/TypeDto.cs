namespace PokédexLib.DTO
{
    public class TypeDto
    {
        public int slot { get; set; }
        public TypeInfo type { get; set; }
        public class TypeInfo
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }



}
