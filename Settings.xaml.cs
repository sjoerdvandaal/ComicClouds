using System.Security.Claims;
using System.Windows;

namespace game
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void MusicOnButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.BackgroundMusic != null)
            {
                App.BackgroundMusic.Play();
            }
            else
            {
                MessageBox.Show("Background music is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MusicOffButton_Click(object sender, RoutedEventArgs e)
        {
            App.BackgroundMusic.Pause();
        }

        private void VolumeOnButton_Click(object sender, RoutedEventArgs e)
        {
            // Volume not finished
        }

        private void VolumeOffButton_Click(object sender, RoutedEventArgs e)
        {
            // Volume not finished
        }
    }
}
