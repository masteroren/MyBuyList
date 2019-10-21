<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavigationLinks.ascx.cs" Inherits="UserControls_NavigationLinks" %>

<div class="navigation-links">
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Recipes.aspx" CssClass="link">
        <asp:Label ID="Label1" runat="server" Text="מתכונים" CssClass="link-text"></asp:Label>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/home.svg" />
    </asp:HyperLink>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Menus.aspx" CssClass="link">
        <asp:Label ID="Label2" runat="server" Text="תפריטים" CssClass="link-text"></asp:Label>
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/menu.svg" />
    </asp:HyperLink>
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Register.aspx" CssClass="on-logout link">
        <asp:Label ID="Label3" runat="server" Text="הצטרפות" CssClass="link-text"></asp:Label>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/signin.svg" />
    </asp:HyperLink>
    <asp:HyperLink ID="lnkHowToUse" runat="server" NavigateUrl="~/ContactUs.aspx" CssClass="link">
        <asp:Label ID="Label4" runat="server" Text="צור קשר" CssClass="link-text"></asp:Label>
        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/contactus.svg" />
    </asp:HyperLink>
</div>