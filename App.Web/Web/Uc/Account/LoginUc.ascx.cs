// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model; using Attribute = App.Model.Attribute;
using System.Diagnostics;

public partial class LoginUc : BaseUc
{
    public Login Login1 { [DebuggerStepThrough] get { return lv1.FindControl("Login1") as Login; } }
    public TextBox UserName { [DebuggerStepThrough] get { return lv1.FindControl("Login1").FindControl("UserName") as TextBox; } }
    public TextBox Password { [DebuggerStepThrough] get { return lv1.FindControl("Login1").FindControl("Password") as TextBox; } }
    public Label FailureText { [DebuggerStepThrough] get { return lv1.FindControl("Login1").FindControl("FailureText") as Label; } }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Logout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();

        Redirect("Default.aspx");
    }
    
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            e.Authenticated = User.Login(Cxt, UserName.Text, Password.Text);
        }
        catch (Exception ex)
        {
            Login1.FailureText = ex.Message;
        }
    }

    protected void Register_Click(object sender, EventArgs e)
    {
        Redirect("~/Web/Page/Account/Register.aspx");
    }

    protected void ForgotPassword_Click(object sender, EventArgs e)
    {
        Redirect("~/Web/Page/Account/ForgotPassword.aspx");
    }

    protected void Login1_LoginError(object sender, EventArgs e)
    {
       
    }
}
