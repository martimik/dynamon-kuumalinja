using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dynamon_kuumalinja
{
    public static class ChatWindowLogic
    {

        public static List<Channel> HaeKanavat()
        {
            try
            {
                List<Channel> channels = new List<Channel>();
                channels = KuumalinjaConnect.GetChannels();            
                return channels;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        
        public static List<Message> HaeViestit(Channel channel)
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

    }
}
