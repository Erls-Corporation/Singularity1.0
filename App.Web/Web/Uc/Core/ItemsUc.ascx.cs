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
using System.Diagnostics;
using App.Model;
using Attribute = App.Model.Attribute;

public partial class ItemsUc : BaseUc
{
    public SearchQuery SearchQuery
    {
        [DebuggerStepThrough]
        get { return (SearchQuery) UWeb.GetS("SearchQuery", null); }
        [DebuggerStepThrough]
        set { Session["SearchQuery"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl(SearchQuery q)
    {
        if (q == null)
        {
            return;
        }

        SearchQuery = q;

        gv1.DataSource = q.GetData();
        gv1.DataBind();

        string recName = Uc.CategoryID == 0 ? "article" : Uc.Category.Name;

        if (q.Count > 0)
        {
            lc.Text = q.Count.ToString() + " matching " + recName + (q.Count > 1 ? "s" : "") + " found";
            lp.Text = "Page " + (gv1.PageIndex + 1).ToString() + " of " + gv1.PageCount;
            lp.Visible = true;
        }
        else
        {
            lc.Text = "No matching " + recName + " found.";
            lc.Visible = false;
        }

        lc.Visible = true;
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AttributesUc asuc1 = e.Row.FindControl("asuc1") as AttributesUc;

            asuc1.Uc.ServiceID = UWeb.ToInt32(e, "ServiceID");
            asuc1.Uc.CategoryID = UWeb.ToInt32(e, "CategoryID");
            asuc1.Uc.ItemID = UWeb.ToInt32(e, "ItemID");
            asuc1.Uc.LayoutID = Uc.LayoutID;

            asuc1.InitControl();
        }
    }

    protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (this.SearchQuery == null)
        {
            Response.Redirect(Request.Url.ToString());
        }

        this.SearchQuery.NewPage(e.NewPageIndex, gv1.PageIndex);

        gv1.PageIndex = e.NewPageIndex;

        InitControl(SearchQuery);
    }
}
