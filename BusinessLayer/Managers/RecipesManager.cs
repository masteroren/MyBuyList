using MyBuyList.DataLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MyBuyList.BusinessLayer.Managers
{
    class RecipesManager
    {
        internal Recipe[] GetRecipesListByFoodId(int foodId)
        {
            return DataFacade.Instance.GetRecipesListByFoodId(foodId);
        }

        internal IQueryable<Recipe> GetRecipesListByFreeText(string freeText)
        {
            return DataFacade.Instance.GetRecipesListByFreeText(freeText);
        }

        internal Recipe[] GetRecipesListByComplexSearch(string freeText, int? servings, int[] recipeCats, int userId)
        {
            return DataFacade.Instance.GetRecipesListByComplexSearch(freeText, servings, recipeCats, userId);
        }

        internal Recipe[] GetUserRecipesList(int userId)
        {
            return DataFacade.Instance.GetUserRecipesList(userId);
        }

        internal Recipe[] GetUserFavoritesRecipes(int userId)
        {
            return DataFacade.Instance.GetUserFavoritesRecipes(userId);
        }

        internal Category[] GetRecipesCategoriesList(int userId)
        {
            return DataFacade.Instance.GetRecipesCategoriesList(userId);
        }

        internal Recipe[] GetRecipesByCategory(int categoryId, int userId)
        {
            return DataFacade.Instance.GetRecipesByCategory(categoryId, userId);
        }

        internal Recipe GetRecipe(int recipeId)
        {
            return DataFacade.Instance.GetRecipe(recipeId);
        }

        internal void AllowRecipe(int recipeId)
        {
             DataFacade.Instance.AllowRecipe(recipeId);
        }

        internal int GetRecipesNum()
        {
            return DataFacade.Instance.GetRecipesNum();
        }

        internal Recipe[] GetRecipesList()
        {
            return DataFacade.Instance.GetRecipesList();
        }

        //internal RecipeIngredientsView[] GetRecipeIngredientsViewList(int recipeId)
        //{
        //    return DataFacade.Instance.GetRecipeIngredientsViewList(recipeId);
        //}

        internal Ingredient[] GetRecipeIngredientsList(int recipeId)
        {
            return DataFacade.Instance.GetRecipeIngredientsList(recipeId);
        }

        internal Dictionary<int, string> GetFoodList(string prefixText)
        {
            return DataFacade.Instance.GetFoodList(prefixText);
        }

        internal bool AddRecipeToUserFavorites(int userId, int recipeId, out int favRecipesNum)
        {
            return DataFacade.Instance.AddRecipeToUserFavorites(userId, recipeId, out favRecipesNum);
        }

        internal bool RemoveUserFavoritesRecipe(int userId, int recipeId, out int favRecipesNum)
        {
            return DataFacade.Instance.RemoveUserFavoritesRecipe(userId, recipeId, out favRecipesNum);
        }

        internal int DeleteRecipe(int recipeId)
        {
            return DataFacade.Instance.DeleteRecipe(recipeId);
        }

        internal bool SaveRecipe(Recipe recipe, List<Ingredient> ingridiants, List<SRL_RecipeCategory> categories, bool isNewRecipe, out int recipeId)
        {
            if (isNewRecipe)
            {
                return DataFacade.Instance.SaveRecipe(recipe, ingridiants, categories, out recipeId);
            }
            else
            {
                recipeId = recipe.RecipeId;
                return DataFacade.Instance.UpdateRecipe(recipe, ingridiants, categories);
            }
        }

        internal bool UpdateRecipePreparationMethod(int recipeId, string preparationMethod)
        {
            return DataFacade.Instance.UpdateRecipePreparationMethod(recipeId, preparationMethod);
        }

        //internal bool SaveRecipeCategories(int recipeId, RecipeCategory[] categories)
        //{
            //return DataFacade.Instance.SaveRecipeCategories(recipeId, categories);
        //}

        internal bool SaveFood(Food food)
        {
            return DataFacade.Instance.SaveFood(food);
        }

        internal Dictionary<string, Recipe> GetRecipesInMenuMeals(int menuId)
        {
           // Menu currMenue = BusinessFacade.Instance.GetMenu(menuId);

            //List<Recipe> MealsrecipesList = new List<Recipe>();
            Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();

            Meal[] menuMeals = BusinessFacade.Instance.GetMealsList(menuId);
            foreach (Meal currMeal in menuMeals)
            {
                MealRecipe[] mealRecipes = BusinessFacade.Instance.GetMealRecipes(currMeal.MealId);
                foreach (MealRecipe currRecipe in mealRecipes)
                {
                    if (!recipes.ContainsKey(currRecipe.Recipes.RecipeName))
                    {
                        recipes.Add(currRecipe.Recipes.RecipeName, currRecipe.Recipes);
                    }
                }
            }

            return recipes;
        }

        internal RecipeTotalNutValues[] GetRecipeTotalNutValues(int recipeId, out bool isCompleteCalculation)
        {
            return DataFacade.Instance.GetRecipeTotalNutValues(recipeId, out isCompleteCalculation);
        }

        internal IEnumerable<Recipe> GetRecipes(RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            return DataFacade.Instance.GetRecipes(orderBy, page, pageSize, out totalPages);
        }

        internal List<Recipe> GetRecipesEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? servings, int[] recipeCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numRecipes)
        {
            return DataFacade.Instance.GetRecipesEx(display, userId, freeText, categoryId, servings, recipeCats, orderBy, page, pageSize, out totalPages, out numRecipes);
        }

        internal int? GetRecipeUserFavoritesCount(int recipeId)
        {
            return DataFacade.Instance.GetRecipeUserFavoritesCount(recipeId);
        }

        internal int? GetRecipeMenusCount(int recipeId)
        {
            return DataFacade.Instance.GetRecipeMenusCount(recipeId);
        }

        //internal IQueryable<RecipesView> GetRecipes(string searchValue, int userId)
        //{
        //    return DataFacade.Instance.GetRecipes(searchValue, userId);
        //}

        internal void AddRecipeToShoppingList(int userId, int recipeId)
        {
            DataFacade.Instance.AddRecipeToShoppingList(userId, recipeId);
        }

        internal IQueryable<RecipesInShoppingList> GetSelectedRecipes(int userId)
        {
            return DataFacade.Instance.GetSelectedRecipes(userId);
        }

        internal void RemoveRecipeFromShoppingList(int userId, int recipeId)
        {
            DataFacade.Instance.RemoveRecipeFromShoppingList(userId, recipeId);
        }

        internal IEnumerable<Recipe> SearchRecipes(string searchedText)
        {
            return DataFacade.Instance.SearchRecipes(searchedText);
        }
    }
}
