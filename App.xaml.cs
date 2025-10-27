using System.Windows;
using System.Windows.Controls;

namespace game
{
    public partial class App : Application
    {
        // Global reference to the hidden MediaElement
        public static MediaElement BackgroundMusic { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Retrieve the MediaElement from resources
            BackgroundMusic = (MediaElement)Resources["BackgroundMusicPlayer"];
            BackgroundMusic.Volume = 1.0; // Start at 100%
            BackgroundMusic.Play();
        }

        // Loop the music
        private void BackgroundMusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundMusic.Position = System.TimeSpan.Zero;
            BackgroundMusic.Play();
        }
    }
}



