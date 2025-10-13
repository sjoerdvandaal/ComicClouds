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

namespace game
{
    /// <summary>
    /// Interaction logic for EnterName.xaml
    /// </summary>
    public partial class EnterName : Window
    {



        public EnterName()
        {
            InitializeComponent();
        }

        private void EnterName_Back_Button_Click(object sender, RoutedEventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
        private void EnterName_Start_Button_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;
            GameWindow gw = new GameWindow(name);
            gw.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

    }
}