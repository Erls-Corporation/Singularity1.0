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

public partial class TableEditUc : BaseUc
{
    public ServiceCategories SelectedCategories
    {
        get { return seuc1.SelectedCategories; }
    }

    public ItemAttributes TableConfigItemAttributes
    {
        get { return asuc1.ItemAttributes; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl()
    {
        int categoryID = 0;
        int attributeID = 0;
        
        #region Init CategoryID
        
        //38 Service ID
        //54 Category ID
        //56 Attribute ID
        
        switch (Uc.TableName)
        {
            case RsOneItemTable.Unknown:
                break;
            case RsOneItemTable.Attribute:
                categoryID = 19;
                attributeID = 56;
                break;
            case RsOneItemTable.AttributeLayout:
                break;
            case RsOneItemTable.AttributeType:
                break;
            case RsOneItemTable.Category:
                categoryID = 18;
                attributeID = 54;
                break;
            case RsOneItemTable.Item:
                break;
            case RsOneItemTable.ItemAttribute:
                break;
            case RsOneItemTable.Layout:
                break;
            case RsOneItemTable.Log:
                break;
            case RsOneItemTable.Role:
                break;
            case RsOneItemTable.RoleTask:
                break;
            case RsOneItemTable.Service:
                categoryID = 17;
                attributeID = 38;
                break;
            case RsOneItemTable.ServiceCategory:
                break;
            case RsOneItemTable.Status:
                break;
            case RsOneItemTable.ServiceTask:
                break;
            case RsOneItemTable.ServiceType:
                break;
            case RsOneItemTable.ServiceUser:
                break;
            case RsOneItemTable.Task:
                break;
            case RsOneItemTable.User:
                break;
            case RsOneItemTable.UserRole:
                break;
            case RsOneItemTable.AttributeTable:
                break;
            case RsOneItemTable.ZAdminAttribute:
                break;
            case RsOneItemTable.ZDummyAttribute:
                break;
            default:
                break;
        } 
        #endregion

        #region Init Config Attributes
        if (categoryID == 0)
        {
            Visible = false;

            return;
        }

        ItemAttribute item = ItemAttributes.GetAdminServiceCategory(categoryID, attributeID, Uc.ItemID.ToString());

        asuc1.Uc.ServiceID = 16;
        asuc1.Uc.ItemID = item.GetColInt32("ItemID");
        asuc1.Uc.CategoryID = categoryID;
        asuc1.Uc.LayoutID = Uc.LayoutID; 
        #endregion
       
        #region Init Table Uc

        switch (Uc.TableName)
        {
            case RsOneItemTable.Unknown:
                break;
            case RsOneItemTable.Attribute:
                break;
            case RsOneItemTable.AttributeLayout:
                break;
            case RsOneItemTable.AttributeType:
                break;
            case RsOneItemTable.Category:
                ceuc1.Uc.CategoryID = Uc.ItemID;
                ceuc1.Uc.LayoutID = Uc.LayoutID;
                ceuc1.Visible = true;
                ceuc1.InitControl();
                break;
            case RsOneItemTable.Item:
                break;
            case RsOneItemTable.ItemAttribute:
                break;
            case RsOneItemTable.Layout:
                break;
            case RsOneItemTable.Log:
                break;
            case RsOneItemTable.Role:
                break;
            case RsOneItemTable.RoleTask:
                break;
            case RsOneItemTable.Service:
                seuc1.Uc.ServiceID = Uc.ItemID;
                seuc1.Uc.LayoutID = Uc.LayoutID;
                seuc1.Visible = true;
                seuc1.InitControl();
                break;
            case RsOneItemTable.ServiceCategory:
                break;
            case RsOneItemTable.Status:
                break;
            case RsOneItemTable.ServiceTask:
                break;
            case RsOneItemTable.ServiceType:
                break;
            case RsOneItemTable.ServiceUser:
                break;
            case RsOneItemTable.Task:
                break;
            case RsOneItemTable.User:
                break;
            case RsOneItemTable.UserRole:
                break;
            case RsOneItemTable.AttributeTable:
                break;
            case RsOneItemTable.ZAdminAttribute:
                break;
            case RsOneItemTable.ZDummyAttribute:
                break;
            default:
                break;
        }
        #endregion

        asuc1.InitControl();
    }

    public void SetItem(BaseItem item)
    {
        #region Save Table
        switch (Uc.TableName)
        {
            case RsOneItemTable.Unknown:
                break;
            case RsOneItemTable.Attribute:
                break;
            case RsOneItemTable.AttributeLayout:
                break;
            case RsOneItemTable.AttributeType:
                break;
            case RsOneItemTable.Category:
                ceuc1.Uc.CategoryID = item.ID;
                break;
            case RsOneItemTable.Item:
                break;
            case RsOneItemTable.ItemAttribute:
                break;
            case RsOneItemTable.Layout:
                break;
            case RsOneItemTable.Log:
                break;
            case RsOneItemTable.Role:
                break;
            case RsOneItemTable.RoleTask:
                break;
            case RsOneItemTable.Service:
                seuc1.Uc.ServiceID = item.ID;
                break;
            case RsOneItemTable.ServiceCategory:
                break;
            case RsOneItemTable.Status:
                break;
            case RsOneItemTable.ServiceTask:
                break;
            case RsOneItemTable.ServiceType:
                break;
            case RsOneItemTable.ServiceUser:
                break;
            case RsOneItemTable.Task:
                break;
            case RsOneItemTable.User:
                break;
            case RsOneItemTable.UserRole:
                break;
            case RsOneItemTable.AttributeTable:
                break;
            case RsOneItemTable.ZAdminAttribute:
                break;
            case RsOneItemTable.ZDummyAttribute:
                break;
            default:
                break;
        } 
        #endregion
    }
}
