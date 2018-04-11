using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dynamon_kuumalinja
{
    public static class ChatWindow
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
        
    }
}
