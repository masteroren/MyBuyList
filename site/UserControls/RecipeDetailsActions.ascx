<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecipeDetailsActions.ascx.cs" Inherits="UserControls_RecipeDetailsActions" %>

<style>
    .remove{
        cursor: pointer;
    }
</style>

<div id="recipe_actions">

    <asp:LinkButton 
        ID="blkAddRemove" 
        runat="server" 
        OnClientClick="StartHeaderInterval();" 
        OnClick="blkAddRemove_Click" 
        Font-Bold="true"
        ForeColor="#a4cb3a" />

    <asp:Label ID="lblAddRemoveSeperator" runat="server">|</asp:Label>

    <asp:LinkButton ID="btnAddRecipeToFavorites" runat="server" Text='הוסף למועדפים שלי'
        OnClick="btnAddRecipeToFavorites_Click" />

    <asp:LinkButton ID="btnRemoveRecipeFromFavorites" runat="server" ForeColor="Red"
        Text='הסר ממועדפים שלי' OnClick="btnRemoveRecipeFromFavorites_Click" />

    <asp:Label ID="lblAddToFavoritesSeparator" runat="server">|</asp:Label>

    <asp:HyperLink ID="btnRecipe" runat="server" Target="print" Text='הדפס' />

    <asp:Label ID="Label1" runat="server">|</asp:Label>

    <asp:LinkButton ID="btnSaveAs" runat="server" OnClick="btnSaveAs_Click1">שמור כתמונה</asp:LinkButton>

    <asp:Label ID="lblEditRecipeSeparator" runat="server">|</asp:Label>

    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <a href="RecipeEdit.aspx?recipeId=<%=RecipeId %>">
            <asp:Literal ID="Literal1" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'></asp:Literal>
        </a>
    </asp:PlaceHolder>

    <asp:Label ID="lblEditRecipeDisabled" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
        Font-Underline="true" ForeColor="LightGray" Visible="false" />

    <asp:Label ID="lblSeparator3" runat="server" Visible="false">|</asp:Label>

    <asp:LinkButton ID="btnCopyRecipe" runat="server" Text='<%$ Resources:MyGlobalResources, CopyRecipe %>'
        OnClick="btnCopyRecipe_Click" Visible="false" />

    <asp:Label ID="lblCopyRecipeSeperator" runat="server">|</asp:Label>

    <a id="removeRecipe" class="remove">מחיקה</a>

    <asp:Label ID="lblDeleteRecipeDisabled" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
        Font-Underline="true" ForeColor="LightGray" Visible="false" />

    <asp:Label ID="lblResult" runat="server" Text="" Visible="false"></asp:Label>
</div>
