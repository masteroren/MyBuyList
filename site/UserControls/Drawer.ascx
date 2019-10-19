<%@ Control Language="C#" AutoEventWireup="true" CodeFile="drawer.ascx.cs" Inherits="UserControls_drawer" %>
<%@ Register Src="~/UserControls/NavigationLinks.ascx" TagPrefix="uc1" TagName="NavigationLinks" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Panel2" runat="server" CssClass="drawer">
            <div class="drawer-trigger">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/drawer_open.svg" OnClick="ImageButton1_Click" />
            </div>
            <asp:Panel ID="Panel1" runat="server" CssClass="drawer-panel" Visible="false">
                <div class="panel-close-trigger">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cross.svg" OnClick="ImageButton2_Click" />
                </div>

                <uc1:NavigationLinks runat="server" ID="NavigationLinks" />
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
