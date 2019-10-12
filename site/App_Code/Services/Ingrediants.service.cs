using MyBuyListShare.Models;
using System.Reactive.Subjects;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Ingrediants
/// </summary>
public class IngrediantsService
{
    public static Subject<IngrediantModel> subject = new Subject<IngrediantModel>();

    public IngrediantsService()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}