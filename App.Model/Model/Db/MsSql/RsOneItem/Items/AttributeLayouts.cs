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

namespace App.Model 
{
    public class AttributeLayouts : BaseItems<AttributeLayout, AttributeLayouts>
	{
        public int CategoryID = 0;

        #region Constructors
        public AttributeLayouts()
        {
        }

        public AttributeLayouts(Cxt cxt, int categoryID)
        {
            Cxt = cxt;

            CategoryID = categoryID;

            Load(categoryID);
        }

        public AttributeLayouts(Cxt cxt, BaseCollection items)
        {
            Cxt = cxt;

            DataTable = items.DataTable;
        }

        public AttributeLayouts(Cxt cxt, DataTable table)
        {
            Cxt = cxt;

            DataTable = table;
        }

        public AttributeLayouts(Cxt cxt, int itemID, int attributeID)
        {
            Cxt = cxt;

            Load(itemID, attributeID);
        }
        
        #endregion

        #region Properties
        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.AttributeLayout; }
            set { base.TableName = value; }
        }
        #endregion

        private Category category = null;
        public Category Category
        {
            get
            {
                if (category == null)
                {
                    category = new Category(Cxt, Category);
                }

                return category;
            }
        } 
        #endregion

        #region Filter

        public AttributeLayout Get(int attributeID)
        {
            return Filter(UStr.FilterInt32("AttributeID", attributeID)).First;
        }

        public AttributeLayout Get(int attributeID, string value)
        {
            return Filter(UStr.FilterInt32("AttributeID", attributeID) + " AND " + UStr.FilterExact("Value", value)).First;
        }

        #endregion

        #region Contains
        public bool Contains(int attributeID)
        {
            AttributeLayout item = Get(attributeID);

            return !item.IsNew;
        }

        #endregion

        #region Load
        private void Load(int layoutID, int attributeID)
        {
            base.DataTable = BaseCollection.Select(RsOneItemTable.AttributeLayout, "ItemID", layoutID, "AttributeID", attributeID);
        }

        private void Load(int categoryID)
        {
            string sql = "SELECT * FROM Attribute RIGHT OUTER JOIN AttributeLayout ON Attribute.AttributeID = AttributeLayout.AttributeID WHERE Attribute.CategoryID = " + categoryID;

            base.DataTable =BaseCollection.ExecuteSql(RsOneItemTable.AttributeLayout, sql);
        }
        #endregion

    }
}
