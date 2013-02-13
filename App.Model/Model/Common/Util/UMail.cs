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
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

#pragma warning disable

namespace App.Model
{
    #region enums
    public enum MailTemplateE
    {
        ActivateAccount,
        ForgotPassword
    } 
    #endregion

    #region UMail
    public class UMail
    {
        public static Mailer Mailer;

        static UMail()
        {
            ItemAttributes ia = Config.DefaultService.ConfigAttributes;

            Mailer = new Mailer();

            Mailer.Templates.Add(MailTemplateE.ActivateAccount.ToString(), ia.ToString(AttributeE.ServiceTemplateEmailSubjectActivateAccount), ia.ToString(AttributeE.ServiceTemplateEmailBodyActivateAccount));
            Mailer.Templates.Add(MailTemplateE.ForgotPassword.ToString(), ia.ToString(AttributeE.ServiceTemplateEmailSubjectForgotPassword), ia.ToString(AttributeE.ServiceTemplateEmailBodyForgotPassword));

            Mailer.SmtpServer = ia.ToString(AttributeE.ServiceSmtpServer);
            Mailer.SmtpPort = ia.ToInt32(AttributeE.ServiceSmtpPort);
            Mailer.SmtpAuthenticate = ia.ToBool(AttributeE.ServiceSmtpAuthenticate);
            Mailer.SmtpUseSsl = ia.ToBool(AttributeE.ServiceSmtpUseSsl);
            Mailer.SmtpUserId = ia.ToString(AttributeE.ServiceSmtpUserId);
            Mailer.SmtpPassword = ia.ToString(AttributeE.ServiceSmtpPassword);
            Mailer.From = ia.ToString(AttributeE.ServiceSmtpFrom);
        }

        public static MailVerifyResult Send(User user, MailTemplateE template)
        {
            MailTemplate t = Mailer.Templates[template.ToString()];

            t.Clear();

            t.Add("%username%", user.UserName);
            t.Add("%password%", user.Password);
            t.Add("%name%", user.Name);

            if (template == MailTemplateE.ActivateAccount)
            {
                t.Add("%link%", UWeb.GetUrlActivateAccount(user.UserName));
            }

            Mailer.To = user.UserName;

            return Mailer.Send(template.ToString());
        }

        public static MailVerifyResult Send(string to, string cc, string bcc, string subject, string body)
        {
            Mailer.To = to;
            Mailer.Cc = cc;
            Mailer.Bcc = bcc;
            Mailer.Subject = subject;
            Mailer.Body = body;

            return Mailer.Send("");
        }

    } // UMail


    #endregion

    #region Mailer
    public class Mailer
    {
        #region Data Members
        private MailMessage mail = new MailMessage();
        private MailTemplates m_templates = new MailTemplates();
        private MailVerifier verifier = new MailVerifier();
        #endregion

        #region Properties

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

        private string m_smtpServer = "";
        public string SmtpServer
        {
            get { return m_smtpServer; }
            set { m_smtpServer = value; }
        }

        private string m_smtpUserId = "";
        public string SmtpUserId
        {
            get { return m_smtpUserId; }
            set { m_smtpUserId = value; }
        }

        private string m_smtpPassword = "";
        public string SmtpPassword
        {
            get { return m_smtpPassword; }
            set { m_smtpPassword = value; }
        }

        private int m_smtpPort;
        public int SmtpPort
        {
            get { return m_smtpPort; }
            set { m_smtpPort = value; }
        }

        private bool m_smtpUseSsl;
        public bool SmtpUseSsl
        {
            get { return m_smtpUseSsl; }
            set { m_smtpUseSsl = value; }
        }

        private bool m_smtpAuthenticate;
        public bool SmtpAuthenticate
        {
            get { return m_smtpAuthenticate; }
            set { m_smtpAuthenticate = value; }
        }

        public MailTemplates Templates
        {
            get { return m_templates; }
        }

        public MailMessage MailMessage
        {
            get { return mail; }
        }

        public MailVerifier MailVerifier
        {
            get { return verifier; }
        }

        #endregion

        #region Events

        #endregion

        #region Helpers

        #endregion

        #region Public Methods
        public string GetVerifyResultString(MailVerifyResult result)
        {
            return verifier.GetVerifyResultString(result);
        }

        public MailVerifyResult Send()
        {
            return Send("");
        }

        public MailVerifyResult Send(string templateName)
        {
            try
            {
                MailVerifyResult result = verifier.Verify(this);

                if (result != MailVerifyResult.Ok)
                {
                    return result;
                }

                string subject__1 = Subject;
                string body__2 = Body;

                if (!string.IsNullOrEmpty(templateName))
                {
                    Subject = Templates[templateName].Subject;
                    Body = Templates[templateName].Body;
                }

                if (SmtpPort == 0)
                {
                    SmtpPort = 25;
                }

                mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = SmtpServer;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = (SmtpAuthenticate ? 1 : 0);
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = SmtpUserId;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = SmtpPassword;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = SmtpPort;
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"] = SmtpUseSsl.ToString().ToLower();
                mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;

                mail.Priority = MailPriority.Normal;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.BodyFormat = MailFormat.Html;

                SmtpMail.SmtpServer = (SmtpServer + ":") + SmtpPort.ToString();
                SmtpMail.Send(mail);

                Subject = subject__1;
                Body = body__2;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return MailVerifyResult.Ok;
        }

        #endregion
    }

    #endregion

    #region MailTemplate
    public class MailTemplates
    {
        private Hashtable templates = new Hashtable();

        #region Public Methods
        public MailTemplate Add(string name, string subject, string body)
        {
            if (templates.Contains(name))
            {
                return null;
            }

            MailTemplate t = new MailTemplate(name, subject, body);

            templates.Add(name, t);

            return t;
        }

        public void Clear()
        {
            templates.Clear();
        }

        public MailTemplate this[string name]
        {
            get { return (MailTemplate)templates[name]; }
        }
        #endregion
    }


    public class MailTemplate
    {
        private Hashtable keywords = new Hashtable();
        private string m_name = "";
        private string m_subject = "";
        private string m_body = "";

        public string Name
        {
            get { return m_name; }
        }

        public string Subject
        {
            get { return Apply(m_subject); }
        }

        public string Body
        {
            get { return Apply(m_body); }
        }

        public MailTemplate(string name, string subject, string body)
        {
            this.m_name = name;
            this.m_subject = subject;
            this.m_body = body;
        }

        #region Public Methods
        public void Add(string keyword, string replacement)
        {
            if (!keywords.Contains(keywords))
            {
                keywords.Add(keyword, replacement);
            }
        }

        public string Apply(string text)
        {
            foreach (string key in keywords.Keys)
            {
                text = text.Replace(key, keywords[key].ToString());
            }

            return text;
        }

        public void Clear()
        {
            keywords.Clear();
        }
        #endregion
    }

    #endregion

    #region MailVerifier

    #region MailVerifyResult
    public enum MailVerifyResult
    {
        Ok,
        Failed,
        ConnectFailed, // Connection to our smtp failed
        HeloFailed,
        MailFailed,
        ToFailed,
        CcFailed,
        BccFailed,
        DataFailed,
        SendFailed,
        QuitFailed
    }
    #endregion

    #region MailVerifyLevel
    public enum MailVerifyLevel
    {
        None,
        All,
        Address
    }

    #endregion

    public class MailVerifier
    {
        #region SmtpResponse
        private enum SmtpResponse : int
        {
            ConnectSuccess = 220,
            GenericSuccess = 250,
            DataSuccess = 354,
            QuitSuccess = 221
        }


        private enum RecipentType
        {
            To,
            Cc,
            Bcc
        }
        #endregion

        #region Data Members

        private IPHostEntry host;
        private IPEndPoint ourSmtp;
        private Socket socket;
        private Mailer mail;
        private MailVerifyLevel level = MailVerifyLevel.None;
        #endregion

        #region Properties
        public MailVerifyLevel MailVerifyLevel
        {
            get { return level; }
            set { level = value; }
        }

        #endregion

        #region Public Methods
        public MailVerifyResult Verify(Mailer mail)
        {
            if (level == MailVerifyLevel.None)
            {
                return MailVerifyResult.Ok;
            }

            MailVerifyResult result = MailVerifyResult.Failed;

            MailVerifier v = new MailVerifier();

            try
            {
                result = v.DoVerify(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public MailVerifyResult DoVerify(Mailer mail)
        {
            this.mail = mail;
            host = Dns.Resolve(mail.SmtpServer);
            ourSmtp = new IPEndPoint(host.AddressList[0], mail.SmtpPort);
            socket = new Socket(ourSmtp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ourSmtp);
            if (!CheckResponse(SmtpResponse.ConnectSuccess))
            {
                socket.Close();
                return MailVerifyResult.ConnectFailed;
            }

            // The SMTP HELO clause is the stage of the SMTP protocol 
            // where a SMTP server introduce them selves to each other. 
            // The sending server will identify who it is and the receiving 
            // server will (as per RFC) accept any given name. There is no 
            // requirement to give the correct information at this stage. 
            SendData(string.Format("HELO {0}" + Environment.NewLine, Dns.GetHostName()));
            if (!CheckResponse(SmtpResponse.GenericSuccess))
            {
                socket.Close();
                return MailVerifyResult.HeloFailed;
            }

            // The SMTP MAIL clause defines the sender of the mail message. 
            // There is no requirement for this address to be the same as 
            // the address given in the From clause of the message itself.
            SendData(string.Format("MAIL From: {0}" + Environment.NewLine, mail.From));
            if (!CheckResponse(SmtpResponse.GenericSuccess))
            {
                socket.Close();
                return MailVerifyResult.MailFailed;
            }

            if (!VerifyAddress(mail.To))
            {
                socket.Close();
                return MailVerifyResult.ToFailed;
            }

            if (!VerifyAddress(mail.Cc))
            {
                socket.Close();
                return MailVerifyResult.CcFailed;
            }

            if (!VerifyAddress(mail.Bcc))
            {
                socket.Close();
                return MailVerifyResult.BccFailed;
            }

            if (MailVerifyLevel == MailVerifyLevel.Address)
            {
                return MailVerifyResult.Ok;
            }

            #region Create Message
            StringBuilder msg = new StringBuilder();

            msg.Append("From: " + mail.From + Environment.NewLine);
            AddRecipent(msg, mail.To, RecipentType.To);
            AddRecipent(msg, mail.Cc, RecipentType.Cc);
            AddRecipent(msg, mail.Bcc, RecipentType.Bcc);
            msg.Append("Date: ");
            msg.Append(DateTime.Now.ToString("ddd, d M y H:m:s z"));
            msg.Append(Environment.NewLine);
            msg.Append("Subject: " + mail.Subject + Environment.NewLine);
            msg.Append("X-Mailer: SMTPDirect v1" + Environment.NewLine);
            AppendBody(msg);
            #endregion

            SendData(("DATA" + Environment.NewLine));
            if (!CheckResponse(SmtpResponse.DataSuccess))
            {
                socket.Close();
                return MailVerifyResult.DataFailed;
            }

            SendData(msg.ToString());
            if (!CheckResponse(SmtpResponse.GenericSuccess))
            {
                socket.Close();
                return MailVerifyResult.SendFailed;
            }

            SendData("QUIT" + Environment.NewLine);
            if (!CheckResponse(SmtpResponse.QuitSuccess))
            {
                socket.Close();
                return MailVerifyResult.QuitFailed;
            }
            else
            {
                socket.Close();
                return MailVerifyResult.Ok;
            }
        }

        public string GetVerifyResultString(MailVerifyResult result)
        {
            switch (result)
            {
                case MailVerifyResult.Ok:
                    return "Email successfully verified";
                case MailVerifyResult.Failed:
                    return "Email verify failed";
                case MailVerifyResult.ConnectFailed:
                    return "Unable to connect to Smtp Server " + mail.SmtpServer;
                case MailVerifyResult.HeloFailed:
                    return "Unable to introduce to Smtp Server " + mail.SmtpServer;
                case MailVerifyResult.MailFailed:
                    return ("Unable to define " + mail.From + " as a sender of from Smtp Server ") + mail.SmtpServer + ". Please check if email address is correct.";
                case MailVerifyResult.ToFailed:
                    return "Incorrect email address " + mail.To + ". Please check if email address is correctly entered.";
                case MailVerifyResult.CcFailed:
                    return "Incorrect email address " + mail.Cc + ". Please check if email address is correctly entered.";
                case MailVerifyResult.BccFailed:
                    return "Incorrect email address " + mail.Bcc + ". Please check if email address is correctly entered.";
                case MailVerifyResult.DataFailed:
                    return "Incorrect email address " + mail.Bcc + ". Please check if email address is correctly entered.";
                case MailVerifyResult.SendFailed:
                    return ("Unable to send email at " + mail.To + " using server") + mail.SmtpServer;
                case MailVerifyResult.QuitFailed:
                    return "Quit failed from " + mail.SmtpServer;
                default:
                    return ("Error occured while sending email at " + mail.To + " using server") + mail.SmtpServer;
            }
        }
        #endregion

        #region Helpers
        private void AppendBody(StringBuilder msg)
        {

        }

        private void AddRecipent(StringBuilder s, string emails, RecipentType type)
        {
            if (emails == null || string.IsNullOrEmpty(emails))
            {
                return;
            }

            string[] emailList = emails.Split(new char[] { ';' });

            if (emailList.Length == 0)
            {
                return;
            }

            s.Append(type.ToString() + ": ");

            for (int i = 0; i <= emailList.Length - 1; i++)
            {
                s.Append((i > 0 ? "," : ""));
                s.Append(emailList[i]);
            }

            s.Append(Environment.NewLine);
        }

        private bool VerifyAddress(string emails)
        {
            string[] emailList = emails.Split(new char[] { ';' });
            foreach (string email in emailList)
            {
                // RCPT clause defines the receiver of the mail message
                SendData(string.Format("RCPT TO: {0}" + Environment.NewLine, email));
                if (!CheckResponse(SmtpResponse.GenericSuccess))
                {
                    socket.Close();
                    return false;
                }
            }

            return true;
        }

        private void SendData(string text)
        {
            byte[] msg = Encoding.ASCII.GetBytes(text);
            socket.Send(msg, 0, msg.Length, SocketFlags.None);
        }

        private bool CheckResponse(SmtpResponse expected)
        {
            string sResponse = null;
            int response = 0;
            byte[] bytes = new byte[1024];

            while (socket.Available == 0)
            {
                System.Threading.Thread.Sleep(100);
            }

            socket.Receive(bytes, 0, socket.Available, SocketFlags.None);
            sResponse = Encoding.ASCII.GetString(bytes);
            response = Convert.ToInt32(sResponse.Substring(0, 3));

            if (response != (int)expected)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
    #endregion
}
#pragma warning enable
