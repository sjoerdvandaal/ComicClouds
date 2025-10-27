using System.Windows;

namespace game
{
    public partial class SettingsWindow : Window
    {
        private bool isLoaded = false; // track when the window is fully loaded

        public SettingsWindow()
        {
            InitializeComponent();
            this.Loaded += SettingsWindow_Loaded;
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            isLoaded = true;

            // Initialize music slider if BackgroundMusic exists
            if (App.BackgroundMusic != null)
            {
                MusicVolumeSlider.Value = App.BackgroundMusic.Volume * 100;
                VolumePercentageLabel.Content = $"{(int)MusicVolumeSlider.Value}%";
            }

            // Initialize optional volume slider safely
            if (VolumeSlider != null && VolumeSliderPercentageLabel != null)
            {
                VolumeSlider.Value = 100;
                VolumeSliderPercentageLabel.Content = "100%";
            }
        }

        private void MusicVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isLoaded) return; // skip if window is not loaded yet

            if (App.BackgroundMusic != null)
            {
                App.BackgroundMusic.Volume = MusicVolumeSlider.Value / 100.0;
                if (VolumePercentageLabel != null)
                    VolumePercentageLabel.Content = $"{(int)MusicVolumeSlider.Value}%";
            }
        }


        //not finisshed, optional slider for sound effects
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isLoaded) return;

            if (VolumeSlider != null && VolumeSliderPercentageLabel != null)
            {
                VolumeSliderPercentageLabel.Content = $"{(int)VolumeSlider.Value}%";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }
}




