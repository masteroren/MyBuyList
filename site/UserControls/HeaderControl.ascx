<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControl.ascx.cs" Inherits="UC_HeaderControl" %>
<%@ Register Src="~/UserControls/ucSearch.ascx" TagPrefix="MBL" TagName="SearchBox" %>

<div id="header" class="header">
    <div class="header-top">
        <div class="welcome">
            <asp:Label ID="lblHeaderUserName" runat="server" Font-Bold="true" CssClass="hello-user" Text="שלום, אורח" />
            <a class="login">כניסה</a>
        </div>
        <div class="links">
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Recipes.aspx">מתכונים</asp:HyperLink>
            <div class="separator">|</div>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Menus.aspx">תפריטים</asp:HyperLink>
            <div class="separator">|</div>
            <asp:PlaceHolder ID="PlaceHolderJoin" runat="server">
                <asp:HyperLink ID="lnkReg1" NavigateUrl="~/Register.aspx" runat="server" Text="הצטרפות"
                    onclick='return allowLeave()'></asp:HyperLink>
                <div class="separator">|</div>
            </asp:PlaceHolder>
            <asp:HyperLink ID="lnkHowToUse" runat="server" Text="צור קשר" NavigateUrl="~/ContactUs.aspx"
                onclick='return allowLeave()'></asp:HyperLink>
        </div>
    </div>
    <div class="header-bottom">
        <span class="logo">
            <asp:HyperLink ID="lnkLogo" runat="server" NavigateUrl="~/" onclick='return allowLeave()'>
                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/mybuylist.png" />
            </asp:HyperLink>
        </span>
        <span class="search">
            <MBL:SearchBox runat="server" ID="Search" />
        </span>
    </div>
</div>
