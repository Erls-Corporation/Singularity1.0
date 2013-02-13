// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using RKLib.ExportData;

namespace App.Model
{
    public enum SqlTypeE
    {
        Insert,
        Update,
        Delete,
        Select
    }

    public class UData
    {
        #region ToSql
        public static string ToSelectAll(RsOneItemTable tableName)
        {
            return ToSelectAll(tableName.ToString());
        }

        public static string ToSelectAll(string tableName)
        {
            return "SELECT * FROM [" + tableName + "]";
        }

        public static string ToSelect(RsOneItemTable tableName, params object[] whereColVals)
        {
            return ToSelect(tableName.ToString(), whereColVals);
        }

        public static string ToSelect(string tableName, params object[] whereColVals)
        {
            return ToSql(ToSelectAll(tableName), ToAnd(whereColVals));
        }

        public static string ToDelete(string tableName, params object[] whereColVals)
        {
            return ToSql("DELETE FROM [" + tableName + "]", ToAnd(whereColVals));
        }

        private static string ToAnd(params object[] whereColVals)
        {
            return UStr.Join(" = ", " AND ", whereColVals);
        }

        public static string ToSql(RsOneItemTable tableName, string filter)
        {
            return ToSql(ToSelectAll(tableName), filter);
        }

        public static string ToSql(string sql, string filter)
        {
            if (String.IsNullOrEmpty(sql))
            {
                return "";
            }

            if (!String.IsNullOrEmpty(filter))
            {
                return sql + " WHERE " + filter;
            }

            return sql;
        }


        #endregion

        #region ToInt32

        public static int ToInt32(object value)
        {
            return ToInt32(value, 0);
        }

        public static int ToInt32(object value, object defaultValue)
        {
            return Convert.ToInt32(ToDecimal(value, defaultValue));
        }

        public static decimal ToDecimal(object value, object defaultValue)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
            }

            return ToDecimal(defaultValue, 0);
        }

        public static decimal ToDecimal(object value, decimal defaultValue)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
            }

            return defaultValue;
        }

        public static string ToString(object value, object defaultValue)
        {
            if (value == null)
            {
                return defaultValue.ToString() == null ? "" : defaultValue.ToString();
            }

            return value.ToString();
        }

        #endregion

        #region ToTable
        public static DataTable ToTable(DataRow row)
        {
            DataSet ds = new DataSet();

            DataTable table = row.Table.Clone();

            table.TableName = row.Table.TableName;

            ds.Tables.Add(table);

            table.ImportRow(row);

            return table;
        }

        public static DataTable ToTable(string tableName, params object[] columnNameType)
        {
            DataSet ds = new DataSet();

            DataTable table = ds.Tables.Add(tableName);

            for (int i = 0; i < columnNameType.Length; i += 2)
            {
                string name = columnNameType.GetValue(i) as string;

                Type type = columnNameType.GetValue(i + 1) as Type;

                table.Columns.Add(new DataColumn(name, type));
            }

            return table;
        }

        public static DataTable ToTable2(string tableName, params string[] columnNames)
        {
            DataSet ds = new DataSet();

            DataTable table = ds.Tables.Add(tableName);

            for (int i = 0; i < columnNames.Length; i++)
            {
                string name = columnNames.GetValue(i) as string;

                Type type = System.Type.GetType("System.String");

                table.Columns.Add(new DataColumn(name, type));
            }

            return table;
        }

        public static DataTable AddColumns(DataTable table, string[] columnNames)
        {
            if (columnNames == null)
            {
                return table;
            }

            for (int i = 0; i < columnNames.Length; i++)
            {
                string name = columnNames.GetValue(i) as string;

                Type type = System.Type.GetType("System.String");

                table.Columns.Add(new DataColumn(name, type));
            }

            return table;
        }

        public static DataTable DummyTable(int rowCount, params string[] columns)
        {
            DataTable table = ToTable2("Dummy", columns);

            return AddRows(table, rowCount);
        }

        public static DataTable AddRows(DataTable table, int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = table.NewRow();

                foreach (DataColumn col in table.Columns)
                {
                    row[col] = "xyz" + (i + 1);
                }

                table.Rows.Add(row);
            }

            return table;
        }
        #endregion

        #region Util
        
        
        public static string TableName(string sql)
        {
            int i = sql.IndexOf("from");

            if (i < 0)
            {
                return "";
            }

            i = i + 5;

            int j = sql.IndexOf(" ", i);

            if (j < 0)
            {
                j = sql.Length;
            }

            string tableName = sql.Substring(i, j - i);

            tableName = tableName.TrimStart('[');
            tableName = tableName.TrimEnd(']');

            return tableName.Trim();
        }

        public static String ToText(DataTable table)
        {
            StringBuilder s = new StringBuilder();

            foreach (DataRow row in table.Rows)
            {
                s.Append(row[0].ToString());    
            }

            return s.ToString();
        }

        
        #endregion

        #region Export
        public static string Export(DataTable table)
        {
            return ExportXml(Config.FolderAppBin, table, true, true);
        }

        public static string ExportXml(DataTable table)
        {
            return ExportXml(Config.FolderAppBin, table, true, false);
        }

        public static string ExportXsd(DataTable table)
        {
            return ExportXml(Config.FolderAppBin, table, false, true);
        }

        private static string ExportXml(string folder, DataTable table, bool writeXml, bool writeXsd)
        {
            string path = folder;
            string xml = "";
            string xsd = "";

            if (table == null)
            {
                return "";
            }

            if (table.TableName == "")
            {
                table.TableName = "Table";
            }

            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }

            if (writeXsd)
            {
                xsd = path + table.TableName + ".xsd";
                table.WriteXmlSchema(xsd);
            }

            if (writeXml)
            {
                xml = path + table.TableName + ".xml";
                table.WriteXml(xml);
                path = xml;
            }
            else
            {
                path = xsd;
            }

            return path;
        }

        public static string ExportCsv(DataTable table)
        {
            if (table == null)
            {
                return "";
            }

            if (table.TableName == "")
            {
                table.TableName = "ExportedData";
            }

            string fileName = table.TableName.Replace(" ", "") + ".csv";

            RKLib.ExportData.Export x = new RKLib.ExportData.Export("Win");

            x.ExportDetails(table, RKLib.ExportData.Export.ExportFormat.CSV, fileName);

            return Config.FolderAppBin + fileName;
        }
        #endregion

        #region ToArray
        public static Array ToArray(DataTable table, string columnName)
        {
            return ToArray(table, columnName, System.Type.GetType("System.String"));
        }

        public static Array ToArray(DataTable table, string columnName, System.Type type)
        {
            ArrayList list = new ArrayList();

            foreach (DataRow row in table.Rows)
            {
                list.Add(row[columnName].ToString());
            }

            return list.ToArray(type);
        }

        #endregion

        #region ImportTable

        public static DataTable ImportTable(DataTable destination, DataTable source)
        {
            destination.Merge(source);

            return destination;
        } 
        #endregion

        public static string ExportSql(DataTable table, SqlTypeE type)
        {
            StringBuilder s = new StringBuilder();

            DataTable t = table.Copy();

            t.TableName = table.TableName;

            foreach (DataRow row in t.Rows)
            {
                switch (type)
                {
                    case SqlTypeE.Insert:
                        row.SetAdded();
                        break;
                    case SqlTypeE.Update:
                        row.SetModified();
                        break;
                    case SqlTypeE.Delete:
                        row.Delete();
                        break;
                    default:
                        break;
                }

                s.AppendLine(SqlHelper.ToSql(row));
            }

            string filePath = Config.FolderAppBin + t.TableName + ".sql";

            UFile.Write(filePath, s.ToString());

            return filePath;
        }

        public static bool Contains(DataTable table, string columnName, string value)
        {
            table.DefaultView.RowFilter = UStr.FilterExact(columnName, value);

            bool contains = table.DefaultView.Count != 0;

            table.DefaultView.RowFilter = "";

            return contains;
        }

        public static string ToSelectCount(int selectCount)
        {
            return " TOP " + (selectCount <= 0 ? " 100 PERCENT " : selectCount.ToString()) + " ";
        }
    }
}
