// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Diagnostics;

namespace App.Model
{
    #region enums
    public enum ItemAttributeTypeE
    {
        Unknown,
        ConfigService,
        ConfigCategory,
        ConfigServiceCategory,
        ConfigAttribute,
        ConfigUser,
        ConfigItem,
        RecordsItem,
        RecordsCategory // all records
    }
    
    #endregion

    public class ItemAttributes : BaseItems<ItemAttribute, ItemAttributes>
    {
        #region Data Members
        private int idx = 0;
        #endregion

        #region Constructors
        public ItemAttributes()
        {
        }

        // creates new record for a given category
        public ItemAttributes(Cxt cxt, RsOneItemTable tableName, int serviceID, int categoryID, int itemID)
        {
            Cxt = cxt;

            TableName = tableName;

            DataTable = BaseCollection.ExecuteSql(TableName, "SELECT NULL AS ItemAttributeID, " + serviceID + " AS ServiceID, CategoryID, " + itemID + " AS ItemID, AttributeID, [Value], CONVERT(ntext, NULL) AS ValueText, NULL AS CreatedBy, CONVERT(datetime, NULL) AS DateCreated, NULL AS ModifiedBy, CONVERT(datetime, NULL) AS DateModified FROM Attribute WHERE CategoryID = @p1 ORDER BY Sequence", categoryID);
        }

        public ItemAttributes(Cxt cxt, RsOneItemTable tableName, ItemAttributeTypeE type, int id)
        {
            Cxt = cxt;

            TableName = tableName;

            Load(type, id);
        }

        public ItemAttributes(Cxt cxt, RsOneItemTable tableName, BaseCollection items)
        {
            Cxt = cxt;

            TableName = tableName;

            DataTable = items.DataTable;
        }

        public ItemAttributes(Cxt cxt, RsOneItemTable tableName, DataTable table)
        {
            Cxt = cxt;

            TableName = tableName;

            DataTable = table;
        }

        public ItemAttributes(Cxt cxt, RsOneItemTable tableName, int itemID, int attributeID)
        {
            Cxt = cxt;

            TableName = tableName;

            Load(itemID, attributeID);
        }

        public ItemAttributes(Cxt cxt, string xsdPath, string xmlPath)
            : base(xsdPath, xmlPath)
        {
            Cxt = cxt;
        }
        #endregion

        #region Properties
        public int ItemID
        {
            [DebuggerStepThrough]
            get { return idx; }
            [DebuggerStepThrough]
            set 
            { 
                idx = value;
                service = null;
                category = null;
                attribute = null;
                item = null;
            }
        }

        public int CategoryID
        {
            [DebuggerStepThrough]
            get { return idx; }
            [DebuggerStepThrough]
            set { idx = value; }
        }

        public int ServiceID
        {
            [DebuggerStepThrough]
            get { return idx; }
            [DebuggerStepThrough]
            set { idx = value; }
        }

        public int AttributeID
        {
            [DebuggerStepThrough]
            get { return idx; }
            [DebuggerStepThrough]
            set { idx = value; }
        }

        private ItemAttributeTypeE type = ItemAttributeTypeE.Unknown;
        public ItemAttributeTypeE ItemAttributeType
        {
            [DebuggerStepThrough]
            get { return type; }
        }

        #region Foregin Keys
        private Service service = null;
        public Service Service
        {
            [DebuggerStepThrough]
            get
            {
                if (service == null)
                {
                    service = new Service(Cxt, ServiceID);
                }

                return service;
            }
        }

        private Category category = null;
        public Category Category
        {
            [DebuggerStepThrough]
            get
            {
                if (category == null)
                {
                    category = new Category(Cxt, CategoryID);
                }

                return category;
            }
        }


        private Attribute attribute = null;
        public Attribute Attribute
        {
            [DebuggerStepThrough]
            get
            {
                if (attribute == null)
                {
                    attribute = new Attribute(Cxt, AttributeID);
                }

                return attribute;
            }
        }

        private _Item item = null;
        public _Item Item
        {
            [DebuggerStepThrough]
            get
            {
                if (item == null)
                {
                    item = new _Item(Cxt, ItemID);
                }

                return item;
            }
        }

        #endregion
        #endregion

        #region Filter

        #region To
        public int ToInt32(AttributeE code)
        {
            return BaseItem.ToInt32(ToString(code));
        }

        public bool ToBool(AttributeE code)
        {
            return ToString(code) == "1";
        }

        public string ToString(AttributeE code)
        {
            return Get(code).Value1.ToString();
        }
        #endregion

        #region Get

        public ItemAttribute Get(AttributeE code)
        {
            return Get(Attribute.GetByCode(code).ID);
        }

        public ItemAttribute Get(int attributeID)
        {
            return Filter(UStr.FilterInt32("AttributeID", attributeID)).First;
        }

        public ItemAttribute Get(int attributeID, string value)
        {
            return Filter(UStr.FilterInt32("AttributeID", attributeID) + " AND " + UStr.FilterExact("Value", value)).First;
        }
        #endregion

        #region Set
        public ItemAttribute Set(AttributeE code, object val)
        {
            return Set(Attribute.GetByCode(code).ID, val);
        }

        public ItemAttribute Set(int attributeID, object val)
        {
            if (val == null)
            {
                return null;
            }

            DataTable.DefaultView.RowFilter = "AttributeID=" + attributeID;

            ItemAttribute ia = null;

            if (DataTable.DefaultView.Count == 1)
            {
                ia = new ItemAttribute(Cxt, DataTable.DefaultView[0].Row);
            }
            else
            {
                ia = NewItem(); // create attribute if it does not exists.

                if (Cxt != null)
                {
                    ia.ServiceID = Cxt.ServiceID;
                    ia.CategoryID = Cxt.CategoryID;
                }

                ia.AttributeID = attributeID;

                Add(ia);
            }

            ia.Value1 = val.ToString();

            DataTable.DefaultView.RowFilter = "";

            return ia;
        } 
        #endregion

        #endregion

        #region Contains
        public bool Contains(int attributeID)
        {
            ItemAttribute item = Get(attributeID);

            return !item.IsNew;
        }

        public bool Contains(int attributeID, string value)
        {
            ItemAttribute item = Get(attributeID, value);

            return !item.IsNew;
        }
        #endregion

        #region Children

        public ItemAttributes Children(int attributeID)
        {
            string sql = "SELECT * FROM " + TableName + " WHERE ItemID = " + Item.ID.ToString() + " AND AttributeID IN (SELECT AttributeID FROM Attribute WHERE ParentAttributeID = " + attributeID.ToString() + ")";

            return new ItemAttributes(Cxt, TableName, BaseCollection.ExecuteSql(TableName, sql));
        }

        public ItemAttribute FirstChild(int attributeID)
        {
            ItemAttributes ias = Children(attributeID);

            return new ItemAttribute(Cxt, ias.First);
        }
        #endregion

        #region Save
        public override void Save()
        {
            SqlTransaction t = null;

            try
            {
                t = SqlHelper.BeginTransaction(Config.ConnectionString);

                Save(t);

                SqlHelper.CommitTransaction(t);
            }
            catch (Exception ex)
            {
                SqlHelper.RollbackTransaction(t);

                throw ex;
            }
        }

        public override void Save(SqlTransaction t)
        {
            try
            {
                Item.StatusID = StatusE.Active;
                Item.ServiceID = First.ServiceID;
                Item.CategoryID = First.CategoryID;

                Item.Save(t);

                ItemID = Item.ID;

                for (int i = 0; i < this.Count; i++)
                {
                    ItemAttribute ia = new ItemAttribute(Cxt, this[i]);

                    ia.ItemID = ItemID;

                    ia.Save(t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Load
        private void Load(int itemID, int attributeID)
        {
            this.idx = itemID;

            base.DataTable = BaseCollection.Select(TableName, "ItemID", itemID, "AttributeID", attributeID);
        }

        private void Load(ItemAttributeTypeE type, int idx)
        {
            this.type = type;
            this.idx = idx;

            AttributeE code = AttributeE.Unknown;

            switch (type)
            {
                case ItemAttributeTypeE.ConfigService:
                    code = AttributeE.ServiceID;
                    break;
                case ItemAttributeTypeE.ConfigCategory:
                    code = AttributeE.CategoryID;
                    break;
                case ItemAttributeTypeE.ConfigServiceCategory:
                    code = AttributeE.ServiceCategoryID;
                    break;
                case ItemAttributeTypeE.ConfigAttribute:
                    code = AttributeE.AttributeID;
                    break;
                case ItemAttributeTypeE.ConfigUser:
                    code = AttributeE.UserID;
                    break;
                case ItemAttributeTypeE.ConfigItem:
                    code = AttributeE.ItemID;
                    break;
            }

            switch (type)
            {
                case ItemAttributeTypeE.ConfigService:
                case ItemAttributeTypeE.ConfigCategory:
                case ItemAttributeTypeE.ConfigServiceCategory:
                case ItemAttributeTypeE.ConfigAttribute:
                case ItemAttributeTypeE.ConfigUser:
                case ItemAttributeTypeE.ConfigItem:
                    DataTable = BaseCollection.ExecuteSql(TableName, "select * from " + TableName + " where ItemID IN (select ItemID from " + TableName + " where AttributeID = @p1 AND Value = @p2)", Attribute.GetID(code), idx);
                    break;
                case ItemAttributeTypeE.RecordsItem:
                    DataTable = BaseCollection.Select(TableName, "ItemID", idx);
                    break;
                case ItemAttributeTypeE.RecordsCategory:
                    DataTable = BaseCollection.ExecuteSql(TableName, "SELECT " + TableName + ".* FROM Attribute RIGHT OUTER JOIN " + TableName + " ON Attribute.AttributeID = " + TableName + ".AttributeID WHERE Attribute.CategoryID = @p1", idx);
                    break;
            }
        }
        #endregion

        #region Upload
        public static void UploadXml(Cxt cxt, RsOneItemTable t, string xml)
        {
            string fileXsd = Config.FolderXsd + "ItemAttribute.xsd";

            ItemAttributes data = new ItemAttributes(cxt, t, new DataTable());

            data.LoadXml(fileXsd, xml);

            Upload(cxt, t, data);
        }

        public static void UploadZip(Cxt cxt, RsOneItemTable t, string fileZip)
        {
            string fileXsd = Config.FolderXsd + "ItemAttribute.xsd";

            UZip.Unzip(fileZip, true);

            ItemAttributes data = new ItemAttributes(cxt, fileXsd, fileZip.Replace(".zip", ".xml"));

            Upload(cxt, t, data);
        }

        public static void Upload(Cxt cxt, RsOneItemTable t, ItemAttributes data)
        {
            DataTable items = data.DataTable.DefaultView.ToTable(true, "ItemID");

            foreach (DataRow row in items.Rows)
            {
                DataTable table = new DataView(data.DataTable, "ItemID=" + row["ItemID"], "", DataViewRowState.Added).ToTable(t.ToString());

                ItemAttributes x = new ItemAttributes(cxt, t, table);

                x.Save();
            }
        }

        #endregion

        #region Methods
        public static ItemAttributes GetTableItemAttributes(RsOneItemTable tableName, int id)
        {
            BaseItem item = BaseCollection.SelectItem(tableName, id);

            ItemAttributes iac = new ItemAttributes();

            foreach (DataColumn col in item.DataRow.Table.Columns)
            {
                ItemAttribute ia = new ItemAttribute();

                ia.ItemID = id;

                ia.Value1 = BaseItem.GetCol(item.DataRow, col.ColumnName);

                iac.Add(ia);
            }

            return iac;
        }

        public static ItemAttribute GetTableItemAttributes(RsOneItemTable tableName, int id, string attributeName)
        {
            BaseItem item = BaseCollection.SelectItem(tableName, id);

            ItemAttribute ia = new ItemAttribute();

            ia.ItemID = id;

            ia.Value1 = BaseItem.GetCol(item.DataRow, attributeName);

            ia.DataRow.AcceptChanges();

            return ia;
        }

        public static BaseItem GetTableFromItemAttributes(RsOneItemTable tableName, ItemAttributes ias)
        {
            BaseItem item = BaseCollection.SelectItem(tableName, 0);

            for (int i = 0; i < ias.Count; i++)
            {
                ItemAttribute ia = new ItemAttribute(ias[i].Cxt, ias[i]);

                item.SetColumn(i, ia.Value1, item.DataRow.Table.Columns[i].DataType);
            }

            return item;
        }

        public static ItemAttribute GetAdminServiceCategory(int categoryID, int attributeID, string value)
        {
            string sql = "select * from [ZAdminAttribute] where serviceid=16 and categoryid=@p1 and attributeid=@p2 and value=@p3";

            return BaseItems<ItemAttribute, ItemAttributes>.SelectItem2(RsOneItemTable.ItemAttribute, sql, categoryID, attributeID, value);
        }

        #endregion    
    }
}
