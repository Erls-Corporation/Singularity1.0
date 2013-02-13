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
using System.IO;
using System.Xml;
using System.Web;

namespace App.Model 
{
    public class AttributeLayout : BaseItem
	{
        private Layout layout = null;
        private Attribute attribute = null;

        #region Constructor
        public AttributeLayout()
            : base(0)
        {
        }

        public AttributeLayout(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public AttributeLayout(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }

        public AttributeLayout(Cxt cxt, int layoutID, int attributeID)
        {
            Cxt = cxt;

            Load(layoutID, attributeID);
        }

        public AttributeLayout(Cxt cxt, int categoryID, bool flag)
        {
            Cxt = cxt;

            Load(categoryID);
        }
        #endregion

        #region Properties
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.AttributeLayout; }
            set { base.TableName = value; }
        }

        public int AttributeID { get { return GetColInt32("AttributeID"); } set { SetColumn("AttributeID", value); } }
        public int LayoutID { get { return GetColInt32("LayoutID"); } set { SetColumn("LayoutID", value); } }
        public bool IsVisible { get { return GetColBool("IsVisible"); } set { SetColumn("IsVisible", value); } }
        public bool IsEnabled { get { return GetColBool("IsEnabled"); } set { SetColumn("IsEnabled", value); } }

        public Attribute Attribute
        {
            get
            {
                if (attribute == null)
                {
                    attribute = new Attribute(Cxt, AttributeID);
                }

                return attribute;
            }
        }

        public Layout Layout
        {
            get
            {
                if (layout == null)
                {
                    layout = new Layout(Cxt, LayoutID);
                }

                return layout;
            }
        }

        #endregion

        #region Load
        private void Load(int layoutID, int attributeID)
        {
            DataTable table = BaseCollection.Select(RsOneItemTable.AttributeLayout, "layoutID", layoutID, "AttributeID", attributeID);

            SetRow(table);

            // if no matching row is found atleast set layoutID and attributeID passed!
            this.LayoutID = layoutID;
            AttributeID = attributeID;
        }
        
        #endregion

        #region ToString
        public override string ToString()
        {
            return ID + "|IID=" + Layout.Name + "|AID=" + Attribute.Name;
        }
        #endregion
    }
}
