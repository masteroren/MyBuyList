using MyBuyListShare.Models;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
[ScriptService]
public class AutoComplete : WebService
{
    public AutoComplete()
    {
    }

    [WebMethod]
    [ScriptMethod]
    public string[] GetIngrediants(string prefixText, int count)
    {
        Response<FoodModel> response = HttpHelper.GetMeny<FoodModel>(string.Format("foods?searchQuery={0}", prefixText));
        return response.results.Select(item => AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.FoodName, item.FoodId.ToString())).ToArray();
    }
}

