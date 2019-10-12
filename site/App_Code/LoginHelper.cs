using MyBuyListShare.Classes;
using MyBuyListShare.Services;

/// <summary>
/// Summary description for LoginHelper
/// </summary>
public class LoginHelper
{
    public LoginHelper()
    {
    }

    public static UserInfo Login(string userName, string password)
    {
        string clearText = string.Format("{0}-{1}", userName, password);
        string a = Encryption.Encrypt(clearText);

        UserInfo userInfo = HttpHelper.Get<UserInfo>(string.Format("users/{0}", a));
        return userInfo;
    }

    public static void SendLoginNotification(UserInfo userInfo)
    {
        AuthService.Instance.Notify(userInfo);
    }
}