<%@ Page Title="Pokerly: Dealt Hand" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlayHand.aspx.cs" Inherits="Pokerly.DealtHand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h3><asp:Label ID="lblResult" runat="server"></asp:Label></h3>
    </div>
     <div class="row">
        <div class="col-md-4">
            <h3><asp:Label ID="lblPlayer1" runat="server"></asp:Label></h3>
            <asp:Label ID="lblPlayer1Hand" runat="server"></asp:Label>
        </div>
        <div class="col-md-4">
            <h3><asp:Label ID="lblPlayer2" runat="server"></asp:Label></h3>
             <asp:Label ID="lblPlayer2Hand" runat="server"></asp:Label>
        </div>
    </div>
        <div class="row top-buffer">
        <asp:Button ID="btnDealAgain" runat="server" Text="Deal Again" OnClick="btnDealAgain_Click" />&nbsp;
            <asp:Button ID="btnStartOver" runat="server" Text="Start Over" OnClick="btnStartOver_Click" />
    </div>
</asp:Content>
