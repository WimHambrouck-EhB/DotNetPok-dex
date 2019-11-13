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

namespace PokédexGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pokédex pokédex;
        private PokémonDto activePokémon;
        private PokémonSpeciesDto activeSpecies;
        private readonly WebClient pokemonEndpoint, pokemonSpeciesEndpoint;
        private const string API_URL = "https://pokeapi.co/api/v2/";
        public MainWindow()
        {
            pokédex = new Pokédex();
            pokédex.Teams.Add(new Team("The very best like no one ever was"));
            pokemonEndpoint = new WebClient
            {
                BaseAddress = API_URL + "pokemon"
            };
            pokemonSpeciesEndpoint = new WebClient
            {
                BaseAddress = API_URL + "pokemon-species"
            };


            InitializeComponent();
            GrpPkmn.Visibility = Visibility.Collapsed;
            InitShortcuts();
        }

        protected override void OnClosed(EventArgs e)
        {
            pokemonEndpoint.Dispose();
            pokemonSpeciesEndpoint.Dispose();
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
                // ter info: voor een betere implementatie van meerdere simultane tasks, check:
                // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/start-multiple-async-tasks-and-process-them-as-they-complete
                EnableGUI(false);
                var t1 = Task.Run(() => pokemonEndpoint.DownloadString($"pokemon/{invoer}"));
                var t2 = Task.Run(() => pokemonSpeciesEndpoint.DownloadString($"pokemon-species/{invoer}"));
                await Task.WhenAll(t1, t2);
                activePokémon = JsonConvert.DeserializeObject<PokémonDto>(t1.Result);
                activeSpecies = JsonConvert.DeserializeObject<PokémonSpeciesDto>(t2.Result);
                UpdateGUI();
                EnableGUI(true);
            }
        }

        private void UpdateGUI()
        {
            GrpPkmn.Header = activePokémon.name.FormatAsName();
            LblAttack.Content = activePokémon.stats.Where(stat => stat.stat.name == "attack").Select(stat => stat.base_stat).First();
            LblDefense.Content = activePokémon.stats.Where(stat => stat.stat.name == "defense").Select(stat => stat.base_stat).First();
            LblHP.Content = activePokémon.stats.Where(stat => stat.stat.name == "hp").Select(stat => stat.base_stat).First();
            LblTypes.Content = string.Format("Type(s): {0}", string.Join(", ", activePokémon.types.Select(type => type.type.name)));
            TxtDescription.Text = activeSpecies.flavor_text_entries.Where(x => x.language.name == "en").Select(x => x.flavor_text).First();
        }

        private void EnableGUI(bool isEnabled)
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
