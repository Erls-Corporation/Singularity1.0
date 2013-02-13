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

    public enum AdTypeE
    {
        One728x90,
        Two250x250,
        One728x15,
        Two728x15,
        Three728x15,
        One468x15,
        Two468x15,
        Three468x15,
        One120x600
    }

    //SELECT replace(name, ' ', '') + '=' + convert(varchar(20),Code) + ',' FROM Attribute where Code is not null order by Code
    public enum AttributeE : int
    {
        Unknown = 0,
        SiteEnableGoogleAds = 1,
        ServiceID = 2,
        ServiceTitle = 3,
        ServiceTitle2 = 4,
        ServiceDescription = 5,
        ServicePageTitle = 6,
        ServiceMetaKeywords = 7,
        ServiceMetaDescription = 8,
        ServiceContact = 9,
        ServiceFax = 10,
        ServicePhone = 11,
        ServiceEmail = 12,
        ServiceAddress = 13,
        ServiceViewCount = 14,
        ServiceEnableGoogleAds = 15,
        ServiceAbout = 16,
        ServiceName = 17,
        ServiceTemplateEmailSubjectActivateAccount = 18,
        ServiceTemplateEmailBodyActivateAccount = 19,
        ServiceTemplateEmailSubjectForgotPassword = 20,
        ServiceTemplateEmailBodyForgotPassword = 21,
        ServiceSmtpServer = 22,
        ServiceSmtpPort = 23,
        ServiceSmtpAuthenticate = 24,
        ServiceSmtpUseSsl = 25,
        ServiceSmtpUserId = 26,
        ServiceSmtpPassword = 27,
        ServiceSmtpFrom = 28,
        CategorySearchLabel = 29,
        CategoryMyItemsLabel = 30,
        CategoryNewLabel = 31,
        CategoryBrowseLabel = 32,
        CategoryID = 33,
        CategoryItemCount = 34,
        CategoryDescription = 35,
        CategorySearchExample = 36,
        AttributeID = 37,
        AttributeRegEx = 38,
        AttributeMaxLength = 39,
        AttributeDescription = 40,
        AttributeMinLength = 41,
        UserID = 42,
        UserCountry = 43,
        UserLastLogin = 44,
        ServiceCategorySearchExample = 45,
        ServiceCategoryID = 46,
        ServiceCategoryDescription = 47,
        ServiceCategorySearchLabel = 48,
        ServiceCategoryMyItemsLabel = 49,
        ServiceCategoryNewLabel = 50,
        ServiceCategoryBrowseLabel = 51,
        ServiceCategoryItemCount = 52,
        ItemID = 53,
        ItemViewCount = 54
    }

    #endregion

    public class Attribute : BaseItem
    {
        #region Data Members
        private Category category = null;
        private AttributeType attributeType = null;
        private Attributes attributes = null;
        private Attribute parent = null;
        private AttributeLayout attributeLayout = null;
        private Kvs kvs = null;
        #endregion

        #region Constructor
        public Attribute()
            : base(0)
        {
        }

        public Attribute(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public Attribute(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }
        #endregion

        #region Properties

        #region Core
        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Attribute; }
            set { base.TableName = value; }
        }
        #endregion

        #region Table Columns
        public int CategoryID { get { return GetColInt32("CategoryID"); } set { SetColumn("CategoryID", value); } }
        public int ParentAttributeID { get { return GetColInt32("ParentAttributeID"); } set { SetColumn("ParentAttributeID", value); } }
        public AttributeTypeE AttributeTypeID { get { return (AttributeTypeE)GetColInt32("AttributeTypeID"); } set { SetColumn("AttributeTypeID", value.ToString("d")); } }
        public string Value { get { return GetCol("Value"); } set { SetColumn("Value", value); } }
        public int Sequence { get { return GetColInt32("Sequence"); } set { SetColumn("Sequence", value); } }
        public int Code { get { return GetColInt32("Code"); } set { SetColumn("Code", value); } }
        public StatusE StatusID { get { return (StatusE)GetColInt32("StatusID"); } set { SetColumn("StatusID", value.ToString("d")); } }
        #endregion

        #region Foregin Keys

        public AttributeType AttributeType
        {
            get
            {
                if (attributeType == null)
                {
                    attributeType = new AttributeType(Cxt, (int)AttributeTypeID);
                }

                return attributeType;
            }
        }

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

        public AttributeLayout AttributeLayout
        {
            get
            {
                if (attributeLayout == null)
                {
                    attributeLayout = new AttributeLayout(Cxt, CategoryID);
                }

                return attributeLayout;
            }
        }

        public Attributes Children
        {
            get
            {
                if (attributes == null)
                {
                    attributes = new Attributes(Cxt, this);
                }

                return attributes;
            }
        }

        public Attribute Parent
        {
            get
            {
                if (parent == null)
                {
                    parent = new Attribute(Cxt, ParentAttributeID);
                }

                return parent;
            }
        }
        #endregion

        #region Calculated Properties

        public bool HasValueSource
        {
            get { return Value.StartsWith("ValueSource|") || Value.StartsWith("<DocumentElement xmlns=\"http://tempuri.org/Kv.xsd\">"); }
        }

        public Kvs Kvs
        {
            get
            {
                if (kvs == null)
                {
                    kvs = new Kvs(Cxt, Value);
                }

                return kvs;
            }
        }

        public string Help
        {
            get
            {
                StringBuilder v = new StringBuilder();

                v.Append(ConfigAttributes.ToString(AttributeE.AttributeDescription));

                int val = ConfigAttributes.ToInt32(AttributeE.AttributeMaxLength);

                if (val > 0)
                {
                    v.Append(" Text limit is " + val + " characters");
                }

                return v.ToString();
            }
        }

        public string ErrorMessage
        {
            get
            {
                StringBuilder v = new StringBuilder("Invalid " + Name);

                int min = MinLength;
                int max = MaxLength;

                if (min > 0 || max > 0)
                {
                    v.Append(". Text size should be between " + min + " to " + max + " characters.");
                }
                else if (min > 0)
                {
                    v.Append(". Minimum text size is " + min + " characters.");
                }
                else if (max > 0)
                {
                    v.Append(". Maximum text size is " + max + " characters.");
                }

                return v.ToString();
            }
        }

        public string ErrorMessageRequired
        {
            get
            {
                return Name + " is required.";
            }
        }

        public bool IsImage
        {
            get
            {
                return AttributeType.AttributeTypeID == AttributeTypeE.RsFileUpload;
            }
        }

        public bool IsRequired
        {
            get
            {
                return ConfigAttributes.ToInt32(AttributeE.AttributeMinLength) > 0;
            }
        }

        public bool HasParent
        {
            get
            {
                return ParentAttributeID != 0;
            }
        }

        public bool HasRegEx
        {
            get
            {
                return ConfigAttributes.ToString(AttributeE.AttributeRegEx) != "";
            }
        }

        public bool IsTextBox
        {
            get
            {
                switch (AttributeTypeID)
                {
                    case AttributeTypeE.RsTextArea:
                    case AttributeTypeE.RsTextBox:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public int MinLength
        {
            get
            {
                return ConfigAttributes.ToInt32(AttributeE.AttributeMinLength);
            }
        }

        public int MaxLength
        {
            get
            {
                return ConfigAttributes.ToInt32(AttributeE.AttributeMaxLength);
            }
        }

        public Attribute this[int index]
        {
            get
            {
                return (Attribute)attributes[index];
            }
        }

        public string UrlAttributeImage
        {
            get { return Config.UrlUploadsChild("Attribute/Image") + ID + ".jpg"; }
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

        public string Get(AttributeE code)
        {
            string val = ConfigAttributes.Get(code).Value1;

            return val;
        }
        #endregion

        #endregion

        #region Code
        public static int GetID(AttributeE code)
        {
            return Attribute.GetByCode(code).ID;
        }

        public static Attribute GetByCode(AttributeE code)
        {
            return Config.SysAttributes.Filter("Code=" + code.ToString("d")).First;
        }

        #endregion

        #region ToString
        public override string ToString()
        {
            return ID + "|" + Name + "|" + AttributeType.Name;
        }
        #endregion

    }
}
