﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRecipeCategories.ascx.cs"
    Inherits="ucRecipeCategories" %>
<asp:Panel ID="Panel1" runat="server" Style="display: none" BorderWidth="1px" BorderColor="#8CAC31"
    BackColor="White" Width="370px" CssClass="categories-modal-popup">
    <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; background-color: #8CAC31; color: White; height: 24px;">
        <div style="text-align: center;">
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="11pt" Font-Bold="true"
                Height="15px" Text="<%$ Resources:MyGlobalResources, SelectCategories %>"></asp:Label>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlList" runat="server" ScrollBars="Vertical" BorderColor="LightGray"
        BorderWidth="1px" Height="330px" Width="358px" Style="padding: 0px 5px 0px 5px">
        <asp:UpdatePanel runat="server" ID="updatePanel">
            <ContentTemplate>
                <asp:TreeView ID="tvCategories" runat="server" ForeColor="#656565" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <div class="categories-modal-footer">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="SuspendLeaveConfirmation();">
                    <asp:Image runat="server" ImageUrl="~/Images/btn_Save_up.png" onmouseover='this.src="Images/btn_Save_over.png";'
                        onmouseout='this.src="Images/btn_Save_up.png";' onmousedown='this.src="Images/btn_Save_down.png";'
                        onmouseup='this.src="Images/btn_Save_up.png";' />
                </asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="false">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/btn_Cancel_up.png" onmouseover='this.src="Images/btn_Cancel_over.png";'
                onmouseout='this.src="Images/btn_Cancel_up.png";' onmousedown='this.src="Images/btn_Cancel_Down.png";'
                onmouseup='this.src="Images/btn_Cancel_up.png";' />
        </asp:LinkButton>
    </div>
</asp:Panel>
<asp:Button ID="btnHidden1" runat="server" Style="display: none;" />
<ajaxToolkit:ModalPopupExtender BehaviorID="categories" ID="mpeCategories" runat="server"
    RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="btnHidden1"
    PopupControlID="Panel1" BackgroundCssClass="modalBackground2" CancelControlID="btnCancel"
    DropShadow="true" PopupDragHandleControlID="pnlTitle" />

<script type="text/javascript">
    function showCategories() {
        var modal = $find("categories");
        modal.show();
    }

    function openCategoriesModal() {
        var btnHidden = document.getElementById('<%=btnHidden1.ClientID %>');
        btnHidden.click();
    }


</script>

