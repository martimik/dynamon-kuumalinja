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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dynamon_kuumalinja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Return)// when enter key is pressed, calling btnLogin_click
            {
                btnLogin_Click(sender, e);
            }
        }

        private void pwbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)// when enter key is pressed, calling btnLogin_click
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) 
        {
            // goes to viewmodel to call databasestuff
            txbUser.Text = txtUser.Text;
        }
    }
}
