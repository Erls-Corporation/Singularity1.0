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
    public class Categories : BaseItems<Category, Categories>
    {
        #region Constructors
        public Categories()
        {
        }

        public Categories(Cxt cxt, bool sysCategories)
        {
            Cxt = cxt;

            base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Category, "SELECT * FROM Category WHERE Code IS NOT NULL", null);
        }

        public Categories(Cxt cxt, BaseCollection items)
            : this(cxt, items.DataTable)
        {
        }

        public Categories(Cxt cxt, DataTable table)
        {
            Cxt = cxt;

            DataTable = table;
        }

        public Categories(Cxt cxt, int serviceID)
            : this(cxt, serviceID, StatusE.Active, 0)
        {
        }

        public Categories(Cxt cxt, int serviceID, StatusE status)
            : this(cxt, serviceID, status, 0)
        {
        }

        public Categories(Cxt cxt, int serviceID, StatusE status, int selectCount)
            : this(cxt, serviceID, status, 0, false)
        {
        }

        public Categories(Cxt cxt, int serviceID, StatusE status, int selectCount, bool includeParentServiceCategory)
        {
            Cxt = cxt;

            Load(serviceID, status, selectCount, includeParentServiceCategory);
        }

        #endregion

        #region Properties
        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Category; }
            set { base.TableName = value; }
        }
        #endregion
        #endregion

        #region Load

        private void Load(int serviceID, StatusE status, int selectCount, bool includeParentServiceCategory)
        {
            string sql = "";

            if (serviceID == 0)
            {
                sql = "SELECT  " + UData.ToSelectCount(selectCount) + " Category.* ";
                sql += "\nFROM         Category ORDER BY [Name]";

                base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Category, sql, null);
            }
            else
            {
                if (includeParentServiceCategory)
                {
                    sql += "SELECT" + UData.ToSelectCount(selectCount) + " ServiceCategory.ServiceID, Category.*";
                    sql += "\nFROM         ServiceCategory INNER JOIN";
                    sql += "\n                      Category ON ServiceCategory.CategoryID = Category.CategoryID";
                    sql += "\nWHERE ServiceCategory.StatusID = @p2 AND (ServiceCategory.ServiceID = @p1) OR";
                    sql += "\n                      (ServiceCategory.ServiceID IN";
                    sql += "\n                          (SELECT     ParentServiceID";
                    sql += "\n                            FROM          Service";
                    sql += "\n                            WHERE StatusID = 1 AND (ServiceID = @p1)))";
                }
                else
                {
                    sql = "SELECT " + UData.ToSelectCount(selectCount) + " Category.* ";
                    sql += "\nFROM         Category INNER JOIN";
                    sql += "\nCategory ON Category.CategoryID = Category.CategoryID";
                    sql += "\nWHERE     (Category.ServiceID = @p1) AND (Category.StatusID = @p2) ORDER BY Sequence";
                }

                base.DataTable = BaseCollection.ExecuteSql(RsOneItemTable.Category, sql, serviceID, status.ToString("d"));
            }
        }

        #endregion
    }
}
