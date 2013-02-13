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
    public class ServiceCategories : BaseItems<ServiceCategory, ServiceCategories>
	{
        #region Constructors
        public ServiceCategories()
        {
        }

        public ServiceCategories(Cxt cxt, BaseCollection items)
        {
            Cxt = cxt;

            DataTable = items.DataTable;
        }

        public ServiceCategories(Cxt cxt, DataTable table)
        {
            Cxt = cxt;

            DataTable = table;
        }

        public ServiceCategories(Cxt cxt, int serviceID, int categoryID)
        {
            Cxt = cxt;

            Load(serviceID, categoryID);
        }

        public ServiceCategories(Cxt cxt, int serviceID, int categoryID, StatusE status)
        {
            Cxt = cxt;

            Load(serviceID, categoryID, status);
        }
        
        #endregion

        #region Properties
        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.ServiceCategory; }
            set { base.TableName = value; }
        }
        #endregion
        #endregion

        #region Load
        private void Load(int serviceID, int categoryID)
        {
            if (serviceID != 0 && categoryID == 0)
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, "ServiceID", serviceID);
            }
            else if (serviceID == 0 && categoryID != 0)
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, "CategoryID", categoryID);
            }
            else if (serviceID == 0 && categoryID == 0)
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, null);
            }
            else
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, "ServiceID", serviceID, "CategoryID", categoryID);
            }
        }

        private void Load(int serviceID, int categoryID, StatusE status)
        {
            if (serviceID != 0 && categoryID == 0)
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, "ServiceID", serviceID, "StatusID", status.ToString("d"));
            }
            else if (serviceID == 0 && categoryID != 0)
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, "CategoryID", categoryID, "StatusID", status.ToString("d"));
            }
            else if (serviceID == 0 && categoryID == 0)
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, "StatusID", status.ToString("d"));
            }
            else
            {
                base.DataTable = BaseCollection.Select(RsOneItemTable.ServiceCategory, "ServiceID", serviceID, "CategoryID", categoryID, "StatusID", status.ToString("d"));
            }
        }

        #endregion

    }
}
