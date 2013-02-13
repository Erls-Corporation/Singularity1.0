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
using System.Data.SqlClient;

namespace App.Model
{
    #region enums

    public enum ServiceTypeE
    {
        Unknown = 0,
        Public = 1,
        Protected = 2,
        Membership = 3,
        Private = 4
    }

    public enum StatusE
    {
        Unknown = 0,
        Active = 1,
        Disabled = 2,
        Inactive = 3,
        Deleted = 4
    }

    //SELECT name + '=' + convert(varchar(20),ServiceID) + ',' FROM Service order by ServiceID
    public enum ServiceE
    {
        Unknown = 0,
        PublicServices = 1,
        ProtectedServices = 2,
        MembershipServices = 3,
        PrivateServices = 4,
        AdminService = 14,
        ConfigService = 16,
        DefaultService = 20,
    }
    
    #endregion
    
    public class Service : BaseItem
    {
        #region Data Members
        public const string DefaultIconUrl = "~/Web/Img/s.jpg";
        #endregion

        #region Constructor
        public Service()
            : base(0)
        {
        }

        public Service(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public Service(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
        
        #endregion

        #region Properties

        #region Core

        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Service; }
            set { base.TableName = value; }
        }
        
        #endregion

        #region Table Columns

        public int ParentServiceID { get { return GetColInt32("ParentServiceID"); } set { SetColumn("ParentServiceID", value); } }
        public int CategoryID { get { return GetColInt32("CategoryID"); } set { SetColumn("CategoryID", value); } }
        public int AttributeTableID { get { return GetColInt32("AttributeTableID"); } set { SetColumn("AttributeTableID", value); } }
        public ServiceTypeE ServiceTypeID { get { return (ServiceTypeE)GetColInt32("ServiceTypeID"); } set { SetColumn("ServiceTypeID", value); } }
        public StatusE StatusID { get { return (StatusE)GetColInt32("StatusID"); } set { SetColumn("StatusID", value); } }

        #endregion

        #region ConfigAttributes

        private ItemAttributes configAttributes;
        public ItemAttributes ConfigAttributes
        {
            get
            {
                if (configAttributes == null)
                {
                    configAttributes = new ItemAttributes(Cxt, RsOneItemTable.ZAdminAttribute, ItemAttributeTypeE.ConfigService, ID);
                }

                return configAttributes;
            }
        }
        
        public string Get(AttributeE attributeID)
        {
            string val = ConfigAttributes.Get(attributeID).Value1;

            if (val == "")
            {
                val = Config.DefaultService.ConfigAttributes.Get(attributeID).Value1;
            }

            return val;
        }
        
        #endregion        

        #region Foregin Keys

        private Service parentService = null;
        public Service ParentService
        {
            get
            {
                if (parentService == null)
                {
                    parentService = new Service(Cxt, ParentServiceID);
                }

                return parentService;
            }
        }

        private AttributeTable attributeTable = null;
        public AttributeTable AttributeTable
        {
            get
            {
                if (attributeTable == null)
                {
                    attributeTable = new AttributeTable(Cxt, AttributeTableID);
                }

                return attributeTable;
            }
        }

        private Category defaultCategory = null;
        public Category DefaultCategory
        {
            get
            {
                if (defaultCategory == null)
                {
                    DataTable table = BaseCollection.ExecuteSql(RsOneItemTable.Category, "SELECT * FROM Category WHERE CategoryID IN (SELECT CategoryID FROM ServiceCategory where IsDefault = 1 and ServiceID = @p1)", ID);

                    defaultCategory = new Category(Cxt, new BaseCollection(table).First);
                }

                return defaultCategory;
            }
        }

        private ServiceCategories serviceCategories = null;
        public ServiceCategories ServiceCategories
        {
            get
            {
                if (serviceCategories == null)
                {
                    serviceCategories = new ServiceCategories(Cxt, ID, 0);
                }

                return serviceCategories;
            }
        } 

        #endregion

        #region Calculated

        public bool IsPublic
        {
            get { return ServiceTypeID == ServiceTypeE.Public; }
        }

        public bool IsProtected
        {
            get { return ServiceTypeID == ServiceTypeE.Protected; }
        }

        public bool IsMembership
        {
            get { return ServiceTypeID == ServiceTypeE.Membership; }
        }

        public bool IsPrivate
        {
            get { return ServiceTypeID == ServiceTypeE.Private; }
        }

        public string UrlService
        {
            get { return Config.UrlServiceChild(ID.ToString()); }
        }

        public string FolderService
        {
            get { return UWeb.MapPath(UrlService); }
        }

        public string UrlDocs
        {
            get { return Config.UrlServiceChild(ID + "/d"); }
        }

        public string FolderDocs
        {
            get { return UWeb.MapPath(UrlDocs); }
        }

        public string UrlImages
        {
            get { return Config.UrlServiceChild(ID + "/i"); }
        }

        public string FolderImages
        {
            get { return UWeb.MapPath(UrlImages); }
        }

        public string PageTitleCalc
        {
            get { return Get(AttributeE.ServiceName) + " - " + Get(AttributeE.ServicePageTitle); }
        }

        public string MetaKeywordsCalc
        {
            get { return Get(AttributeE.ServiceMetaKeywords); }
        }

        public string MetaDescriptionCalc
        {
            get { return Get(AttributeE.ServiceMetaDescription); }
        }

        #endregion

        #endregion

        #region Init
        public void Init()
        {
            try
            {
                // root folders
                UFile.CreateFolder(Config.FolderUploads);
                UFile.CreateFolder(Config.FolderService);

                // service specific folder
                UFile.CreateFolder(FolderService);
                UFile.CreateFolder(FolderDocs);
                UFile.CreateFolder(FolderImages);
            }
            catch (Exception ex)
            {
                Log.Write(Cxt, ex);
            }
        } 
        #endregion

        #region SelectServices
        public static BaseCollection SelectServices()
        {
            return SelectServices(null);
        }

        public static BaseCollection SelectServices(string parentServiceIDList)
        {
            return SelectServices(parentServiceIDList, 0);
        }

        public static BaseCollection SelectServicesAll()
        {
            if (User.IsInRole(RoleE.SiteAdmin))
            {
                return SelectServices(null, -1);
            }
            else
            {
                return SelectServices("", -1);
            }
        }

        public static BaseCollection SelectServices(string parentServiceIDList, int selectCount)
        {
            string sql = "SELECT " + UData.ToSelectCount(selectCount) + " * FROM Service WHERE";

            if (String.IsNullOrEmpty(parentServiceIDList))
            {
                if (User.IsInRole(RoleE.SiteAdmin))
                {
                    sql += " ParentServiceID IS NULL";
                }
                else
                {
                    sql += " ParentServiceID IS NULL AND ServiceTypeID <> 4";
                }
            }
            else
            {
                sql += " ParentServiceID IN (" + parentServiceIDList + ")";
            }

            sql += " AND (StatusID IS NULL OR StatusID = 1) ORDER BY ServiceTypeID, ParentServiceID, Sequence";
            
            return BaseCollection.SelectItems(RsOneItemTable.Service, sql);
        } 
        #endregion

        #region IconUrl
        public static Attributes TableAttributes
        {
            get { return Attributes.GetTableAttributes(RsOneItemTable.Service); }
        }

        public static string IconUrl(int serviceID)
        {
            return IconUrl(serviceID, true);
        }

        public static string IconUrl(int serviceID, bool defaultUrl)
        {
            string url = "~/Web/u/s/" + serviceID + "/i/s" + serviceID + ".jpg";

            if (!UWeb.Exists(url) && defaultUrl)
            {
                return Service.DefaultIconUrl;
            }

            return url;
        }

        public static BaseCollection Children(int serviceID)
        {
            string sql = "SELECT * FROM Service WHERE ParentServiceID =" + serviceID.ToString();

            return BaseCollection.SelectItems(RsOneItemTable.Service, sql);
        }

        public static bool HasChild(int serviceID)
        {
            return Service.Children(serviceID).Count != 0;
        } 
        #endregion

        public static Service Clone(Cxt cxt, int serviceID)
        {
            Service s = new Service(cxt, serviceID);

            if (s.IsNew)
            {
                return s;
            }

            SqlTransaction t = null;

            try
            {
                s.IsNew = true;

                t = SqlHelper.BeginTransaction(Config.ConnectionString);

                s.Save(t);

                SqlHelper.CommitTransaction(t);

                return s;
            }
            catch (Exception ex)
            {
                SqlHelper.RollbackTransaction(t);

                throw ex;
            }
        }
    }
}
