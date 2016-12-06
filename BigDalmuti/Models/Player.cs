using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigDalmuti.Models
{
    public class Player
    {
        public int PlayerNumber { get; set; }
        public string DisplayName { get; set; }
      
        public List<Card> Hand { get; set; }

        public Player()
        {
            this.Hand = new List<Card>();
        }

        public List<Card> GetHand(int playerNumber)
        {
            return this.Hand;
        }
    }
}