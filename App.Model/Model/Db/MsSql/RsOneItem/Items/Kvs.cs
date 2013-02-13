// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace App.Model 
{
    public class Kvs : BaseItems<Kv, Kvs>
	{
        private string fileKvXsd = Config.FolderXsd + "Kv.xsd";

        #region Constructor
        public Kvs()
        {
        }

        public string FileKvXsd
        {
            get { return fileKvXsd; }
            set { fileKvXsd = value; }
        }

        public Kvs(Cxt cxt, string xml)
        {
            Cxt = cxt;

            base.LoadXml(FileKvXsd, xml);
        }

        public Kvs(Cxt cxt, string xsdPath, string xmlPath)
        {
            Cxt = cxt;

            base.Load(xsdPath, xmlPath);
        } 
        #endregion

        #region Filter

        #region To
        public DataTable ToTable(string k)
        {
            string sql = ToSql(k);

            return BaseCollection.ExecuteSql(sql);
        }

        public DataTable ToTable(string kDataSource, string kValueField, string kValue)
        {
            string sql = ToSql(kDataSource);

            string filter = UStr.FilterExact(kValueField, kValue);

            sql = UData.ToSql(sql, filter);

            return BaseCollection.ExecuteSql(sql);
        }

        public string ToSql(string k)
        {
            string sql = ToString(k);
            
            int t = ToInt32(k + "Type");

            switch (t)
            {
                case 0: // Sql
                    return sql;
                case 1: // TableName
                    return UData.ToSelectAll(sql);
                default:
                    AppException.Throw("Invalid DataSourceType " + UStr.Bracket(t) + "in Kv");
                    break;
            }

            return "";
        }

        public int ToInt32(string k)
        {
            return BaseItem.ToInt32(ToString(k));
        }

        public bool ToBool(string k)
        {
            return BaseItem.ToBool(ToString(k));
        }

        public string ToString(string k)
        {
            return Get(k).v;
        }
        #endregion

        #region Get

        public Kv Get(string k)
        {
            Kvs items = Filter(UStr.FilterExact("k", k));

            if (items.Count == 0)
            {
                return new Kv();
            }
            else
            {
                return new Kv(Cxt, items.First);
            }
        }

        public Kv Get(int k, string v)
        {
            Kvs items = Filter(UStr.FilterInt32("k", k) + " AND " + UStr.FilterExact("v", v));

            if (items.Count == 0)
            {
                return new Kv();
            }
            else
            {
                return new Kv(Cxt, items.First);
            }
        }
        #endregion
        #endregion

        #region SetProperties
        public void SetProperties(object type)
        {
            for (int i = 0; i < this.Count; i++)
            {
                Kv kv = this[i];

                PropertyInfo pi = type.GetType().GetProperty(kv.k);

                if (pi != null)
                {
                    if (kv.k == "DataSource")
                    {
                        pi.SetValue(type, this.ToTable("DataSource"), null);
                    }
                    else
                    {
                        pi.SetValue(type, BaseItem.ToType(pi.PropertyType, kv.v), null);
                    }
                }
            }
        } 
        #endregion

        public static string ToXml(params object[] kv)
        {
            if (kv == null || kv.Length == 0)
            {
                return "";
            }

            if (kv.Length % 2 != 0)
            {
                throw new Exception("Length of key-value collection should be an even number. Incorrect key-value collection [" + UStr.GetString(kv) + "]");
            }

            StringBuilder s = new StringBuilder();

            s.AppendLine("<DocumentElement xmlns=\"http://tempuri.org/Kv.xsd\">");
           
            for (int i = 0; i < kv.Length; i += 2)
            {
                if (kv.GetValue(i) != null)
                {
                    string val = "";

                    if (kv.GetValue(i + 1) != null)
                    {
                        val = kv.GetValue(i + 1).ToString();
                    }

                    s.AppendLine("<Kv><k>" + kv.GetValue(i).ToString() + "</k>" + "<v>" + val + "</v></Kv>");
                }
            }

            s.AppendLine("</DocumentElement>");

            return s.ToString();
        }
    }
}
