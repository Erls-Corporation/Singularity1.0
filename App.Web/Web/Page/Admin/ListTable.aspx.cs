// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model; using Attribute = App.Model.Attribute;

public partial class ListTable : BasePage 
{
    #region Properties
    public RsOneItemTable TableName
    {
        get
        {
            return (RsOneItemTable)UWeb.QsInt32("tn");
        }
    }
    
    public string PrimaryKey
    {
        get
        {
            return TableName.ToString() + "ID";
        }
    }

    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (UWeb.UserID != 1)
        {
            Redirect("~/Default.aspx");
        }

        CheckIfAuthorized();

        if (!Page.IsPostBack)
        {
            lnkAdd.NavigateUrl = UWeb.GetUrlItemEdit(0, LayoutE.Edit, 0, 0, TableName);

            lblName.Text = TableName.ToString();

            InitControl(TableName);
        }
    }

    protected void lbGoBack_Click(object sender, EventArgs e)
    {
        Redirect("~/Web/Page/Admin/Admin.aspx");
    }

    protected void lbClone_Click(object sender, EventArgs e)
    {
        RsListItems selectedItems = RsGridView1.SelectedItems;

        for (int i = 0; i < selectedItems.Count; i++)
        {
            RsListItem selectedItem = selectedItems[i];

            if (TableName == RsOneItemTable.Service)
            {
                Service.Clone(Cxt, BaseItem.ToInt32(selectedItem.ValueOfFirstDataKey));
            }
        }
    }

    public void InitControl(RsOneItemTable tableName)
    {
        string sql = "";

        sql += "select * from " + tableName.ToString();
        
        if (SqlHelper.IsSqlTableContainsColumn(Config.ConnectionString, tableName.ToString(), "Name"))
        {
            sql +=" order by [Name]";
        }

        InitControl(BaseCollection.ExecuteSql(tableName, sql));
    }

    private void InitControl(DataTable table)
    {
        RsGridView1.ShowCheckBox = false;
        RsGridView1.DataKeyNames = UStr.Split(PrimaryKey, "");
        RsGridView1.DataSource = table;
        RsGridView1.DataBind();
    }

    protected void RsGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BindItem(e);
        }
    }

    private void BindItem(GridViewRowEventArgs e)
    {
        HyperLink lnkName = e.Row.FindControl("lnkName") as HyperLink;
        Label lblID = e.Row.FindControl("lblID") as Label;

        lblID.Text = BaseItem.ToString(e, PrimaryKey);
        lnkName.Text = BaseItem.ToString(e, "Name");
        lnkName.NavigateUrl = UWeb.GetUrlItemEdit(0, LayoutE.Edit, 0, BaseItem.ToInt32(e, TableName.ToString() + "ID"), TableName);
    }
}
