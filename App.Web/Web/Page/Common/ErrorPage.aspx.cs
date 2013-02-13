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
using App.Model; using Attribute = App.Model.Attribute;

public partial class ErrorPage : BasePage 
{
    public int Code
    {
        [System.Diagnostics.DebuggerStepThrough]
        get { return UWeb.QsInt32("cd"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Code)
        {
            case 404:
                PlaceHolder1.Visible = false;
                PlaceHolder2.Visible = true;
                break;
            default:
                PlaceHolder1.Visible = true;
                PlaceHolder2.Visible = false;
                break;
        }
    }
}
