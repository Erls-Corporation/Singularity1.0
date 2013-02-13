// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Model;

public partial class CategoryEditUc : BaseUc
{
    #region Properties
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl()
    {
        InitState();

        if (Uc.LayoutID == LayoutE.Edit)
        {
            RsFileUpload1.ShowEditButton = true;
            RsFileUpload1.ShowDeleteButton = true;
        }

        RsFileUpload1.NoImageUrl = Category.DefaultIconUrl;
        RsFileUpload1.SetValue(Category.IconUrl(Uc.CategoryID, false));

        if (!Page.IsPostBack)
        {
            if (Uc.CategoryID != 0)
            {
                lnkAdd.Visible = true;
                lnkAdd.NavigateUrl = UWeb.GetUrlItemEdit(0, LayoutE.Edit, 0, 0, RsOneItemTable.Attribute);

                RsGridView1.DataSource = new Attributes(Cxt, Uc.CategoryID).DataTable;
                RsGridView1.DataBind();
            }
        }
    }

    private void InitState()
    {
    }

    protected void RsGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        RsLabel lblAttributeID = e.Row.FindControl("lblAttributeID") as RsLabel;
        RsHyperLink lnkName = e.Row.FindControl("lnkName") as RsHyperLink;

        lblAttributeID.Text = UWeb.ToString(e, "AttributeID");
        lnkName.Text = UWeb.ToString(e, "Name");
        lnkName.NavigateUrl = UWeb.GetUrlItemEdit(0, LayoutE.Edit, 0, lblAttributeID.Text, RsOneItemTable.Attribute);
    }
}
