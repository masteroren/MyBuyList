using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;

public class AppEnv
{
    public const int USER_ADMIN = 1;    

    public static void MoveToDefaultPage()
    {
        HttpContext.Current.Response.Redirect("~/Default.aspx");
    }
}

public enum PersonalAreaViewEnum
{
    MyMenusList = 0,
    MyRecipesList = 1,
    FavoritesRecipes = 2,
    SimpleSearch = 3,
    ComplexSearch = 4,
    CategoriesSearch = 5,
    MyRecipesSearch = 6,
    MyFavoritesRecipesSearch = 7
}

[Serializable]
public class SRL_Recipe
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Name { get; set; }

    public SRL_Recipe(recipes item)
    {
        RecipeId = item.RecipeId;
        RecipeName = item.RecipeName;

        if (item.users != null)
        {
            if (!string.IsNullOrEmpty(item.users.DisplayName))
            {
                Name = item.users.DisplayName;
            }
            else
            {
                Name = item.users.Name;
            }
        }
    }
}

[Serializable]
public class SRL_MealRecipe
{
    public int MealId { get; set; }
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public int Servings { get; set; }

    public SRL_MealRecipe(mealrecipes item)
    {
        MealId = item.MealId;
        RecipeId = item.RecipeId;
        RecipeName = item.recipes.RecipeName;
        Servings = item.Servings;
    }
}

[Serializable]
public class SRL_Meal
{
    public int MealId { get; set; }
    public int MealTypeId { get; set; }
    public int? DayIndex { get; set; }
    public int? CourseTypeId { get; set; }
    public int? Diners { get; set; }
    public SRL_MealRecipe[] MealRecipes { get; set; }

    public SRL_Meal(meals meal)
    {
        MealId = meal.MealId;
        MealTypeId = meal.MealTypeId;
        DayIndex = meal.DayIndex;
        CourseTypeId = meal.CourseTypeId;
        Diners = meal.Diners;

        if (meal.mealrecipes != null)
        {
            var list = from item in meal.mealrecipes
                       select new SRL_MealRecipe(item);
            MealRecipes = list.ToArray();
        }
    }
}

[Serializable]
public class SRL_Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int? ParentCategoryId { get; set; }
    public int RecipesCount { get; set; }

    public SRL_Category(int categoryId, string categoryName, int? parentCategoryId)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
        ParentCategoryId = parentCategoryId;
    }

    public SRL_Category(int categoryId, string categoryName, int? parentCategoryId, int recipesCount) :
        this(categoryId, categoryName, parentCategoryId)
    {
        RecipesCount = recipesCount;
    }
}
