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
    public class Kv : BaseItem
	{
        #region Data Members
        #endregion

        #region Constructor
        public Kv()
        {
        }

        public Kv(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
        #endregion

        #region Properties

        public string k { get { return GetCol("k"); } set { SetColumn("k", value); } }
        public string v { get { return GetCol("v"); } set { SetColumn("v", value); } }

        #endregion

        #region ToString
        public override string ToString()
        {
            return k + "|" + v;
        }
        #endregion
    }
}
