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

        /// <summary>
        /// Adds a Pokémon to team.
        /// </summary>
        /// <param name="pokémon">The <see cref="Pokémon"/> to add.</param>
        /// <returns>true if the Pokémon has been added.
        /// false if the Pokémon is already present in the team.</returns>
        /// <exception cref="TeamIsFullException">Throw when team contains the amount of Pokémon defined in <see cref="MAX_POKÉMON"/>.</exception>
        public bool AddPokémon(Pokémon pokémon)
        {
            if (MyPokémon.Count >= MAX_POKÉMON)
            {
                throw new TeamIsFullException($"A team cannot contain more than {MAX_POKÉMON} Pokémon.");
            }

            return MyPokémon.Add(pokémon);
        }

        /// <summary>
        /// Removes Pokémon from team.
        /// </summary>
        /// <param name="pokémon">The <see cref="Pokémon"/> to remove.</param>
        /// <returns>true if the Pokémon is successfully found and removed; otherwise, false (e.g.: if the Pokémon is not present in this team).</returns>
        public bool RemovePokémon(Pokémon pokémon)
        {
            return MyPokémon.Remove(pokémon);
        }

    }
}