using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace game
{
    /// <summary>
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : Window
    {
        public ObservableCollection<Player> Players { get; set; }

        public Leaderboard()
        {
            InitializeComponent();

            Players = new ObservableCollection<Player>
            {
                new Player { Name = "Bas", Score = 150 },
                new Player { Name = "Jente", Score = 100 }
            };

            DataContext = this;
        }
    }

    public class Player
    {
        public required string Name { get; set; }
        public int Score { get; set; }
    }
}
