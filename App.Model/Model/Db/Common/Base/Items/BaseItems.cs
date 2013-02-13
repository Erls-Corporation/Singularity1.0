// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace App.Model
{
    public class BaseItems<T, TC> : IBaseItems<T, TC>
        where T : BaseItem, new()
        where TC : IBaseItems<T, TC>, new()
    {
        #region Data Members
        private DataTable dataTable = null;
        private Cxt cxt = null;
        public Dictionary<int, T> cache = new Dictionary<int, T>();
        private RsOneItemTable tableName = RsOneItemTable.Unknown;
        #endregion

        #region Constructor
        public BaseItems()
        {

        }

        public BaseItems(DataTable table)
        {
            DataTable = table;
        }

        public BaseItems(string xsdPath, string xmlPath)
        {
            Load(xsdPath, xmlPath);
        }

        public BaseItems(string sql)
        {
            DataTable = BaseCollection.ExecuteSql(sql);
        }

        #endregion

        #region Properties
        public override DataTable DataTable
        {
            [DebuggerStepThrough]
            get { return dataTable; }
            [DebuggerStepThrough]
            set { dataTable = value; }
        }
        public override Cxt Cxt
        {
            [DebuggerStepThrough]
            get { return cxt; }
            [DebuggerStepThrough]
            set { cxt = value; }
        }

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

        #endregion

        #region Select

        #region Select
        public static DataTable Select(string tableName)
        {
            DataTable table = SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, UData.ToSelectAll(tableName)).Tables[0];

            table.TableName = tableName;

            return table;
        }

        public static DataTable Select(RsOneItemTable tableName)
        {
            return Select(tableName.ToString());
        }

        public static DataTable Select(RsOneItemTable tableName, int id)
        {
            return Select(tableName, tableName.ToString() + "ID", id);
        }

        public static DataTable Select(RsOneItemTable tableName, params object[] whereColVals)
        {
            return Select(tableName.ToString(), whereColVals);
        }

        public static DataTable Select(string tableName, params object[] whereColVals)
        {
            string sql = UData.ToSelect(tableName, whereColVals);

            DataTable table = SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, sql).Tables[0];

            table.TableName = tableName;

            return table;
        }

        #endregion

        #region SelectItem
        public static T SelectItem(RsOneItemTable tableName, int id)
        {
            return SelectItem(tableName, tableName.ToString() + "ID", id);
        }

        public static T SelectItem(RsOneItemTable tableName, params object[] whereColVals)
        {
            TC items = SelectItems(tableName, whereColVals);

            return items.First;
        }

        public static T SelectItem(string sql)
        {
            return SelectItem(sql, "");
        }

        public static T SelectItem(string sql, string filter)
        {
            TC items = SelectItems(sql, filter);

            return items.First;
        }

        public static T SelectItem2(RsOneItemTable tableName, string sql, params object[] vals)
        {
            return SelectItem2(tableName.ToString(), sql, vals);
        }

        public static T SelectItem2(string tableName, string sql, params object[] vals)
        {
            TC items = SelectItems2(tableName, sql, vals);

            return items.First;
        }
        #endregion

        #region SelectItems

        public static TC SelectItems(RsOneItemTable tableName, params object[] whereColVals)
        {
            string s = UData.ToSelect(tableName, whereColVals);

            return SelectItems(tableName.ToString(), s);
        }

        public static TC SelectItems(RsOneItemTable tableName, string sql)
        {
            TC items = SelectItems(tableName.ToString(), sql);

            return items;
        }

        public static TC SelectItems(string tableName, string sql, string filter)
        {
            string s = UData.ToSql(sql, filter);

            return SelectItems(tableName, s);
        }

        public static TC SelectItems(string tableName, string sql)
        {
            TC items = new TC();

            items.DataTable = BaseCollection.ExecuteSql(sql);

            items.DataTable.TableName = tableName;

            return items;
        }

        public static TC SelectItems2(RsOneItemTable tableName, string sql, params object[] vals)
        {
            return SelectItems2(tableName.ToString(), sql, vals);
        }

        public static TC SelectItems2(string tableName, string sql, params object[] vals)
        {
            TC items = new TC();

            items.DataTable = BaseCollection.ExecuteSql(tableName, sql, vals);

            items.DataTable.TableName = tableName;

            return items;
        }

        #endregion


        #endregion

        #region Execute
        public static DataTable Execute(string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteDataset(Config.ConnectionString, spName, parameterValues).Tables[0];
        }

        public static DataTable ExecuteSql(string sql)
        {
            return ExecuteSql("Table1", sql, null);
        }

        public static DataTable ExecuteSql(RsOneItemTable tableName, string sql, params object[] vals)
        {
            return ExecuteSql(tableName.ToString(), sql, vals);
        }

        public static DataTable ExecuteSql(string tableName, string sql, params object[] vals)
        {
            DataSet ds = SqlHelper.ExecuteSql(Config.ConnectionString, sql, vals);

            DataTable table = null;

            if (ds.Tables.Count != 0)
            {
                table = ds.Tables[0];

                table.TableName = tableName;
            }

            return table;
        }

        public static DataTable ExecuteSql(RsOneItemTable tableName, string sql, SqlParameter[] commandParameters)
        {
            return ExecuteSql(tableName.ToString(), sql, commandParameters);
        }

        public static DataTable ExecuteSql(string tableName, string sql, SqlParameter[] commandParameters)
        {
            DataSet ds = SqlHelper.ExecuteSql(Config.ConnectionString, sql, commandParameters);

            DataTable table = null;

            if (ds.Tables.Count != 0)
            {
                table = ds.Tables[0];

                table.TableName = tableName;
            }

            return table;
        }

        #endregion

        #region Indexer
        public override T this[int index]
        {
            get
            {
                try
                {
                    if (cache.ContainsKey(index))
                    {
                        return cache[index];
                    }

                    T item = new T();

                    item.Cxt = Cxt;

                    if (DataTable != null)
                    {
                        item.SetTableName(DataTable.TableName);

                        if (DataTable.Rows.Count == 0)
                        {
                            item.DataRow = DataTable.NewRow();

                            DataTable.Rows.Add(item.DataRow);
                        }
                        else
                        {
                            item.DataRow = DataTable.Rows[index];
                        }
                    }

                    return item;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            set
            {
                cache[index] = value;
            }
        }

        public override T First
        {
            get { return this[0]; }
        }

        public override T Last
        {
            get { return this[Count - 1]; }
        }

        public override T GetByID(int id)
        {
            return Filter(PrimaryKey + "=" + id).First;
        }

        #endregion

        #region Count
        public override int Count
        {
            get { return DataTable == null ? 0 : DataTable.Rows.Count; }
        }
        #endregion

        #region Filter
        public override TC Filter(string filter)
        {
            DataTable.DefaultView.RowFilter = filter;

            TC items = new TC();

            items.DataTable = DataTable.DefaultView.ToTable(DataTable.TableName);

            items.DataTable.AcceptChanges();

            return items;
        }

        public override bool Contains(string filter)
        {
            return Filter(filter).Count != 0;
        }
        #endregion

        #region Delete

        public static void Delete(RsOneItemTable tableName, int id)
        {
            ExecuteSql(UData.ToDelete(tableName.ToString(), tableName.ToString() + "ID", id));
        }

        public static void Delete(RsOneItemTable tableName, params object[] whereColVals)
        {
            ExecuteSql(UData.ToDelete(tableName.ToString(), whereColVals));
        }

        public static void Delete(string tableName, params object[] whereColVals)
        {
            ExecuteSql(UData.ToDelete(tableName, whereColVals));
        }
        #endregion

        #region Save

        public virtual void Save()
        {
            Save(Config.ConnectionString);
        }

        public virtual void Save(string connectionString)
        {
            SqlTransaction t = null;

            try
            {
                t = SqlHelper.BeginTransaction(connectionString);

                Save(t);

                SqlHelper.CommitTransaction(t);
            }
            catch (Exception ex)
            {
                SqlHelper.RollbackTransaction(t);

                throw ex;
            }
        }

        public virtual void Save(SqlTransaction t)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Save(t);
            }
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return "Count=" + Count;
        }
        #endregion

        #region DataTable Xml
        #region Add
        public virtual void Add(T item)
        {
            if (item == null)
            {
                return;
            }

            if (DataTable == null)
            {
                DataTable = item.DataRow.Table.Clone();
            }

            if (item.DataRow.Table == DataTable)
            {
                DataTable.Rows.Add(item.DataRow);
            }
            else
            {
                DataTable.ImportRow(item.DataRow);
            }
        }

        public virtual void Add(TC items)
        {
            if (items == null)
            {
                return;
            }

            if (DataTable == null)
            {
                DataTable = items.DataTable.Copy();

                return;
            }

            UData.ImportTable(DataTable, items.DataTable);
        }
        #endregion

        #region NewItem
        public virtual T NewItem()
        {
            T item = null;

            if (DataTable != null)
            {
                DataRow row = DataTable.NewRow();

                item = new T();

                item.Cxt = Cxt;

                item.DataRow = row;
            }

            return item;
        }

        #endregion

        #region Delete
        public void Delete(int rowIndex)
        {
            if (DataTable != null)
            {
                if (rowIndex > 0 && rowIndex < DataTable.Rows.Count)
                {
                    DataTable.Rows[rowIndex].Delete();

                    DataTable.AcceptChanges();
                }
            }
        }

        #endregion

        #region Write

        public void WriteXml(string filePath)
        {
            if (DataTable.TableName == "")
            {
                DataTable.TableName = "Table";
            }

            DataTable.WriteXml(filePath);
        }

        public void WriteXsd(string filePath)
        {
            if (DataTable.TableName == "")
            {
                DataTable.TableName = "Table";
            }

            DataTable.WriteXmlSchema(filePath);
        }
        #endregion

        #region Load
        public void LoadXsd(string xsdPath)
        {
            try
            {
                DataTable = new DataTable(UFile.GetFileNameNoExtension(xsdPath));

                DataTable.ReadXmlSchema(xsdPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadXsdText(string xsd)
        {
            try
            {
                DataTable = new DataTable();

                if (!String.IsNullOrEmpty(xsd))
                {
                    MemoryStream s = new MemoryStream(UStr.ToBytes(xsd));

                    s.Position = 0;

                    DataSet ds = new DataSet();

                    ds.ReadXmlSchema(s);

                    DataTable = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadXsdRes(string xsdResourceName)
        {
            try
            {
                LoadXsdText(UWeb.ResText(xsdResourceName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadXml(string xsdPath, string xml)
        {
            try
            {
                LoadXsd(xsdPath);

                if (!String.IsNullOrEmpty(xml))
                { 
                    MemoryStream s = new MemoryStream(UStr.ToBytes(xml));

                    s.Position = 0;

                    DataTable.ReadXml(s);

                    s.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(string xsdPath, string xmlPath)
        {
            try
            {
                LoadXsd(xsdPath);

                if (UFile.Exists(xmlPath))
                {
                    DataTable.ReadXml(xmlPath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #endregion

        #region Copy

        public TC Copy()
        {
            TC items = new TC();

            items.Cxt = this.Cxt;
            items.DataTable = this.DataTable.Copy();

            return items;
        }
        #endregion
    }
}
