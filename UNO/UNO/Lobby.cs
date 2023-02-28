using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    internal class Lobby
    {
        #region Variables
        // to store all the created lobbies
        static private List<Lobby> lobbies = new List<Lobby> ();
        // maximum number of players
        static private int maxPlayers = 4;
        // to store the hosts IP address
        private string hostIP;
        // to store true if the gamemode is multiplayer
        private bool multiplayer;
        // to store all the players in the lobby
        private List<Player> players = new List<Player> ();
        // to hold the pickup deck
        private List<UNOCard> pickupDeck = new List<UNOCard> ();
        // to hold the discard deck
        private List<UNOCard> discardDeck = new List<UNOCard>();
        // stores the current colour
        private string currentColour;
        // stores the current value
        private string currentValue;
        // stores the direction fo the turns, true for clockwise
        private bool clockwise;
        // to store the next player in line
        private Player nextPlayer;
        // to store the current player whos taking their turn
        private Player currentPlayer;
        #endregion

        #region Constructors
        public Lobby(Player host, bool isMultiplayer)
        {
            SetHostIP(host.getPlayerIP());
            SetGameMode(isMultiplayer);
            AddLobby(this);
        }
        #endregion

        #region methods
        /// <summary>
        /// adds a lobby to the list of public lobbies
        /// </summary>
        /// <param name="lobby"></param>
        static public void AddLobby(Lobby lobby)
        {
            Lobby.lobbies.Add(lobby);
        }

        /// <summary>
        /// removes a specific lobby form the list of lobbies
        /// </summary>
        /// <param name="lobby"></param>
        static public void RemoveLobby(Lobby lobby)
        {
            Lobby.lobbies.Remove(lobby);
        }

        /// <summary>
        /// gets the list of all lobbies
        /// </summary>
        /// <returns></returns>
        static public List<Lobby> GetLobbies()
        {
            return Lobby.lobbies;
        }

        /// <summary>
        /// sestt he hostsIP address of the lobby
        /// </summary>
        /// <param name="IP"></param>
        private void SetHostIP(string IP)
        {
            this.hostIP = IP;
        }

        /// <summary>
        /// returns the hosts IP address
        /// </summary>
        /// <returns></returns>
        public string GetHostIP()
        {
            return this.hostIP;
        }

        /// <summary>
        /// sets the multiplayer boolean value. True if game mode is multiplayer
        /// </summary>
        /// <param name="isMultiplayer"></param>
        private void SetGameMode(bool isMultiplayer)
        {
            this.multiplayer = isMultiplayer;
        }

        /// <summary>
        /// returns true if the game mode is set to multiplayer
        /// </summary>
        /// <returns></returns>
        public bool IsMultiplayer()
        {
            return this.multiplayer;
        }

        /// <summary>
        /// adds a player to the list of players
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        /// <summary>
        /// removes a specific player from the list of players
        /// </summary>
        /// <param name="player"></param>
        public void RemovePlayer(Player player)
        {
            this.players.Remove(player);
        }

        /// <summary>
        /// returns a list of all the players
        /// </summary>
        /// <returns></returns>
        public List<Player> GetPlayers()
        {
            return this.players;
        }

        /// <summary>
        /// adds a card to the pickup deck
        /// </summary>
        /// <param name="card"></param>
        private void AddCardToPickupDeck(UNOCard card)
        {
            this.pickupDeck.Add(card);
        }

        /// <summary>
        /// gets the next top card from the pickup deck
        /// </summary>
        /// <returns></returns>
        public UNOCard GetNewCard()
        {
            UNOCard newCard = this.pickupDeck[0];
            this.pickupDeck.RemoveAt(0);
            if (this.pickupDeck.Count == 0)
            {
                ResetPickupDeck();
            }
            return newCard;
        }

        /// <summary>
        /// takes all discarded cards, except top card, shuffles them, then moves them to pickup deck
        /// </summary>
        public void ResetPickupDeck()
        {
            UNOCard topCard = this.discardDeck[this.discardDeck.Count - 1];
            this.discardDeck.RemoveAt(this.discardDeck.Count - 1);
            foreach (UNOCard card in UNOCard.ShuffleCards(this.discardDeck))
            {
                AddCardToPickupDeck(card);
            }
            this.discardDeck.Clear();
            this.discardDeck.Add(topCard);
        }

        /// <summary>
        /// sets the current colour of the game
        /// </summary>
        /// <param name="colour"></param>
        public void SetCurrentColour(string colour)
        {
            this.currentColour = colour;
        }

        /// <summary>
        /// gets the current colour of the game
        /// </summary>
        /// <returns></returns>
        public string GetCurrentColour()
        {
            return this.currentColour;
        }

        /// <summary>
        /// sets the current value of the game
        /// </summary>
        /// <param name="value"></param>
        public void SetCurrentValue(string value)
        {
            this.currentValue = value;
        }

        /// <summary>
        /// gets the current value of the game
        /// </summary>
        /// <returns></returns>
        public string GetCurrentValue()
        {
            return this.currentValue;
        }

        /// <summary>
        /// sets the rotation fot he game, true for clockwise (itering up)
        /// </summary>
        /// <param name="isClockwise"></param>
        public void SetRotation(bool isClockwise)
        {
            this.clockwise = isClockwise;
        }

        /// <summary>
        /// returns the rotation of the game, true for clockwise (itering up)
        /// </summary>
        /// <returns></returns>
        public bool GetRotation()
        {
            return this.clockwise;
        }

        /// <summary>
        /// sets the current players turn
        /// </summary>
        /// <param name="player"></param>
        public void SetCurrentPlayer(Player player)
        {
            this.currentPlayer = player;
            if (GetRotation())
            {
                for (int i = 0; i < Lobby.maxPlayers; i++)
                {
                    if (i == Lobby.maxPlayers - 1)
                    {
                        SetNextPlayer(GetPlayers()[0]);
                    }
                    else if (player == GetPlayers()[i])
                    {
                        SetNextPlayer(GetPlayers()[i + 1]);
                    }
                }    
            }
            else
            {
                for (int i = Lobby.maxPlayers-1; i > -1; i--)
                {
                    if (i == 0)
                    {
                        SetNextPlayer(GetPlayers()[Lobby.maxPlayers-1]);
                    }
                    else if (player == GetPlayers()[i])
                    {
                        SetNextPlayer(GetPlayers()[i - 1]);
                    }
                }
            }
        }

        /// <summary>
        /// gets the current player
        /// </summary>
        /// <returns></returns>
        public Player GetCurrentPlayer()
        {
            return this.currentPlayer;
        }

        /// <summary>
        /// sets the next players turn
        /// </summary>
        /// <param name="player"></param>
        private void SetNextPlayer(Player player)
        {
            this.nextPlayer = player;
        }

        /// <summary>
        /// returns the next player in line
        /// </summary>
        /// <returns></returns>
        public Player GetNextPlayer()
        {
            return this.nextPlayer;
        }
        #endregion

    }
}
