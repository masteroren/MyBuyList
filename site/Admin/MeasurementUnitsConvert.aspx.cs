using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class PageMeasurementUnitsConvert : BasePage
{
    int ConvertId
    {
        get 
        { 
            return ViewState["ConvertId"] == null ? 0 : (int) ViewState["ConvertId"];
        }
        set
        {
            ViewState["ConvertId"] = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (((BasePage)Page).UserType != AppEnv.USER_ADMIN)
            {
                AppEnv.MoveToDefaultPage();
            }
            else
            {
                ddlFromMeasurementUnits.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
                ddlFromMeasurementUnits.DataTextField = "UnitName";
                ddlFromMeasurementUnits.DataValueField = "UnitId";
                ddlFromMeasurementUnits.DataBind();

                ddlToMeasurementUnits.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
                ddlToMeasurementUnits.DataTextField = "UnitName";
                ddlToMeasurementUnits.DataValueField = "UnitId";
                ddlToMeasurementUnits.DataBind();

                if (!string.IsNullOrEmpty(this.Request["ConvertId"]))
                {
                    ConvertId = int.Parse(this.Request["ConvertId"]);

                    MeasurementUnitsConvert convert = BusinessFacade.Instance.GetMeasurementUnitsConvert(ConvertId);

                    Food food = BusinessFacade.Instance.GetFood(convert.FoodId);

                    if (convert != null)
                    {
                        txtIngredientName.Text = food.FoodName;
                        txtFromQuantity.Text = convert.FromQuantity.ToString();
                        ddlFromMeasurementUnits.SelectedValue = convert.FromUnitId.ToString();
                        txtToQuantity.Text = convert.ToQuantity.ToString();
                        ddlToMeasurementUnits.SelectedValue = convert.ToUnitId.ToString();
                    }
                }
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.Validate();
        if (this.IsValid)
        {
            Food selectedFood = BusinessFacade.Instance.GetFood(this.txtIngredientName.Text);
            MeasurementUnitsConvert convert = new MeasurementUnitsConvert();
            convert.ConvertId = this.ConvertId;
            convert.FoodId = selectedFood.FoodId;
            convert.FromQuantity = decimal.Parse(this.txtFromQuantity.Text);
            convert.FromUnitId = int.Parse(this.ddlFromMeasurementUnits.SelectedValue);
            convert.ToQuantity = decimal.Parse(this.txtToQuantity.Text);
            convert.ToUnitId = int.Parse(this.ddlToMeasurementUnits.SelectedValue);


            if (BusinessFacade.Instance.SaveMeasurementUnitsConvert(convert))
            {
                this.Response.Redirect("~/Admin/MeasurementUnitsConvertList.aspx");
            }
        }
    }


    protected void custValidIngredientName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtIngredientName.Text))
        {
            Food selected = BusinessFacade.Instance.GetFood(this.txtIngredientName.Text);
            if (selected == null)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void custValidatorQty_ServerValidate(object source, ServerValidateEventArgs args)
    {
        decimal result = 0;
        args.IsValid = decimal.TryParse(args.Value, out result);
    }
}
