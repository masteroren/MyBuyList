using System;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using MyBuyListShare.Classes;
using MyBuyList.Shared;

public class LoginActions : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string method = context.Request["method"];
        if (!string.IsNullOrEmpty(method))
        {
            // Calling methods using reflection
            MethodInfo methodInfo = typeof(LoginActions).GetMethod(method);
            if (methodInfo != null)
            {
                object response = methodInfo.Invoke(new LoginActions(), new object[] { context });
                context.Response.Write(response);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public string IsLoggedIn(HttpContext context)
    {
        UserInfo userInfo = (UserInfo)HttpContext.Current.Session[AppConstants.CURR_USER];
        if (userInfo != null)
        {
            return HttpHelper.JsonSerializer<UserInfo>(userInfo);
        }
        return null;
    }

    public void Logout(HttpContext context)
    {
        HttpContext.Current.Session[AppConstants.CURR_USER] = null;
        LoginHelper.SendLoginNotification(null);
    }

    public string Login(HttpContext context)
    {
        UserInfo userInfo = null;

        try
        {
            string userName = context.Request["UserName"];
            string password = context.Request["Password"];
            userInfo = LoginHelper.Login(userName, password);
            HttpContext.Current.Session.Add(AppConstants.CURR_USER, userInfo);
        }
        catch (Exception ex)
        {
        }

        return HttpHelper.JsonSerializer<UserInfo>(userInfo);
    }

}