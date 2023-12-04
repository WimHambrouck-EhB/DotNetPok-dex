using System.Collections.Generic;

namespace PokédexLibrary.Models
{
    public class Pokédex
    {
        public List<Team> Teams { get; set; }

        public Pokédex()
        {
            Teams = new List<Team>();
        }
    }
}
