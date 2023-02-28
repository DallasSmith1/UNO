using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    internal class Bot : Player
    {
        #region constructor
        /// <summary>
        /// constructor to create a bot
        /// </summary>
        public Bot() : base(true) { }
        #endregion

        #region methods
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
                else if (CheckForPickUpTwo(this.GetCards()) != null)
                {
                    return CheckForPickUpTwo(this.GetCards());
                }
                else if (CheckForSpecial(this.GetCards()) != null)
                {
                    return CheckForSpecial(this.GetCards());
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
        private UNOCard? CheckForPickUpTwo(List<UNOCard> hand)
        {
            foreach (UNOCard card in hand)
            {
                if (card.GetValue() == "+2")
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
        private UNOCard? CheckForSpecial(List<UNOCard> hand)
        {
            foreach (UNOCard card in hand)
            {
                if (card.IsSpecial())
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
                if (card.GetColour() == lobby.GetCurrentColour())
                {
                    return card;
                }
                else if (card.GetValue() == lobby.GetCurrentValue())
                {
                    return card;
                }
                else if (card.GetValue() == "+4" || card.GetValue() == "swap")
                {
                    return card;
                }
            }
            return null;
        }
        #endregion
    }
}
