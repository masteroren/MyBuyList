using AutoMapper;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class IngredientComparer : IEqualityComparer<ingredients>
    {
        public bool Equals(ingredients x, ingredients y)
        {
            return x != null & y != null && x.IngredientId.Equals(y.IngredientId);
        }

        public int GetHashCode(ingredients obj)
        {
            return obj.IngredientId;
        }
    }

    class RecipesDA : BaseContextDataAdapter<mybuylistEntities>
    {
        public const int USER_ADMIN = 1;

        internal recipes[] GetRecipesListByFoodId(int foodId)
        {
            using (DataContext)
            {
                try
                {

                    var list = (from r in DataContext.recipes
                                join i in DataContext.ingredients.Where(i => i.food.FoodId == foodId) on r.RecipeId equals i.RecipeId
                                select r).Distinct();

                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal IQueryable<recipes> GetRecipesListByFreeText(string freeText)
        {
            IQueryable<recipes> recipes = DataContext.recipes.Where(p => p.RecipeName.Contains(freeText));
            return recipes;
        }

        //internal recipes[] GetrecipesListByComplexSearch(string freeText, int? servings, int[] recipeCats, int userId)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            var ingredients = from r in DataContext.recipes.Where(r => r.IsPublic == true || r.UserId == userId || userId == USER_ADMIN)
        //                              join i in DataContext.ingredients.Where(i => (i.food.FoodName.Trim().IndexOf(freeText.Trim()) != -1) ||
        //                                                                           (i.Remarks.Trim().IndexOf(freeText.Trim()) != -1)
        //                                                                     ) on r.RecipeId equals i.RecipeId
        //                              select r.RecipeId;

        //            var recipes = from r1 in DataContext.recipes.Where(r => (r.IsPublic == true || r.UserId == userId) &&
        //                                                  ((r.RecipeName.Trim().IndexOf(freeText.Trim()) != -1) ||
        //                                                   (r.Remarks.Trim().IndexOf(freeText.Trim()) != -1) ||
        //                                                   (r.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1) ||
        //                                                    (r.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1) ||
        //                                                    (r.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
        //                                                   ))
        //                          select r1.RecipeId;

        //            var list2 = from a in DataContext.recipes
        //                        from b in a.Categories
        //                        where (a.IsPublic == true || a.UserId == userId || userId == USER_ADMIN) && b.CategoryName.Trim().IndexOf(freeText.Trim()) != -1
        //                        select a.RecipeId;


        //            var tmpList = recipes.Union(ingredients);
        //            var list1 = tmpList.Union(list2);

        //            var list = DataContext.recipes.Where(r => list1.Contains(r.RecipeId));

        //            if (servings.HasValue)
        //            {
        //                list = list.Where(r => r.Servings >= servings.Value);
        //            }

        //            if (recipeCats != null && recipeCats.Length != 0)
        //            {
        //                List<recipes> temp = new List<recipes>();

        //                foreach (int categoryId in recipeCats)
        //                {

        //                    var recipesByCategory = from a in DataContext.categories
        //                                            from b in a.recipes
        //                                            where a.CategoryId == categoryId || a.ParentCategoryId == categoryId
        //                                            select b;

        //                    foreach (var recipe in recipesByCategory)
        //                    {
        //                        if (!temp.Contains(recipe))
        //                        {
        //                            temp.Add(recipe);
        //                        }
        //                    }

        //                    //foreach (var rcat in DataContext.Categories.Where(rc => rc.CategoryId == categoryId ||
        //                    //                                                                        rc.Category.ParentCategoryId == categoryId))
        //                    //{
        //                    //    if (!temp.Contains(rcat.Recipe))
        //                    //    {
        //                    //        temp.Add(rcat.Recipe);
        //                    //    }
        //                    //}
        //                }

        //                list = (from r in list
        //                        where temp.Contains(r)
        //                        select r);
        //            }

        //            return list.ToArray();
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        internal recipes[] GetUserRecipesList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.recipes.Where(r => r.UserId == userId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        //internal recipes[] GetUserFavoritesRecipes(int userId)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            var list = from a in DataContext.recipes
        //                       from b in a.users
        //                       where b.UserId == userId
        //                       select a;

        //            return list.ToArray();
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        //internal int? GetRecipeUserFavoritesCount(int recipeId)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            var list = from a in DataContext.users
        //                       from b in a.recipes1
        //                       select a;

        //            return list.ToArray().Length;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        internal categories[] GetRecipesCategoriesList()
        {
            return DataContext.categories.OrderBy(cat => cat.SortOrder).ToArray();
        }

        internal recipes[] GetRecipesByCategory(int categoryId, int userId)
        {
            //try
            //{
            //    var list = (from a in DataContext.recipes
            //                from b in a.categories
            //                where b.CategoryId == categoryId
            //                select a).ToArray();

            //    return list;
            //}
            //catch
            //{
                return null;
            //}
        }

        //private recipes[] GetRecipesByCategory(mybuylistEntities dc, int categoryId, int userId)
        //{
        //    try
        //    {
        //        var list = from a in DataContext.recipes
        //                   from b in a.categories
        //                   where (a.IsPublic == true || a.UserId == userId || userId == USER_ADMIN)
        //                   select a;

        //        return (from r in dc.recipes
        //                where list.Contains(r)
        //                select r).ToArray();

        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        internal recipes[] GetRecipesList()
        {
            recipes[] list = (from r in DataContext.recipes select r).ToArray();
            return list.ToArray();
        }

        internal void AllowRecipe(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    recipes recipe = DataContext.recipes.Single(r => r.RecipeId == recipeId);
                    recipe.IsApproved = !recipe.IsApproved;
                    DataContext.SaveChanges();
                }
                catch
                {

                }
            }
        }

        internal recipes GetRecipe(int recipeId)
        {
            recipes recipe = DataContext.recipes.SingleOrDefault(r => r.RecipeId == recipeId);

            //if (recipe != null)
            //{
            //    UserFavoriteRecipe ufrep = DataContext.UserFavoriterecipes.SingleOrDefault(ufr => ufr.RecipeId == recipe.RecipeId &&
            //                                                                                      ufr.UserId == recipe.UserId);
            //    recipe.ShowInFavorites = (ufrep != null);

            //    //recipe.SHOPPING_LIST = recipe.recipesInShoppingLists.Any();
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

        internal ingredients[] GetRecipeIngredientsList(int recipeId)
        {
            try
            {
                List<ingredients> ingredients = DataContext.ingredients.Where((ingredients ri) => ri.RecipeId == recipeId).ToList();
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
                    return (from e in DataContext.food.Where(f => f.FoodName.StartsWith(prefixText))
                               select new { id = e.FoodId, name = e.FoodName }).ToDictionary(k=>k.id, v=> v.name);
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool AddRecipeToUserFavorites(int userId, int recipeId, out int favrecipesNum)
        {
            using (DataContext)
            {
                try
                {

                    //UserFavoriteRecipe ufrep = DataContext.UserFavoriterecipes.SingleOrDefault(ufr => ufr.UserId == userId &&
                    //                                                                                  ufr.RecipeId == recipeId);

                    var favoriteRecipe = from a in DataContext.users
                                         from b in a.recipes1
                                         where a.UserId == userId && b.RecipeId == recipeId
                                         select a;


                    //if (favoriteRecipe == null)
                    //{
                    //    favoriteRecipe = new UserFavoriteRecipe();
                    //    favoriteRecipe.UserId = userId;
                    //    favoriteRecipe.RecipeId = recipeId;
                    //    DataContext.UserFavoriterecipes.InsertOnSubmit(favoriteRecipe);
                    //    DataContext.SubmitChanges();
                    //}

                    //favrecipesNum = DataContext.UserFavoriterecipes.Where(ufr => ufr.UserId == userId && (ufr.Recipe.UserId == userId || ufr.Recipe.IsPublic)).Count();
                    favrecipesNum = 0;
                    return true;
                }
                catch
                {
                    favrecipesNum = 0;
                    return false;
                }
            }
        }

        //internal bool RemoveUserFavoritesRecipe(int userId, int recipeId, out int favrecipesNum)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            //UserFavoriteRecipe ufrep = DataContext.UserFavoriterecipes.Single(ufr => ufr.UserId == userId &&
        //            //                                                                         ufr.RecipeId == recipeId);



        //            var userFavoriteRecipe = DataContext.recipes.Single(p => p.RecipeId == recipeId);
        //            users user = DataContext.users.Single(a => a.UserId == userId);
        //            user.recipes1.Remove(userFavoriteRecipe);
        //            DataContext.SaveChanges();

        //            //DataContext.UserFavoriterecipes.DeleteOnSubmit(ufrep);
        //            //DataContext.SubmitChanges();

        //            //favrecipesNum = DataContext.UserFavoriterecipes.Where(ufr => ufr.UserId == userId && (ufr.Recipe.UserId == userId || ufr.Recipe.IsPublic)).Count();

        //            favrecipesNum = (from a in DataContext.recipes
        //                             from b in a.users
        //                             where b.UserId == userId && a.IsPublic
        //                             select a).Count();

        //            return true;
        //        }
        //        catch
        //        {
        //            favrecipesNum = 0;
        //            return false;
        //        }
        //    }
        //}

        internal int DeleteRecipe(int recipeId)
        {
            recipes recipe = DataContext.recipes.SingleOrDefault(r => r.RecipeId == recipeId);
            if (recipe != null)
            {
                DataContext.recipes.Remove(recipe);
                return DataContext.SaveChanges();
            }
            return 0;
        }

        internal bool UpdateRecipe(recipes recipe, List<ingredients> ingridiants, List<SRL_RecipeCategory> categories)
        {
            AddNewFoodItems(ingridiants, recipe.UserId);

            recipe.ModifiedDate = DateTime.Now;

           

            ingridiants.ForEach(item =>
            {
                ingredients t = DataContext.ingredients.SingleOrDefault(x => x.IngredientId == item.IngredientId && x.RecipeId == item.RecipeId);
                if (t != null)
                {
                    t.FoodId = item.FoodId;
                    t.MeasurementUnitId = item.MeasurementUnitId;
                    t.Quantity = item.Quantity;
                }
                else
                {
                    DataContext.ingredients.Add(item);
                }
            });

            IEnumerable<ingredients> p = DataContext.ingredients.Where((ingredients i) => i.RecipeId == recipe.RecipeId);
            IEnumerable<ingredients> s = p.Except(ingridiants, new IngredientComparer());
            DataContext.ingredients.RemoveRange(s);

            recipes r = DataContext.recipes.SingleOrDefault(x => x.RecipeId == recipe.RecipeId);

            //r.categories.Clear();
            //foreach (SRL_RecipeCategory cat in categories)
            //{
            //    categories c = GetCategory(cat.CategoryId);
            //    r.categories.Add(c);
            //}

            IEnumerable<string> propertyNames = DataContext.Entry<recipes>(r).CurrentValues.PropertyNames;
            foreach(string propertyName in propertyNames)
            {
                DataContext.Entry<recipes>(r).Property(propertyName).CurrentValue = DataContext.Entry<recipes>(recipe).Property(propertyName).CurrentValue;
            }

            DataContext.SaveChanges();

            return true;
        }

        internal bool SaveRecipe(recipes recipe, List<ingredients> ingridiants, List<SRL_RecipeCategory> categories, out int recipeId)
        {

            //string Source = "MyBuyList";
            //if (!EventLog.SourceExists(Source))
            //{
            //    EventLog.CreateEventSource(Source, "Application");
            //}
            //EventLog.WriteEntry(Source, "Save recipes", EventLogEntryType.Information);

            using (DataContext)
            {
                try
                {
                    AddNewFoodItems(ingridiants, recipe.UserId);

                    recipe.CreatedDate = DateTime.Now;
                    int max = 0;

                    if (DataContext.recipes.Where(r => r.UserId == recipe.UserId).Count() > 0)
                    {
                        max = DataContext.recipes.Where(r => r.UserId == recipe.UserId).Max(r => r.SortOrder);
                        recipe.SortOrder = max + 1;
                    }

                    recipe.ModifiedDate = DateTime.Now;

                    DataContext.recipes.Add(recipe);
                    DataContext.SaveChanges();

                    recipeId = recipe.RecipeId;

                    recipes savedRecipe = GetRecipe(recipeId);
                    foreach (SRL_RecipeCategory cat in categories)
                    {
                        categories category = GetCategory(cat.CategoryId);
                        //category.recipes.Add(savedRecipe);
                    }

                    ingridiants.ForEach(item =>
                    {
                        item.RecipeId = recipe.RecipeId;
                        DataContext.ingredients.Add(item);
                    });

                    DataContext.SaveChanges();

                    //Show recipe in favorites
                    //UserFavoriteRecipe ufrep = DataContext.UserFavoriterecipes.SingleOrDefault(fr => fr.RecipeId == recipe.RecipeId &&
                    //                                                                                 fr.UserId == recipe.UserId);
                    //if (recipe.ShowInFavorites)
                    //{
                    //    if (ufrep == null)
                    //    {
                    //        ufrep = new UserFavoriteRecipe();
                    //        ufrep.RecipeId = recipe.RecipeId;
                    //        ufrep.UserId = recipe.UserId;
                    //        DataContext.UserFavoriterecipes.InsertOnSubmit(ufrep);   v
                    //        DataContext.SubmitChanges();
                    //    }
                    //}
                    //else
                    //{
                    //    if (ufrep != null)
                    //    {
                    //        DataContext.UserFavoriterecipes.DeleteOnSubmit(ufrep);
                    //        DataContext.SubmitChanges();
                    //    }
                    //}

                    return true;
                }
                catch (Exception ex)
                {
                    //if (!EventLog.SourceExists(Source))
                    //{
                    //    EventLog.CreateEventSource(Source, "Application");
                    //}
                    //EventLog.WriteEntry(Source, ex.Message, EventLogEntryType.Error);

                    recipeId = 0;
                    return false;
                }
            }
        }

        private categories GetCategory(int categoryId)
        {
            try
            {
                return DataContext.categories.SingleOrDefault(x => x.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void AddNewFoodItems(List<ingredients> ingridiants, int userId)
        {
            // Foods
            foreach (ingredients ing in ingridiants)
            {
                //find food by name for getting FoodId
                food food = DataContext.food.SingleOrDefault((food f) => f.FoodId == ing.FoodId);

                if (food != null)
                {
                    ing.FoodId = food.FoodId;
                }
                else
                {
                    //Create new food and mark it temporary
                    food = new food();
                    food.FoodName = ing.food.FoodName;
                    food.IsTemporary = true;
                    food.FoodCategoryId = 0;
                    food.CalculateUnitId = 0;
                    food.CreatedBy = userId;
                    food.CreatedDate = DateTime.Now;
                    food.ModifiedBy = userId;
                    food.ModifiedDate = DateTime.Now;

                    DataContext.food.Add(food);
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
                    recipes recipe = DataContext.recipes.Single(r => r.RecipeId == recipeId);
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
        //            Recipe recipe = DataContext.recipes.Single(r => r.RecipeId == recipeId);
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

        internal food GetFood(int id)
        {
            using (DataContext)
            {
                try
                {
                    food item = DataContext.food.SingleOrDefault(f => f.FoodId == id);

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
                    return DataContext.recipes.Count();
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

            int ingredientsCount = DataContext.ingredients.Where(r => r.RecipeId == recipeId).Count();
            var measureUnitConverts = DataContext.measurementunitsconverts;

            var query = from r in DataContext.recipes.Where(r => r.RecipeId == recipeId && r.Servings > 0)
                        join i in DataContext.ingredients on r.RecipeId equals i.RecipeId
                        join f in DataContext.food on i.FoodId equals f.FoodId
                        join nv in DataContext.nutvalues on f.FoodId equals nv.FoodId
                        join ni in DataContext.nutitems on nv.NutItemId equals ni.NutItemId
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
                    measurementunitsconverts unitConvert = measureUnitConverts.SingleOrDefault(muc => muc.FoodId == row.FoodId &&
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
            var ingredientsList = from ing in DataContext.ingredients
                                  where ing.RecipeId == recipeId
                                  select ing;

            foreach (ingredients ingredient in ingredientsList)
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
                    measurementunitsconverts unitConvert = measureUnitConverts.SingleOrDefault((measurementunitsconverts muc) => muc.FoodId == ingredient.FoodId &&
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
                        unitConvert = measureUnitConverts.SingleOrDefault((measurementunitsconverts muc) => muc.FoodId == ingredient.FoodId &&
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

        internal IEnumerable<recipes> GetRecipes(RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            using (DataContext)
            {
                try
                {
                    var count = (from r in DataContext.recipes
                                 select r).Count();

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from r in DataContext.recipes
                               select r;


                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(r => r.RecipeName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(r => r.users.DisplayName);
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

        internal List<recipes> GetRecipesEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? servings, int[] recipeCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numrecipes)
        {
            List<recipes> recipesList = null;

            switch (display)
            {
                case RecipeDisplayEnum.All:
                    recipesList = GetAllrecipes(userId).ToList();
                    break;
                case RecipeDisplayEnum.MyRecipes:
                    if (!string.IsNullOrEmpty(freeText))
                        recipesList = GetUserRecipesList(userId).Where(r => r.RecipeName.Contains(freeText)).ToList();
                    else
                        recipesList = GetUserRecipesList(userId).ToList();
                    break;
                //case RecipeDisplayEnum.MyFavoriteRecipes:
                //    if (!string.IsNullOrEmpty(freeText))
                //        recipesList = GetUserFavoritesRecipes(userId).Where(r => r.RecipeName.Contains(freeText)).ToList();
                //    else
                //        recipesList = GetUserFavoritesrecipes(userId).ToList();
                //    break;
                //case RecipeDisplayEnum.ByCategory:
                //    if (categoryId.HasValue)
                //    {
                //        recipesList = GetrecipesByCategory(categoryId.Value, userId).ToList();
                //    }
                //    else
                //    {
                //        recipesList = this.GetAllrecipes(userId).ToList();
                //    }
                //    break;
                case RecipeDisplayEnum.BySearchSimple:
                    recipesList = GetRecipesListByFreeText(freeText).ToList();
                    break;
                //case RecipeDisplayEnum.BySearchAdvanced:
                //    recipesList = this.GetrecipesListByComplexSearch(freeText, servings, recipeCats, userId).ToList();
                //    break;
            }

            var count = (from r in recipesList
                         select r).Count();

            numrecipes = count;

            totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

            var list = from r in recipesList
                       select r;


            switch (orderBy)
            {
                case RecipeOrderEnum.Name:
                    list = list.OrderBy(r => r.RecipeName);
                    break;
                case RecipeOrderEnum.Publisher:
                    list = list.OrderBy(r => r.users.DisplayName);
                    break;
                case RecipeOrderEnum.LastUpdate:
                    list = list.OrderByDescending(r => r.ModifiedDate);
                    break;
            }

            // paging
            var list2 = list.Skip((page - 1) * pageSize).Take(pageSize);

            return list2.ToList();
        }

        internal List<recipes> GetAllrecipes(int userId)
        {
            return (from r in DataContext.recipes.Where(r => r.IsPublic || r.UserId == userId || userId == USER_ADMIN) select r).ToList();
        }

        internal int? GetRecipeMenusCount(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    var list = from a in DataContext.menus
                               from b in a.recipes
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


        //internal IQueryable<recipesView> Getrecipes(string searchValue, int userId)
        //{
        //    IQueryable<recipesView> recipes = DataContext.recipesViews.Where(p => p.RecipeName.Contains(searchValue));
        //    return recipes;
        //}

        internal void AddRecipeToShoppingList(int userId, int recipeId)
        {
            recipesinshoppinglist recipesInShoppingList = DataContext.recipesinshoppinglist.SingleOrDefault(p => p.USER_ID == userId && p.RECIPE_ID == recipeId);

            if (recipesInShoppingList == null)
            {
                recipes recipe = DataContext.recipes.SingleOrDefault(p => p.RecipeId == recipeId);

                recipesInShoppingList = new recipesinshoppinglist()
                {
                    USER_ID = userId,
                    RECIPE_ID = recipeId,
                    SERVINGS = recipe.Servings
                };

                DataContext.recipesinshoppinglist.Add(recipesInShoppingList);
                DataContext.SaveChanges();
            }
        }

        internal IQueryable<recipesinshoppinglist> GetSelectedRecipes(int userId)
        {
            IQueryable<recipesinshoppinglist> recipesInShoppingList = DataContext.recipesinshoppinglist.Where(p => p.USER_ID == userId);
            return recipesInShoppingList;
        }

        internal void RemoveRecipeFromShoppingList(int userId, int recipeId)
        {
            recipesinshoppinglist recipesInShoppingList = DataContext.recipesinshoppinglist.SingleOrDefault(p => p.USER_ID == userId && p.RECIPE_ID == recipeId);
            DataContext.recipesinshoppinglist.Remove(recipesInShoppingList);
            DataContext.SaveChanges();
        }

        internal IEnumerable<recipes> SearchRecipes(string searchedText)
        {
            IEnumerable<recipes> result1 = DataContext.recipes.Where(p=>p.RecipeName.Contains(searchedText));
            IEnumerable<recipes> result2 = from p in DataContext.recipes
                                          join p1 in DataContext.ingredients on p.RecipeId equals p1.RecipeId
                                          where p1.food.FoodName.Contains(searchedText)
                                          select p;
            IEnumerable<recipes> recipes = result1.Concat(result2);

            return recipes;
        }
    }
}
