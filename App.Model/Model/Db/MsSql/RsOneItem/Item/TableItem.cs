// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;

namespace App.Model
{
    #region enums

    #endregion
    
    public class TableItem
    {
        #region Data Members
        public Cxt cxt = null;
        public RsOneItemTable TableName = RsOneItemTable.Unknown;
        public ItemAttributes TableItemAttributes = null;
        public ItemAttributes TableConfigItemAttributes = null;
        public ServiceCategories SelectedCategories = null;
        public BaseItem Item = null;
        #endregion

        #region Ctor
        public TableItem()
        {
        }

        public TableItem(Cxt cxt)
        {
            this.cxt = cxt;
        }

        #endregion

        #region Properties
        #endregion

        #region Save
        public void Save()
        {
            SqlTransaction t = null;

            try
            {
                t = SqlHelper.BeginTransaction(Config.ConnectionString);

                Item = ItemAttributes.GetTableFromItemAttributes(TableName, TableItemAttributes);

                Item.IsNew = cxt.IsNew;

                Item.Save(t);

                #region Set ID
                switch (TableName)
                {
                    case RsOneItemTable.Unknown:
                        break;
                    case RsOneItemTable.Attribute:
                        TableConfigItemAttributes.Set(AttributeE.AttributeID, Item.ID);
                        break;
                    case RsOneItemTable.AttributeLayout:
                        break;
                    case RsOneItemTable.AttributeType:
                        break;
                    case RsOneItemTable.Category:
                        TableConfigItemAttributes.Set(AttributeE.CategoryID, Item.ID);
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
                        TableConfigItemAttributes.Set(AttributeE.ServiceID, Item.ID);
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

                TableConfigItemAttributes.Save(t);

                SqlHelper.CommitTransaction(t);

                t = SqlHelper.BeginTransaction(Config.ConnectionString);

                SelectedCategories.Save(t);

                SqlHelper.CommitTransaction(t);
            }
            catch (Exception ex)
            {
                SqlHelper.RollbackTransaction(t);

                throw ex;
            }
        }
        #endregion
    }
}
