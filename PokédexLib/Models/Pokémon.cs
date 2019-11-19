using System.Collections.Generic;

namespace PokédexLib.Models
{
    public class Pokémon
    {
        public string Name { get; set; }
        public List<Move> Moves { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public List<string> Types { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Pokémon pokémon &&
                   Name == pokémon.Name;
        }

        public override int GetHashCode()
        {
            return -1386946022 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
