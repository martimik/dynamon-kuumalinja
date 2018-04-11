using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;// mysql nimiavaruus
using MySql.Data.MySqlClient; // mysql nimiavaruus


namespace Dynamon_kuumalinja
{
    public static class KuumalinjaConnect
    {        
        public static List<User> ConnectToKuumalinja(string sql) // parametrinä välitetään sql-query
        {
            try
            {
                List<User> users = new List<User>();

                // luodaan yhteys tietokantaan
                string connstr = ConnectionString();
                using(MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            //ORM, readerin tiedot olioon
                            
                        }
                        return users;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
        
        private static string ConnectionString()// 
        {
            string pw = "7D96QWGRw3MofzwXw7pr7Dqj6Uhvp9Hj";
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8936_3;user=K8936;password={0}", pw);
        }

        public static User CheckLogin(User user)
        {
            try
            {                
                // luodaan yhteys tietokantaan
                string connstr = ConnectionString();
                // haetaan käyttäjät
                string sql = string.Format("SELECT userID, username, password FROM user WHERE username = '{0}' AND password = '{1}'", user.UserName, user.PassWord);
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            //ORM, readerin tiedot olioon
                            if (user.UserName == reader.GetString(1) && user.PassWord == reader.GetString(2)) // oikeat tunnukset ja salasanat
                            {
                                // otataan käyttäjän id muuttujaan
                                var id = reader.GetValue(0);
                                if(id != null)
                                {
                                    user.UserID = (int)id;// castataan id käyttäjän id:ksi
                                }                                
                                return user;
                            }
                        }
                    }                    
                }
                user = null;
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
