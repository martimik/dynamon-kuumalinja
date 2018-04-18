using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;// mysql nimiavaruus
using MySql.Data.MySqlClient; // mysql nimiavaruus
using System.Configuration;
using System.Security.Cryptography;
using System.Windows;

namespace Dynamon_kuumalinja
{
    public static class KuumalinjaConnect
    {       
        // connection string pitää muuttaa
        private static string ConnectionString()// 
        {

            //string pw = "7D96QWGRw3MofzwXw7pr7Dqj6Uhvp9Hj";       

            string pw = "";
            
            if (ConfigurationManager.AppSettings["hash"] == "false")// hash logiikka
            {
                pw = ConfigurationManager.AppSettings["Password"];                              
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["hash"].Value = "true";// asetetaan hashays todeksi
                configuration.AppSettings.Settings["Password"].Value = EncryptPass(ConfigurationManager.AppSettings["Password"]);
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            else if(ConfigurationManager.AppSettings["hash"] == "true")
            {
                pw = DecryptPass(ConfigurationManager.AppSettings["Password"]);
            }


            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8936_3;user=K8936;password={0}", pw);            
        }
        #region hash      

        private static string EncryptPass(string pass)
        {            
            char[] charhash = pass.ToCharArray();
            pass = "";
            for (int i = 0; i < charhash.Length; i++)
            {
                int no = Convert.ToInt32(charhash[i]) + 10;
                string r = Convert.ToChar(no).ToString();
                pass += r;
            }
            return pass;            
        }

        private static string DecryptPass(string pass)
        {
            
            char[] readChar = pass.ToCharArray();
            pass = "";
            for (int i = 0; i < readChar.Length; i++)
            {
                int no = Convert.ToInt32(readChar[i]) - 10;
                string r = Convert.ToChar(no).ToString();
                pass += r;
            }
            return pass;
        }
        #endregion
        #region Login
        /*
         *
         * Login    
         *   
         */
        public static User CheckLogin(User user)
        {
            try
            {                
                // luodaan yhteys tietokantaan
                string connstr = ConnectionString();
                // haetaan käyttäjät
                string sql = string.Format("SELECT userID, username, password FROM user WHERE username = @username AND password = @password");
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    cmd.Parameters.AddWithValue("@username", user.UserName);
                    cmd.Parameters.AddWithValue("@password", user.PassWord);
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
        #endregion
        #region Register
        public static bool CreateUser(string username, string password)// tarkistaa onko käyttäjänimi vapaana
        {
            try
            {
                // luodaan yhteys tietokantaan
                string connstr = ConnectionString();
                // haetaan käyttäjät
                string sql = string.Format("SELECT userID, username FROM user WHERE username = @username");
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            //ORM, readerin tiedot olioon
                            if (username == reader.GetString(0)) // jos löydetään käyttäjänimellä mitään palautetaan false
                            {
                                return false;
                            }                            
                        }
                    }

                    // tietokannasta ei ole tässä pisteessä löytynyt mitään, joten luodaan insert
                    sql = string.Format("INSERT INTO user (username, password, classID) VALUES (@username, @password, 1)"); // muutetaan sql lause
                    cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan                    
                    cmd.Parameters.AddWithValue("@password", password);                        
                    cmd = conn.CreateCommand();// luodaan komento yhteyteen
                    cmd.CommandText = sql; // sql insert lause
                    cmd.ExecuteNonQuery(); // ajetaan sql lause
                    return true;                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        #endregion
        #region Channels
        /*
         *
         * Channels    
         *   
         */


        public static List<Channel> GetChannels()// haetaan kanavat
        {
            try
            {
                List<Channel> kanavat = new List<Channel>();
                string connstr = ConnectionString();
                string sql = string.Format("SELECT channelID, channelName FROM channel"); 
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            kanavat.Add(new Channel() { ChannelID = reader.GetInt32(0), ChannelName = reader.GetString(1) });
                        }
                    }
                }
                return kanavat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckChannelPassword(int kanava, string pw)// kanavan salasanan tarkistus
        {
            try
            {
                string connstr = ConnectionString();
                string sql = string.Format("SELECT channelPassword FROM channel WHERE channelID = @channel");
                
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    cmd.Parameters.AddWithValue("@channel", kanava);
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            if(pw == reader.GetString(0))// jos syötetty salasana on sama kuin tietokannassa oleva, palautetaan truena
                            {
                                return true;
                            }                            
                        }
                    }
                }
                return false; // jos rivit ei matchaa niin false
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Messages
        /*
         *
         * Messages
         *   
         */

        public static List<Message> GetMessages(int kanava)
        {
            try
            {
                List<Message> messages = new List<Message>();
                string connstr = ConnectionString();
                string sql = string.Format("SELECT message.timeStamp, user.username, message.content, message.messageID FROM message JOIN user ON message.userID = user.userID WHERE channelID = @channel ORDER BY message.messageID ASC");
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    cmd.Parameters.AddWithValue("@channel", kanava);
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            messages.Add(new Message() { TimeStamp = reader.GetString(0), UserName = reader.GetString(1), Content = reader.GetString(2) });
                        }
                    }
                }                
                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendMessage(Message message)// viestin lähetys
        {
            try
            {
                string connstr = ConnectionString();
                string sql = string.Format("INSERT INTO message (channelID, userID, timeStamp, content) VALUES (@channel, @userid, NOW(), @content)");
                using(MySqlConnection conn = new MySqlConnection(connstr))
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = conn.CreateCommand();// luodaan komento yhteyteen
                    cmd.Parameters.AddWithValue("@channel", message.ChannelID);
                    cmd.Parameters.AddWithValue("@userid", message.UserID);
                    cmd.Parameters.AddWithValue("@content", message.Content);
                    cmd.CommandText = sql; // sql insert lause
                    cmd.ExecuteNonQuery(); // ajetaan sql lause                    
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
