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
                RecipeModel recipe = RecipeId.HasValue ? HttpHelper.Get<RecipeModel>(string.Format("recipes/{0}", RecipeId)) : null;
                ucRecipe.RecipeId = RecipeId.HasValue ? RecipeId.Value : 0;
                ucRecipe.recipe = recipe;
                ucRecipe.SelectedCategories = ucRecipe.recipe.categories;
                ucRecipeCats.RecipeId = RecipeId.HasValue ? RecipeId.Value : 0;
                ucRecipeCats.recipe = recipe;

                ViewState["Recipe"] = Json.JsonSerializer(ucRecipe.recipe);
                ViewState["RecipeId"] = ucRecipe.RecipeId;
                ViewState["Categories"] = Json.JsonSerializer(ucRecipe.recipe.categories);

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
            if (ViewState["RecipeId"] != null)
            {
                ucRecipe.RecipeId = (int)ViewState["RecipeId"];
                ucRecipe.recipe = Json.JsonDeserializer<RecipeModel>((string)ViewState["Recipe"]);
                ucRecipe.SelectedCategories = Json.JsonDeserializer<List<ShortCategoryModel>>((string)ViewState["Categories"]);
                ucRecipeCats.RecipeId = (int)ViewState["RecipeId"];
                ucRecipeCats.recipe = Json.JsonDeserializer<RecipeModel>((string)ViewState["Recipe"]);
            }
        }
    }

    protected void RecipeCategories_RefreshData(SRL_RecipeCategory[] arr)
    {
        ucRecipe.RefreshCategories(arr);
    }


    protected void ucRecipeCats_SaveClick(object sender, SaveEventArgs e)
    {
        ucRecipe.SelectedCategories = e.SelectedCategories;
    }
}
