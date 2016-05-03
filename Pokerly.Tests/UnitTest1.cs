using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokerly.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokerly.Classes.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void TestCardCount()
        {
            CardDeck cardDeck = new CardDeck();

            cardDeck.FillDeck();

            var player1 = new Player(Guid.NewGuid().ToString(), "Sam");
            var player2 = new Player(Guid.NewGuid().ToString(), "Joe");

            Round round = new Round(cardDeck);

            round.Players.Add(player1);
            round.Players.Add(player2);
            round.DealCards();

            Assert.IsTrue(player1.Hand.Cards.Count == 5 && player2.Hand.Cards.Count == 5);

        }

        [TestMethod()]
        public void TestPair()
        {
            Hand h = new Hand();

            List<Card> cards = new List<Card>();
            cards.Add(new Card(Enums.SuitType.Club, Enums.FaceValueType.Ace));
            cards.Add(new Card(Enums.SuitType.Heart, Enums.FaceValueType.Ace));
            cards.Add(new Card(Enums.SuitType.Heart, Enums.FaceValueType.Ten));
            cards.Add(new Card(Enums.SuitType.Spade, Enums.FaceValueType.Three));
            cards.Add(new Card(Enums.SuitType.Club, Enums.FaceValueType.Seven));
            h.Cards = cards;
            h.EvaluateHand();
            bool isPair = (h.HandType == Enums.HandType.Pair);
            Assert.IsTrue(isPair);
        }

        [TestMethod()]
        public void TestPair2()
        {
            CardDeck cardDeck = new CardDeck();

            cardDeck.FillDeck();

            var player1 = new Player(Guid.NewGuid().ToString(), "Sam");
            player1.HandTypeOverride = Enums.HandType.Flush;
            var player2 = new Player(Guid.NewGuid().ToString(), "Joe");
            player2.HandTypeOverride = Enums.HandType.Pair;

            Round round = new Round(cardDeck);

            round.Players.Add(player1);
            round.Players.Add(player2);
            round.DealCards();
            player2.Hand.EvaluateHand();
            Assert.IsTrue(player2.Hand.HandType == Enums.HandType.Pair);
        }

        [TestMethod()]
        public void TestWinner1()
        {
            CardDeck cardDeck = new CardDeck();

            cardDeck.FillDeck();

            var player1 = new Player(Guid.NewGuid().ToString(), "Sam");
            player1.HandTypeOverride = Enums.HandType.Flush;
            var player2 = new Player(Guid.NewGuid().ToString(), "Joe");
            player2.HandTypeOverride = Enums.HandType.Pair;

            Round round = new Round(cardDeck);

            round.Players.Add(player1);
            round.Players.Add(player2);
            round.DealCards();
            foreach (var player in round.Players)
            {
                player.Hand.EvaluateHand();
            }
            List<Player> winners = round.DetermineWinner();
            Assert.IsTrue((winners.Count() == 1) && (winners[0].Id == player1.Id));
        }
        
    }
}