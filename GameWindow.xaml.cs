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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace game
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private DispatcherTimer gameTimer;
        private bool isPaused = false;
        public GameWindow()
        {
            InitializeComponent();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }


        private string _playerName;

        public GameWindow(string playerName)
        {
            InitializeComponent();
            _playerName = playerName;
            PlayerNameTextBlock.Text = playerName;
        }


        private void GameWindow_Back_Button_Click(object sender, RoutedEventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (isPaused) return;

        }
        private void GameWindow_Finnish_Game_Button_Click(object sender, RoutedEventArgs e)
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
                PauseButton.Content = "Pause";
                PauseOverlay.Visibility = Visibility.Collapsed;
                PauseButton.Visibility = Visibility.Visible;
            }
            else
            {
                isPaused = true;
                PauseButton.Content = "Resume";
                PauseOverlay.Visibility = Visibility.Visible;
                PauseButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
