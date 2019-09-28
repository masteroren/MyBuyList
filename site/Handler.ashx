<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Reflection;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using ProperServices.Common.Log;
using System.Collections.Generic;
using System.Linq;
using MyBuyListShare.Models;
using MyBuyListShare.Classes;

[Serializable]
internal class SearchItem
{
    public string value { get; set; }
    public string label { get; set; }
}

public class Handler : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string method = context.Request["method"];
        if (!string.IsNullOrEmpty(method))
        {
            // Calling methods using reflection
            MethodInfo methodInfo = typeof(Handler).GetMethod(method);
            if (methodInfo != null)
            {
                object response = methodInfo.Invoke(new Handler(), new object[] { context });
                context.Response.Write(response);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    //public string JsonSerializer<T>(T t)
    //{
    //    System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
    //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
    //    ser.WriteObject(ms, t);
    //    string jsonString = System.Text.Encoding.UTF8.GetString(ms.ToArray());
    //    ms.Close();
    //    return jsonString;
    //}

    public string IsLoggedIn(HttpContext context)
    {
        UserInfo userInfo = (UserInfo)context.Session[AppConstants.CURR_USER];
        if (userInfo != null)
        {
            return HttpHelper.JsonSerializer<UserInfo>(userInfo);
        }
        return null;
    }

    public void Logout(HttpContext context)
    {
        HttpContext.Current.Session[AppConstants.CURR_USER] = null;
    }

    public string Login(HttpContext context)
    {
        Logger.Info("Login", new object[] { });
        UserInfo userInfo = null;

        try
        {
            string userName = context.Request["UserName"];
            string password = context.Request["Password"];
            userInfo = LoginHelper.Login(userName, password);
            context.Session.Add(AppConstants.CURR_USER, userInfo);
            HttpContext.Current.Session.Add(AppConstants.CURR_USER, userInfo);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Login", new object[] { });
        }

        return HttpHelper.JsonSerializer<UserInfo>(userInfo);
    }

    #region food
    public int AddFood(HttpContext context)
    {
        //try
        //{
        //    string foodName = context.Request["foodName"];
        //    food food = BusinessFacade.Instance.AddFood(foodName, 0);
        //    if (food != null)
        //        return food.FoodId;
        //}
        //catch(Exception ex)
        //{
        //}

        return -1;
    }

    public int GetFoodCategory(HttpContext context)
    {
        string foodName = context.Request["foodName"];
        try
        {
            food food = BusinessFacade.Instance.GetFoodsList().SingleOrDefault(p => p.FoodName == foodName);
            if (food != null)
                return food.FoodCategoryId;
        }
        catch (Exception ex)
        {

        }

        return 0;
    }
    #endregion

    public int AddShoppingListCustomIngrediant(HttpContext context)
    {
        string itemName = context.Request["ItemName"];
        int measureUnit = int.Parse(context.Request["ItemMeasureId"]);
        int itemQuantity = int.Parse(context.Request["ItemQuantity"]);

        //User user = (User)HttpContext.Current.Session[AppConstants.CURR_USER];
        //if (user != null)
        //{
        //    MissingList missingList = BusinessFacade.Instance.GetMissingList(user.UserId);
        //    int listId;
        //    if (missingList != null)
        //        listId = missingList.ID;
        //    else
        //        listId = BusinessFacade.Instance.AddMissingList(user.UserId);

        //    MissingListDetail missingListDetails = BusinessFacade.Instance.AddMissingListItem(itemName, measureUnit, itemQuantity, listId);
        //    return missingListDetails.FOOD_ID;
        //}

        return -1;
    }

    public void SetSearchParameters(HttpContext context)
    {
        context.Session["SearchIn"] = context.Request["searchIn"];
        context.Session["SearchFor"] = context.Request["searchFor"];
    }

    public string GetSelectedRecipes(HttpContext context)
    {
        try
        {
            Dictionary<int, recipes> selectedRecipes = ProperControls.General.Utils.SelectedRecipes;
            return selectedRecipes.Count.ToString();
        }
        catch (Exception ex)
        {
            Logger.Write("GetSelectedRecipes failed", ex, Logger.Level.Error);
            return null;
        }
    }

    public string SearchValues(HttpContext context)
    {
        string value = context.Request["term"];
        string category = context.Request["category"];
        Logger.Write(string.Format("Search Values: term -> {0}, Category -> {1}", value, category), Logger.Level.Info);

        try
        {
            ListResponse<RecipeModel[]> recipes = null;
            IEnumerable<menus> menus = null;

            users user;
            string isLoggedIn = IsLoggedIn(context);

            int totalPages;
            int numOfMenus;
            int numOfRecipes;

            switch (category)
            {
                case "0": // הכל
                          //recipes = BusinessFacade.Instance.SearchRecipes(value).Take(10);
                    menus = BusinessFacade.Instance.SearchMenus(value).Take(10);
                    break;
                case "1": // מתכונים
                    recipes = HttpHelper.Get<ListResponse<RecipeModel[]>>(string.Format("recipes?pageSize=10&searchQuery={0}", value));
                    break;
                case "2": // המתכונים שלי
                    if (isLoggedIn != null)
                    {
                        user = (users)HttpContext.Current.Session[AppConstants.CURR_USER];
                        //recipes = BusinessFacade.Instance.GetUserRecipesList(user.UserId).Where(p=>p.RecipeName.Contains(value)).Take(10);
                        //recipes = BusinessFacade.Instance.GetRecipesEx(MyBuyList.Shared.Enums.RecipeDisplayEnum.All, user.UserId, value, null, null, null, MyBuyList.Shared.Enums.RecipeOrderEnum.LastUpdate, 1, 7, out totalPages, out numOfRecipes);
                    }
                    break;
                case "3": // המתכונים המועדפים שלי
                    if (isLoggedIn != null)
                    {
                        user = (users)HttpContext.Current.Session[AppConstants.CURR_USER];
                        //recipes = BusinessFacade.Instance.GetUserFavoritesRecipes(user.UserId).Where(p => p.RecipeName.Contains(value)).Take(10);
                    }
                    break;
                case "4": // תפריטים
                          //menus = BusinessFacade.Instance.SearchMenus(value).Take(10);
                    menus = BusinessFacade.Instance.GetMenusEx(MyBuyList.Shared.Enums.RecipeDisplayEnum.All, -1, value, null, null, null, MyBuyList.Shared.Enums.RecipeOrderEnum.LastUpdate, 1, 7, out totalPages, out numOfMenus).Take(10);
                    break;
                case "5": // התפריטים שלי
                    if (isLoggedIn != null)
                    {
                        user = (users)HttpContext.Current.Session[AppConstants.CURR_USER];
                        //menus = BusinessFacade.Instance.GetUserMenusList(user.UserId).Where(p => p.MenuName.Contains(value)).Take(10);
                        menus = BusinessFacade.Instance.GetMenusEx(MyBuyList.Shared.Enums.RecipeDisplayEnum.MyRecipes, user.UserId, value, null, null, null, MyBuyList.Shared.Enums.RecipeOrderEnum.LastUpdate, 1, 7, out totalPages, out numOfMenus).Take(10);
                    }
                    break;
                case "6": // התפריטים המועדפים שלי
                    if (isLoggedIn != null)
                    {
                        user = (users)HttpContext.Current.Session[AppConstants.CURR_USER];
                        //menus = BusinessFacade.Instance.GetUserFavoritesMenus(user.UserId).Where(p => p.MenuName.Contains(value)).Take(10);
                        menus = BusinessFacade.Instance.GetMenusEx(MyBuyList.Shared.Enums.RecipeDisplayEnum.MyFavoriteRecipes, user.UserId, value, null, null, null, MyBuyList.Shared.Enums.RecipeOrderEnum.LastUpdate, 1, 7, out totalPages, out numOfMenus).Take(10);
                    }
                    break;
            }

            IEnumerable<SearchItem> results1 = null;
            IEnumerable<SearchItem> results2 = null;
            string serialize = string.Empty;
            if (recipes != null)
            {
                results1 = from p in recipes.results
                           select new SearchItem
                           {
                               value = string.Format("{0}|0", p.id),
                               label = p.name
                           };
            }
            if (menus != null)
            {
                results2 = from p in menus
                           select new SearchItem
                           {
                               value = string.Format("{0}|1", p.MenuId),
                               label = p.MenuName
                           };
            }

            IEnumerable<SearchItem> results = null;

            if (results1 != null && results2 != null)
                results = results1.Concat(results2);

            if (results1 != null && results2 == null)
                results = results1;

            if (results1 == null && results2 != null)
                results = results2;

            if (results != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serialize = serializer.Serialize(results.Take(10));

                return serialize;
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            Logger.Write("SearchValues", ex, Logger.Level.Error);
            return string.Empty;
        }
    }

    public bool RemoveFromMissingList(HttpContext context)
    {
        int foodId = 0;
        try
        {
            users user = null;
            if (context.Session[AppConstants.CURR_USER] != null)
                user = (users)HttpContext.Current.Session[AppConstants.CURR_USER];

            if (user != null)
            {
                int.TryParse(context.Request["foodId"], out foodId);
                //BusinessFacade.Instance.DeleteFromMissingList(MyBuyList.BusinessLayer.Managers.RetrievType.ByFoodId, foodId, user.UserId);
                return true;
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "RemoveFromMissingList -> Remove Item {0} Failed", new object[] { foodId });
        }

        return false;
    }
}