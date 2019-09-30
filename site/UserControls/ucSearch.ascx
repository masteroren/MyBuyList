<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearch.ascx.cs" Inherits="UserControls_ucSearch" %>

<script type="text/javascript">
    var hfSearchedValueClientId = '<%=hfSearchValue.ClientID%>';
    var hfSearchCategoryIdClientId = '<%=hfSelectedCategoryId.ClientID%>';
    var hfSearchCategoryTextClientId = '<%=hfSelectedCategoryText.ClientID%>'
</script>

<script type="text/javascript" src="Scripts/Search.js"></script>

<style>
    .search-box {
        width: 100%;
    }

    .search-value, .search-category {
        font-size: large;
    }
</style>

<div class="search-box flex-container">
    <asp:TextBox ID="TextBoxSearchValue" runat="server" CssClass="search-value" flex-grow="1"></asp:TextBox>
    <div>
        <asp:DropDownList ID="SearchType" runat="server" CssClass="search-category" DataTextField="value" DataValueField="key">
            <asp:ListItem Value="1">מתכונים</asp:ListItem>
            <asp:ListItem Value="2" class="show-on-logged-in">המתכונים שלי</asp:ListItem>
            <asp:ListItem Value="3" class="show-on-logged-in">המתכונים המועדפים שלי</asp:ListItem>
            <asp:ListItem Value="4">תפריטים</asp:ListItem>
            <asp:ListItem Value="5" class="show-on-logged-in">התפריטים שלי</asp:ListItem>
            <asp:ListItem Value="6" class="show-on-logged-in">התפריטים המועדפים שלי</asp:ListItem>
        </asp:DropDownList>
        <div class="search-btn">
            <asp:ImageButton ID="ImageButtonSearch" runat="server" ImageUrl="~/Images/Very-Basic-Search-icon.png" CssClass="search-button" OnClick="ImageButtonSearch_Click" OnClientClick="return false;" />
        </div>
    </div>
</div>

<asp:HiddenField ID="hfSelectedCategoryId" runat="server" />
<asp:HiddenField ID="hfSelectedCategoryText" runat="server" />
<asp:HiddenField ID="hfSearchValue" runat="server" />
