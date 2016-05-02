using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokerly.Classes
{
    public class Enums
    {
        public enum SuitType
        {
            [StringValue("♣")]
            Club = 0,
            [StringValue("♠")]
            Spade = 1,
            [StringValue("♥")]
            Heart = 2,
            [StringValue("♦")]
            Diamond = 3
        }

        public enum FaceValueType
        {
            [StringValue("2")]
            Two = 2,
            [StringValue("3")]
            Three = 3,
            [StringValue("4")]
            Four = 4,
            [StringValue("5")]
            Five = 5,
            [StringValue("6")]
            Six = 6,
            [StringValue("7")]
            Seven = 7,
            [StringValue("8")]
            Eight = 8,
            [StringValue("9")]
            Nine = 9,
            [StringValue("10")]
            Ten = 10,
            [StringValue("J")]
            Jack = 11,
            [StringValue("Q")]
            Queen = 12,
            [StringValue("K")]
            King = 13,
            [StringValue("A")]
            Ace = 14,

        }

        public enum HandType
        {
            [StringValue("Straight Flush")]
            StraightFlush = 8,
            [StringValue("Four of a Kind")]
            FourOfAKind = 7,
            [StringValue("Full House")]
            FullHouse = 6,
            [StringValue("Flush")]
            Flush = 5,
            [StringValue("Straight")]
            Straight = 4,
            [StringValue("Three of a Kind")]
            ThreeOfAKind = 3,
            [StringValue("Two Pairs")]
            TwoPairs = 2,
            [StringValue("Pair")]
            Pair = 1,
            [StringValue("High Card")]
            HighCard = 0,
            [StringValue("Deal from Deck")]
            DealFromDeck = -1,
        }

    }
}