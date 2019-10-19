using MyBuyListShare;
using MyBuyListShare.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public delegate void ChangeEventHandler(object sender, ChangeEventArgs e);

public class ChangeEventArgs
{
    public string category;
    public SortBy sortBy;
}

public partial class UserControls_ucRecipesFilter : System.Web.UI.UserControl
{
    private string recipeCategoryChangeBaseUrl;

    public event ChangeEventHandler FilterChanged;

    public int Category { get; set; }

    protected virtual void OnFilterChanged(object sender, ChangeEventArgs e)
    {
        if (FilterChanged != null)
        {
            FilterChanged(this, e);
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

        if (lstCategories.SelectedIndex != 0)
        {
            Category = Convert.ToInt32(lstCategories.SelectedValue);
            OnFilterChange();
        }
    }

    protected void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnFilterChange();
    }

    private void OnFilterChange()
    {
        OnFilterChanged(this, new ChangeEventArgs
        {
            category = lstCategories.SelectedIndex == 0 ? null : lstCategories.SelectedValue,
            sortBy = (SortBy)(Convert.ToInt32(ddlSortBy.SelectedValue))
        });
    }

    public void FillList(IEnumerable<CategoryModel> categoryList)
    {
        if (categoryList == null) { return; }

        lstCategories.Items.Clear();
        lstCategories.Items.Add(new ListItem("בחר קטגוריה"));

        foreach (CategoryModel category in categoryList)
        {
            string Text = string.Format("{0} ({1})", category.CategoryName, category.recipes);
            lstCategories.Items.Add(new ListItem(Text, category.CategoryId.ToString()));
        }
    }

    protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SRL_Category category = e.Item.DataItem as SRL_Category;
        if (category != null)
        {
            //LinkButton btn = e.Item.FindControl("btnCategory") as LinkButton;
            //btn.Text += " (" + category.RecipesCount + ")";
            HyperLink lnk = (HyperLink)e.Item.FindControl("lnkCategory");
            lnk.Text = string.Format("{0} ({1})", category.CategoryName, category.RecipesCount);
            //lnk.NavigateUrl = string.Format(this.RecipeCategoryChangeBaseUrl, category.CategoryId);
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

    protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnFilterChange();
    }
}