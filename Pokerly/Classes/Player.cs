using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokerly.Classes
{
    public class Player
    {
        private string id;
        private string name;
        private Hand hand;
        private Enums.HandType handTypeOverride;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public Hand Hand
        {
            get
            {
                return hand;
            }

            set
            {
                hand = value;
            }
        }

        public Enums.HandType HandTypeOverride
        {
            get
            {
                return handTypeOverride;
            }

            set
            {
                handTypeOverride = value;
            }
        }

        public Player(string id, string name)
        {
            Hand = new Hand();
            Id = id;
            Name = name;
        }

    }
}