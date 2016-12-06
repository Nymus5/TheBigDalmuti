using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BigDalmuti.Models;
using System.Web.Mvc;
using System.Threading;
using BigDalmuti.Extensions;

namespace BigDalmuti.Models
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Card> Cards { get; set; }
        public List<Card> CardsPlayed { get; set; }
        public List<int> TempPlayedCardlist { get; set; }
        public bool CanPlay { get; set; }
        public bool IsStarted { get; set; }

        public int ThisPlayerTurn { get; set; }
        public int MaxPlayers { get; set; }

        public Game()
        {
            Cards = new List<Card>();
            this.Players = new List<Player>();
            this.MaxPlayers = 2;

            Cards.Add(new Card() { Value = 1, Name = "The Big Dalmuti" });
            Cards.Add(new Card() { Value = 2, Name = "Archbishop" });
            Cards.Add(new Card() { Value = 2, Name = "Archbishop" });
            Cards.Add(new Card() { Value = 3, Name = "Lord Chamberlain" });
            Cards.Add(new Card() { Value = 3, Name = "Lord Chamberlain" });
            Cards.Add(new Card() { Value = 3, Name = "Lord Chamberlain" });
            Cards.Add(new Card() { Value = 4, Name = "Baroness" });
            Cards.Add(new Card() { Value = 4, Name = "Baroness" });
            Cards.Add(new Card() { Value = 4, Name = "Baroness" });
            Cards.Add(new Card() { Value = 4, Name = "Baroness" });
            Cards.Add(new Card() { Value = 5, Name = "Mother Superior" });
            Cards.Add(new Card() { Value = 5, Name = "Mother Superior" });
            Cards.Add(new Card() { Value = 5, Name = "Mother Superior" });
            Cards.Add(new Card() { Value = 5, Name = "Mother Superior" });
            Cards.Add(new Card() { Value = 5, Name = "Mother Superior" });

            Cards.Shuffle();
        }

        public int AddPlayerToGame(string displayName)
        {
            if (this.Players.Count < this.MaxPlayers)
            {
                var newPlayerNumber = this.Players.Count + 1;
                this.Players.Add(new Player() { DisplayName = displayName, PlayerNumber = newPlayerNumber });
                return newPlayerNumber;
            }
            else
            {
                throw new ApplicationException("Max players in game..");
            }
        }

        public void Deal()
        {
            while (Cards.Count > 0)
            {
                foreach (var player in Players)
                {
                    player.Hand.Add(Cards[Cards.Count - 1]);
                    Cards.RemoveAt(Cards.Count - 1);
                    if (Cards.Count == 0)
                        break;
                }
            }
        }

        public bool Start()
        {
            if (this.Players.Count == this.MaxPlayers)
            {
                Deal();
                IsStarted = true;
                if (ThisPlayerTurn == 0)
                {
                    ThisPlayerTurn = 1;
                }
                return true;
            }
            return false;
        }

        public void Stop()
        {

        }

        public void PlayCard(int playerNumber, List<int> cardList)
        {
            foreach (var player in this.Players)
            {
                if (player.PlayerNumber == this.ThisPlayerTurn)
                {
                    List<Card> hand = player.GetHand(this.ThisPlayerTurn);
                    int cardPlayed = cardList.First();

                    if (TempPlayedCardlist == null)
                    {
                        for (int i = hand.Count - 1; i >= 0; i--)
                        {
                            hand.RemoveAll(j => j.Value == cardPlayed);
                            TempPlayedCardlist = cardList;
                        }
                    }
                    else
                    {
                        CanIPlay(this.TempPlayedCardlist, cardList);

                        if (CanPlay == true)
                        {
                            for (int i = hand.Count - 1; i >= 0; i--)
                            {
                                hand.RemoveAll(j => j.Value == cardPlayed);
                                List<int> TempPlayedCardlist = cardList;
                            }
                        }
                        else
                        {
                            throw new ApplicationException("You didn't play the right amount of cards or lower cards...");
                        }
                    }

                    foreach (var card in cardList)
                    {
                        if (CardsPlayed == null)
                        {
                            CardsPlayed = new List<Card>();
                            CardsPlayed.Add(new Card() { Value = cardPlayed });
                        }
                        else
                        {
                            this.CardsPlayed.Add(new Card() { Value = cardPlayed });
                        }
                    }
                }
            }
        }

        public void NextTurn(int playerNumber)
        {
            if (playerNumber < MaxPlayers)
            {
                ThisPlayerTurn++;
            }
            else
            {
                ThisPlayerTurn = 1;
            }
        }

        private void CanIPlay(List<int> TempPlayedCardlist, List<int> cardList)
        {
            if (cardList.Any(o => o != cardList[0]))
            {
                this.CanPlay = false;              
            }
            else
            {
                if (TempPlayedCardlist.Count == cardList.Count)
                {
                    int previousCard = TempPlayedCardlist.First();
                    int newCard = cardList.First();

                    if (newCard < previousCard)
                    {
                        this.CanPlay = true;
                    }
                    else
                    {
                        this.CanPlay = false;
                    }
                }
                else
                {
                    this.CanPlay = false;
                }
            }
        }

    }
}


