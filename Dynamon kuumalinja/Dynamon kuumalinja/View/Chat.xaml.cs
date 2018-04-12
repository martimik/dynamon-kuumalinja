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
            //List<Channel> kuumatlinjat = new List<Channel>();
            libChannels.ItemsSource = ChatWindowLogic.HaeKanavat();
            
        }

        public void GetMessages(int channel)
        {
            txbChatWindow.Children.Clear();
            List<Message> kuumatviestit = new List<Message>();
            kuumatviestit = ChatWindowLogic.HaeViestit(channel);
            foreach(Message viesti in kuumatviestit)
            {
                TextBlock kuumaviesti = new TextBlock();
                kuumaviesti.Height = 40;
                kuumaviesti.Width = 300;
                kuumaviesti.Text = string.Format("[{0}] - {1} : {2}", viesti.TimeStamp, viesti.UserName, viesti.Content);
                txbChatWindow.Children.Add(kuumaviesti);
            }
        }

        //Events      

       

        private void libChannels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                txbChatWindow.Children.Clear();
                int kanava = (int)libChannels.SelectedValue;
                GetMessages(kanava);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void txbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    txbChatWindow.Children.Clear();
                    int kanava = (int)libChannels.SelectedValue;
                    Message message = new Message() { ChannelID = kanava, UserID = kayttaja.UserID, Content = txbMessage.Text};                    
                    ChatWindowLogic.ViestinLahetys(message); // lähetetään viesti                    
                    GetMessages(kanava);// haetaan viestit uudelleen 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
            
        }
    }
}
