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
    public class AttributeTable : BaseItem
	{
        #region Constructor
        public AttributeTable()
            : base(0)
        {
        }

        public AttributeTable(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public AttributeTable(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }

        #endregion

        #region Properties

        #region Core

        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.AttributeTable; }
            set { base.TableName = value; }
        }

        #endregion

        #region Table Columns

     
        #endregion

        #region ConfigAttributes


        #endregion

        #region Foregin Keys

       
        #endregion

        #region Calculated
        public RsOneItemTable AttributeTableName
        {
            get 
            {
                switch (ID)
                {
                    case 1:
                        return RsOneItemTable.ItemAttribute;

                    case 2:
                        return RsOneItemTable.ZAdminAttribute;

                    default:
                        return RsOneItemTable.Unknown; 
                }
            }
        }
     
        #endregion

        #endregion

        public static RsOneItemTable GetRsOneItemTable(int id)
        {
            switch (id)
            {
                case 1:
                    return RsOneItemTable.ItemAttribute;
                case 2:
                    return RsOneItemTable.ZAdminAttribute;
                default:
                    return RsOneItemTable.Unknown;
            }
        }
    }
}
