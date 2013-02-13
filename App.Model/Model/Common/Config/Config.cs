// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;

namespace App.Model
{

    [System.Diagnostics.DebuggerStepThrough]
    public class Config
    {
        static Config()
        {
            if (HttpContext.Current != null)
            {
                try
                {
                    UFile.CreateFolder(FolderUploads);
                    UFile.CreateFolder(FolderService);
                    UFile.CreateFolder(FolderUploadsZip);
                }
                catch (Exception ex)
                {
                    Log.Write(Cxt.Instance, ex);
                }
            }
        }

        #region GetKey
        public static string GetKey(string key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static int GetKeyInt32(string key, string defaultValue)
        {
            return Convert.ToInt32(Config.GetKey(key, defaultValue));
        }
        #endregion

        #region ConnectionString

        public static string DefaultConnectionString
        {
            get { return GetKey("DefaultConnectionString", "ConnectionString"); }
        }
        
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[DefaultConnectionString].ConnectionString.Replace("%AppFolder%", AppDomain.CurrentDomain.BaseDirectory); }
        }
        #endregion

        #region Uploads
        public static string UrlUploads
        {
            get { return "~/Web/u/"; }
        }

        public static string FolderUploads
        {
            get { return UWeb.MapPath(Config.UrlUploads); }
        }

        public static string FolderUploadsZip
        {
            get { return UWeb.MapPath(UrlUploadsChild("z")); }
        }

        public static string UrlUploadsChild(string subfolder)
        {
            return Config.UrlUploads + subfolder + "/";
        }
        #endregion

        #region Service
        public static string UrlService
        {
            get { return UrlUploadsChild("s"); }
        }

        public static string FolderService
        {
            get { return UWeb.MapPath(Config.UrlService); }
        }

        public static string UrlServiceChild(string subfolder)
        {
            return Config.UrlService + subfolder + "/";
        }
        #endregion

        #region Crypto
        public static byte[] IV
        {
            get { return new byte[] { 210, 152, 152, 141, 6, 84, 161, 212, 77, 71, 46, 38, 68, 110, 128, 159 }; }
        }

        public static byte[] Key
        {
            get { return new byte[] { 186, 176, 251, 49, 154, 190, 169, 253, 33, 120, 181, 202, 38, 102, 8, 104, 38, 59, 243, 44, 174, 14, 63, 152, 29, 74, 225, 121, 229, 238, 11, 33 }; }
        } 
        #endregion

        #region About
        public static string About
        {
            get { return ""; }
        }

        public static bool IsDev
        { 
            get { return ConnectionString.Contains(@".\SQLEXPRESS"); } 
        }

        public static string ProductName
        {
            get { return GetKey("ProductName", "Product information not available"); }
        }
        #endregion

        #region AppFolder
        public static string FolderAppBin
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public static string FolderAppRoot
        {
            get 
            {
                if (HttpContext.Current == null)
                {
                    return System.IO.Path.GetFullPath(FolderAppBin + @".\..\..\");
                }
                else
                {
                    return AppDomain.CurrentDomain.BaseDirectory + @"Web\";
                }
            }
        }

        public static string FolderXsd
        {
            get { return FolderAppRoot + @"Data\Xsd\"; }
        }

        #endregion

        #region Cache

        #region SiteConfig
        private static ItemAttributes siteConfig;
        public static ItemAttributes SiteConfig 
        {
            get
            {
                if (siteConfig == null)
                {
                    siteConfig = new ItemAttributes(new Cxt(), RsOneItemTable.ZAdminAttribute, ItemAttributeTypeE.RecordsCategory, (int) CategoryE.SiteConfig);
                }

                return siteConfig;
            }
            set { siteConfig = value;  }
        }

        public static bool EnableGoogleAds
        {
            get { return SiteConfig.ToBool(AttributeE.SiteEnableGoogleAds); }
        }

        #endregion

        #region DefaultService

        private static Service defaultService;
        public static Service DefaultService
        {
            get
            {
                if (defaultService == null)
                {
                    defaultService = new Service(SiteConfig.Cxt, (int)ServiceE.DefaultService);
                }

                return defaultService;
            }
            set { defaultService = value; }
        }
        #endregion

        #region SysAttributes

        private static Attributes sysAttributes;
        public static Attributes SysAttributes
        {
            get
            {
                if (sysAttributes == null)
                {
                    sysAttributes = new Attributes(SiteConfig.Cxt, true);
                }

                return sysAttributes;
            }
            set { sysAttributes = value; }
        }
        #endregion

        #region SysCategories

        private static Categories sysCategories;
        public static Categories SysCategories
        {
            get
            {
                if (sysCategories == null)
                {
                    sysCategories = new Categories(SiteConfig.Cxt, true);
                }

                return sysCategories;
            }
            set { sysCategories = value; }
        }
        #endregion

        #endregion

        #region Reload
        public static void Reload()
        {
            SiteConfig = null;
            DefaultService = null;
            SysCategories = null;
            SysAttributes = null;

            ItemAttributes ia = SiteConfig;
            Service s = DefaultService;
            Categories c = SysCategories;
            Attributes a = SysAttributes;
        } 
        #endregion
    }
}
