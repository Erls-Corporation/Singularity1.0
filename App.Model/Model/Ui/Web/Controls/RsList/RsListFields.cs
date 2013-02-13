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

[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsList.Res.RsListField.xsd", "text/html")]

#endregion

namespace App.Model
{

    public class RsListFields : BaseItems<RsListField, RsListFields>
    {
        public RsListFields()
        {
            LoadXsdRes(UWeb.ResNameCtrl(typeof(RsList), "RsListField.xsd"));
        }

        public RsListFields(DataTable table)
        {
            base.DataTable = table;
        }
    }
}
