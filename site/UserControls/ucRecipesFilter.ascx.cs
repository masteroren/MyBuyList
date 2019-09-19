using MyBuyListShare.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public delegate void ChangeEventHandler(object sender, ChangeEventArgs e);

public class ChangeEventArgs
{
    public string category;
    public int sortBy;
}

public partial class UserControls_ucRecipesFilter : System.Web.UI.UserControl
{
    private string recipeCategoryChangeBaseUrl;

    public event ChangeEventHandler CategoryChanged;
    public event ChangeEventHandler SortChanged;

    protected virtual void OnCategoryChanged(object sender, ChangeEventArgs e)
    {
        if (CategoryChanged != null)
        {
            CategoryChanged(this, e);
        }
    }

    protected virtual void OnSortChanged(object sender, ChangeEventArgs e)
    {
        if (SortChanged != null)
        {
            SortChanged(this, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //string currentDisplay = Request.QueryString["disp"] == string.Empty ? "All" : Request.QueryString["disp"];

        //lnkRecipes.Style["text-decoration"] = "none";
        //lnkMyRecipes.Style["text-decoration"] = "none";
        //lnkMyFavoriteRecipes.Style["text-decoration"] = "none";

        //switch (currentDisplay)
        //{
        //    case "All":
        //        lnkRecipes.Style["text-decoration"] = "underline";
        //        break;
        //    case "MyRecipes":
        //        lnkMyRecipes.Style["text-decoration"] = "underline";
        //        break;
        //    case "MyFavoriteRecipes":
        //        lnkMyFavoriteRecipes.Style["text-decoration"] = "underline";
        //        break;
        //}

        if (!IsPostBack)
        {
            var categories = HttpHelper.Get<ListResponse<IEnumerable<CategoryModel>>>("categories");
            FillList(categories.results);
        }
    }

    protected void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnCategoryChanged(this, new ChangeEventArgs
        {
            category = lstCategories.SelectedIndex == 0 ? null : lstCategories.SelectedValue
        });
    }

    public void FillList(IEnumerable<CategoryModel> categoryList)
    {
        lstCategories.Items.Clear();
        lstCategories.Items.Add(new ListItem("בחר קטגוריה"));

        foreach (CategoryModel category in categoryList)
        {
            string Text = string.Format("{0} ({1})", category.CategoryName, category.recipes);
            lstCategories.Items.Add(new ListItem(Text, category.CategoryId.ToString()));
        }
    }

    //protected void lnkAllMenus_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Recipes.aspx");
    //}

    //protected void lnkMyMenus_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyRecipes.ToString()));
    //}

    //protected void lnkFavMenus_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyFavoriteRecipes.ToString()));
    //}

    //public void EmphasizeCurrentSearch(RecipeDisplayEnum currentDisplay)
    //{
    //    this.lnkAllMenus.Style["text-decoration"] = "none";
    //    this.lnkMyMenus.Style["text-decoration"] = "none";
    //    this.lnkFavMenus.Style["text-decoration"] = "none";

    //    switch (currentDisplay)
    //    {
    //        case RecipeDisplayEnum.All:
    //            this.lnkAllMenus.Style["text-decoration"] = "underline";
    //            break;
    //        case RecipeDisplayEnum.MyRecipes:
    //            this.lnkMyMenus.Style["text-decoration"] = "underline";
    //            break;
    //        case RecipeDisplayEnum.MyFavoriteRecipes:
    //            this.lnkFavMenus.Style["text-decoration"] = "underline";
    //            break;
    //    }
    //}

    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp=BySearchSimple&term={0}", txtSearchTerm.Text));
    //}
}