<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRecipesFilter.ascx.cs"
    Inherits="UserControls_ucRecipesFilter" %>

<script src="UserControls/scripts/ucRecipesFilter.js"></script>

<style>
    .recipes-search-option {
        text-decoration: none !important;
        color: #A4CB3A !important;
    }

        .recipes-search-option:hover {
            text-decoration: underline !important;
        }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" class="recipes-filter">
    <ContentTemplate>
        <div class="filter">
            <asp:DropDownList ID="lstCategories" runat="server" Width="137px" Height="18px" BackColor="#DDECB6"
                Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; padding-right: 5px;"
                OnSelectedIndexChanged="lstCategories_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSortBy" runat="server" Width="129px" Height="18px" BackColor="#DDECB6"
                Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; padding-right: 5px;"
                OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="מיין לפי" Value="0"></asp:ListItem>
                <asp:ListItem Text="תאריך" Value="1"></asp:ListItem>
                <asp:ListItem Text="שם" Value="2"></asp:ListItem>
                <asp:ListItem Text="מחבר" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="categories" class="categories-breadcrumbs" runat="server" visible="true">
            <div id="pathLinks" runat="server" style="margin-bottom: 10px;">
            </div>
            <div style="float: right">
                <asp:Panel ID="pnlCategories" runat="server" Width="300px" Height="170px" BorderWidth="1px"
                    BorderColor="#656565" ScrollBars="Vertical" Style="margin: 0px auto;" Visible="false">
                    <table style="width: 90%">
                        <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td align="right">&nbsp;
                                                            <asp:HyperLink ID="lnkCategory" runat="server" ForeColor="#656565"></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

