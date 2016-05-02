<%@ Page Title="Pokerly" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pokerly._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblMain" runat="server"></asp:Label>
    <div class="row">

        <p class="lead">This web application will build a deck of cards, deal a poker hand to each player, and calculate the winner of the hand.</p>
        <p>To get started, please enter a name for each player. Optionally, you may specify the type of hand you would like each player to be dealt.</p>
    </div>
    <div class="row">
        <ul class="list-group">
            <li class="list-group-item">Player 1:
                <asp:TextBox ID="txtPlayer1" runat="server"></asp:TextBox>&nbsp;
            <asp:RequiredFieldValidator ID="rfdPlayer1" ControlToValidate="txtPlayer1" runat="server" Text="*" ForeColor="Red" ErrorMessage="Please enter a name for player 1."></asp:RequiredFieldValidator> <asp:DropDownList ID="ddlHandPlayer1" style="float:right;" runat="server"></asp:DropDownList></li>
            <li class="list-group-item">Player 2:
                <asp:TextBox ID="txtPlayer2" runat="server"></asp:TextBox>&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlayer2" runat="server" ForeColor="Red" Text="*" ErrorMessage="Please enter a name for player 2."></asp:RequiredFieldValidator> <asp:DropDownList ID="ddlHandPlayer2" style="float:right;" runat="server"></asp:DropDownList></li>

        </ul>
    </div>
    <div class="row">
        <asp:ValidationSummary ID="valSumMain" DisplayMode="List" runat="server" />
        <asp:Button ID="btnSubmit" runat="server" Text="Start Game" CausesValidation="true" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
