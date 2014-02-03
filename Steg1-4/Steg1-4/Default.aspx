<%@ Page Title="Gissa det hemliga talet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Steg1_4._Default" ViewStateMode="Disabled" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:Label ID="LabelGuessInfo" runat="server" Text="Ange ett tal mellan 1 och 100:"></asp:Label>
    <asp:TextBox ID="TextBoxGuess" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Fältet får inte vara tomt" ControlToValidate="TextBoxGuess" Display="None"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Gissningen måste vara ett tal mellan 1 och 100" ControlToValidate="TextBoxGuess" MinimumValue="1" MaximumValue="100" Type="Integer" Display="None"></asp:RangeValidator>
    <asp:Button ID="ButtonGuess" runat="server" Text="Skicka gissning" OnClick="ButtonGuess_Click" />
    <br />
    <asp:Literal ID="LiteralGuesses" runat="server"></asp:Literal>
    <asp:Literal ID="LiteralOutcome" runat="server"></asp:Literal>
    <br />
    <asp:Literal ID="LiteralFail" runat="server"></asp:Literal>
    <asp:Button ID="ButtonNewSecretNumber" runat="server" Text="Slumpa ett nytt hemligt tal" Visible="False" Enabled="False" OnClick="ButtonNewSecretNumber_Click" />
</asp:Content>
