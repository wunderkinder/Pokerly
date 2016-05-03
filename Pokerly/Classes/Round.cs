using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokerly.Classes
{
    /// <summary>
    /// Represents a round of cards: n players and a deck of cards.
    /// </summary>
    public class Round
    {
        private List<Player> players;
        private CardDeck cardDeck;
        private bool isTie = false;

        public List<Player> Players
        {
            get
            {
                return players;
            }

            set
            {
                players = value;
            }
        }

        public CardDeck CardDeck
        {
            get
            {
                return cardDeck;
            }

            set
            {
                cardDeck = value;
            }
        }

        public bool IsTie
        {
            get
            {
                return isTie;
            }

            set
            {
                isTie = value;
            }
        }

        public Round(CardDeck cardDeck)
        {
            this.CardDeck = cardDeck;
            Players = new List<Player>();
        }
        
        public void DealCards(int cardsPerHand = 5)
        {
            if (CardDeck.CardsPerDeck < (cardsPerHand * Players.Count()))
            {
                //not enough cards for this many players
            }
            List<Player> playersToOverrideDeal = Players.FindAll(p => p.HandTypeOverride != Enums.HandType.DealFromDeck).ToList();
            if (playersToOverrideDeal.Count > 0)
            {
                CardDeck.Cards.OrderByDescending(c => c.SortOrder);
            }
            for (int j = 0; j < playersToOverrideDeal.Count; j++)
            {
                Player p = playersToOverrideDeal[j];
                Random r = new Random();
                var cardsAreAvailable = false;
                Enums.FaceValueType f;
                List<Card> foundCards = null;
                switch (p.HandTypeOverride)
                {
                    case Enums.HandType.StraightFlush:
                        cardsAreAvailable = false;
                        while (cardsAreAvailable == false)
                        {
                            foreach (Enums.SuitType suit in Enum.GetValues(typeof(Enums.SuitType)))
                            {
                                foundCards = new List<Card>();
                                var ii = r.Next(0, 7);
                                for (int i = ii; i < (12 - ii); i++)
                                {
                                    f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(i);
                                    var cardToFind = CardDeck.Cards.Find(c => c.FaceValue == f && c.Suit == suit);
                                    if (cardToFind != null)
                                    {
                                        foundCards.Add(cardToFind);
                                        CardDeck.Cards.Remove(cardToFind);
                                        cardsAreAvailable = (foundCards.Count == 5);
                                    }
                                    else
                                    {
                                        //the card to find is not in the deck. We have a gap so we need to clear the found cards to start over.
                                        foundCards.Clear();
                                    }
                                    if (cardsAreAvailable)
                                    {
                                        break;
                                    }
                                }
                                if (cardsAreAvailable)
                                {
                                    break;
                                }
                            }
                        }
                        //TODO: handle no cards available. 
                        //We could get this far without any available straight flushes, but not with only two players. Should handle for cases with more than two players.
                        if (foundCards.Count == 5)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                var c = foundCards[i];
                                CardDeck.Cards.Remove(c);
                                p.Hand.Cards.Add(c);
                            }
                        }

                        break;
                    case Enums.HandType.FourOfAKind:
                        cardsAreAvailable = false;
                        while (cardsAreAvailable == false)
                        {
                            int rNext = r.Next(0, 12);
                            f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(rNext);
                            foundCards = CardDeck.Cards.FindAll(c => c.FaceValue == f).ToList();
                            cardsAreAvailable = foundCards.Count >= 4;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            var c = foundCards[i];
                            CardDeck.Cards.Remove(c);
                            p.Hand.Cards.Add(c);
                        }
                        break;
                    case Enums.HandType.FullHouse:
                        cardsAreAvailable = false;
                        while (cardsAreAvailable == false)
                        {
                            int rNext = r.Next(0, 12);
                            f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(rNext);
                            foundCards = CardDeck.Cards.FindAll(c => c.FaceValue == f).ToList();
                            cardsAreAvailable = foundCards.Count >= 3;
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            var c = foundCards[i];
                            CardDeck.Cards.Remove(c);
                            p.Hand.Cards.Add(c);
                        }
                        //now the pair
                        cardsAreAvailable = false;
                        while (cardsAreAvailable == false)
                        {
                            int rNext = r.Next(0, 12);
                            f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(rNext);
                            foundCards = CardDeck.Cards.FindAll(c => c.FaceValue == f).ToList();
                            cardsAreAvailable = foundCards.Count >= 2;
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            var c = foundCards[i];
                            CardDeck.Cards.Remove(c);
                            p.Hand.Cards.Add(c);
                        }
                        break;
                    case Enums.HandType.Flush:
                        cardsAreAvailable = false;
                        while (cardsAreAvailable == false)
                        {
                            foreach (Enums.SuitType suit in Enum.GetValues(typeof(Enums.SuitType)))
                            {
                                foundCards = new List<Card>();
                                int ii = r.Next(0, 3);
                                for (int i = ii; i < 12; i+=2)
                                {
                                    f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(i);
                                    var cardToFind = CardDeck.Cards.Find(c => c.FaceValue == f && c.Suit == suit);
                                    if (cardToFind != null)
                                    {
                                        foundCards.Add(cardToFind);
                                        CardDeck.Cards.Remove(cardToFind);
                                        cardsAreAvailable = (foundCards.Count == 5);
                                    }
                                    if (cardsAreAvailable)
                                    {
                                        break;
                                    }
                                }
                                if (cardsAreAvailable)
                                {
                                    break;
                                }
                            }
                        }
                        //TODO: handle no cards available. 
                        //We could get this far without any available flushes, but not with only two players. Should handle for cases with more than two players.
                        if (foundCards.Count == 5)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                var c = foundCards[i];
                                CardDeck.Cards.Remove(c);
                                p.Hand.Cards.Add(c);
                            }
                        }
                        break;
                    case Enums.HandType.Straight:
                        cardsAreAvailable = false;
                        //List<Card> tempCards;// = CardDeck.Cards.OrderBy(c => c.SortOrder).ToList();
                        while (cardsAreAvailable == false)
                        {
                            foundCards = new List<Card>();
                            Enums.SuitType lastSuit = Enums.SuitType.Spade;
                            //count 0 to 12 and look for a card using a suit that doesn't match the last card.
                            //TODO:this never goes above a high card of 6. Fix.
                            foreach (Enums.FaceValueType fTemp in Enum.GetValues(typeof(Enums.FaceValueType)))
                            {
                                var card = CardDeck.Cards.FindAll(c => c.FaceValue == fTemp).Find(c => c.Suit != lastSuit);
                                if (card != null)
                                {
                                    foundCards.Add(card);
                                    lastSuit = card.Suit;
                                }
                                else
                                {
                                    foundCards.Clear();
                                }
                                cardsAreAvailable = (foundCards.Count == 5);
                                if (cardsAreAvailable)
                                {
                                    break;
                                }
                            }
                            if (cardsAreAvailable)
                            {
                                break;
                            }
                        }
                        //TODO: handle no cards available. 
                        //We could get this far without any available straight flushes, but not with only two players. Should handle for cases with more than two players.
                        if (foundCards.Count == 5)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                var c = foundCards[i];
                                CardDeck.Cards.Remove(c);
                                p.Hand.Cards.Add(c);
                            }
                        }
                        break;
                    case Enums.HandType.ThreeOfAKind:
                        cardsAreAvailable = false;
                        while (cardsAreAvailable == false)
                        {
                            int rNext = r.Next(0, 12);
                            f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(rNext);
                            foundCards = CardDeck.Cards.FindAll(c => c.FaceValue == f).ToList();
                            cardsAreAvailable = foundCards.Count >= 3;
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            var c = foundCards[i];
                            CardDeck.Cards.Remove(c);
                            p.Hand.Cards.Add(c);
                        }
                        for (int i = 0; i < cardsPerHand; i++)
                        {
                            if (Players[j].Hand.Cards.Count == cardsPerHand)
                            {
                                break;
                            }
                            bool canExit = false;
                            Card cTemp = null;
                            int jj = 0;
                            while (!canExit)
                            {
                                cTemp = CardDeck.Cards[jj];
                                canExit = (Players[j].Hand.Cards.FindAll(c => c.FaceValue == cTemp.FaceValue).Count == 0);
                                jj += 1;
                            }
                            CardDeck.Cards.Remove(cTemp);
                            Players[j].Hand.Cards.Add(cTemp);
                        }
                        break;
                    case Enums.HandType.TwoPairs:
                        //first pair:
                        int lastValue = 0;
                        while (cardsAreAvailable == false)
                        {
                            int rNext = r.Next(0, 12);
                            lastValue = rNext;
                            f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(rNext);
                            foundCards = CardDeck.Cards.FindAll(c => c.FaceValue == f).ToList();
                            cardsAreAvailable = foundCards.Count >= 2;
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            var c = foundCards[i];
                            CardDeck.Cards.Remove(c);
                            p.Hand.Cards.Add(c);
                        }
                        cardsAreAvailable = false;
                        //second pair
                        while (cardsAreAvailable == false)
                        {
                            int rNext = r.Next(0, 12);
                            f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(rNext);
                            if (rNext != lastValue)
                            {
                                foundCards = CardDeck.Cards.FindAll(c => c.FaceValue == f).ToList();
                                cardsAreAvailable = foundCards.Count >= 2;
                            }
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            var c = foundCards[i];
                            CardDeck.Cards.Remove(c);
                            p.Hand.Cards.Add(c);
                        }
                        break;
                    case Enums.HandType.Pair:
                        while (cardsAreAvailable == false)
                        {
                            int rNext = r.Next(0, 12);
                            f = (Enums.FaceValueType)Enum.GetValues(typeof(Enums.FaceValueType)).GetValue(rNext);
                            foundCards = CardDeck.Cards.FindAll(c => c.FaceValue == f).ToList();
                            cardsAreAvailable = foundCards.Count >= 2;
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            var c = foundCards[i];
                            CardDeck.Cards.Remove(c);
                            p.Hand.Cards.Add(c);
                        }
                        for (int i = 0; i < cardsPerHand; i++)
                        {
                            if (Players[j].Hand.Cards.Count == cardsPerHand)
                            {
                                break;
                            }
                            bool canExit = false;
                            Card cTemp = null;
                            int jj = 0;
                            while (!canExit)
                            {
                                cTemp = CardDeck.Cards[jj];
                                canExit = (Players[j].Hand.Cards.FindAll(c => c.FaceValue == cTemp.FaceValue).Count == 0);
                                jj += 1;
                            }
                            CardDeck.Cards.Remove(cTemp);
                            Players[j].Hand.Cards.Add(cTemp);
                        }
                        break;
                    case Enums.HandType.HighCard:
                        //deal with no dupes
                        for (int i = 0; i < cardsPerHand; i++)
                        {
                            if (Players[j].Hand.Cards.Count == cardsPerHand)
                            {
                                break;
                            }
                            bool canExit = false;
                            Card cTemp = null;
                            int jj = 0;
                            while (!canExit)
                            {
                                cTemp = CardDeck.Cards[jj];
                                canExit = (Players[j].Hand.Cards.FindAll(c => c.FaceValue == cTemp.FaceValue).Count == 0);
                                jj += 1;
                            }
                            CardDeck.Cards.Remove(cTemp);
                            Players[j].Hand.Cards.Add(cTemp);
                        }
                        break;
                    default:
                        break;
                }
            }

            for (int j = 0; j < Players.Count; j++)
            {
                for (int i = 0; i < cardsPerHand; i++)
                {
                    if (Players[j].Hand.Cards.Count == cardsPerHand)
                    {
                        break;
                    }
                    var c = CardDeck.Cards[0];
                    CardDeck.Cards.Remove(c);
                    Players[j].Hand.Cards.Add(c);
                }
            }

        }

        public List<Player> DetermineWinner()
        {
            //let's sort the players by hand type:
            Players = Players.OrderByDescending(p => p.Hand.HandType).ToList();
            //players should be sorted by top hand. Let's set the top player then loop through the rest to see how things compare.
            List<Player> topPlayers = new List<Player>();
            Player topPlayer = Players[0];
            bool possibleTie = false;
            //do any other players have the same hand type?
            List<Player> tempPlayers = Players.FindAll(p => p.Hand.HandType == topPlayer.Hand.HandType).ToList();

            //we only need to compare hands if we have a match
            if (tempPlayers.Count() > 1)
            {
                Card c1, c2;
                for (int i = 1; i < tempPlayers.Count(); i++)
                {
                    var player = tempPlayers[i];
                    //let's compare top card (cards should still be sorted from previous action)
                    switch (topPlayer.Hand.HandType)
                    {
                        case Enums.HandType.StraightFlush:
                            c1 = topPlayer.Hand.Cards[0];
                            c2 = player.Hand.Cards[0];
                            if (c2.FaceValue > c1.FaceValue)
                            {
                                topPlayer = player;
                            }
                            else if (c2.FaceValue == c1.FaceValue)
                            {
                                isTie = true;
                                topPlayers.Add(player);
                            }
                            break;
                        case Enums.HandType.FourOfAKind:
                            c1 = topPlayer.Hand.Quadruple[0];
                            c2 = player.Hand.Quadruple[0];
                            if (c2.FaceValue > c1.FaceValue)
                            {
                                topPlayer = player;
                            }
                            break;
                        case Enums.HandType.FullHouse:
                            c1 = topPlayer.Hand.Triple[0];
                            c2 = player.Hand.Triple[0];
                            if (c2.FaceValue > c1.FaceValue)
                            {
                                topPlayer = player;
                            }
                            break;
                        case Enums.HandType.Flush:
                            //let's loop through both hands and stop when we get a high card
                            possibleTie = false;
                            for (int j = 0; j < topPlayer.Hand.Cards.Count(); j++)
                            {
                                c1 = topPlayer.Hand.Cards[j];
                                c2 = player.Hand.Cards[j];
                                if (c2.FaceValue != c1.FaceValue)
                                {
                                    topPlayer = (c2.FaceValue > c1.FaceValue ? player : topPlayer);
                                    possibleTie = false;
                                    break;
                                }
                                else
                                {
                                    possibleTie = true;
                                }
                            }
                            if (possibleTie)
                            {
                                isTie = true;
                                topPlayers.Add(player);
                            }
                            break;
                        case Enums.HandType.Straight:
                            c1 = topPlayer.Hand.Cards[0];
                            c2 = player.Hand.Cards[0];
                            if (c2.FaceValue > c1.FaceValue)
                            {
                                topPlayer = player;
                            }
                            else if (c2.FaceValue == c1.FaceValue)
                            {
                                isTie = true;
                                topPlayers.Add(player);
                            }
                            break;
                        case Enums.HandType.ThreeOfAKind:
                            c1 = topPlayer.Hand.Triple[0];
                            c2 = player.Hand.Triple[0];
                            if (c2.FaceValue > c1.FaceValue)
                            {
                                topPlayer = player;
                            }
                            break;
                        case Enums.HandType.TwoPairs:
                            //each player has two pairs. Let's sort the pairs first.
                            topPlayer.Hand.Pairs = topPlayer.Hand.Pairs.OrderByDescending(p => p[0].FaceValue).ToList();
                            player.Hand.Pairs = player.Hand.Pairs.OrderByDescending(p => p[0].FaceValue).ToList();
                            for (int j = 0; j < topPlayer.Hand.Pairs.Count(); j++)
                            {
                                c1 = topPlayer.Hand.Pairs[0][0];
                                c2 = player.Hand.Pairs[0][0];
                                if (c2.FaceValue > c1.FaceValue)
                                {
                                    topPlayer = player;
                                    break;
                                }
                            }
                            //both pairs must match, let's look for high card. Not worth it to look for the lone card - just compare all of them.
                            possibleTie = false;
                            for (int j = 0; j < topPlayer.Hand.Cards.Count(); j++)
                            {
                                c1 = topPlayer.Hand.Cards[j];
                                c2 = player.Hand.Cards[j];
                                if (c2.FaceValue != c1.FaceValue)
                                {
                                    topPlayer = (c2.FaceValue > c1.FaceValue ? player : topPlayer);
                                    possibleTie = false;
                                    break;
                                }
                                else
                                {
                                    possibleTie = true;
                                }
                            }
                            if (possibleTie)
                            {
                                isTie = true;
                                topPlayers.Add(player);
                            }

                            break;
                        case Enums.HandType.Pair:
                            c1 = topPlayer.Hand.Pairs[0][0];
                            c2 = player.Hand.Pairs[0][0];
                            if (c2.FaceValue > c1.FaceValue)
                            {
                                topPlayer = player;
                                break;
                            }
                            else if (c2.FaceValue == c1.FaceValue)
                            {
                                //pairs match, let's look for high card
                                possibleTie = false;
                                for (int j = 0; j < topPlayer.Hand.Cards.Count(); j++)
                                {
                                    c1 = topPlayer.Hand.Cards[j];
                                    c2 = player.Hand.Cards[j];
                                    if (c2.FaceValue != c1.FaceValue)
                                    {
                                        topPlayer = (c2.FaceValue > c1.FaceValue ? player : topPlayer);
                                        possibleTie = false;
                                        break;
                                    }
                                    else
                                    {
                                        possibleTie = true;
                                    }
                                }
                                if (possibleTie)
                                {
                                    isTie = true;
                                    topPlayers.Add(player);
                                }
                            }
                            break;
                        case Enums.HandType.HighCard:
                            possibleTie = false;
                            for (int j = 0; j < topPlayer.Hand.Cards.Count(); j++)
                            {
                                c1 = topPlayer.Hand.Cards[j];
                                c2 = player.Hand.Cards[j];
                                if (c2.FaceValue != c1.FaceValue)
                                {
                                    topPlayer = (c2.FaceValue > c1.FaceValue ? player : topPlayer);
                                    possibleTie = false;
                                    break;
                                }
                                else
                                {
                                    possibleTie = true;
                                }
                            }
                            if (possibleTie)
                            {
                                isTie = true;
                                topPlayers.Add(player);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            topPlayers.Add(topPlayer);
            return topPlayers;
        }
    }
}