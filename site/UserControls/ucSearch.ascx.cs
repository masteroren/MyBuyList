using System;

public partial class UserControls_ucSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ImageButtonSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string selectedSearch = SearchType.SelectedValue;
    }
}