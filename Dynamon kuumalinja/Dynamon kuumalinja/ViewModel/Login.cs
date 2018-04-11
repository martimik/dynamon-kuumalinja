using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dynamon_kuumalinja
{
    public static class Login
    {
        public static void Kirjaudu(string username, string password)
        {
            try
            {
                if (KuumalinjaConnect.CheckLogin(username, password))
                {                    
                    var mainWindow = (Application.Current.MainWindow as MainWindow);
                    if(mainWindow != null)
                    {
                        Chat chatti = new Chat();                        
                        mainWindow.Close();
                        chatti.Show();
                    }                    
                }
                else
                {
                    MessageBox.Show("Wrong username/password");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        

    }
}
