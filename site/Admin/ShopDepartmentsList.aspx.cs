using AjaxControlToolkit;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System;
using System.Web.UI.WebControls;

public partial class PageShopDepartmentsList : BasePage
{
    ShopDepartment[] Data
    {
        get { return (ShopDepartment[]) this.Cache["ShopDepartments"]; }
        set { this.Cache["ShopDepartments"] = value; }
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
                this.Rebind();
            }
        }
    }

    private void Rebind()
    {
        this.Data = BusinessFacade.Instance.GetShopDepartmentsList();
        this.rolShopDepartments.DataSource = this.Data;
        this.rolShopDepartments.DataBind();
    }


    protected void rolShopDepartments_ItemDataBound(object sender, ReorderListItemEventArgs e)
    {
        ReorderListItem rolItem = e.Item as ReorderListItem;
        LinkButton btn = rolItem.FindControl("btnUpdate") as LinkButton;
        ShopDepartment dep = e.Item.DataItem as ShopDepartment;
        if (dep != null)
        {
            btn.PostBackUrl = string.Format("~/Admin/ShopDepartment.aspx?depId={0}", dep.ShopDepartmentId);
        }

        btn = rolItem.FindControl("btnDelete") as LinkButton;
        //btn.Visible = dep.AllowDelete;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["depId"]))
        {
            int departmentId = int.Parse(btn.Attributes["depId"]);
            ShopDepartment sd = BusinessFacade.Instance.GetShopDepartment(departmentId);

            string name = string.Empty;

            if(sd != null)
            {
                 name = sd.ShopDepartmentName;
            }

            if (BusinessFacade.Instance.DeleteShopDepartment(departmentId))
            {
                FoodCategory f = BusinessFacade.Instance.GetFoodCategoryByName(name);

                if(f != null)
                {
                    BusinessFacade.Instance.DeleteFoodCategory(f.FoodCategoryId);
                }

                this.Rebind();
            }
        }
    }

    protected void rolShopDepartments_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    {
       this.DoReorder(e.OldIndex, e.NewIndex);
    }

    private void DoReorder(int oldIndex, int newIndex)
    {
        ShopDepartment[] arr = this.Data;

        if (oldIndex < newIndex) //item moved down
        {
            int tempOrder = arr[newIndex].SortOrder;
            
            for (int i = newIndex; i > oldIndex; i--)
            {
                arr[i].SortOrder = arr[i-1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }
        else  //item moved up
        {
            int tempOrder = arr[newIndex].SortOrder;

            for (int i = newIndex; i < oldIndex; i++)
            {
                arr[i].SortOrder = arr[i+1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }

        if (BusinessFacade.Instance.ReorderShopDepartments(arr))
        {
            this.Rebind();
        }
    }
}
