using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Dynamon_kuumalinja
{
    public static class ChatWindowLogic
    {
        


        #region Channels
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

        public static bool CheckChannelPassword(int kanava, string password)
        {
            try
            {
                if (KuumalinjaConnect.CheckChannelPassword(kanava, password))
                {
                    // jos salasana on ok, haetaan kanavan viestit
                    return true;                    
                }
                else
                {
                    // virheviesti tooltippiin
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Messages
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
        #endregion
    }
}
