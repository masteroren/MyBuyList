using System;

public partial class UserControls_drawer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ShowRecipeActions(bool value, int recipeOwner = 0, int recipeId = 0)
    {
        RecipeDetailsActions.Visible = value;
        RecipeDetailsActions.RecipeOwnerID = recipeOwner;
        RecipeDetailsActions.RecipeId = recipeId;
    }
}