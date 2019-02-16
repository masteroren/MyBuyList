using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using MyBuyListRest.Recipes;
using MyBuyListRest.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace MyBuyListRest
{
    [ServiceContract]
    public interface IRecipeService
    {
        [OperationContract]
        [WebGet(UriTemplate = "HelloWorld", ResponseFormat = WebMessageFormat.Json)]
        string HelloWorld();

        [OperationContract]
        [WebGet(UriTemplate = "/list", ResponseFormat = WebMessageFormat.Json)]
        RecipeServiceResponse GetRecipes();

        //[OperationContract]
        //[WebGet(UriTemplate = "/{id}", ResponseFormat = WebMessageFormat.Json)]
        //RecipeModel GetRecipe(string id);

        //[OperationContract]
        //[WebGet(UriTemplate = "/sort/{name}", ResponseFormat = WebMessageFormat.Json)]
        //List<RecipeModel> GetRecipesByName(string name);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void ShoppingList(string body);

        [OperationContract]
        [WebGet(UriTemplate = "ingredients/{prefix}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<KeyValueResponse> GetIngridiants(string prefix);

        [OperationContract]
        [WebInvoke(UriTemplate = "add", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AddRecipeResponse AddRecipe(AddRecipeRequest request);

        [OperationContract]
        [WebInvoke(UriTemplate = "DeleteRecipe", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        RecipeServiceResponse DeleteRecipe(DeleteRecipeRequest request);

        [OperationContract]
        [WebInvoke(UriTemplate = "test", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string test();

        [OperationContract]
        [WebInvoke(UriTemplate = "fractions", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        List<KeyValueResponse> GetFractions();

        [OperationContract]
        [WebInvoke(UriTemplate = "measure-units", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        List<KeyValueResponse> GetMeasureUnits();

        [OperationContract]
        [WebInvoke(UriTemplate = "categories", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        List<CategoriesResponse> GetCategories();
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RecipeService : IRecipeService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        //public RecipeModel GetRecipe(string id)
        //{
        //    return RecipeDL.GetRecipes(int.Parse(id));
        //}

        public RecipeServiceResponse GetRecipes()
        {
            RecipeServiceResponse response = new RecipeServiceResponse();
            //try
            //{
            //    response.recipes = RecipeDL.GetRecipes();
            //}
            //catch(Exception ex)
            //{
            //    response.ex = ex;
            //}
            return response;
        }

        //public List<RecipeModel> GetRecipesByName(string name)
        //{
        //    return RecipeDL.GetRecipesByName(name);
        //}

        public void ShoppingList(string body)
        {
        }

        public List<KeyValueResponse> GetIngridiants(string prefix)
        {
            List<KeyValueResponse> response = new List<KeyValueResponse>();
            Dictionary<int, string> foodList = BusinessFacade.Instance.GetFoodList(prefix);

            foreach (KeyValuePair<int, string> item in foodList)
            {
                response.Add(new KeyValueResponse { label = item.Value, value = item.Key });
            }
            return response;
        }

        public RecipeServiceResponse DeleteRecipe(DeleteRecipeRequest request)
        {
            RecipeServiceResponse response = new RecipeServiceResponse();
            try
            {
                int numOfDeleted = BusinessFacade.Instance.DeleteRecipe(request.recipeId);
                response.status = numOfDeleted == 1;
                response.deletedId = request.recipeId;
            }
            catch (Exception ex)
            {
                response.ex = ex;
                response.status = false;
            }
            return response;
        }

        public string test()
        {
            return "Test successful";
        }

        public List<KeyValueResponse> GetFractions()
        {
            List<KeyValueResponse> response = new List<KeyValueResponse>();
            response.Add(new KeyValueResponse { label = "⅛", value = 0.125 });
            response.Add(new KeyValueResponse { label = "¼", value = 0.25 });
            response.Add(new KeyValueResponse { label = "⅓", value = 0.33 });
            response.Add(new KeyValueResponse { label = "½", value = 0.5 });
            response.Add(new KeyValueResponse { label = "⅔", value = 0.66 });
            response.Add(new KeyValueResponse { label = "¾", value = 0.75 });
            return response;
        }

        public List<KeyValueResponse> GetMeasureUnits()
        {
            List<KeyValueResponse> response = new List<KeyValueResponse>();

            try
            {
                List<MeasurementUnit> measurementUnit = BusinessFacade.Instance.GetMeasurementUnitsList().ToList();

                measurementUnit.ForEach(item =>
                {
                    response.Add(new KeyValueResponse { label = item.UnitName, value = item.UnitId });
                });
            }
            catch(Exception ex)
            {
            }


            return response;
        }

        public List<CategoriesResponse> GetCategories()
        {
            List<CategoriesResponse> response = new List<CategoriesResponse>();

            try
            {
                List<MyBuyList.Shared.Entities.Category> categories = BusinessFacade.Instance.GetCategoriesList().OrderBy(x => x.ParentCategoryId).ToList();

                categories.ForEach(item =>
                {
                    response.Add(new CategoriesResponse { id = item.CategoryId, name = item.CategoryName, parentId = item.ParentCategoryId });
                });
            }
            catch (Exception ex)
            {

            }

            return response;

        }

        private int GetTimeInMinutes(int time, int unitValue)
        {
            int timeInMinutes = 0;
            if (unitValue == 0) //minutes
            {
                timeInMinutes = time;
            }
            if (unitValue == 1) //hours
            {
                timeInMinutes = time * 60;
            }

            return timeInMinutes;
        }

        public AddRecipeResponse AddRecipe(AddRecipeRequest request)
        {
            int recipeId;
            List<Ingredients> ingridiants = new List<Ingredients>();
            List<SRL_RecipeCategory> categories = new List<SRL_RecipeCategory>();
            AddRecipeResponse response = new AddRecipeResponse();
            try
            {
                Recipe recipe = new Recipe();

                //if (fuRecipeImage.HasFile && fuRecipeImage.PostedFile != null && ImageHelper.IsImage(fuRecipeImage.PostedFile.FileName))
                //{
                //    Bitmap bitmap = ImageHelper.ResizeImage(new Bitmap(this.fuRecipeImage.PostedFile.InputStream, false), 300, 231);
                //    RecipePicture = ImageHelper.GetBitmapBytes(bitmap);
                //}

                recipe.RecipeName = request.recipe.name;
                recipe.IsPublic = request.recipe.shared;
                recipe.Description = request.recipe.description;
                recipe.Tags = request.recipe.tags;
                recipe.PreparationMethod = request.recipe.instructions;
                recipe.Remarks = request.recipe.comments;
                recipe.Tools = request.recipe.tools;
                recipe.DifficultyLevel = request.recipe.level;
                recipe.PreperationTime = GetTimeInMinutes(request.recipe.prepareFor, request.recipe.prepareUnit);
                recipe.CookingTime = this.GetTimeInMinutes(request.recipe.cookFor, request.recipe.cookUnit);
                recipe.Servings = request.recipe.servings;
                recipe.UserId = request.recipe.userId;

                //recipe.VideoLink = request.link;
                //recipe.Picture = this.RecipePicture;

                request.recipe.ingredients.ForEach(item =>
                {
                    Ingredients ingredient = new Ingredients
                    {
                        FoodId = item.id,
                        FoodName = item.name,
                        MeasurementUnitId = item.measureUnit,
                        Quantity = (decimal)item.quantity + (decimal)item.fraction,
                        Remarks = item.text
                    };
                    ingridiants.Add(ingredient);
                });

                request.categories.ForEach(item =>
                {
                    SRL_RecipeCategory category = new SRL_RecipeCategory(0, item.id, item.name);
                    categories.Add(category);
                });


                BusinessFacade.Instance.SaveRecipe(recipe, ingridiants, categories, true, out recipeId);
            }
            catch
            {

            }

            return response;
        }
    }
}