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

public partial class ServiceEditUc : BaseUc
{
    #region Properties
    ServiceCategories serviceCategories = null;
    public ServiceCategories ServiceCategories
     {
         get
         {
             if (serviceCategories == null)
             {
                 int serviceID = Uc.ServiceID;

                 if (serviceID == 0)
                 {
                     serviceID = -1;
                 }

                 serviceCategories = new ServiceCategories(Cxt, serviceID, 0, StatusE.Active);
             }

             return serviceCategories;
         }
     }

    ServiceCategories selectedCategories = null;
    public ServiceCategories SelectedCategories
    {
        get
        {
            int sequence = 10;
            int serviceID = Uc.ServiceID;

            RsListItems selectedItems = RsGridView1.SelectedItems;

            if (serviceID == 0) // if new service
            {
                serviceID = -1;
            }

            selectedCategories = new ServiceCategories(Cxt, serviceID, 0);

            #region Delete
            for (int i = 0; i < selectedCategories.Count; i++)
            {
                ServiceCategory scx = selectedCategories[i];

                if (selectedItems.Contains("CategoryID=" + scx.CategoryID))
                {
                    scx.StatusID = StatusE.Active;
                    scx.IsDefault = (sequence == 10);
                    sequence += 10;
                }
                else
                {
                    selectedCategories[i].StatusID = StatusE.Deleted;
                }
            } 
            #endregion

            #region Add
            for (int i = 0; i < selectedItems.Count; i++)
            {
                RsListItem selectedItem = selectedItems[i];

                ServiceCategory scx = selectedCategories.Filter("CategoryID=" + selectedItem.ValueOfFirstDataKey).First;

                if (scx.ID == 0)
                {
                    ServiceCategory sc = selectedCategories.NewItem();

                    sc.ServiceID = Uc.ServiceID;
                    sc.CategoryID = BaseItem.ToInt32(selectedItem.ValueOfFirstDataKey);
                    sc.Sequence = sequence;
                    sc.IsDefault = (sequence == 10);
                    sc.StatusID = StatusE.Active;

                    selectedCategories.Add(sc);
                }

                sequence += 10;
            } 
            #endregion
            
            return selectedCategories;
        }
    }
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

        RsFileUpload1.NoImageUrl = Service.DefaultIconUrl;
        RsFileUpload1.SetValue(Service.IconUrl(Uc.ServiceID, false));

        if (!Page.IsPostBack)
        {
            if (Uc.ServiceID != 0)
            {
                lnkAdd.Visible = true;
                lnkAdd.NavigateUrl = UWeb.GetUrlItemEdit(0, LayoutE.Edit, 0, 0, RsOneItemTable.Category);

                RsGridView1.ShowCheckBox = true;
                RsGridView1.DataSource = new Categories(Cxt, 0).DataTable;
                RsGridView1.DataSourceSelected = ServiceCategories.DataTable;
                RsGridView1.DataKeyNames = UStr.Split("CategoryID", "");
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

        RsLabel lblCategoryID = e.Row.FindControl("lblCategoryID") as RsLabel;
        RsHyperLink lnkName = e.Row.FindControl("lnkName") as RsHyperLink;
        RsTextBox txtSequence = e.Row.FindControl("txtSequence") as RsTextBox;
        RsRadioButton rdoDefault = e.Row.FindControl("rdoDefault") as RsRadioButton;

        lblCategoryID.Text = UWeb.ToString(e, "CategoryID");
        lnkName.Text = UWeb.ToString(e, "Name");
        lnkName.NavigateUrl = UWeb.GetUrlItemEdit(0, LayoutE.Edit, 0, lblCategoryID.Text, RsOneItemTable.Category);

        ServiceCategory scx = ServiceCategories.Filter("CategoryID=" + lblCategoryID.Text).First;

        if (scx.CategoryID.ToString() == lblCategoryID.Text)
        {
            txtSequence.Text = scx.Sequence.ToString();
            rdoDefault.Checked = scx.IsDefault;
        }
    }
}
