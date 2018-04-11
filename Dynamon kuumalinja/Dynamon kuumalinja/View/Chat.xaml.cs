using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dynamon_kuumalinja
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        // fields
        private User kayttaja;
        // constructors
        public Chat(User user)
        {
            InitializeComponent();
            kayttaja = user;
            InitChat();
        }
        // methods
        public void InitChat()
        {
            List<Channel> kuumatlinjat = new List<Channel>();
            kuumatlinjat = ChatWindowLogic.HaeKanavat();
            foreach(Channel kuumalinja in kuumatlinjat)
            {
                libChannels.Items.Add(kuumalinja.ChannelName);
            }
        }

        public void GetMessages(Channel channel)
        {
            List<Message> kuumatviestit = new List<Message>();
            kuumatviestit = ChatWindowLogic.HaeViestit(channel);
        }

    }
}
