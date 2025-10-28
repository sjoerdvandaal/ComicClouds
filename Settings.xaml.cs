using System;
using System.Windows;
using System.Windows.Controls;

namespace game
{
    public partial class SettingsWindow : Window
    {
        private bool isLoaded = false;

        public SettingsWindow()
        {
            InitializeComponent();
            this.Loaded += SettingsWindow_Loaded;
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            isLoaded = true;

            // ✅ Get current running App instance
            var app = (ComicClouds.App)Application.Current;

            // Initialize music slider if BackgroundMusic exists
            if (app.BackgroundMusic != null)
            {
                MusicVolumeSlider.Value = app.BackgroundMusic.Volume * 100;
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
            if (!isLoaded) return;

            var app = (ComicClouds.App)Application.Current;

            if (app.BackgroundMusic != null)
            {
                app.BackgroundMusic.Volume = MusicVolumeSlider.Value / 100.0;
                VolumePercentageLabel.Content = $"{(int)MusicVolumeSlider.Value}%";
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isLoaded) return;

            var app = (ComicClouds.App)Application.Current;

            if (app.BackgroundMusic != null)
            {
                app.BackgroundMusic.Volume = VolumeSlider.Value / 100.0;
                VolumeSliderPercentageLabel.Content = $"{(int)VolumeSlider.Value}%";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Go back to StartPage
            StartPage sp = new StartPage();
            sp.Show();
            this.Close();
        }
    }
}
