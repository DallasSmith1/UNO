using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace UNO
{
    internal class Player
    {
        #region variables
        // hold the local ip address of the player
        private string playerIP;
        // hold the players hand of UNO cards
        private List<UNOCard> hand = new List<UNOCard>();
        // trues if player is a bot
        private bool bot = false;
        // to hold the ip of the lobby they are in
        private string lobbyIP = "";
        #endregion

        #region constructors
        /// <summary>
        /// creates a new player and automatcially gets their local IP address
        /// </summary>
        /// <param name="IP"></param>
        public Player()
        {
            setPlayerIP();
        }

        /// <summary>
        /// creates a player with a manual IP address
        /// </summary>
        /// <param name="IP"></param>
        public Player(string IP)
        {
            setPlayerIP(IP);
        }

        /// <summary>
        /// if bot is true then create, if not create dead player
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="bot"></param>
        public Player(bool bot)
        {
            if (bot)
            {
                setPlayerIP("N/A");
                IsBot(bot);
            }
            else
            {
                new Player();
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// sets the players local IP address automatically
        /// </summary>
        /// <param name="IP"></param>
        private void setPlayerIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    this.playerIP = ip.ToString();
                }
            }
            throw new Exception("Your device is not setup to a Local Network!");
        }

        /// <summary>
        /// sets the players IP address manually
        /// </summary>
        /// <param name="IP"></param>
        private void setPlayerIP(string IP)
        {
            this.playerIP = IP;
        }

        /// <summary>
        /// gest the players local IP address
        /// </summary>
        /// <returns></returns>
        public string getPlayerIP()
        {
            return this.playerIP;
        }

        /// <summary>
        /// sets the lobby ip address
        /// </summary>
        /// <param name="IP"></param>
        public void setLobbyIP(string IP)
        {
            this.lobbyIP = IP;
        }

        /// <summary>
        /// gets the lobbies IP address
        /// </summary>
        /// <returns></returns>
        public string getLobbyIP()
        {
            return this.lobbyIP;
        }

        /// <summary>
        /// adds a card to the hand then sorts it again
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(UNOCard card)
        {
            this.hand.Add(card);
            UNOCard.SortCards(GetCards());
        }

        /// <summary>
        /// gets the hand of cards
        /// </summary>
        /// <returns></returns>
        public List<UNOCard> GetCards()
        {
            return this.hand;
        }

        /// <summary>
        /// returns the number of cards int he players hand
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfCards()
        {
            return GetCards().Count;
        }

        /// <summary>
        /// removes a specific card from the hand of the player
        /// </summary>
        /// <param name="cardToRemove"></param>
        public void RemoveCard(UNOCard cardToRemove)
        {
            foreach (UNOCard cardInHand in GetCards())
            {
                if (cardToRemove == cardInHand)
                {
                    this.hand.Remove(cardToRemove);
                }
            }
        }

        /// <summary>
        /// returns tre if the player is a bot
        /// </summary>
        /// <returns></returns>
        public bool IsBot()
        {
            return this.bot;
        }

        /// <summary>
        /// sets if the player is a bot or not
        /// </summary>
        /// <param name="bot"></param>
        private void IsBot(bool bot)
        {
            this.bot = bot;
        }
        #endregion

    }
}
