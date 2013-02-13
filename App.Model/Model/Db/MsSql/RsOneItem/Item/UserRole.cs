// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;

namespace App.Model 
{
    public class UserRole : BaseItem
	{
        #region Constructor
        public UserRole()
            : base(0)
        {
        }

        public UserRole(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public UserRole(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
        #endregion

        #region Properties

        #region Core

        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.UserRole; }
            set { base.TableName = value; }
        }

        #endregion

        #region Table Columns

        public int UserID { get { return GetColInt32("UserID"); } set { SetColumn("UserID", value); } }
        public int RoleID { get { return GetColInt32("CategoryID"); } set { SetColumn("RoleID", value); } }

        #endregion
        #endregion
    }
}
