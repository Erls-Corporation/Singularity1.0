// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;

using App.Model;
using Attribute = App.Model.Attribute;
using System.Diagnostics;

public partial class AttributeUc : BaseUc
{
    #region Data Member
    public IRsControl RsCtrl = null;
    public Control Ctrl = null;
    public int RowIndex = 0;
    #endregion

    #region Properties

    public int AttributeID
    {
        [DebuggerStepThrough]
        get { return UWeb.VsInt32(ViewState, "AttributeID", 0); }
        [DebuggerStepThrough]
        set { ViewState["AttributeID"] = value; }
    }

    public DataTable AttributeTable
    {
        [DebuggerStepThrough]
        get
        {
            return UWeb.VsTable(ViewState, "AttributeTable", null);
        }
        [DebuggerStepThrough]
        set { ViewState["AttributeTable"] = value; }
    }

    private Attribute attribute = null;
    public Attribute Attribute
    {
        [DebuggerStepThrough]
        get
        {
            if (AttributeTable == null)
            {
                attribute = new Attribute(Cxt, AttributeID);

                AttributeTable = attribute.DataRow.Table;
            }
            else
            {
                attribute = new Attributes(Cxt, AttributeTable).First; 
            }

            return attribute;
        }
        set
        {
            attribute = value; 
            
            if (attribute != null)
            {
                AttributeTable = UData.ToTable(attribute.DataRow);
            }
        }
    }

    public ItemAttribute itemAttribute = null;
    public ItemAttribute ItemAttribute
    {
        [DebuggerStepThrough]
        get
        {
            if (itemAttribute == null)
            {
                if (Uc.TableName == RsOneItemTable.Unknown)
                {
                    itemAttribute = new ItemAttribute(Cxt, Uc.ItemID, AttributeID);
                    itemAttribute.ServiceID = Uc.ServiceID;
                    itemAttribute.CategoryID = Uc.CategoryID;
                }
                else
                {
                    itemAttribute = ItemAttributes.GetTableItemAttributes(Uc.TableName, Uc.ItemID, Attribute.Name);
                }
            }

            itemAttribute.Attribute = Attribute;

            return itemAttribute;
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
        if (Page.IsPostBack)
        {
            CreateControl();
        }
    }

    public void InitControl()
    {
        SetUi();

        SetValue();
    }

    private void SetUi()
    {
        if (!Attribute.HasParent)
        {
            ln.Text = Attribute.Name + ": ";
            lhlp.Text = Attribute.Help;
        }

        switch (Uc.LayoutID)
        {
            case LayoutE.View:
            case LayoutE.SearchResult:
                ph3.Visible = ph1.Visible = false;
                break;
        }
    }

    #endregion

    #region SetValue

    public void SetValue()
    {
        CreateControl();

        if (RsCtrl != null)
        {
            if (Uc.LayoutID == LayoutE.Edit || Uc.LayoutID == LayoutE.Search)
            {
                RsCtrl.SetValue(Uc.LayoutID, ItemAttribute);
            }

            Visible = Ctrl.Visible;
        }
    }

    public void CreateControl()
    {
        if (Attribute.Name == "")
        {
            return;
        }

        Object o = GetControl();
        RsCtrl = (IRsControl)o;
        Ctrl = (Control)o;

        if (Uc.LayoutID == LayoutE.View || Uc.LayoutID == LayoutE.SearchResult)
        {
            string val = "";
            
            if (Uc.LayoutID == LayoutE.SearchResult)
            {
                string url = UWeb.GetUrlItemEdit(Uc.ServiceID, LayoutE.View, Uc.CategoryID, Uc.ItemID);

                url = url.Replace("~/", base.BaseMasterPage.BaseUrl + "/");

                val = UStr.More(UStr.StripHtml(RsCtrl.GetText(ItemAttribute)), 300, url);
            }
            else
            {
                val = RsCtrl.GetText(ItemAttribute);
            }

            if (RowIndex == 0 && Uc.LayoutID == LayoutE.SearchResult)
            {
                RsHyperLink cx = new RsHyperLink();
                cx.CssClass = "lnksr";
                cx.Text = val;
                cx.NavigateUrl = UWeb.GetUrlItemEdit(Uc.ServiceID, LayoutE.View, Uc.CategoryID, Uc.ItemID);
                o = cx;
            }
            else 
            {
                RsLabel cx = new RsLabel();
                cx.Text = val;
                o = cx;
            }
        }

        RsCtrl = (IRsControl)o;
        Ctrl = (Control)o;
        Ctrl.ID = "c" + Attribute.ID;
        
        ph2.Controls.Add(Ctrl);

        if (Attribute.AttributeLayout.ID == 1)
        {
            // you have to load attribute layout and apply to it
            // now you will have attribute config instead of layout
        }

        CreateValidationControl();
    }

    private void CreateValidationControl()
    {
        if (Uc.LayoutID != LayoutE.Edit)
        {
            return;
        }

        int max = 0;
        if (Attribute.IsTextBox)
        {
            max = Attribute.ConfigAttributes.ToInt32(AttributeE.AttributeMaxLength);
            if (max > 0)
            {
                ph4.Visible = true;
                Fluent.MultiLineTextBoxValidator val = new Fluent.MultiLineTextBoxValidator();
                val.ID = "val" + Ctrl.ID;
                val.ControlToValidate = Ctrl.ID;
                val.MaxLength = max;
                val.ErrorMessage = Attribute.ErrorMessage;
                val.EnableClientSideRestriction = true;
                val.ShowCharacterCount = true;
                val.OutputControl = lo.ID;
                val.Display = ValidatorDisplay.Dynamic;

                lo.Columns = val.MaxLength.ToString().Length;

                ph4.Controls.Add(val);
            }
        }

        if (Attribute.HasRegEx)
        {
            ph5.Visible = true;
            val1.Visible = true;
            val1.Enabled = true;
            val1.ValidationExpression = Attribute.Get(AttributeE.AttributeRegEx);
            val1.ControlToValidate = Ctrl.ID;

            if (max <= 0)
            {
                val1.ErrorMessage = Attribute.ErrorMessage;
            }
        }

        if (Attribute.IsRequired)
        {
            ph6.Visible = true;
            val2.Visible = true;
            val2.Enabled = true;
            val2.ControlToValidate = Ctrl.ID;
            val2.ErrorMessage = Attribute.ErrorMessageRequired;
        }

    }

    private Object GetControl()
    {
        Type type = Type.GetType(Attribute.AttributeType.AssemblyName + "." + Attribute.AttributeType.Name + ", " + Attribute.AttributeType.AssemblyName, true, false);

        Assembly assembly = Assembly.GetAssembly(type);

        Object o = Activator.CreateInstance(type);

        return o;
    }

    #endregion

    #region GetValue
    public void GetValue(ItemAttributes list)
    {
        RsCtrl.GetValue(list, ItemAttribute);
    }

    #endregion

    #region GetFilter
    public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
    {
        RsCtrl.GetFilter(q, ia);
    }

    #endregion

    #region Command
    protected void imgValue_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("ItemEdit.aspx?&ItemID=" + e.CommandArgument);
    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ItemAttribute ia = new ItemAttribute(Cxt, Uc.Item.ID, Convert.ToInt32(e.CommandArgument));

        ia.Delete();

        InitControl();
    }
    #endregion
}
