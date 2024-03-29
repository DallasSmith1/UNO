﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Documents.Serialization;

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
                new Player("N/A");
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
                    return;
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
            List<UNOCard> sortedHand = new List<UNOCard>(UNOCard.SortCards(this.GetCards()));
            this.hand.Clear();
            foreach (UNOCard sortedCard in sortedHand)
            {
                this.hand.Add(sortedCard);
            }
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
        /// returns the number of cards in the players hand
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
            foreach (UNOCard cardInHand in GetCards().ToList<UNOCard>())
            {
                if (cardToRemove == cardInHand)
                {
                    this.hand.Remove(cardToRemove);
                }
            }
        }

        /// <summary>
        /// returns true if the player is a bot
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

        /// <summary>
        /// bots algorithmatic function that returns the next card it wants to play
        /// </summary>
        /// <param name="nextPlayer"></param>
        /// <returns></returns>
        public UNOCard? MakeMove(Player nextPlayer, Lobby lobby)
        {
            if (nextPlayer.GetNumberOfCards() < this.GetNumberOfCards())
            {
                if (CheckForPickUpFour(this.GetCards()) != null)
                {
                    return CheckForPickUpFour(this.GetCards());
                }
                else if (CheckForPickUpTwo(this.GetCards(), lobby) != null)
                {
                    return CheckForPickUpTwo(this.GetCards(), lobby);
                }
                else if (CheckForSpecial(this.GetCards(), lobby) != null)
                {
                    return CheckForSpecial(this.GetCards(), lobby);
                }
                else if (CheckForPlayableCard(this.GetCards(), lobby) != null)
                {
                    return CheckForPlayableCard(this.GetCards(), lobby);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (CheckForPlayableCard(this.GetCards(), lobby) != null)
                {
                    return CheckForPlayableCard(this.GetCards(), lobby);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// checks a list of cards for a +4 card. returns the card if found, returns null if not
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private UNOCard? CheckForPickUpFour(List<UNOCard> hand)
        {
            foreach (UNOCard card in hand)
            {
                if (card.GetValue() == "+4")
                {
                    return card;
                }
            }
            return null;
        }

        /// <summary>
        /// checks a list of cards for a +4 card. returns the card if found, returns null if not
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private UNOCard? CheckForPickUpTwo(List<UNOCard> hand, Lobby lobby)
        {
            foreach (UNOCard card in hand)
            {
                if (card.GetValue() == "+2" && card.GetColour() == lobby.GetLiveCard().GetColour())
                {
                    return card;
                }
            }
            return null;
        }

        /// <summary>
        /// checks a list of cards for a special card. returns the card if found, returns null if not
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private UNOCard? CheckForSpecial(List<UNOCard> hand, Lobby lobby)
        {
            foreach (UNOCard card in hand)
            {
                if (card.IsSpecial() && card.GetColour() == lobby.GetLiveCard().GetColour() || card.GetColour() == "black")
                {
                    return card;
                }
            }
            return null;
        }

        /// <summary>
        /// checks for any playable card
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="lobby"></param>
        /// <returns></returns>
        private UNOCard? CheckForPlayableCard(List<UNOCard> hand, Lobby lobby)
        {
            foreach (UNOCard card in hand)
            {
                if (lobby.IsPlayableCard(card))
                {
                    return card;
                }
            }
            return null;
        }

        /// <summary>
        /// chooses a colour to change a swap or +4 card. Picks best colours based off of own cards
        /// </summary>
        /// <returns></returns>
        public string ChooseColour()
        {
            int red = 0;
            int green = 0;
            int yellow = 0;
            int blue = 0;

            // count each colour
            foreach (UNOCard card in GetCards())
            {
                if (card.GetColour() == "red")
                {
                    red++;
                }
                if (card.GetColour() == "green")
                {
                    green++;
                }
                if (card.GetColour() == "yellow")
                {
                    yellow++;
                }
                if (card.GetColour() == "blue")
                {
                    blue++;
                }
            }

            // sort colours from highest to lowest
            int[] colours = new int[] { red, green, yellow, blue };
            string[] coloursS = new string[] { "red", "green", "yellow", "blue" };
            bool changed = true;

            while (changed)
            {
                changed = false;
                for(int i = 0; i < colours.Length-1; i++)
                {
                    if (colours[i] < colours[i + 1])
                    {
                        int holder = colours[i];
                        colours[i] = colours[i + 1];
                        colours[i + 1] = holder;

                        string holderS = coloursS[i];
                        coloursS[i] = coloursS[i + 1];
                        coloursS[i + 1] = holderS;

                        changed = true;
                    }
                }
            }

            return coloursS[3];
        }
        #endregion

    }
}
