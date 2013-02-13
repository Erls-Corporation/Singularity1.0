// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Security.Principal;
using System.Web.Security;
using System.Data.SqlClient;

namespace App.Model
{
    public class User : BaseItem
    {
        #region Data Members

        #endregion

        #region Constructor
        public User()
            : base(0)
        {
        }

        public User(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public User(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }

        public User(Cxt cxt, DataRow row)
        {
            Cxt = cxt;

            DataRow = row;
        }

        public User(Cxt cxt, string userName)
        {
            Cxt = cxt;

            Load(userName);
        }
        #endregion

        #region Properties

        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.User; }
            set { base.TableName = value; }
        }
        
        #endregion

        #region Table Columns

        public string UserName { get { return GetCol("UserName").ToLower(); } set { SetColumn("UserName", value.ToLower()); }}
        public string Password { get { return UCrypto.Decrypt(PasswordEncrypted); } set { PasswordEncrypted = UCrypto.Encrypt(value); } }
        public StatusE StatusID { get { return (StatusE)GetColInt32("StatusID"); } set { SetColumn("StatusID", value); } }
        
        #endregion

        #region Calculated
        public string PasswordEncrypted
        {
            get { return GetCol("Password"); }
            set { SetColumn("Password", value); }
        }

        string[] roles = null;

        public string [] Roles
        {
            get 
            {
                if (roles == null)
                {
                    roles = GetRoles(ID);
                }

                return roles; 
            }
            set { roles = value; }
        }

        #endregion

        #region ConfigAttributes
        private ItemAttributes configAttributes;
        public ItemAttributes ConfigAttributes
        {
            get
            {
                if (configAttributes == null)
                {
                    configAttributes = new ItemAttributes(Cxt, RsOneItemTable.ZAdminAttribute, ItemAttributeTypeE.ConfigAttribute, ID);
                }

                return configAttributes;
            }
        }

        public string Get(AttributeE attributeID)
        {
            string val = ConfigAttributes.Get(attributeID).Value1;

            return val;
        }
        #endregion

        #endregion

        #region Login
        public static IPrincipal GetPrincipal(FormsAuthenticationTicket ticket)
        {
            GenericIdentity identity = new GenericIdentity(ticket.Name);

            GenericPrincipal principal = new GenericPrincipal(identity, User.GetRoles(ticket.Name));

            return principal;
        }

        public static bool Login(Cxt cxt, string userName, string password)
        {
            User user = User.GetUser(cxt, userName, password);

            switch (user.StatusID)
            {
                case StatusE.Disabled:
                    AppException.Throw("Account is disabled.");
                    break;
                case StatusE.Inactive:
                case StatusE.Deleted:
                    AppException.Throw("Account is inactive, please <a href='" + UWeb.GetUrlActivationEmail(user.UserName) + "'>click here</a> to receive activation email.");
                    break;
                default:
                    break;
            }

            if (!user.IsNew && user.Roles.Length == 0)
            {
                AppException.Throw("Can not sign in, " + UStr.B(userName) + " has not assigned any role.");
            }

            return !user.IsNew;
        }

        #endregion

        #region GetUser

        public void Load(string userName)
        {
            DataTable table = BaseCollection.ExecuteSql(RsOneItemTable.User, "select * from [User] where LOWER(UserName)=LOWER(@p1)", userName);

            SetRow(table);
        }

        public static User GetUser(Cxt cxt, string userName, string password)
        {
            DataTable table = BaseCollection.ExecuteSql(RsOneItemTable.User, "select * from [User] where LOWER(UserName)=LOWER(@p1) AND Password=@p2", userName, UCrypto.Encrypt(password));

            User user = null;

            if (table.Rows.Count > 0)
            {
                user = new User(cxt, table.Rows[0]);
            }
            else
            {
                user = new User();
            }

            return user;
        }

        public static User GetUser(Cxt cxt, string userName)
        {
            DataTable table = BaseCollection.ExecuteSql(RsOneItemTable.User, "select * from [User] where LOWER(UserName)=LOWER(@p1)", userName);

            User user = null;

            if (table.Rows.Count > 0)
            {
                user = new User(cxt, table.Rows[0]);
            }
            else
            {
                user = new User();
            }

            return user;
        }

        public static string[] GetRoles(string userName)
        {
            DataTable table = BaseCollection.ExecuteSql(RsOneItemTable.User, "SELECT Role.RoleID FROM UserRole INNER JOIN Role ON UserRole.RoleID = Role.RoleID INNER JOIN[User] ON UserRole.UserID = [User].UserID WHERE (LOWER([User].UserName) = LOWER(@p1))", userName);

            return (string[])UData.ToArray(table, "RoleID");
        }

        public static string[] GetRoles(int userID)
        {
            DataTable table = BaseCollection.ExecuteSql(RsOneItemTable.User, "SELECT RoleID FROM UserRole WHERE UserID = @p1", userID);

            return (string[])UData.ToArray(table, "RoleID");
        }

        public static bool Exists(Cxt cxt, string userName)
        {
            User user = User.GetUser(cxt, userName);

            return !user.IsNew;
        }

        #endregion

        #region IsAuthorize
        public bool IsAuthorize(TaskE task)
        {
            if (User.IsInRole(RoleE.SiteAdmin))
            {
                return true;
            }

            bool allowed = Task.HasTask(Cxt.ServiceID, task);

            if (User.IsInRole(RoleE.SiteUser) && !Cxt.Service.IsPublic)
            {
                switch (task)
                {
                    case TaskE.NewItem:
                    case TaskE.EditItem:
                    case TaskE.DeleteItem:
                        allowed = false;
                        break;
                }
            }

            if (User.IsInRole(RoleE.ServiceAdmin) && !Cxt.Service.IsPublic)
            {
                switch (task)
                {
                    case TaskE.NewItem:
                    case TaskE.EditItem:
                    case TaskE.DeleteItem:
                        allowed = allowed && IsServiceAdmin;
                        break;
                }
            }

            return allowed;
        }
        #endregion

        #region IsServiceAdmin

        public bool IsServiceAdmin
        {
            get
            {
                if (User.IsInAnyRole(RoleE.SiteAdmin, RoleE.ServiceAdmin))
                {
                    return true;
                }

                return false;
            }
        }

        public static bool IsUserInService(string userName)
        {
            DataTable table = BaseCollection.ExecuteSql(RsOneItemTable.Service, "SELECT Service.ServiceID FROM ServiceUser INNER JOIN Service ON ServiceUser.ServiceID = Service.ServiceID INNER JOIN[User] ON ServiceUser.UserID = [User].UserID WHERE (LOWER([User].UserName) = LOWER(@p1))", userName);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IsInRole
        public static bool IsInAllRole(params RoleE[] roles)
        {
            return User.IsInRole(true, roles);
        }

        public static bool IsInAnyRole(params RoleE[] roles)
        {
            return User.IsInRole(false, roles);
        }

        public static bool IsInRole(bool checkAll, params RoleE[] roles)
        {
            foreach (RoleE role in roles)
            {
                if (checkAll)
                {
                    if (!User.IsInRole(role))
                    {
                        return false; // is not in one role
                    }
                }
                else
                {
                    if (User.IsInRole(role))
                    {
                        return true; // is in any role
                    }
                }
            }

            if (checkAll)
            {
                return true; // found in all mentioned roles
            }
            else
            {
                return false; // not found in any role
            }
        }

        public static bool IsInRole(RoleE role)
        {
            return UWeb.Principal.IsInRole(((int)role).ToString());
        }

        #endregion

        #region Save

        protected override void Save(string connectionString, SqlTransaction t)
        {
            bool isNew = false;

            if (IsNew)
            {
                if (Exists(Cxt, UserName))
                {
                    AppException.Throw(Msg.UserNameExists(UserName));
                }

                this.StatusID = StatusE.Inactive;

                isNew = true;
            }

            try
            {
                if (t == null)
                {
                    t = SqlHelper.BeginTransaction(connectionString);
                }
               
                base.Save(connectionString, t);
                
                if (isNew)
                {
                    #region Save User Role
                    UserRole ur = new UserRole();

                    ur.Cxt = Cxt;

                    ur.UserID = ID;
                    ur.RoleID = 4;

                    ur.Save(t);
                    #endregion

                    UMail.Send(this, MailTemplateE.ActivateAccount);
                } 
                
                SqlHelper.CommitTransaction(t);
            }
            catch (Exception ex)
            {
                SqlHelper.RollbackTransaction(t);

                throw ex;
            }
        }

        #endregion

        #region Activate User

        public static void HandleRequestType(Cxt cxt, AccountRequestTypeE RequestType, string userName)
        {
            switch (RequestType)
            {
                case AccountRequestTypeE.ActivateEmail:
                    SendActivationEmail(cxt, userName);
                    break;
                case AccountRequestTypeE.ActivateAccount:
                    ActivateUser(cxt, userName);
                    break;
                default:
                    break;
            }
        }
        
        public static void SendActivationEmail(Cxt cxt, string userName)
        {
            User user = User.GetUser(cxt, userName);

            if (user.IsNew)
            {
                AppException.Throw(Msg.UserNameNotExists(userName));
            }

            MailVerifyResult result = UMail.Send(user, MailTemplateE.ActivateAccount);

            if (result == MailVerifyResult.Ok)
            {
                AppException.Throw(Msg.ActivationEmailOk(userName));
            }
        }

        public static void ActivateUser(Cxt Cxt, string userName)
        {
            User user = User.GetUser(Cxt, userName);

            if (user.IsNew)
            {
                AppException.Throw(Msg.UserNameNotExists(userName));
            }

            switch (user.StatusID)
            {
                case StatusE.Active:
                    AppException.Throw("Account is already active. <a href='http://RafeySoft.com/Web/Page/Account/SignIn.aspx'>Sign In</a> now!");
                    break;
                case StatusE.Disabled:
                    AppException.Throw("Account is disabled.");
                    break;
                case StatusE.Inactive:
                case StatusE.Deleted:
                    user.StatusID = StatusE.Active;
                    user.Save();
                    AppException.Throw("Account activated successfully. <a href='http://RafeySoft.com/Web/Page/Account/SignIn.aspx'>Sign In</a> now!");
                    break;
            }
        }

        public static void SendActivationEmail(Cxt cxt)
        {
            BaseCollection items = BaseCollection.SelectItems(RsOneItemTable.User, "StatusID", 3);

            for (int i = 0; i < items.Count; i++)
            {
                if (UMail.Send(new User(cxt, items[i]), MailTemplateE.ActivateAccount) != MailVerifyResult.Ok)
                {
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }
        #endregion


    }
}
