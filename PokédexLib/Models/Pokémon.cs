using System.Collections.Generic;

namespace PokédexLib.Models
{
    public class Pokémon
    {
        public string Naam { get; set; }
        public List<Move> Moves { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public List<string> Types { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Pokémon pokémon &&
                   Naam == pokémon.Naam;
        }

        public override int GetHashCode()
        {
            return -1386946022 + EqualityComparer<string>.Default.GetHashCode(Naam);
        }
    }
}
