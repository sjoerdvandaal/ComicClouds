using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace game
{
    public partial class GameWindow : Window
    {
        private DispatcherTimer gameTimer;
        private bool isPaused = false;
        private string _playerName;
        private DateTime startTime;
        private TimeSpan pausedDuration = TimeSpan.Zero;
        private DateTime pauseStartTime;
        private int finalScore = 0;
        private double scoreMultiplier = 1.5; // change this for difficulty adjustment

        // Dragging clouds
        private bool isCloudDragging = false;
        private Point cloudClickPosition;
        private Border selectedCloud = null;

        // ✅ Track if player finished the game
        private bool hasCompletedGame = false;

        public GameWindow()
        {
            InitializeComponent();
            InitializeGameTimer();
        }

        public GameWindow(string playerName)
        {
            InitializeComponent();
            _playerName = playerName;
            InitializeGameTimer();
        }

        private void InitializeGameTimer()
        {
            startTime = DateTime.Now;
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (isPaused) return;

            TimeSpan elapsed = DateTime.Now - startTime - pausedDuration;
            if (GameTimerText != null)
                GameTimerText.Text = $"{elapsed.Minutes:00}:{elapsed.Seconds:00}";
        }

        private void GameWindow_Back_Button_Click(object sender, RoutedEventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPaused)
            {
                isPaused = false;
                PauseOverlay.Visibility = Visibility.Collapsed;
                pausedDuration += DateTime.Now - pauseStartTime;
            }
            else
            {
                isPaused = true;
                PauseOverlay.Visibility = Visibility.Visible;
                pauseStartTime = DateTime.Now;
            }
        }

        // --- CLOUD DRAGGING & SNAP LOGIC ---
        private void Cloud_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedCloud = sender as Border;
            if (selectedCloud != null)
            {
                isCloudDragging = true;
                cloudClickPosition = e.GetPosition(GameCanvas);
                selectedCloud.CaptureMouse();
            }
        }

        private void Cloud_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCloudDragging && selectedCloud != null)
            {
                Point currentPosition = e.GetPosition(GameCanvas);
                double offsetX = currentPosition.X - cloudClickPosition.X;
                double offsetY = currentPosition.Y - cloudClickPosition.Y;

                double newLeft = Canvas.GetLeft(selectedCloud) + offsetX;
                double newTop = Canvas.GetTop(selectedCloud) + offsetY;

                Canvas.SetLeft(selectedCloud, newLeft);
                Canvas.SetTop(selectedCloud, newTop);

                cloudClickPosition = currentPosition;
            }
        }

        private void Cloud_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedCloud != null)
            {
                isCloudDragging = false;
                selectedCloud.ReleaseMouseCapture();

                Border[] panels = { Panel1, Panel2, Panel3 };

                foreach (var panel in panels)
                {
                    double panelLeft = Canvas.GetLeft(panel);
                    double panelTop = Canvas.GetTop(panel);
                    double panelRight = panelLeft + panel.ActualWidth;
                    double panelBottom = panelTop + panel.ActualHeight;

                    double cloudLeft = Canvas.GetLeft(selectedCloud);
                    double cloudTop = Canvas.GetTop(selectedCloud);
                    double cloudRight = cloudLeft + selectedCloud.ActualWidth;
                    double cloudBottom = cloudTop + selectedCloud.ActualHeight;

                    bool isOverlapping = cloudRight > panelLeft && cloudLeft < panelRight &&
                                         cloudBottom > panelTop && cloudTop < panelBottom;

                    if (isOverlapping)
                    {
                        Canvas.SetLeft(selectedCloud, panelLeft);
                        Canvas.SetTop(selectedCloud, panelTop);
                        selectedCloud.Width = panel.ActualWidth;
                        selectedCloud.Height = panel.ActualHeight;

                        if (panel.RenderTransform != null)
                            selectedCloud.RenderTransform = panel.RenderTransform.Clone();

                        if (selectedCloud.Child is TextBlock tb)
                        {
                            tb.TextAlignment = TextAlignment.Center;
                            tb.VerticalAlignment = VerticalAlignment.Center;
                            tb.TextWrapping = TextWrapping.Wrap;
                        }

                        break;
                    }
                }

                selectedCloud = null;
            }
        }

        // --- CHECK ORDER LOGIC ---
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            string[] correctOrder = {
                "We should go now!",
                "Wait, what about the others?",
                "Let's stick together!"
            };

            Border[] panels = { Panel1, Panel2, Panel3 };
            string[] playerOrder = new string[3];

            for (int i = 0; i < panels.Length; i++)
            {
                Border panel = panels[i];
                string cloudText = string.Empty;

                foreach (var cloud in new Border[] { Cloud1, Cloud2, Cloud3 })
                {
                    double panelLeft = Canvas.GetLeft(panel);
                    double panelTop = Canvas.GetTop(panel);
                    double panelRight = panelLeft + panel.ActualWidth;
                    double panelBottom = panelTop + panel.ActualHeight;

                    double cloudLeft = Canvas.GetLeft(cloud);
                    double cloudTop = Canvas.GetTop(cloud);
                    double cloudRight = cloudLeft + cloud.Width;
                    double cloudBottom = cloudTop + cloud.Height;

                    bool isOverlapping = cloudRight > panelLeft && cloudLeft < panelRight &&
                                         cloudBottom > panelTop && cloudTop < panelBottom;

                    if (isOverlapping && cloud.Child is TextBlock tb)
                    {
                        cloudText = tb.Text;
                        break;
                    }
                }
                playerOrder[i] = cloudText;
            }

            bool allCorrect = true;
            for (int i = 0; i < 3; i++)
            {
                if (playerOrder[i] != correctOrder[i])
                {
                    allCorrect = false;
                    break;
                }
            }

            if (allCorrect)
            {
                TimeSpan totalTime = DateTime.Now - startTime - pausedDuration;
                int timeInSeconds = (int)totalTime.TotalSeconds;

                finalScore = (int)(10000 / Math.Max(timeInSeconds, 1) * scoreMultiplier);

                MessageBox.Show($"Correct order! Well done!\n\nYour Score: {finalScore}",
                                "Result", MessageBoxButton.OK, MessageBoxImage.Information);

                hasCompletedGame = true;

                // Save the score to database
                SaveHighScore(_playerName, finalScore);

                // ✅ Return to Start Screen after pressing OK
                StartPage sp = new StartPage();
                sp.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Not quite right. Try again!", "Result", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        // --- DATABASE HELPER METHODS ---
        private string GetDatabasePath()
        {
            string folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            string dbPath = System.IO.Path.Combine(folder, "HighScores.db");
            return dbPath;
        }

        private void EnsureDatabaseExists()
        {
            string dbPath = GetDatabasePath();

            if (!System.IO.File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    string createTable = @"CREATE TABLE IF NOT EXISTS HighScores (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            PlayerName TEXT NOT NULL,
                                            Score INTEGER NOT NULL,
                                            DatePlayed TEXT NOT NULL
                                          );";
                    using (var cmd = new SQLiteCommand(createTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void SaveHighScore(string playerName, int score)
        {
            EnsureDatabaseExists();

            string dbPath = GetDatabasePath();

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string insertQuery = "INSERT INTO HighScores (PlayerName, Score, DatePlayed) VALUES (@name, @score, @date)";
                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@name", playerName);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
