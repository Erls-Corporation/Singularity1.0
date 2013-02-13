// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Model
{
    #region enum
    public enum SearchTypeE
    {
        Unknown = 0,
        Quick = 1,
        Advance = 2,
        MyItems = 3,
        Browse = 4,
        Latest = 5
    }

    #endregion

    #region SearchQuery
    public class SearchQuery
    {
        #region Data Members
        public Cxt Cxt = null;
        public string SearchText = "";
        public Dictionary<string, SqlParameter> Params = new Dictionary<string, SqlParameter>();
        public SearchTypeE SearchType = SearchTypeE.Quick;
        public static int PageSize = 10;
       
        private string Filter = "";
        private int paramId = 3;
        private int pageNo = 1;
        private int count = -1;

        public const int LatestCount = 99;
        #endregion

        #region Constructor
        public SearchQuery()
        {
        }
        #endregion

        #region Properties

        public string NextParamId
        {
            get { return "@p" + paramId++; }
        }

        public SqlParameter[] Parameters
        {
            get
            {
                SqlParameter[] p = new SqlParameter[Params.Count];

                Params.Values.CopyTo(p, 0);

                return p;
            }
        }

        public virtual RsOneItemTable TableName
        {
            get
            {
                // 1st try, obtain attribute table from ServiceCategory table
                RsOneItemTable t = Cxt.Category.AttributeTableName;

                // Sometimes CategoryID=0 (e.g. Default Category is assumed to be 0) 
                // So try Service
                if (t == RsOneItemTable.ZDummyAttribute)
                {
                    // 2nd try, obtain attribute table from Service table
                    t = Cxt.Service.AttributeTable.AttributeTableName;
                }

                return t;
            }
        }
        #endregion

        public DataTable ExecuteSql(bool count)
        {
            DataTable table = null;

            switch (SearchType)
            {
                case SearchTypeE.Advance:
                    table = GetAdvanceTable(count);
                    break;

                case SearchTypeE.MyItems:
                    table = GetMyItemsTable(count);
                    break;

                case SearchTypeE.Browse:
                    table = GetBrowseTable(count);
                    break;

                default:
                    table = GetQuickTable(count);
                    break;
            }

            return table;
        }

        #region GetTable

        private DataTable GetAdvanceTable(bool count)
        {
            DataTable table = null;
            string sql = "";
            string where = "";

            if (String.IsNullOrEmpty(Filter))
            {
                return null;
            }

            where += " WHERE StatusID = 1 AND ServiceID = @p1 AND CategoryID = @p2";

            where += " AND ItemID IN (SELECT DISTINCT ItemID FROM " + TableName + " WHERE ServiceID = @p1 AND CategoryID = @p2";

            where += " AND " + BaseItem.TrimOr(Filter) + ")";

            if (count)
            {
                sql = " SELECT COUNT(ItemID) AS ItemCount FROM Item" + where;
            }
            else
            {
                sql = PagedSql(where);
            }

            table = BaseCollection.ExecuteSql(RsOneItemTable.Item, sql, Parameters);

            return table;
        }

        private DataTable GetQuickTable(bool count)
        {
            DataTable table = null;
            string sql = "";
            string where = "";
            RsOneItemTable t = RsOneItemTable.Unknown;

            if (String.IsNullOrEmpty(SearchText))
            {
                if (SearchType != SearchTypeE.Latest)
                {
                    return null;
                }
            }

            string filter = CreateQuickSearchParams(SearchText);

            where += " WHERE StatusID = 1";

            if (Cxt.ServiceID != 0)
            {
                t = TableName;
                where += " AND ServiceID = @p1 AND CategoryID = @p2";
            }
            else
            {
                t = RsOneItemTable.ItemAttribute; // hard coding, will be removed
                where += " AND ServiceID IN (SELECT ServiceID FROM Service WHERE ServiceTypeID IN (1,2,3))";
            }

            where += " AND ItemID IN (SELECT DISTINCT ItemID FROM " + t + " WHERE ";

            if (Cxt.ServiceID != 0)
            {
                where += " ServiceID = @p1 AND CategoryID = @p2";
            }
            else
            {
                where += " 1=1";
            }

            where += filter + ")";

            if (count)
            {
                sql = " SELECT COUNT(ItemID) AS ItemCount FROM Item" + where;
            }
            else
            {
                sql = PagedSql(where);
            }

            table = BaseCollection.ExecuteSql(RsOneItemTable.Item, sql, Parameters);

            return table;
        }

        private DataTable GetMyItemsTable(bool count)
        {
            DataTable table = null;
            string sql = "";
            string where = "";

            where += " WHERE StatusID = 1 AND ServiceID = @p1 AND CategoryID = @p2 AND CreatedBy = @p3";

            if (count)
            {
                sql = " SELECT COUNT(ItemID) AS ItemCount FROM Item" + where;
            }
            else
            {
                sql = PagedSql(where);
            }

            table = BaseCollection.ExecuteSql(RsOneItemTable.Item, sql, Cxt.ServiceID, Cxt.CategoryID, Cxt.User.ID);

            return table;
        }

        private DataTable GetBrowseTable(bool count)
        {
            DataTable table = null;
            string sql = "";
            string where = "";

            where += " WHERE StatusID = 1 AND ServiceID = @p1 AND CategoryID = @p2";

            if (count)
            {
                sql = " SELECT COUNT(ItemID) AS ItemCount FROM Item" + where;
            }
            else
            {
                sql = PagedSql(where);
            }

            table = BaseCollection.ExecuteSql(RsOneItemTable.Item, sql, Cxt.ServiceID, Cxt.CategoryID);

            return table;
        }

        #region Helpers

        private string PagedSql(string filter)
        {
            string sql = "";

            sql += "\nSELECT " + UData.ToSelectCount(SearchType == SearchTypeE.Latest ? LatestCount : 0) + " t.* FROM (";
            sql += "\n SELECT TOP " + PageSize + " ItemID, DateCreated FROM (";
            sql += "\n  SELECT TOP " + PageSize * pageNo + " ItemID, DateCreated";
            sql += "\n  FROM Item";
            sql += "\n  " + filter;
            sql += "\n  ORDER BY DateCreated DESC, ItemID DESC) AS foo";
            sql += "\n ORDER BY DateCreated ASC, ItemID ASC) AS bar";
            sql += "\nINNER JOIN Item AS t ON bar.ItemID = t.ItemID";
            sql += "\nORDER BY bar.DateCreated DESC, bar.ItemID DESC";

            return sql;
        } 
        #endregion

        #endregion

        public void AppendFilter(string filterText)
        {
            if (String.IsNullOrEmpty(filterText))
            {
                return;
            }

            Filter = BaseItem.FilterOr(Filter, filterText);
        }

        public void SetParam(string pid, object v)
        {
            Params.Add(pid, new SqlParameter(pid, v));
        }

        public void NewPage(int newPageIndex, int currentPageIndex)
        {
            pageNo = newPageIndex + 1;
        }

        #region GridViewFiller

        public int Count { get { return count; } }

        public ObjectDataSource GetData()
        {
            ObjectDataSource ods = new ObjectDataSource();

            ods.ID = "ods1";
            ods.EnablePaging = true;
            ods.TypeName = "App.Model.TableAdapter"; 
            ods.SelectMethod = "GetData";
            ods.SelectCountMethod = "VirtualItemCount";
            ods.StartRowIndexParameterName = "startRow";
            ods.MaximumRowsParameterName = "maxRows";
            ods.EnableViewState = false;

            ods.ObjectCreating += new ObjectDataSourceObjectEventHandler(ods_ObjectCreating);
            
            return ods;
        }

        private void ods_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            DataTable table = null;

            if (count < 0)
            {
                SetParam("@p1", Cxt.ServiceID);
                SetParam("@p2", Cxt.CategoryID);

                table = ExecuteSql(true);

                if (table == null || table.Rows.Count == 0)
                {
                    e.ObjectInstance = new TableAdapter(null, 0);

                    return;
                }

                count = BaseItem.GetColInt32(table.Rows[0], "ItemCount");

                if (SearchType == SearchTypeE.Latest)
                {
                    count = (count > LatestCount ? LatestCount : count);
                }
            }

            table = ExecuteSql(false);

            e.ObjectInstance = new TableAdapter(table, count);
        }

        #endregion

        private string CreateQuickSearchParams(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return "";
            }

            bool exactSearch = UStr.InQuotes(text);
            string filter = "";
            ArrayList list = new ArrayList();

            if (exactSearch)
            {
                text = UStr.RemoveQuotes(text);            
            }

            filter += CreateQuickSearchParam(text);
            list.Add(text);

            if (!exactSearch)
            {
                string[] vals = UStr.Split(text, " ");
                foreach (string val in vals)
                {
                    if (!list.Contains(val))
                    {
                        list.Add(val);
                        filter += CreateQuickSearchParam(val);
                    }
                }
            }

            filter = BaseItem.TrimOr(filter);
            filter = " AND (" + filter + ")";

            return filter;
        }

        private string CreateQuickSearchParam(string val)
        {
            string pid = "";

            pid = NextParamId;
            SetParam(pid, UStr.Percent(val));
            return BaseItem.FilterOr(Filter, UStr.FilterParam("Value", pid));
        }

        public static SearchQuery GetFilter(Cxt cxt, string searchText)
        {
            return GetFilter(cxt, SearchTypeE.Quick, searchText);
        }

        public static SearchQuery GetFilter(Cxt cxt, SearchTypeE type, string searchText)
        {
            SearchQuery q = new SearchQuery();

            q.Cxt = cxt;

            q.SearchType = type;

            q.SearchText = searchText;

            return q;
        }

    }
    
    #endregion

    #region TableAdapter
    public class TableAdapter
    {
        private DataTable _dt;
        private int _vic;

        public TableAdapter(DataTable dt, int vic)
        {
            _dt = dt;
            _vic = vic;
        }

        //this returns the datatable (10 records)
        public DataTable GetData()
        {
            return _dt;
        }

        //this returns the total number of records in the table (30.000)
        public int VirtualItemCount()
        {
            return _vic;
        }

        //this also returns the datatable (10 records) but the ODS needs it for paging purposes
        public DataTable GetData(int startRow, int maxRows)
        {
            return _dt;
        }
    } 
    #endregion
}