// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Model;

public partial class TaskUc : BaseUc
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl()
    {

    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        RunTask();
    }

    public void RunTask()
    {
        try
        {
            lblMsg.Text = "";

            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    //ServiceCategory.UpdateStats(Cxt);
                    break;

                case 1:
                    App.Model.User.SendActivationEmail(Cxt);
                    break;

                case 2:
                    Config.Reload();
                    break;
            }


            lblMsg.Text = UStr.Bracket(DropDownList1.SelectedItem.Text) + " completed successfully";

        }
        catch (Exception ex)
        {
            lblMsg.Text = AppException.GetError(ex);
        }
    }

}
