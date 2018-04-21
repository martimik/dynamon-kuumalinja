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
using System.Windows.Threading;

namespace Dynamon_kuumalinja
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        // fields
        private User kayttaja;
        private DispatcherTimer timer;
        private int chatRefresh = 10;

        // constructors
#region Initialize
        public Chat(User user)
        {
            InitializeComponent();
            kayttaja = user; // joudutaan linkittämään muuttujaan, jotta saadaan käyttäjä välitettyä luokan sisässä oleville metodeille
            user = null;
            InitChat();
        }
        // methods
        public void InitChat()
        {
            //bindingin kautta linkitetään setit
            libChannels.ItemsSource = ChatWindowLogic.HaeKanavat();
            timer = new DispatcherTimer();            
            timer.Tick += new EventHandler(ViestiHaku);
            timer.Interval = TimeSpan.FromSeconds(chatRefresh);            
        }

        private void ViestiHaku(object sender, EventArgs e)
        {
            int kanava = (int)libChannels.SelectedValue;
            GetMessages(kanava);
        }
        #endregion
        #region Passwordprompt

        private void btnAddChannel_Click(object sender, RoutedEventArgs e) // kanavan lisäys
        {
            timer.Stop();
            txbChatWindow.Children.Clear();

            TextBlock title = new TextBlock();
            title.Height = 40;
            title.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            title.Margin = new Thickness(0, 20, 0, 20);
            title.FontSize = 24;
            title.FontStyle = FontStyles.Italic;
            title.Text = "Add New Channel";

            TextBlock channel = new TextBlock();
            channel.Height = 30;
            channel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            channel.Margin = new Thickness(0, 20, 0, 10);
            channel.FontSize = 18;
            channel.Text = "Channel Name:";

            TextBox channelname = new TextBox();
            channelname.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            channelname.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            channelname.Height = 25;
            channelname.Width = 200;
            channelname.FontSize = 16;
            channelname.Name = "txbChannel";
            channelname.KeyDown += new KeyEventHandler(createChannel_KeyDown);

            TextBlock pass = new TextBlock();
            pass.Height = 30;
            pass.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            pass.Margin = new Thickness(0, 20, 0, 10);
            pass.FontSize = 18;
            pass.Text = "Channel Password:";

            PasswordBox password = new PasswordBox();
            password.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            password.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            password.Height = 25;
            password.Width = 200;
            password.FontSize = 16;
            password.Name = "pwbPass";

            txbChatWindow.Children.Add(title);
            txbChatWindow.Children.Add(channel);
            txbChatWindow.Children.Add(channelname);
            txbChatWindow.Children.Add(pass);
            txbChatWindow.Children.Add(password);
            password.KeyDown += new KeyEventHandler(createChannel_KeyDown);
            txbMessage.Visibility = Visibility.Hidden;

        }

        private void ShowChannelPrompt() // swaps chat to password prompt
        {
            timer.Stop(); // pysäyttää automaattisen viestinhaun

            TextBlock password = new TextBlock();// luodaan 
            password.Height = 30;
            password.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            password.Margin = new Thickness(0, 20, 0, 10);
            password.FontSize = 18;
            password.Text = "Channel Password:";

            PasswordBox pass = new PasswordBox();
            pass.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            pass.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            pass.Height = 25;
            pass.Width = 200;
            pass.FontSize = 16;
            pass.Name = "pwbPass";
            pass.KeyDown += new KeyEventHandler(pwbPass_KeyDown);

            txbChatWindow.Children.Clear();
            txbMessage.Visibility = Visibility.Hidden;
            txbChatWindow.Children.Add(password);
            txbChatWindow.Children.Add(pass);
        }

       public void GetMessages(int channel)
        {
            txbChatWindow.Children.Clear();
            List<Message> kuumatviestit = new List<Message>();
            kuumatviestit = ChatWindowLogic.HaeViestit(channel);
            foreach (Message viesti in kuumatviestit)
            {
                TextBlock kuumaviesti = new TextBlock();
                kuumaviesti.Height = 20;
                kuumaviesti.Width = 500;
                kuumaviesti.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                kuumaviesti.FontSize = 16;
                kuumaviesti.Text = string.Format("[{0}] - {1} : {2}", viesti.TimeStamp, viesti.UserName, viesti.Content);
                txbChatWindow.Children.Add(kuumaviesti);
            }
        }
        #endregion
        #region Events
        //Events
        private void createChannel_KeyDown(object sender, KeyEventArgs e)
        {
            /* KESKEN
            *             
            *            
            *           
            *          
            *         
            */
            try
            {
                if (e.Key == Key.Return)
                {
                                        
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                kayttaja = null;            
                if (kayttaja == null)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void txbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    txbChatWindow.Children.Clear(); // tyhjentää chatti ikkunan
                    int kanava = (int)libChannels.SelectedValue;
                    Message message = new Message() { ChannelID = kanava, UserID = kayttaja.UserID, Content = txbMessage.Text };
                    ChatWindowLogic.ViestinLahetys(message); // lähetetään viesti                    
                    GetMessages(kanava);// haetaan viestit uudelleen 
                    txbMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }      
        private void pwbPass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    string password = "";
                    txbChatWindow.Children.Clear(); // tyhjentää chatti ikkunan
                    int kanava = (int)libChannels.SelectedValue;
                    if(sender is PasswordBox)// castataan lähettäjä
                    {
                        PasswordBox pass;
                        pass = (PasswordBox)sender;
                        password = pass.Password;
                    }                    
                    if(ChatWindowLogic.CheckChannelPassword(kanava, password)) // lähetetään viesti                    
                    {
                        GetMessages(kanava);// haetaan viestit uudelleen
                        txbMessage.Visibility = Visibility.Visible;
                        timer.Start(); // jatkaa automaattista viestinhakua
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password");
                    } 
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void libChannels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {                
                ShowChannelPrompt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void txbMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            txbMessage.Text = "";
        }

        private void txbMessage_LostFocus(object sender, RoutedEventArgs e)
        {
            txbMessage.Text = "Send Message";
        }

        
    }
}
