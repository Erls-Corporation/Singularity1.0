using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model;

[System.Diagnostics.DebuggerStepThrough]
public class BasePage : System.Web.UI.Page
{
    #region Constructor
    public BasePage()
    {
    }

    #endregion

    #region Cxt

    public BaseMasterPage BaseMasterPage
    {
        get { return (BaseMasterPage) Master; }
    } 

    public Cxt Cxt
    {
        get { return BaseMasterPage.Cxt; }
    }

    #endregion

    #region Util
    #region Redirect
    public string UrlReferrer
    {
        get { return BaseMasterPage.UrlReferrer; }
        set { BaseMasterPage.UrlReferrer = value; }
    }

    public void CheckIfAuthenticated()
    {
        BaseMasterPage.CheckIfAuthenticated();
    }

    public void CheckIfAuthorized()
    {
        BaseMasterPage.CheckIfAuthorized();
    }

    public void Redirect(string url)
    {
        BaseMasterPage.Redirect(url);
    }

    public void Redirect(string url, params object[] pv)
    {
        BaseMasterPage.Redirect(url, pv);
    }

    public void GoBack()
    {
        BaseMasterPage.GoBack();
    }
    
    public void ReloadPage()
    {
        BaseMasterPage.ReloadPage();
    }
    #endregion

    #region CanDo

    public void CanDo(TaskE task)
    {
        Cxt.CanDo(task);

    }

    #endregion

    #endregion
}
