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

public partial class CategoryListUc : BaseUc
{
    #region InitControl

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitControl()
    {
        ServiceCategories scc = new ServiceCategories(Cxt, Uc.ServiceID, 0, StatusE.Active);

        InitControl(scc.DataTable);
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
        int cid = UWeb.ToInt32(e, "CategoryID");
        int sid = UWeb.ToInt32(e, "ServiceID");
        
        Category c = new Category(Cxt, cid);
        ServiceCategory sc = new ServiceCategory(Cxt, sid, cid);

        Image imgc = e.Row.FindControl("imgc") as Image;
        imgc.ImageUrl = Category.IconUrl(cid);
        imgc.AlternateText = c.Name;

        HyperLink lnkm = e.Row.FindControl("lnkm") as HyperLink;
        lnkm.Text = c.Name + " " + UStr.PBracket(sc.ConfigAttributes.ToInt32(AttributeE.ServiceCategoryItemCount));
        lnkm.NavigateUrl = UWeb.GetUrlItemSearch(Uc.ServiceID, cid, SearchTypeE.Browse);

        CategoryTaskUc ctuc1 = e.Row.FindControl("ctuc1") as CategoryTaskUc;
        ctuc1.Uc.CategoryID = cid;
        ctuc1.Uc.ServiceID = sid;

        ctuc1.InitControl();
    }
    
    #endregion

    protected void lnkShowAll_Click(object sender, EventArgs e)
    {
        
    } 

    #region OnCategoryCommand
    protected void lnkCategory_Command(object sender, CommandEventArgs e)
    {
        OnCategoryCommand(e);
    }

    protected void imgCategory_Command(object sender, CommandEventArgs e)
    {
        OnCategoryCommand(e);
    }

    private void OnCategoryCommand(CommandEventArgs e)
    {
        Redirect("ItemEdit.aspx", "cid", e.CommandArgument);
    } 
    #endregion

}
