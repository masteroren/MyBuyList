using MyBuyList.Shared;
using MyBuyListShare.Classes;
using System;
using System.Threading.Tasks;
using System.Web;

public partial class UserControls_RecipeDetailsActions : System.Web.UI.UserControl
{
    private int recipeId;
    public int RecipeId
    {
        set
        {
            recipeId = value;
            HyperLink1.NavigateUrl = string.Format("~/RecipeEdit.aspx?recipeId={0}", value);
        }
        get
        {
            return recipeId;
        }
    }

    public int RecipeOwnerID;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo userInfo = (UserInfo)HttpContext.Current.Session[AppConstants.CURR_USER];
        bool me = userInfo != null && userInfo.UserId == RecipeOwnerID;
        PlaceHolder1.Visible = me;
        PlaceHolder2.Visible = me;
        PlaceHolder3.Visible = me;
    }

    protected void blkAddRemove_Click(object sender, EventArgs e)
    {
    }

    protected void btnAddRecipeToFavorites_Click(object sender, EventArgs e)
    {

    }

    protected void btnRemoveRecipeFromFavorites_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveAs_Click1(object sender, EventArgs e)
    {

    }

    protected void btnCopyRecipe_Click(object sender, EventArgs e)
    {

    }

    // Delete Recipe
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Task<string> t = HttpHelper.Delete(string.Format("recipes/{0}", RecipeId));
        t.Wait();
        Response.Redirect("Recipes.aspx");
    }
}