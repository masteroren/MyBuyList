using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System;
using System.Web.UI.WebControls;

public partial class PageShopDepartment : BasePage
{
    int? DepartmentId
    {
        get { return (int?)ViewState["DepartmentId"]; }
        set { ViewState["DepartmentId"] = value; }
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
                if (!string.IsNullOrEmpty(this.Request["depId"]))
                {
                    this.DepartmentId = int.Parse(this.Request["depId"]);
                    shopdepartments department = BusinessFacade.Instance.GetShopDepartment(this.DepartmentId.Value);
                    if (department != null)
                    {
                        this.txtDepName.Text = department.ShopDepartmentName;
                    }
                }
            }                  
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.Validate();
        if (!this.IsValid)
        {
            return;
        }

        shopdepartments dep = new shopdepartments();
        dep.ShopDepartmentId = this.DepartmentId == null ? -1 : this.DepartmentId.Value;
        dep.ShopDepartmentName = this.txtDepName.Text;

        foodcategories f = null;
        if (dep.ShopDepartmentId == -1)
        {
            f = new foodcategories();
            f.FoodCategoryId = -1;
        }
        else
        {
            shopdepartments department = BusinessFacade.Instance.GetShopDepartment(this.DepartmentId.Value);
            f = BusinessFacade.Instance.GetFoodCategoryByName(department.ShopDepartmentName);
        }

        if (BusinessFacade.Instance.SaveShopDepartment(dep))
        {
            f.FoodCategoryName = dep.ShopDepartmentName;
            f.ShopDepartmentId = dep.ShopDepartmentId;
            if (BusinessFacade.Instance.SaveFoodCategory(f))
            {
                this.Response.Redirect("~/Admin/ShopDepartmentsList.aspx");
            }
            else
            {
                BusinessFacade.Instance.DeleteShopDepartment(dep.ShopDepartmentId);
            }
        }    
    }
    protected void custValidDepartmentName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtDepName.Text))
        {
            args.IsValid = !BusinessFacade.Instance.CheckDuplicateShopDepartmentName(this.DepartmentId == null ? -1 : this.DepartmentId.Value,
                                                                                     this.txtDepName.Text);
        }
        else
        {
            args.IsValid = true;
        }
    }
}
