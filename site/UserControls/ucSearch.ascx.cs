using System;
using System.Collections.Generic;

public partial class UserControls_ucSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) return;


    }

    public void SetSearchOptions(Dictionary<int, string> options)
    {
    }
}