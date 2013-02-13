// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Diagnostics;

namespace App.Model 
{
    #region Code Generator SQL
    
    //select 
    //'public int ' + name + ' { get { return GetColInt32("' + name + '"); } set { SetColumn("' + name + '", value); } } '
    //from sys.columns where object_id in (select object_id from sys.tables where name = 'AttributeLayout')
    
    #endregion

    public class BaseItem
	{
        #region Data Members
        public DataRow DataRow = null;
        public Cxt Cxt = null;
        private RsOneItemTable tableName = RsOneItemTable.Unknown;
        #endregion

        #region Constructor
        public BaseItem()
        {
        }

        public BaseItem(int id)
            : this(null, id)
        {
        }

        public BaseItem(Cxt cxt, int id)
        {
            Cxt = cxt;

            Load(id);
        }

        public BaseItem(Cxt cxt, BaseItem item)
        {
            Cxt = cxt;

            DataRow = item.DataRow;
        }

        public BaseItem(Cxt cxt, DataRow row)
        {
            Cxt = cxt;

            DataRow = row;
        } 
        #endregion

        #region Properties
        public virtual RsOneItemTable TableName
        {
            [DebuggerStepThrough]
            get { return tableName; }
            [DebuggerStepThrough]
            set { tableName = value; }
        }

        public virtual string PrimaryKey
        {
            [DebuggerStepThrough]
            get { return TableName.ToString() + "ID"; }
        }

        public virtual int ID
        {
            [DebuggerStepThrough]
            get { return GetColInt32(PrimaryKey); }
        }

        public virtual bool IsNew
        {
            get
            {
                if (DataRow == null)
                {
                    return true;
                }

                if (DataRow.RowState == DataRowState.Added)
                {
                    return true;
                }

                return false; 
            }
            set
            {
                if (DataRow == null)
                {
                    return;
                }

                if (!CanGetRow(DataRow))
                {
                    return;
                }

                DataRow.AcceptChanges();

                if (value)
                {
                    DataRow.SetAdded();
                }
                else
                {
                    DataRow.SetModified();
                }
            }
        }

        public virtual bool IsUnchanged
        {
            get
            {
                if (DataRow == null)
                {
                    return true;
                }

                if (DataRow.RowState == DataRowState.Unchanged)
                {
                    return true;
                }

                return false;
            }
        }

        public int ModifiedBy
        {
            get { return GetColInt32("ModifiedBy"); }
            set { SetColumn("ModifiedBy", value); }
        }

        public DateTime DateModified
        {
            get { return GetColDateTime("DateModified"); }
            set { SetColumn("DateModified", value); }
        }

        public int CreatedBy
        {
            get { return GetColInt32("CreatedBy"); }
            set { SetColumn("CreatedBy", value); }
        }

        public DateTime DateCreated
        {
            get { return GetColDateTime("DateCreated"); }
            set { SetColumn("DateCreated", value); }
        }

        public string Description
        {
            get { return GetCol("Description"); }
            set { SetColumn("Description", value); }
        }

        public string Name
        {
            get { return GetCol("Name"); }
            set { SetColumn("Name", value); }
        }

        #region Foreign Keys

        private User createdUser = null;
        public User CreatedUser
        {
            get
            {
                if (createdUser == null)
                {
                    createdUser = new User(Cxt, CreatedBy);
                }

                return createdUser;
            }
        }

        private User modifiedUser = null;
        public User ModifiedUser
        {
            get
            {
                if (modifiedUser == null)
                {
                    modifiedUser = new User(Cxt, ModifiedBy);
                }

                return modifiedUser;
            }
        } 
        #endregion

        #region Calculated
        public string CreatedDuration
        {
            get
            {
                return UStr.Duration(DateCreated);
            }
        }

        public string ModifiedDuration
        {
            get
            {
                return UStr.Duration(DateModified);
            }
        }
        #endregion
        #endregion

        #region Common
        public void Load(int id)
        {
            if (TableName == RsOneItemTable.Unknown)
            {
                return;
            }

            DataTable table = BaseCollection.Select(TableName, PrimaryKey, id);

            SetRow(table);
        }

        protected void SetRow(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                DataRow = table.Rows[0];
            }
            else
            {
                NewRow(table);
            }
        }

        public virtual void Save()
        {
            Save(Config.ConnectionString);
        }

        public virtual void Save(string connectionString)
        {
            Save(connectionString, null);
        }

        public virtual void Save(SqlTransaction t)
        {
            Save(t.Connection.ConnectionString, t);
        }

        protected virtual void Save(string connectionString, SqlTransaction t)
        {
            if (IsUnchanged)
            {
                return;
            }

            UpdateAuditLog();

            SqlHelper.Save(connectionString, t, DataRow);
        }

        private void UpdateAuditLog()
        {
            int userID = 1;

            if (Cxt != null)
            {
                if (Cxt.User.ID > 0)
                {
                    userID = Cxt.User.ID;
                }
            }

            if (IsNew)
            {
                CreatedBy = userID;
                DateCreated = DateTime.Now;
            }
            else
            {
                ModifiedBy = userID;
                DateModified = DateTime.Now;
            }
        }

        public virtual void NewRow(DataTable table)
        {
            DataRow = table.NewRow();

            table.Rows.Add(DataRow);

            SetColumn("CreatedBy", 1);
            SetColumn("DateCreated", DateTime.Now);
        }

        public virtual void SetTableName(string tableName)
        {
            //SELECT 'case "'+name+'": TableName = RsOneItemTable.'+name+'; break;' FROM sys.objects where type='U' and name not in ('sysdiagrams', 'dtproperties') order by name
            switch (tableName)
            {
                case "Attribute": TableName = RsOneItemTable.Attribute; break;
                case "AttributeLayout": TableName = RsOneItemTable.AttributeLayout; break;
                case "AttributeTable": TableName = RsOneItemTable.AttributeTable; break;
                case "AttributeType": TableName = RsOneItemTable.AttributeType; break;
                case "Category": TableName = RsOneItemTable.Category; break;
                case "Item": TableName = RsOneItemTable.Item; break;
                case "ItemAttribute": TableName = RsOneItemTable.ItemAttribute; break;
                case "Layout": TableName = RsOneItemTable.Layout; break;
                case "Log": TableName = RsOneItemTable.Log; break;
                case "Role": TableName = RsOneItemTable.Role; break;
                case "RoleTask": TableName = RsOneItemTable.RoleTask; break;
                case "Service": TableName = RsOneItemTable.Service; break;
                case "ServiceCategory": TableName = RsOneItemTable.ServiceCategory; break;
                case "ServiceTask": TableName = RsOneItemTable.ServiceTask; break;
                case "ServiceType": TableName = RsOneItemTable.ServiceType; break;
                case "ServiceUser": TableName = RsOneItemTable.ServiceUser; break;
                case "Status": TableName = RsOneItemTable.Status; break;
                case "Task": TableName = RsOneItemTable.Task; break;
                case "User": TableName = RsOneItemTable.User; break;
                case "UserRole": TableName = RsOneItemTable.UserRole; break;
                case "ZAdminAttribute": TableName = RsOneItemTable.ZAdminAttribute; break;
                case "ZDummyAttribute": TableName = RsOneItemTable.ZDummyAttribute; break;
                default: TableName = RsOneItemTable.Unknown; break;
            }
        }

        #endregion

        #region GetColDateTime

        public DateTime GetColDateTime(string columnName)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(DataRow, columnName, false));
        }

        public DateTime GetColDateTime(object row, string columnName, DateTime deafultValue)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        public DateTime GetColDateTime(string columnName, DateTime deafultValue)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(DataRow, columnName, deafultValue));
        }

        public static DateTime GetColDateTime(object row, string columnName)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(row, columnName, false));
        }

        public static DateTime GetColDateTime(DataRowView row, string columnName)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(row, columnName, false));
        }

        public static DateTime GetColDateTime(DataRow row, string columnName)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(row, columnName, false));
        }

        public static DateTime GetColDateTime(DataRowView row, string columnName, DateTime deafultValue)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        public static DateTime GetColDateTime(DataRow row, string columnName, DateTime deafultValue)
        {
            return Convert.ToDateTime(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        #endregion

        #region GetColBool

        public bool GetColBool(string columnName)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(DataRow, columnName, false));
        }

        public bool GetColBool(object row, string columnName, bool deafultValue)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        public bool GetColBool(string columnName, bool deafultValue)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(DataRow, columnName, deafultValue));
        }

        public static bool GetColBool(object row, string columnName)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(row, columnName, false));
        }

        public static bool GetColBool(DataRowView row, string columnName)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(row, columnName, false));
        }

        public static bool GetColBool(DataRow row, string columnName)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(row, columnName, false));
        }

        public static bool GetColBool(DataRowView row, string columnName, bool deafultValue)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        public static bool GetColBool(DataRow row, string columnName, bool deafultValue)
        {
            return Convert.ToBoolean(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        #endregion

        #region GetColInt32

        public int GetColInt32(string columnName)
        {
            return Convert.ToInt32(BaseItem.GetColumn(DataRow, columnName, 0));
        }

        public int GetColInt32(object row, string columnName, int deafultValue)
        {
            return Convert.ToInt32(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        public int GetColInt32(string columnName, int deafultValue)
        {
            return Convert.ToInt32(BaseItem.GetColumn(DataRow, columnName, deafultValue));
        }

        public static int GetColInt32(object row, string columnName)
        {
            return Convert.ToInt32(BaseItem.GetColumn(row, columnName, 0));
        }

        public static int GetColInt32(DataRowView row, string columnName)
        {
            return Convert.ToInt32(BaseItem.GetColumn(row, columnName, 0));
        }

        public static int GetColInt32(DataRow row, string columnName)
        {
            return Convert.ToInt32(BaseItem.GetColumn(row, columnName, 0));
        }

        public static int GetColInt32(DataRowView row, string columnName, int deafultValue)
        {
            return Convert.ToInt32(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        public static int GetColInt32(DataRow row, string columnName, int deafultValue)
        {
            return Convert.ToInt32(BaseItem.GetColumn(row, columnName, deafultValue));
        }

        #endregion

        #region GetCol

        public string GetCol(string columnName)
        {
            return BaseItem.GetColumn(DataRow, columnName, "").ToString();
        }

        public string GetCol(object row, string columnName, string deafultValue)
        {
            return BaseItem.GetColumn(row, columnName, deafultValue).ToString();
        }

        public string GetCol(string columnName, string deafultValue)
        {
            return BaseItem.GetColumn(DataRow, columnName, deafultValue).ToString();
        }

        public static string GetCol(object row, string columnName)
        {
            return BaseItem.GetColumn(row, columnName, "").ToString();
        }

        public static string GetCol(DataRowView row, string columnName)
        {
            return BaseItem.GetColumn(row, columnName, "").ToString();
        }

        public static string GetCol(DataRow row, string columnName)
        {
            return BaseItem.GetColumn(row, columnName, "").ToString();
        }

        public static string GetCol(DataRowView row, string columnName, string deafultValue)
        {
            return BaseItem.GetColumn(row, columnName, deafultValue).ToString();
        }

        public static string GetCol(DataRow row, string columnName, string deafultValue)
        {
            return BaseItem.GetColumn(row, columnName, deafultValue).ToString();
        }

        #endregion

        #region GetColumn
        public object GetColumn(string columnName)
        {
            return GetColumn(columnName, "");
        }

        public object GetColumn(string columnName, object deafultValue)
        {
            return BaseItem.GetColumn(DataRow, columnName, deafultValue);
        }

        public static object GetColumn(object row, string columnName, object deafultValue)
        {
            if (row is DataRow)
            {
                return BaseItem.GetColumn((DataRow)row, columnName, deafultValue);
            }
            if (row is DataRowView)
            {
                return BaseItem.GetColumn((DataRowView)row, columnName, deafultValue);
            }
            else
            {
                return deafultValue;
            }
        }

        public static object GetColumn(DataRowView row, string columnName, object deafultValue)
        {
            return BaseItem.GetColumn(row.Row, columnName, deafultValue);
        }

        public static object GetColumn(DataRow row, string columnName, object deafultValue)
        {
            try
            {
                if (!CanGetColumn(row, columnName))
                {
                    return deafultValue;
                }

                object val = row[columnName].ToString();

                if (row.Table.Columns[columnName].DataType != Type.GetType("System.String"))
                {
                    if (val == null || String.IsNullOrEmpty(val.ToString()))
                    {
                        return deafultValue;
                    }
                }

                return val;
            }
            catch
            {
                return deafultValue;
            }
        }

        public static bool CanGetColumn(DataRow row, string columnName)
        {
            try
            {
                if (row == null || 
                    row.RowState == DataRowState.Deleted || 
                    String.IsNullOrEmpty(columnName) || 
                    !row.Table.Columns.Contains(columnName))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool CanGetRow(DataRow row)
        {
            return !(row == null || row.RowState == DataRowState.Deleted);
        }
        #endregion

        #region SetColumn
        public bool SetColumn(string columnName, object val)
        {
            if (!CanGetColumn(DataRow, columnName))
            {
                return false;
            }

            DataRow[columnName] = val;

            return true;
        }

        public bool SetColumn(int columnIndex, object val)
        {
            if (!CanGetColumn(DataRow, DataRow.Table.Columns[columnIndex].ColumnName))
            {
                return false;
            }

            DataRow[columnIndex] = val;

            return true;
        }

        public bool SetColumn(int columnIndex, object val, Type type)
        {
            if (!CanGetColumn(DataRow, DataRow.Table.Columns[columnIndex].ColumnName))
            {
                return false;
            }

            DataRow[columnIndex] = ToType(type, val, System.DBNull.Value);

            return true;
        } 
        #endregion

        #region ToString
        public override string ToString()
        {
            return ID + "|" + Name;
        } 
        #endregion

        #region To

        #region ToDouble

        public static double ToDouble(object o)
        {
            return BaseItem.ToDouble(o, 0);
        }

        public static double ToDouble(GridViewRowEventArgs e, string columnName)
        {
            return BaseItem.ToDouble(((System.Data.DataRowView)(e.Row.DataItem)).Row[columnName]);
        }

        public static double ToDouble(DataListItemEventArgs e, string columnName)
        {
            return BaseItem.ToDouble(((System.Data.DataRowView)(e.Item.DataItem)).Row[columnName]);
        }

        public static double ToDouble(object o, double defaultValue)
        {
            try
            {
                if (o != null)
                {
                    return Convert.ToDouble(o.ToString());
                }
            }
            catch
            {
            }

            return defaultValue;
        }
        #endregion

        #region ToInt32

        public static int ToInt32(object o)
        {
            return BaseItem.ToInt32(o, 0);
        }

        public static int ToInt32(GridViewRowEventArgs e, string columnName)
        {
            return BaseItem.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row[columnName]);
        }

        public static int ToInt32(DataListItemEventArgs e, string columnName)
        {
            return BaseItem.ToInt32(((System.Data.DataRowView)(e.Item.DataItem)).Row[columnName]);
        }

        public static int ToInt32(object o, int defaultValue)
        {
            try
            {
                if (o != null)
                {
                    return Convert.ToInt32(o.ToString());
                }
            }
            catch
            {
            }

            return defaultValue;
        }
        #endregion

        #region ToBool

        public static bool ToBool(object o)
        {
            return BaseItem.ToBool(o, false);
        }

        public static bool ToBool(object o, bool defaultValue)
        {
            try
            {
                if (o != null)
                {
                    return Convert.ToBoolean(o.ToString());
                }
            }
            catch
            {
            }

            return defaultValue;
        }
        #endregion

        #region ToString

        public static string ToString(object o)
        {
            return BaseItem.ToString(o, "");
        }

        public static string ToString(GridViewRowEventArgs e, string columnName)
        {
            return BaseItem.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row[columnName]);
        }

        public static string ToString(DataListItemEventArgs e, string columnName)
        {
            return BaseItem.ToString(((System.Data.DataRowView)(e.Item.DataItem)).Row[columnName]);
        }

        public static string ToString(object o, string defaultValue)
        {
            try
            {
                if (o != null)
                {
                    return o.ToString();
                }
            }
            catch
            {
            }

            return defaultValue;
        }
        #endregion 

        #region ToType
        public static object ToType(Type type, object o)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return ToBool(o);

                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return ToDouble(o);

                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.DBNull:
                case TypeCode.DateTime:
                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.SByte:
                case TypeCode.String:
                default:
                    return ToString(o);
            }
        }

        public static object ToType(Type type, object o, object defaultValue)
        {
            try
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                        return Convert.ToBoolean(o);

                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return Convert.ToDouble(o);

                    case TypeCode.Byte:
                    case TypeCode.Char:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                    case TypeCode.Empty:
                    case TypeCode.Object:
                    case TypeCode.SByte:
                    case TypeCode.String:
                    default:
                        return Convert.ToString(o);
                }
            }
            catch
            {
            }

            return defaultValue;
        } 
        #endregion

        #endregion

        #region Filter
        public static string FilterOr(string filter, string append)
        {
            return ApppendFilter(filter, append, "OR");
        }

        public static string FilterAnd(string filter, string append)
        {
            return ApppendFilter(filter, append, "AND");
        }

        public static string ApppendFilter(string filter, string append, string logicalOperator)
        {
            if (append != "")
            {
                filter += append + " " + logicalOperator + " ";
            }

            return filter;
        }

        public static string TrimOr(string filter)
        {
            filter = UStr.TrimEnd(filter, " OR ");

            return filter;
        }

        public static string TrimAnd(string filter)
        {
            filter = UStr.TrimEnd(filter, " AND ");

            return filter;
        } 
        #endregion
    }
}
