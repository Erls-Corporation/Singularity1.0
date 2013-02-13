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
using App.Model;
using Attribute = App.Model.Attribute;
using System.Diagnostics;

public partial class AttributesUc : BaseUc
{
    #region Properties
    public DataTable AttributesTable
    {
        [DebuggerStepThrough]
        get
        {
            return UWeb.VsTable(ViewState, "AttributesTable", null);
        }
        [DebuggerStepThrough]
        set { ViewState["AttributesTable"] = value; }
    }

    public Attributes attributes = null;
    public new Attributes Attributes
    {
        [DebuggerStepThrough]
        get
        {
            if (AttributesTable == null)
            {
                if (Uc.TableName == RsOneItemTable.Unknown)
                {
                    attributes = new Attributes(Cxt, BaseCollection.ExecuteSql(GetSql()));
                }
                else
                {
                    attributes = Attributes.GetTableAttributes(Uc.TableName);
                }

                AttributesTable = attributes.DataTable;
            }
            else
            {
                attributes = new Attributes(Cxt, AttributesTable);
            }

            return attributes;
        }
        [DebuggerStepThrough]
        set { attributes = value; }
    }

    public ItemAttributes ItemAttributes
    {
        [DebuggerStepThrough]
        get
        {
            return GetItemAttributes();
        }
    }

    #endregion

    #region InitControl
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Controls.Count > 0)
        {
            Controls[0].Focus();
        }
    }

    public void InitControl()
    {
        if (gv1.Rows.Count > 0)
        {
            return;
        }

        RefreshToolBar();

        if (Uc.LayoutID == LayoutE.SearchResult)
        {
            ph1.Visible = true;
        }

        gv1.DataSource = Attributes.DataTable;
        gv1.DataBind();
    }

    private string GetSql()
    {
        string sql = "";

        if (Uc.CategoryID == 0)
        {
            sql += "\nSELECT     *";
            sql += "\nFROM         Attribute";
            sql += "\nWHERE     (ParentAttributeID IS NULL) AND (CategoryID IN";
            sql += "\n                          (SELECT     TOP (1) CategoryID";
            sql += "\n                            FROM          ServiceCategory";
            sql += "\n                            WHERE      (ServiceID = " + Uc.ServiceID + " OR";
            sql += "\n                                                   ServiceID IN";
            sql += "\n                                                       (SELECT     ParentServiceID";
            sql += "\n                                                         FROM          Service";
            sql += "\n                                                         WHERE      (StatusID = 1) AND (ServiceID = " + Uc.ServiceID + "))) AND (IsDefault = 1)))";
            sql += "\n			AND Attribute.AttributeID NOT IN (select AttributeID from AttributeLayout where LayoutID = " + Uc.LayoutID.ToString("d") + " AND IsVisible = 0)";
            sql += "\nORDER BY Sequence";
        }
        else
        {
            sql += "\nSELECT     *";
            sql += "\nFROM         Attribute";
            sql += "\nWHERE     (ParentAttributeID IS NULL) AND (CategoryID  = " + Uc.CategoryID + ")";
            sql += "\n			AND Attribute.AttributeID NOT IN (select AttributeID from AttributeLayout where LayoutID = " + Uc.LayoutID.ToString("d") + " AND IsVisible = 0)";
            sql += "\nORDER BY Sequence";
        }

        return sql;
    }

    private void RefreshToolBar()
    {
        if (Uc.LayoutID == LayoutE.SearchResult)
        {
            be.Visible = Cxt.User.IsAuthorize(TaskE.EditItem) || Uc.Item.CreatedBy == Cxt.User.ID;
            bd.Visible = Cxt.User.IsAuthorize(TaskE.DeleteItem) || Uc.Item.CreatedBy == Cxt.User.ID;

            be.NavigateUrl = UWeb.GetUrlItemEdit(Uc.ServiceID, LayoutE.Edit, Uc.CategoryID, Uc.ItemID);
            bd.CommandArgument = Uc.ItemID.ToString();

            bd.Attributes.Add("onclick", "return dc();");
        }
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AttributeUc auc1 = InitAttributeUc(e.Row.Cells[0]);

            auc1.InitControl();
        }
    }

    private int rowIndex = 0;
    private AttributeUc InitAttributeUc(TableCell cell)
    {
        AttributeUc auc1 = cell.FindControl("auc1") as AttributeUc;

        Label laid = cell.FindControl("laid") as Label;

        auc1.AttributeID = BaseItem.ToInt32(laid.Text);
        auc1.Attribute = Attributes[rowIndex];
        auc1.RowIndex = rowIndex++;

        auc1.Uc.ServiceID = Uc.ServiceID;
        auc1.Uc.CategoryID = Uc.CategoryID;
        auc1.Uc.ItemID = Uc.ItemID;
        auc1.Uc.LayoutID = Uc.LayoutID;
        auc1.Uc.TableName = Uc.TableName;

        if (Uc.LayoutID == LayoutE.SearchResult || Uc.LayoutID == LayoutE.View)
        {
            ituc1.Uc.ServiceID = Uc.ServiceID;
            ituc1.Uc.CategoryID = Uc.CategoryID;
            ituc1.Uc.ItemID = Uc.ItemID;
            ituc1.Uc.LayoutID = Uc.LayoutID;
            ituc1.Uc.TableName = Uc.TableName;

            ituc1.InitControl();
        }

        if (Uc.LayoutID == LayoutE.SearchResult)
        {
            ph3.Visible = true;

            imgc.ImageUrl = Category.IconUrl(Uc.CategoryID);
            imgc.AlternateText = Uc.Category.Name;
            imgc.ToolTip = Uc.Category.Name;
            imgc.Visible = true;

            lnks.Text = Uc.Service.Name;
            lnks.NavigateUrl = UWeb.GetUrlServiceSearch(Uc.ServiceID, SearchTypeE.Browse);

            lnkc.Text = Uc.Category.Name;
            lnkc.NavigateUrl = UWeb.GetUrlItemSearch(Uc.ServiceID, Uc.CategoryID, SearchTypeE.Browse);
        }

        return auc1;
    }

    public void GetFilter(SearchQuery q)
    {
        foreach (GridViewRow row in gv1.Rows)
        {
            AttributeUc auc1 = InitAttributeUc(row.Cells[0]);

            ItemAttribute ia = new ItemAttribute(Cxt, Uc.ItemID, auc1.AttributeID);

            auc1.GetFilter(q, ia);
        }
    }

    ItemAttributes list = null;
    private ItemAttributes GetItemAttributes()
    {
        if (list != null)
        {
            return list;
        }

        list = new ItemAttributes();
        list.Cxt = Cxt;
        list.ItemID = Uc.ItemID;

        foreach (GridViewRow row in gv1.Rows)
        {
            AttributeUc auc1 = row.Cells[0].FindControl("auc1") as AttributeUc;
            Label laid = row.Cells[0].FindControl("laid") as Label;

            auc1.GetValue(list);
        }

        return list;
    }
    #endregion

    #region ToolBar Buttons

    protected void bd_Command(object sender, CommandEventArgs e)
    {
        _Item.SetStatus(Cxt, BaseItem.ToInt32(e.CommandArgument), StatusE.Deleted);

        base.ReloadPage();
    }

    #endregion
}