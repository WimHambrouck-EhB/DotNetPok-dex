using PokédexLib.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace PokédexLib.Models
{
    public class Team
    {
        public string Name { get; set; }
        public List<Pokémon> AllPokémon => MyPokémon.ToList();
        private readonly HashSet<Pokémon> MyPokémon;
        private const int MAX_POKÉMON = 6;

        public Team(string name)
        {
            Name = name;
            MyPokémon = new HashSet<Pokémon>();
        }

        public bool AddPokémon(Pokémon pokémon)
        {
            if (MyPokémon.Count >= MAX_POKÉMON)
            {
                throw new TeamIsFullException($"A team cannot contain more than {MAX_POKÉMON} Pokémon.");
            }

            return MyPokémon.Add(pokémon);
        }

        public bool RemovePokémon(Pokémon pokémon)
        {
            return MyPokémon.Remove(pokémon);
        }

    }
}