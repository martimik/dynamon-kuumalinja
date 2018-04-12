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
        // connection string pitää muuttaa
        private static string ConnectionString()// 
        {
            string pw = "7D96QWGRw3MofzwXw7pr7Dqj6Uhvp9Hj";
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8936_3;user=K8936;password={0}", pw);
        }


        // login
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

        public static List<Channel> GetChannels()// haetaan kanavat
        {
            try
            {
                List<Channel> kanavat = new List<Channel>();
                string connstr = ConnectionString();
                string sql = string.Format("SELECT channelID, channelName, channelPassword FROM channel"); // haet
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                    {
                        while (reader.Read())
                        {
                            kanavat.Add(new Channel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
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

        public static List<Message> GetMessages(int kanava)
        {
            try
            {
                List<Message> messages = new List<Message>();
                string connstr = ConnectionString();
                string sql = string.Format("SELECT message.timeStamp, user.username, message.content FROM message JOIN user ON message.userID = user.userID WHERE channelID = {0}", kanava);
                using (MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
                {
                    conn.Open(); // avataan yhteys
                    MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
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
    }

}
