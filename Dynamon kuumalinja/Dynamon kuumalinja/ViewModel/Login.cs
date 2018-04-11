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
                User kayttaja = new User(username, password);

                // haetaan käyttäjät ja salasanat
                if (KuumalinjaConnect.CheckLogin(kayttaja) != null)
                {
                    kayttaja = KuumalinjaConnect.CheckLogin(kayttaja); // ei hyvä, tekee mysql haun uudestaan
                    var mainWindow = (Application.Current.MainWindow as MainWindow);
                    if(mainWindow != null)
                    {                       
                        Chat chatti = new Chat(kayttaja);
                        mainWindow.Close();
                        chatti.Show();
                    }                    
                }
                else // jos väärin niin popataan virheviesti
                {
                    MessageBox.Show("Wrong username/password");
                }
            }
            catch (Exception ex) // poikkeustenkäsittelyä
            {
                throw ex;
            }
            
        }
        

    }
}
