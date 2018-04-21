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

        
        #region Events
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

        private void btnLogin_Click(object sender, RoutedEventArgs e) // on button click, based on buttons content do stuff
        {
            try
            {
                if(btnLogin.Content.ToString() == "Kirjaudu")
                {
                    // goes to viewmodel to check users'n'stuff
                    Login.Kirjaudu(txtUser.Text, pwbPassword.Password);
                }
                else if(btnLogin.Content.ToString() == "Rekisteröidy")
                {
                    Register.LuoKayttaja(txtUser.Text, pwbPassword.Password);
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnLogin.Content.ToString() == "Kirjaudu")
                {
                    btnLogin.Content = "Rekisteröidy";
                    btnRegister.Content = "Kirjaudu";
                }
                else if (btnLogin.Content.ToString() == "Rekisteröidy")
                {
                    btnLogin.Content = "Kirjaudu";
                    btnRegister.Content = "Rekisteröidy";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
