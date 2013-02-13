// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model; using Attribute = App.Model.Attribute;

public partial class ItemEdit : BasePage 
{
    #region Properties
    public RsOneItemTable TableName
    {
        get
        {
            return (RsOneItemTable)UWeb.QsInt32("tn");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Cxt.LayoutID == LayoutE.Edit)
        {
            CheckIfAuthorized();
        }

        if (Cxt.Service.ServiceTypeID == ServiceTypeE.Membership)
        {
            if (Cxt.LayoutID == LayoutE.Edit && UWeb.UserID != 1)
            {
                GoBack();
            }
        }

        if (!Page.IsPostBack && Cxt.LayoutID == LayoutE.View)
        {
            Cxt.Item.IncViewCount();
        }

        InitUi();
    }

    private void InitUi()
    {
        InitState();

        if (TableName == RsOneItemTable.Unknown)
        {
            string val = "";

            if (Cxt.IsNew)
            {
                val = Cxt.Category.Get(AttributeE.CategoryNewLabel, Category.DefaultNew);
            }
            else
            {
                if (Cxt.LayoutID == LayoutE.Edit)
                {
                    val = Cxt.Category.Get(AttributeE.CategoryNewLabel, Category.DefaultEdit);
                }
                else
                {
                    val = Cxt.Category.Get(AttributeE.CategoryBrowseLabel, Category.DefaultBrowse);
                }
            }

            ln.Text = Cxt.Service.Name + " > <span style='color:#999999;'>" + val + "</span>";
            asuc1.InitControl();
            teuc1.Visible = false;
        }
        else
        {
            ln.Text = TableName.ToString();
            asuc1.InitControl();
            teuc1.InitControl();
        }

        guc11.InitControl();
        guc12.InitControl();

        CaptchaControl1.Visible = UWeb.UserID != 1;

        if (UrlReferrer != "")
        {
            btnCancel1.OnClientClick = btnCancel.OnClientClick = "javascript:window.location=" + UrlReferrer;
        }

        if (Cxt.LayoutID != LayoutE.Edit)
        {
            btnCancel1.Text = btnCancel.Text = "<  Go Back";
            lblInfo.Text = "<br/>";

            ph1.Visible = false;
            ph2.Visible = false;
            ph3.Visible = false;
        }
    }

    private void InitState()
    {
        asuc1.Uc.ServiceID = Cxt.ServiceID;
        asuc1.Uc.ItemID = Cxt.ItemID;
        asuc1.Uc.CategoryID = Cxt.CategoryID;
        asuc1.Uc.LayoutID = Cxt.LayoutID;
        asuc1.Uc.TableName = TableName;

        teuc1.Uc.ServiceID = Cxt.ServiceID;
        teuc1.Uc.ItemID = Cxt.ItemID;
        teuc1.Uc.CategoryID = Cxt.CategoryID;
        teuc1.Uc.LayoutID = Cxt.LayoutID;
        teuc1.Uc.TableName = TableName;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        bool isNew = asuc1.ItemAttributes.Item.IsNew;

        if (TableName == RsOneItemTable.Unknown)
        {
            asuc1.ItemAttributes.Save();
        }
        else
        {
            TableItem item = new TableItem(Cxt);

            item.TableName = TableName;
            item.TableItemAttributes = asuc1.ItemAttributes;
            item.TableConfigItemAttributes = teuc1.TableConfigItemAttributes;
            item.SelectedCategories = teuc1.SelectedCategories;

            item.Save();

            teuc1.SetItem(item.Item);
        }

        if (Page.IsValid)
        {
            if (isNew)
            {
                try
                {
                    ServiceCategory.UpdateStats(Cxt, Cxt.ServiceID, Cxt.CategoryID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            ph4.Visible = true;
            ph5.Visible = false;
            lcd.Text = "Information saved successfully.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GoBack();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        GoBack();
    }
}
