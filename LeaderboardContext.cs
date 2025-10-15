SQLite SQLiteAssembly = System.Data.SQLite;
< DataGrid x: Name = "LeaderboardGrid" AutoGenerateColumns = "True" Margin = "10" />
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.ComponentModel;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Input;
    using System.Windows.Documents;
    using System.Data;
using System.Data.SQLite;

private void LoadLeaderboard()
{
    string dbPath = "Data Source=Data/highscore.db"; 
    string query = "SELECT Player, Highscore, Date FROM Highscores ORDER BY Highscore DESC";

    using (SQLiteConnection connection = new SQLiteConnection(dbPath))
    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
    {
        DataTable table = new DataTable();
        adapter.Fill(table);
        LeaderboardGrid.ItemsSource = table.DefaultView;
    }
}
private void Window_Loaded(object sender, RoutedEventArgs e)
{
    LoadLeaderboard();
}