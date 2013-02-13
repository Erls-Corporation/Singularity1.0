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
    #region enum RoleE
    public enum RoleE
    {
        Unknown = 0,
        Anonymous = 1,
        SiteAdmin = 2,
        ServiceAdmin = 3,
        SiteUser = 4,
        ServiceUser = 5
    }
    #endregion

    public class Role : BaseItem
    {
          #region Constructor
        public Role()
            : base(0)
        {
        }

        public Role(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public Role(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
      
       
        #endregion

        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Role; }
            set { base.TableName = value; }
        }

    }
}
