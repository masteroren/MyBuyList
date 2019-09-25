using MyBuyList.Shared;
using MyBuyListShare.Classes;

/// <summary>
/// Summary description for LoginHelper
/// </summary>
public class LoginHelper
{
    public LoginHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static UserInfo Login(string userName, string password)
    {
        string clearText = string.Format("{0}-{1}", userName, password);
        string a = Encryption.Encrypt(clearText);

        UserInfo userInfo = HttpHelper.Get<UserInfo>(string.Format("users/{0}", a));
        //context.Session.Add(AppConstants.CURR_USER, userInfo);
        return userInfo;
    }
}