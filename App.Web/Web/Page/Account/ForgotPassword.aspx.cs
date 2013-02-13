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

public partial class ForgotPassword : BasePage
{
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (!Page.IsValid)
        {
            return;
        }

        App.Model.User user = App.Model.User.GetUser(Cxt, txtEmail.Text);

        if (user.IsNew)
        {
            ErrorMessage.Text = "Email <b>" + txtEmail.Text + "</b> is not registered in our database.";
        }
        else
        {
            UMail.Send(user, MailTemplateE.ForgotPassword);
            ErrorMessage.Text = "A password remider email is sent at <b>" + txtEmail.Text + "</b>.";
            PlaceHolder2.Visible = false;
        }
    }
}
