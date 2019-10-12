using MyBuyListShare.Classes;
using MyBuyListShare.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            IngrediantModelContainer ingrediantModelContainer = Json.JsonDeserializer<IngrediantModelContainer>((string)ViewState["Ingrediants"]);
            return ingrediantModelContainer.ingrediants;
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
        ddlFractions.Items.Add(new ListItem("", ""));
        ddlFractions.Items.Add(new ListItem("¼", string.Format("{0}", 0.25)));
        ddlFractions.Items.Add(new ListItem("⅓", string.Format("{0}", 0.33)));
        ddlFractions.Items.Add(new ListItem("½", string.Format("{0}", 0.5)));
        ddlFractions.Items.Add(new ListItem("⅔", string.Format("{0}", 0.66)));
        ddlFractions.Items.Add(new ListItem("¾", string.Format("{0}", 0.75)));
        ddlFractions.Items.Add(new ListItem("⅛", string.Format("{0}", 0.125)));

        var units = HttpHelper.GetMeny<UnitModel>("general/units");
        ddlMeasurementUnits.DataSource = units.results;
        ddlMeasurementUnits.DataTextField = "unitName";
        ddlMeasurementUnits.DataValueField = "unitId";
        ddlMeasurementUnits.DataBind();
    }

    private bool IsExist(int id)
    {
        List<IngrediantModel> ingrediants = Ingrediants;
        IngrediantModel f = ingrediants.Find(item => item.id == id);
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
        IngrediantModel SelectedIngrediant = ingrediants.Find(item => item.id == Convert.ToInt32(ViewState["SelectedIngrediantId"]));
        if (SelectedIngrediant != null)
        {
            SelectedIngrediant.quantity0 = TextBox1.Text;
            SelectedIngrediant.quantity1 = ddlFractions.SelectedValue;
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
        if (TextBox1.Text == "" || IngridiantName.Text == "")
        {
            return;
        }

        int id = Convert.ToInt32(ddlMeasurementUnits.SelectedItem.Value);
        if (IsExist(Convert.ToInt32(ddlMeasurementUnits.SelectedItem.Value)))
        {
            ModifyIngrediant();
        } else
        {
            IngrediantModel ingrediantModel = new IngrediantModel
            {
                id = id,
                quantity0 = TextBox1.Text,
                unit = ddlMeasurementUnits.SelectedItem.Text,
                unitId = Convert.ToInt32(ddlMeasurementUnits.SelectedItem.Value),
                name = IngridiantName.Text
            };
            List<IngrediantModel> ingrediants = Ingrediants;
            ingrediants.Add(ingrediantModel);
            Ingrediants = ingrediants;
        }
    }

    // Edit
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string id = ((LinkButton)sender).CommandArgument;
        List<IngrediantModel> ingrediants = Ingrediants;
        IngrediantModel ingrediantModel = ingrediants.Find(item => item.id == Convert.ToInt32(id));
        TextBox1.Text = ingrediantModel.quantity0;
        ddlFractions.SelectedValue = ingrediantModel.quantity1 == "0" ? "" : ingrediantModel.quantity1;
        ddlMeasurementUnits.SelectedValue = ingrediantModel.unitId.ToString();
        IngridiantName.Text = ingrediantModel.name;
        txtFoodRemark.Text = ingrediantModel.howTo;
        ImageButton1.Visible = false;
        ImageButton2.Visible = true;
        ViewState["SelectedIngrediantId"] = ingrediantModel.id.Value;
    }

    // Delete
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string id = ((LinkButton)sender).CommandArgument;
        List<IngrediantModel> ingrediants = Ingrediants;
        int indx = ingrediants.FindIndex(item => item.id == Convert.ToInt32(id));
        ingrediants.RemoveAt(indx);
        Ingrediants = ingrediants;
        ImageButton1.Visible = true;
        ImageButton2.Visible = false;
        Clear();
    }

    private void Clear()
    {
        TextBox1.Text = "";
        ddlFractions.SelectedValue = "";
        ddlMeasurementUnits.SelectedValue = "0";
        IngridiantName.Text = "";
        txtFoodRemark.Text = "";
    }
}