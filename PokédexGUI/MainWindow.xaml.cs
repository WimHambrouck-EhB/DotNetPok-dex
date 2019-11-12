using PokédexLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace PokédexGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pokédex pokédex;
        public MainWindow()
        {
            InitializeComponent();
            pokédex = new Pokédex();
            pokédex.Teams.Add(new Team("The very best like no one ever was"));

            InitShortcuts();
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
    }
}
