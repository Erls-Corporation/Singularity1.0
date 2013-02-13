// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.ComponentModel;

#pragma warning disable
namespace App.Model
{
    #region "SubscribeStatus"
    public enum SubscribeStatus
    {
        SubscribeEmailSent = 1,//Subscribe confirmation email sent

        SubscribeConfirmed = 2,// 2=Subscribe confirmed

        UnsubscribeEmailSent = 3,// 3=Unsubscribe confirmation email sent 

        UnsubscribeConfirmed = 4,// 4=Unsubscribe confirmed

        AlreadySubscribed = 5, // 5=Already subscribed

        EmailDoesNotExists = 6,//6=Email does not exists
        Failed = 7
    }

    #endregion

    #region "RsDoubleOpt"
    public class RsDoubleOpt : Control, INamingContainer
    {
        #region "Data Members"
        private Label Label1 = new Label();
        private TextBox TextBox1 = new TextBox();
        private RequiredFieldValidator RequiredFieldValidator1 = new RequiredFieldValidator();
        private RegularExpressionValidator RegularExpressionValidator1 = new RegularExpressionValidator();
        private LinkButton LinkButton1 = new LinkButton();
        private LinkButton LinkButton2 = new LinkButton();
        private RsMail RsMail1 = new RsMail();
        private Label Label2 = new Label();

        public delegate void StatusUpdateEventHandler(object sender, SubscribeStatusEventArgs e);
        public event EventHandler SubscribeClick;
        public event EventHandler UnsubscribeClick;
        public event StatusUpdateEventHandler StatusUpdate;

        private const string m_errorMessage = "Please provide a valid email address.";
        #endregion

        #region "Properties"

        public string LabelText
        {
            get
            {
                EnsureChildControls();
                return Label1.Text;
            }
            set
            {
                EnsureChildControls();
                Label1.Text = value;
            }
        }

        public string EmailText
        {
            get
            {
                EnsureChildControls();
                return TextBox1.Text;
            }
            set
            {
                EnsureChildControls();
                TextBox1.Text = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                EnsureChildControls();
                return RequiredFieldValidator1.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                RequiredFieldValidator1.ErrorMessage = value;
                RegularExpressionValidator1.ErrorMessage = value;
            }
        }

        public string ValidationExpression
        {
            get
            {
                EnsureChildControls();
                return RegularExpressionValidator1.ValidationExpression;
            }
            set
            {
                EnsureChildControls();
                RegularExpressionValidator1.ValidationExpression = value;
            }
        }

        public bool EmailRequired
        {
            get
            {
                EnsureChildControls();
                return RequiredFieldValidator1.Enabled;
            }
            set
            {
                EnsureChildControls();
                RequiredFieldValidator1.Enabled = value;
            }
        }

        private string m_updateSubscribeSpName = "EmailList_Insert";
        public string UpdateSubscribeSpName
        {
            get { return m_updateSubscribeSpName; }
            set { m_updateSubscribeSpName = value; }
        }

        private string m_connectionStringKey = "";
        [Description("Provide connection string key in web.config under <appSettings> tag.")]
        public string ConnectionStringKey
        {
            get { return m_connectionStringKey; }
            set { m_connectionStringKey = value; }
        }

        [Browsable(false)]
        public RsMail Mail
        {
            get
            {
                EnsureChildControls();
                return RsMail1;
            }
        }

        private string m_subscribeTemplateName;
        public string SubscribeTemplateName
        {
            get { return m_subscribeTemplateName; }
            set { m_subscribeTemplateName = value; }
        }

        private string m_unsubscribeTemplateName;
        public string UnsubscribeTemplateName
        {
            get { return m_unsubscribeTemplateName; }
            set { m_unsubscribeTemplateName = value; }
        }

        private string m_urlConfirmation;
        [Description("Page to redirect after subscribe or unsubscribe button is clicked.")]
        public string UrlConfirmation
        {
            get { return m_urlConfirmation; }
            set { m_urlConfirmation = value; }
        }

        private string m_urlHome;
        [Description("Home page of wesite that is sending subscribe or unsubscribe email.")]
        public string UrlHome
        {
            get { return m_urlHome; }
            set { m_urlHome = value; }
        }

        private string m_senderName;
        [Description("Company or person name who is sending subscribe or unsubscribe email.")]
        public string SenderName
        {
            get { return m_senderName; }
            set { m_senderName = value; }
        }

        private bool m_confirmationMode;
        [Description("True if control is placed on a confirmation page.")]
        public bool ConfirmationMode
        {
            get { return m_confirmationMode; }
            set { m_confirmationMode = value; }
        }

        public string RequestId
        {
            get
            {
                if (HttpContext.Current.Request["rid"] == null)
                {
                    return null;
                }

                return Decrypt(HttpContext.Current.Request["rid"].ToString());
            }
        }

        public bool IsSubscribeRequest
        {
            get
            {
                if (HttpContext.Current.Request["t"] == null)
                {
                    return true;
                }

                return HttpContext.Current.Request["t"].ToString().ToLower() == "s";
            }
        }

        private bool m_confirmSubscribe;
        [Description("True if confirmation email is to be sent on subscribe.")]
        public bool ConfirmSubscribe
        {
            get { return m_confirmSubscribe; }
            set { m_confirmSubscribe = value; }
        }

        private bool m_confirmUnsubscribe;
        [Description("True if confirmation email is to be sent on unsubscribe.")]
        public bool ConfirmUnsubscribe
        {
            get { return m_confirmUnsubscribe; }
            set { m_confirmUnsubscribe = value; }
        }

        #endregion

        #region "Events"
        protected override void CreateChildControls()
        {
            if (ConfirmationMode)
            {
                CreateConfirmationControls();
            }
            else
            {
                CreateNormalControls();
            }
        }

        private void CreateNormalControls()
        {
            Label1.Text = "Email: ";
            Label1.ID = "Label1";
            Controls.Add(Label1);

            TextBox1.ID = "TextBox1";
            Controls.Add(TextBox1);

            RequiredFieldValidator1.ID = "RequiredFieldValidator1";
            RequiredFieldValidator1.ControlToValidate = "TextBox1";
            RequiredFieldValidator1.ErrorMessage = m_errorMessage;
            RequiredFieldValidator1.Display = ValidatorDisplay.Dynamic;
            Controls.Add(RequiredFieldValidator1);

            RegularExpressionValidator1.ID = "RegularExpressionValidator1";
            RegularExpressionValidator1.ControlToValidate = "TextBox1";
            RegularExpressionValidator1.ErrorMessage = m_errorMessage;
            RegularExpressionValidator1.Display = ValidatorDisplay.Dynamic;
            RegularExpressionValidator1.ValidationExpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            Controls.Add(RegularExpressionValidator1);

            LinkButton1.ID = "LinkButton1";
            LinkButton1.Text = "Subscribe";
            Controls.Add(LinkButton1);
            LinkButton1.Click += OnLinkButton1Click;

            LinkButton2.ID = "LinkButton2";
            LinkButton2.Text = "Unsubscribe";
            Controls.Add(LinkButton2);
            LinkButton2.Click += OnLinkButton2Click;

            RsMail1.ID = "RsMail1";
            RsMail1.BodyFormat = System.Web.Mail.MailFormat.Html;
            MailTemplate t = RsMail1.Templates.Add("Default", "Please activate your %subscribed% at %sendername%", "<html><head></head><body>Please click link below to confirm your %subscribed% at %sendername%.<p/><a href='%link%'>%link%</a><p/><b>%sendername% Staff</b><br/>%urlhome%</body></html>");
            RsMail1.MailVerifier.MailVerifyLevel = MailVerifyLevel.None;
            Controls.Add(RsMail1);

            Label2.ForeColor = System.Drawing.Color.Red;
            Label2.ID = "Label2";
            Label2.Text = "";
            Controls.Add(Label2);
        }

        private void CreateConfirmationControls()
        {
            Label2.ForeColor = System.Drawing.Color.Red;
            Label2.ID = "Label2";
            Controls.Add(Label2);

            EmailText = RequestId;
        }

        protected override void Render(HtmlTextWriter w)
        {
            EnsureChildControls();

            if (ConfirmationMode)
            {
                RenderConfirmation(w);
            }
            else
            {
                RenderNormal(w);
            }
        }

        private void RenderNormal(HtmlTextWriter w)
        {
            w.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1", false);
            w.RenderBeginTag(HtmlTextWriterTag.Table);

            // ROW # 1
            w.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
            w.RenderBeginTag(HtmlTextWriterTag.Tr);
            w.AddAttribute(HtmlTextWriterAttribute.Align, "right");
            w.RenderBeginTag(HtmlTextWriterTag.Td);
            Label1.RenderControl(w);
            w.RenderEndTag();
            //</td>
            w.RenderBeginTag(HtmlTextWriterTag.Td);
            TextBox1.RenderControl(w);
            Add(w, "<br/>");
            LinkButton1.RenderControl(w);
            Add(w, "&nbsp;&nbsp;");
            LinkButton2.RenderControl(w);
            w.RenderEndTag();
            //</td>
            w.RenderBeginTag(HtmlTextWriterTag.Td);
            Add(w, "&nbsp;&nbsp;");
            w.RenderEndTag();
            //</td>
            w.RenderBeginTag(HtmlTextWriterTag.Td);
            RegularExpressionValidator1.RenderControl(w);
            RequiredFieldValidator1.RenderControl(w);
            Label2.RenderControl(w);
            w.RenderEndTag();
            //</td>
            w.RenderEndTag();
            //</tr>
            //</table>
            w.RenderEndTag();
        }

        private void RenderConfirmation(HtmlTextWriter w)
        {
            UpdateSubscribe((IsSubscribeRequest ? SubscribeStatus.SubscribeConfirmed : SubscribeStatus.UnsubscribeConfirmed));

            Label2.RenderControl(w);
        }

        protected void OnLinkButton1Click(object sender, EventArgs e)
        {
            Label2.Text = "";

            UpdateSubscribe((ConfirmSubscribe ? SubscribeStatus.SubscribeEmailSent : SubscribeStatus.SubscribeConfirmed));

            if (SubscribeClick != null)
            {
                SubscribeClick(this, e);
            }

            TextBox1.Text = "";
        }

        protected void OnLinkButton2Click(object sender, EventArgs e)
        {
            Label2.Text = "";

            UpdateSubscribe((ConfirmUnsubscribe ? SubscribeStatus.UnsubscribeEmailSent : SubscribeStatus.UnsubscribeConfirmed));

            if (UnsubscribeClick != null)
            {
                UnsubscribeClick(this, e);
            }

            TextBox1.Text = "";
        }
        #endregion

        #region "Public Methods"
        public void ShowMessage(string message)
        {
            Label2.Text = message;
        }
        #endregion

        #region "Helpers"

        #region "Add"
        private void Add(HtmlTextWriter w, string html)
        {
            LiteralControl lit = new LiteralControl(html);

            lit.RenderControl(w);
        }

        private void Add(string html)
        {
            Controls.Add(new LiteralControl(html));
        }
        #endregion

        #region "Update Subscribe"
        private void UpdateSubscribe(SubscribeStatus subscribeStatus)
        {
            try
            {
                SubscribeStatus status = UpdateDb(subscribeStatus);

                if (subscribeStatus == status)
                {
                    SendMail(subscribeStatus);
                }

                OnStatusUpdate(status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SubscribeStatus UpdateDb(SubscribeStatus subscribeStatus__1)
        {
            bool strict = true;

            switch (subscribeStatus__1)
            {
                case SubscribeStatus.SubscribeEmailSent:
                case SubscribeStatus.SubscribeConfirmed:
                    strict = ConfirmSubscribe;
                    break;
                case SubscribeStatus.UnsubscribeEmailSent:
                case SubscribeStatus.UnsubscribeConfirmed:
                    strict = ConfirmUnsubscribe;
                    break;
                default:
                    return subscribeStatus__1;
                // No need to updated database for any other status
            }

            DataSet ds = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationSettings.AppSettings[ConnectionStringKey], UpdateSubscribeSpName, EmailText, subscribeStatus__1);

            SubscribeStatus status = (SubscribeStatus)Convert.ToInt32(ds.Tables[0].Rows[0]["SubscribeStatus"]);

            return status;
        }

        private void SendMail(SubscribeStatus subscribeStatus__1)
        {
            MailTemplate t = null;
            string type = "";

            #region "Select Template"
            if (SubscribeTemplateName == null || string.IsNullOrEmpty(SubscribeTemplateName))
            {
                t = RsMail1.Templates["Default"];
            }
            else
            {
                switch (subscribeStatus__1)
                {
                    case SubscribeStatus.SubscribeEmailSent:
                        t = RsMail1.Templates[SubscribeTemplateName];
                        break;
                    case SubscribeStatus.UnsubscribeEmailSent:
                        t = RsMail1.Templates[UnsubscribeTemplateName];
                        break;
                    default:
                        return;
                    // This function does not work on any other status
                }
            }

            #endregion

            //#Region "Set Keywords"
            switch (subscribeStatus__1)
            {
                case SubscribeStatus.SubscribeEmailSent:
                    t.Add("%subscribed%", "Subscribe");
                    type = "s";


                    break;
                case SubscribeStatus.UnsubscribeEmailSent:
                    t.Add("%subscribed%", "Unsubscribe");
                    type = "u";


                    break;
                default:
                    return;
                // This function does not work on any other status
            }

            string url = GetConfirmationUrl(UrlConfirmation, type, EmailText);

            t.Add("%link%", url);
            t.Add("%urlhome%", UrlHome);
            t.Add("%sendername%", SenderName);
            t.Add("%unsubscribelink%", GetConfirmationUrl(UrlConfirmation, "u", EmailText));
            //#End Region

            //#Region "Send Email"
            RsMail1.To = EmailText;

            MailVerifyResult result = RsMail1.Send(t.Name);
            //#End Region

            //#Region "Check Send Result"
            switch (result)
            {
                case MailVerifyResult.Ok:
                    ShowMessage("We have sent you a confirmation email at " + EmailText);
                    break;
                default:
                    ShowMessage(RsMail1.MailVerifier.GetVerifyResultString(result));
                    break;
                //#End Region
            }
        }

        private static string GetConfirmationUrl(string urlConfirm, string type, string email)
        {
            return ((urlConfirm + "?t=") + type + "&rid=") + Encrypt(email);
        }

        private void OnStatusUpdate(SubscribeStatus status)
        {
            SubscribeStatusEventArgs e = new SubscribeStatusEventArgs();
            e.NewStatus = status;
            if (StatusUpdate != null)
            {
                StatusUpdate(this, e);
            }
        }

        public void ShowStatusMessage(SubscribeStatus status)
        {
            switch (status)
            {
                case SubscribeStatus.SubscribeEmailSent:
                case SubscribeStatus.SubscribeConfirmed:
                case SubscribeStatus.UnsubscribeEmailSent:
                case SubscribeStatus.UnsubscribeConfirmed:


                    break;
                case SubscribeStatus.AlreadySubscribed:
                    ShowMessage(EmailText + " is already subscribed.");


                    break;
                case SubscribeStatus.EmailDoesNotExists:
                    ShowMessage("We do not have your email in our database.");


                    break;
                default:


                    break;
            }
        }
        #endregion

        #region "Crypto"
        private static string Encrypt(string data)
        {
            try
            {
                return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(data));
            }
            catch (Exception e)
            {
                throw new Exception("Error in Encrypt" + e.Message);
            }
        }

        private static string Decrypt(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new string(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Decrypt" + e.Message);
            }
        }
        #endregion
        #endregion
    }
    #endregion

    #region "SubscribeStatusEventArgs"
    public class SubscribeStatusEventArgs : EventArgs
    {
        private SubscribeStatus m_newStatus;
        public SubscribeStatus NewStatus
        {
            get { return m_newStatus; }
            set { m_newStatus = value; }
        }
    }
    #endregion
}
#pragma warning enable