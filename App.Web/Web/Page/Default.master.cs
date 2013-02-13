// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model; 
using Attribute = App.Model.Attribute;

public partial class Default_master : BaseMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Cxt = new Cxt(ViewState);

        if (!Cxt.Service.IsNew)
        {
            Cxt.Service.Init();
        }

        guc12.InitControl();
        guc22.InitControl();
        fuc1.InitControl();
    }
}
