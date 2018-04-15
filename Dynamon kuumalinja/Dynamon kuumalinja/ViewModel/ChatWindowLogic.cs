using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dynamon_kuumalinja
{
    public static class ChatWindowLogic
    {

        public static List<Channel> HaeKanavat()// haetaan kanavat
        {
            try
            {
                List<Channel> channels = new List<Channel>();
                channels = KuumalinjaConnect.GetChannels(); // haetaan kanavat
                
                return channels;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        
        public static List<Message> HaeViestit(int channel)// haetaan kanavan viestit
        {
            try
            {
                List<Message> messages = new List<Message>();
                messages = KuumalinjaConnect.GetMessages(channel); // hakee viestit tietokannasta
                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ViestinLahetys(Message viesti) // lähetetään viesti
        {
            try
            {
                KuumalinjaConnect.SendMessage(viesti);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static void CheckChannelPassword(int kanava, string password)
        {
            try
            {
                if (KuumalinjaConnect.CheckChannelPassword(kanava, password))
                {
                    // jos salasana on ok, haetaan kanavan viestit
                    HaeViestit(kanava);
                }
                else
                {
                    // virheviesti tooltippiin
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
