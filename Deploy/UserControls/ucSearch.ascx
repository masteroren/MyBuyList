<%@ control language="C#" autoeventwireup="true" inherits="UserControls_ucSearch, mybuylist" %>

<script type="text/javascript">
    var hfSearchedValueClientId = '<%=hfSearchValue.ClientID%>';
    var hfSearchCategoryIdClientId = '<%=hfSelectedCategoryId.ClientID%>';
    var hfSearchCategoryTextClientId = '<%=hfSelectedCategoryText.ClientID%>'
</script>

<script type="text/javascript" src="Scripts/Search.js"></script>

<div class="search-box flex-container" justify-content="start">
    <asp:TextBox ID="TextBoxSearchValue" runat="server" CssClass="search-value" flex-grow="1"></asp:TextBox>
    <div class="flex-container">
        <asp:DropDownList ID="DropDownListSearchType" runat="server" CssClass="search-category" DataTextField="value" DataValueField="key">
            <asp:ListItem Value="1">מתכונים</asp:ListItem>
            <asp:ListItem Value="2" class="requireLogin">המתכונים שלי</asp:ListItem>
            <asp:ListItem Value="3" class="requireLogin">המתכונים המועדפים שלי</asp:ListItem>
            <asp:ListItem Value="4">תפריטים</asp:ListItem>
            <asp:ListItem Value="5" class="requireLogin">התפריטים שלי</asp:ListItem>
            <asp:ListItem Value="6" class="requireLogin">התפריטים המועדפים שלי</asp:ListItem>
        </asp:DropDownList>
        <div class="search-btn">
            <asp:ImageButton ID="ImageButtonSearch" runat="server" ImageUrl="~/Images/Very-Basic-Search-icon.png" CssClass="search-button" OnClientClick="return false;" />
        </div>
    </div>
</div>

<asp:HiddenField ID="hfSelectedCategoryId" runat="server" />
<asp:HiddenField ID="hfSelectedCategoryText" runat="server" />
<asp:HiddenField ID="hfSearchValue" runat="server" />
