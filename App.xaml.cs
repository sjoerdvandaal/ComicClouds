using System;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace game
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MediaPlayer BackgroundMusic { get; private set; }

        static App()
        {
            BackgroundMusic = new MediaPlayer();
            BackgroundMusic.Open(new Uri("Audio/AudioGame.mp3", UriKind.Relative));
            BackgroundMusic.Volume = 1.0;

            BackgroundMusic.MediaEnded += (s, args) =>
            {
                BackgroundMusic.Position = TimeSpan.Zero;
                BackgroundMusic.Play();
            };
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BackgroundMusic.Play(); // Start playing after app launches
        }
    }

}