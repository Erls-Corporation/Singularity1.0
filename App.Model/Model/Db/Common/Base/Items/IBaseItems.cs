// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace App.Model
{
    public class IBaseItems<T, TC>
     where T : App.Model.BaseItem
    {
        #region Ctor
        public IBaseItems()
        { }

        public IBaseItems(DataTable table)
        { }
        
        #endregion

        public virtual DataTable DataTable { get { return null; } set { } }
        public virtual Cxt Cxt { get { return null; } set { } }

        public virtual bool Contains(string filter) { return false; }
        public virtual TC Filter(string filter) { return default(TC); }
        public virtual int Count { get { return 0; } }

        #region Indexer
        public virtual T this[int index] { get { return null; } set { } }
        public virtual T GetByID(int id) { return null; }
        public virtual T First { get { return null; } }
        public virtual T Last { get { return null; } } 
        #endregion
    }
}
