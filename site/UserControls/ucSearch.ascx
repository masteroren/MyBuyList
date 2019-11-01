<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearch.ascx.cs" Inherits="UserControls_ucSearch" %>

<script type="text/javascript">
    var hfSearchedValueClientId = '<%=hfSearchValue.ClientID%>';
    var hfSearchCategoryIdClientId = '<%=hfSelectedCategoryId.ClientID%>';
    var hfSearchCategoryTextClientId = '<%=hfSelectedCategoryText.ClientID%>'
</script>

<script type="text/javascript" src="Scripts/Search.js"></script>

<div class="search-box">
    <input id="Text1" type="text" class="search-value" />
    <%--<asp:TextBox ID="TextBoxSearchValue" runat="server" CssClass="search-value" flex-grow="1"></asp:TextBox>--%>
</div>
<%--<div class="search-filter">
    <asp:RadioButton ID="RadioButton1" runat="server" Text="מתכונים" Checked="true" GroupName="search-filter" OnCheckedChanged="RadioButton1_CheckedChanged"/>
    <asp:RadioButton ID="RadioButton2" runat="server" CssClass="on-login" Text="המתכונים שלי" GroupName="search-filter" OnCheckedChanged="RadioButton2_CheckedChanged" />
    <asp:RadioButton ID="RadioButton3" runat="server" CssClass="on-login" Text="המתכונים המועדפים שלי" GroupName="search-filter" OnCheckedChanged="RadioButton3_CheckedChanged" />
    <asp:RadioButton ID="RadioButton4" runat="server" Text="תפריטים" GroupName="search-filter" OnCheckedChanged="RadioButton4_CheckedChanged" />
    <asp:RadioButton ID="RadioButton5" runat="server" CssClass="on-login" Text="התפריטים שלי" GroupName="search-filter" OnCheckedChanged="RadioButton5_CheckedChanged" />
    <asp:RadioButton ID="RadioButton6" runat="server" CssClass="on-login" Text="התפריטים המועדפים שלי" GroupName="search-filter" OnCheckedChanged="RadioButton6_CheckedChanged" />
</div>--%>

<asp:HiddenField ID="hfSelectedCategoryId" runat="server" />
<asp:HiddenField ID="hfSelectedCategoryText" runat="server" />
<asp:HiddenField ID="hfSearchValue" runat="server" />
