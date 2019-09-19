using MyBuyList.Shared;
using System;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class MealsDA : BaseContextDataAdapter<mybuylistEntities>
    {
        internal mealrecipes[] GetMealsWeeklyList(int menuId, int startDayIndex, int endDayIndex)
        {
            using (DataContext)
            {
                try
                {
                    var meals = DataContext.meals.Where(m => m.MenuId == menuId &&
                                                        m.DayIndex >= startDayIndex &&
                                                        m.DayIndex <= endDayIndex);

                    var list = from mr in DataContext.mealrecipes
                               join m in meals on mr.MealId equals m.MealId
                               select mr;
                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal coursetypes[] GetCourseTypes()
        {
            using (DataContext)
            {
                var list = DataContext.coursetypes.OrderBy(ct => ct.SortOrder);
                return list.ToArray();
            }
        }

        internal mealtypes[] GetMealTypes()
        {
            using (DataContext)
            {
                var list = DataContext.mealtypes.OrderBy(mt => mt.SortOrder);
                return list.ToArray();
            }
        }

        internal meals[] GetMealsList(int menuId)
        {
            using (DataContext)
            {
                var list = DataContext.meals.Where(m => m.MenuId == menuId);
                return list.ToArray();
            }
        }

        internal meals GetMeal(int mealId)
        {
            using (DataContext)
            {
                meals item = DataContext.meals.SingleOrDefault(m => m.MealId == mealId);
                return item;
            }
        }

        //internal meals GetMeal(int menuId, int dayIndex, int mealTypeId)
        //{
        //    using (DataContext)
        //    { 
        //        return GetMeal(menuId, dayIndex, mealTypeId);
        //    }
        //}

        //internal meals GetMeal(int menuId, int courseTypeId)
        //{
        //    using (DataContext)
        //    {
        //        return GetMeal(menuId, courseTypeId);
        //    }
        //}

        internal meals GetMeal(int menuId, int courseTypeId)
        {
            using (DataContext)
            {
                meals item = DataContext.meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                          m.CourseTypeId == courseTypeId);
                return item;
            }
        }

        internal meals GetMeal(int menuId, int dayIndex, int mealTypeId)
        {
            using (DataContext)
            {
                meals item = DataContext.meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                          m.DayIndex == dayIndex &&
                                                          m.MealTypeId == mealTypeId);
                return item;
            }
        }

        internal bool SaveMeal(int menuId, int courseTypeId, int? diners)
        {
            using (DataContext)
            {
                int mealId = 0;
                return SaveMeal(menuId, courseTypeId, diners, out mealId);
            }
        }

        internal bool SaveMeal(int menuId, int dayIndex, int mealTypeId, int? diners)
        {
            int mealId = 0;
            return SaveMeal(menuId, dayIndex, mealTypeId, diners, out mealId);
        }

        private bool SaveMeal(int menuId, int courseTypeId, int? diners, out int mealId)
        {
            using (DataContext)
            {
                meals item = DataContext.meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                      m.CourseTypeId == courseTypeId);

                int mealTypeId = 4; //Others
                return this.SaveMeal(item, menuId, mealTypeId, null, courseTypeId, diners, out mealId);
            }

        }

        private bool SaveMeal(int menuId, int dayIndex, int mealTypeId, int? diners, out int mealId)
        {
            meals item = DataContext.meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                         m.DayIndex == dayIndex &&
                                                         m.MealTypeId == mealTypeId);

            return this.SaveMeal(item, menuId, mealTypeId, dayIndex, null, diners, out mealId);
        }

        private bool SaveMeal(meals item, int menuId, int mealTypeId, int? dayIndex, int? courseTypeId, int? diners, out int mealId)
        {
            using (DataContext)
            { 
                if (item == null)
                {
                    if (diners != null)
                    {
                        item = new meals();
                        item.MenuId = menuId;
                        item.MealTypeId = mealTypeId;
                        item.DayIndex = dayIndex;
                        item.CourseTypeId = courseTypeId;

                        item.Diners = diners;
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        DataContext.meals.Add(item);
                        DataContext.SaveChanges();

                        mealId = item.MealId;
                        return true;
                    }
                    else
                    {
                        mealId = 0;
                        return false;
                    }
                }
                else
                {
                    if (diners != null)
                    {
                        item.Diners = diners;
                        item.ModifiedDate = DateTime.Now;
                        mealId = item.MealId;
                    }
                    else
                    {
                        DataContext.meals.Remove(item);
                        //dc.Meals.DeleteOnSubmit(item);
                        mealId = 0;
                    }

                    DataContext.SaveChanges();
                    //dc.SubmitChanges();
                    return true;
                }
            }
        }

        internal mealrecipes[] GetMealRecipesList(int menuId, int dayIndex, int mealTypeId)
        {
            using (DataContext)
            {
                try
                {
                    var meals = DataContext.meals.Where(m => m.MenuId == menuId &&
                                                             m.DayIndex == dayIndex &&
                                                             m.MealTypeId == mealTypeId);

                    var list = from mr in DataContext.mealrecipes
                               join m in meals on mr.MealId equals m.MealId
                               select mr;
                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal mealrecipes[] GetMealRecipes(int mealId)
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.mealrecipes.Where(m => m.MealId == mealId);
                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }
        private int GetRecipeServings(mybuylistEntities dc, int recipeId)
        {
            try
            {
                recipes recipe = dc.recipes.Single(r => r.RecipeId == recipeId);
                return recipe.Servings;
            }

            catch
            {
                return 0;
            }
        }
        private bool AddMealRecipe(mybuylistEntities dc, int mealId, int recipeId, int servings)
        {
            using(DataContext)
            {
                mealrecipes mealRecipe = DataContext.mealrecipes.SingleOrDefault(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                if (mealRecipe == null)
                {
                    mealRecipe = new mealrecipes();
                    mealRecipe.MealId = mealId;
                    mealRecipe.RecipeId = recipeId;
                    mealRecipe.Servings = servings;
                    DataContext.mealrecipes.Add(mealRecipe);
                    DataContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }               
            }
        }

        internal bool AddMealRecipe(int mealId, int recipeId)
        {
            using (DataContext)
            {
                return this.AddMealRecipe(DataContext, mealId, recipeId, 1);
            }
        }
        internal bool AddMealRecipe(int menuId, int courseTypeId, int recipeId, out int mealId)
        {
            using (DataContext)
            {
                meals meal = GetMeal(menuId, courseTypeId);

                int servings = this.GetRecipeServings(DataContext, recipeId);
                if (meal == null)
                {


                    mealId = 0;
                    if (SaveMeal(menuId, courseTypeId, servings, out mealId))
                    {
                        return this.AddMealRecipe(DataContext, mealId, recipeId, servings);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    mealId = meal.MealId;
                    return this.AddMealRecipe(DataContext, meal.MealId, recipeId, servings);
                }
            }
        }

        internal bool AddMealRecipe(int menuId, int dayIndex, int mealTypeId, int recipeId, out int mealId)
        {
            using (DataContext)
            {
                meals meal = GetMeal(menuId, dayIndex, mealTypeId);

                int servings = this.GetRecipeServings(DataContext, recipeId);

                if (meal == null)
                {
                    mealId = 0;
                    if (SaveMeal(menuId, dayIndex, mealTypeId, servings, out mealId))
                    {
                        return this.AddMealRecipe(DataContext, mealId, recipeId, servings);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    mealId = meal.MealId;
                    return this.AddMealRecipe(DataContext, meal.MealId, recipeId, servings);
                }
            }
        }

        internal mealrecipes GetMealRecipe(int mealId, int recipeId)
        {
            using (DataContext)
            {
                mealrecipes mealRecipe = DataContext.mealrecipes.Single(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                return mealRecipe;
            }
        }

        

        internal bool RemoveMealRecipe(int mealId, int recipeId)
        {
            using (DataContext)
            {
                mealrecipes mealRecipe = DataContext.mealrecipes.Single(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                DataContext.mealrecipes.Remove(mealRecipe);
                DataContext.SaveChanges();
                return true;
            }
        }

        internal bool ClearAllMeals(int menuId)
        {
            using (DataContext)
            {
                var list = DataContext.meals.Where(m => m.MenuId == menuId);
                DataContext.meals.RemoveRange(list); //Delete also all recipes for each meal (DeleteRule Constraint between tables)
                DataContext.SaveChanges();
                return true;
            }
        }

        internal bool SaveMealRecipe(int mealId, int recipeId, int servings)
        {
            using (DataContext)
            {
                mealrecipes mealRecipe = DataContext.mealrecipes.Single(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                mealRecipe.Servings = servings;

                DataContext.SaveChanges();
                return true;
            }
        }

        internal bool CreateQuickListMealRecipes(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    //MenuRecipe[] menuRecipes = DataContext.MenuRecipes.Where(mr => mr.MenuId == menuId).ToArray();
                    //if (menuRecipes.Length == 0) 
                    //    return false;
                    
                    //int mealId = 0;
                    //Meal meal = this.GetMeal(DataContext, menuId, 0);
                    //if (meal == null)
                    //{
                    //    if (!SaveMeal(DataContext, menuId, 0, 1, out mealId))
                    //        return false;
                    //}
                    //else
                    //{
                    //    mealId = meal.MealId;
                    //}
                    //foreach (MenuRecipe item in menuRecipes)
                    //{
                    //    this.AddMealRecipe(DataContext, mealId, item.RecipeId, item.Recipe.Servings);
                    //}
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
