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

public partial class EmailUc : BaseUc
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl()
    {

    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        SendEmail();
    }

    public void SendEmail()
    {
        try
        {
            lblMsg.Text = "";

            UMail.Send(RsTextArea1.Text, RsTextArea2.Text, RsTextArea3.Text, RsTextBox1.Text, RsRichTextBox1.Text);

            lblMsg.Text = "Email sent successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = AppException.GetError(ex);
        }
    }

}
