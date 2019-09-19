using MyBuyList.Shared;
using System;
using System.Data.Linq;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class AdminDA : BaseContextDataAdapter<mybuylistEntities>
    {
        #region Categories

        internal bool CheckDuplicateCategoryName(int categoryId, string categoryName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.categories.Any(cat => cat.CategoryId != categoryId &&
                                                             cat.CategoryName.Trim() == categoryName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool CheckDuplicateMCategoryName(int categoryId, string categoryName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.mcategories.Any(cat => cat.MCategoryId != categoryId &&
                                                             cat.MCategoryName.Trim() == categoryName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal categories[] GetCategoriesList()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.categories.OrderBy(cat => cat.SortOrder);
                    //foreach (Category item in list)
                    //{
                    //    item.ParentCategories.Load();
                    //}
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal mcategories[] GetMCategoriesList()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.mcategories.OrderBy(mcat => mcat.SortOrder);
                   
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal categories GetCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    categories cat = DataContext.categories.Single(c => c.CategoryId == categoryId);
                    //cat.ParentCategory = DataContext.Categories.SingleOrDefault(c => c.CategoryId == cat.ParentCategoryId);
                    //cat.AllowDelete = (cat.RecipeCategories.Count == 0);
                    //if (cat.AllowDelete)
                    //{
                    //    //check if this category has children categories
                    //    cat.AllowDelete = !DataContext.Categories.Any(c => c.ParentCategoryId == categoryId);
                    //}
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal mcategories GetMenuCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    mcategories cat = DataContext.mcategories.Single(c => c.MCategoryId == categoryId);
                    //cat.ParentMCategory = DataContext.MCategories.SingleOrDefault(c => c.MCategoryId == cat.ParentMCategoryId);
                    //cat.AllowDelete = (cat.MenuCategories.Count == 0);
                    //if (cat.AllowDelete)
                    //{
                    //    //check if this category has children categories
                    //    cat.AllowDelete = !DataContext.MCategories.Any(c => c.ParentMCategoryId == categoryId);
                    //}
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveCategory(categories category)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.categories.Contains(category))
                    {
                        category.CreatedDate = DateTime.Now;
                        category.SortOrder = DataContext.categories.Max(cat => cat.SortOrder) + 1;
                        //DataContext.Categories.InsertOnSubmit(category);
                        DataContext.categories.Add(category);
                    }
                    else
                    {
                        string categoryName = category.CategoryName;
                        int? parentCategoryId = category.ParentCategoryId;
                        category = DataContext.categories.Single(cat => cat.CategoryId == category.CategoryId);
                        category.CategoryName = categoryName;
                        category.ParentCategoryId = parentCategoryId;
                    }

                    category.ModifiedDate = DateTime.Now;
                    //DataContext.SubmitChanges();
                    DataContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            using (DataContext)
            {
                try
                {

                    categories category = DataContext.categories.SingleOrDefault(cat => cat.CategoryId == categoryId);
                    if (category == null)
                    {
                        category = new categories();
                        category.CategoryName = categoryName;
                        category.ParentCategoryId = parentCategoryId;
                        category.CreatedDate = DateTime.Now;
                        category.ModifiedDate = DateTime.Now;
                        category.SortOrder = DataContext.categories.Max(cat => cat.SortOrder) + 1;
                        //DataContext.Categories.InsertOnSubmit(category);
                        DataContext.categories.Add(category);
                    }
                    else
                    {
                        category.CategoryName = categoryName;
                        category.ParentCategoryId = parentCategoryId;
                        category.ModifiedDate = DateTime.Now;
                    }

                    //DataContext.SubmitChanges();
                    DataContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveMenuCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            using (DataContext)
            {
                try
                {

                    mcategories category = DataContext.mcategories.SingleOrDefault(cat => cat.MCategoryId == categoryId);
                    if (category == null)
                    {
                        category = new mcategories();
                        category.MCategoryName = categoryName;
                        category.ParentMCategoryId = parentCategoryId;
                        category.CreatedDate = DateTime.Now;
                        category.ModifiedDate = DateTime.Now;
                        category.SortOrder = DataContext.categories.Max(cat => cat.SortOrder) + 1;
                        DataContext.mcategories.Add(category);
                        //DataContext.MCategories.InsertOnSubmit(category);
                    }
                    else
                    {
                        category.MCategoryName = categoryName;
                        category.ParentMCategoryId = parentCategoryId;
                        category.ModifiedDate = DateTime.Now;
                    }

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

        internal bool DeleteCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    categories category = DataContext.categories.Single(cat => cat.CategoryId == categoryId);
                    DataContext.categories.Remove(category);
                    DataContext.SaveChanges();
                    //DataContext.Categories.DeleteOnSubmit(category);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool DeleteMenuCategory(int categoryId)
        {
            using (DataContext)
            {
                try
                {
                    mcategories category = DataContext.mcategories.Single(cat => cat.MCategoryId == categoryId);
                    DataContext.mcategories.Remove(category);
                    DataContext.SaveChanges();
                    //DataContext.MCategories.DeleteOnSubmit(category);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion Categories

        #region FoodCategories

        internal bool CheckDuplicateFoodCategoryName(int FoodCategoryId, string FoodCategoryName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.foodcategories.Any(cat => cat.FoodCategoryId != FoodCategoryId &&
                                                             cat.FoodCategoryName.Trim() == FoodCategoryName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool CheckDuplicateFoodName(int FoodId, string FoodName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.food.Any(f => f.FoodId != FoodId &&
                                                             f.FoodName.Trim() == FoodName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal foodcategories[] GetFoodCategoriesList()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.foodcategories.OrderBy(fcat => fcat.SortOrder);
                    //foreach (FoodCategory item in list)
                    //{
                    //    item.FoodCategories.Load();
                    //}
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal foodcategories GetFoodCategory(int FoodCategoryId)
        {
            using (DataContext)
            {
                try
                {
                    foodcategories cat = DataContext.foodcategories.Single(c => c.FoodCategoryId == FoodCategoryId);
                    //cat.ParentFoodCategory = DataContext.FoodCategories.SingleOrDefault(c => c.FoodCategoryId == cat.ParentCategoryId);
                    //cat.AllowDelete = (cat.FoodCategories.Count == 0);
                    //if (cat.AllowDelete)
                    //{
                    //    //check if this FoodCategory has children FoodCategories
                    //    cat.AllowDelete = !DataContext.FoodCategories.Any(c => c.ParentCategoryId == FoodCategoryId);
                    //}
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveFoodCategory(foodcategories FoodCategory)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.foodcategories.Contains(FoodCategory))
                    {
                        FoodCategory.CreatedDate = DateTime.Now;
                        FoodCategory.SortOrder = DataContext.foodcategories.Max(cat => cat.SortOrder) + 1;
                        DataContext.foodcategories.Add(FoodCategory);
                        //DataContext.FoodCategories.InsertOnSubmit(FoodCategory);
                    }
                    else
                    {
                        string FoodCategoryName = FoodCategory.FoodCategoryName;
                        int? ParentCategoryId = FoodCategory.ParentCategoryId;
                        FoodCategory = DataContext.foodcategories.Single(cat => cat.FoodCategoryId == FoodCategory.FoodCategoryId);
                        FoodCategory.FoodCategoryName = FoodCategoryName;
                        FoodCategory.ParentCategoryId = ParentCategoryId;
                    }

                    FoodCategory.ModifiedDate = DateTime.Now;
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



        internal bool SaveFoodCategory(int FoodCategoryId, string FoodCategoryName, int? ParentCategoryId)
        {
            using (DataContext)
            {
                try
                {

                    foodcategories FoodCategory = DataContext.foodcategories.SingleOrDefault(cat => cat.FoodCategoryId == FoodCategoryId);
                    if (FoodCategory == null)
                    {
                        FoodCategory = new foodcategories();
                        FoodCategory.FoodCategoryName = FoodCategoryName;
                        FoodCategory.ParentCategoryId = ParentCategoryId;
                        FoodCategory.CreatedDate = DateTime.Now;
                        FoodCategory.ModifiedDate = DateTime.Now;
                        FoodCategory.SortOrder = DataContext.foodcategories.Max(cat => cat.SortOrder) + 1;
                        DataContext.foodcategories.Add(FoodCategory);
                        //DataContext.FoodCategories.InsertOnSubmit(FoodCategory);
                    }
                    else
                    {
                        FoodCategory.FoodCategoryName = FoodCategoryName;
                        FoodCategory.ParentCategoryId = ParentCategoryId;
                        FoodCategory.ModifiedDate = DateTime.Now;
                    }

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

        internal bool DeleteFoodCategory(int FoodCategoryId)
        {
            using (DataContext)
            {
                try
                {
                    foodcategories FoodCategory = DataContext.foodcategories.Single(cat => cat.FoodCategoryId == FoodCategoryId);
                    DataContext.foodcategories.Remove(FoodCategory);
                    DataContext.SaveChanges();
                    //DataContext.FoodCategories.DeleteOnSubmit(FoodCategory);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion FoodCategories

        #region Foods

        internal food[] GetFoodsList()
        {
            //DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<Food>(f => f.Ingredients);
            //DataContext.LoadOptions = dlo;
            var list = DataContext.food.OrderBy(f => f.FoodName);
            return list.ToArray();
        }

        internal food GetFood(int foodId)
        {
            using (DataContext)
            {
                food food = DataContext.food.Single(f => f.FoodId == foodId);
                return food;
            }
        }

        internal food GetFood(string name)
        {
            using (DataContext)
            {
                try
                {
                    food food = DataContext.food.SingleOrDefault(f => f.FoodName.Trim() == name);
                    return food;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveFood(food food)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.food.Contains(food))
                    {
                        food.CreatedDate = DateTime.Now;
                        food.IsTemporary = true;
                        DataContext.food.Add(food);
                        //DataContext.Foods.InsertOnSubmit(food);
                    }
                    else
                    {
                        food.IsTemporary = false;
                        DataContext.food.Attach(food);
                        //DataContext.Refresh(RefreshMode.KeepCurrentValues, food);
                    }

                    food.ModifiedDate = DateTime.Now;
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

        #endregion Foods

        #region ShopDepartments

        internal bool CheckDuplicateShopDepartmentName(int departmentId, string departmentName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.shopdepartments.Any(sd => sd.ShopDepartmentId != departmentId &&
                                                                 sd.ShopDepartmentName.Trim() == departmentName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal shopdepartments GetShopDepartment(int departmentId)
        {
            using (DataContext)
            {
                try
                {
                    shopdepartments dep = DataContext.shopdepartments.Single(sd => sd.ShopDepartmentId == departmentId);
                    return dep;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal shopdepartments[] GetShopDepartmentsList()
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<ShopDepartment>(sd => sd.FoodCategories);
                    //dlo.LoadWith<FoodCategory>(fc => fc.Foods);

                    //DataContext.LoadOptions = dlo;

                    var list = DataContext.shopdepartments.OrderBy(sd => sd.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool DeleteShopDepartment(int shopDepartmentId)
        {
            using (DataContext)
            {
                DataLoadOptions dlo = new DataLoadOptions();
                dlo.LoadWith<shopdepartments>(sd => sd.foodcategories);


                //DataContext.LoadOptions = dlo;
                try
                {
                    shopdepartments department = DataContext.shopdepartments.Single(sd => sd.ShopDepartmentId == shopDepartmentId);
                    foodcategories[] depFoodCat = department.foodcategories.ToArray<foodcategories>();
                    foreach (foodcategories curr in depFoodCat)
                    {
                        this.DeleteFoodCategory(curr.FoodCategoryId);
                    }

                    DataContext.shopdepartments.Remove(department);
                    DataContext.SaveChanges();
                    //DataContext.ShopDepartments.DeleteOnSubmit(department);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveShopDepartment(shopdepartments department)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.shopdepartments.Contains(department))
                    {
                        department.CreatedDate = DateTime.Now;
                        department.SortOrder = DataContext.shopdepartments.Max(d => d.SortOrder) + 1;
                        DataContext.shopdepartments.Add(department);
                        //DataContext.ShopDepartments.InsertOnSubmit(department);
                    }
                    else
                    {
                        string departmentName = department.ShopDepartmentName;
                        department = DataContext.shopdepartments.Single(d => d.ShopDepartmentId == department.ShopDepartmentId);
                        department.ShopDepartmentName = departmentName;
                    }

                    department.ModifiedDate = DateTime.Now;
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

        internal bool ReorderShopDepartments(shopdepartments[] arr)
        {
            using (DataContext)
            {
                try
                {
                    foreach (shopdepartments item in arr)
                    {
                        shopdepartments dep = DataContext.shopdepartments.Single(sd => sd.ShopDepartmentId == item.ShopDepartmentId);
                        dep.SortOrder = item.SortOrder;
                        dep.ModifiedDate = DateTime.Now;
                        DataContext.SaveChanges();
                        //DataContext.SubmitChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion ShopDepartments

        #region GeneralItems

        internal bool CheckDuplicateGeneralItemName(int ItemId, string ItemName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.generalitems.Any(sd => sd.GeneralItemId != ItemId &&
                                                                 sd.GeneralItemName.Trim() == ItemName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal generalitems GetGeneralItem(int ItemId)
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<GeneralItem>(sd => sd.ShoppingListAdditionalItems);
                    //DataContext.LoadOptions = dlo;

                    generalitems dep = DataContext.generalitems.Single(sd => sd.GeneralItemId == ItemId);
                    return dep;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal generalitems[] GetGeneralItemsList()
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<GeneralItem>(sd => sd.ShoppingListAdditionalItems);
                    //DataContext.LoadOptions = dlo;

                    var list = DataContext.generalitems.OrderBy(sd => sd.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool DeleteGeneralItem(int GeneralItemId)
        {
            using (DataContext)
            {
                try
                {
                    generalitems Item = DataContext.generalitems.Single(sd => sd.GeneralItemId == GeneralItemId);
                    DataContext.generalitems.Remove(Item);
                    DataContext.SaveChanges();
                    //DataContext.GeneralItems.DeleteOnSubmit(Item);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveGeneralItem(generalitems Item)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.generalitems.Contains(Item))
                    {
                        // Item.CreatedDate = DateTime.Now;
                        Item.SortOrder = DataContext.generalitems.Max(d => d.SortOrder) + 1;
                        DataContext.generalitems.Add(Item);
                        //DataContext.GeneralItems.InsertOnSubmit(Item);
                    }
                    else
                    {
                        string ItemName = Item.GeneralItemName;
                        Item = DataContext.generalitems.Single(d => d.GeneralItemId == Item.GeneralItemId);
                        Item.GeneralItemName = ItemName;
                    }

                    //Item.ModifiedDate = DateTime.Now;
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

        internal bool ReorderGeneralItems(generalitems[] arr)
        {
            using (DataContext)
            {
                try
                {
                    foreach (generalitems item in arr)
                    {
                        generalitems dep = DataContext.generalitems.Single(sd => sd.GeneralItemId == item.GeneralItemId);
                        dep.SortOrder = item.SortOrder;
                        //dep.ModifiedDate = DateTime.Now;
                        DataContext.SaveChanges();
                        //DataContext.SubmitChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion GeneralItems

        #region MeasurementUnits

        internal bool CheckDuplicateMeasurementUnitName(int unitId, string unitName)
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.measurementunits.Any(mu => mu.UnitId != unitId &&
                                                                  mu.UnitName.Trim() == unitName.Trim());
                }
                catch
                {
                    return false;
                }
            }
        }

        internal measurementunits GetMeasurementUnit(int unitId)
        {
            using (DataContext)
            {
                try
                {
                    measurementunits unit = DataContext.measurementunits.Single(mu => mu.UnitId == unitId);
                    return unit;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal measurementunits[] GetMeasurementUnitsList()
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.measurementunits.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool DeleteMeasurementUnit(int unitId)
        {
            using (DataContext)
            {
                try
                {
                    measurementunits unit = DataContext.measurementunits.Single(mu => mu.UnitId == unitId);
                    DataContext.measurementunits.Remove(unit);
                    DataContext.SaveChanges();
                    //DataContext.MeasurementUnits.DeleteOnSubmit(unit);
                    //DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveMeasurementUnit(measurementunits unit)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.measurementunits.Contains(unit))
                    {
                        unit.CreatedDate = DateTime.Now;
                        unit.SortOrder = DataContext.measurementunits.Max(mu => mu.SortOrder) + 1;
                        DataContext.measurementunits.Add(unit);
                        //DataContext.MeasurementUnits.InsertOnSubmit(unit);
                    }
                    else
                    {
                        string unitName = unit.UnitName;
                        unit = DataContext.measurementunits.Single(mu => mu.UnitId == unit.UnitId);
                        unit.UnitName = unitName;
                    }

                    unit.ModifiedDate = DateTime.Now;
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

        internal bool ReorderMeasurementUnits(measurementunits[] arr)
        {
            using (DataContext)
            {
                try
                {
                    foreach (measurementunits item in arr)
                    {
                        measurementunits unit = DataContext.measurementunits.Single(mu => mu.UnitId == item.UnitId);
                        unit.SortOrder = item.SortOrder;
                        unit.ModifiedDate = DateTime.Now;
                        DataContext.SaveChanges();
                        //DataContext.SubmitChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion MeasurementUnits

        internal bool SaveMeasurementUnitsConvert(measurementunitsconverts convert)
        {
            using (DataContext)
            {
                try
                {
                    if (!DataContext.measurementunitsconverts.Contains(convert))
                    {
                        convert.CreatedDate = DateTime.Now;
                        convert.ModifiedDate = DateTime.Now;
                        DataContext.measurementunitsconverts.Add(convert);
                        //DataContext.MeasurementUnitsConverts.InsertOnSubmit(convert);
                    }
                    else
                    {
                        measurementunitsconverts itemToSave = DataContext.measurementunitsconverts.Single(muc => muc.ConvertId == convert.ConvertId);
                        itemToSave.FoodId = convert.FoodId;
                        itemToSave.FromUnitId = convert.FromUnitId;
                        itemToSave.FromQuantity = convert.FromQuantity;
                        itemToSave.ToUnitId = convert.ToUnitId;
                        itemToSave.ToQuantity = convert.ToQuantity;
                        itemToSave.ModifiedDate = DateTime.Now;
                    }

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

        internal measurementunitsconverts[] GetMeasurementUnitsConvertList()
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.Food);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.FromMeasurementUnit);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.ToMeasurementUnit);

                //DataContext.LoadOptions = dlo;

                try
                {
                    var list = DataContext.measurementunitsconverts.OrderBy(sd => sd.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal measurementunitsconverts GetMeasurementUnitsConvert(int id)
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.Food);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.FromMeasurementUnit);
                //dlo.LoadWith<MeasurementUnitsConvert>(m => m.ToMeasurementUnit);
                //DataContext.LoadOptions = dlo;

                try
                {
                    measurementunitsconverts unit = DataContext.measurementunitsconverts.SingleOrDefault(mu => mu.ConvertId == id);

                    return unit;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal users GetUserByName(string Name)
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<User>(u => u.Foods);
                //dlo.LoadWith<User>(u => u.Foods1);
                //dlo.LoadWith<User>(u => u.Recipes);
                //dlo.LoadWith<User>(u => u.UserFavoriteRecipes);
                //dlo.LoadWith<User>(u => u.UserType);
                //dlo.LoadWith<User>(u => u.Menus);
                //DataContext.LoadOptions = dlo;

                try
                {

                    users user = DataContext.users.SingleOrDefault(u => u.Name.Trim() == Name.Trim());

                    return user;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int GetNextTempUser(int anonymous)
        {
            using (DataContext)
            {
                try
                {
                    //int anonymous = int.Parse(ConfigurationManager.AppSettings["anonymous"]);
                    int? lastTempUser = (from m in DataContext.menus
                                         where m.UserId == anonymous
                                         select m.TempUserId)
                                        .Max();
                    if (lastTempUser.HasValue)
                    {
                        return lastTempUser.Value + 1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }

        internal int GetUsersNum()
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<User>(u => u.Foods);
                //dlo.LoadWith<User>(u => u.Foods1);
                //dlo.LoadWith<User>(u => u.Recipes);
                //dlo.LoadWith<User>(u => u.UserFavoriteRecipes);
                //dlo.LoadWith<User>(u => u.UserType);
                //dlo.LoadWith<User>(u => u.Menus);
                //DataContext.LoadOptions = dlo;

                try
                {
                    return DataContext.users.Count();
                }
                catch
                {
                    return -1;
                }
            }
        }

        internal users GetUser(int Id)
        {
            //using (DataContext)
            //{
                try
                {
                users user = DataContext.users.SingleOrDefault(u => u.UserId == Id);

                    return user;
                }
                catch
                {
                    return null;
                }
            //}
        }

        internal users GetUser(string userName, string password)
        {
            //using (DataContext)
            //{
            users user = DataContext.users.SingleOrDefault(u => u.Name == userName && u.Password == password);
                return user;
            //}
        }

        internal users GetUserEx(int Id)
        {
            using (DataContext)
            {
                //DataLoadOptions dlo = new DataLoadOptions();
                //dlo.LoadWith<User>(u => u.UserFavoriteRecipes);
                //dlo.LoadWith<User>(u => u.UserFavoriteMenus);
                //dlo.LoadWith<UserFavoriteMenu>(ufm => ufm.Menu);
                //dlo.LoadWith<UserFavoriteRecipe>(ufr => ufr.Recipe); 
                //DataContext.LoadOptions = dlo;

                try
                {
                    users user = DataContext.users.SingleOrDefault(u => u.UserId == Id);

                    return user;
                }
                catch
                {
                    return null;
                }
            }
        }


        internal bool SaveUser(users userToSave)
        {
            using (DataContext)
            {
                users user = DataContext.users.SingleOrDefault(p => p.Name == userToSave.Name && p.Password == userToSave.Password);
                if (user == null)
                {
                    DateTime currentDate = DateTime.Now;
                    userToSave.UserId = 0;

                    DataContext.users.Add(userToSave);
                    //DataContext.Users.InsertOnSubmit(userToSave);
                }
                else
                {
                    users update = DataContext.users.Single(u => u.UserId == userToSave.UserId);
                    update.DisplayName = userToSave.DisplayName;
                    update.Email = userToSave.Email;
                    update.FirstName = userToSave.FirstName;
                    update.LastName = userToSave.LastName;
                    update.AgreeToMail = userToSave.AgreeToMail;
                    update.Name = userToSave.Name;
                    update.Password = userToSave.Password;
                }

                DataContext.SaveChanges();
                //DataContext.SubmitChanges();
                return true;
            }
        }

        internal foodcategories GetFoodCategoryByName(string name)
        {
            using (DataContext)
            {
                try
                {
                    foodcategories cat = DataContext.foodcategories.Single(c => c.FoodCategoryName == name.Trim());
                    //cat.ParentFoodCategory = DataContext.FoodCategories.SingleOrDefault(c => c.FoodCategoryId == cat.ParentCategoryId);
                    //cat.AllowDelete = (cat.FoodCategories.Count == 0);
                    //if (cat.AllowDelete)
                    //{
                    //    //check if this FoodCategory has children FoodCategories
                    //    cat.AllowDelete = !DataContext.FoodCategories.Any(c => c.ParentCategoryId == cat.FoodCategoryId);
                    //}
                    return cat;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal void ReplaceUserIds(int sourceUserId, int targetUserId)
        {
            using (DataContext)
            {
                // foods
                var foods = from f in DataContext.food
                            where f.CreatedBy == sourceUserId
                            select f;

                foreach (var food in foods)
                {
                    food.CreatedBy = targetUserId;
                }

                var foods2 = from f in DataContext.food
                             where f.ModifiedBy == sourceUserId
                             select f;

                foreach (var food in foods2)
                {
                    food.ModifiedBy = targetUserId;
                }

                // menus
                var menus = from m in DataContext.menus
                            where m.UserId == sourceUserId
                            select m;

                foreach (var menu in menus)
                {
                    menu.UserId = targetUserId;
                }

                // recipes
                var recipes = from r in DataContext.recipes
                              where r.UserId == sourceUserId
                              select r;

                foreach (var recipe in recipes)
                {
                    recipe.UserId = targetUserId;
                }

                // favorites
                //var favs = from f in DataContext.UserFavoriteRecipes
                //           where f.UserId == sourceUserId
                //           select f;

                //foreach (var fav in favs)
                //{
                //    UserFavoriteRecipe fav2 = new UserFavoriteRecipe();
                //    fav2.UserId = targetUserId;
                //    fav2.RecipeId = fav.RecipeId;
                //    DataContext.UserFavoriteRecipes.InsertOnSubmit(fav2);
                //    DataContext.UserFavoriteRecipes.DeleteOnSubmit(fav);
                //}

                DataContext.SaveChanges();
                //DataContext.SubmitChanges();
            }
        }

        internal bool DeleteFood(int foodId)
        {
            using (DataContext)
            {
                try
                {
                    food Item = DataContext.food.Single(f => f.FoodId == foodId);
                    DataContext.food.Remove(Item);
                    DataContext.SaveChanges();
                    //DataContext.Foods.DeleteOnSubmit(Item);
                    //DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal articles GetArticleById(int articleId)
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<Article>(art => art.ArticleId);
                    //DataContext.LoadOptions = dlo;

                    articles article = DataContext.articles.SingleOrDefault(a => a.ArticleId == articleId );

                    return article;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal articles[] GetArticlesList()
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<Article>(art => art.ArticleId);
                    //DataContext.LoadOptions = dlo;

                    var list = from ar in DataContext.articles.Where(ar => ar.ArticleId != 6 )
                               select ar;

                    return list.ToArray();                    
                }
                catch
                {
                    return null;
                }
            }

        }

        internal bool CreateOrUpdateArticle(int id, string title, string abs, string body, string publisher, out int returnedId )
        {
            using (DataContext)
            {
                try
                {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<Article>(art => art.ArticleId);
                    //DataContext.LoadOptions = dlo;

                    articles article = DataContext.articles.SingleOrDefault(art => art.ArticleId == id);

                    bool isNew = false;

                    if (article == null)
                    {
                        article = new articles();
                        article.CreatedDate = DateTime.Now;
                        isNew = true;
                    }

                    article.Title = title;
                    article.Abstract = abs;
                    article.Body = body;
                    article.Publisher = publisher;
                    article.ModifiedDate = DateTime.Now;

                    if (isNew)
                    {
                        DataContext.articles.Add(article);
                        //DataContext.Articles.InsertOnSubmit(article);
                    }

                    DataContext.SaveChanges();
                    //DataContext.SubmitChanges();

                    returnedId = article.ArticleId;

                    return true;
                }
                catch
                {
                    returnedId = -1;
                    return false;
                }
            }

        }
    }
}
