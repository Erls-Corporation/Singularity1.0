// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Model
{
    public class RsListItem : BaseItem
    {
        #region Data

        #endregion

        #region Ctor
        public RsListItem()
        {
        }

        public RsListItem(DataRow row)
        {
            base.DataRow = row;
        } 
        #endregion

        #region Prop
        public int RowIndex { get { return GetColInt32("RowIndex"); } set { SetColumn("RowIndex", value); } }
        
  

        public string ValueOfFirstDataKey
        {
            get { return DataRow[1].ToString(); }
        }
        #endregion

        #region Func
        public string ValueOf(string dataKeyName)
        {
            return GetCol(DataRow, dataKeyName);
        }

        internal void SetDataKeys(DataKey dataKey)
        {
            for (int i = 0; i < dataKey.Values.Keys.Count; i++)
            {
                SetColumn(i + 1, dataKey.Values[i]);
            }
        } 
        #endregion
    }
}
