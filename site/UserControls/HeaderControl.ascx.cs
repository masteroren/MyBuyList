using MyBuyListShare.Classes;
using System;

public partial class UC_HeaderControl : System.Web.UI.UserControl
{
    public const int USER_ADMIN = 1;

    public string SearchFor { get; set; }
    public string SearchIn { get; set; }
     
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo userInfo = ((BasePage)Page).CurrUser;
        if (userInfo != null && userInfo.UserId != 0)
        {
            lblHeaderUserName.Text = "שלום " + userInfo.DisplayName;
            loginButton.Text = "יציאה";
        } else
        {
            PlaceHolderJoin.Visible = ((BasePage)Page).CurrUser == null;
        }
    }
}
