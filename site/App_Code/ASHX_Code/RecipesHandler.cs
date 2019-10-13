using System.Web;
using System.Reflection;
using MyBuyListShare.Models;
using MyBuyListShare.Classes;

public class RecipesHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        string method = context.Request["method"];
        if (!string.IsNullOrEmpty(method))
        {
            // Calling methods using reflection
            MethodInfo methodInfo = typeof(RecipesHandler).GetMethod(method);
            if (methodInfo != null)
            {
                object response = methodInfo.Invoke(new RecipesHandler(), new object[] { context });
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

    public string IsUserFavoritRecipe(HttpContext context)
    {
        string userId = context.Request["userId"];
        string recipeId = context.Request["recipeId"];
        FavoritRecipesModel result = HttpHelper.Get<FavoritRecipesModel>(string.Format("users/{0}/recipes/{1}/favorites", userId, recipeId));
        return HttpHelper.JsonSerializer(result);
    }

    public void AddIngredianToRecipe(HttpContext context)
    {
        //IngrediantsService.subject.OnNext(new IngrediantModel
        //{
        //    quantity0 = context.Request["quantity0"],
        //    quantity1 = context.Request["quantity1"],
        //    unit = context.Request["unit"],
        //    name = context.Request["name"],
        //});
    }
}