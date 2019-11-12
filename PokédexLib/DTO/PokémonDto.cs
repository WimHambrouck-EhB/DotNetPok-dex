using System;
using System.Collections.Generic;
using System.Text;

namespace PokédexLib.DTO
{
    public class PokémonDto
    {
        public AbilityDTO[] abilities { get; set; }
        public int base_experience { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public bool is_default { get; set; }
        public string location_area_encounters { get; set; }
        public MoveDto[] moves { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public SpeciesDto species { get; set; }
        public SpritesDto sprites { get; set; }
        public StatsDto[] stats { get; set; }
        public TypeDto[] types { get; set; }
        public int weight { get; set; }
    }
}
