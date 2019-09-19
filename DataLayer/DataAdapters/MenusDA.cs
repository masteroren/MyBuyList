using MyBuyList.Shared;
using MyBuyList.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class MenusDA : BaseContextDataAdapter<mybuylistEntities>
    {
        public const int USER_ADMIN = 1;

        internal menus[] SearchMenus(string searchValue)
        {
            using (DataContext)
            {
                var list = DataContext.menus.Where(mnu => mnu.MenuName.Contains(searchValue) && mnu.IsDeleted == false);
                return list.ToArray();
            }
        }

        internal menus[] GetMenusList(int userId)
        {
            using (DataContext)
            {
                var list = DataContext.menus.Where(mnu => mnu.UserId == userId && mnu.IsDeleted == false);
                return list.ToArray();
            }
        }

        internal menus[] GetMenusList(int userId, int tempUser)
        {
            using (DataContext)
            {
                var list = DataContext.menus.Where(mnu => mnu.UserId == userId && mnu.TempUserId == tempUser);
                return list.ToArray();
            }
        }

        internal menus GetMenu(int menuId)
        {
            using (DataContext)
            {
                menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                return menu;
            }
        }

        internal menus GetMenuEx(int menuId)
        {
            using (DataContext)
            {
                menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                return menu;
            }
        }

        internal meals[] GetMenuMeals(int menuId)
        {
            using (DataContext)
            {
                var list = from meal in DataContext.meals
                           where meal.MenuId == menuId
                           select meal;
                return list.ToArray();
            }
        }

        internal int GetMenusNum()
        {
            using (DataContext)
            {
                return DataContext.menus.Count();
            }
        }

        internal menutypes[] GetMenuTypes()
        {
            using (DataContext)
            {
                var list = DataContext.menutypes.OrderBy(mt => mt.SortOrder);
                return list.ToArray();
            }
        }

        internal mealrecipes[] GetMenuMealsRecipes(int menulId)
        {
            using (DataContext)
            {
                var mealsIdList = from meal in DataContext.meals
                                  where meal.MenuId == menulId
                                  select meal.MealId;

                var list = DataContext.mealrecipes.Where(m => mealsIdList.Contains(m.MealId));

                return list.ToArray();
            }
        }

        internal menutypes GetMenuType(int menuId)
        {
            using (DataContext)
            {
                menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                return menu.menutypes;
            }
        }

        internal int? GetMenuUserId(int menuId)
        {
            using (DataContext)
            {
                menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                return menu.UserId;
            }
        }

        internal int? GetMenuTempUserId(int menuId)
        {
            using (DataContext)
            {
                menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                return menu.TempUserId;
            }
        }

        internal recipes[] GetMenuRecipes(int menuId)
        {
            using (DataContext)
            {
                var list = from a in DataContext.recipes
                           from b in a.menus
                           where b.MenuId == menuId
                           select a;
                return list.ToArray();
            }
        }

        //internal MenuRecipe[] GetMenuRecipes(int menuId)
        //{
        //    using (DataContext)
        //    {
        //            var list = DataContext.MenuRecipes.Where(mr => mr.MenuId == menuId);
        //            return list.ToArray();
        //    }
        //}                

        internal bool CreateMenu(int menuTypeId, int userId, int tempUser, out int menuId)
        {
            using (DataContext)
            {
                string menuTypeName = DataContext.menutypes.Single(mt => mt.MenuTypeId == menuTypeId).MenuTypeName;
                DateTime currentDate = DateTime.Now;

                menus menu = new menus
                {
                    MenuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss"),
                    MenuTypeId = menuTypeId,
                    CreatedDate = currentDate,
                    ModifiedDate = currentDate,
                    UserId = userId
                };

                if (tempUser != 0)
                {
                    menu.TempUserId = tempUser;
                }

                var list = DataContext.menus.Where(m => m.UserId == userId);
                if (list.ToArray().Length == 0)
                {
                    menu.SortOrder = 1;
                }
                else
                {
                    menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                }

                DataContext.menus.Add(menu);
                DataContext.SaveChanges();

                menuId = menu.MenuId;
                return true;
            }
        }

        internal bool CreateMenuEx(int menuTypeId, int userId, int tempUser, string menuName, string description, bool isPublic, out int menuId)
        {
            using (DataContext)
            {
                try
                {

                    string menuTypeName = DataContext.menutypes.Single(mt => mt.MenuTypeId == menuTypeId).MenuTypeName;
                    DateTime currentDate = DateTime.Now;

                    if (menuName == string.Empty)
                    {
                        menuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    menus menu = new menus
                    {
                        MenuName = menuName,
                        MenuTypeId = menuTypeId,
                        CreatedDate = currentDate,
                        ModifiedDate = currentDate,
                        UserId = userId,
                        Description = description,
                        IsPublic = isPublic
                    };

                    if (tempUser != 0)
                    {
                        menu.TempUserId = tempUser;
                    }

                    var list = DataContext.menus.Where(m => m.UserId == userId);
                    if (list.ToArray().Length == 0)
                    {
                        menu.SortOrder = 1;
                    }
                    else
                    {
                        menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                    }

                    DataContext.menus.Add(menu);
                    DataContext.SaveChanges();
                    //DataContext.Menus.InsertOnSubmit(menu);
                    //DataContext.SubmitChanges();

                    menuId = menu.MenuId;
                    return true;
                }
                catch
                {
                    menuId = 0;
                    return false;
                }
            }
        }

        internal bool CreateMenuEx1(menus menu, int tempUser, out int menuId)
        {
            using (DataContext)
            {
                try
                {
                    string menuTypeName = DataContext.menutypes.Single(mt => mt.MenuTypeId == menu.MenuTypeId).MenuTypeName;
                    DateTime currentDate = DateTime.Now;

                    if (menu.MenuName == string.Empty)
                    {
                        menu.MenuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    menu.CreatedDate = currentDate;
                    menu.ModifiedDate = currentDate;

                    if (tempUser != 0)
                    {
                        menu.TempUserId = tempUser;
                    }

                    var list = DataContext.menus.Where(m => m.UserId == menu.UserId);
                    if (list.ToArray().Length == 0)
                    {
                        menu.SortOrder = 1;
                    }
                    else
                    {
                        menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                    }

                    menus newMenu = new menus
                    {
                        MenuName = menu.MenuName,
                        MenuTypeId = menu.MenuTypeId,
                        Description = menu.Description,
                        CreatedDate = menu.CreatedDate,
                        ModifiedDate = menu.ModifiedDate,
                        SortOrder = menu.SortOrder,
                        UserId = menu.UserId,
                        IsPublic = menu.IsPublic
                    };

                    //Array.ForEach(
                    //    menu.MenuRecipes.ToArray(),
                    //    mr =>
                    //        newMenu.MenuRecipes.Add(
                    //                                new MenuRecipe()
                    //                                {
                    //                                    //Menu = newMenu,
                    //                                    RecipeId = mr.RecipeId
                    //                                }));

                    DataContext.menus.Add(newMenu);
                    DataContext.SaveChanges();
                    //DataContext.Menus.InsertOnSubmit(newMenu);
                    //DataContext.SubmitChanges();

                    menuId = newMenu.MenuId;
                    return true;
                }
                catch
                {
                    menuId = 0;
                    return false;
                }
            }
        }

        internal void CreateOrUpdateMenu(menus menu, out int menuId)
        {
            //using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            using (DataContext)
            {
                //try
                //{
                    string menuTypeName = DataContext.menutypes.Single(mt => mt.MenuTypeId == menu.MenuTypeId).MenuTypeName;
                    DateTime currentDate = DateTime.Now;
                    if (menu.MenuName == string.Empty)
                    {
                        menu.MenuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                menus menuToSave;

                    if (menu.MenuId != 0)
                    {
                        menuToSave = DataContext.menus.Single(m => m.MenuId == menu.MenuId);
                        menuToSave.ModifiedDate = currentDate;
                    }
                    else
                    {
                        menuToSave = new menus
                        {
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                UserId = menu.UserId
                            };

                        var list = DataContext.menus.Where(m => m.UserId == menu.UserId);
                        if (list.ToArray().Length == 0)
                        {
                            menu.SortOrder = 1;
                        }
                        else
                        {
                            menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                        }

                        menuToSave.SortOrder = menu.SortOrder;

                        menuToSave.MenuTypeId = menu.MenuTypeId; //for now - it's not possible to change menu type for an existing menu.
                    }

                    menuToSave.MenuName = menu.MenuName;
                    menuToSave.Description = menu.Description;
                    menuToSave.Tags = menu.Tags;
                    menuToSave.EmbeddedVideo = menu.EmbeddedVideo;
                    menuToSave.Picture = menu.Picture;
                    menuToSave.IsPublic = menu.IsPublic;

                    //foreach (MenuRecipe mr in menu.MenuRecipes.ToArray())
                    //{
                    //    if (menuToSave.MenuRecipes.SingleOrDefault(recipe => recipe.RecipeId == mr.RecipeId) == null)
                    //    {
                    //        menuToSave.MenuRecipes.Add(new MenuRecipe() { RecipeId = mr.RecipeId });
                    //    }
                    //}

                    //for (int i = 0; i < menuToSave.MenuRecipes.ToArray().Length; i++)
                    //{
                    //    if (menu.MenuRecipes.SingleOrDefault(recipe => recipe.RecipeId == menuToSave.MenuRecipes.ToArray()[i].RecipeId) == null)
                    //    {
                    //        context.MenuRecipes.DeleteOnSubmit(menuToSave.MenuRecipes.ToArray()[i]); //it works, but is it correct?
                    //        //menuToSave.MenuRecipes.Remove(menuToSave.MenuRecipes.ToArray()[i]);
                    //    }
                    //}

                    //foreach (MenuCategory mr in menu.MenuCategories.ToArray())
                    //{
                    //    if (menuToSave.MenuCategories.SingleOrDefault(mcat => mcat.MCategoryId == mr.MCategoryId) == null)
                    //    {
                    //        menuToSave.MenuCategories.Add(new MenuCategory() { MCategoryId = mr.MCategoryId });
                    //    }
                    //}

                    //for (int i = 0; i < menuToSave.MenuCategories.ToArray().Length; i++)
                    //{
                    //    if (menu.MenuCategories.SingleOrDefault(mcat => mcat.MCategoryId == menuToSave.MenuCategories.ToArray()[i].MCategoryId) == null)
                    //    {
                    //        context.MenuCategories.DeleteOnSubmit(menuToSave.MenuCategories.ToArray()[i]);
                    //    }
                    //}

                    foreach (meals meal in menu.meals.ToArray())
                    {
                        if (meal.MealId == 0)
                        {
                        meals newMeal = new meals
                        {
                                    CourseTypeId = meal.CourseTypeId,
                                    DayIndex = meal.DayIndex,
                                    MealTypeId = meal.MealTypeId,
                                    CreatedDate = meal.CreatedDate,
                                    ModifiedDate = meal.ModifiedDate,
                                    Diners = meal.Diners
                                };

                            foreach (mealrecipes mealRecipe in meal.mealrecipes.ToArray())
                            {
                                //newMeal.MealRecipes.Add(new MealRecipe() { RecipeId = mealRecipe.RecipeId, Servings = mealRecipe.Servings });
                                //newMeal.MealRecipes.Add(new MealRecipe() { RecipeId = mealRecipe.RecipeId, Servings = mealRecipe.ExpectedServings });
                            }

                            menuToSave.meals.Add(newMeal);
                        }
                        else
                        {
                            meals sameMeal = menuToSave.meals.SingleOrDefault(sm => sm.MealId == meal.MealId);
                            if (sameMeal != null)
                            {
                                bool modified = false;
                                if (sameMeal.Diners != meal.Diners)
                                {
                                    sameMeal.Diners = meal.Diners;
                                    modified = true;
                                }
                                foreach (mealrecipes mealRecipe in meal.mealrecipes.ToArray())
                                {
                                    mealrecipes sameMealRecipe = sameMeal.mealrecipes.SingleOrDefault(smr => smr.RecipeId == mealRecipe.RecipeId);
                                    if (sameMealRecipe == null)
                                    {
                                        sameMeal.mealrecipes.Add(new mealrecipes() { RecipeId = mealRecipe.RecipeId, Servings = mealRecipe.Servings });
                                        modified = true;
                                    }
                                    else if (sameMealRecipe.Servings != mealRecipe.Servings)
                                    {
                                        sameMealRecipe.Servings = mealRecipe.Servings;
                                        modified = true;
                                    }
                                }

                                if (modified)
                                {
                                    sameMeal.ModifiedDate = currentDate;
                                }
                            }
                        }
                    }

                    if (menu.MenuId != 0)
                    {
                        for (int i = 0; i < menuToSave.meals.ToArray().Length; i++)
                        {
                        meals menuToSaveMeal = menuToSave.meals.ToArray()[i];

                        meals menuMeal = null;
                            if (menuToSave.MenuTypeId == 1) //one meal
                            {
                                menuMeal = menu.meals.SingleOrDefault(mm => mm.CourseTypeId == menuToSaveMeal.CourseTypeId);
                            }
                            if (menuToSave.MenuTypeId == 4) //quick meal
                            {
                                menuMeal = menu.meals.SingleOrDefault(mm => mm.MealTypeId == menuToSaveMeal.MealTypeId);
                            }
                            if (menuToSave.MenuTypeId == 2) //weekly
                            {
                                menuMeal = menu.meals.SingleOrDefault(mm => mm.DayIndex == menuToSaveMeal.DayIndex && mm.MealTypeId == menuToSaveMeal.MealTypeId);
                            }

                            if (menuMeal == null)   //meal that was erased (emptied) during edit - delete all meal recipes and then delete meal.
                            {
                                for (int j = 0; j < menuToSaveMeal.mealrecipes.ToArray().Length; j++)
                                {
                                DataContext.mealrecipes.Remove(menuToSaveMeal.mealrecipes.ToArray()[j]);
                                //DataContext.MealRecipes.DeleteOnSubmit(menuToSaveMeal.MealRecipes.ToArray()[j]);
                            }

                            DataContext.meals.Remove(menuToSaveMeal);
                            //DataContext.Meals.DeleteOnSubmit(menuToSaveMeal);
                        }
                            else    //meal that still exists - check all meal recipes if any were erased. is so - erase meal recipe from menuToSaveMeal.
                            {
                                for (int j = 0; j < menuToSaveMeal.mealrecipes.ToArray().Length; j++)
                                {
                                    if (menuMeal.mealrecipes.SingleOrDefault(mmr => mmr.RecipeId == menuToSaveMeal.mealrecipes.ToArray()[j].RecipeId) == null)
                                    {
                                    DataContext.mealrecipes.Remove(menuToSaveMeal.mealrecipes.ToArray()[j]);
                                    //DataContext.MealRecipes.DeleteOnSubmit(menuToSaveMeal.MealRecipes.ToArray()[j]);
                                    menuToSaveMeal.mealrecipes.Remove(menuToSaveMeal.mealrecipes.ToArray()[j]);
                                    }
                                }
                            }
                        }
                    }

                    if (menu.MenuId == 0)
                {
                    DataContext.menus.Add(menuToSave);
                    //DataContext.Menus.InsertOnSubmit(menuToSave);
                    }

                DataContext.SaveChanges();
                //DataContext.SubmitChanges();

                menuId = menuToSave.MenuId;
                    //return true;
                //}
                //catch
                //{
                //    menuId = 0;
                //    return false;
                //}
            }
        }

        internal bool UpdateMenuNameAndDescription(int menuId, string menuName, string description)
        {
            using (DataContext)
            {
                try
                {
                    menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                    menu.MenuName = menuName;
                    menu.Description = description;
                    menu.ModifiedDate = DateTime.Now;

                    DataContext.SaveChanges();
                    //DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateMenuUser(int menuId, int userId)
        {
            using (DataContext)
            {
                try
                {
                    menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                    menu.UserId = userId;
                    //menu.ModifiedDate = DateTime.Now;

                    DataContext.SaveChanges();
                    //DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool DeleteMenu(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    menus menu = DataContext.menus.Single(m => m.MenuId == menuId);
                    menu.IsDeleted = true;
                    menu.ModifiedDate = DateTime.Now;

                    //var menuRecipesToDelete = from mr in DataContext.MenuRecipes
                    //                          where mr.MenuId == menuId
                    //                          select mr;

                    //DataContext.MenuRecipes.DeleteAllOnSubmit(menuRecipesToDelete);

                    var mealsToDelete = from ml in DataContext.meals
                                        where ml.MenuId == menuId
                                        select ml;

                    DataContext.meals.RemoveRange(mealsToDelete);
                    DataContext.SaveChanges();
                    //DataContext.Meals.DeleteAllOnSubmit(mealsToDelete);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool RemoveMenuRecipe(int menuId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    //MenuRecipe mnuRecipe = DataContext.MenuRecipes.Single(mr => mr.MenuId == menuId && mr.RecipeId == recipeId);
                    //DataContext.MenuRecipes.DeleteOnSubmit(mnuRecipe);
                    //DataContext.SubmitChanges();

                    //delete this recipe from all meals for menuId
                    mealrecipes[] mealRecipes = DataContext.mealrecipes.Where(mr => mr.RecipeId == recipeId &&
                                                                                   mr.meals.MenuId == menuId).ToArray();

                    DataContext.mealrecipes.RemoveRange(mealRecipes);
                    DataContext.SaveChanges();
                    //DataContext.MealRecipes.DeleteAllOnSubmit(mealRecipes);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool CheckIfMenuRecipeExistInMeals(int menuId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    bool check = DataContext.mealrecipes.Any(mr => mr.RecipeId == recipeId &&
                                                             mr.meals.MenuId == menuId);
                    //if (list != null)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    return check;
                    // }

                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool AddMenuRecipe(int menuId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    //MenuRecipe mnuRecipe = DataContext.MenuRecipes.SingleOrDefault(mr => mr.MenuId == menuId && mr.RecipeId == recipeId);
                    //if (mnuRecipe == null)
                    //{
                    //    mnuRecipe = new MenuRecipe();
                    //    mnuRecipe.MenuId = menuId;
                    //    mnuRecipe.RecipeId = recipeId;
                    //    DataContext.MenuRecipes.InsertOnSubmit(mnuRecipe);
                    //}

                    //DataContext.SubmitChanges();
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }

        internal bool AddMenuToUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            using (DataContext)
            {
                try
                {
                    favMenusNum = 0;

                    //UserFavoriteMenu favMenu = DataContext.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == userId &&
                    //                                                                                  ufm.MenuId == menuId);
                    //if (favMenu == null)
                    //{
                    //    favMenu = new UserFavoriteMenu();
                    //    favMenu.UserId = userId;
                    //    favMenu.MenuId = menuId;
                    //    DataContext.UserFavoriteMenus.InsertOnSubmit(favMenu);
                    //    DataContext.SubmitChanges();
                    //}

                    //favMenusNum = DataContext.UserFavoriteMenus.Where(ufr => ufr.UserId == userId && ufr.Menu.IsDeleted == false && (ufr.Menu.IsPublic == true || ufr.Menu.UserId == userId || userId == USER_ADMIN)).Count();

                    return true;
                }
                catch
                {
                    favMenusNum = 0;
                    return false;
                }
            }
        }

        internal bool RemoveMenuFromUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            using (DataContext)
            {
                try
                {
                    favMenusNum = 0;

                    //UserFavoriteMenu favMenu = DataContext.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == userId &&
                    //                                                                                  ufm.MenuId == menuId);
                    //DataContext.UserFavoriteMenus.DeleteOnSubmit(favMenu);
                    //DataContext.SubmitChanges();

                    //favMenusNum = DataContext.UserFavoriteMenus.Where(ufr => ufr.UserId == userId && ufr.Menu.IsDeleted == false && (ufr.Menu.IsPublic == true || ufr.Menu.UserId == userId || userId == USER_ADMIN)).Count();

                    return true;
                }
                catch
                {
                    favMenusNum = 0;
                    return false;
                }
            }
        }

        internal int GetMenuMaxDay(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    var days = from meal in DataContext.meals
                               where meal.MenuId == menuId
                               select meal.DayIndex;


                    return days.Max().Value;
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal IEnumerable<menus> GetMenus(int userid, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            //using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<Menu>(m => m.User);

                    //DataContext.LoadOptions = dlo;

                    var count = (from m in DataContext.menus
                                 where m.IsDeleted == false && (m.IsPublic == true || m.UserId == userid)
                                 select m).Count();

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from m in DataContext.menus
                               where m.IsDeleted == false && (m.IsPublic == true || m.UserId == userid)
                               select m;

                    if (userid == 1)
                    {
                        count = (from m in DataContext.menus
                                 where m.IsDeleted == false
                                 select m).Count();

                        totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                        list = from m in DataContext.menus
                               where m.IsDeleted == false
                               select m;
                    }




                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(m => m.MenuName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(m => m.users.DisplayName);
                            break;
                        case RecipeOrderEnum.LastUpdate:
                            list = list.OrderByDescending(m => m.ModifiedDate);
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

        internal IEnumerable<menus> GetMenusEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? diners, int[] menuCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numMenus)
        {
            using (DataContext)
            {
                try
                {
                    List<menus> MenusList = null;

                    switch (display)
                    {
                        case RecipeDisplayEnum.All:
                            MenusList = GetAllMenus(userId).ToList();
                            break;
                        case RecipeDisplayEnum.MyRecipes:
                            if (!string.IsNullOrEmpty(freeText))
                                MenusList = GetUserMenusList(userId).Where(r => r.MenuName.Contains(freeText)).ToList();
                            else
                                MenusList = GetUserMenusList(userId).ToList();
                            break;
                        case RecipeDisplayEnum.MyFavoriteRecipes:
                            if (!string.IsNullOrEmpty(freeText))
                                MenusList = GetUserFavoritesMenus(userId).Where(r => r.MenuName.Contains(freeText)).ToList();
                            else
                                MenusList = GetUserFavoritesMenus(userId).ToList();
                            break;
                        case RecipeDisplayEnum.ByCategory:
                            if (categoryId.HasValue)
                            {
                                MenusList = GetMenusByCategory(categoryId.Value, userId).ToList();
                            }
                            else
                            {
                                MenusList = GetAllMenus(userId).ToList();
                            }
                            break;
                        case RecipeDisplayEnum.BySearchSimple:
                            MenusList = GetMenusListByFreeText(freeText, userId).ToList();
                            break;
                        case RecipeDisplayEnum.BySearchAdvanced:
                            MenusList = GetMenusListByComplexSearch(freeText, diners, menuCats, userId).ToList();
                            break;
                    }

                    var count = (from m in MenusList
                                 select m).Count();

                    numMenus = count;

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from m in MenusList
                               select m;

                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(m => m.MenuName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(m => m.users.DisplayName);
                            break;
                        case RecipeOrderEnum.LastUpdate:
                            list = list.OrderByDescending(m => m.ModifiedDate);
                            break;
                    }

                    // paging
                    var list2 = list.Skip((page - 1) * pageSize).Take(pageSize);

                    return list2.ToList();

                }
                catch
                {
                    numMenus = 0;
                    totalPages = 0;
                    return null;
                }
            }
        }

        internal menus[] GetAllMenus(int userId)
        {
            var list = from m in DataContext.menus.Where(m => m.IsDeleted == false && m.IsPublic == true)
                       select m;

            //var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
            //           select m;

            return list.ToArray();
        }

        internal menus[] GetUserMenusList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.menus.Where(m => m.IsDeleted == false && m.UserId == userId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal menus[] GetUserFavoritesMenus(int userId)
        {
            using (DataContext)
            {
                //    var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                //               join ufm in DataContext.UserFavoriteMenus.Where(ufm => ufm.UserId == userId) on m.MenuId equals ufm.MenuId
                //               select m;
                //    return list.ToArray();
                return null;
            }
        }

        internal menus[] GetMenusListByFreeText(string freeText, int userId)
        {
            //var menuRecipes = from m in DataContext.Menus.Where(m => m.IsDeleted == false && m.IsPublic == true)
            //                  join mr in DataContext.MenuRecipes.Where(mr => mr.Recipe.RecipeName.Trim().IndexOf(freeText.Trim()) != -1 ||
            //                              mr.Recipe.Remarks.Trim().IndexOf(freeText.Trim()) != -1 ||
            //                              mr.Recipe.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1 ||
            //                              mr.Recipe.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
            //                              mr.Recipe.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
            //                  on m.MenuId equals mr.MenuId
            //                  select m.MenuId;

            //var list = from m1 in DataContext.Menus.Where(m => m.IsDeleted == false && m.IsPublic &&
            //                                    (m.MenuName.Trim().IndexOf(freeText.Trim()) != -1 ||
            //                                     m.MenuType.MenuTypeName.Trim().IndexOf(freeText.Trim()) != -1 ||
            //                                     m.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
            //                                     m.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1
            //                                     ))
            //           select m1.MenuId;

            //var menuCategories = from m2 in DataContext.Menus.Where(m => m.IsDeleted == false && m.IsPublic == true)
            //                     join mc in DataContext.MenuCategories.Where(mc => (mc.MCategory.MCategoryName.Trim().IndexOf(freeText.Trim()) != -1))
            //                                                                         on m2.MenuId equals mc.MenuId
            //                     select m2.MenuId;

            ////var list1 = list.Union(menuRecipes);
            //var tmpList = list.Union(menuRecipes);
            //var list1 = tmpList.Union(menuCategories);

            //return DataContext.Menus.Where(m => list1.Contains(m.MenuId)).ToArray();

            return null;
        }


        //internal Menus[] GetMenusListByFreeText(string freeText, int userId)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            DataLoadOptions dlo = new DataLoadOptions();
        //            dlo.LoadWith<Menu>(m => m.User);
        //            dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);

        //            DataContext.LoadOptions = dlo;

        //            var menuRecipes = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
        //                              join mr in DataContext.MenuRecipes.Where(mr => mr.Recipe.RecipeName.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.Remarks.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
        //                              on m.MenuId equals mr.MenuId
        //                              select m.MenuId;

        //            var list = from m1 in DataContext.Menus.Where(m => (m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN)) &&
        //                                                (m.MenuName.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                                 m.MenuType.MenuTypeName.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                                 m.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                                 m.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1
        //                                                 ))
        //                       select m1.MenuId;

        //            var menuCategories = from m2 in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
        //                        join mc in DataContext.MenuCategories.Where(mc => (mc.MCategory.MCategoryName.Trim().IndexOf(freeText.Trim()) != -1))
        //                                                                            on m2.MenuId equals mc.MenuId
        //                        select m2.MenuId;

        //            //var list1 = list.Union(menuRecipes);
        //            var tmpList = list.Union(menuRecipes);
        //            var list1 = tmpList.Union(menuCategories);

        //            return DataContext.Menus.Where(m => list1.Contains(m.MenuId)).ToArray();

        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        internal menus[] GetMenusListByComplexSearch(string freeText, int? diners, int[] menuCats, int userId)
        {
            using (DataContext)
            {
                return null;

                //try
                //{
                //    //DataLoadOptions dlo = new DataLoadOptions();
                //    //dlo.LoadWith<Menu>(m => m.User);
                //    //dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);

                //    //DataContext.LoadOptions = dlo;

                //    var menuRecipes = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                //                      join mr in DataContext.MenuRecipes.Where(mr => mr.Recipe.RecipeName.Trim().IndexOf(freeText.Trim()) != -1 ||
                //                                  mr.Recipe.Remarks.Trim().IndexOf(freeText.Trim()) != -1 ||
                //                                  mr.Recipe.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1 ||
                //                                  mr.Recipe.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
                //                                  mr.Recipe.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
                //                      on m.MenuId equals mr.MenuId                                      
                //                      select m.MenuId;

                //    var menus = from m1 in DataContext.Menus.Where(m => (m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN)) &&
                //                                        (m.MenuName.Trim().IndexOf(freeText.Trim()) != -1 ||
                //                                         m.MenuType.MenuTypeName.Trim().IndexOf(freeText.Trim()) != -1 ||
                //                                         m.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
                //                                         m.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1
                //                                         ))
                //               select m1.MenuId;

                //    var menuCategories = from m2 in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                //                         join mc in DataContext.MenuCategories.Where(mc => (mc.MCategory.MCategoryName.Trim().IndexOf(freeText.Trim()) != -1))
                //                                                                             on m2.MenuId equals mc.MenuId
                //                         select m2.MenuId;

                //    //var list1 = menus.Union(menuRecipes);
                //    var tmpList = menus.Union(menuRecipes);
                //    var list1 = tmpList.Union(menuCategories);                    

                //    var list = DataContext.Menus.Where(m => list1.Contains(m.MenuId));

                //    if (diners.HasValue)
                //    {

                //        //list = list.Where(m => m >= diners.Value);
                //    }

                //    if (menuCats != null && menuCats.Length != 0)
                //    {
                //        List<Menu> temp = new List<Menu>();

                //        foreach (int categoryId in menuCats)
                //        {
                //            foreach (MenuCategory mcat in DataContext.MenuCategories.Where(mc => mc.MCategoryId == categoryId ||
                //                                                                                    mc.MCategory.ParentMCategoryId == categoryId))
                //            {
                //                if (!temp.Contains(mcat.Menu))
                //                {
                //                    temp.Add(mcat.Menu);
                //                }
                //            }
                //        }

                //        list = (from m in list
                //                where temp.Contains(m)
                //                select m);
                //    }

                //    return list.ToArray();
                //}
                //catch
                //{
                //    return null;
                //}
            }
        }

        internal menus[] GetMenusByCategory(int categoryId, int userId)
        {
            return null;

            //using (DataContext)
            //{
            //    try
            //    {
            //        var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
            //                   join mcat in DataContext.MenuCategories.Where(mc => mc.MCategoryId == categoryId ||
            //                                                                mc.MCategory.ParentMCategoryId == categoryId)
            //                   on m.MenuId equals mcat.MenuId
            //                   select m;

            //        return (from m in DataContext.Menus
            //                where list.Contains(m)
            //                select m).ToArray();
            //    }
            //    catch
            //    {
            //        return null;
            //    }
            //}
        }

        internal mcategories[] GetMenusCategoriesList(int userId)
        {
            return null;

            //using (DataContext)
            //{
            //    try
            //    {
            //        DataLoadOptions dlo = new DataLoadOptions();
            //        dlo.LoadWith<Menu>(r => r.User);
            //        DataContext.LoadOptions = dlo;
            //        var list = DataContext.MCategories.OrderBy(mcat => mcat.SortOrder);
            //        foreach (MCategory item in list)
            //        {
            //            item.ParentMCategories.Load();
            //            item.MenusCount = GetMCategoryMenusNum(item.MCategoryId, userId);
            //        }
            //        return list.ToArray();
            //    }
            //    catch
            //    {
            //        return null;
            //    }
            //}
        }

        internal int GetMCategoryMenusNum(int categoryId, int userId)
        {
            return 0;

            //using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            //using (DataContext)
            //{
            //    var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
            //               join mcat in DataContext.MenuCategories.Where(mc => mc.MCategoryId == categoryId ||
            //                                                            mc.MCategory.ParentMCategoryId == categoryId)
            //               on m.MenuId equals mcat.MenuId
            //               select m;

            //    return (from m in DataContext.Menus
            //            where list.Contains(m)
            //            select m).ToArray().Length;
            //}
        }

        internal void AddMenuToShoppingList(int userId, int menuId, bool check)
        {
            //MenusInShoppingList menusInShoppingList = DataContext.MenusInShoppingLists.SingleOrDefault(p => p.UserId == userId && p.MenuId == menuId);

            //if (!check)
            //{
            //    if (menusInShoppingList != null)
            //    {
            //        DataContext.MenusInShoppingLists.DeleteOnSubmit(menusInShoppingList);
            //    }
            //}
            //else
            //{
            //    if (menusInShoppingList == null)
            //    {
            //        menusInShoppingList = new MenusInShoppingList
            //        {
            //            UserId = userId,
            //            MenuId = menuId
            //        };
            //        DataContext.MenusInShoppingLists.InsertOnSubmit(menusInShoppingList);
            //    }
            //}
            //DataContext.SubmitChanges();
        }

        //internal IQueryable<MenusInShoppingList> GetMenusInShoppingList(int userId)
        //{
        //    IQueryable<MenusInShoppingList> menusInShoppingList = DataContext.MenusInShoppingLists.Where(p => p.UserId == userId);
        //    return menusInShoppingList;
        //}

        internal void RemoveMenuFromShoppingList(int userId, int menuId)
        {
            //MenusInShoppingList menusInShoppingList = DataContext.MenusInShoppingLists.SingleOrDefault(p => p.UserId == userId && p.MenuId == menuId);
            //DataContext.MenusInShoppingLists.DeleteOnSubmit(menusInShoppingList);
            //DataContext.SubmitChanges();
        }
    }
}
