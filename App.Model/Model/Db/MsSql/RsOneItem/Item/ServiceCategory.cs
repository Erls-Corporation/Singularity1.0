// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Web;

namespace App.Model 
{
    public class ServiceCategory : BaseItem
	{
        #region Constructor
        public ServiceCategory()
            : base(0)
        {
        }

        public ServiceCategory(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public ServiceCategory(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }

        public ServiceCategory(Cxt cxt, int serviceID, int categoryID)
        {
            Cxt = cxt;

            Load(serviceID, categoryID);
        }

        #endregion

        #region Load
        private void Load(int serviceID, int categoryID)
        {
            DataTable table = BaseCollection.Select(RsOneItemTable.ServiceCategory, "serviceID", serviceID, "CategoryID", categoryID);

            SetRow(table);

            // if no matching row is found atleast set serviceID and CategoryID passed!
            ServiceID = serviceID;
            CategoryID = categoryID;
        }

        #endregion
        
        #region Properties

        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.ServiceCategory; }
            set { base.TableName = value; }
        }
        #endregion

        #region Table Columns
        public int ServiceID { get { return GetColInt32("ServiceID"); } set { SetColumn("ServiceID", value); } }
        public int CategoryID { get { return GetColInt32("CategoryID"); } set { SetColumn("CategoryID", value); } }
        public int Sequence { get { return GetColInt32("Sequence"); } set { SetColumn("Sequence", value); } }
        public bool IsDefault { get { return GetColBool("IsDefault"); } set { SetColumn("IsDefault", value); } }
        public StatusE StatusID { get { return (StatusE)GetColInt32("StatusID"); } set { SetColumn("StatusID", value); } }
        public int AttributeTableID { get { return GetColInt32("AttributeTableID"); } set { SetColumn("AttributeTableID", value); } }
   
        #endregion

        #region ConfigAttributes

        private ItemAttributes configAttributes;
        public ItemAttributes ConfigAttributes
        {
            get
            {
                if (configAttributes == null)
                {
                    configAttributes = new ItemAttributes(Cxt, RsOneItemTable.ZAdminAttribute, ItemAttributeTypeE.ConfigServiceCategory, ID);
                }

                return configAttributes;
            }
        }

        public string Get(AttributeE attributeID, string defaultValue)
        {
            string val = Get(attributeID);

            return val == "" ? defaultValue : val;
        }

        public string Get(AttributeE attributeID)
        {
            string val = ConfigAttributes.Get(attributeID).Value1;

            if (val == "")
            {
                val = Category.Get(attributeID);
            }

            return val;
        }

        #endregion        

        #region Foregin Keys
        private Service service = null;
        public Service Service
        {
            get
            {
                if (service == null)
                {
                    service = new Service(Cxt, ServiceID);
                }

                return service;
            }
        }

        private Category category = null;
        public Category Category
        {
            get
            {
                if (category == null)
                {
                    category = new Category(Cxt, CategoryID);
                }

                return category;
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
        #endregion

        #endregion

        #region Delete
        public static void Delete(int serviceID, int categoryID)
        {
            BaseCollection.Delete(RsOneItemTable.ServiceCategory, "serviceID", serviceID, "CategoryID", categoryID);
        }

        #endregion

        #region ToString
        public override string ToString()
        {
            return ID + "|" + UStr.Bracket(ServiceID) + Service + "|" + UStr.Bracket(CategoryID) + Category;
        }
        #endregion

        #region UpdateStats
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
                ServiceCategory c = new ServiceCategory(cxt, items[i].GetColInt32("ServiceID"), items[i].GetColInt32("CategoryID"));

                c.ConfigAttributes.Set(AttributeE.ServiceCategoryItemCount, items[i].GetCol("ItemCount"));

                c.ConfigAttributes.Save();
            }
        }

        #endregion

        #region Save

        protected override void Save(string connectionString, SqlTransaction t)
        {
            base.Save(connectionString, t);

            ItemAttribute ia = ConfigAttributes.Set(AttributeE.ServiceCategoryID, ID);

            ia.ServiceID = (int) ServiceE.ConfigService;
            ia.CategoryID = (int)CategoryE.ServiceCategoryConfig;

            ConfigAttributes.Save();
        } 

        #endregion

    }
}
