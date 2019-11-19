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
    public class PokéCache
    {
        public PokémonDto[] PokémonCache { get; set; }
        private readonly string APIUrl;

        public PokéCache(string apiUrl)
        {
            APIUrl = apiUrl;
            PokémonCache = new PokémonDto[150];
        }

        public void RefreshCache()
        {
            Parallel.For(1, 15, async i => PokémonCache[i] = await GetPokémon(i));
        }

        private async Task<PokémonDto> GetPokémon(int i)
        {
            WebClient webClient = new WebClient();
            var result = await Task.Run(() => webClient.DownloadString(string.Format(APIUrl, i)));
            return JsonConvert.DeserializeObject<PokémonDto>(result);
        }
    }
}
