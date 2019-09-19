using AjaxControlToolkit;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System;
using System.Web.UI.WebControls;

public partial class PageGeneralItemsList : BasePage
{
    generalitems[] Data
    {
        get { return (generalitems[]) Cache["GeneralItems"]; }
        set { Cache["GeneralItems"] = value; }
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
                Rebind();
            }     
        }
    }

    private void Rebind()
    {
        Data = BusinessFacade.Instance.GetGeneralItemsList();
        rolGeneralItems.DataSource = Data;
        rolGeneralItems.DataBind();
    }


    protected void rolGeneralItems_ItemDataBound(object sender, ReorderListItemEventArgs e)
    {
        ReorderListItem rolItem = e.Item as ReorderListItem;
        LinkButton btn = rolItem.FindControl("btnUpdate") as LinkButton;
        generalitems Item = e.Item.DataItem as generalitems;
        if (Item != null)
        {
            btn.PostBackUrl = string.Format("~/Admin/generalitems.aspx?ItemId={0}", Item.GeneralItemId);
        }

        btn = rolItem.FindControl("btnDelete") as LinkButton;
        //btn.Visible = Item.AllowDelete;
        btn.Attributes.Add("ItemId", Item.GeneralItemId.ToString());
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["ItemId"]))
        {
            int ItemId = int.Parse(btn.Attributes["ItemId"]);
            if (BusinessFacade.Instance.DeleteGeneralItem(ItemId))
            {
                Rebind();
            }
        }
    }

    protected void rolGeneralItems_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    {
       DoReorder(e.OldIndex, e.NewIndex);
    }

    private void DoReorder(int oldIndex, int newIndex)
    {
        generalitems[] arr = Data;

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

        if (BusinessFacade.Instance.ReorderGeneralItems(arr))
        {
            Rebind();
        }
    }
}
