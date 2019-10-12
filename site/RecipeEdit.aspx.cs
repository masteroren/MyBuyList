using MyBuyList.Shared.Entities;
using MyBuyListShare.Classes;
using MyBuyListShare.Models;
using System;
using System.Collections.Generic;

public partial class RecipeEdit : BasePage
{
    #region Properties
    protected int? RecipeId
    {
        get
        {
            int recipeId;

            if (string.IsNullOrEmpty(Request["recipeId"]) || !int.TryParse(Request["recipeId"], out recipeId))
                return null;
            else
            {
                ViewState["RecipeId"] = recipeId;
                return recipeId;
            }
        }
    }

    protected bool ShouldCopy
    {
        get
        {
            return !string.IsNullOrEmpty(Request["docopy"]) && Request["docopy"].Equals("1");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (RecipeId.HasValue)
            {
                ucRecipe.RecipeId = RecipeId.HasValue ? RecipeId.Value : 0;
                ucRecipe.recipe = RecipeId.HasValue ? HttpHelper.Get<RecipeModel>(string.Format("recipes/{0}", RecipeId)) : null;
                ucRecipe.SelectedCategories = ucRecipe.recipe.categories;
                ViewState["Recipe"] = Json.JsonSerializer(ucRecipe.recipe);
                ViewState["RecipeId"] = ucRecipe.RecipeId;

                if (ShouldCopy)
                    ucRecipe.CopyRecipe();
                else
                    ucRecipe.EditRecipe();
            }
            else
            {
                ucRecipe.NewRecipe();
            }
        } else
        {
            ucRecipe.RecipeId = (int)ViewState["RecipeId"];
            ucRecipe.recipe = Json.JsonDeserializer<RecipeModel>((string)ViewState["Recipe"]);
            ucRecipe.SelectedCategories = Json.JsonDeserializer<List<ShortCategoryModel>>((string)ViewState["Categories"]);
        }
    }

    protected void RecipeCategories_RefreshData(SRL_RecipeCategory[] arr)
    {
        ucRecipe.RefreshCategories(arr);
    }


    protected void ucRecipeCats_SaveClick(object sender, SaveEventArgs e)
    {
        ViewState["Categories"] = Json.JsonSerializer(e.SelectedCategories);
        ucRecipe.SelectedCategories = e.SelectedCategories;
    }
}
