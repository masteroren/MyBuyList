<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRecipesFilter.ascx.cs"
    Inherits="UserControls_ucRecipesFilter" %>

<script src="UserControls/scripts/ucRecipesFilter.js"></script>

<style>
    .filter {
        display: flex;
        width: 270px;
        justify-content: space-between;
    }

    .recipes-search-option {
        text-decoration: none !important;
        color: #A4CB3A !important;
    }

        .recipes-search-option:hover {
            text-decoration: underline !important;
        }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="filter">
            <asp:DropDownList ID="lstCategories" runat="server" Width="137px" Height="18px" BackColor="#DDECB6"
                Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; padding-right: 5px;"
                OnSelectedIndexChanged="lstCategories_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSortBy" runat="server" Width="129px" Height="18px" BackColor="#DDECB6"
                Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; padding-right: 5px;"
                onchange="return changeSort(this);">
                <asp:ListItem Text="מיין לפי" Value="0"></asp:ListItem>
                <asp:ListItem Text="תאריך" Value="LastUpdate"></asp:ListItem>
                <asp:ListItem Text="שם" Value="Name"></asp:ListItem>
                <asp:ListItem Text="מחבר" Value="Publisher"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

