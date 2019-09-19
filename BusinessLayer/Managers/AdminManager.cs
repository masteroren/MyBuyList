
using MyBuyList.DataLayer;
using MyBuyList.Shared;

namespace MyBuyList.BusinessLayer.Managers
{
    class AdminManager
    {
        #region Category
        internal bool CheckDuplicateCategoryName(int categoryId, string categoryName)
        {
            return DataFacade.Instance.CheckDuplicateCategoryName(categoryId, categoryName);
        }

        internal bool CheckDuplicateMCategoryName(int categoryId, string categoryName)
        {
            return DataFacade.Instance.CheckDuplicateMCategoryName(categoryId, categoryName);
        }

        internal categories[] GetCategoriesList()
        {
            return DataFacade.Instance.GetCategoriesList();
        }

        internal mcategories[] GetMCategoriesList()
        {
            return DataFacade.Instance.GetMCategoriesList();
        }

        internal categories GetCategory(int categoryId)
        {
            return DataFacade.Instance.GetCategory(categoryId);
        }

        internal mcategories GetMenuCategory(int categoryId)
        {
            return DataFacade.Instance.GetMenuCategory(categoryId);
        }

        internal bool SaveCategory(categories category)
        {
            return DataFacade.Instance.SaveCategory(category);
        }

        internal bool SaveCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return DataFacade.Instance.SaveCategory(categoryId, categoryName, parentCategoryId);
        }

        internal bool SaveMenuCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return DataFacade.Instance.SaveMenuCategory(categoryId, categoryName, parentCategoryId);
        }

        internal bool DeleteCategory(int categoryId)
        {
            return DataFacade.Instance.DeleteCategory(categoryId);
        }

        internal bool DeleteMenuCategory(int categoryId)
        {
            return DataFacade.Instance.DeleteMenuCategory(categoryId);
        }
        #endregion Category


        #region foodcategories
        internal bool CheckDuplicateFoodCategoryName(int FoodCategoryId, string FoodCategoryName)
        {
            return DataFacade.Instance.CheckDuplicateFoodCategoryName(FoodCategoryId, FoodCategoryName);
        }

        internal bool CheckDuplicateFoodName(int FoodId, string FoodName)
        {
            return DataFacade.Instance.CheckDuplicateFoodName(FoodId, FoodName);
        }

        internal foodcategories[] GetFoodCategoriesList()
        {
            return DataFacade.Instance.GetFoodCategoriesList();
        }

        internal foodcategories GetFoodCategory(int FoodCategoryId)
        {
            return DataFacade.Instance.GetFoodCategory(FoodCategoryId);
        }

        internal bool SaveFoodCategory(foodcategories foodcategories)
        {
            return DataFacade.Instance.SaveFoodCategory(foodcategories);
        }

        internal bool SaveFoodCategory(int FoodCategoryId, string FoodCategoryName, int? parentFoodCategoryId)
        {
            return DataFacade.Instance.SaveFoodCategory(FoodCategoryId, FoodCategoryName, parentFoodCategoryId);
        }

        internal bool DeleteFoodCategory(int FoodCategoryId)
        {
            return DataFacade.Instance.DeleteFoodCategory(FoodCategoryId);
        }
        #endregion foodcategories

        internal food[] GetFoodsList()
        {
            return DataFacade.Instance.GetFoodsList();
        }

        internal food GetFood(int foodId)
        {
            return DataFacade.Instance.GetFood(foodId);
        }

        internal food GetFood(string name)
        {
            return DataFacade.Instance.GetFood(name);
        }

        //internal foodcategories[] GetFoodCategoriesList()
        //{
        //    return DataFacade.Instance.GetFoodCategoriesList();
        //}

        internal shopdepartments[] GetShopDepartmentsList()
        {
            return DataFacade.Instance.GetShopDepartmentsList();
        }

        internal bool CheckDuplicateShopDepartmentName(int departmentId, string departmentName)
        {
            return DataFacade.Instance.CheckDuplicateShopDepartmentName(departmentId, departmentName);
        }

        internal shopdepartments GetShopDepartment(int departmentId)
        {
            return DataFacade.Instance.GetShopDepartment(departmentId);
        }

        internal bool DeleteShopDepartment(int shopDepartmentId)
        {
            return DataFacade.Instance.DeleteShopDepartment(shopDepartmentId);
        }

        internal bool SaveShopDepartment(shopdepartments department)
        {
            return DataFacade.Instance.SaveShopDepartment(department);
        }

        internal bool ReorderShopDepartments(shopdepartments[] arr)
        {
            return DataFacade.Instance.ReorderShopDepartments(arr);
        }

        //item
        internal generalitems[] GetGeneralItemsList()
        {
            return DataFacade.Instance.GetGeneralItemsList();
        }

        internal bool CheckDuplicateGeneralItemName(int ItemId, string ItemName)
        {
            return DataFacade.Instance.CheckDuplicateGeneralItemName(ItemId, ItemName);
        }

        internal generalitems GetGeneralItem(int ItemId)
        {
            return DataFacade.Instance.GetGeneralItem(ItemId);
        }

        internal bool DeleteGeneralItem(int GeneralItemId)
        {
            return DataFacade.Instance.DeleteGeneralItem(GeneralItemId);
        }

        internal bool SaveGeneralItem(generalitems Item)
        {
            return DataFacade.Instance.SaveGeneralItem(Item);
        }

        internal bool ReorderGeneralItems(generalitems[] arr)
        {
            return DataFacade.Instance.ReorderGeneralItems(arr);
        }

        // item

        public bool CheckDuplicateMeasurementUnitName(int unitId, string unitName)
        {
            return DataFacade.Instance.CheckDuplicateMeasurementUnitName(unitId, unitName);
        }

        internal measurementunits GetMeasurementUnit(int unitId)
        {
            return DataFacade.Instance.GetMeasurementUnit(unitId);
        }

        internal measurementunits[] GetMeasurementUnitsList()
        {
            return DataFacade.Instance.GetMeasurementUnitsList();
        }

        internal bool DeleteMeasurementUnit(int unitId)
        {
            return DataFacade.Instance.DeleteMeasurementUnit(unitId);
        }

        internal bool SaveMeasurementUnit(measurementunits unit)
        {
            return DataFacade.Instance.SaveMeasurementUnit(unit);
        }

        internal bool ReorderMeasurementUnits(measurementunits[] arr)
        {
            return DataFacade.Instance.ReorderMeasurementUnits(arr);
        }

        internal bool SaveMeasurementUnitsConvert(measurementunitsconverts unit)
        {
            return DataFacade.Instance.SaveMeasurementUnitsConvert(unit);
        }

        internal measurementunitsconverts[] GetMeasurementUnitsConvertList()
        {
            return DataFacade.Instance.GetMeasurementUnitsConvertList();
        }

        internal measurementunitsconverts GetMeasurementUnitsConvert(int ConvertId)
        {
            return DataFacade.Instance.GetMeasurementUnitsConvert(ConvertId);
        }

        public int GetNextTempUser(int anonymous)
        {
            return DataFacade.Instance.GetNextTempUser( anonymous);
        }

        internal users GetUser(int Id)
        {
            return DataFacade.Instance.GetUser(Id);
        }

        public users GetUser(string userName, string password)
        {
            return DataFacade.Instance.GetUser(userName, password);
        }

        internal users GetUserEx(int Id)
        {
            return DataFacade.Instance.GetUserEx(Id);
        }

        internal bool SaveUser(users currUser)
        {
            return DataFacade.Instance.SaveUser(currUser);
        }

        internal users GetUserByUserName(string userName)
        {
            return DataFacade.Instance.GetUserByName(userName);
        }

        internal foodcategories GetFoodCategoryByName(string name)
        {
            return DataFacade.Instance.GetFoodCategoryByName(name);
        }

        internal int GetUsersNum()
        {
            return DataFacade.Instance.GetUsersNum();
        }

        internal bool DeleteFood(int foodId)
        {
             return DataFacade.Instance.DeleteFood(foodId);
        }

        internal articles GetArticleById(int articleId)
        {
            return DataFacade.Instance.GetArticleById(articleId);
        }

        internal articles[] GetArticlesList()
        {
            return DataFacade.Instance.GetArticlesList();
        }

        internal bool CreateOrUpdateArticle(int id, string title, string abs, string body, string publisher, out int returnedId)
        {
            return DataFacade.Instance.CreateOrUpdateArticle(id, title, abs, body, publisher, out returnedId);
        }
    }
}
