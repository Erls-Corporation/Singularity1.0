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

namespace App.Model
{
    #region enums
    public enum CategoryTypeID
    {
        Unknown = 0,
        System = 1,
        General = 2
    }

    //SELECT replace(name, ' ', '') + '=' + convert(varchar(20),Code) + ',' FROM Category where Code is not null order by Code
    public enum CategoryE : int
    {
        SiteConfig = 1,
        ServiceConfig = 2,
        CategoryConfig = 3,
        AttributeConfig = 4,
        UserConfig = 5,
        ServiceCategoryConfig = 6,
        ItemConfig = 7
    }
    
    #endregion

    public class Category : BaseItem
    {
        #region Data Members
        private ServiceCategories serviceCategories = null;
        public const string DefaultIconUrl = "~/Web/Img/c.jpg";
        public const string DefaultNew = "New";
        public const string DefaultBrowse = "Browse";
        public const string DefaultSearch = "Search";
        public const string DefaultMyItems = "My Items";
        public const string DefaultEdit = "Edit";
        #endregion

        #region Constructor
        public Category()
            : base(0)
        {
        }

        public Category(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public Category(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
        #endregion

        #region Properties

        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Category; }
            set { base.TableName = value; }
        }
        #endregion

        #region Table Columns
        public CategoryTypeID CategoryTypeID
        {
            get { return (CategoryTypeID)GetColInt32("CategoryTypeID"); }
            set { SetColumn("CategoryTypeID", value.ToString("d")); }
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
                    configAttributes = new ItemAttributes(Cxt, RsOneItemTable.ZAdminAttribute, ItemAttributeTypeE.ConfigCategory, ID);
                }

                return configAttributes;
            }
        }

        public string Get(AttributeE code, string defaultValue)
        {
            string val = Get(code);

            return val == "" ? defaultValue : val;
        }

        public string Get(AttributeE code)
        {
            string val = ConfigAttributes.Get(code).Value1;

            return val;
        }

        #endregion

        #region Foregin Keys

        public ServiceCategories ServiceCategories
        {
            get
            {
                if (serviceCategories == null)
                {
                    serviceCategories = new ServiceCategories(Cxt, 0, ID);
                }

                return serviceCategories;
            }
        }

        #endregion

        #region Calculated
        public RsOneItemTable AttributeTableName
        {
            get
            {
                RsOneItemTable t = RsOneItemTable.Unknown;

                if (IsNew)
                {
                    return RsOneItemTable.ZDummyAttribute;
                }

                 // 1st try, obtain attribute table from ServiceCategory table
                if (ServiceCategories.Count > 0)
                {
                    t = ServiceCategories.First.AttributeTable.AttributeTableName;

                    if (t == RsOneItemTable.Unknown)
                    {
                        // 2nd try, obtain attribute table from Service table
                        t = ServiceCategories.First.Service.AttributeTable.AttributeTableName;
                    }
                
                    // Sometimes CategoryID=0 (e.g. Default Category is assumed to be 0) 
                    // So First.Service is also has id 0
                    if (t == RsOneItemTable.Unknown)
                    {
                        t = RsOneItemTable.ZDummyAttribute;
                    }
                }

                return t;
            }
        } 
        #endregion

        #endregion

        #region Code
        public static int GetID(CategoryE code)
        {
            return Category.GetByCode(code).ID;
        }

        public static Category GetByCode(CategoryE code)
        {
            return Config.SysCategories.Filter("Code=" + code.ToString("d")).First;
        }

        #endregion


        #region Methods
        public static void UpdateStats(Cxt cxt)
        {
            string sql = "";
            
            sql = "SELECT distinct CategoryID, ServiceID, COUNT(CategoryID) ItemCount FROM Item group by CategoryID, ServiceID";

            BaseCollection items = BaseCollection.SelectItems(RsOneItemTable.Unknown, sql);

            UpdateStats(cxt, items);
        }

        public static void UpdateStats(Cxt cxt, int serviceID, int categoryID)
        {
            string sql = "";

            sql = "SELECT CategoryID, ServiceID, COUNT(CategoryID) ItemCount FROM Item where ServiceID=@p1 AND CategoryID=@p2 group by CategoryID, ServiceID";

            BaseCollection items = new BaseCollection(BaseCollection.ExecuteSql(RsOneItemTable.Unknown, sql, serviceID, categoryID));

            UpdateStats(cxt, items);
        }

        private static void UpdateStats(Cxt cxt, BaseCollection items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Category c = new Category(cxt, items[i].GetColInt32("CategoryID"));

                c.ConfigAttributes.Set(AttributeE.CategoryItemCount, items[i].GetCol("ItemCount"));

                c.ConfigAttributes.Save();
            }
        }

        public static string IconUrl(int categoryID)
        {
            return IconUrl(categoryID, true);
        }

        public static string IconUrl(int categoryID, bool defaultUrl)
        {
            string url = "~/Web/u/c/" + categoryID + "/i/c" + categoryID + ".jpg";

            if (!UWeb.Exists(url) && defaultUrl)
            {
                return DefaultIconUrl;
            }

            return url;
        }
        #endregion
    }
}
