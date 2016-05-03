using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Pokerly.Classes;

namespace Pokerly
{
    public partial class DealtHand : System.Web.UI.Page
    {
        private Round r;
        private Player player1;
        private Player player2;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPlayers();
            DealCards();
            ShowHands();
            ShowResult();
        }
        private void LoadPlayers()
        {
            try
            {
                player1 = (Player)Session["Player1"];
                player2 = (Player)Session["Player2"];

                lblPlayer1.Text = player1.Name;
                lblPlayer2.Text = player2.Name;
            }
            catch (Exception)
            {
                //if this fails, the session has liklely expired. Head home.
                Response.Redirect("Default.aspx");
            }

        }

        private void DealCards()
        {
            CardDeck cardDeck = new CardDeck();
            var obj = Cache.Get("DeckOfCards");
            Card[] freshCards;
            if (obj != null && obj.GetType() == typeof(Card[]))
            {
                freshCards = (Card[])obj;
                cardDeck.Cards = freshCards.ToList();
            }
            else
            {
                cardDeck.FillDeck();
                Cache.Insert("DeckOfCards", cardDeck.Cards.ToArray<Card>());
            }

            cardDeck.Shuffle();

            r = new Round(cardDeck);
            r.Players.Add(player1);
            r.Players.Add(player2);

            foreach (var player in r.Players)
            {
                player.Hand.Reset();
            }

            r.DealCards();
        }
        private void ShowHands()
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (var player in r.Players)
            {
                player.Hand.EvaluateHand();

                foreach (var card in player.Hand.Cards)
                {
                    if (player.Id == player1.Id)
                    {
                        sb1.Append("<span class='" + card.Suit.ToString() + "'>" + StringEnum.GetStringValue(card.FaceValue) + StringEnum.GetStringValue(card.Suit) + "</span>");
                    }
                    else if (player.Id == player2.Id)
                    {
                        sb2.Append("<span class='" + card.Suit.ToString() + "'>" + StringEnum.GetStringValue(card.FaceValue) + StringEnum.GetStringValue(card.Suit) + "</span>");
                    }
                }
                if (player.Id == player1.Id)
                {
                    sb1.Append("<br/> " + StringEnum.GetStringValue(player.Hand.HandType));
                }
                else if (player.Id == player2.Id)
                {
                    sb2.Append("<br/> " + StringEnum.GetStringValue(player.Hand.HandType));
                }
            }

            lblPlayer1Hand.Text = sb1.ToString();
            lblPlayer2Hand.Text = sb2.ToString();

        }

        private void ShowResult()
        {
            StringBuilder sbResult = new StringBuilder();
            List<Player> winners = r.DetermineWinner();
            foreach (var w in winners)
            {
                if(sbResult.Length > 0)
                {
                    sbResult.Append(" and ");
                }
                sbResult.Append(w.Name);
            }
            if (r.IsTie)
            {
                sbResult.Append(" tied as the winners of this round.");
            }
            else
            {
                sbResult.Append(" won this round.");
            }

            lblResult.Text = sbResult.ToString();
        }

        protected void btnDealAgain_Click(object sender, EventArgs e)
        {            
                Response.Redirect("PlayHand.aspx");
        }

        protected void btnStartOver_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

    }
}