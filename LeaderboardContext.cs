using System;
using System.Collections.Generic;
using System.Data;
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
using game;

public partial class LeaderboardWindow : Window
{
    public DataGrid LeaderboardGrid { get; set; }

    public LeaderboardWindow()
    {
        InitializeComponent();
        LeaderboardGrid = new DataGrid
        {
            Name = "LeaderboardGrid",
            AutoGenerateColumns = true,
            Margin = new Thickness(10)
        };
        this.Content = LeaderboardGrid;
        this.Loaded += Window_Loaded;
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private void LoadLeaderboard()
    {
        string dbPath = "Data Source=Data/highscore.db";
        string query = "SELECT Player, Highscore, Date FROM Highscores ORDER BY Highscore DESC";

        using (var connection = new System.Data.SQLite.SQLiteConnection(dbPath))
        using (var adapter = new SQLiteDataAdapter(query, connection))
        {
            var table = new DataTable();
            adapter.Fill(table);
            LeaderboardGrid.ItemsSource = table.DefaultView;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadLeaderboard();
    }
}