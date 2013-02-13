// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace App.Model 
{
    public class ProcessUpdateCategory : BaseProcess
	{
        public ProcessUpdateCategory(Cxt cxt)
            : base(cxt)
        {
        }

        public override void Start()
        {
            Update();
        }

        public void Update()
        {
            string sql = "SELECT * FROM sys.tables WHERE name NOT IN (select name collate Latin1_General_CS_AI FROM Category) AND type ='U' AND name NOT IN ('sysdiagrams', 'dtproperties') ORDER BY name";

            DataTable table = BaseCollection.ExecuteSql(sql);

            foreach (DataRow row in table.Rows)
            {
                Update(row);
            }
        }

        private void Update(DataRow row)
        {
            Category c = new Category(Cxt, BaseCollection.SelectItem(RsOneItemTable.Category, "Name", UStr.Quote(BaseItem.GetCol(row, "name"))));

            if (c.IsNew)
            {
                c = new Category();
            }
            
            c.Cxt = Cxt;
            c.Name = BaseItem.GetCol(row, "name");
            c.CategoryTypeID = CategoryTypeID.System;
            c.Save();

            UpdateAttributes(c.Name);
        }

        public void UpdateAttributes(string categoryName)
        {
            Category c = new Category(Cxt, BaseCollection.SelectItem(RsOneItemTable.Category, "Name", UStr.Quote(categoryName)));

            if (c.CategoryTypeID != CategoryTypeID.System)
            {
                return;
            }

            string sql = "SELECT * FROM sys.columns where object_id IN (SELECT object_id FROM sys.tables WHERE name = " + UStr.Quote(categoryName) + ")";

            DataTable table = BaseCollection.ExecuteSql(sql);

            foreach (DataRow row in table.Rows)
            {
                UpdateAttribute(c, row);
            }
        }

        private void UpdateAttribute(Category c, DataRow row)
        {
            Attribute a = new Attribute(Cxt, BaseCollection.SelectItem(RsOneItemTable.Attribute, "CategoryID", c.ID, "Name", UStr.Quote(BaseItem.GetCol(row, "name"))));

            if (a.IsNew)
            {
                a = new Attribute();
            }

            a.Cxt = Cxt;
            a.Name = a.Description = BaseItem.GetCol(row, "name");
            a.Sequence = BaseItem.GetColInt32(row, "column_id");

            a.Save();
        }
    }
}
