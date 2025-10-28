using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace game
{
    public partial class HighScoresWindow : Window
    {
        private void Leaderboard_Back_Button_Click(object sender, RoutedEventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
        public HighScoresWindow()
        {
            InitializeComponent();
            LoadHighScores();
        }

        private void LoadHighScores()
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "HighScores.db");
            if (!System.IO.File.Exists(dbPath)) return;

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT PlayerName, Score, DatePlayed FROM HighScores ORDER BY Score DESC LIMIT 10";
                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    HighScoreGrid.ItemsSource = table.DefaultView;
                }
            }
        }
    }
}
