<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecipeDetailsActions.ascx.cs" Inherits="UserControls_RecipeDetailsActions" %>

<style>
    .remove {
        cursor: pointer;
    }

    ul.actions {
        list-style-type: none;
        display: flex;
        margin-right: 0;
        padding-right: 0;
    }

        ul.actions li a {
            color: #666666;
            text-decoration: none;
        }

        ul.actions li span {
            margin: 0 5px;
        }

        ul.actions li.hide-on-logout {
            display: none;
        }
</style>

<div class="recipe-actions">

    <%--<asp:LinkButton
        ID="blkAddRemove"
        runat="server"
        OnClientClick="StartHeaderInterval();"
        OnClick="blkAddRemove_Click"
        Font-Bold="true"
        ForeColor="#a4cb3a" />

    <asp:Label ID="lblAddRemoveSeperator" runat="server">|</asp:Label>--%>

    <ul class="actions">
        <li>
            <asp:LinkButton ID="btnSaveAs" runat="server" OnClick="btnSaveAs_Click1">שמור כתמונה</asp:LinkButton>
        </li>
        <li class="hide-on-logout">
            <div class="add-to-favorites" style="display: none">
                <asp:Label ID="lblAddToFavoritesSeparator" runat="server">|</asp:Label>
                <asp:LinkButton ID="btnAddRecipeToFavorites" runat="server" Text='הוסף למועדפים שלי'
                    OnClick="btnAddRecipeToFavorites_Click" />
            </div>
            <div class="remove-from-favorites" style="display: none">
                <asp:Label ID="Label2" runat="server">|</asp:Label>
                <asp:LinkButton ID="btnRemoveRecipeFromFavorites" runat="server"
                    Text='הסר ממועדפים שלי' OnClick="btnRemoveRecipeFromFavorites_Click" />
            </div>
        </li>
        <li class="hide-on-logout">
            <asp:Label ID="Label1" runat="server">|</asp:Label>
            <asp:HyperLink ID="btnRecipe" runat="server" Target="print" Text='הדפס' />
        </li>
        <li class="hide-on-logout">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <asp:Label ID="lblSeparator3" runat="server">|</asp:Label>
                <asp:HyperLink ID="HyperLink1" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'></asp:HyperLink>
                <asp:Label ID="lblEditRecipeDisabled" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
                    Font-Underline="true" ForeColor="LightGray" Visible="false" />
            </asp:PlaceHolder>
        </li>
        <li class="hide-on-logout">
            <asp:PlaceHolder ID="PlaceHolder3" runat="server">
                <asp:Label ID="lblCopyRecipeSeperator" runat="server">|</asp:Label>
                <asp:LinkButton ID="btnCopyRecipe" runat="server" Text='<%$ Resources:MyGlobalResources, CopyRecipe %>' />
            </asp:PlaceHolder>
        </li>
        <li class="hide-on-logout">
            <asp:PlaceHolder ID="PlaceHolder2" runat="server">
                <asp:Label ID="Label3" runat="server">|</asp:Label>
                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'></asp:LinkButton>
                <%--<a id="removeRecipe" class="remove">מחיקה</a>--%>
                <asp:Label ID="lblDeleteRecipeDisabled" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
                    Font-Underline="true" ForeColor="LightGray" Visible="false" />
            </asp:PlaceHolder>
        </li>
    </ul>
    <asp:Label ID="lblResult" runat="server" Text="" Visible="false"></asp:Label>
</div>
