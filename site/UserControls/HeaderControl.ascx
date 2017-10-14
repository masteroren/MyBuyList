<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControl.ascx.cs" Inherits="UC_HeaderControl" %>
<%@ Register Src="~/UserControls/ucSearch.ascx" TagPrefix="MBL" TagName="SearchBox" %>

<div id="header" class="header flex-container flex-column">
    <div class="helloUser_and_links flex-container" justify-content="space-between" align-items="center">
        <div class="helloUser flex-container" justify-content="space-between">
            <asp:Label ID="lblHeaderUserName" runat="server" Font-Bold="true" CssClass="HelloUser" Text="שלום, אורח" />
            <a class="login">כניסה</a>
        </div>
        <div class="links flex-container" justify-content="space-between">
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Recipes.aspx">מתכונים</asp:HyperLink>
            <span>|</span>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Menus.aspx">תפריטים</asp:HyperLink>
            <span>|</span>
            <asp:PlaceHolder ID="PlaceHolderJoin" runat="server">
                <asp:HyperLink ID="lnkReg1" NavigateUrl="~/Register.aspx" runat="server" Text="הצטרפות"
                    onclick='return allowLeave()'></asp:HyperLink>
                <span>|</span>
            </asp:PlaceHolder>
            <asp:HyperLink ID="lnkHowToUse" runat="server" Text="צור קשר" NavigateUrl="~/ContactUs.aspx"
                onclick='return allowLeave()'></asp:HyperLink>
        </div>
    </div>
    <div class="logo-and-search flex-container" align-items="center">
        <span class="logo">
            <asp:HyperLink ID="lnkLogo" runat="server" NavigateUrl="~/" onclick='return allowLeave()'>
                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/mybuylist.png" />
            </asp:HyperLink>
        </span>
        <span class="search" flex-grow="1">
            <MBL:SearchBox runat="server" ID="Search" />
        </span>
    </div>
</div>    
