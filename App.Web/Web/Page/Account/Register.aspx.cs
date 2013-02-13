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

public partial class Register : BasePage
{
    #region Properties
    public AccountRequestTypeE RequestType
    {
        get
        {
            return (AccountRequestTypeE)UWeb.QsInt32("rt");
        }
    }

    public string RequestID
    {
        get
        {
            return UWeb.Qs("rid");
        }
    }

    public string UserName
    {
        get
        {
            return UCrypto.FromBase64(RequestID);
        }
    }
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        HandleRequestType();
    }

    protected void HandleRequestType()
    {
        try
        {
            App.Model.User.HandleRequestType(Cxt, RequestType, UserName);
        }
        catch (Exception ex)
        {
            PlaceHolder1.Visible = false;

            ErrorMessage.Text = ex.Message;
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate();

            if (!Page.IsValid)
            {
                return;
            }

            Save();
        }
        catch (Exception ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }

    private void Save()
    {
        User user = new User();

        user.SetColumn("Name", txtName.Text);
        user.SetColumn("UserName", txtEmail.Text);
        user.SetColumn("Password", UCrypto.Encrypt(txtPassword.Text));
        user.Save();

        PlaceHolder1.Visible = false;

        ErrorMessage.Text = Msg.NewAccountCreatedOk(txtEmail.Text);
    }
}
