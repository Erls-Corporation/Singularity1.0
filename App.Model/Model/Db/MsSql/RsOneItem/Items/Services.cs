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
    public class Services : BaseItems<Service, Services>
	{
        #region Constructors
        public Services()
        {
        }

        public Services(Cxt cxt)
        {
            Cxt = cxt;
        }

        public Services(Cxt cxt, BaseCollection items)
        {
            Cxt = cxt;

            DataTable = items.DataTable;
        }

        public Services(Cxt cxt, DataTable table)
        {
            Cxt = cxt;

            DataTable = table;
        }

        #endregion

        #region Properties
        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Service; }
            set { base.TableName = value; }
        }
        #endregion
        #endregion
    }
}
