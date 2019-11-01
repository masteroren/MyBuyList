using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using MyBuyListShare;
using MyBuyListShare.Models;
using MyBuyListShare.Services;
using ProperControls.General;
using ProperServices.Common.Extensions;
using ProperServices.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Recipes : BasePage
{
    #region Properties

    public int CurrentPage;

    public RecipeOrderEnum OrderBy
    {
        get
        {
            RecipeOrderEnum orderBy;

            if (!string.IsNullOrEmpty(Request["orderBy"]))
            {
                try
                {
                    orderBy = (RecipeOrderEnum)Enum.Parse(typeof(RecipeOrderEnum), Request["orderBy"], true);
                    return orderBy;
                }
                catch
                {
                    return RecipeOrderEnum.LastUpdate;
                }
            }
            else
            {
                return RecipeOrderEnum.LastUpdate;
            }
        }
    }

    public RecipeDisplayEnum Display { get; set; }

    public string FreeText
    {
        get { return (!string.IsNullOrEmpty(Request["term"]) ? Request["term"] : null); }
    }

    private int? categoryId;
    private SortBy sortBy;

    public int? Servings
    {
        get
        {
            int servings = 0;

            if (!string.IsNullOrEmpty(Request["serv"]) && int.TryParse(Request["serv"], out servings))
            {
                return servings;
            }
            else
            {
                return null;
            }
        }
    }

    public int[] RecipeCategories
    {
        get
        {
            if (!string.IsNullOrEmpty(Request["categories"]))
            {
                return this.GetCategoryIdsFromString(Request["categories"]);
            }
            else
            {
                return null;
            }
        }
    }

    public int PageSize { get { return 12; } }

    public int totalPages;

    protected string RecipeCategoryChangeBaseUrl
    {
        get
        {
            EnsureRecipeCategoryChangeBaseUrlExists();

            return this.recipeCategoryChangeBaseUrl;
        }
    }

    protected string OrderByLastUpdateUrl
    {
        get
        {
            EnsureUrlsForSortingAreGenerated();
            return orderByLastUpdateUrl;
        }
    }

    protected string OrderByNameUrl
    {
        get
        {
            EnsureUrlsForSortingAreGenerated();
            return orderByNameUrl;
        }
    }

    protected string OrderByPublisherUrl
    {
        get
        {
            EnsureUrlsForSortingAreGenerated();
            return orderByPublisherUrl;
        }
    }
    #endregion

    bool urlsGenerated = false;

    private string orderByLastUpdateUrl;
    private string orderByNameUrl;
    private string orderByPublisherUrl;

    private string recipeCategoryChangeBaseUrl;

    private IDisposable searchSubscription;

    protected void Page_Load(object sender, EventArgs e)
    {
        RebindRecipes();
    }

    public void UcRecipesFilter_FilterChanged(object sender, ChangeEventArgs e)
    {
        CurrentPage = 1;
        sortBy = e.sortBy;
        ViewState["SortBy"] = e.sortBy;

        if (e.category == null)
        {
            Display = RecipeDisplayEnum.All;
        }
        else
        {
            Display = RecipeDisplayEnum.ByCategory;
            categoryId = Convert.ToInt32(e.category);
            ViewState["Category"] = categoryId;
            ViewState["page"] = 0;
        }

        RebindRecipes();
    }

    private void RebindRecipes()
    {
        int userId = -1;

        if (CurrUser != null && CurrUser.UserId != -1)
        {
            userId = CurrUser.UserId;
        }

        RecipesResults recipesResults = null;

        int PageIndex = ViewState["page"] == null ? 0 : (int)ViewState["page"];
        int RecipeOwner = ViewState["RecipeOwner"] == null ? -1 : Convert.ToInt32(ViewState["RecipeOwner"]);
        int CategoryId = ViewState["Category"] == null ? -1 : Convert.ToInt32(ViewState["Category"]);
        int SortBy = ViewState["SortBy"] == null ? -1 : Convert.ToInt32(ViewState["SortBy"]);

        if (CategoryId == -1 && RecipeOwner == -1)
        {
            recipesResults = HttpHelper.Get<RecipesResults>(string.Format("recipes?pageIndex={0}&sortBy={1}", PageIndex, SortBy));
        }

        if (CategoryId == -1 && RecipeOwner != -1)
        {
            recipesResults = HttpHelper.Get<RecipesResults>(string.Format("users/{0}/recipes?pageIndex={1}&sortBy={2}", RecipeOwner, PageIndex, SortBy));
        }

        if (CategoryId == -1 && RecipeOwner != -1)
        {
            recipesResults = HttpHelper.Get<RecipesResults>(string.Format("users/{0}/recipes?pageIndex={1}&sortBy={2}", RecipeOwner, PageIndex, SortBy));
        }

        if (CategoryId != -1 && RecipeOwner == -1)
        {
            recipesResults = HttpHelper.Get<RecipesResults>(string.Format("categories/{0}/recipes?pageIndex={1}&sortBy={2}", CategoryId, PageIndex, SortBy));
        }

        if (CategoryId != -1 && RecipeOwner != -1)
        {
            recipesResults = HttpHelper.Get<RecipesResults>(string.Format("users/{0}/categories/{1}/recipes?pageIndex={2}&sortBy={3}", RecipeOwner, CategoryId, PageIndex, SortBy));
        }

        if (recipesResults != null)
        {
            totalPages = recipesResults.metadata.pages;
            lblNumRecipes.Text = string.Format("נמצאו {0} מתכונים", recipesResults.metadata.totalItems);

            rptRecipes.DataSource = recipesResults.results;
            rptRecipes.DataBind();
        }

    }

    public void rptRecipes_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        int pageIndex = ViewState["page"] == null ? 0 : (int)ViewState["page"];

        if (e.Item.ItemType == ListItemType.Header || e.Item.ItemType == ListItemType.Footer)
        {
            PlaceHolder phPager = (PlaceHolder)e.Item.FindControl("phPager");

            bool isEnd = false, isStart = false;

            int startPage = 10 * (pageIndex/ 10) + 1;
            int endpage = 10 * (pageIndex/ 10) + 10;

            if (startPage == 1)
            {
                isStart = true;
            }
            if (endpage >= totalPages)
            {
                isEnd = true;
                endpage = totalPages;
            }

            if (!isStart)
            {
                LinkButton lnkArrow = new LinkButton();
                lnkArrow.CommandArgument = (startPage - 11).ToString();
                lnkArrow.Click += LnkPage_Click;

                lnkArrow.Font.Size = FontUnit.Larger;
                lnkArrow.Text = "&lt;&lt;";
                phPager.Controls.Add(lnkArrow);
                phPager.Controls.Add(new LiteralControl("&nbsp;"));
            }

            for (int page = startPage; page <= endpage; page++)
            {
                if (page == pageIndex + 1)
                {
                    Label lblPage = new Label();
                    lblPage.Font.Size = FontUnit.Large;
                    lblPage.Font.Underline = false;
                    lblPage.Font.Bold = true;
                    lblPage.Text = page.ToString();
                    phPager.Controls.Add(lblPage);
                }
                else
                {
                    LinkButton lnkPage = new LinkButton();
                    lnkPage.CommandArgument = (page - 1).ToString();
                    lnkPage.Click += LnkPage_Click;

                    lnkPage.Font.Size = FontUnit.Larger;
                    lnkPage.Font.Bold = true;
                    lnkPage.Text = page.ToString();
                    phPager.Controls.Add(lnkPage);
                }

                phPager.Controls.Add(new LiteralControl("&nbsp;"));
            }

            if (!isEnd)
            {
                LinkButton lnkArrow = new LinkButton();
                lnkArrow.CommandArgument = (startPage + 9).ToString();
                lnkArrow.Click += LnkPage_Click;

                lnkArrow.Font.Size = FontUnit.Larger;
                lnkArrow.Text = "&gt;&gt;";
                phPager.Controls.Add(lnkArrow);
            }
        }
    }

    private void LnkPage_Click(object sender, EventArgs e)
    {
        int page = Convert.ToInt32((sender as LinkButton).CommandArgument);
        ViewState["page"] = page;
        RebindRecipes();
    }

    public void rptRecipes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RecipeModel recipe = e.Item.DataItem as RecipeModel;

        if (recipe != null && e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //LinkButton linkBtn = e.Item.FindControl("blkAddRemove") as LinkButton;
            //if (linkBtn != null)
            //{
            //    linkBtn.Attributes["id"] = recipe.id.ToString();
            //}

            bool inShoppingList = false;

            if (CurrUser != null)
                inShoppingList = Utils.SelectedRecipes.SingleOrDefault(r => r.Key == recipe.id).Value != null;

            LinkButton linkBtnShoppingList = e.Item.FindControl("ShoppingListAddRemove") as LinkButton;
            if (linkBtnShoppingList != null)
            {
                linkBtnShoppingList.Attributes["id"] = recipe.id.ToString();
                linkBtnShoppingList.Text = inShoppingList ? "הסר מרשימת הקניות" : "הוסף לרשימת הקניות";
                linkBtnShoppingList.Style["color "] = inShoppingList ? "Red" : "#656565";
                linkBtnShoppingList.CssClass += inShoppingList ? " remove-recipe" : " add-recipe";
            }

            //LinkButton linkBtn = e.Item.FindControl("btnSendToFriend") as LinkButton;
            //if (linkBtn != null)
            //{
            //    linkBtn.Attributes["recipeId"] = recipe.id.ToString();
            //}

            HtmlGenericControl divMyFavoritesInfoTag = e.Item.FindControl("myFavoritesInfoTag") as HtmlGenericControl;

            bool inMyFavorites = false;
            inMyFavorites = false;
            //if (recipe.users != null && CurrUser != null)
            //    inMyFavorites = recipe.users.SingleOrDefault(ufr => ufr.UserId == CurrUser.UserId) != null;
            divMyFavoritesInfoTag.Visible = inMyFavorites;

            Label lblAllFavorites = e.Item.FindControl("lblAllFavorites") as Label;
            Label lblAllMenus = e.Item.FindControl("lblAllMenus") as Label;

            Label mainCategor = e.Item.FindControl("lblMainCategory") as Label;
            //List<ShortCategoryModel> recipeCategories = (from a in recipe.categories select new categories { CategoryName = a }).ToArray();
            if (recipe.categories.Count > 0)
            {
                mainCategor.Text = recipe.categories[0].categoryName;
            }

            Image image = e.Item.FindControl("imgThumbnail") as Image;
            if (recipe.picture == null)
            {
                image.ImageUrl = "~/Images/Img_Default_small.jpg";
            }
            else
            {
                //image.ImageUrl = recipe.picture;
            }


            //lblAllFavorites.Text = ((Recipe)e.Item.DataItem).NumUsersFavorite.ToString();
            //lblAllMenus.Text = ((Recipe)e.Item.DataItem).NumMenusInclude.ToString();
        }
    }

    protected void blkAddRemove_click(object sender, EventArgs e)
    {
        //try
        //{
        //    LinkButton linkBtn = sender as LinkButton;
        //    int recipeId;
        //    string preStr = "";

        //    Logger.Write("Add/Remove Recipe -> Start", Logger.Level.Info);

        //    if (int.TryParse(linkBtn.Attributes["recipeID"], out recipeId))
        //    {
        //        Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
        //        if (recipe != null)
        //        {
        //            Logger.Write(string.Format("Add/Remove Recipe -> Reciepe Id {0}", recipe.RecipeId), Logger.Level.Info);

        //            Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;
        //            if (selectedRecipes.Keys.Contains(recipe.RecipeId))
        //            {
        //                selectedRecipes.Remove(recipeId);
        //                Utils.SelectedRecipes = selectedRecipes;
        //                preStr = "מרשימת הקניות / תפריט הוסר המתכון:";

        //                if (CurrUser != null)
        //                {
        //                    BusinessFacade.Instance.RemoveRecipeFromShoppingList(CurrUser.UserId, recipeId);
        //                }

        //                Logger.Write(string.Format("Add/Remove Recipe -> Recipe {0} removed", recipe.RecipeId), Logger.Level.Info);
        //            }
        //            else
        //            {
        //                selectedRecipes.Add(recipeId, recipe);
        //                Utils.SelectedRecipes = selectedRecipes;
        //                preStr = "לרשימת הקניות / תפריט התווסף המתכון:";

        //                if (CurrUser != null)
        //                {
        //                    BusinessFacade.Instance.AddRecipeToShoppingList(CurrUser.UserId, recipeId);
        //                }

        //                Logger.Write(string.Format("Add/Remove Recipe -> Recipe {0} added", recipe.RecipeId), Logger.Level.Info);
        //            }

        //            ((BasePage)Page).DisplayMessage = preStr + "<br/>" + recipe.RecipeName;

        //            ucShoppingList1.UpdateList();
        //            upShoppingList.Update();
        //        }
        //    }
        //}
        //catch(Exception ex)
        //{
        //    Logger.Write("Add/Remove Recipe failed", ex, Logger.Level.Error);
        //}
    }

    //protected void btnSendToFriend_Click(object sender, EventArgs e)
    //{
    //    LinkButton linkBtn = sender as LinkButton;
    //    int recipeId;

    //    if (linkBtn != null && int.TryParse(linkBtn.Attributes["recipeID"], out recipeId))
    //    {
    //        Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);

    //        string userEmail = string.Empty;

    //        MyBuyList.Shared.Entities.User currentUser = BusinessFacade.Instance.GetUser(((BasePage)this.Page).UserId);

    //        if (currentUser != null)
    //        {
    //            userEmail = currentUser.Email;
    //        }

    //        this.ucSendMailToFriend.BindItemDetails("Recipe", recipe.RecipeId, recipe.RecipeName, userEmail);

    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "popupSendToFriend", "showSendMailToFriendBox()", true);
    //    }        
    //}

    protected void ucSendMailToFriend_EmailSent(object sender, SendToFriendEventArgs e)
    {
        ((BasePage)this.Page).DisplayMessage = "אימייל נשלח ל- " + e.recipentName;
    }

    //private void BindInfoTags(RepeaterItem item)
    //{
    //    RecipeView recipeView = item.DataItem as RecipeView;

    //    Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeView.RecipeId);

    //    HtmlGenericControl divMyFavoritesInfoTag = item.FindControl("myFavoritesInfoTag") as HtmlGenericControl;
    //    Label lblAllFavorites = item.FindControl("lblAllFavorites") as Label;
    //    Label lblAllMenus = item.FindControl("lblAllMenus") as Label;

    //    if (recipe != null)
    //    {
    //        Recipe[] ufRecipes = BusinessFacade.Instance.GetUserFavoritesRecipes(((BasePage)Page).UserId);
    //        Recipe inMyFavorites = ufRecipes.SingleOrDefault(fr => fr.RecipeId == recipe.RecipeId);
    //        if (inMyFavorites == null)
    //        {
    //            divMyFavoritesInfoTag.Visible = false;
    //        }
    //        else
    //        {
    //            divMyFavoritesInfoTag.Visible = true;
    //        }
    //    }

    //    int? allUsersFavorite = BusinessFacade.Instance.GetRecipeUserFavoritesCount(recipe.RecipeId);
    //    if (allUsersFavorite != null)
    //    {
    //        lblAllFavorites.Text = allUsersFavorite.ToString();
    //    }

    //    int? allMenus = BusinessFacade.Instance.GetRecipeMenusCount(recipe.RecipeId);
    //    if (allMenus != null)
    //    {
    //        lblAllMenus.Text = allMenus.ToString();
    //    }
    //}

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        //this.btnSearch.CommandArgument = "BySearchSimple";

        //this.simpleSearch.Visible = true;
        //this.btnSearch.Visible = true;
        //this.advancedSearch.Visible = false;
        //this.categories.Visible = false;
        //EmphasizeCurrentSearch(RecipeDisplayEnum.BySearchSimple);
        //this.upSearch.Update();
    }

    protected void lnkAdvancedSearch_Click(object sender, EventArgs e)
    {
        //this.btnSearch.CommandArgument = "BySearchAdvanced";

        //this.simpleSearch.Visible = true;
        //this.btnSearch.Visible = true;
        //this.advancedSearch.Visible = true;
        //this.categories.Visible = false;
        //EmphasizeCurrentSearch(RecipeDisplayEnum.BySearchAdvanced);
        //this.upSearch.Update();
    }

    //protected void btnSearch_Command(object sender, CommandEventArgs e)
    //{
    //    string categoryIDs = "";

    //    if (e.CommandArgument.ToString() == "BySearchAdvanced")
    //    {
    //        categoryIDs = this.hdnCategorieIDs.Value;
    //    }

    //    //Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}{1}{2}{3}", e.CommandArgument.ToString(), (!string.IsNullOrEmpty(this.txtSearchTerm.Text)) ? string.Format("&term={0}", this.txtSearchTerm.Text) : "", (!string.IsNullOrEmpty(this.txtServings.Text)) ? string.Format("&serv={0}", this.txtServings.Text) : "", (!string.IsNullOrEmpty(categoryIDs)) ? string.Format("&categories={0}", categoryIDs) : ""));
    //}

    protected void lnkAllRecipes_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Recipes.aspx");
    }

    protected void lnkMyRecipes_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyRecipes.ToString()));
    }

    protected void lnkFavRecipes_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyFavoriteRecipes.ToString()));
    }

    protected void lnkCategories_Click(object sender, EventArgs e)
    {
        this.RebindCategories();
        //this.pnlCategories.Visible = true;

        //this.simpleSearch.Visible = false;
        //this.btnSearch.Visible = false;
        //this.advancedSearch.Visible = false;
        //this.categories.Visible = true;
        //EmphasizeCurrentSearch(RecipeDisplayEnum.ByCategory);
        //this.upSearch.Update();
    }

    private int RebindCategories()
    {
        int count = 0;
        var categories = HttpHelper.Get<ListResponse<IEnumerable<CategoryModel>>>("categories");
        //categories[] categories = BusinessFacade.Instance.GetRecipesCategoriesList();
        //var list = from cat in categories.Where(cat => cat.ParentCategoryId == categoryId).ToArray()
        //           select new SRL_Category(cat.CategoryId, cat.CategoryName, cat.ParentCategoryId, cat.recipes.Count());
        //this.rptCategories.DataSource = list.ToArray();
        //this.rptCategories.DataBind();
        //count = list.ToArray().Length;

        List<CategoryModel> pathList = new List<CategoryModel>();
        if (categoryId != null)
        {
            CategoryModel tmpCategory = categories.results.SingleOrDefault(ct => ct.CategoryId == categoryId.Value);

            do
            {
                pathList.Add(tmpCategory);
                if (tmpCategory.ParentCategoryId == null)
                    break;
                //tmpCategory = tmpCategory.Categories1.SingleOrDefault(x => x.ParentCategoryId == tmpCategory.ParentCategoryId);
            }
            while (tmpCategory != null);
        }

        //LinkButton primaryCatLink = new LinkButton();
        //primaryCatLink.Text = "הכל";
        //primaryCatLink.Style["background-color"] = "#A4CB3A";
        //primaryCatLink.Click += new EventHandler(primaryCatLink_Click);

        HyperLink primaryCatLink = new HyperLink();
        primaryCatLink.Text = "הכל";
        primaryCatLink.Style["background-color"] = "#A4CB3A";
        primaryCatLink.NavigateUrl = string.Format("~/Recipes.aspx?disp={0}{1}", RecipeDisplayEnum.ByCategory.ToString(), (Request["orderBy"] != null) ? string.Format("&orderBy={0}", Request["orderBy"]) : "");
        //this.pathLinks.Controls.Clear();
        //this.pathLinks.Controls.Add(primaryCatLink);

        pathList.Reverse();
        foreach (CategoryModel category in pathList)
        {
            Label arrows = new Label();
            arrows.Text = " >> ";
            arrows.Style["background-color"] = "#A4CB3A";
            //this.pathLinks.Controls.Add(arrows);

            HyperLink link = new HyperLink();
            link.Text = category.CategoryName;
            link.Style["background-color"] = "#A4CB3A";
            link.NavigateUrl = string.Format(this.RecipeCategoryChangeBaseUrl, category.CategoryId);
            //link.Command += new CommandEventHandler(link_Command);
            //this.pathLinks.Controls.Add(link);
        }

        return count;
    }

    private void EnsureRecipeCategoryChangeBaseUrlExists()
    {
        if (!string.IsNullOrEmpty(this.recipeCategoryChangeBaseUrl))
        {
            return;
        }

        string currentUrl = Request.Url.OriginalString;

        if (currentUrl.ToLower().Contains("page"))
        {
            this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Replace(Request["page"].ToLower(), "1");
            currentUrl = this.recipeCategoryChangeBaseUrl;
        }

        if (currentUrl.ToLower().Contains("disp"))
        {
            if (!currentUrl.ToLower().Contains("disp=&"))
            {
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Replace(Request["disp"].ToLower(), RecipeDisplayEnum.ByCategory.ToString());
                currentUrl = this.recipeCategoryChangeBaseUrl;
            }
            else  //should not happen - make sure and remove          
            {
                int index = currentUrl.ToLower().IndexOf("disp=&") + 5;
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Insert(index, RecipeDisplayEnum.ByCategory.ToString());
                currentUrl = this.recipeCategoryChangeBaseUrl;
            }
        }
        else
        {
            this.recipeCategoryChangeBaseUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "disp=" + RecipeDisplayEnum.ByCategory.ToString();
            currentUrl = this.recipeCategoryChangeBaseUrl;
        }

        if (currentUrl.ToLower().Contains("cat="))
        {
            if (!currentUrl.ToLower().Contains("cat=&"))
            {
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Replace("cat=" + Request["cat"].ToLower(), "cat={0}");
            }
            else //should not happen - make sure and remove
            {
                int index = currentUrl.ToLower().IndexOf("cat=&") + 4;
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Insert(index, "{0}");
            }
        }
        else
        {
            this.recipeCategoryChangeBaseUrl = currentUrl + "&cat={0}";
        }
    }

    //protected void btnCategory_Command(object sender, CommandEventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}&cat={1}", RecipeDisplayEnum.ByCategory, e.CommandArgument.ToString()));
    //}

    protected void primaryCatLink_Click(object sender, EventArgs e)
    {
        this.RebindCategories();

        //this.simpleSearch.Visible = false;
        //this.btnSearch.Visible = false;
        //this.advancedSearch.Visible = false;
        //this.categories.Visible = true;
        //this.upSearch.Update();
    }

    protected void link_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}&cat={1}", RecipeDisplayEnum.ByCategory, e.CommandArgument.ToString()));
    }

    protected void ucRecipeCats_RefreshData(SRL_RecipeCategory[] arr)
    {
        string strIDs = "";
        string strNames = "";
        foreach (SRL_RecipeCategory cat in arr)
        {
            strIDs += cat.CategoryId.ToString() + ",";
            strNames += cat.CategoryName + ", ";
        }

        //this.hdnCategorieIDs.Value = strIDs.Substring(0, strIDs.Length - 1);
        //this.txtCategory.Text = strNames.Substring(0, strNames.Length - 2);
        //this.upSearch.Update();
    }

    private int[] GetCategoryIdsFromString(string str)
    {
        string[] tmpArray = str.Split(new char[1] { ',' });
        int[] ids = new int[tmpArray.Length];
        bool allSucceeded = true;
        for (int i = 0; i < tmpArray.Length; i++)
        {
            int x;
            if (int.TryParse(tmpArray[i], out x))
            {
                ids[i] = x;
            }
            else
            {
                allSucceeded = false;
            }
        }
        if (allSucceeded)
        {
            return ids;
        }
        else
        {
            return null;
        }
    }

    //private void EmphasizeCurrentSearch(RecipeDisplayEnum currentDisplay)
    //{
    //    ReciepesSearchControl1.EmphasizeCurrentSearch(currentDisplay);
    //}

    /// <summary>
    /// this function creates the relevant urls for sort changes
    /// </summary>
    private void EnsureUrlsForSortingAreGenerated()
    {
        if (urlsGenerated)
            return;

        string currentUrl = Request.Url.OriginalString;

        // replace orderby
        if (currentUrl.ToLower().Contains("orderby"))
        {
            orderByLastUpdateUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), "LastUpdate");
            orderByNameUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), "Name");
            orderByPublisherUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), "Publisher");
        }
        else
        {
            orderByLastUpdateUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "orderBy=LastUpdate";
            orderByNameUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "orderBy=Name";
            orderByPublisherUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "orderBy=Publisher";
        }

        urlsGenerated = true;
    }

    //protected string OrderByLastUpdateUrl
    //{
    //    get
    //    {
    //        string currentUrl = Request.Url.OriginalString;
    //        string targetSortName = "LastUpdate";
    //        return GetSortUrl(currentUrl, targetSortName);
    //    }
    //}

    //protected string OrderByNameUrl
    //{
    //    get
    //    {
    //        string currentUrl = Request.Url.OriginalString;
    //        string targetSortName = "Name";
    //        return GetSortUrl(currentUrl, targetSortName);
    //    }
    //}

    //protected string OrderByPublisherUrl
    //{
    //    get
    //    {
    //        string currentUrl = Request.Url.OriginalString;
    //        string targetSortName = "Publisher";
    //        return GetSortUrl(currentUrl, targetSortName);
    //    }
    //}

    /// <summary>
    /// this function creates the relevant url for sort changes
    /// </summary>
    /// <param name="currentUrl"></param>
    /// <param name="targetSortName"></param>
    /// <returns></returns>
    //private string GetSortUrl(string currentUrl, string targetSortName)
    //{
    //    string resultUrl;

    //    // replace orderby
    //    if (currentUrl.ToLower().Contains("orderby"))
    //    {
    //        resultUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), targetSortName);
    //    }
    //    else if (currentUrl.ToLower().Contains("?"))
    //    {
    //        resultUrl = currentUrl + "&orderBy=" + targetSortName;
    //    }
    //    else
    //    {
    //        resultUrl = currentUrl + "?orderBy=" + targetSortName;
    //    }

    //    return resultUrl;
    //}

    private string AdjustLength(string catName, int length)
    {
        if (catName.Length > length)
        {
            catName = catName.TrimToMax(length);
        }

        return catName;
    }
    protected void ButtonRecipesRefresh_Click(object sender, EventArgs e)
    {
        string preStr = string.Empty;
        int recipeId;
        int.TryParse(hfRecipeId.Value, out recipeId);
        string recipeName = hfRecipeName.Value;

        Dictionary<int, recipes> selectedRecipes = Utils.SelectedRecipes;
        if (selectedRecipes.ContainsKey(recipeId))
        {
            selectedRecipes.Remove(recipeId);
            Utils.SelectedRecipes = selectedRecipes;
            preStr = "מרשימת הקניות / תפריט הוסר המתכון:";

            if (CurrUser != null)
            {
                BusinessFacade.Instance.RemoveRecipeFromShoppingList(CurrUser.UserId, recipeId);
            }

            Logger.Write(string.Format("Add/Remove Recipe -> Recipe {0} removed", recipeName), Logger.Level.Info);
        }

        ((BasePage)Page).DisplayMessage = preStr + "<br/>" + recipeName;

        RebindRecipes();
        //upRecipes.Update();
        //ucShoppingList1.UpdateList();
        //upShoppingList.Update();
    }

    protected void lnkPublisher_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32((sender as LinkButton).CommandArgument);
        ViewState["RecipeOwner"] = userId;
        RebindRecipes();
    }
}
