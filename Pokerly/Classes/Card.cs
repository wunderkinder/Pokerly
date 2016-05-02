using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokerly.Classes
{
    public class Card
    {
        private int sortOrder;
        private Enums.SuitType suit;
        private string displayString;
        private Enums.FaceValueType faceValue;


        public int SortOrder
        {
            get { return sortOrder; }
            set
            {
                sortOrder = value;
            }

        }

        public Enums.SuitType Suit
        {
            get
            {
                return suit;
            }

            set
            {
                suit = value;
            }
        }

        public Enums.FaceValueType FaceValue
        {
            get
            {
                return faceValue;
            }

            set
            {
                faceValue = value;
            }
        }

        public Card(Enums.SuitType suit, Enums.FaceValueType faceValue )
        {
            Suit = suit;
            FaceValue = faceValue;
            SortOrder = (int)StringEnum.Parse(typeof(Enums.FaceValueType), StringEnum.GetStringValue(FaceValue));
        }
    }

    class CardFaceValueComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card x, Card y)
        {
                        // Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the Cards' properties are equal. 
            return x.FaceValue == y.FaceValue;
        }

        // If Equals() returns true for a pair of objects, 
        // GetHashCode must return the same value for these objects. 

        public int GetHashCode(Card Card)
        {
            // Check whether the object is null. 
            if (Object.ReferenceEquals(Card, null)) return 0;

            // Get the hash code for the FaceValue. 
            int hashCardName = Card.FaceValue.GetHashCode();

            // Get the hash code for the Code field. 
            int hashCardCode = Card.Suit.GetHashCode();

            // Calculate the hash code for the Card. 
            return hashCardName ^ hashCardCode;
        }

    }
}