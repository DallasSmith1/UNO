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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UNO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cvsSinglePlayer.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Starts a single player game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            // create host player
            Player player = new Player("LocalHost");
            // create lobby, add host player as host
            Lobby lobby = new Lobby(player, false);

            // fill the rest of the players with bots
            for(int i = 0; i < 3; i++)
            {
                lobby.AddPlayer(new Player(true));
            }

            // create a new deck of cards
            foreach (UNOCard card in UNOCard.GenerateUnorderedDeck())
            {
                lobby.AddCardToPickupDeck(card);
            }

            // distribute cards to each player (to the specified start amount in lobby)
            for (int i = 0; i < Lobby.GetNumOfStartCards(); i++)
            {
                lobby.GetPlayers()[0].AddCard(lobby.GetNewCard());
                lobby.GetPlayers()[1].AddCard(lobby.GetNewCard());
                lobby.GetPlayers()[2].AddCard(lobby.GetNewCard());
                lobby.GetPlayers()[3].AddCard(lobby.GetNewCard());
            }

            // add the players cards to the list boxes for display
            List<Player> players = new List<Player>();
            foreach (Player playing in lobby.GetPlayers())
            {
                players.Add(playing);
            }


            for (int i = 0; i < players.Count; i++)
            {
                foreach (UNOCard card in players[i].GetCards())
                {
                    if (i == 0)
                    {
                        lbxPlayer1.Items.Add(card.GetImage());
                    }
                    else if (i == 1)
                    {
                        lbxPlayer2.Items.Add(card.GetImage());
                    }
                    if (i == 2)
                    {
                        lbxPlayer3.Items.Add(card.GetImage());
                    }
                    if (i == 3)
                    {
                        lbxPlayer4.Items.Add(card.GetImage());
                    }
                }

            }

            cvsMainMenu.Visibility = Visibility.Hidden;
            cvsSinglePlayer.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// closes the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// opens the multiplayer canvas and displays all the games
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMultiplayer_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// displays the settings canvas and shows all settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
