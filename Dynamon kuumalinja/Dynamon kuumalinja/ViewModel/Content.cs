using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamon_kuumalinja
{
    public class User
    {
        // properties
        public int UserID { get; set; }
        public string UserName { get; set; } 
        public string PassWord { get; set; }
    }

    public class Message
    {
        // properties
        public int MessageID { get; set; }
        public int ChannelID { get; set; }
        public int UserID { get; set; }
        public string TimeStamp { get; set; }
        public string Content { get; set; }
    }

    public class Channel
    {
        // properties
        public int ChannelID { get; set; }
        public string ChannelName { get; set; }
        public string ChannelPassword { get; set; }
    }
}
