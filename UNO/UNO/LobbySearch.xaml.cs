using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace UNO
{
    /// <summary>
    /// Interaction logic for LobbySearch.xaml
    /// </summary>
    public partial class LobbySearch : Window
    {
        public LobbySearch(string ip)
        {
            me = new Player(ip);
            InitializeComponent();
        }

        Player me;
        Lobby myLobby;

        /// <summary>
        /// creates a new host
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHost_Click(object sender, RoutedEventArgs e)
        {
            myLobby = new Lobby(me, true, 5732);
            TcpListener server = new TcpListener(System.Net.IPAddress.Any, 5732);
            server.Start();
            server.AcceptSocket();
        }

        /// <summary>
        /// when the window is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opened(object sender, EventArgs e)
        {
            lblIP.Content = "Your IP: " + me.getPlayerIP();
        }

        /// <summary>
        /// closes the lobby window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LookForLobbies()
        {

        }
    }
}
