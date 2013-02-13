// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Collections;
using System.ComponentModel;

#pragma warning disable
namespace App.Model
{
    public class RsMail : Control, INamingContainer
    {
        #region "Data Members"
        private Mailer mail = new Mailer();
        #endregion

        #region "Properties"

        public string From
        {
            get { return mail.From; }
            set { mail.From = value; }
        }

        public string To
        {
            get { return mail.To; }
            set { mail.To = value; }
        }

        public string Cc
        {
            get { return mail.Cc; }
            set { mail.Cc = value; }
        }

        public string Bcc
        {
            get { return mail.Bcc; }
            set { mail.Bcc = value; }
        }

        public string Subject
        {
            get { return mail.Subject; }
            set { mail.Subject = value; }
        }

        public string Body
        {
            get { return mail.Body; }
            set { mail.Body = value; }
        }

        public MailFormat BodyFormat
        {
            get { return mail.BodyFormat; }
            set { mail.BodyFormat = value; }
        }

        public string SmtpServer
        {
            get { return mail.SmtpServer; }
            set { mail.SmtpServer = value; }
        }

        public string SmtpUserId
        {
            get { return mail.SmtpUserId; }
            set { mail.SmtpUserId = value; }
        }

        public string SmtpPassword
        {
            get { return mail.SmtpPassword; }
            set { mail.SmtpPassword = value; }
        }

        public int SmtpPort
        {
            get { return mail.SmtpPort; }
            set { mail.SmtpPort = value; }
        }

        public bool SmtpUseSsl
        {
            get { return mail.SmtpUseSsl; }
            set { mail.SmtpUseSsl = value; }
        }

        
        public bool SmtpAuthenticate
        {
            get { return mail.SmtpAuthenticate; }
            set { mail.SmtpAuthenticate = value; }
        }

        public MailTemplates Templates
        {
            get { return mail.Templates; }
        }

        public MailMessage MailMessage
        {
            get { return mail.MailMessage; }
        }

        public MailVerifier MailVerifier
        {
            get { return mail.MailVerifier; }
        }

        #endregion

        #region "Events"
        protected override void CreateChildControls()
        {
        }

        protected override void Render(HtmlTextWriter w)
        {
            EnableViewState = false;
            // don't want smtp credentials in viewstate
            EnsureChildControls();
        }

        protected override void OnPreRender(EventArgs e)
        {

        }

        #endregion

        #region "Helpers"

        #endregion

        #region "Public Methods"
        public string GetVerifyResultString(MailVerifyResult result)
        {
            return mail.GetVerifyResultString(result);
        }

        public MailVerifyResult Send()
        {
            return mail.Send("");
        }

        public MailVerifyResult Send(string templateName)
        {
            return mail.Send(templateName);
        }
        #endregion
    }
}
#pragma warning enable