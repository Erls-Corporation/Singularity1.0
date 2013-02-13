// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

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
using App.Model; using Attribute = App.Model.Attribute;

public partial class SearchMainUc : BaseUc
{
    #region Properties
    public string SearchText
    {
        get
        {
            return UWeb.Qs("q");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Cxt.ServiceID == 16)
        {
            CheckIfAuthorized();
        }

        if (Cxt.ServiceID == 0 && SearchText == "")
        {
            Redirect("Default.aspx");
        }

        suc1.SearchClick += new SearchClickEventHandler(suc1_SearchClick);

        InitState();

        if (!Page.IsPostBack)
        {
            guc11.InitControl();

            if (SearchText == "")
            {
                cluc1.InitControl();
                suc1.InitControl();
                ph2.Visible = true;
            }
            else
            {
                DoSearch(SearchQuery.GetFilter(Cxt, SearchText));
                ph2.Visible = false;
                ph1.Visible = true;
            }
        }
    }

    #region InitControl

    public void InitControl()
    {

    }

    #endregion

    private void InitState()
    {
        suc1.Uc.ServiceID = Cxt.ServiceID;
        suc1.Uc.LayoutID = LayoutE.Search;
        suc1.Uc.CategoryID = Cxt.CategoryID;

        isuc1.Uc.ServiceID = Cxt.ServiceID;
        isuc1.Uc.LayoutID = LayoutE.SearchResult;
        isuc1.Uc.CategoryID = Cxt.CategoryID;

        cluc1.Uc.ServiceID = Cxt.ServiceID;
        cluc1.Uc.LayoutID = LayoutE.SearchResult;
        cluc1.Uc.CategoryID = Cxt.CategoryID;
    }

    void suc1_SearchClick(object sender, SearchQuery q)
    {
        DoSearch(q);
    }

    void DoSearch(SearchQuery q)
    {
        InitState();
        isuc1.InitControl(q);
    }

}
