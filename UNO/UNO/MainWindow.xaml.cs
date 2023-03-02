using System;
using System.Collections.Generic;
using System.Drawing;
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
            checkForIP();
        }

        #region variables
        Lobby myLobby;
        Player me;
        #endregion

        #region methods
        /// <summary>
        /// checks for the ip and determines if miltiplayer is unlocked or not
        /// </summary>
        private void checkForIP()
        {
            try
            {
                me = new Player();
            }
            catch (Exception e)
            {
                me = new Player("LocalHost");
            }

            if (me.getPlayerIP() == "LocalHost")
            {
                btnMultiplayer.IsEnabled = false;
            }
        }

        /// <summary>
        /// refreshes the UI
        /// </summary>
        private void refreshHands()
        {
            // refresh top discard deck card
            imgDiscardDeck.Source = myLobby.GetLiveCard().GetImage();

            // create a visual list of the cards
            List<Image> cards = new List<Image>();
            foreach (UNOCard card in me.GetCards())
            {
                Image image = new Image();
                image.Source = card.GetImage();
                image.Width = 150;
                image.Height = 240;
                // add event handlers to card
                image.MouseEnter += Image_MouseEnter;
                image.MouseLeave += Image_MouseLeave;
                cards.Add(image);
            }


            // add the cards to the hand dynamically
            cvsMyHand.Children.Clear();

            Grid newGrid = new Grid();
            newGrid.Width = 1320;
            newGrid.Height = 296;

            for (int i = 0; i < cards.Count; i++)
            {
                ColumnDefinition newCol = new ColumnDefinition();
                newGrid.ColumnDefinitions.Add(newCol);
            }

            for (int i = 0; i < cards.Count; i++)
            {
                Grid.SetColumn(cards[i],i);
                newGrid.Children.Add(cards[i]);
            }

            cvsMyHand.Children.Add(newGrid);



            // adds other player cards hand dynamically
            List<Canvas> canvases = new List<Canvas>();
            canvases.Add(cvsPlayer2);
            canvases.Add(cvsPlayer3);
            canvases.Add(cvsPlayer4);

            for(int i = 0; i < canvases.Count; i++)
            {
                // create a list of all card images
                List<Image> images = new List<Image>();
                BitmapImage bitmap = new BitmapImage(new Uri("/images/UNOCards/back.png", UriKind.Relative));
                for (int j = 0; j < myLobby.GetPlayers()[i+1].GetCards().Count; j++)
                {
                    Image image = new Image();
                    image.Source = bitmap;
                    image.Width = 125;
                    image.Height = 210;
                    images.Add(image);
                }

                // add the cards to the hands dynamically
                canvases[i].Children.Clear();

                Grid newGrid2 = new Grid();
                newGrid2.Width = 485;
                newGrid2.Height = 298;

                for (int j = 0; j < myLobby.GetPlayers()[i+1].GetCards().Count; j++)
                {
                    ColumnDefinition newCol = new ColumnDefinition();
                    newGrid2.ColumnDefinitions.Add(newCol);
                }

                for (int j = 0; j < myLobby.GetPlayers()[i+1].GetCards().Count; j++)
                {
                    Grid.SetColumn(images[j], j);
                    newGrid2.Children.Add(images[j]);
                }

                canvases[i].Children.Add(newGrid2);
            }


            // update the number of cards
            lblPlayer1Number.Content = myLobby.GetPlayers()[0].GetNumberOfCards();
            lblPlayer2Number.Content = myLobby.GetPlayers()[1].GetNumberOfCards();
            lblPlayer3Number.Content = myLobby.GetPlayers()[2].GetNumberOfCards();
            lblPlayer4Number.Content = myLobby.GetPlayers()[3].GetNumberOfCards();
        }


        /// <summary>
        /// refreshes the hand but with the hovered card
        /// </summary>
        /// <param name="card"></param>
        private void hoverCard(Image card)
        {
            List<Image> cards = new List<Image>();
            int focus = 0;
            for(int i = 0; i < myLobby.GetPlayers()[0].GetCards().Count; i++)
            {
                Image image = new Image();
                image.Source = myLobby.GetPlayers()[0].GetCards()[i].GetImage();
                image.Width = 150;
                image.Height = 240;
                cards.Add(image);
                // if card is the same as the one hovered over (sender)
                if (image == card)
                {
                    focus = i;
                }
            }


            // add the cards to the hand dynamically
            cvsMyHand.Children.Clear();

            Grid newGrid = new Grid();
            newGrid.Width = 1320;
            newGrid.Height = 296;

            for (int i = 0; i < cards.Count; i++)
            {
                ColumnDefinition newCol = new ColumnDefinition();
                if (i == focus)
                {
                    // create new length for that specific column
                    GridLength length = new GridLength(150);
                    newCol.Width = length;
                }
                newGrid.ColumnDefinitions.Add(newCol);
            }

            for (int i = 0; i < cards.Count; i++)
            {
                Grid.SetColumn(cards[i], i);
                newGrid.Children.Add(cards[i]);
            }

            cvsMyHand.Children.Add(newGrid);
            
        }

        /// <summary>
        /// dehovers all cards
        /// </summary>
        private void dehoverCard()
        {
            refreshHands();
        }
        #endregion

        #region Event Listeners
        /// <summary>
        /// Starts a single player game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            // create lobby, add host player as host
            myLobby = new Lobby(me, false);

            // fill the rest of the players with bots
            for(int i = 0; i < 3; i++)
            {
                myLobby.AddPlayer(new Player(true));
            }

            // create a new deck of cards
            foreach (UNOCard card in UNOCard.GenerateUnorderedDeck())
            {
                myLobby.AddCardToPickupDeck(card);
            }

            // distribute cards to each player (to the specified start amount in lobby)
            for (int i = 0; i < Lobby.GetNumOfStartCards(); i++)
            {
                myLobby.GetPlayers()[0].AddCard(myLobby.GetNewCard());
                myLobby.GetPlayers()[1].AddCard(myLobby.GetNewCard());
                myLobby.GetPlayers()[2].AddCard(myLobby.GetNewCard());
                myLobby.GetPlayers()[3].AddCard(myLobby.GetNewCard());
            }

            // add the players cards to the list boxes for display
            List<Player> players = new List<Player>();
            foreach (Player playing in myLobby.GetPlayers())
            {
                players.Add(playing);
            }

            // move next card to the discard pile to start the game
            myLobby.AddCardToDiscardDeck(myLobby.GetNewCard());
            refreshHands();
            // swap canvases
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

        /// <summary>
        /// gets a new card and gives it to the players hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgPickupDeck_Click(object sender, MouseButtonEventArgs e)
        {
            myLobby.GetPlayers()[0].AddCard(myLobby.GetNewCard());
            refreshHands();
        }

        /// <summary>
        /// quits the current single player game and returns to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            cvsSinglePlayer.Visibility = Visibility.Hidden;
            cvsMainMenu.Visibility = Visibility.Visible;
            checkForIP();
            myLobby = null;
        }

        /// <summary>
        /// checks if connected to router again, validating if multiplayer is enebaled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            checkForIP();
        }

        /// <summary>
        /// when hovered over a card, display the card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image card = (Image)sender;
            hoverCard(card);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            dehoverCard();
        }
        #endregion

    }
}
