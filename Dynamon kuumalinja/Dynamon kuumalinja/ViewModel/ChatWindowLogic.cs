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
                channels = KuumalinjaConnect.GetChannels(); // kutsutaan tietokantaan yhdistämistä     
                return channels;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        
        public static List<Message> HaeViestit(int channel)
        {
            try
            {
                List<Message> messages = new List<Message>();
                messages = KuumalinjaConnect.GetMessages(channel);
                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ViestinLahetys(Message viesti)
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

    }
}
