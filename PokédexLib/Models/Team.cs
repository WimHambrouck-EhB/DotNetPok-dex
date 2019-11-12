using PokédexLib.Exceptions;
using System.Collections.Generic;

namespace PokédexLib.Models
{
    public class Team
    {
        public string Name { get; set; }
        private readonly HashSet<Pokémon> MyPokémon;
        private const int MAX_POKÉMON = 6;

        public Team(string name)
        {
            Name = name;
            MyPokémon = new HashSet<Pokémon>();
        }

        public bool AddPokémon(Pokémon pokémon)
        {
            if(MyPokémon.Count > MAX_POKÉMON)
            {
                throw new TeamIsFullException($"Een team mag maximaal {MAX_POKÉMON} Pokémon bevatten.");
            }

            return MyPokémon.Add(pokémon);
        }

        public bool RemovePokémon(Pokémon pokémon)
        {
            return MyPokémon.Remove(pokémon);
        }

    }
}