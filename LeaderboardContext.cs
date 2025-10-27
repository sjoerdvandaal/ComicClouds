using game;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLeaderboard();
        }

        private void LoadLeaderboard()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "highscore.db");
            string connectionString = $"Data Source={dbPath}";
            string query = "SELECT Player, Highscore, Date FROM Highscores ORDER BY Highscore DESC";

            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found: " + dbPath);
                return;
            }

            using (var connection = new SQLiteConnection(connectionString))
            using (var adapter = new SQLiteDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                LeaderboardGrid.ItemsSource = table.DefaultView;
            }
        }
    }
}
