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


public partial class ServiceListUc : BaseUc
{
    #region InitControl

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl()
    {
        if (Uc.ServiceID == 0)
        {
            InitControl(Service.SelectServicesAll().DataTable);
        }
        else
        {
            Uc.CategoryID = Uc.Service.DefaultCategory.ID;

            if (Service.HasChild(Uc.ServiceID))
            {
                InitControl(Service.SelectServices(Uc.ServiceID.ToString()).DataTable);
            }
            else
            {
                Response.Redirect(UWeb.GetUrlItemSearch(Uc.ServiceID, Uc.CategoryID, SearchTypeE.Browse));
            }
        }
    }

    private void InitControl(DataTable table)
    {
        gv1.DataSource = table;
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
        int sid = UWeb.ToInt32(e, "ServiceID");

        Service s = new Service(Cxt, sid);

        ImageButton imgService = e.Row.FindControl("imgs") as ImageButton;
        imgService.ImageUrl = Service.IconUrl(sid);
        imgService.AlternateText = s.Get(AttributeE.ServiceName);

        HyperLink lnks1 = e.Row.FindControl("lnks1") as HyperLink;
        lnks1.NavigateUrl = UWeb.GetUrlServiceSearch(sid, SearchTypeE.Browse);

        HyperLink lnks2 = e.Row.FindControl("lnks2") as HyperLink;
        lnks2.Text = s.Get(AttributeE.ServiceName);
        lnks2.NavigateUrl = lnks1.NavigateUrl;

        Label ld = e.Row.FindControl("ld") as Label;
        ld.Text = s.Get(AttributeE.ServiceTitle);
    }
    
    #endregion
}
