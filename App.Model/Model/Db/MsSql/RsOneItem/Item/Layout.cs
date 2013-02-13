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
    public enum LayoutE
    {
        Unknown = 0,
        Search = 1,
        SearchResult = 2,
        Edit = 3,
        View = 4
    } 

    public class Layout : BaseItem
	{
        #region Constructor
        public Layout()
            : base(0)
        {
        }

        public Layout(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public Layout(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
        #endregion
    }
}
