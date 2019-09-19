using AjaxControlToolkit;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System;
using System.Web.UI.WebControls;

public partial class PageMeausurementUnitsList : BasePage
{
    measurementunits[] Data
    {
        get { return (measurementunits[])Cache["MeasurementUnits"]; }
        set { Cache["MeasurementUnits"] = value; }
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
        Data = BusinessFacade.Instance.GetMeasurementUnitsList();
        //Array.Sort(Data);
        rolMeasurementUnits.DataSource = Data;
        rolMeasurementUnits.DataBind();
    }


    protected void rolMeasurementUnits_ItemDataBound(object sender, ReorderListItemEventArgs e)
    {
        ReorderListItem rolItem = e.Item as ReorderListItem;
        LinkButton btn = rolItem.FindControl("btnUpdate") as LinkButton;
        measurementunits unit = e.Item.DataItem as measurementunits;
        if (unit != null)
        {
            btn.PostBackUrl = string.Format("~/Admin/measurementunits.aspx?unitId={0}", unit.UnitId);
        }

        btn = rolItem.FindControl("btnDelete") as LinkButton;
        //btn.Visible = unit.AllowDelete;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["unitId"]))
        {
            int unitId = int.Parse(btn.Attributes["unitId"]);
            if (BusinessFacade.Instance.DeleteMeasurementUnit(unitId))
            {
                Rebind();
            }
        }
    }

    protected void rolMeasurementUnits_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    {
        DoReorder(e.OldIndex, e.NewIndex);
    }

    private void DoReorder(int oldIndex, int newIndex)
    {
        measurementunits[] arr = Data;

        if (oldIndex < newIndex) //item moved down
        {
            int tempOrder = arr[newIndex].SortOrder;

            for (int i = newIndex; i > oldIndex; i--)
            {
                arr[i].SortOrder = arr[i - 1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }
        else  //item moved up
        {
            int tempOrder = arr[newIndex].SortOrder;

            for (int i = newIndex; i < oldIndex; i++)
            {
                arr[i].SortOrder = arr[i + 1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }

        if (BusinessFacade.Instance.ReorderMeasurementUnits(arr))
        {
            Rebind();
        }
    }
}
