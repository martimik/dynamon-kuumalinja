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
                // haetaan käyttäjät ja salasanat
                if (KuumalinjaConnect.CreateUser(username, password))
                {
                    //kirjaudutaan sisään uudella haulla, koska ei tiedetä käyttäjän userID:tä
                    Login.Kirjaudu(username, password);
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
