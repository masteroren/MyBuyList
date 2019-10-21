<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecipeDetailsActions.ascx.cs" Inherits="UserControls_RecipeDetailsActions" %>

<%--<style>
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
</style>--%>

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
        <li class="on-login">
            <div class="add-to-favorites" style="display: none">
                <asp:LinkButton ID="btnAddRecipeToFavorites" runat="server" Text='הוסף למועדפים שלי'
                    OnClick="btnAddRecipeToFavorites_Click" />
            </div>
            <div class="remove-from-favorites" style="display: none">
                <asp:LinkButton ID="btnRemoveRecipeFromFavorites" runat="server"
                    Text='הסר ממועדפים שלי' OnClick="btnRemoveRecipeFromFavorites_Click" />
            </div>
        </li>
        <li class="on-login">
            <asp:HyperLink ID="btnRecipe" runat="server" Target="print" Text='הדפס' />
        </li>
        <li class="on-login">
            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'></asp:HyperLink>
        </li>
        <li class="on-login">
            <asp:LinkButton ID="btnCopyRecipe" runat="server" Text='<%$ Resources:MyGlobalResources, CopyRecipe %>' />
        </li>
        <li class="on-login">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%$ Resources:MyGlobalResources, Delete %>'></asp:LinkButton>
        </li>
    </ul>
    <asp:Label ID="lblResult" runat="server" Text="" Visible="false"></asp:Label>
</div>
