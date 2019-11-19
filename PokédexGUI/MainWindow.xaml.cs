using Newtonsoft.Json;
using PokédexLib.DTO;
using PokédexLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokédexLib.Extensions;
using System.IO;

namespace PokédexGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pokédex pokédex;
        private Pokémon activePokémon;
        private readonly WebClient pokemonClient, pokemonSpeciesClient, imageClient;
        private const string API_URL = "https://pokeapi.co/api/v2/";
        public MainWindow()
        {
            pokédex = new Pokédex();
            pokédex.Teams.Add(new Team("The very best like no one ever was"));
            pokemonClient = new WebClient
            {
                BaseAddress = API_URL + "pokemon/"
            };
            pokemonSpeciesClient = new WebClient
            {
                BaseAddress = API_URL + "pokemon-species/"
            };
            imageClient = new WebClient();

            InitializeComponent();
            GrpPkmn.Visibility = Visibility.Collapsed;
            InitShortcuts();
        }

        protected override void OnClosed(EventArgs e)
        {
            pokemonClient.Dispose();
            pokemonSpeciesClient.Dispose();
            imageClient.Dispose();
            base.OnClosed(e);
        }

        private void InitShortcuts()
        {
            var routedCommandCtrlLeft = new RoutedCommand();
            routedCommandCtrlLeft.InputGestures.Add(new KeyGesture(Key.Left, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(routedCommandCtrlLeft, BtnPrev_Click));

            var routedCommandCtrlRight = new RoutedCommand();
            routedCommandCtrlRight.InputGestures.Add(new KeyGesture(Key.Right, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(routedCommandCtrlRight, BtnNext_Click));
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("prev");
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("next");
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            var invoer = TxtSearch.Text;
            if (string.IsNullOrWhiteSpace(invoer))
            {
                MessageBox.Show("Gelieve de naam of een id van een Pokémon op te geven...", "Ongeldige invoer", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtSearch.Focus();
            }
            else
            {
                /*
                 * De volgende code gaat wachten tot alle tasks voltooid zijn.
                 * Voor een implementatie met WhenAny, zie: 
                 * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/start-multiple-async-tasks-and-process-them-as-they-complete
                 */
                GUIEnabled(false);
                var pkmnTask = Task.Run(() => pokemonClient.DownloadString(invoer));
                var speciesTask = Task.Run(() => pokemonSpeciesClient.DownloadString(invoer));
                await Task.WhenAll(pkmnTask, speciesTask);
                var pkmnDto = JsonConvert.DeserializeObject<PokémonDto>(pkmnTask.Result);           
                var speciesDto = JsonConvert.DeserializeObject<PokémonSpeciesDto>(speciesTask.Result);
                activePokémon = new Pokémon()
                {
                    Name = pkmnDto.name.FormatAsName(),
                    Attack = pkmnDto.stats.Where(stat => stat.stat.name == "attack").Select(stat => stat.base_stat).First(),
                    Defense = pkmnDto.stats.Where(stat => stat.stat.name == "defense").Select(stat => stat.base_stat).First(),
                    HP = pkmnDto.stats.Where(stat => stat.stat.name == "hp").Select(stat => stat.base_stat).First(),
                    Types = pkmnDto.types.Select(type => type.type.name).ToList(),
                    Description = speciesDto.flavor_text_entries.Where(x => x.language.name == "en").Select(x => x.flavor_text).First()
                };
                UpdateGUI();
                GUIEnabled(true);
                var imgData = await Task.Run(() => imageClient.DownloadData(pkmnDto.sprites.front_default));
                UpdateImage(imgData);
            }
        }

        private void UpdateImage(byte[] imgData)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(imgData);
            image.EndInit();
            ImgPkmn.Source = image;
        }

        private void UpdateGUI()
        {
            GrpPkmn.Header = activePokémon.Name;
            LblAttack.Content = activePokémon.Attack;
            LblDefense.Content = activePokémon.Defense;
            LblHP.Content = activePokémon.HP;
            LblTypes.Content = string.Format("Type(s): {0}", string.Join(", ", activePokémon.Types));
            TxtDescription.Text = activePokémon.Description;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GUIEnabled(bool isEnabled)
        {
            TxtSearch.IsEnabled = isEnabled;
            BtnSearch.IsEnabled = isEnabled;
            GrpPkmn.Visibility = (isEnabled) ? Visibility.Visible : Visibility.Hidden;
            BtnAdd.IsEnabled = isEnabled;
            BtnRemove.IsEnabled = isEnabled;
            BtnNext.IsEnabled = isEnabled;
            BtnPrev.IsEnabled = isEnabled;
        }
    }
}
