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
    #region enum RsOneItemTable
    //SELECT name + ',' FROM sys.objects where type='U' and name not in ('sysdiagrams', 'dtproperties') order by name
    public enum RsOneItemTable
    {
        Unknown,
        Attribute,
        AttributeLayout,
        AttributeTable,
        AttributeType,
        Category,
        Item,
        ItemAttribute,
        Layout,
        Log,
        Role,
        RoleTask,
        Service,
        ServiceCategory,
        ServiceTask,
        ServiceType,
        ServiceUser,
        Status,
        Task,
        User,
        UserRole,
        ZAdminAttribute,
        ZDummyAttribute
    }
    #endregion

    public class _Item : BaseItem
    {
        #region Constructor
        public _Item()
            : base(0)
        {
        }

        public _Item(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public _Item(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
        #endregion

        #region Properties

        #region Core

        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Item; }
            set { base.TableName = value; }
        }

        #endregion

        #region Table Columns
        public int ServiceID { get { return GetColInt32("ServiceID"); } set { SetColumn("ServiceID", value); } }
        public int CategoryID { get { return GetColInt32("CategoryID"); } set { SetColumn("CategoryID", value); } }
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
                    configAttributes = new ItemAttributes(Cxt, RsOneItemTable.ItemAttribute, ItemAttributeTypeE.ConfigItem, ID);
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

        #endregion

        #region Calculated


        #endregion

        #endregion

        #region Methods
        public static void SetStatus(Cxt cxt, int id, StatusE status)
        {
            _Item item = new _Item(cxt, id);

            item.StatusID = status;

            item.Save();
        } 
        #endregion

        #region Inc
        public void IncViewCount()
        {
            ItemAttribute ia = ConfigAttributes.Get(AttributeE.ItemViewCount);

            if (ia.IsNew)
            {
                int cid = Category.GetID(CategoryE.ItemConfig);

                ia = ConfigAttributes.Set(AttributeE.ItemID, ID);
                ia.ServiceID = (int)ServiceE.ConfigService;
                ia.CategoryID = cid;

                ia = ConfigAttributes.Set(AttributeE.ItemViewCount, 1);
                ia.ServiceID = (int)ServiceE.ConfigService;
                ia.CategoryID = cid;

                ConfigAttributes.ItemID = 0; // to make sure new item is created
                ConfigAttributes.Save();
            }
            else
            {
                int viewCount = ia.Value1Int32;
                ia = ConfigAttributes.Set(AttributeE.ItemViewCount, ++viewCount);
                ia.Save();
            }
        } 
        #endregion
    }
}
