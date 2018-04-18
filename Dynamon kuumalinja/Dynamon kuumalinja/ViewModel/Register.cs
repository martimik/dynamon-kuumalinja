using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Dynamon_kuumalinja
{
    static class Register
    {
        public static void LuoKayttaja(string username, string password)
        {
            try
            {
                User kayttaja = new User() { UserName = username, PassWord = password};

                // haetaan käyttäjät ja salasanat
                if (KuumalinjaConnect.CreateUser(kayttaja.UserName, kayttaja.PassWord))
                {
                    //avataan chat käyttäjälle
                    var mainWindow = (Application.Current.MainWindow as MainWindow);
                    if (mainWindow != null)
                    {
                        Chat chatti = new Chat(kayttaja);
                        mainWindow.Close();
                        chatti.Show();
                    }
                }
                else // jos väärin niin popataan virheviesti
                {
                    MessageBox.Show("User already exists");
                }
            }
            catch (Exception ex) // poikkeustenkäsittelyä
            {
                throw ex;
            }
        }
        
    }
}
