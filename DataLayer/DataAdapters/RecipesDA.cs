using AutoMapper;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class IngredientComparer : IEqualityComparer<Ingredient>
    {
        public bool Equals(Ingredient x, Ingredient y)
        {
            return x != null & y != null && x.IngredientId.Equals(y.IngredientId);
        }

        public int GetHashCode(Ingredient obj)
        {
            return obj.IngredientId;
        }
    }

    class RecipesDA : BaseContextDataAdapter<MyBuyListEntities1>
    {
        public const int USER_ADMIN = 1;

        internal Recipe[] GetRecipesListByFoodId(int foodId)
        {
            using (DataContext)
            {
                try
                {

                    var list = (from r in DataContext.Recipes
                                join i in DataContext.Ingredients.Where(i => i.Food.FoodId == foodId) on r.RecipeId equals i.RecipeId
                                select r).Distinct();

                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal IQueryable<Recipe> GetRecipesListByFreeText(string freeText)
        {
            IQueryable<Recipe> recipes = DataContext.Recipes.Where(p => p.RecipeName.Contains(freeText));
            return recipes;
        }

        internal Recipe[] GetRecipesListByComplexSearch(string freeText, int? servings, int[] recipeCats, int userId)
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<Recipe>(r => r.User);
                    //dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
                    //dlo.LoadWith<Recipe>(r => r.MenuRecipes);
                    //dlo.LoadWith<Recipe>(r => r.RecipeCategories);
                    //dlo.LoadWith<RecipeCategory>(rc => rc.Category);
                    //DataContext.LoadOptions = dlo;

                    var ingredients = from r in DataContext.Recipes.Where(r => r.IsPublic == true || r.UserId == userId || userId == USER_ADMIN)
                                      join i in DataContext.Ingredients.Where(i => (i.Food.FoodName.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                                                   (i.Remarks.Trim().IndexOf(freeText.Trim()) != -1)
                                                                             ) on r.RecipeId equals i.RecipeId
                                      select r.RecipeId;

                    var recipes = from r1 in DataContext.Recipes.Where(r => (r.IsPublic == true || r.UserId == userId) &&
                                                          ((r.RecipeName.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                           (r.Remarks.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                           (r.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                            (r.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1) ||
                                                            (r.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
                                                           ))
                                  select r1.RecipeId;

                    //var list2 = from r2 in DataContext.Recipes.Where(r => (r.IsPublic == true || r.UserId == userId || userId == USER_ADMIN))
                    //            join rc in DataContext.RecipeCategories.Where(rc => (rc.Category.CategoryName.Trim().IndexOf(freeText.Trim()) != -1))
                    //                                                                on r2.RecipeId equals rc.RecipeId
                    //            select r2.RecipeId;

                    var list2 = from a in DataContext.Recipes
                                from b in a.Categories
                                where (a.IsPublic == true || a.UserId == userId || userId == USER_ADMIN) && b.CategoryName.Trim().IndexOf(freeText.Trim()) != -1
                                select a.RecipeId;


                    //var list1 = recipes.Union(ingredients);
                    var tmpList = recipes.Union(ingredients);
                    var list1 = tmpList.Union(list2);

                    var list = DataContext.Recipes.Where(r => list1.Contains(r.RecipeId));

                    if (servings.HasValue)
                    {
                        list = list.Where(r => r.Servings >= servings.Value);
                    }

                    if (recipeCats != null && recipeCats.Length != 0)
                    {
                        List<Recipe> temp = new List<Recipe>();

                        foreach (int categoryId in recipeCats)
                        {

                            var recipesByCategory = from a in DataContext.Categories
                                                    from b in a.Recipes
                                                    where a.CategoryId == categoryId || a.ParentCategoryId == categoryId
                                                    select b;

                            foreach (var recipe in recipesByCategory)
                            {
                                if (!temp.Contains(recipe))
                                {
                                    temp.Add(recipe);
                                }
                            }

                            //foreach (var rcat in DataContext.Categories.Where(rc => rc.CategoryId == categoryId ||
                            //                                                                        rc.Category.ParentCategoryId == categoryId))
                            //{
                            //    if (!temp.Contains(rcat.Recipe))
                            //    {
                            //        temp.Add(rcat.Recipe);
                            //    }
                            //}
                        }

                        list = (from r in list
                                where temp.Contains(r)
                                select r);
                    }

                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Recipe[] GetUserRecipesList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<Recipe>(r => r.User);
                    //dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
                    //dlo.LoadWith<Recipe>(r => r.MenuRecipes);
                    //dlo.LoadWith<Recipe>(r => r.RecipeCategories);
                    //dlo.LoadWith<RecipeCategory>(rc => rc.Category);
                    //DataContext.LoadOptions = dlo;

                    var list = DataContext.Recipes.Where(r => r.UserId == userId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Recipe[] GetUserFavoritesRecipes(int userId)
        {
            using (DataContext)
            {
                try
                {
                    var list = from a in DataContext.Recipes
                               from b in a.Users1
                               where b.UserId == userId
                               select a;

                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int? GetRecipeUserFavoritesCount(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    var list = from a in DataContext.Users
                               from b in a.Recipes1
                               select a;

                    return list.ToArray().Length;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Category[] GetRecipesCategoriesList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.Categories.OrderBy(cat => cat.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Recipe[] GetRecipesByCategory(int categoryId, int userId)
        {
            using (DataContext)
            {
                return GetRecipesByCategory(DataContext, categoryId, userId);
            }
        }

        private Recipe[] GetRecipesByCategory(MyBuyListEntities1 dc, int categoryId, int userId)
        {
            try
            {
                var list = from a in DataContext.Recipes
                           from b in a.Categories
                           where (a.IsPublic == true || a.UserId == userId || userId == USER_ADMIN)
                           select a;

                return (from r in dc.Recipes
                        where list.Contains(r)
                        select r).ToArray();

            }
            catch
            {
                return null;
            }
        }

        internal Recipe[] GetRecipesList()
        {
            //DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<Recipe>(r => r.User);
            //DataContext.LoadOptions = dlo;

            Recipe[] list = (from r in DataContext.Recipes select r).ToArray();

            return list.ToArray();
        }

        internal void AllowRecipe(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);
                    recipe.IsApproved = !recipe.IsApproved;
                    DataContext.SaveChanges();
                }
                catch
                {

                }
            }
        }

        internal Recipe GetRecipe(int recipeId)
        {
            Recipe recipe = DataContext.Recipes.SingleOrDefault(r => r.RecipeId == recipeId);

            //if (recipe != null)
            //{
            //    UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.RecipeId == recipe.RecipeId &&
            //                                                                                      ufr.UserId == recipe.UserId);
            //    recipe.ShowInFavorites = (ufrep != null);

            //    //recipe.SHOPPING_LIST = recipe.RecipesInShoppingLists.Any();
            //}
            //else
            //{
            //    recipe.ShowInFavorites = false;
            //}

            return recipe;
        }

        //internal RecipeIngredientsView[] GetRecipeIngredientsViewList(int recipeId)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            var list = DataContext.RecipeIngredientsViews.Where(ri => ri.RecipeId == recipeId);
        //            return list.ToArray();
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        internal Ingredient[] GetRecipeIngredientsList(int recipeId)
        {
            try
            {
                List<Ingredient> ingredients = DataContext.Ingredients.Where((Ingredient ri) => ri.RecipeId == recipeId).ToList();
                return ingredients.ToArray();
            }
            catch
            {
                return null;
            }
        }

        internal Dictionary<int, string> GetFoodList(string prefixText)
        {
            using (DataContext)
            {
                try
                {
                    return (from e in DataContext.Food.Where(f => f.FoodName.StartsWith(prefixText))
                               select new { id = e.FoodId, name = e.FoodName }).ToDictionary(k=>k.id, v=> v.name);
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool AddRecipeToUserFavorites(int userId, int recipeId, out int favRecipesNum)
        {
            using (DataContext)
            {
                try
                {

                    //UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.UserId == userId &&
                    //                                                                                  ufr.RecipeId == recipeId);

                    var favoriteRecipe = from a in DataContext.Users
                                         from b in a.Recipes1
                                         where a.UserId == userId && b.RecipeId == recipeId
                                         select a;


                    //if (favoriteRecipe == null)
                    //{
                    //    favoriteRecipe = new UserFavoriteRecipe();
                    //    favoriteRecipe.UserId = userId;
                    //    favoriteRecipe.RecipeId = recipeId;
                    //    DataContext.UserFavoriteRecipes.InsertOnSubmit(favoriteRecipe);
                    //    DataContext.SubmitChanges();
                    //}

                    //favRecipesNum = DataContext.UserFavoriteRecipes.Where(ufr => ufr.UserId == userId && (ufr.Recipe.UserId == userId || ufr.Recipe.IsPublic)).Count();
                    favRecipesNum = 0;
                    return true;
                }
                catch
                {
                    favRecipesNum = 0;
                    return false;
                }
            }
        }

        internal bool RemoveUserFavoritesRecipe(int userId, int recipeId, out int favRecipesNum)
        {
            using (DataContext)
            {
                try
                {
                    //UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.Single(ufr => ufr.UserId == userId &&
                    //                                                                         ufr.RecipeId == recipeId);



                    var userFavoriteRecipe = DataContext.Recipes.Single(p => p.RecipeId == recipeId);
                    User user = DataContext.Users.Single(a => a.UserId == userId);
                    user.Recipes1.Remove(userFavoriteRecipe);
                    DataContext.SaveChanges();

                    //DataContext.UserFavoriteRecipes.DeleteOnSubmit(ufrep);
                    //DataContext.SubmitChanges();

                    //favRecipesNum = DataContext.UserFavoriteRecipes.Where(ufr => ufr.UserId == userId && (ufr.Recipe.UserId == userId || ufr.Recipe.IsPublic)).Count();

                    favRecipesNum = (from a in DataContext.Recipes
                                     from b in a.Users1
                                     where b.UserId == userId && a.IsPublic
                                     select a).Count();

                    return true;
                }
                catch
                {
                    favRecipesNum = 0;
                    return false;
                }
            }
        }

        internal int DeleteRecipe(int recipeId)
        {
            Recipe recipe = DataContext.Recipes.SingleOrDefault(r => r.RecipeId == recipeId);
            if (recipe != null)
            {
                DataContext.Recipes.Remove(recipe);
                return DataContext.SaveChanges();
            }
            return 0;
        }

        internal bool UpdateRecipe(Recipe recipe, List<Ingredient> ingridiants, List<SRL_RecipeCategory> categories)
        {
            AddNewFoodItems(ingridiants, recipe.UserId);

            recipe.ModifiedDate = DateTime.Now;

           

            ingridiants.ForEach(item =>
            {
                Ingredient t = DataContext.Ingredients.SingleOrDefault(x => x.IngredientId == item.IngredientId && x.RecipeId == item.RecipeId);
                if (t != null)
                {
                    t.FoodId = item.FoodId;
                    t.MeasurementUnitId = item.MeasurementUnitId;
                    t.Quantity = item.Quantity;
                }
                else
                {
                    DataContext.Ingredients.Add(item);
                }
            });

            IEnumerable<Ingredient> p = DataContext.Ingredients.Where((Ingredient i) => i.RecipeId == recipe.RecipeId);
            IEnumerable<Ingredient> s = p.Except(ingridiants, new IngredientComparer());
            DataContext.Ingredients.RemoveRange(s);

            Recipe r = DataContext.Recipes.SingleOrDefault(x => x.RecipeId == recipe.RecipeId);

            r.Categories.Clear();
            foreach (SRL_RecipeCategory cat in categories)
            {
                Category c = GetCategory(cat.CategoryId);
                r.Categories.Add(c);
            }

            IEnumerable<string> propertyNames = DataContext.Entry<Recipe>(r).CurrentValues.PropertyNames;
            foreach(string propertyName in propertyNames)
            {
                DataContext.Entry<Recipe>(r).Property(propertyName).CurrentValue = DataContext.Entry<Recipe>(recipe).Property(propertyName).CurrentValue;
            }

            DataContext.SaveChanges();

            return true;
        }

        internal bool SaveRecipe(Recipe recipe, List<Ingredient> ingridiants, List<SRL_RecipeCategory> categories, out int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    AddNewFoodItems(ingridiants, recipe.UserId);

                    recipe.CreatedDate = DateTime.Now;
                    int max = 0;

                    if (DataContext.Recipes.Where(r => r.UserId == recipe.UserId).Count() > 0)
                    {
                        max = DataContext.Recipes.Where(r => r.UserId == recipe.UserId).Max(r => r.SortOrder);
                        recipe.SortOrder = max + 1;
                    }

                    recipe.ModifiedDate = DateTime.Now;

                    DataContext.Recipes.Add(recipe);
                    DataContext.SaveChanges();

                    recipeId = recipe.RecipeId;

                    Recipe savedRecipe = GetRecipe(recipeId);
                    foreach (SRL_RecipeCategory cat in categories)
                    {
                        Category category = GetCategory(cat.CategoryId);
                        category.Recipes.Add(savedRecipe);
                    }

                    ingridiants.ForEach(item =>
                    {
                        item.RecipeId = recipe.RecipeId;
                        DataContext.Ingredients.Add(item);
                    });

                    DataContext.SaveChanges();

                    //Show recipe in favorites
                    //UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.SingleOrDefault(fr => fr.RecipeId == recipe.RecipeId &&
                    //                                                                                 fr.UserId == recipe.UserId);
                    //if (recipe.ShowInFavorites)
                    //{
                    //    if (ufrep == null)
                    //    {
                    //        ufrep = new UserFavoriteRecipe();
                    //        ufrep.RecipeId = recipe.RecipeId;
                    //        ufrep.UserId = recipe.UserId;
                    //        DataContext.UserFavoriteRecipes.InsertOnSubmit(ufrep);
                    //        DataContext.SubmitChanges();
                    //    }
                    //}
                    //else
                    //{
                    //    if (ufrep != null)
                    //    {
                    //        DataContext.UserFavoriteRecipes.DeleteOnSubmit(ufrep);
                    //        DataContext.SubmitChanges();
                    //    }
                    //}

                    return true;
                }
                catch (Exception ex)
                {
                    recipeId = 0;
                    return false;
                }
            }
        }

        private Category GetCategory(int categoryId)
        {
            try
            {
                return DataContext.Categories.SingleOrDefault(x => x.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void AddNewFoodItems(List<Ingredient> ingridiants, int userId)
        {
            // Foods
            foreach (Ingredient ing in ingridiants)
            {
                //find food by name for getting FoodId
                Food food = DataContext.Food.SingleOrDefault((Food f) => f.FoodId == ing.FoodId);

                if (food != null)
                {
                    ing.FoodId = food.FoodId;
                }
                else
                {
                    //Create new food and mark it temporary
                    food = new Food();
                    food.FoodName = ing.Food.FoodName;
                    food.IsTemporary = true;
                    food.FoodCategoryId = 0;
                    food.CalculateUnitId = 0;
                    food.CreatedBy = userId;
                    food.CreatedDate = DateTime.Now;
                    food.ModifiedBy = userId;
                    food.ModifiedDate = DateTime.Now;

                    DataContext.Food.Add(food);
                    DataContext.SaveChanges();

                    ing.FoodId = food.FoodId;
                }
            }
        }

        internal bool UpdateRecipePreparationMethod(int recipeId, string preparationMethod)
        {
            using (DataContext)
            {
                try
                {
                    Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);
                    recipe.PreparationMethod = preparationMethod;
                    recipe.ModifiedDate = DateTime.Now;
                    DataContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        //internal bool SaveRecipeCategories(int recipeId, RecipeCategory[] categories)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);
        //            recipe.Categories.rem(recipe.RecipeCategories);
        //            DataContext.SubmitChanges();
        //            recipe.RecipeCategories.AddRange(categories);
        //            recipe.ModifiedDate = DateTime.Now;
        //            DataContext.SubmitChanges();
        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //}

        internal Food GetFood(int id)
        {
            using (DataContext)
            {
                try
                {
                    Food item = DataContext.Food.SingleOrDefault(f => f.FoodId == id);


                    return item;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int GetRecipesNum()
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.Recipes.Count();
                }
                catch
                {
                    return -1;
                }
            }
        }

        internal RecipeTotalNutValues[] GetRecipeTotalNutValues(int recipeId, out bool isCompleteCalculation)
        {
            isCompleteCalculation = false;

            int ingredientsCount = DataContext.Ingredients.Where(r => r.RecipeId == recipeId).Count();
            var measureUnitConverts = DataContext.MeasurementUnitsConverts;

            var query = from r in DataContext.Recipes.Where(r => r.RecipeId == recipeId && r.Servings > 0)
                        join i in DataContext.Ingredients on r.RecipeId equals i.RecipeId
                        join f in DataContext.Food on i.FoodId equals f.FoodId
                        join nv in DataContext.NutValues on f.FoodId equals nv.FoodId
                        join ni in DataContext.NutItems on nv.NutItemId equals ni.NutItemId
                        orderby ni.NutCategoryId, ni.NutItemId
                        select new
                        {
                            ni.NutItemId,
                            ni.NutItemName,
                            ni.DisplayUnit,
                            ni.NutCategoryId,
                            nv.Value,
                            r.Servings,
                            i.Quantity,
                            f.FoodId,
                            i.MeasurementUnitId,
                            i.IngredientId //added
                        };


            List<RecipeTotalNutValues> list = new List<RecipeTotalNutValues>();

            decimal totalRecipeWeight = 0;
            Dictionary<int, int> NutValuesIncludedInCalaculation = new Dictionary<int, int>();

            foreach (var row in query)
            {
                bool calculateDataFound = false;
                decimal convertRatio = 0;

                if (row.MeasurementUnitId == AppConstants.GRAMM_UNIT_ID)
                {
                    convertRatio = 1;
                    calculateDataFound = true;
                }
                else if (row.MeasurementUnitId == AppConstants.KILOGRAMM_UNIT_ID)
                {
                    convertRatio = 1000;
                    calculateDataFound = true;
                }
                else
                {
                    MeasurementUnitsConvert unitConvert = measureUnitConverts.SingleOrDefault(muc => muc.FoodId == row.FoodId &&
                                                                                              muc.FromUnitId == row.MeasurementUnitId &&
                                                                                              muc.ToUnitId == AppConstants.GRAMM_UNIT_ID);
                    if (unitConvert != null)
                    {
                        if (unitConvert.FromQuantity > 0)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity;
                            calculateDataFound = true;
                        }
                    }
                    else
                    {
                        unitConvert = measureUnitConverts.SingleOrDefault(muc => muc.FoodId == row.FoodId &&
                                                                          muc.FromUnitId == row.MeasurementUnitId &&
                                                                          muc.ToUnitId == AppConstants.KILOGRAMM_UNIT_ID);
                        if (unitConvert != null)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity * 1000;
                            calculateDataFound = true;
                        }
                    }
                }

                if (calculateDataFound)
                {
                    RecipeTotalNutValues item = new RecipeTotalNutValues
                    {
                        NutCategoryId = row.NutCategoryId,
                        NutItemId = row.NutItemId,
                        NutItemName = row.NutItemName,
                        DisplayUnit = row.DisplayUnit,
                        //TotalValue = (row.Value == null ? null : row.Quantity / row.Servings * convertRatio * row.Value / 100)
                        //calculate nutvalue for the whole recipe, not per serving as above:
                        TotalValue = (row.Value == null ? null : row.Quantity * convertRatio * (row.Value / 100))
                    };

                    //this if statement is not needed, since DisplayTotalValue should only be set after final TotalValue is determined.
                    if (item.TotalValue != null)
                    {
                        if (item.TotalValue != 0)
                        {
                            item.DisplayTotalValue = item.TotalValue.Value.ToString("F");
                        }
                        else
                        {
                            item.DisplayTotalValue = "0";
                        }
                    }

                    list.Add(item);

                    if (!NutValuesIncludedInCalaculation.Keys.Contains(row.IngredientId))
                    {
                        NutValuesIncludedInCalaculation.Add(row.IngredientId, 1);

                    }
                    else
                    {
                        NutValuesIncludedInCalaculation[row.IngredientId]++;
                    }
                }
            }
            // added code: calculate total "recipe mass":
            var ingredientsList = from ing in DataContext.Ingredients
                                  where ing.RecipeId == recipeId
                                  select ing;

            foreach (Ingredient ingredient in ingredientsList)
            {
                bool calculateDataFound = false;
                decimal convertRatio = 0;

                if (ingredient.MeasurementUnitId == AppConstants.GRAMM_UNIT_ID)
                {
                    convertRatio = 1;
                    calculateDataFound = true;
                }
                else if (ingredient.MeasurementUnitId == AppConstants.KILOGRAMM_UNIT_ID)
                {
                    convertRatio = 1000;
                    calculateDataFound = true;
                }
                else
                {
                    MeasurementUnitsConvert unitConvert = measureUnitConverts.SingleOrDefault((MeasurementUnitsConvert muc) => muc.FoodId == ingredient.FoodId &&
                                                                                              muc.FromUnitId == ingredient.MeasurementUnitId &&
                                                                                              muc.ToUnitId == MyBuyList.Shared.AppConstants.GRAMM_UNIT_ID);
                    if (unitConvert != null)
                    {
                        if (unitConvert.FromQuantity > 0)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity;
                            calculateDataFound = true;
                        }
                    }
                    else
                    {
                        unitConvert = measureUnitConverts.SingleOrDefault((MeasurementUnitsConvert muc) => muc.FoodId == ingredient.FoodId &&
                                                                          muc.FromUnitId == ingredient.MeasurementUnitId &&
                                                                          muc.ToUnitId == AppConstants.KILOGRAMM_UNIT_ID);
                        if (unitConvert != null)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity * 1000;
                            calculateDataFound = true;
                        }
                    }
                }

                if (calculateDataFound)
                {
                    totalRecipeWeight += (ingredient.Quantity * convertRatio);
                }
            }
            //end of added code.

            Dictionary<int, RecipeTotalNutValues> dict = new Dictionary<int, RecipeTotalNutValues>();
            foreach (RecipeTotalNutValues item in list)
            {
                if (!dict.ContainsKey(item.NutItemId))
                {
                    if (item.TotalValue != null)
                    {
                        dict.Add(item.NutItemId, item);
                    }
                }
                else
                {
                    dict[item.NutItemId].TotalValue += item.TotalValue;
                    //if (dict[item.NutItemId].TotalValue != null)
                    //{
                    //    if (dict[item.NutItemId].TotalValue.Value != 0)
                    //    {
                    //        dict[item.NutItemId].DisplayTotalValue = dict[item.NutItemId].TotalValue.Value.ToString("F");
                    //    }
                    //    else
                    //    {
                    //        dict[item.NutItemId].DisplayTotalValue = "0";
                    //    }
                    //}
                }
            }
            RecipeTotalNutValues[] array = dict.Values.ToArray();

            //divide each nutValue by the weight of the whole recipe in grams and then multiply by 100, to get the nutValues for a 100 grams of "recipe mass"...
            if (totalRecipeWeight != 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].TotalValue.HasValue)
                    {
                        decimal x = array[i].TotalValue.Value;
                        array[i].TotalValue = (x / totalRecipeWeight) * 100;

                        if (array[i].TotalValue != 0)
                        {
                            array[i].DisplayTotalValue = array[i].TotalValue.Value.ToString("F");
                        }
                        else
                        {
                            array[i].DisplayTotalValue = "0";
                        }
                    }
                }
            }

            if (ingredientsCount == NutValuesIncludedInCalaculation.Count())
            {
                isCompleteCalculation = true;

                foreach (KeyValuePair<int, int> pair in NutValuesIncludedInCalaculation)
                {
                    if (pair.Value < 26)
                    {
                        isCompleteCalculation = false;
                    }
                }
            }

            return array;
        }

        internal IEnumerable<Recipe> GetRecipes(RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            using (DataContext)
            {
                try
                {
                    var count = (from r in DataContext.Recipes
                                 select r).Count();

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from r in DataContext.Recipes
                               select r;


                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(r => r.RecipeName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(r => r.Users.DisplayName);
                            break;
                        case RecipeOrderEnum.LastUpdate:
                            list = list.OrderByDescending(r => r.ModifiedDate);
                            break;
                    }

                    // paging
                    var list2 = list.Skip((page - 1) * pageSize).Take(pageSize);

                    return list2.ToList();

                }
                catch
                {
                    totalPages = 0;
                    return null;
                }
            }
        }

        internal List<Recipe> GetRecipesEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? servings, int[] recipeCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numRecipes)
        {
            try
            {
                List<Recipe> recipesList = null;

                switch (display)
                {
                    case RecipeDisplayEnum.All:
                        recipesList = GetAllRecipes(userId).ToList();
                        break;
                    case RecipeDisplayEnum.MyRecipes:
                        if (!string.IsNullOrEmpty(freeText))
                            recipesList = GetUserRecipesList(userId).Where(r => r.RecipeName.Contains(freeText)).ToList();
                        else
                            recipesList = GetUserRecipesList(userId).ToList();
                        break;
                    case RecipeDisplayEnum.MyFavoriteRecipes:
                        if (!string.IsNullOrEmpty(freeText))
                            recipesList = GetUserFavoritesRecipes(userId).Where(r => r.RecipeName.Contains(freeText)).ToList();
                        else
                            recipesList = GetUserFavoritesRecipes(userId).ToList();
                        break;
                    case RecipeDisplayEnum.ByCategory:
                        if (categoryId.HasValue)
                        {
                            recipesList = this.GetRecipesByCategory(categoryId.Value, userId).ToList();
                        }
                        else
                        {
                            recipesList = this.GetAllRecipes(userId).ToList();
                        }
                        break;
                    case RecipeDisplayEnum.BySearchSimple:
                        recipesList = GetRecipesListByFreeText(freeText).ToList();
                        break;
                    case RecipeDisplayEnum.BySearchAdvanced:
                        recipesList = this.GetRecipesListByComplexSearch(freeText, servings, recipeCats, userId).ToList();
                        break;
                }

                var count = (from r in recipesList
                             select r).Count();

                numRecipes = count;

                totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                var list = from r in recipesList
                           select r;


                switch (orderBy)
                {
                    case RecipeOrderEnum.Name:
                        list = list.OrderBy(r => r.RecipeName);
                        break;
                    case RecipeOrderEnum.Publisher:
                        list = list.OrderBy(r => r.Users.DisplayName);
                        break;
                    case RecipeOrderEnum.LastUpdate:
                        list = list.OrderByDescending(r => r.ModifiedDate);
                        break;
                }

                // paging
                var list2 = list.Skip((page - 1) * pageSize).Take(pageSize);

                return list2.ToList();

            }
            catch
            {
                totalPages = 0;
                numRecipes = 0;
                return null;
            }
        }

        internal List<Recipe> GetAllRecipes(int userId)
        {
            try
            {
                return (from r in DataContext.Recipes.Where(r => r.IsPublic || r.UserId == userId || userId == USER_ADMIN) select r).ToList();
            }
            catch
            {
                return null;
            }
        }

        internal int? GetRecipeMenusCount(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    var list = from a in DataContext.Menus
                               from b in a.Recipes
                               where b.RecipeId == recipeId
                               select a;

                    return list.ToArray().Length;
                }
                catch
                {
                    return null;
                }
            }
        }


        //internal IQueryable<RecipesView> GetRecipes(string searchValue, int userId)
        //{
        //    IQueryable<RecipesView> recipes = DataContext.RecipesViews.Where(p => p.RecipeName.Contains(searchValue));
        //    return recipes;
        //}

        internal void AddRecipeToShoppingList(int userId, int recipeId)
        {
            RecipesInShoppingList recipesInShoppingList = DataContext.RecipesInShoppingList.SingleOrDefault(p => p.USER_ID == userId && p.RECIPE_ID == recipeId);

            if (recipesInShoppingList == null)
            {
                Recipe recipe = DataContext.Recipes.SingleOrDefault(p => p.RecipeId == recipeId);

                recipesInShoppingList = new RecipesInShoppingList()
                {
                    USER_ID = userId,
                    RECIPE_ID = recipeId,
                    SERVINGS = recipe.Servings
                };

                DataContext.RecipesInShoppingList.Add(recipesInShoppingList);
                DataContext.SaveChanges();
            }
        }

        internal IQueryable<RecipesInShoppingList> GetSelectedRecipes(int userId)
        {
            IQueryable<RecipesInShoppingList> recipesInShoppingList = DataContext.RecipesInShoppingList.Where(p => p.USER_ID == userId);
            return recipesInShoppingList;
        }

        internal void RemoveRecipeFromShoppingList(int userId, int recipeId)
        {
            RecipesInShoppingList recipesInShoppingList = DataContext.RecipesInShoppingList.SingleOrDefault(p => p.USER_ID == userId && p.RECIPE_ID == recipeId);
            DataContext.RecipesInShoppingList.Remove(recipesInShoppingList);
            DataContext.SaveChanges();
        }

        internal IEnumerable<Recipe> SearchRecipes(string searchedText)
        {
            IEnumerable<Recipe> result1 = DataContext.Recipes.Where(p=>p.RecipeName.Contains(searchedText));
            IEnumerable<Recipe> result2 = from p in DataContext.Recipes
                                          join p1 in DataContext.Ingredients on p.RecipeId equals p1.RecipeId
                                          where p1.Food.FoodName.Contains(searchedText)
                                          select p;
            IEnumerable<Recipe> recipes = result1.Concat(result2);

            return recipes;
        }
    }
}
