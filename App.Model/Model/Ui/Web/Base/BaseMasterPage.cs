using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model;

[System.Diagnostics.DebuggerStepThrough]
public class BaseMasterPage : System.Web.UI.MasterPage
{
    public Cxt Cxt = null;

    #region Constructor
    public BaseMasterPage()
    {
        Cxt = new Cxt(ViewState);

        base.Load += new EventHandler(BaseMasterPage_Load);
    }

    protected void BaseMasterPage_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && UrlReferrer == "")
        {
            if (Request.UrlReferrer == null)
            {
                UrlReferrer = Request.Url.ToString();
            }
            else
            {
                UrlReferrer = Request.UrlReferrer.ToString();
            }
        }

        if (UWeb.Qs("q") != "" && !Request.RawUrl.Contains("ItemSearch.aspx"))
        { 
            Redirect(UWeb.GetUrlItemSearch(UWeb.Qs("q")));
        }
    }
  
    #endregion

    #region Util
    #region Redirect
    public string UrlReferrer
    {
        get { return UWeb.Vs(ViewState, "UrlReferrer"); }
        set { UWeb.SetVs(ViewState, "UrlReferrer", value); }
    }

    public void CheckIfAuthenticated()
    {
        if (!UWeb.IsAuthenticated)
        {
            Response.Redirect("~/Web/Page/Account/SignIn.aspx");
        }
    }

    public void CheckIfAuthorized()
    {
        CheckIfAuthenticated();
    }

    public void Redirect(string url)
    {
        Redirect(url, null);
    }

    public void Redirect(string url, params object[] pv)
    {
        UWeb.Redirect(url, pv);
    }

    public void GoBack()
    {
        if (UrlReferrer == Request.Url.ToString())
        {
            NameValueCollection kv = UWeb.ParseQs(UrlReferrer);

            string url = UWeb.GetUrlItemSearch(kv["sid"], kv["cid"], SearchTypeE.Quick);

            Response.Redirect(url, true);
        }
        else
        {
            Response.Redirect(UrlReferrer, true);
        }
    }

    public void ReloadPage()
    {
        Response.Redirect(Request.Url.ToString(), true);
    }
    #endregion

    #region CanDo

    public void CanDo(TaskE task)
    {
        Cxt.CanDo(task);

    }

    #endregion

    public string BaseUrl
    {
        get
        {
            try
            {
                return string.Format("http://{0}{1}",
                                     Request.ServerVariables["HTTP_HOST"],
                                     (Request.ApplicationPath.Equals("/")) ? string.Empty : Request.ApplicationPath);
            }
            catch
            {
                // This is for design time
                return null;
            }
        }
    }

    #endregion 

}
