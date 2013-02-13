// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Model;

public partial class LogUc : BaseUc
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void InitControl()
    {
        InitControl(Log.SelectAll());
    }

    private void InitControl(DataTable table)
    {
        GridView1.DataSource = table;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BindItem(e);
        }
    }

    private void BindItem(GridViewRowEventArgs e)
    {
       
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Log.Clear();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        InitControl();
    }
}
