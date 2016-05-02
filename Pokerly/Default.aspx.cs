using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using Pokerly.Classes;

namespace Pokerly
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {

            foreach (Enums.HandType item in Enum.GetValues(typeof(Enums.HandType)))
            {
                var s = StringEnum.GetStringValue(item);
                var id = item.ToString();
                ddlHandPlayer1.Items.Add(new ListItem(s, id));
                ddlHandPlayer2.Items.Add(new ListItem(s, id));
            }
            ddlHandPlayer1.SelectedValue = Enums.HandType.DealFromDeck.ToString();
            ddlHandPlayer2.SelectedValue = Enums.HandType.DealFromDeck.ToString();

            try
            {
                var player1 = (Player)Session["Player1"];
               var player2 = (Player)Session["Player2"];

                txtPlayer1.Text = player1.Name;
                ddlHandPlayer1.SelectedValue = player1.HandTypeOverride.ToString();
                txtPlayer2.Text = player2.Name;
                ddlHandPlayer2.SelectedValue = player2.HandTypeOverride.ToString();
            }
            catch (Exception)
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var player1 = new Player(Guid.NewGuid().ToString(), txtPlayer1.Text);
            var player2 = new Player(Guid.NewGuid().ToString(), txtPlayer2.Text);

            if (ddlHandPlayer1.SelectedIndex > 0)
            {
                player1.HandTypeOverride = (Enums.HandType)Enum.Parse(typeof(Enums.HandType), ddlHandPlayer1.SelectedValue);
            }

            if (ddlHandPlayer1.SelectedIndex > 0)
            {
                player2.HandTypeOverride = (Enums.HandType)Enum.Parse(typeof(Enums.HandType), ddlHandPlayer2.SelectedValue);
            }

            Session.Add("Player1", player1);
            Session.Add("Player2", player2);

            if (Page.IsValid)
                Response.Redirect("PlayHand.aspx");
        }
    }
}