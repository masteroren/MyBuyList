using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System;
using System.Data.Linq;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

public partial class PageFood : BasePage
{
    int FoodId
    {
        get { return ViewState["FoodId"] == null ? 0 : (int)ViewState["FoodId"]; }
        set { ViewState["FoodId"] = value; }
    }

    Binary FoodPicture
    {
        get { return (Binary)ViewState["FoodPicture"]; }
        set { ViewState["FoodPicture"] = value; }
    }

    int CreatedBy
    {
        get { return ViewState["CreatedBy"] == null ? 0 : (int)ViewState["CreatedBy"]; }
        set { ViewState["CreatedBy"] = value; }
    }

    DateTime CreatedDate
    {
        get { return ViewState["CreatedDate"] == null ? DateTime.MinValue : (DateTime)ViewState["CreatedDate"]; }
        set { ViewState["CreatedDate"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            users u = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);
            if (u != null)
            {
                if (u.UserTypeId != AppEnv.USER_ADMIN)
                {
                    AppEnv.MoveToDefaultPage();
                }
                else
                {
                    this.ddlCategory.DataSource = BusinessFacade.Instance.GetFoodCategoriesList();
                    this.ddlCategory.DataTextField = "FoodCategoryName";
                    this.ddlCategory.DataValueField = "FoodCategoryId";
                    this.ddlCategory.DataBind();

                    this.ddlCalculateUnit.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
                    this.ddlCalculateUnit.DataTextField = "UnitName";
                    this.ddlCalculateUnit.DataValueField = "UnitId";
                    this.ddlCalculateUnit.DataBind();

                    if (!string.IsNullOrEmpty(this.Request["foodId"]))
                    {
                        this.FoodId = int.Parse(this.Request["foodId"]);
                        food food = BusinessFacade.Instance.GetFood(this.FoodId);
                        if (food != null)
                        {
                            this.txtFoodName.Text = food.FoodName;
                            this.ddlCategory.Text = food.FoodCategoryId.ToString();
                            this.ddlCalculateUnit.Text = food.CalculateUnitId.ToString();
                            this.txtRemarks.Text = food.Remarks;
                            this.CreatedBy = food.CreatedBy;
                            this.CreatedDate = food.CreatedDate;
                            this.FoodPicture = food.Picture;

                            if (food.Picture != null)
                            {
                                this.imgContainer.ImageUrl = "~/ShowPicture.ashx?FoodId=" + food.FoodId;
                                this.imgContainer.Visible = true;
                            }

                            this.cbPrint.Checked = food.PrintPicture;

                            recipes[] recipes = BusinessFacade.Instance.GetRecipesListByFoodId(food.FoodId);
                            if (recipes.Length > 0)
                            {
                                this.lblRecipesCaption.Visible = true;
                                this.pnlRecipes.Visible = true;
                                this.rptRecipesLinks.DataSource = recipes;
                                this.rptRecipesLinks.DataBind();
                            }
                        }
                    }
                }
            }
        }
    }
    protected void cbDeletePicture_CheckedChanged(object sender, EventArgs e)
    {
        if (cbDeletePicture.Checked)
        {
            this.lblSelectPicture.Enabled = false;
            this.pictureFile.Enabled = false;
            imgContainer.Visible = false;
        }
        else
        {
            this.lblSelectPicture.Enabled = true;
            this.pictureFile.Enabled = true;
            if (!string.IsNullOrEmpty(imgContainer.ImageUrl))
            {
                imgContainer.Visible = true;
            }
        }
    }
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        if (this.pictureFile.HasFile && this.pictureFile.PostedFile != null)
        {
            string filename = Path.GetTempFileName();
            Bitmap bitmap = ImageHelper.ResizeImage(new Bitmap(pictureFile.PostedFile.InputStream, false), 200, 240);
            bitmap.Save(filename);
            this.imgContainer.ImageUrl = "~/ShowPicture.ashx?picture=" + new FileInfo(filename).Name;
            this.imgContainer.Visible = true;
            this.FoodPicture = ImageHelper.GetBitmapBytes(bitmap);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.Validate();
        if (!this.IsValid)
        {
            return;
        }

        food food = new food();
        if (this.FoodId == 0)
        {
            food.CreatedBy = ((BasePage)Page).UserId;
            food.IsTemporary = false;
        }
        else
        {
            food.CreatedBy = this.CreatedBy;
            food.CreatedDate = this.CreatedDate;
        }
        
        food.FoodId = this.FoodId;
        food.FoodName = this.txtFoodName.Text;
        food.FoodCategoryId = int.Parse(this.ddlCategory.SelectedItem.Value);
        food.CalculateUnitId = int.Parse(this.ddlCalculateUnit.SelectedItem.Value);
        food.Remarks = this.txtRemarks.Text;
        food.ModifiedBy = ((BasePage)Page).UserId;
        food.PrintPicture = cbPrint.Checked;

        if (cbDeletePicture.Checked)
        {
            food.Picture = null;
        }
        else
        {
            //food.Picture = FoodPicture;
        }

        if (BusinessFacade.Instance.SaveFood(food))
        {
            this.Response.Redirect("~/Admin/FoodsList.aspx");
        }
    }

    protected void custValidFoodName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        
        if (!string.IsNullOrEmpty(this.txtFoodName.Text))
        {
            args.IsValid = !BusinessFacade.Instance.CheckDuplicateFoodName(this.FoodId, this.txtFoodName.Text);
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void rptRecipesLinks_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            RepeaterItem rptItem = e.Item as RepeaterItem;
            recipes recipe = e.Item.DataItem as recipes;
            HyperLink link = rptItem.FindControl("lnkShowRecipe") as HyperLink;
            link.NavigateUrl = string.Format("~/RecipeDetails.aspx?recipeId={0}", recipe.RecipeId);
        }
    }
}
