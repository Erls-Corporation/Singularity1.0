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

public partial class SearchDbUc : BaseUc
{
    #region Data Members
   
    public event SearchClickEventHandler SearchClick = null; 
   
    #endregion

    #region Properties
    public SearchTypeE SearchType
    {
        get { return (SearchTypeE)UWeb.QvInt32(ViewState, "st", (int) SearchTypeE.Browse); }
        set { ViewState["st"] = (int)value; }
    }

    public string SearchText
    {
        get
        {
            return UWeb.Qs("q");
        }
    }
    #endregion

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SearchText != "")
        {
            return;
        }

        InitState();

        switch (SearchType)
        {
            case SearchTypeE.MyItems:
            case SearchTypeE.Browse:
                DoSearch();
                break;
            default:
                break;
        }

        if (!Page.IsPostBack)
        { 
           lse.Text = Uc.Category.Get(AttributeE.CategorySearchExample);
        }
    }

    #region InitControl

    public void InitControl()
    {
        switch (SearchType)
        {
            case SearchTypeE.Advance:
                InitAdvanceSearchUI();
                break;

            case SearchTypeE.MyItems:
                InitMyItemsUI();
                break;

            case SearchTypeE.Browse:
                InitBrowseUI();
                break;

            default:
                InitQuickSearchUI();
                break;
        }
    }

    private void SetCaption(string text)
    {
        ln.Text = Uc.Service.Name + " > <span style='color:#999999;'>" + text + "</span>";
    }

    private string GetUrl()
    {
        SearchTypeE st = SearchTypeE.Quick;

        switch (SearchType)
        {
            case SearchTypeE.Advance:
                st = SearchTypeE.Quick;
                break;
            default:
                st = SearchTypeE.Advance;
                break;
        }

        string pst = UWeb.Qs("st"); // previous st
        string url = Request.Url.ToString();

        if (pst != "")
        {
            url = url.Replace("&st=" + pst, "");
        }

        return UWeb.GetUrl(url, "st", st.ToString("d"));
    }

    private void InitMyItemsUI()
    {
        this.SetCaption(Uc.Category.Get(AttributeE.CategoryMyItemsLabel, Category.DefaultMyItems));

        HideAll();
    }

    private void InitBrowseUI()
    {
        this.SetCaption(Uc.Category.Get(AttributeE.CategoryBrowseLabel, Category.DefaultBrowse));

        HideAll();
    }

    private void HideAll()
    {
        ph3.Visible = false;
        btnSearch2.Visible = false;

        lnkSearchType.Visible = true;
        ph2.Visible = false;
        btnSearch1.Visible = true;
    }

    private void InitQuickSearchUI()
    {
        this.SetCaption(Uc.Category.Get(AttributeE.CategorySearchLabel, Category.DefaultSearch));

        lnkSearchType.Text = "Advance Search";
        lnkSearchType.NavigateUrl = GetUrl();

        ph1.Visible = true;
        ph2.Visible = true;
        btnSearch1.Visible = true;
    }

    private void InitAdvanceSearchUI()
    {
        this.SetCaption(Uc.Category.Get(AttributeE.CategorySearchLabel, Category.DefaultSearch));

        lnkSearchType.Text = "Quick Search";
        lnkSearchType.NavigateUrl = GetUrl();

        ph1.Visible = true;
        ph3.Visible = true;
        ph4.Visible = true;

        InitState();

        asuc1.InitControl();
    }

    private void InitState()
    {
        asuc1.Uc.ServiceID = Cxt.ServiceID;
        asuc1.Uc.CategoryID = Cxt.CategoryID;
        asuc1.Uc.LayoutID = LayoutE.Search;
        asuc1.Uc.ItemID = 0;
    }

    #endregion

    #region GetFilter

    public SearchQuery GetFilter()
    {
        return GetFilter(SearchType);
    }

    public SearchQuery GetFilter(SearchTypeE type)
    {
        SearchQuery q = new SearchQuery();

        q.Cxt = Cxt;
        q.SearchType = type;

        switch (type)
        {
            case SearchTypeE.Advance:
                asuc1.GetFilter(q);
                break;
            case SearchTypeE.MyItems:
            case SearchTypeE.Browse:
                break; // no info required, we have passed Cxt above
            default:
                q.SearchText = txtSearch.Text;
                break;
        }

        return q;
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (SearchType == SearchTypeE.Browse)
        {
            SearchType = SearchTypeE.Quick;
        }

        DoSearch();
    }

    protected void DoSearch()
    {
        DoSearch(SearchType);
    }

    protected void DoSearch(SearchTypeE type)
    {
        ph2.Visible = true;

        if (SearchClick != null)
        {
            SearchClick(this, GetFilter(type));
        }
    }

}
