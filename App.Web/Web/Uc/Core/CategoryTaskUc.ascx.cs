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

public partial class CategoryTaskUc : BaseUc
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #region InitControl

    public void InitControl()
    {
        BaseCollection c = new BaseCollection(UData.ToTable2("CategoryTask", "TaskID"));
        BaseItem i = null;

        i = c.NewItem();
        c.Add(i);
        i.SetColumn(0, TaskE.SearchItem.ToString("d"));

        if (UWeb.IsAuthenticated)
        {
            bool show = true;

            if (Cxt.Service.ServiceTypeID == ServiceTypeE.Membership)
            {
                if (UWeb.UserID != 1)
                {
                    show = false;
                }
            }

            if (show)
            {
                i = c.NewItem();
                c.Add(i);
                i.SetColumn(0, TaskE.NewItem.ToString("d"));

                i = c.NewItem();
                c.Add(i);
                i.SetColumn(0, TaskE.MyItem.ToString("d"));
            }
        }

        gv1.DataSource = c.DataTable;
        gv1.DataBind();
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BindItem(e);
        }
    }

    private void BindItem(GridViewRowEventArgs e)
    {
        int cid = Uc.CategoryID;
        int sid = Uc.ServiceID;
        TaskE task = (TaskE)UWeb.ToInt32(e, "TaskID");
        ServiceCategory sc = new ServiceCategory(Cxt, sid, cid);

        HyperLink lnkt = e.Row.FindControl("lnkt") as HyperLink;

        switch (task)
        {
            case TaskE.SearchItem:
                lnkt.Text = sc.Get(AttributeE.CategorySearchLabel, Category.DefaultSearch);
                lnkt.NavigateUrl = UWeb.GetUrlItemSearch(Uc.ServiceID, cid, SearchTypeE.Quick);
                break;
            case TaskE.NewItem:
                lnkt.Text = sc.Get(AttributeE.CategoryNewLabel, Category.DefaultNew);
                lnkt.NavigateUrl = UWeb.GetUrlItemEdit(Uc.ServiceID, LayoutE.Edit, cid, Uc.ItemID);
                break;
            case TaskE.BrowseItem:
                lnkt.Text = sc.Get(AttributeE.CategoryBrowseLabel, Category.DefaultBrowse);
                lnkt.NavigateUrl = UWeb.GetUrlItemSearch(Uc.ServiceID, cid, SearchTypeE.Browse);
                break;
            case TaskE.MyItem:
                lnkt.Text = sc.Get(AttributeE.CategoryMyItemsLabel, Category.DefaultMyItems);
                lnkt.NavigateUrl = UWeb.GetUrlItemSearch(Uc.ServiceID, cid, SearchTypeE.MyItems);
                break;
        }
    }
    
    #endregion
}
