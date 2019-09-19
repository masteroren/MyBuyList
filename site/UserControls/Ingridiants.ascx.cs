using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using ProperServices.Common.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Ingridiants : UserControl
{
    private string JsonSerializer<T>(T t)
    {
        System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        ser.WriteObject(ms, t);
        string jsonString = System.Text.Encoding.UTF8.GetString(ms.ToArray());
        ms.Close();
        return jsonString;
    }

    public List<ingredients> ListOfIngediants
    {
        get
        {
            try
            {
                if (!string.IsNullOrEmpty(hfIngridiants.Value))
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    List<ingredients> ingredients = jsSerializer.Deserialize<List<ingredients>>(hfIngridiants.Value);
                    return ingredients;
                }
                return null;
            }
            catch(Exception ex)
            {
                Logger.Write("Retrieve ListOfIngediants -> get failed", ex, Logger.Level.Error);
                return null;
            }
        }
        set
        {
            try
            {
                ViewState["Ingredients"] = value;

                Logger.Write("Retrieve ListOfIngediants -> Before Serialized", Logger.Level.Info);

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                hfIngridiants.Value = jsSerializer.Serialize(value);

                Logger.Write(string.Format("Retrieve ListOfIngediants -> After Serialized, {0}", hfIngridiants.Value.Replace("{", "{{").Replace("}", "}}")), Logger.Level.Info);
            }
            catch(Exception ex)
            {
                Logger.Write("Retrieve ListOfIngediants -> set failed", ex, Logger.Level.Error);
                hfIngridiants.Value = string.Empty;
            }
        }
    }

    public int recipeId {
        get
        {
            return int.Parse(hfRecipeId.Value);
        }
        set
        {
            hfRecipeId.Value = value.ToString();
        }
    }

    public string Ingridiants
    {
        get
        {
            return hfIngridiants.Value;
        }
        set
        {
            hfIngridiants.Value = value;
        }
    }

    public string DecimalSeperator
    {
        get
        {
            return ConfigurationManager.AppSettings["NumberSeperator"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        try
        {
            string decimalSeperator = DecimalSeperator;
            ddlFractions.Items.Add(new ListItem("", ""));
            ddlFractions.Items.Add(new ListItem("¼", string.Format("0{0}25", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("⅓", string.Format("0{0}33", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("½", string.Format("0{0}5", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("⅔", string.Format("0{0}66", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("¾", string.Format("0{0}75", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("⅛", string.Format("0{0}125", decimalSeperator)));

            ddlMeasurementUnits.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
            ddlMeasurementUnits.DataTextField = "UnitName";
            ddlMeasurementUnits.DataValueField = "UnitId";
            ddlMeasurementUnits.DataBind();
        }
        catch(Exception ex)
        {
            Logger.Write("Ingrediants.Page_Load -> failed", ex, Logger.Level.Error);
        }
    }
}