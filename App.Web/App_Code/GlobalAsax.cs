// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Model;

/// <summary>
/// Summary description for GlobalAsax
/// </summary>
public class GlobalAsax
{
    public GlobalAsax()
    {

    }

    public static void Application_Start(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Application["OnlineUserCount"] = 0;
        }
        catch (Exception ex)
        {
            Log.Write(Cxt.Instance, ex);
        }
    }

    public static void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    public static void Application_Error(object sender, EventArgs e)
    {
        if (Config.IsDev)
        {
            return;
        }

        Exception ex = HttpContext.Current.Server.GetLastError();

        if (ex != null)
        { 
            ex = HttpContext.Current.Server.GetLastError().GetBaseException();
        }

        Log.Write(null, ex, "Application_Error");

        HttpContext.Current.Response.Redirect("~/Web/Page/Common/ErrorPage.aspx");
    }

    public static void Session_Start(object sender, EventArgs e)
    {
        AddToOnlineUserCount(1);
    }

    public static void Session_End(object sender, EventArgs e)
    {
        AddToOnlineUserCount(-1);
    }

    public static void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs args)
    {
        if (FormsAuthentication.CookiesSupported)
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                try
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);

                    args.User = App.Model.User.GetPrincipal(ticket);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        else
        {
            throw new HttpException("Cookieless Forms Authentication is not " +
                                    "supported for this application.");
        }
    }

    public static void Application_BeginRequest(object sender, EventArgs e)
    {

    }

    public static void AddToOnlineUserCount(int count)
    {
        try
        {
            HttpContext cxt = HttpContext.Current;

            cxt.Application.Lock();
            cxt.Application["OnlineUserCount"] = BaseItem.ToInt32(cxt.Application["OnlineUserCount"]) + count;
            cxt.Application.UnLock();
        }
        catch (Exception ex)
        {
            Log.Write(Cxt.Instance, ex);
        }
    }
}
