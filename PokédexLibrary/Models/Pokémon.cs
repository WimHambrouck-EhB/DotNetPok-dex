using System;
using System.Collections.Generic;

namespace PokédexLibrary.Models
{
    public class Pokémon
    {
        public int Id { get; set; }
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
                   Id == pokémon.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
