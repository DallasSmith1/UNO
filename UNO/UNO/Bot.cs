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
        /// bots algorithmatic function make next card
        /// </summary>
        /// <param name="nextPlayerHand"></param>
        /// <returns></returns>
        public UNOCard MakeMove(Player nextPlayer, Lobby, lobby)
        {

        }
        #endregion
    }
}
