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

public partial class TableComboUc : BaseUc
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl()
    {
        AddTable(RsOneItemTable.Service);
        AddTable(RsOneItemTable.Category);
        AddTable(RsOneItemTable.Attribute);

        //AddTable(RsOneItemTable.Attribute);
        //AddTable(RsOneItemTable.AttributeLayout);
        //AddTable(RsOneItemTable.AttributeTable);
        //AddTable(RsOneItemTable.AttributeType);
        //AddTable(RsOneItemTable.Category);
        //AddTable(RsOneItemTable.Item);
        //AddTable(RsOneItemTable.ItemAttribute);
        //AddTable(RsOneItemTable.Layout);
        //AddTable(RsOneItemTable.Log);
        //AddTable(RsOneItemTable.Role);
        //AddTable(RsOneItemTable.RoleTask);
        //AddTable(RsOneItemTable.Service);
        //AddTable(RsOneItemTable.ServiceCategory);
        //AddTable(RsOneItemTable.ServiceTask);
        //AddTable(RsOneItemTable.ServiceType);
        //AddTable(RsOneItemTable.ServiceUser);
        //AddTable(RsOneItemTable.Status);
        //AddTable(RsOneItemTable.Task);
        //AddTable(RsOneItemTable.User);
        //AddTable(RsOneItemTable.UserRole);
        //AddTable(RsOneItemTable.ZAdminAttribute);
        //AddTable(RsOneItemTable.ZDummyAttribute);
    }

    public void AddTable(RsOneItemTable tableName)
    {
        DropDownList1.Items.Add(new ListItem(tableName.ToString(), tableName.ToString("d")));
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        SelectTable();
    }

    public void SelectTable()
    {
        base.Redirect("~/Web/Page/Admin/ListTable.aspx?", "tn", DropDownList1.SelectedValue);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string url = UWeb.GetUrlItemEdit(0, LayoutE.Edit, 0, 0, (RsOneItemTable) BaseItem.ToInt32(DropDownList1.SelectedValue));

        base.Redirect(url);
    }
}
