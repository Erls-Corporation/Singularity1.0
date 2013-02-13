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
    public class ItemAttribute : BaseItem
	{
        #region Data Members
        #endregion

        #region Constructor
        public ItemAttribute()
            : base(0)
        {
        }

        public ItemAttribute(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public ItemAttribute(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }

        public ItemAttribute(Cxt cxt, DataRow row)
            : base(cxt, row)
        {
        }

        public ItemAttribute(Cxt cxt, int itemID, int attributeID)
        {
            Cxt = cxt;

            Load(itemID, attributeID);
        }

        public ItemAttribute(Cxt cxt, int itemID, int attributeID, bool loadFromParent)
        {
            Cxt = cxt;

            if (loadFromParent)
            {
                LoadFromParent(itemID, attributeID);
            }
            else
            {
                Load(itemID, attributeID);
            }
        }
        #endregion

        #region Load
        private void Load(int itemID, int attributeID)
        {
            // store in local variables
            ItemID = itemID;
            AttributeID = attributeID;

            DataTable table = BaseCollection.Select(TableName, "ItemID", itemID, "AttributeID", attributeID);

            SetRow(table);

            // as row is created now store it in DataRow as well
            ItemID = itemID;
            AttributeID = attributeID;
        }

        // loads first child of attributeID's parent. For example 8=Gender has child 9=male, 10=female
        // LoadFromParent(1, 10) may load 9 or 10 whatever is saved in database
        private void LoadFromParent(int itemID, int attributeID)
        {
            string sql = "SELECT * FROM ItemAttribute WHERE ItemID = " + itemID + " AND AttributeID IN (SELECT AttributeID FROM Attribute WHERE ParentAttributeID IN (SELECT ParentAttributeID FROM Attribute WHERE AttributeID = " + attributeID + "))";

            DataTable table = BaseCollection.ExecuteSql(TableName, sql);

            SetRow(table);
        }
        
        #endregion

        #region Properties

        #region Core
        public override RsOneItemTable TableName
        {
            get
            {
                if (base.TableName == RsOneItemTable.Unknown || base.TableName == RsOneItemTable.ZDummyAttribute)
                {
                    base.TableName = Attribute.Category.AttributeTableName;
                }

                return base.TableName;
            }
            set { base.TableName = value; }
        }

        public override string PrimaryKey
        {
            get { return RsOneItemTable.ItemAttribute.ToString() + "ID"; }
        }

        #endregion

        #region Table Columns
        public int ServiceID { get { return GetColInt32("ServiceID"); } set { SetColumn("ServiceID", value); } }
        public int CategoryID { get { return GetColInt32("CategoryID"); } set { SetColumn("CategoryID", value); } }
        public int ItemID { get { return itemID == 0 ? GetColInt32("ItemID") : itemID; } set { itemID = value; SetColumn("ItemID", value); } }
        public int AttributeID { get { return attributeID == 0 ? GetColInt32("AttributeID") : attributeID; } set { attributeID = value; SetColumn("AttributeID", value); } }
        public string ValueText { get { return GetCol("ValueText"); } set { SetColumn("ValueText", value); } }
        public string Value { get { return GetCol("Value"); } set { SetColumn("Value", value); } }

        #endregion

        #region ConfigAttributes

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
            set { service = value; }
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
            set { category = value; }
        }

        private int attributeID = 0;
        private Attribute attribute = null;
        public Attribute Attribute
        {
            get
            {
                if (attribute == null)
                {
                    attribute = new Attribute(Cxt, AttributeID);
                }

                return attribute;
            }
            set { attribute = value; }
        }

        private int itemID = 0;
        private _Item item = null;
        public _Item Item
        {
            get
            {
                if (item == null)
                {
                    item = new _Item(Cxt, ItemID);
                }

                return item;
            }
            set { item = value; }
        }

        #endregion

        #region Calculated

        #region Value

        #region Value1
        public string Value1
        {
            get
            {
                string s = Value;

                if (ValueText.Length < s.Length)
                {
                    return s;
                }
                else
                {
                    return ValueText;
                }
            }
            set
            {
                if (value.Length <= 3500)
                {
                    Value = value;
                    ValueText = "";
                }
                else
                {
                    Value = UStr.Substring(value, 3500);
                    ValueText = value;
                }
            }
        }

        public int Value1Int32
        {
            get
            {
                return BaseItem.ToInt32(Value1);
            }
            set
            {
                Value1 = value.ToString();
            }
        }

        public bool Value1Bool
        {
            get
            {
                return BaseItem.ToBool(Value1);
            }
            set
            {
                Value1 = value.ToString();
            }
        }

        public double Value1Double
        {
            get
            {
                return BaseItem.ToDouble(Value1);
            }
            set
            {
                Value1 = value.ToString();
            }
        } 
        #endregion

        public bool IsValueSource
        {
            get
            {
                return Value.StartsWith("ValueSource|");
            }
        }

        public string DefaultValue
        {
            get
            {
                try
                {
                    if (IsNew)
                    {
                        return Attribute.Value;
                    }
                    else
                    {
                        return Value1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        #endregion

        #region FileName
        public string FileName()
        {
            return FileName(ItemID, AttributeID);
        }

        public string FileName(int no)
        {
            return FileName(ItemID, no);
        }

        public static string FileName(int itemID, int no)
        {
            return "" + itemID.ToString() + "." + no.ToString() + ".jpg";
        }

        #endregion

        #region UrlFile

        public string UrlNoFile
        {
            get { return "~/Web/Img/ni/nis.jpg"; }
        }

        public string UrlFile(int no)
        {
            return Cxt.Service.UrlDocs + FileName(no);
        }

        public string UrlFile()
        {
            return UrlFile(AttributeID);
        }

        public string UrlFile(bool noFile)
        {
            if (FileExists)
            {
                return UrlFile();
            }
            else
            {
                if (noFile)
                {
                    return UrlNoFile;
                }
            }

            return "";
        }

        public bool FileExists
        {
            get
            {
                return File.Exists(FilePath());
            }
        }
        #endregion

        #region FilePath
        public string FilePath(int no)
        {
            return UWeb.MapPath(UrlFile(no));
        }

        public string FilePath()
        {
            return UWeb.MapPath(UrlFile());
        }

        #endregion

        #endregion

        #endregion

        #region Delete
        public void Delete(int itemID, int attributeID)
        {
            BaseCollection.Delete(TableName, "ItemID", itemID, "AttributeID", attributeID);
        }

        public void Delete()
        {
            string path = FilePath();

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return ID + "|IID=" + ItemID + "|AID=" + Attribute + "|" + Value1;
        }
        #endregion

        #region AddToList

        public static ItemAttribute AddToList(ItemAttributes list, int itemID, int attributeID, object value)
        {
            ItemAttribute ia = new ItemAttribute();

            ia.Cxt = list.Cxt;
            ia.ItemID = itemID;
            ia.AttributeID = attributeID;

            if (value != null)
            {
                ia.Value1 = value.ToString();
            }

            list.Add(ia);

            return ia;
        } 
        #endregion
    }
}
