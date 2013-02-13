using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model;

[System.Diagnostics.DebuggerStepThrough]
public class BaseUc : System.Web.UI.UserControl
{
    private CxtParam param = null;

    #region Constructor
    public BaseUc()
    {
        param = new CxtParam(null, true);
        param.ViewState = ViewState;
    }
    #endregion

    #region Cxt

    public BasePage BasePage
    {
        get { return (BasePage)Page; }
    }

    public BaseMasterPage BaseMasterPage
    {
        get { return (BaseMasterPage)BasePage.BaseMasterPage; }
    }

    public Cxt Cxt
    {
        get { return BasePage.Cxt; }
    }

    public CxtParam Uc
    {
        get { param.Cxt = Cxt; return param; }
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
        BaseMasterPage.Redirect(url, null);
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
