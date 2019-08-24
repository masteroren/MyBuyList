using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System;

public partial class PasswordRecovery : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
            {
                this.lnkLogin.NavigateUrl += string.Format("?ReturnUrl={0}", Request["ReturnUrl"]);
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.txtUserName.Text))
        {
            User u = BusinessFacade.Instance.GetUserByUserName(this.txtUserName.Text);
            lblResult.Visible = true;

            if (u != null)
            {
                if (!(u.Email == txtMail.Text.Trim()))
                {
                    lblResult.Text = "האימייל אינו תואם לאימייל ההרשמה";
                }
                else
                {

                    //
                    //MailMessage msgMail = new MailMessage();

                    //msgMail.To = this.txtMail.Text;
                    //msgMail.From = "MyBuyList@MyBuyList.com";
                    //msgMail.Subject = "שיחזור סיסמא";

                    //msgMail.BodyFormat = MailFormat.Html;
                    string strBody = "<html>" +
                                            "<body dir='rtl'>" +
                                                  "<table  > " +
                                                        "<tr> " +
                                                                "<td colspan='2'> " +
                                                                     "<b>שלום " + u.FirstName + " " + u.LastName + "</b>" +
                                                                "</td> " +
                                                        "</tr> " +
                                                        "<tr> " +
                                                                "<td colspan='3' dir='rtl'> " +
                                                                    "שם משתמש: " +
                                                                "</td> " +
                                                                 "<td colspan='3' dir='rtl'> " +
                                                                    u.Name +
                                                                "</td> " +
                                                        "</tr> " +
                                                        "<tr> " +
                                                                "<td colspan='3' dir='rtl'> " +
                                                                    "סיסמא: " +
                                                                "</td> " +
                                                                "<td> " +
                                                                    u.Password +
                                                                "</td> " +
                                                        "</tr> " +
                                                        "<tr> " +
                                                                "<td colspan='2'> " +
                                                                     "<a  href='www.MyBuyList.com'>התחברות למערכת</a>" +
                                                                "</td> " +
                                                        "</tr> " +
                                                 "<table> " +
                                            "</body>" +
                                      "</html>";

                    //msgMail.Body = strBody;

                    //SmtpMail.Send(msgMail);
                    //

                    ProperServices.Common.Mail.Mailer.SendMail(this.txtMail.Text, ProperControls.General.Utils.FromEmail, "שיחזור סיסמא", strBody, true);

                    lblResult.Text = "הסיסמא נשלחה אל האימייל שהוכנס";
                }
            }
            else
            {
                lblResult.Text = "שם משתמש לא קיים";
            }
        }
    }
}
