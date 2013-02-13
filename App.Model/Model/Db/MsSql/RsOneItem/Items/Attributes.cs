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
    public class Attributes : BaseItems<Attribute, Attributes>
	{
        public Attribute Parent = null;

        #region Ctor
        public Attributes()
        {
        }


        public Attributes(Cxt cxt, bool sysAttributes)
        {
            Cxt = cxt;

            base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Attribute, "SELECT * FROM Attribute WHERE Code IS NOT NULL", null);
        }

        public Attributes(Cxt cxt, BaseCollection items)
            : this(cxt, items.DataTable)
        {
        }

        public Attributes(Cxt cxt, DataTable table)
        {
            Cxt = cxt;

            DataTable = table;
        }

        public Attributes(Cxt cxt, Attribute parent)
        {
            Cxt = cxt;

            Parent = parent;

            DataTable = BaseCollection.ExecuteSql("select * from Attribute where ParentAttributeID = " + Parent.ID + " order by Sequence");
        }

        public Attributes(Cxt cxt, int categoryID)
            : this(cxt, categoryID, StatusE.Active, 0)
        {
        }

        public Attributes(Cxt cxt, int categoryID, StatusE status)
            : this(cxt, categoryID, status, 0)
        {
        }

        public Attributes(Cxt cxt, int categoryID, StatusE status, int selectCount)
        {
            Cxt = cxt;

            Load(categoryID, status, selectCount);
        }

        #endregion

        #region Properties
        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Attribute; }
            set { base.TableName = value; }
        }
        #endregion 
        #endregion

        #region Methods
        public static Attributes GetTableAttributes(RsOneItemTable tableName)
        {
            string sql = "SELECT * FROM syscolumns where id IN (SELECT id FROM sysobjects WHERE name = '" + tableName.ToString() + "') order by colid";

            DataTable table = BaseCollection.ExecuteSql(tableName, sql);

            Attributes ac = new Attributes();

            foreach (DataRow row in table.Rows)
            {
                Attribute a = GetTableAttribute(row);

                if (a != null)
                {
                    ac.Add(a);
                }
            }

            return ac;
        }

        private static Attribute GetTableAttribute(DataRow row)
        {
            string name = BaseItem.GetCol(row, "name");

            if (UStr.Contains("CreatedBy,DateCreated,ModifiedBy,DateModified", name))
            {
                return null;
            }

            Attribute a = new Attribute();

            a.Name = a.Description = name;
            a.Sequence = BaseItem.GetColInt32(row, "colid");
            a.AttributeTypeID = AttributeType.FromSqlType((SqlSysTypeE)BaseItem.GetColInt32(row, "xusertype"));

            if (a.Name == SqlHelper.PkColumn(row))
            {
                a.AttributeTypeID = AttributeTypeE.RsLabel;
            }
            else if (a.Name.EndsWith("ID"))
            {
                a.AttributeTypeID = AttributeTypeE.RsDropDownList;

                string t = "";

                t = UStr.TrimEnd(a.Name, "ID");
                t = UStr.TrimStart(t, "Parent");

                a.Value = Kvs.ToXml(
                            "DataSource", t,
                            "DataSourceType", 1,
                            "DataValueField", t + "ID",
                            "DataTextField", "Name",
                            "ShowDefaultItem", "true");
            }

            return a;
        } 
        #endregion

        #region Load

        private void Load(int categoryID, StatusE status, int selectCount)
        {
            string sql = "";

            if (categoryID == 0)
            {
                if (status == StatusE.Unknown)
                {
                    sql = "SELECT  " + UData.ToSelectCount(selectCount) + " Attribute.* ";
                    sql += "\nFROM         Attribute ORDER BY [Name]";

                    base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Attribute, sql, null);
                }
                else
                { 
                    sql = "SELECT  " + UData.ToSelectCount(selectCount) + " Attribute.* ";
                    sql += "\nFROM         Attribute WHERE (StatusID = @p1) ORDER BY [Name]";

                    base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Attribute, sql, status.ToString("d"));
                }
            }
            else
            {
                if (status == StatusE.Unknown)
                {
                    sql = "SELECT  " + UData.ToSelectCount(selectCount) + " Attribute.* ";
                    sql += "\nFROM         Attribute WHERE (CategoryID = @p1) ORDER BY [Sequence]";

                    base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Attribute, sql, categoryID);
                }
                else
                {
                    sql = "SELECT  " + UData.ToSelectCount(selectCount) + " Attribute.* ";
                    sql += "\nFROM         Attribute WHERE (CategoryID = @p1) AND (StatusID = @p2) ORDER BY [Sequence]";

                    base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Attribute, sql, categoryID, status.ToString("d"));
                }
            }
        }

        #endregion

    }
}
