<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddRecipeButton.ascx.cs" Inherits="UserControls_AddRecipeButton" %>

<!-- Add recipe button control -->
<script>
    var lnkNewRecipeClientId = '<%=lnkNewRecipe.ClientID%>';

    $(document).ready(() => {
        registerToLoginNotifications((loginNotification) => {
            if (loginNotification.loggedIn) {
                $('.add-recipe').show();
            } else {
                $('.add-recipe').hide();
            }
        });
    });
</script>

<style>
    .add-recipe {
        /*padding-left: 20px;*/
    }
</style>

<div class="add-recipe on-login">
    <asp:HyperLink ID="lnkNewRecipe" runat="server" NavigateUrl="~/RecipeEdit.aspx">
                                <asp:Image runat="server" ImageUrl="~/Images/btn_AddNewRecipe_up.png" onmouseover='this.src="Images/btn_AddNewRecipe_over.png";' 
                                    onmouseout='this.src="Images/btn_AddNewRecipe_up.png";' onmousedown='this.src="Images/btn_AddNewRecipe_Down.png";' 
                                    nmouseup='this.src="Images/btn_AddNewRecipe_up.png";' />
    </asp:HyperLink>
</div>
<!-- Add recipe button control -->
