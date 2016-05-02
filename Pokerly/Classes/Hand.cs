using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokerly.Classes
{
    public class Hand
    {
        private List<Card> cards;
        private Enums.HandType handType;
        private List<List<Card>> pairs;
        private List<Card> triple;
        private List<Card> quadruple;
        public List<Card> Cards
        {
            get
            {
                return cards;
            }

            set
            {
                cards = value;
            }
        }

        public Enums.HandType HandType
        {
            get
            {
                return handType;
            }

            set
            {
                handType = value;
            }
        }

        public List<Card> Quadruple
        {
            get
            {
                return quadruple;
            }

            set
            {
                quadruple = value;
            }
        }

        public List<Card> Triple
        {
            get
            {
                return triple;
            }

            set
            {
                triple = value;
            }
        }

        public List<List<Card>> Pairs
        {
            get
            {
                return pairs;
            }

            set
            {
                pairs = value;
            }
        }

        public Hand()
        {
            Cards = new List<Card>();
        }


        public void Reset()
        {
            Cards = new List<Card>();
            Quadruple = null;
            Triple = null;
            Pairs = null;
        }

        public void EvaluateHand()
        {
            Cards = Cards.OrderByDescending(c => c.SortOrder).ToList<Card>();
            if (isStraightFlush())
            {
                HandType = Enums.HandType.StraightFlush;
                return;
            }
            if (isFourOfAKind())
            {
                HandType = Enums.HandType.FourOfAKind;
                return;
            }
            if (isFullHouse())
            {
                HandType = Enums.HandType.FullHouse;
                return;
            }
            if (isFlush())
            {
                HandType = Enums.HandType.Flush;
                return;
            }
            if (isStraight())
            {
                HandType = Enums.HandType.Straight;
                return;
            }
            if (isThreeOfAKind())
            {
                HandType = Enums.HandType.ThreeOfAKind;
                return;
            }
            if (isTwoPairs())
            {
                HandType = Enums.HandType.TwoPairs;
                return;
            }
            if (isPair(Cards))
            {
                HandType = Enums.HandType.Pair;
                return;
            }

            HandType = Enums.HandType.HighCard;
            return;

        }

        /// <summary>
        /// Five cards of the same suit with consecutive values. Ranked by the highest card in the hand.
        /// </summary>
        /// <returns></returns>
        private bool isStraightFlush()
        {
            if (isFlush())
            {
                return isStraight();
            }

            return false;
        }

        private bool isFourOfAKind()
        {
            //we only need to evaluate the first two cards. If neither of them match 3 other cards, we do not have a four of a kind.
            for (int i = 0; i < 2; i++)
            {
                List<Card> cards = Cards.FindAll(c => c.FaceValue == Cards[i].FaceValue).ToList<Card>();
                if (cards.Count() == 4)
                {
                    //four cards match, we have a four of a kind
                    Quadruple = cards;
                    return true;
                }
            }
            return false;
        }

        private bool isFullHouse()
        {
            //check for triple first:
            if (isThreeOfAKind())
            {
                //we have a triple, let's check for a pair in the remaining cards.
                if (isPair(Cards.FindAll(c => c.FaceValue != Triple[0].FaceValue)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isFlush()
        {
            if (Cards.All(c => c.Suit == Cards[0].Suit))
            {
                //the suits match; it's a flush
                return true;
            }

            return false;
        }

        private bool isStraight()
        {
            //are the cards sequential?
            int lastCardSortOrder = -1;
            foreach (var card in Cards)
            {
                //if (lastCardSortOrder == -1) { lastCardSortOrder = card.SortOrder; }
                int diff = (lastCardSortOrder - card.SortOrder);
                if ((diff > 1 || diff == 0) && (lastCardSortOrder != -1))
                {
                    //the gap between cards is more than one (or is zero); not a straight flush
                    return false;
                }
                lastCardSortOrder = card.SortOrder;
            }

            return true;
        }
        private bool isThreeOfAKind()
        {
            //we only need to evaluate the first three cards. If neither of them match 2 other cards, we do not have a three of a kind.
            for (int i = 0; i < 3; i++)
            {
                int count = Cards.Count(c => c.FaceValue == Cards[i].FaceValue);
                if (count == 3)
                {
                    //three cards match, we have a three of a kind
                    Triple = Cards.FindAll(c => c.FaceValue == cards[i].FaceValue);
                    return true;
                }
            }
            return false;
        }

        private bool isTwoPairs()
        {
            //check for the first pair:
            if (isPair(Cards))
            {
                //we have one pair, let's check for another pair in the remaining cards.
                //the pairs list should have one item in it.
                List<Card> pair = Pairs[0];
                if (isPair(Cards.FindAll(c => c.FaceValue != pair[0].FaceValue)))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cards">This can be the whole hand or a subset of cards that have had another set of cards removed</param>
        /// <returns></returns>
        private bool isPair(List<Card> Cards)
        {
            for (int i = 0; i < Cards.Count(); i++)
            {
                int count = Cards.Count(c => c.FaceValue == Cards[i].FaceValue);
                if (count == 2)
                {
                    //two cards match, we have a pair
                    if (Pairs == null)
                    {
                        Pairs = new List<List<Card>>();
                    }
                    var pair = Cards.FindAll(c => c.FaceValue == Cards[i].FaceValue);
                    Pairs.Add(pair);
                    return true;
                }
            }

            return false;
        }
    }
}