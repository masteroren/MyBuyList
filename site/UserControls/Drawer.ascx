<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Drawer.ascx.cs" Inherits="UserControls_drawer" %>
<%@ Register Src="~/UserControls/NavigationLinks.ascx" TagPrefix="uc1" TagName="NavigationLinks" %>
<%@ Register Src="~/UserControls/RecipeDetailsActions.ascx" TagPrefix="uc1" TagName="RecipeDetailsActions" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Panel2" runat="server" CssClass="drawer">
            <div class="drawer-trigger">
                <img alt="" src="Images/drawer_open.svg" onclick="openDrawer()" />
            </div>
            <asp:Panel ID="Panel1" runat="server" CssClass="drawer-panel">
                <div class="panel-close-trigger">
                    <img alt="" src="Images/cross.svg" onclick="closeDrawer()" />
                </div>

                <uc1:NavigationLinks runat="server" ID="NavigationLinks" />
                <div class="show-on-login">
                    <uc1:RecipeDetailsActions runat="server" ID="RecipeDetailsActions" />
                </div>
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<script>
    $(document).ready(() => {
        openDrawer = () => {
            var drawerPanel = $('.drawer-panel');
            drawerPanel.removeClass('close');
            drawerPanel.addClass('open');
            $('.drawer-trigger').hide(500);
        }

        closeDrawer = () => {
            var drawerPanel = $('.drawer-panel');
            drawerPanel.removeClass('open');
            drawerPanel.addClass('close');
            $('.drawer-trigger').show(500);
        }
    });
</script>
