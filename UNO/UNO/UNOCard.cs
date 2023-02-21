using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    internal class UNOCard
    {
        #region Variables
        // to hold a deck of cards
        static private List<UNOCard> deck = new List<UNOCard>();
        // to hold all the colours of cards
        static private string[] coloursList = {"red", "blue", "yellow", "green"};
        // to hold all the values of cards
        static private string[] valuesList = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "skip", "reverse", "+2", "+4", "swap"};
        // the number of decks that are held in 1 deck
        static private int numDecks = 2;
        // to hold the colour of the card
        private string colour = "";
        // to hold the value of the card
        private string value = "";
        // to hold if true if the card is a special card
        private bool special = false;
        // to hold the file path to the image of the card
        private string image = "";
        #endregion

        #region Constructors
        /// <summary>
        /// paramatarized constructor. colour is the colour of the card. value is the value of the card
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="value"></param>
        private UNOCard(string colour, string value)
        {
            SetColour(colour);
            SetValue(value);
            if (value == "skip" || value == "reverse" || value == "+2" || value == "+4" || value == "swap")
            {
                IsSpecial(true);
            }
            //
            // have to edit this after making the uno card images (inside of setImage function)
            SetImage(colour, value);
            //
            //
            AddCard(this);
        }
        #endregion

        #region Methods
        // sets and gets
        /// <summary>
        /// set the colour of the card
        /// </summary>
        /// <param name="colour"></param>
        private void SetColour(string colour)
        {
            this.colour = colour;
        }
        /// <summary>
        /// return the colour fo the card
        /// </summary>
        /// <returns></returns>
        public string GetColour()
        {
            return this.colour;
        }
        /// <summary>
        /// set the value of the card
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(string value)
        {
            this.value = value;
        }
        /// <summary>
        /// get the value fo the card
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            return this.value;
        }
        /// <summary>
        /// set true if the card if a speical type pf card
        /// </summary>
        /// <param name="is_special"></param>
        private void IsSpecial(bool is_special)
        {
            this.special = is_special;
        }
        /// <summary>
        /// returns true if the card is a special card
        /// </summary>
        /// <returns></returns>
        public bool IsSpecial()
        {
            return this.special;
        }
        /// <summary>
        /// sets the image file path
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="value"></param>
        private void SetImage(string colour, string value)
        {
            this.image = "./" + colour + value + ".jpg";
        }
        /// <summary>
        /// returns the image path of the card
        /// </summary>
        /// <returns></returns>
        public string GetImage()
        {
            return this.image;
        }

        // static private methods
        /// <summary>
        /// returns the deck of cards
        /// </summary>
        /// <returns></returns>
        static private List<UNOCard> GetDeck()
        {
            return UNOCard.deck;
        }
        /// <summary>
        /// adds a card to the deck of uno cards
        /// </summary>
        /// <param name="card"></param>
        static private void AddCard(UNOCard card)
        {
            UNOCard.deck.Add(card);
        }
        /// <summary>
        /// empties the static deck
        /// </summary>
        static private void ClearDeck()
        {
            UNOCard.deck.Clear();
        }

        // static public methods
        /// <summary>
        /// shuffles a given list of cards
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        static public List<UNOCard> ShuffleCards(List<UNOCard> deck)
        {
            List<UNOCard> shuffledList = new List<UNOCard>();
            Random random = new Random();
            int total = deck.Count;

            for (int i = 0; i < total; i++)
            {
                UNOCard card = deck[random.Next(0, deck.Count)];
                shuffledList.Add(card);
                deck.Remove(card);
            }

            return shuffledList;
        }
        /// <summary>
        /// generate a brand new UNO deck 
        /// </summary>
        /// <returns></returns>
        static public List<UNOCard> GenerateOrderedDeck()
        {
            ClearDeck();
            int iter = 0;
            while (iter < numDecks)
            {
                if(iter == 0)
                {
                    foreach(string colour in UNOCard.coloursList)
                    {
                        foreach(string value in UNOCard.valuesList)
                        {
                            if (value == "+4" || value == "swap")
                            {
                                new UNOCard("N/A", value);
                            }
                            else
                            {
                                new UNOCard(colour, value);
                            }
                        }
                    }
                }
                else
                {
                    foreach (string colour in UNOCard.coloursList)
                    {
                        foreach (string value in UNOCard.valuesList)
                        {
                            if (value != "0" || value != "+4" || value != "swap")
                            {
                                new UNOCard(colour, value);
                            }
                        }
                    }
                }
                iter++;
            }
            return GetDeck();
        }
        /// <summary>
        /// generate new shuffled UNO deck
        /// </summary>
        /// <returns></returns>
        static public List<UNOCard> GenerateUnorderedDeck()
        {
            List<UNOCard> newDeck = UNOCard.GenerateOrderedDeck();
            newDeck = UNOCard.ShuffleCards(newDeck);
            return newDeck;
        }
        #endregion
    }
}
