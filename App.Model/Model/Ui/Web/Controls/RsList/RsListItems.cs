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

#region Res

[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsList.Res.RsListItem.xsd", "text/html")]

#endregion

namespace App.Model
{
    public class RsListItems : BaseItems<RsListItem, RsListItems>
    {
        public string[] DataKeyNames = null;

        public RsListItems()
        { 
        }

        public RsListItems(string[] dataKeyNames)
        {
            DataKeyNames = dataKeyNames;

            LoadXsdRes(UWeb.ResNameCtrl(typeof(RsList), "RsListItem.xsd"));

            UData.AddColumns(base.DataTable, dataKeyNames);
        }

        public bool Contains(int rowIndex)
        {
            return Filter("RowIndex=" + rowIndex).Count > 0;
        }

        public RsListItem Add(int rowIndex)
        {
            RsListItem item = new RsListItem(NewItem().DataRow);

            item.RowIndex = rowIndex;

            return item;
        }

        #region Properties

        public string FirstDataKeyName
        {
            get { return DataKeyNames.Length > 0 ? DataKeyNames.GetValue(0).ToString() : ""; }
        }

        public string LastDataKeyName
        {
            get { return DataKeyNames.Length > 0 ? DataKeyNames.GetValue(DataKeyNames.Length - 1).ToString() : ""; }
        }
        #endregion

    }
}
