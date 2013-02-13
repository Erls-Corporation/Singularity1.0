// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace App.Model 
{
    #region enum
    //SELECT name +' = ' +convert(varchar(20), AttributeTypeID) + ',' FROM AttributeType	
    public enum AttributeTypeE
    {
        Unknown = 0,
        RsTextBox = 1,
        RsDropDownList = 2,
        RsCheckBoxList = 3,
        RsFileUpload = 4,
        RsTextArea = 9,
        RsImageButton = 10,
        RsRichTextBox = 11,
        RsRadioButtonList = 12,
        RsLabel = 13,
        RsYearCombo = 17,
        RsCheckBox = 18,
        RsHyperLink = 19
    }

    //SELECT name + '=' + convert(varchar(200), xtype) + ',' FROM systypes order by xtype
    public enum SqlSysTypeE
    {
        Image = 34,
        Text = 35,
        Uniqueidentifier = 36,
        Tinyint = 48,
        Smallint = 52,
        Int = 56,
        Smalldatetime = 58,
        Real = 59,
        Money = 60,
        Datetime = 61,
        Float = 62,
        SqlVariant = 98,
        Ntext = 99,
        Bit = 104,
        Decimal = 106,
        Numeric = 108,
        Smallmoney = 122,
        Bigint = 127,
        Varbinary = 165,
        Varchar = 167,
        Binary = 173,
        Char = 175,
        Timestamp = 189,
        Nvarchar = 231,
        Sysname = 256,
        Nchar = 239,
        Xml = 241,
    }
    #endregion

    public class AttributeType : BaseItem
	{
        public Attribute Attribute = null;

        #region Constructor
        public AttributeType()
            : base(0)
        {
        }

        public AttributeType(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public AttributeType(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }

        public AttributeType(Cxt cxt, Attribute attribute)
        {
            Cxt = cxt;

            Attribute = attribute;
        }
        #endregion

        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.AttributeType; }
            set { base.TableName = value; }
        }

        public string AssemblyName
        {
            get { return GetCol("AssemblyName"); }
            set { SetColumn("AssemblyName", value); }
        } 

        public AttributeTypeE AttributeTypeID
        {
            get { return (AttributeTypeE) GetColInt32("AttributeTypeID"); }
        }

        public static AttributeTypeE FromSqlType(SqlSysTypeE type)
        {
            switch (type)
            {
                case SqlSysTypeE.Image:
                    return AttributeTypeE.RsRichTextBox;

                case SqlSysTypeE.Text:
                    return AttributeTypeE.RsRichTextBox;

                case SqlSysTypeE.Uniqueidentifier:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Tinyint:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Smallint:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Int:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Smalldatetime:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Real:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Money:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Datetime:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Float:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.SqlVariant:
                    return AttributeTypeE.RsRichTextBox;

                case SqlSysTypeE.Ntext:
                    return AttributeTypeE.RsRichTextBox;

                case SqlSysTypeE.Bit:
                    return AttributeTypeE.RsCheckBox;

                case SqlSysTypeE.Decimal:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Numeric:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Smallmoney:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Bigint:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Varbinary:
                    return AttributeTypeE.RsRichTextBox;

                case SqlSysTypeE.Varchar:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Binary:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Char:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Timestamp:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Nvarchar:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Sysname:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Nchar:
                    return AttributeTypeE.RsTextBox;

                case SqlSysTypeE.Xml:
                    return AttributeTypeE.RsTextArea;

                default:
                    return AttributeTypeE.RsTextBox;
            }
        }
    }
}
