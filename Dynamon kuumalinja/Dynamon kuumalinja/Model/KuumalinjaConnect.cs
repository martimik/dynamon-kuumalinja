using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;// mysql nimiavaruus
using MySql.Data.MySqlClient; // mysql nimiavaruus


namespace Dynamon_kuumalinja.Model
{
    public static class KuumalinjaConnect
    {
        public static void ConnectToKuumalinja(string sql) // parametrinä välitetään sql-query
        {
            // luodaan yhteys tietokantaan
            string connstr = ConnectionString();
            using(MySqlConnection conn = new MySqlConnection(connstr)) // tietokannan määritykset tässä
            {
                conn.Open(); // avataan yhteys
                MySqlCommand cmd = new MySqlCommand(sql, conn);// asetetaan yhteys ja komento samaan muuttujaan
                using (MySqlDataReader reader = cmd.ExecuteReader()) // luodaan datanlukija
                {
                    
                }
            }

        }
        
        private static string ConnectionString()
        {
            string pw = "sqlqyXvH72gMsLbyuFuXLAll43MHIcTJ";
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=L4721_3;user=L4721;password={0}", pw);
        }
    }

    
}
