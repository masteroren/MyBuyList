﻿//using ACAWebThumbLib;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using MyBuyListShare.Models;
using MyBuyListShare.Services;
using ProperControls.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class PageRecipeDetails : BasePage
{
    protected string FBUrl;
    protected int RecipeId;

    int ViewId
    {
        get { return ViewState["ViewId"] == null ? 0 : (int)ViewState["ViewId"]; }
        set { ViewState["ViewId"] = value; }
    }
    string menuId
    {
        get { return ViewState["menuId"] == null ? string.Empty : (string)ViewState["menuId"]; }
        set { ViewState["menuId"] = value; }
    }

    string lastPage
    {
        get { return ViewState["lastPage"] == null ? string.Empty : (string)ViewState["lastPage"]; }
        set { ViewState["lastPage"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        //AuthService.Instance.login.Subscribe(userInfo =>
        //{
        //    var userId = userInfo.UserId;
        //    RecipeDetailsActions.ApplyEdit = false;
        //    FavoritRecipesModel result = HttpHelper.Get<FavoritRecipesModel>(string.Format("users/{0}/recipes/{1}/favorites", userId, RecipeId));
        //    if (result != null)
        //    {
        //        RecipeDetailsActions.ApplyEdit = true;
        //    }
        //});

        //PlaceHolder1.Visible = false;
        //PlaceHolder2.Visible = false;

        //this.ucRecipe.RefreshData += new ucRecipe.RefreshHandler(this.RefreshPage);

        //SetDrives();
        //SetCategories();

        if (!IsPostBack)
        {
            RecipeId = int.Parse(Request["recipeId"]);
            hfRecipeId.Value = RecipeId.ToString();

            RecipeModel currRecipe = HttpHelper.Get<RecipeModel>(string.Format("recipes/{0}", RecipeId));
            Rebind(currRecipe);

            RecipeDetailsActions.RecipeId = RecipeId;
            RecipeDetailsActions.RecipeOwnerID = currRecipe.userId;

            RecipeDetailsActions2.RecipeId = RecipeId;
            RecipeDetailsActions2.RecipeOwnerID = currRecipe.userId;

            //FBUrl = Request.Url.AbsoluteUri;

            //if (string.IsNullOrEmpty(this.Request["recipeId"]))
            //{
            //    AppEnv.MoveToDefaultPage();
            //}

            //if (Request.UrlReferrer != null)
            //{
            //    int indexEnd;
            //    int indexStart;
            //    int length;

            //    if (Request.UrlReferrer.AbsoluteUri.Contains("menuId="))
            //    {
            //        indexEnd = Request.UrlReferrer.AbsoluteUri.IndexOf("&");
            //        indexStart = Request.UrlReferrer.AbsoluteUri.IndexOf("menuId=") + "menuId=".Length;
            //        length = indexEnd - indexStart;
            //        if (length > 0)
            //        {
            //            this.menuId = Request.UrlReferrer.AbsoluteUri.Substring(indexStart, length);

            //        }
            //        else
            //        {

            //            this.menuId = Request.UrlReferrer.AbsoluteUri.Substring(indexStart);
            //        }
            //    }

            //    indexStart = Request.UrlReferrer.AbsoluteUri.LastIndexOf("/") + 1;
            //    indexEnd = Request.UrlReferrer.AbsoluteUri.IndexOf("?");
            //    length = indexEnd - indexStart;
            //    if (length > 0)
            //    {
            //        this.lastPage = Request.UrlReferrer.AbsoluteUri.Substring(indexStart, length);
            //    }
            //    else
            //    {
            //        this.lastPage = Request.UrlReferrer.AbsoluteUri.Substring(indexStart);
            //    }
            //}




            //recipes currRecipe = BusinessFacade.Instance.GetRecipe(recipeId);
            //if (currRecipe == null)
            //{
            //    AppEnv.MoveToDefaultPage();
            //}

            //if ((((BasePage)Page).UserType != 1) && (currRecipe.userId != ((BasePage)Page).UserId) && (!currRecipe.isPublic))
            //{
            //    AppEnv.MoveToDefaultPage();
            //}
            //else
            //{

            //    RecipeId = RecipeId;
            //    Page.Title = string.Format(" ארגון רשימת הקניות שלך - MyBuyList - {0}", currRecipe.name);
            //    PageDescription.Attributes["content"] = string.Format("פרטי מתכון - {0} מאת {1}", currRecipe.name, currRecipe.publishedBy);

            //    if (!string.IsNullOrEmpty(Request["view"]))
            //    {
            //        ViewId = int.Parse(Request["view"]);
            //    }
            //    //btnRecipe.NavigateUrl = "~/PrintRecipe.aspx?recipeId=" + RecipeId;
            //    //btnRecipe_bottom.NavigateUrl = "~/PrintRecipe.aspx?recipeId=" + RecipeId;

            //    Rebind(currRecipe);
            //}
        }
    }

    protected void NutValue_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RecipeTotalNutValues nutValueItem = (RecipeTotalNutValues)e.Item.DataItem;
            HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tdRepeater");
            string colorCode = "";
            switch (nutValueItem.NutCategoryId)
            {
                case 1:
                    colorCode = "#EE1E3E";
                    break;
                case 2:
                    colorCode = "#FBAB14";
                    break;
                case 3:
                    colorCode = "#A4CB3A";
                    break;
                case 4:
                    colorCode = "#1A98D5";
                    break;
                case 5:
                    colorCode = "#656565";
                    break;
                default:
                    colorCode = "#656565";
                    break;
            }
            td.Style["background-color"] = colorCode;
        }
    }

    private void RefreshPage()
    {
        this.Response.Redirect(this.Request.Url.OriginalString);
    }

    private void Rebind(RecipeModel recipe)
    {
        //recipes recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);
        //PlaceHolder1.Visible = ((BasePage)Page).UserId == recipe.userId;
        //PlaceHolder2.Visible = ((BasePage)Page).UserId == recipe.userId;

        //FavoritRecipesModel userFavoritRecipe = HttpHelper.Get<FavoritRecipesModel>(string.Format("{0}/recipes/{1}/favorites", ((BasePage)Page).UserId, recipe.id));

        this.lblRecipeName.Text = recipe.name;
        this.lblServNumber.Text = recipe.servings.ToString();

        if (recipe.tags != null)
        {
            this.lblRecipeTags.Text = recipe.tags;
        }
        if (recipe.description != null)
        {
            this.lblRecipeDescription.Text = recipe.description;
        }

        if (recipe.level != null)
        {
            this.txtDifficulty.Text = this.GetDifficultyLevelString(recipe.level.Value);
        }

        if (recipe.prepTime != null)
        {
            string units;
            this.lblPrepTime.Text = this.GetTimeInCorrectUnits(recipe.prepTime.Value, out units).ToString() + " " + units;
        }

        if (recipe.cookTime != null)
        {
            string units;
            this.lblCookTime.Text = this.GetTimeInCorrectUnits(recipe.cookTime.Value, out units).ToString() + " " + units;
        }

        if (!string.IsNullOrEmpty(recipe.remarks))
        {
            this.lblRemarks.Text = recipe.remarks;
        }
        else
        {
            //this.recipe_remarks.Visible = false;
        }

        if (!string.IsNullOrEmpty(recipe.preparationMethod))
        {
            this.txtPreparationMethod.Text = recipe.preparationMethod.Replace("\n", "<br />");
        }

        if (!string.IsNullOrEmpty(recipe.tools))
        {
            this.txtTools.Text = recipe.tools.Replace("\n", "<br />");
        }

        //bool isInMyFavorites = (recipe.users.SingleOrDefault(ufr => ufr.UserId == ((BasePage)this.Page).UserId) != null);
        //if (isInMyFavorites)
        //{
        //    this.myFavoritesTopTag.Visible = true;
        //}
        //else
        //{
        //    this.myFavoritesTopTag.Visible = false;
        //}

        //lblAllFavorites.Text = recipe.users.Count.ToString();

        //lblAllMenus.Text = recipe.Menus.Count.ToString();

        //if (recipe.Categories.Any())
        //{
        //    string str = "";
        //    foreach (Category rc in recipe.Categories)
        //    {
        //        str += rc.CategoryName + ", ";
        //    }
        //    str = str.Remove(str.Length - 2);

        string[] cats = recipe.categories.Select(category => category.categoryName).ToArray();
        lblRecipeCategories.Text = string.Join(", ", cats);
        //}

        //ingredients[] ingredients = BusinessFacade.Instance.GetRecipeIngredientsList(recipe.id);
        //dlistIngredients.ItemDataBound += DlistIngredients_ItemDataBound;
        dlistIngredients.DataSource = recipe.ingrediants;
        dlistIngredients.DataBind();

        bool allowRecipeEdit = (bool)((recipe.userId == ((BasePage)Page).UserId) || (((BasePage)Page).UserType == 1));
        //lblEditRecipeSeparator.Visible = allowRecipeEdit;
        //lblEditRecipeSeparator_bottom.Visible = allowRecipeEdit;
        //btnEditRecipe.Visible = allowRecipeEdit;
        //btnEditRecipe_bottom.Visible = allowRecipeEdit;
        //lblCopyRecipeSeperator.Visible = allowRecipeEdit;
        //lblCopyRecipeSeperator_bottom.Visible = allowRecipeEdit;
        //btnDeleteRecipe.Visible = allowRecipeEdit;
        //btnDeleteRecipe_bottom.Visible = allowRecipeEdit;

        //users user = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);
        //bool isValidUser = ((BasePage)Page).UserId != -1;
        //bool existInFavorites = false;
        //if (isValidUser)
        //{
            //recipes[] ufrep = user.Recipes1.ToArray();
            //existInFavorites = (ufrep != null);
        //}

        //lblAddToFavoritesSeparator.Visible = isValidUser;
        //lblAddToFavoritesSeparator_bottom.Visible = isValidUser;
        //btnAddRecipeToFavorites.Visible = ((!existInFavorites) && isValidUser);
        //btnAddRecipeToFavorites_bottom.Visible = ((!existInFavorites) && isValidUser);
        //btnRemoveRecipeFromFavorites.Visible = ((existInFavorites) && isValidUser);
        //btnRemoveRecipeFromFavorites_bottom.Visible = ((existInFavorites) && isValidUser);

        //if ((isValidUser) && (((BasePage)Page).UserType == 1))
        //{
            //btnCopyRecipe.Visible = true;
            //btnCopyRecipe_bottom.Visible = true;
            //lblSeparator3.Visible = true;
            //lblSeparator3_bottom.Visible = true;
        //}

        //btnSendMail.Visible = isValidUser;
        //lblSeparator1.Visible = isValidUser;

        //if (allowRecipeEdit && recipe.MenuRecipes.Count > 0)
        //if (allowRecipeEdit && recipe.Menus.Count > 0 &&((BasePage)Page).UserType != 1)
        //{
        //    //btnDeleteRecipe.Visible = false;
        //    //btnDeleteRecipe_bottom.Visible = false;
        //    lblDeleteRecipeDisabled.Visible = true;
        //    lblDeleteRecipeDisabled_bottom.Visible = true;
        //    lblDeleteRecipeDisabled.ToolTip = "לא ניתן למחוק את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך למחוק את המתכון - נא ליצור קשר עם מנהלת האתר.";
        //    lblDeleteRecipeDisabled_bottom.ToolTip = "לא ניתן למחוק את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך למחוק את המתכון - נא ליצור קשר עם מנהלת האתר";
        //    //btnEditRecipe.Visible = false;
        //    //btnEditRecipe_bottom.Visible = false;
        //    lblEditRecipeDisabled.Visible = true;
        //    lblEditRecipeDisabled_bottom.Visible = true;
        //    lblEditRecipeDisabled.ToolTip = "לא ניתן לערוך את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך לערוך את המתכון - נא ליצור קשר עם מנהלת האתר.";
        //    lblEditRecipeDisabled_bottom.ToolTip = "לא ניתן לערוך את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך לערוך את המתכון - נא ליצור קשר עם מנהלת האתר";
        //}

        //Dictionary<int, recipes> selectedRecipes = Utils.SelectedRecipes;
        //bool isSelectedRecipe = (selectedRecipes.Keys.Contains(recipe.id));

        //if (!isSelectedRecipe)
        //{
            //blkAddRemove.Text = "הוסף לרשימת קניות";
            //blkAddRemove_bottom.Text = "הוסף לרשימת קניות";
        //}
        //else
        //{
            //blkAddRemove.Text = "הסר מרשימת קניות";
            //blkAddRemove_bottom.Text = "הסר מרשימת קניות";
            //blkAddRemove.Style["color"] = "Red";
            //blkAddRemove_bottom.Style["color"] = "Red";
        //}

        //bool isCompleteCalculation = false;
        //RecipeTotalNutValues[] nutritionalValues = BusinessFacade.Instance.GetRecipeTotalNutValues(this.RecipeId, out isCompleteCalculation);
        //if (nutritionalValues == null || nutritionalValues.Length == 0 || !isCompleteCalculation)            
        //{
        //    divNutritionalValues.Visible = false;
        //    txtNoDataForNutritionalValues.Visible = true;
        //    txtNoDataForNutritionalValues.Text = MyGlobalResources.NoDataForNutritionalValues.Replace("\n", "<br>");
        //}
        //else
        //{
        //    if (rptNutritionalValues != null)
        //    {
        //        rptNutritionalValues.DataSource = nutritionalValues;
        //        rptNutritionalValues.DataBind();
        //        rptNutritionalValues1.DataSource = nutritionalValues;
        //        rptNutritionalValues1.DataBind();
        //    }
        //}

        //if (recipe.picture != null)
        //{
        //    this.imgRecipePicture.ImageUrl = "~/ShowPicture.ashx?RecipeId=" + recipe.id;
        //}
        //else
        //{
        //    this.imgRecipePicture.ImageUrl = "~/Images/Img_Default.jpg";
        //}

        //if (!string.IsNullOrEmpty(recipe.videoLink) && recipe.videoLink.Contains("object") && recipe.videoLink.Contains("embed"))
        //{
        //    //adjustment method may not work for embedded videos that are not from youtube.
        //    this.recipe_video.InnerHtml = this.AdjustVideo(recipe.videoLink);
        //}
        //else
        //{
        //    this.recipe_video.Visible = false;
        //}

        //this.lnkPublisher.Text = recipe.publishedBy;
        //this.lblPublishDate.Text = recipe.modifyDate.ToString("dd/MM/yyyy");

        //string userEmail = string.Empty;
        //users currentUser = BusinessFacade.Instance.GetUser(((BasePage)this.Page).UserId);
        //if (currentUser != null)
        //{
        //    userEmail = currentUser.Email;
        //}
        //this.ucSendMailToFriend.BindItemDetails("Recipe", recipe.id, recipe.name, userEmail);

        //UpdatePanel2.Update();
        //UpdatePanel1.Update();
    }

    //private void DlistIngredients_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    IngrediantModel ingredient = (IngrediantModel)e.Item.DataItem;
    //    if (ingredient != null)
    //    {

    //    }
    //}

    private void RefreshTopTags()
    {
        recipes recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);
        //if (recipe != null)
        //{
        //    bool isInMyFavorites = (recipe.Users.SingleOrDefault(ufr => ufr.UserId == ((BasePage)this.Page).UserId) != null);
        //    if (isInMyFavorites)
        //    {
        //        this.myFavoritesTopTag.Visible = true;
        //    }
        //    else
        //    {
        //        this.myFavoritesTopTag.Visible = false;
        //    }

        //    this.lblAllFavorites.Text = recipe.Users.Count.ToString();

        //    this.lblAllMenus.Text = recipe.Menus.Count.ToString();
        //}
    }

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    string url;
    //    if (this.ViewId >= (int)PersonalAreaViewEnum.SimpleSearch)
    //    {
    //        url = "~/" + this.lastPage;

    //        if (this.menuId != string.Empty)
    //        {
    //            if (this.ViewId == (int)PersonalAreaViewEnum.CategoriesSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["categoryId"]))
    //                {
    //                    url += string.Format("?menuId={0}&view={1}&categoryId={2}", this.menuId, this.ViewId, this.Request["categoryId"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?menuId={0}&view={1}", this.menuId, this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.SimpleSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["text"]))
    //                {
    //                    url += string.Format("?menuId={0}&view={1}&text={2}", this.menuId, this.ViewId, this.Request["text"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?menuId={0}&view={1}", this.menuId, this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.ComplexSearch)
    //            {
    //                //if ((!string.IsNullOrEmpty(this.Request["text"])) && (!string.IsNullOrEmpty(this.Request["servings"])))
    //                // {
    //                url += string.Format("?menuId={0}&view={1}&text={2}&servings={3}", this.menuId, this.ViewId, this.Request["text"], this.Request["servings"]);
    //                //}
    //                //else if (!string.IsNullOrEmpty(this.Request["text"]))
    //                //{
    //                //    url = string.Format("~/MenuRecipes.aspx?menuId={0}&view={1}&text={2}", this.menuId, this.ViewId, this.Request["text"]);
    //                //}
    //                //else if (!string.IsNullOrEmpty(this.Request["servings"]))
    //                //{
    //                //    url = string.Format("~/MenuRecipes.aspx?menuId={0}&view={1}&servings={2}", this.menuId, this.ViewId, this.Request["servings"]);
    //                //}
    //                //else
    //                //{
    //                //    url = string.Format("~/MenuRecipes.aspx?menuId={0}&view={1}", this.menuId, this.ViewId);
    //                //}
    //            }
    //            else
    //            {
    //                url += string.Format("?menuId={0}&view={1}", this.menuId, this.ViewId);
    //            }
    //        }
    //        else
    //        {
    //            if (this.ViewId == (int)PersonalAreaViewEnum.CategoriesSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["categoryId"]))
    //                {
    //                    url += string.Format("?view={0}&categoryId={1}", this.ViewId, this.Request["categoryId"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?view={0}", this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.SimpleSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["text"]))
    //                {
    //                    url += string.Format("?view={0}&text={1}", this.ViewId, this.Request["text"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?view={0}", this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.ComplexSearch)
    //            {
    //                url += string.Format("?view={0}&text={1}&servings={2}", this.ViewId, this.Request["text"], this.Request["servings"]);
    //            }
    //            else
    //            {
    //                url += string.Format("?view={0}", this.ViewId);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        url = string.Format("~/Users/PersonalArea.aspx?view={0}", this.ViewId);
    //    }

    //    this.Response.Redirect(url);
    //}

    protected void blkAddRemove_Click(object sender, EventArgs e)
    {
        recipes recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);

        string preStr = "";

        Dictionary<int, recipes> selectedRecipes = Utils.SelectedRecipes;

        if (selectedRecipes.Keys.Contains(recipe.RecipeId))
        {
            selectedRecipes.Remove(this.RecipeId);
            Utils.SelectedRecipes = selectedRecipes;
            //this.blkAddRemove.Text = "הוסף לרשימת קניות";
            //this.blkAddRemove_bottom.Text = "הוסף לרשימת קניות";
            //this.blkAddRemove.Style["color"] = "#656565";
            //this.blkAddRemove_bottom.Style["color"] = "#656565";
            preStr = "מרשימת הקניות / תפריט הוסר המתכון:";
        }
        else
        {
            selectedRecipes.Add(this.RecipeId, recipe);
            Utils.SelectedRecipes = selectedRecipes;
            //this.blkAddRemove.Text = "הסר מרשימת קניות";
            //this.blkAddRemove_bottom.Text = "הסר מרשימת קניות";
            //this.blkAddRemove.Style["color"] = "Red";
            //this.blkAddRemove_bottom.Style["color"] = "Red";
            preStr = "לרשימת הקניות / תפריט התווסף המתכון:";
        }

        //this.UpdatePanel1.Update();

        UserControl uc = ((this.Master)).FindControl("HeaderControl1") as UserControl;

        if (uc != null)
        {
            Label lbl = uc.FindControl("lblSelectedRecipesNum") as Label;
            if (lbl != null)
            {
                lbl.Text = "(" + Utils.SelectedRecipes.Count.ToString() + ")";
            }

            UpdatePanel up = uc.FindControl("upSelectedRecipesList") as UpdatePanel;
            if (up != null)
            {
                up.Update();
            }
        }

        ((BasePage)this.Page).DisplayMessage = preStr + "<br/>" + recipe.RecipeName;
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        //StringWriter html = new StringWriter();
        //Server.Execute("~/PrintRecipe.aspx?recipeId=" + this.RecipeId.ToString(), html);
        //string to = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId).Email;
        //try
        //{
        //    ProperServices.Common.Mail.Mailer.SendMail(to, ProperControls.General.Utils.FromEmail, BusinessFacade.Instance.GetRecipe(this.RecipeId).RecipeName, html.ToString(), true);

        //    this.lblResult.Visible = true;
        //    this.lblResult.Text = "המתכון נשלח ל-" + to;
        //}
        //catch
        //{
        //    this.lblResult.Visible = true;
        //    this.lblResult.Text = "בעיה בשליחה";
        //}        
    }

    //protected void btnDeleteRecipe_Click(object sender, EventArgs e)
    //{
    //if (BusinessFacade.Instance.DeleteRecipe(RecipeId))
    //{
    //    Response.Redirect("~/Recipes.aspx");
    //}
    //}

    protected void ucSendMailToFriend_EmailSent(object sender, SendToFriendEventArgs e)
    {
        ((BasePage)this.Page).DisplayMessage = "אימייל נשלח ל- " + e.recipentName;
    }

    protected void btnEditRecipe_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("~/RecipeEdit.aspx?recipeId={0}", this.RecipeId));
    }

    protected void btnCopyRecipe_Click(object sender, EventArgs e)
    {
        //this.ucRecipe.CopyRecipe(this.RecipeId);
        Response.Redirect(string.Format("~/RecipeEdit.aspx?recipeId={0}&docopy=1", this.RecipeId));
    }

    protected void btnAddRecipeToFavorites_Click(object sender, EventArgs e)
    {
        int favRecipesNum = 0;
        if (BusinessFacade.Instance.AddRecipeToUserFavorites(((BasePage)Page).UserId, this.RecipeId, out favRecipesNum))
        {
            //this.btnAddRecipeToFavorites.Visible = false;
            //this.btnAddRecipeToFavorites_bottom.Visible = false;
            //this.btnRemoveRecipeFromFavorites.Visible = true;
            //this.btnRemoveRecipeFromFavorites_bottom.Visible = true;
            RefreshTopTags();
            //upTopTags.Update();

            List<int> favRecipesList = Utils.FavoriteRecipesAdded;
            favRecipesList.Add(this.RecipeId);
            Utils.FavoriteRecipesAdded = favRecipesList;

            UserControl uc = ((this.Master)).FindControl("HeaderControl1") as UserControl;

            if (uc != null)
            {
                Label lbl = uc.FindControl("lblFavRecipesNum") as Label;
                if (lbl != null)
                {
                    lbl.Text = "(" + Utils.FavoriteRecipesAdded.Count + ")";
                }

                UpdatePanel up = uc.FindControl("upFavorites") as UpdatePanel;
                if (up != null)
                {
                    up.Update();
                }
            }
        }
    }

    protected void btnRemoveRecipeFromFavorites_Click(object sender, EventArgs e)
    {
        int favRecipesNum = 0;
        //if (BusinessFacade.Instance.RemoveUserFavoritesRecipe(((BasePage)Page).UserId, this.RecipeId, out favRecipesNum))
        //{
        //    this.btnAddRecipeToFavorites.Visible = true;
        //    this.btnAddRecipeToFavorites_bottom.Visible = true;
        //    this.btnRemoveRecipeFromFavorites.Visible = false;
        //    this.btnRemoveRecipeFromFavorites_bottom.Visible = false;
        //    this.RefreshTopTags();
        //    this.upTopTags.Update();

        //    List<int> favRecipesList = Utils.FavoriteRecipesAdded;
        //    if (favRecipesList.Contains(this.RecipeId))
        //    {
        //        favRecipesList.Remove(this.RecipeId);
        //    }
        //    Utils.FavoriteRecipesAdded = favRecipesList;

        //    UserControl uc = ((this.Master)).FindControl("HeaderControl1") as UserControl;

        //    if (uc != null)
        //    {
        //        Label lbl = uc.FindControl("lblFavRecipesNum") as Label;
        //        if (lbl != null)
        //        {
        //            lbl.Text = "(" + Utils.FavoriteRecipesAdded.Count + ")";
        //        }

        //        UpdatePanel up = uc.FindControl("upFavorites") as UpdatePanel;
        //        if (up != null)
        //        {
        //            up.Update();
        //        }
        //    }
        //}        
    }

    //protected void ShowCategories_Click(int recipeId, SRL_RecipeCategory[] arr)
    //{
    //    this.ucRecipeCats.ShowCategories(recipeId, arr);
    //}
    //protected void SelectPicture_Click(int recipeId)
    //{
    //    this.ucRecipePicture.ShowPicture(recipeId);
    //}

    //protected void RecipePicture_Updated(Binary picture)
    //{
    //    this.ucRecipe.UpdatePicture(picture);
    //}

    //protected void RecipeCategories_RefreshData(SRL_RecipeCategory[] arr)
    //{
    //    this.ucRecipe.RefreshCategories(arr);
    //} 

    protected string AdjustVideo(string embeddedVideoString)
    {
        string str = embeddedVideoString;
        int embedIndex = str.IndexOf("embed");
        int heightIndex = str.IndexOf("height=", embedIndex);
        string heightValue = str.Substring(heightIndex + "height=".Length + 1, 3);
        int widthIndex = str.IndexOf("width=", embedIndex);
        string widthValue = str.Substring(widthIndex + "width=".Length + 1, 3);
        str = str.Replace(widthValue, "443");
        str = str.Replace(heightValue, "360");
        return str;
    }

    private string GetDifficultyLevelString(int level)
    {
        string str = "";

        switch (level)
        {
            case 1:
                str = "קל";
                break;
            case 2:
                str = "בינוני";
                break;
            case 3:
                str = "קשה";
                break;
        }

        return str;
    }

    private decimal GetTimeInCorrectUnits(int time, out string unitString)
    {
        decimal timeInCorrectUnits = 0;
        unitString = "דקות";

        if (time < 120)
        {
            timeInCorrectUnits = (decimal)time;
        }
        else if (time >= 120 && time < 600)
        {
            if (time % 30 == 0)
            {
                int temp = time / 30;
                timeInCorrectUnits = (decimal)temp / 2;
                unitString = "שעות";
            }
            else
            {
                timeInCorrectUnits = (decimal)time;
            }
        }
        else
        {
            int temp = time / 30;
            timeInCorrectUnits = (decimal)temp / 2;
            unitString = "שעות";
        }

        return timeInCorrectUnits;
    }

    protected void SaveImage(object sender, EventArgs e)
    {
        recipes currRecipe = BusinessFacade.Instance.GetRecipe(RecipeId);
        string category = "ReciepsScreenShots";

        string savePath = Server.MapPath(string.Format(@"~\Images\{0}\", category));

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string t_strLargeImage = string.Format(@"{0}{1}.jpg", savePath, currRecipe.RecipeName);

        // Create instance
        //ThumbMakerClass t_xThumbMaker = new ThumbMakerClass();

        //t_xThumbMaker.SetURL(string.Format("http://{0}{1}/ScreenShotRecipe.aspx?recipeId={2}&action=screenshot", Request.Url.Host,
        //                                   Request.ApplicationPath, RecipeId));
        //t_xThumbMaker.SetRegInfo("KRMAXARQW-XTABNYBXW-KMQXRWMKB-BNTQABQTE");
        //t_xThumbMaker.StartSnap();

        //// Save the image with full size in C#
        //bool saveImage = t_xThumbMaker.SaveImage(t_strLargeImage);

        //if (saveImage)
        //{
        //    Response.Redirect(string.Format("ScreenShotRecipe.aspx?RecipeId={0}", RecipeId));
        //}
    }
}


