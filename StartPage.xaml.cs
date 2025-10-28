using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace game
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {

        public StartPage()
        {
            InitializeComponent();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            EnterName en = new EnterName();
            en.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void High_Scores_Button_Click(object sender, RoutedEventArgs e)
        {
            HighScoresWindow lb = new HighScoresWindow();
            lb.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}