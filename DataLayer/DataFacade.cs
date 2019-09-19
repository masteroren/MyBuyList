using System.Collections.Generic;
using System.Linq;

using MyBuyList.Shared.Entities;
using MyBuyList.DataLayer.DataAdapters;
using MyBuyList.Shared.Enums;
using System;
using MyBuyList.Shared;

namespace MyBuyList.DataLayer
{
    public sealed class DataFacade
    {
        #region Singleton
        private static DataFacade singleton = new DataFacade();

        private DataFacade() { }

        public static DataFacade Instance
        {
            get { return singleton; }
        }
        #endregion

        #region Menus

        public menus[] GetUserFavoritesMenus(int userId)
        {
            return new MenusDA().GetUserFavoritesMenus(userId);
        }

        public menus[] SearchMenus(string searchValue)
        {
            return new MenusDA().SearchMenus(searchValue);
        }

        public menus[] GetUserMenusList(int userId)
        {
            return new MenusDA().GetUserMenusList(userId);
        }

        public menus[] GetMenusList(int userId)
        {
            return new MenusDA().GetMenusList(userId);
        }

        public menus GetMenu(int menuId)
        {
            return new MenusDA().GetMenu(menuId);
        }

        public menus GetMenuEx(int menuId)
        {
            return new MenusDA().GetMenuEx(menuId);
        }

        public menutypes[] GetMenuTypes()
        {
            return new MenusDA().GetMenuTypes();
        }

        public menutypes GetMenuType(int menuId)
        {
            return new MenusDA().GetMenuType(menuId);
        }

        public recipes[] GetMenuRecipes(int menuId)
        {
            return new MenusDA().GetMenuRecipes(menuId);
        }

        public mealrecipes[] GetMenuMealsRecipes(int menulId)
        {
            return new MenusDA().GetMenuMealsRecipes(menulId);
        }

        public bool CreateMenu(int menuTypeId, int userId, int tempUser, out int menuId)
        {
            return new MenusDA().CreateMenu(menuTypeId, userId, tempUser, out menuId);
        }

        public bool CreateMenuEx(int menuTypeId, int userId, int tempUser, string menuName, string description, bool isPublic, out int menuId)
        {
            return new MenusDA().CreateMenuEx(menuTypeId, userId, tempUser, menuName, description, isPublic, out menuId);
        }

        public bool CreateMenuEx1(menus menu, int tempUser, out int menuId)
        {
            return new MenusDA().CreateMenuEx1(menu, tempUser, out menuId);
        }

        public void CreateOrUpdateMenu(menus menu, out int menuId)
        {
            new MenusDA().CreateOrUpdateMenu(menu, out menuId);
        }

        public bool UpdateMenuNameAndDescription(int menuId, string menuName, string description)
        {
            return new MenusDA().UpdateMenuNameAndDescription(menuId, menuName, description);
        }

        public bool DeleteMenu(int menuId)
        {
            return new MenusDA().DeleteMenu(menuId);
        }

        public bool RemoveMenuRecipe(int menuId, int recipeId)
        {
            return new MenusDA().RemoveMenuRecipe(menuId, recipeId);
        }

        public bool AddMenuRecipe(int menuId, int recipeId)
        {
            return new MenusDA().AddMenuRecipe(menuId, recipeId);
        }

        public bool CheckIfMenuRecipeExistInMeals(int menuId, int recipeId)
        {
            return new MenusDA().CheckIfMenuRecipeExistInMeals(menuId, recipeId);
        }

        #endregion Menus

        #region Meals

        public mealrecipes[] GetMealsWeeklyList(int menuId, int startDayIndex, int endDayIndex)
        {
            return new MealsDA().GetMealsWeeklyList(menuId, startDayIndex, endDayIndex);
        }

        public coursetypes[] GetCourseTypes()
        {
            return new MealsDA().GetCourseTypes();
        }

        public mealtypes[] GetMealTypes()
        {
            return new MealsDA().GetMealTypes();
        }
        public meals[] GetMealsList(int menuId)
        {
            return new MealsDA().GetMealsList(menuId);
        }

        public meals GetMeal(int mealId)
        {
            return new MealsDA().GetMeal(mealId);
        }

        public meals GetMeal(int menuId, int courseTypeId)
        {
            return new MealsDA().GetMeal(menuId, courseTypeId);
        }

        public meals GetMeal(int menuId, int dayIndex, int mealTypeId)
        {
            return new MealsDA().GetMeal(menuId, dayIndex, mealTypeId);
        }

        public bool SaveMeal(int menuId, int courseTypeId, int? diners)
        {
            return new MealsDA().SaveMeal(menuId, courseTypeId, diners);
        }

        public bool SaveMeal(int menuId, int dayIndex, int mealTypeId, int? diners)
        {
            return new MealsDA().SaveMeal(menuId, dayIndex, mealTypeId, diners);
        }

        public mealrecipes[] GetMealRecipesList(int menuId, int dayIndex, int mealTypeId)
        {
            return new MealsDA().GetMealRecipesList(menuId, dayIndex, mealTypeId);
        }

        public bool CreateQuickListMealRecipes(int menuId)
        {
            return new MealsDA().CreateQuickListMealRecipes(menuId);
        }

        public bool AddMealRecipe(int menuId, int courseTypeId, int recipeId, out int mealId)
        {
            return new MealsDA().AddMealRecipe(menuId, courseTypeId, recipeId, out mealId);
        }

        public bool AddMealRecipe(int menuId, int dayIndex, int mealTypeId, int recipeId, out int mealId)
        {
            return new MealsDA().AddMealRecipe(menuId, dayIndex, mealTypeId, recipeId, out mealId);
        }

        public bool RemoveMealRecipe(int mealId, int recipeId)
        {
            return new MealsDA().RemoveMealRecipe(mealId, recipeId);
        }

        public mealrecipes GetMealRecipe(int mealId, int recipeId)
        {
            return new MealsDA().GetMealRecipe(mealId, recipeId);
        }

        public bool ClearAllMeals(int menuId)
        {
            return new MealsDA().ClearAllMeals(menuId);
        }

        public bool SaveMealRecipe(int mealId, int recipeId, int servings)
        {
            return new MealsDA().SaveMealRecipe(mealId, recipeId, servings);
        }

        #endregion Meals

        #region Recipes
        public recipes[] GetRecipesListByFoodId(int foodId)
        {
            return new RecipesDA().GetRecipesListByFoodId(foodId);
        }

        public IQueryable<recipes> GetRecipesListByFreeText(string freeText)
        {
            return new RecipesDA().GetRecipesListByFreeText(freeText);
        }

        public recipes[] GetRecipesListByComplexSearch(string freeText, int? servings, int[] recipeCats, int userId)
        {
            //return new RecipesDA().GetRecipesListByComplexSearch(freeText, servings, recipeCats, userId);
            return null;
        }

        public void AllowRecipe(int recipeId)
        {
            new RecipesDA().AllowRecipe(recipeId);
        }

        public recipes[] GetUserRecipesList(int userId)
        {
            return new RecipesDA().GetUserRecipesList(userId);
        }

        //public recipes[] GetUserFavoritesRecipes(int userId)
        //{
        //    return new RecipesDA().GetUserFavoritesRecipes(userId);
        //}

        public categories[] GetRecipesCategoriesList()
        {
            return new RecipesDA().GetRecipesCategoriesList();
        }

        public recipes[] GetRecipesByCategory(int categoryId, int userId)
        {
            //return new RecipesDA().GetRecipesByCategory(categoryId, userId);
            return null;
        }

        public recipes GetRecipe(int recipeId)
        {
            return new RecipesDA().GetRecipe(recipeId);
        }

        public recipes[] GetRecipesList()
        {
            return new RecipesDA().GetRecipesList();
        }

        public int GetRecipesNum()
        {
            return new RecipesDA().GetRecipesNum();
        }

        //public RecipeIngredientsView[] GetRecipeIngredientsViewList(int recipeId)
        //{
        //    return new RecipesDA().GetRecipeIngredientsViewList(recipeId);
        //}

        public ingredients[] GetRecipeIngredientsList(int recipeId)
        {
            return new RecipesDA().GetRecipeIngredientsList(recipeId);
        }

        public Dictionary<int, string> GetFoodList(string prefixText)
        {
            return new RecipesDA().GetFoodList(prefixText);
        }

        public bool AddRecipeToUserFavorites(int userId, int recipeId, out int favRecipesNum)
        {
            return new RecipesDA().AddRecipeToUserFavorites(userId, recipeId, out favRecipesNum);
        }

        //public bool RemoveUserFavoritesRecipe(int userId, int recipeId, out int favRecipesNum)
        //{
        //    return new RecipesDA().RemoveUserFavoritesRecipe(userId, recipeId, out favRecipesNum);
        //}

        public int DeleteRecipe(int recipeId)
        {
            return new RecipesDA().DeleteRecipe(recipeId);
        }

        public bool SaveRecipe(recipes recipe, List<ingredients> ingridiants, List<SRL_RecipeCategory> categories, out int recipeId)
        {
            return new RecipesDA().SaveRecipe(recipe, ingridiants, categories, out recipeId);
        }

        public bool UpdateRecipe(recipes recipe, List<ingredients> ingridiants, List<SRL_RecipeCategory> categories)
        {
            return new RecipesDA().UpdateRecipe(recipe, ingridiants, categories);
        }

        public bool UpdateRecipePreparationMethod(int recipeId, string preparationMethod)
        {
            return new RecipesDA().UpdateRecipePreparationMethod(recipeId, preparationMethod);
        }

        //public bool SaveRecipeCategories(int recipeId, RecipeCategory[] categories)
        //{
            //return new RecipesDA().SaveRecipeCategories(recipeId, categories);
        //}

        public RecipeTotalNutValues[] GetRecipeTotalNutValues(int recipeId, out bool isCompleteCalculation)
        {
            return new RecipesDA().GetRecipeTotalNutValues(recipeId, out isCompleteCalculation);
        }

        #endregion Recipes

        #region Shopping List

        public void RemoveItemFromShoppingList(int userId, int foodId)
        {
            new ShoppingsListDA().RemoveItemFromShoppingList(userId, foodId);
        }

        public shopdepartments[] GetMenuShopDepartments(int menuId)
        {
            return new ShoppingsListDA().GetMenuShopDepartments(menuId);
        }

        public ShoppingFood[] GetMenuShoppingList(int menuId)
        {
            return new ShoppingsListDA().GetMenuShoppingList(menuId);
        }

        public shoppinglistadditionalitems[] GetShoppingListAdditionalItems(int menuId)
        {
            return new ShoppingsListDA().GetShoppingListAdditionalItems(menuId);
        }

        public bool DeleteShoppingListAdditionalItem(int itemId)
        {
            return new ShoppingsListDA().DeleteShoppingListAdditionalItem(itemId);
        }

        public bool AddShoppingListAdditionalItem(int menuId, string itemName)
        {
            return new ShoppingsListDA().AddShoppingListAdditionalItem(menuId, itemName);
        }

        public string[] GetGeneralItemsList(string prefixText)
        {
            return new ShoppingsListDA().GetGeneralItemsList(prefixText);
        }

        public List<usershoppinglist> GetShoppingList(int userId)
        {
            return new ShoppingsListDA().GetShoppingList(userId);
        }

        #endregion Shopping List

        #region Shortage List
        public int AddMissingList(int userId)
        {
            return new MissingItemsListDA().AddList(userId);
        }

        public void AddMissinListItem(string name, int quantity, int measureUnit, int userId)
        {
            new MissingItemsListDA().AddListItem(name, quantity, measureUnit, userId);
        }

        public IQueryable<missinglistdetails> GetMissingList(int userId)
        {
            return new MissingItemsListDA().GetList(userId);
        }

        public void DeleteMissingListItem(int id)
        {
            new MissingItemsListDA().DeleteListItem(id);
        }
        #endregion

        #region Routine List
        //public SavedList AddSavedList(int userId, string name)
        //{
        //    return new SavedListDA().AddList(userId, name);
        //}

        //public SavedListDetail AddSavedListItem(string name, int quantity, int listId)
        //{
        //    return new SavedListDA().AddListItem(name, quantity, listId);
        //}

        //public IQueryable<SavedListDetail> GetSavedListDetails(int listId)
        //{
        //    return new SavedListDA().GetListDetails(listId);
        //}

        //public void DeleteSavedListItem(int ingredientId)
        //{
        //    new SavedListDA().DeleteListItem(ingredientId);
        //}

        //public IQueryable<SavedList> GetSavedLists(int userId)
        //{
        //    return new SavedListDA().GetLists(userId);
        //}

        //public bool DeleteSavedList(int listId)
        //{
        //    return new SavedListDA().DeleteList(listId);
        //}
        #endregion

        #region General List
        //public int AddGeneralList(int userId, ListTypes listType)
        //{
        //    return new GeneralListDA().AddGeneralList(userId, listType);
        //}

        //public bool AddGeneralListItem(SRL_Ingredient ingredient, int listId)
        //{
        //    return new GeneralListDA().AddGeneralListItem(ingredient, listId);
        //}

        //public List<SRL_Ingredient> GetGeneralList(int userId, ListTypes listType)
        //{
        //    return new GeneralListDA().GetGeneralList(userId, listType);
        //}

        //public bool DeleteGeneralList(int listId)
        //{
        //    return new GeneralListDA().DeleteGeneralList(listId);
        //}

        //public bool DeleteGeneralListItem(int ingredientId)
        //{
        //    return new GeneralListDA().DeleteGeneralListItem(ingredientId);
        //}
        #endregion


        #region Summery List
        //public int AddSummeryList(int userId)
        //{
        //    return new SummeryListDA().AddList(userId);
        //}

        //public bool AddSummeryListItem(SRL_Ingredient ingredient, int listId, int sourceId)
        //{
        //    return new SummeryListDA().AddListItem(ingredient, listId, sourceId);
        //}

        //public int GetSummeryList(int userId)
        //{
        //    return new SummeryListDA().GetList(userId);
        //}

        //public List<SRL_Ingredient> GetSummeryListDetails(int listId)
        //{
        //    return new SummeryListDA().GetListDetails(listId);
        //}

        //public void DeleteSummeryList(int listId)
        //{
        //    new SummeryListDA().DeleteList(listId);
        //}

        //public void DeleteSummeryListItem(int summeryId, int sourceId, SRL_Ingredient ingredient)
        //{
        //    new SummeryListDA().DeleteListItem(summeryId, sourceId, ingredient);
        //}
        #endregion

        #region Admin

        #region Category
        public bool CheckDuplicateCategoryName(int categoryId, string categoryName)
        {
            return new AdminDA().CheckDuplicateCategoryName(categoryId, categoryName);
        }

        public bool CheckDuplicateMCategoryName(int categoryId, string categoryName)
        {
            return new AdminDA().CheckDuplicateMCategoryName(categoryId, categoryName);
        }

        public categories[] GetCategoriesList()
        {
            return new AdminDA().GetCategoriesList();
        }

        public mcategories[] GetMCategoriesList()
        {
            return new AdminDA().GetMCategoriesList();
        }

        public categories GetCategory(int categoryId)
        {
            return new AdminDA().GetCategory(categoryId);
        }

        public mcategories GetMenuCategory(int categoryId)
        {
            return new AdminDA().GetMenuCategory(categoryId);
        }

        public bool SaveCategory(categories category)
        {
            return new AdminDA().SaveCategory(category);
        }

        public bool SaveCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return new AdminDA().SaveCategory(categoryId, categoryName, parentCategoryId);
        }

        public bool SaveMenuCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return new AdminDA().SaveMenuCategory(categoryId, categoryName, parentCategoryId);
        }

        public bool DeleteCategory(int categoryId)
        {
            return new AdminDA().DeleteCategory(categoryId);
        }

        public bool DeleteMenuCategory(int categoryId)
        {
            return new AdminDA().DeleteMenuCategory(categoryId);
        }
        #endregion Category


        #region FoodCategory
        public bool CheckDuplicateFoodCategoryName(int FoodCategoryId, string FoodCategoryName)
        {
            return new AdminDA().CheckDuplicateFoodCategoryName(FoodCategoryId, FoodCategoryName);
        }

        public bool CheckDuplicateFoodName(int FoodId, string FoodName)
        {
            return new AdminDA().CheckDuplicateFoodName(FoodId, FoodName);
        }

        public foodcategories[] GetFoodCategoriesList()
        {
            return new AdminDA().GetFoodCategoriesList();
        }

        public foodcategories GetFoodCategory(int FoodCategoryId)
        {
            return new AdminDA().GetFoodCategory(FoodCategoryId);
        }

        public bool SaveFoodCategory(foodcategories  FoodCategory)
        {
            return new AdminDA().SaveFoodCategory(FoodCategory);
        }

        public bool SaveFoodCategory(int FoodCategoryId, string FoodCategoryName, int? parentFoodCategoryId)
        {
            return new AdminDA().SaveFoodCategory(FoodCategoryId, FoodCategoryName, parentFoodCategoryId);
        }

        public bool DeleteFoodCategory(int FoodCategoryId)
        {
            return new AdminDA().DeleteFoodCategory(FoodCategoryId);
        }
        #endregion FoodCategory

        public food[] GetFoodsList()
        {
            return new AdminDA().GetFoodsList();
        }

        public food GetFood(string name)
        {
            return new AdminDA().GetFood(name);
        }

        public food GetFood(int foodId)
        {
            return new AdminDA().GetFood(foodId);
        }


        public bool SaveFood(food food)
        {
            return new AdminDA().SaveFood(food);
        }

        public bool DeleteFood(int foodId)
        {
            return new AdminDA().DeleteFood(foodId);
        }

        public bool CheckDuplicateShopDepartmentName(int departmentId, string departmentName)
        {
            return new AdminDA().CheckDuplicateShopDepartmentName(departmentId, departmentName);
        }

        public shopdepartments[] GetShopDepartmentsList()
        {
            return new AdminDA().GetShopDepartmentsList();
        }

        public shopdepartments GetShopDepartment(int departmentId)
        {
            return new AdminDA().GetShopDepartment(departmentId);
        }

        public bool DeleteShopDepartment(int shopDepartmentId)
        {
            return new AdminDA().DeleteShopDepartment(shopDepartmentId);
        }

        public bool SaveShopDepartment(shopdepartments department)
        {
            return new AdminDA().SaveShopDepartment(department);
        }

        public bool ReorderShopDepartments(shopdepartments[] arr)
        {
            return new AdminDA().ReorderShopDepartments(arr);
        }

        //item
        public bool CheckDuplicateGeneralItemName(int departmentId, string departmentName)
        {
            return new AdminDA().CheckDuplicateGeneralItemName(departmentId, departmentName);
        }

        public generalitems[] GetGeneralItemsList()
        {
            return new AdminDA().GetGeneralItemsList();
        }

        public generalitems GetGeneralItem(int departmentId)
        {
            return new AdminDA().GetGeneralItem(departmentId);
        }

        public bool DeleteGeneralItem(int GeneralItemId)
        {
            return new AdminDA().DeleteGeneralItem(GeneralItemId);
        }

        public bool SaveGeneralItem(generalitems department)
        {
            return new AdminDA().SaveGeneralItem(department);
        }

        public bool ReorderGeneralItems(generalitems[] arr)
        {
            return new AdminDA().ReorderGeneralItems(arr);
        }
        //item

        public bool CheckDuplicateMeasurementUnitName(int unitId, string unitName)
        {
            return new AdminDA().CheckDuplicateMeasurementUnitName(unitId, unitName);
        }

        public measurementunits[] GetMeasurementUnitsList()
        {
            return new AdminDA().GetMeasurementUnitsList();
        }

        public measurementunits GetMeasurementUnit(int unitId)
        {
            return new AdminDA().GetMeasurementUnit(unitId);
        }

        public bool DeleteMeasurementUnit(int unitId)
        {
            return new AdminDA().DeleteMeasurementUnit(unitId);
        }

        public bool SaveMeasurementUnit(measurementunits unit)
        {
            return new AdminDA().SaveMeasurementUnit(unit);
        }

        public bool ReorderMeasurementUnits(measurementunits[] arr)
        {
            return new AdminDA().ReorderMeasurementUnits(arr);
        }

        public measurementunitsconverts[] GetMeasurementUnitsConvertList()
        {
            return new AdminDA().GetMeasurementUnitsConvertList();
        }

        public bool SaveMeasurementUnitsConvert(measurementunitsconverts unit)
        {
            return new AdminDA().SaveMeasurementUnitsConvert(unit);
        }

        #endregion Admin

        public measurementunitsconverts GetMeasurementUnitsConvert(int ConvertId)
        {
            return new AdminDA().GetMeasurementUnitsConvert(ConvertId);
        }

        public int GetNextTempUser(int anonymous)
        {
            return new AdminDA().GetNextTempUser(anonymous);
        }

        public users GetUserByName(string username)
        {
            return new AdminDA().GetUserByName(username);
        }

        public users GetUser(int Id)
        {
            return new AdminDA().GetUser(Id);
        }

        public users GetUser(string userName, string password)
        {
            return new AdminDA().GetUser(userName, password);
        }

        public users GetUserEx(int Id)
        {
            return new AdminDA().GetUserEx(Id);
        }

        public int GetUsersNum()
        {
            return new AdminDA().GetUsersNum();
        }



        public bool SaveUser(users currUser)
        {
            return new AdminDA().SaveUser(currUser);
        }

        public foodcategories GetFoodCategoryByName(string name)
        {
            return new AdminDA().GetFoodCategoryByName(name);
        }

        public mealrecipes[] GetMealRecipes(int mealId)
        {
            return new MealsDA().GetMealRecipes(mealId);
        }

        public menus[] GetMenusList(int userId, int tempUser)
        {
            return new MenusDA().GetMenusList(userId, tempUser);
        }

        public bool UpdateMenuUser(int menuId, int userId)
        {
            return new MenusDA().UpdateMenuUser(menuId, userId);
        }



        public int GetMenuMaxDay(int menuId)
        {
            return new MenusDA().GetMenuMaxDay(menuId);
        }

        public int? GetMenuUserId(int menuId)
        {
            return new MenusDA().GetMenuUserId(menuId);
        }

        public int? GetMenuTempUserId(int menuId)
        {
            return new MenusDA().GetMenuTempUserId(menuId); 
        }

        public int GetMenusNum()
        {
            return new MenusDA().GetMenusNum();
        }

        public void ReplaceUserIds(int sourceUserId, int targetUserId)
        {
            new AdminDA().ReplaceUserIds(sourceUserId, targetUserId);
        }

        public IEnumerable<recipes> GetRecipes(RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            return new RecipesDA().GetRecipes(orderBy, page, pageSize, out totalPages);
        }

        public List<recipes> GetRecipesEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? servings, int[] recipeCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numRecipes)
        {
            return new RecipesDA().GetRecipesEx(display, userId, freeText, categoryId, servings, recipeCats, orderBy, page, pageSize, out totalPages, out numRecipes);
        }

        public IEnumerable<menus> GetMenus(int userid, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            return new MenusDA().GetMenus(userid, orderBy, page, pageSize, out totalPages);
        }

        //public int? GetRecipeUserFavoritesCount(int recipeId)
        //{
        //    return new RecipesDA().GetRecipeUserFavoritesCount(recipeId);
        //}

        public int? GetRecipeMenusCount(int recipeId)
        {
            return new RecipesDA().GetRecipeMenusCount(recipeId);
        }

        public meals[] GetMenuMeals(int menuId)
        {
            return new MenusDA().GetMenuMeals(menuId);
        }

        public MBLSettingsWrapper GetMBLSettingsWrapper()
        {
            return new SettingsDA().GetMBLSettingsWrapper();
        }

        public articles GetArticleById(int articleId)
        {
            return new AdminDA().GetArticleById(articleId);
        }

        public articles[] GetArticlesList()
        {
            return new AdminDA().GetArticlesList();
        }

        public bool AddMenuToUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            return new MenusDA().AddMenuToUserFavorites(userId, menuId , out favMenusNum);
        }

        public bool RemoveMenuFromUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            return new MenusDA().RemoveMenuFromUserFavorites(userId, menuId, out favMenusNum);
        }

        public IEnumerable<menus> GetMenusEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? diners, int[] menuCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numMenus)
        {
            return new MenusDA().GetMenusEx(display, userId, freeText, categoryId, diners, menuCats, orderBy, page, pageSize, out totalPages, out numMenus);
        }

        public mcategories[] GetMenusCategoriesList(int userId)
        {
            return new MenusDA().GetMenusCategoriesList(userId);
        }

        public bool SaveMBLSettingsRecentItem(string recentItems, string itemType)
        {
            return new SettingsDA().SaveMBLSettingsRecentItem(recentItems, itemType);
        }

        public bool CreateOrUpdateArticle(int id, string title, string abs, string body, string publisher, out int returnedId)
        {
            return new AdminDA().CreateOrUpdateArticle(id, title, abs, body, publisher, out returnedId);
        }

        public void CheckShoppingListItem(int userId, int foodId, bool active)
        {
            new ShoppingsListDA().CheckShoppingListItem(userId, foodId, active);
        }

        //public IQueryable<RecipesView> GetRecipes(string searchValue, int userId)
        //{
        //    return new RecipesDA().GetRecipes(searchValue, userId);
        //}

        public void AddRecipeToShoppingList(int userId, int recipeId)
        {
            new RecipesDA().AddRecipeToShoppingList(userId, recipeId);
        }

        public IQueryable<recipesinshoppinglist> GetSelectedRecipes(int userId)
        {
            return new RecipesDA().GetSelectedRecipes(userId);
        }

        public void RemoveRecipeFromShoppingList(int userId, int recipeId)
        {
            new RecipesDA().RemoveRecipeFromShoppingList(userId, recipeId);
        }

        public void AddMenuToShoppingList(int userId, int menuId, bool check)
        {
            new MenusDA().AddMenuToShoppingList(userId, menuId, check);
        }

        //public IQueryable<MenusInShoppingList> GetMenusInShoppingList(int userId)
        //{
        //    return new MenusDA().GetMenusInShoppingList(userId);
        //}

        public void RemoveMenuFromShoppingList(int userId, int menuId)
        {
            new MenusDA().RemoveMenuFromShoppingList(userId, menuId);
        }

        //public void UpdateSaveList(int listId, bool shoppingList)
        //{
        //    new SavedListDA().UpdateSaveList(listId, shoppingList);
        //}

        public void UpdateMissingListItem(int id, int quantity)
        {
            new MissingItemsListDA().UpdateMissingListItem(id, quantity);
        }

        //public void UpdateSavedListItem(int id, int quantity)
        //{
        //    new SavedListDA().UpdateSavedListItem(id, quantity);
        //}

        public IEnumerable<recipes> SearchRecipes(string searchedText)
        {
            return new RecipesDA().SearchRecipes(searchedText);
        }
    }
}
