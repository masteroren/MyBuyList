using MyBuyListShare.Classes;
using MyBuyListShare.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Ingridiants : UserControl
{
    public List<IngrediantModel> Ingrediants
    {
        set
        {
            IngrediantModelContainer ingrediantModelContainer = new IngrediantModelContainer { ingrediants = value };
            ViewState.Add("Ingrediants", Json.JsonSerializer(ingrediantModelContainer));
            ListView1.DataSource = value;
            ListView1.DataBind();
        }
        get
        {
            if (ViewState["Ingrediants"] != null)
            {
                IngrediantModelContainer ingrediantModelContainer = Json.JsonDeserializer<IngrediantModelContainer>((string)ViewState["Ingrediants"]);
                return ingrediantModelContainer.ingrediants;
            }
            return null;
        }
    }

    public string DecimalSeperator
    {
        get
        {
            return ConfigurationManager.AppSettings["NumberSeperator"];
        }
    }

    private int SelectedIngrediantId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        string decimalSeperator = DecimalSeperator;
        ddlFractions.Items.Add(new ListItem("", "0"));
        ddlFractions.Items.Add(new ListItem("¼", 0.25.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"))));
        ddlFractions.Items.Add(new ListItem("⅓", 0.33.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"))));
        ddlFractions.Items.Add(new ListItem("½", 0.5.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"))));
        ddlFractions.Items.Add(new ListItem("⅔", 0.66.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"))));
        ddlFractions.Items.Add(new ListItem("¾", 0.75.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"))));
        ddlFractions.Items.Add(new ListItem("⅛", 0.125.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"))));

        var units = HttpHelper.GetMeny<UnitModel>("general/units");
        ddlMeasurementUnits.DataSource = units.results;
        ddlMeasurementUnits.DataTextField = "unitName";
        ddlMeasurementUnits.DataValueField = "unitId";
        ddlMeasurementUnits.DataBind();
    }

    private bool IsExist()
    {

        if (Ingrediants == null || Ingrediants.Count == 0)
        {
            Ingrediants = new List<IngrediantModel>();
            return false;
        }

        List<IngrediantModel> ingrediants = Ingrediants;
        IngrediantModel f = ingrediants.Find(item => item.foodId == Convert.ToInt32(IngridiantId.Value));
        return f != null;
    }

    // Modify Ingrediant
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ModifyIngrediant();
    }

    private void ModifyIngrediant()
    {
        List<IngrediantModel> ingrediants = Ingrediants;
        IngrediantModel SelectedIngrediant = ingrediants.Find(item => item.foodId == Convert.ToInt32(IngridiantId.Value));
        if (SelectedIngrediant != null)
        {
            int quantity0 = TextBox1.Text == "" ? 0 : Convert.ToInt32(TextBox1.Text);
            decimal quantity1 = Convert.ToDecimal(ddlFractions.SelectedValue, CultureInfo.CreateSpecificCulture("he-IL"));
            SelectedIngrediant.quantity = quantity0 + quantity1;
            SelectedIngrediant.quantity0 = quantity0;
            SelectedIngrediant.quantity1 = quantity1;
            SelectedIngrediant.unitId = Convert.ToInt32(ddlMeasurementUnits.SelectedValue);
            SelectedIngrediant.unit = ddlMeasurementUnits.SelectedItem.Text;
            SelectedIngrediant.name = IngridiantName.Text;
            SelectedIngrediant.howTo = txtFoodRemark.Text;
            Ingrediants = ingrediants;
            ImageButton1.Visible = true;
            ImageButton2.Visible = false;
            Clear();
        }
    }

    // Add Ingrediant
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if ((TextBox1.Text == "" || ddlFractions.SelectedValue == "") && IngridiantName.Text == "")
        {
            return;
        }

        int id = 0;
        if (IsExist())
        {
            ModifyIngrediant();
        } else
        {
            int quantity0 = TextBox1.Text == "" ? 0 : Convert.ToInt32(TextBox1.Text);
            decimal quantity1 = Convert.ToDecimal(ddlFractions.SelectedValue, CultureInfo.CreateSpecificCulture("he-IL"));

            IngrediantModel ingrediantModel = new IngrediantModel
            {
                id = ++id,
                quantity = quantity0 + quantity1,
                quantity0 = quantity0,
                quantity1 = quantity1,
                unit = ddlMeasurementUnits.SelectedItem.Text,
                unitId = Convert.ToInt32(ddlMeasurementUnits.SelectedItem.Value),
                name = IngridiantName.Text,
                foodId = Convert.ToInt32(IngridiantId.Value),
                howTo = txtFoodRemark.Text
            };
            List<IngrediantModel> ingrediants = Ingrediants;
            ingrediants.Add(ingrediantModel);
            Ingrediants = ingrediants;
            Clear();
        }
    }

    // Edit
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string id = ((LinkButton)sender).CommandArgument;
        List<IngrediantModel> ingrediants = Ingrediants;
        IngrediantModel ingrediantModel = ingrediants.Find(item => item.foodId == Convert.ToInt32(id));
        TextBox1.Text = ingrediantModel.quantity0 == 0 ? "" : ingrediantModel.quantity0.ToString();
        ddlFractions.SelectedValue = ingrediantModel.quantity1 == 0 ? "0" : ingrediantModel.quantity1.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"));
        ddlMeasurementUnits.SelectedValue = ingrediantModel.unitId.ToString();
        IngridiantName.Text = ingrediantModel.name;
        txtFoodRemark.Text = ingrediantModel.howTo;
        ImageButton1.Visible = false;
        ImageButton2.Visible = true;
        ViewState["SelectedIngrediantId"] = ingrediantModel.id.Value;
        IngridiantId.Value = ingrediantModel.foodId.ToString();
    }

    // Delete
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string id = ((LinkButton)sender).CommandArgument;
        List<IngrediantModel> ingrediants = Ingrediants;
        int indx = ingrediants.FindIndex(item => item.foodId == Convert.ToInt32(id));
        ingrediants.RemoveAt(indx);
        Ingrediants = ingrediants;
        ImageButton1.Visible = true;
        ImageButton2.Visible = false;
        Clear();
    }

    private void Clear()
    {
        TextBox1.Text = "";
        ddlFractions.SelectedValue = "0";
        ddlMeasurementUnits.SelectedValue = "0";
        IngridiantName.Text = "";
        txtFoodRemark.Text = "";
    }
}