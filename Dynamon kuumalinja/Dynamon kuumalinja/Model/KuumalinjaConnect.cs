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
        // KESKEN
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

        public static bool CheckLogin(string username, string password)
        {
            try
            {
                // luodaan yhteys tietokantaan
                string connstr = ConnectionString();
                // haetaan käyttäjät
                string sql = string.Format("SELECT username, password FROM user WHERE username = '{0}' AND password = '{1}'", username, password);
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            //ORM, readerin tiedot olioon
                            if (username == reader.GetString(0) && password == reader.GetString(1))
                            {
                                return true;
                            }
                        }
                    }                    
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
