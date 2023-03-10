using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            CheckForIP();
            backgroundworker.DoWork += Backgroundworker_DoWork;
            backgroundworker.ProgressChanged += Backgroundworker_ProgressChanged;
            backgroundworker.RunWorkerCompleted += Backgroundworker_RunWorkerCompleted;
            backgroundworker.WorkerReportsProgress = true;
            backgroundworker.WorkerSupportsCancellation = true;
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }




        #region variables
        Lobby myLobby;
        Player me;
        BackgroundWorker backgroundworker = new BackgroundWorker();
        byte[] NotSelectedRBG = { 243, 143, 72 };
        byte[] SelectedRBG = { 243, 72, 72 };
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        #region methods
        /// <summary>
        /// checks for the ip and determines if miltiplayer is unlocked or not
        /// </summary>
        private void CheckForIP()
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
        private void RefreshHands()
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
                image.MouseDown += Image_MouseDown;
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

            // refresh top card too
            imgDiscardDeck.Source = myLobby.GetLiveCard().GetImage();

            // update the current players turn
            UpdateCurrentPlayerLabel();
        }

        /// <summary>
        /// refreshes the hand but with the hovered card
        /// </summary>
        /// <param name="card"></param>
        private void HoverCard(Image card)
        {
            lblIsPlayable.Visibility = Visibility.Visible;
            imgHoverCard.Visibility = Visibility.Visible;
            tbxCardDescription.Visibility = Visibility.Visible;
            imgHoverCard.Source = card.Source;

            foreach (UNOCard unoCard in myLobby.GetPlayers()[0].GetCards())
            {
                if (unoCard.GetImage() == card.Source)
                {
                    tbxCardDescription.Text = unoCard.GetDescription();

                    if (myLobby.IsPlayableCard(unoCard))
                    {
                        lblIsPlayable.Content = "Playable";
                        lblIsPlayable.Foreground = Brushes.Green;
                    }
                    else
                    {
                        lblIsPlayable.Content = "Not Playable";
                        lblIsPlayable.Foreground = Brushes.Red;
                    }
                }
            }
        }

        /// <summary>
        /// dehovers all cards
        /// </summary>
        private void DehoverCard()
        {
            lblIsPlayable.Visibility = Visibility.Hidden;
            imgHoverCard.Visibility = Visibility.Hidden;
            tbxCardDescription.Visibility = Visibility.Hidden;
            RefreshHands();
        }

        /// <summary>
        /// sets the top card (only for +4 and swap cards) to a specific colour. USed only in the colour buttons
        /// </summary>
        /// <param name="colour"></param>
        private void SetTopCardColour(string colour)
        {
            myLobby.SetCurrentColour(colour);
            UNOCard liveCard = myLobby.GetLiveCard();
            liveCard.SetColour(colour);
            liveCard.SetImage(liveCard.GetColour(), liveCard.GetValue());
            myLobby.SetLiveCard(liveCard);
            RefreshHands();
            cvsColours.Visibility = Visibility.Hidden;
            RunBotTurns();
        }

        /// <summary>
        /// runs the bots turns
        /// </summary>
        private void RunBotTurns()
        {
            // set Localhosts player label to notselected and set the  
            lblPlayer1.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(NotSelectedRBG[0], NotSelectedRBG[1], NotSelectedRBG[2]));
            if (myLobby.GetRotation())
            {
                lblPlayer2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SelectedRBG[0], SelectedRBG[1], SelectedRBG[2]));
            }
            else
            {
                lblPlayer4.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SelectedRBG[0], SelectedRBG[1], SelectedRBG[2]));
            }
            btnMainMenu.IsEnabled = false;
            backgroundworker.RunWorkerAsync();
        }

        /// <summary>
        /// updates the current players label
        /// </summary>
        private void UpdateCurrentPlayerLabel()
        {
            lblPlayer1.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(NotSelectedRBG[0], NotSelectedRBG[1], NotSelectedRBG[2]));
            lblPlayer2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(NotSelectedRBG[0], NotSelectedRBG[1], NotSelectedRBG[2]));
            lblPlayer3.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(NotSelectedRBG[0], NotSelectedRBG[1], NotSelectedRBG[2]));
            lblPlayer4.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(NotSelectedRBG[0], NotSelectedRBG[1], NotSelectedRBG[2]));
            if (myLobby.GetCurrentPlayer() == myLobby.GetPlayers()[0])
            {
                lblPlayer1.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SelectedRBG[0], SelectedRBG[1], SelectedRBG[2]));
            }
            else if (myLobby.GetCurrentPlayer() == myLobby.GetPlayers()[1])
            {
                lblPlayer2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SelectedRBG[0], SelectedRBG[1], SelectedRBG[2]));
            }
            else if (myLobby.GetCurrentPlayer() == myLobby.GetPlayers()[2])
            {
                lblPlayer3.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SelectedRBG[0], SelectedRBG[1], SelectedRBG[2]));
            }
            else if (myLobby.GetCurrentPlayer() == myLobby.GetPlayers()[3])
            {
                lblPlayer4.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SelectedRBG[0], SelectedRBG[1], SelectedRBG[2]));
            }
        }

        /// <summary>
        /// runs the bots algorithmatic turns in the background thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backgroundworker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            // if placed a card
            if(e.ProgressPercentage == 1)
            {
                // if +4 or swap card is played, randomly select a colour for it to change to
                if (myLobby.GetLiveCard().GetColour() == "black")
                {
                    Random rnd = new Random();

                    int num = rnd.Next(3);

                    if (num == 0)
                    {
                        myLobby.GetLiveCard().SetColour("red");
                        myLobby.GetLiveCard().SetImage(myLobby.GetLiveCard().GetColour(), myLobby.GetLiveCard().GetValue());
                    }
                    else if (num == 1)
                    {
                        myLobby.GetLiveCard().SetColour("green");
                        myLobby.GetLiveCard().SetImage(myLobby.GetLiveCard().GetColour(), myLobby.GetLiveCard().GetValue());
                    }
                    else if (num == 2)
                    {
                        myLobby.GetLiveCard().SetColour("blue");
                        myLobby.GetLiveCard().SetImage(myLobby.GetLiveCard().GetColour(), myLobby.GetLiveCard().GetValue());
                    }
                    else if (num == 3)
                    {
                        myLobby.GetLiveCard().SetColour("yellow");
                        myLobby.GetLiveCard().SetImage(myLobby.GetLiveCard().GetColour(), myLobby.GetLiveCard().GetValue());
                    }
                }

                // if any special cards were played, do the visuals for that card
                if (myLobby.GetLiveCard().GetValue() == "reverse")
                {
                    UpdateRotationImage();
                    ShowPopUp("reverse");
                }
                else if (myLobby.GetLiveCard().GetValue() == "skip")
                {
                    ShowPopUp("skip");
                }

            }
            
            RefreshHands();

        }

        /// <summary>
        /// after each bots make a move, update the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Backgroundworker_DoWork(object? sender, DoWorkEventArgs e)
        {
            myLobby.SetCurrentPlayer(myLobby.GetNextPlayer());

            // while its not the localhosts turn
            while (myLobby.GetCurrentPlayer() != myLobby.GetPlayers()[0])
            {
                Thread.Sleep(2500);
                // holds the current player
                Player currentPlayer = myLobby.GetCurrentPlayer();

                // while its the current players turn
                while (currentPlayer == myLobby.GetCurrentPlayer())
                {

                    // process of picking a card to play
                    UNOCard newCard = myLobby.GetCurrentPlayer().MakeMove(myLobby.GetNextPlayer(), myLobby);

                    // if no cards to play grab a new card
                    if (newCard == null)
                    {
                        newCard = myLobby.GetNewCard();
                        myLobby.GetCurrentPlayer().AddCard(newCard);

                        backgroundworker.ReportProgress(0);
                        Thread.Sleep(1000);
                    }
                    // else play card
                    else
                    {
                        myLobby.AddCardToDiscardDeck(newCard);
                        myLobby.GetCurrentPlayer().RemoveCard(newCard);

                        if(newCard.GetValue() == "reverse")
                        {
                            UpdateRotation();
                        }
                        else if (newCard.GetValue() == "skip")
                        {
                            PerformSkip();
                        }
                        myLobby.SetCurrentPlayer(myLobby.GetNextPlayer());

                        backgroundworker.ReportProgress(1);
                    }


                }
            }
        }

        /// <summary>
        /// when the background work has finished
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Backgroundworker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            RefreshHands();
            btnMainMenu.IsEnabled = true;
        }

        /// <summary>
        /// pops up the popup image with the specified image
        /// </summary>
        /// <param name="image"></param>
        private void ShowPopUp(string image)
        {
            imgPopup.Source = new BitmapImage(new Uri("/images/visuals/" + image + ".png", UriKind.Relative));
            imgPopup.Visibility = Visibility.Visible;
            dispatcherTimer.Start();
        }

        /// <summary>
        /// hides the popup
        /// </summary>
        private void HidePopUp()
        {
            imgPopup.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// updates the rotation image to the correct rotation based on the rotation in the lobby
        /// </summary>
        private void UpdateRotationImage()
        {
            if (myLobby.GetRotation())
            {
                imgRotation.Source = new BitmapImage(new Uri("/images/visuals/clockwise.png", UriKind.Relative));
            }
            else
            {
                imgRotation.Source = new BitmapImage(new Uri("/images/visuals/cclockwise.png", UriKind.Relative));
            }
        }

        /// <summary>
        /// updates the rotation of the game and everything included within that
        /// </summary>
        private void UpdateRotation()
        {
            if (myLobby.GetRotation())
            {
                myLobby.SetRotation(false);
            }
            else
            {
                myLobby.SetRotation(true);
            }
            myLobby.SetCurrentPlayer(myLobby.GetCurrentPlayer());
        }

        /// <summary>
        /// skips the next players turn and sets the following player as the next player
        /// </summary>
        private void PerformSkip()
        {
            // find the next player
            for (int i = 0; i < myLobby.GetPlayers().Count; i++)
            {
                // if found
                if (myLobby.GetPlayers()[i] == myLobby.GetNextPlayer())
                {
                    // if clockwise
                    if (myLobby.GetRotation())
                    {
                        // if end of list loop to beginning
                        if ((i + 1) == 4)
                        {
                            myLobby.SetNextPlayer(myLobby.GetPlayers()[0]);
                        }
                        else
                        {
                            myLobby.SetNextPlayer(myLobby.GetPlayers()[i+1]);
                        }
                    }
                    // if counter clockwise
                    else
                    {
                        // if beginning of list loop to end
                        if ((i - 1) == -1)
                        {
                            myLobby.SetNextPlayer(myLobby.GetPlayers()[3]);
                        }
                        else
                        {
                            myLobby.SetNextPlayer(myLobby.GetPlayers()[i - 1]);
                        }
                    }
                    break;
                }
            }
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
            RefreshHands();
            // swap canvases
            cvsMainMenu.Visibility = Visibility.Hidden;
            cvsSinglePlayer.Visibility = Visibility.Visible;

            // set locahost as current player
            myLobby.SetCurrentPlayer(myLobby.GetPlayers()[0]);
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
            if (cvsColours.Visibility == Visibility.Hidden && !backgroundworker.IsBusy)
            {
                myLobby.GetPlayers()[0].AddCard(myLobby.GetNewCard());
                RefreshHands();
            }
        }

        /// <summary>
        /// quits the current single player game and returns to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            backgroundworker.CancelAsync();
            cvsSinglePlayer.Visibility = Visibility.Hidden;
            cvsMainMenu.Visibility = Visibility.Visible;
            CheckForIP();
            myLobby = null;
        }

        /// <summary>
        /// checks if connected to router again, validating if multiplayer is enebaled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            CheckForIP();
        }

        /// <summary>
        /// when hovered over a card, display the card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            HoverCard((Image)sender);
        }

        /// <summary>
        /// when the mouse moves off of a card, hide the side panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            DehoverCard();
        }

        /// <summary>
        /// when a card is clicked place it into the discard pile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // only if its your turn to play pick a card
            if (cvsColours.Visibility == Visibility.Hidden && !backgroundworker.IsBusy)
            {
                Image card = (Image)sender;
                // get card that corresponds to the image player clicked
                foreach (UNOCard unoCard in myLobby.GetPlayers()[0].GetCards().ToList<UNOCard>())
                {
                    if (unoCard.GetImage() == card.Source)
                    {
                        if (myLobby.IsPlayableCard(unoCard))
                        {
                            myLobby.AddCardToDiscardDeck(unoCard);
                            myLobby.GetPlayers()[0].RemoveCard(unoCard);
                            RefreshHands();
                            //if a +4 or swap card, show the colours canvas to select a colour
                            if (unoCard.GetValue() == "+4" || unoCard.GetValue() == "swap")
                            {
                                cvsColours.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                if (myLobby.GetLiveCard().GetValue() == "reverse")
                                {
                                    UpdateRotation();
                                    UpdateRotationImage();
                                    ShowPopUp("reverse");
                                }
                                else if (myLobby.GetLiveCard().GetValue() == "skip")
                                {
                                    PerformSkip();
                                    ShowPopUp("skip");
                                }
                                RunBotTurns();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// when the red button is clicked, change the top card to red
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            SetTopCardColour("red");
        }

        /// <summary>
        /// sets the top colour to blue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            SetTopCardColour("blue");
        }

        /// <summary>
        /// sets the top colour to yellow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYellow_Click(object sender, RoutedEventArgs e)
        {
            SetTopCardColour("yellow");
        }

        /// <summary>
        /// sets the top colour to green
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            SetTopCardColour("green");
        }

        /// <summary>
        /// triggered when the timer is ticked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            HidePopUp();
        }

        #endregion

    }
}
