using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokerly.Classes
{
    public class CardDeck
    {
        public const int CardsPerDeck = 52;

        private List<Card> cards;
        Card[] freshCards;
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

        public CardDeck()
        {
        }

        public void FillDeck()
        {
            Cards = new List<Card>();
            foreach (Enums.SuitType suit in Enum.GetValues(typeof(Enums.SuitType)))
            {
                foreach (Enums.FaceValueType faceValue in Enum.GetValues(typeof(Enums.FaceValueType)))
                {
                    Cards.Add(new Card(suit, faceValue));
                }
            }
        }

        public void Shuffle()
        {
            List<Card> cardsNew = new List<Card>();
            Random r = new Random();
            while (cards.Count() > 0)
            {
                var c = cards[r.Next(0, cards.Count() - 1)];
                cards.Remove(c);
                cardsNew.Add(c);
            }
            cards = cardsNew;
        }
    }
}