using System;
using System.Windows;
using System.Windows.Controls;

namespace ComicClouds
{
    public partial class App : Application
    {
        public MediaElement BackgroundMusic { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BackgroundMusic = (MediaElement)Resources["BackgroundMusicPlayer"];
            BackgroundMusic.MediaEnded += BackgroundMusicPlayer_MediaEnded;
            BackgroundMusic.Play();
        }

        private void BackgroundMusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundMusic.Position = TimeSpan.Zero;
            BackgroundMusic.Play();
        }
    }
}
