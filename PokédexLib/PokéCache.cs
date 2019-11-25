using Newtonsoft.Json;
using PokédexLib.DTO;
using PokédexLib.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PokédexLib
{
    /// <summary>
    /// Singleton klasse die een cache van PokémonDTO's bevat.
    /// Meer info over Singletons in C#: https://csharpindepth.com/articles/singleton
    /// </summary>
    public class PokéCache
    {
        public static PokéCache Instance { get; } = new PokéCache();

        public PokémonDto[] PokémonDtos { get; private set; }
        public PokémonSpeciesDto[] PokémonSpeciesDtos { get; private set; }

        public static string ApiUrl => "https://pokeapi.co/api/v2/";
        private const int amountToCache = 10;
        private const int amountOfPkmn = 964; // er zitten 964 Pokémon in de API


        static PokéCache() { }

        private PokéCache()
        {
            PokémonDtos = new PokémonDto[amountOfPkmn + 1];
            PokémonSpeciesDtos = new PokémonSpeciesDto[amountOfPkmn + 1];
            DownloadPokémon();
        }

        private async void DownloadPokémon()
        {
            await Task.Run(() => Parallel.For(1, amountToCache + 1, i =>
            {
                WebClient webClient = new WebClient();
                var pkmn = webClient.DownloadString($"{ApiUrl}pokemon/{i}");
                var species = webClient.DownloadString($"{ApiUrl}pokemon-species/{i}");
                PokémonDtos[i] = JsonConvert.DeserializeObject<PokémonDto>(pkmn);
                PokémonSpeciesDtos[i] = JsonConvert.DeserializeObject<PokémonSpeciesDto>(species);
                webClient.Dispose();
            }));
        }
    }
}
