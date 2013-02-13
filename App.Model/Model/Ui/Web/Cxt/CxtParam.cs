// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model;

namespace App.Model
{
    [System.Diagnostics.DebuggerStepThrough]
    public class CxtParam
    {
        #region Data Members
        public StateBag ViewState = null;
        public bool ViewStateOnly = false;
        public Cxt Cxt = null; 
        #endregion

        #region Constructor
        public CxtParam(Cxt cxt, bool viewStateOnly)
        {
            Cxt = cxt;
            ViewStateOnly = viewStateOnly;
        }
        #endregion

        #region Core IDs
        public int ServiceID
        {
            get 
            {
                if (ViewStateOnly)
                {
                    return UWeb.VsInt32(ViewState, "sid"); 
                }
                else
                {
                    return UWeb.QsInt32("sid", UWeb.VsInt32(ViewState, "sid"));
                }
            }
            set { service = null; UWeb.SetVs(ViewState, "sid", value); }
        }

        public int CategoryID
        {
            get
            {
                if (ViewStateOnly)
                {
                    return UWeb.VsInt32(ViewState, "cid");
                }
                else
                {
                    return UWeb.QsInt32("cid", UWeb.VsInt32(ViewState, "cid"));
                }
            }
            set { category = null; UWeb.SetVs(ViewState, "cid", value); }
        }

        public int ItemID
        {
            get
            {
                if (ViewStateOnly)
                {
                    return UWeb.VsInt32(ViewState, "iid");
                }
                else
                {
                    return UWeb.QsInt32("iid", UWeb.VsInt32(ViewState, "iid"));
                }
            }
            set { item = null; UWeb.SetVs(ViewState, "iid", value); }
        }

        public LayoutE LayoutID
        {
            get
            {
                if (ViewStateOnly)
                {
                    return (LayoutE)UWeb.VsInt32(ViewState, "lid");
                }
                else
                {
                    return (LayoutE)UWeb.QsInt32("lid", UWeb.VsInt32(ViewState, "lid"));
                }
            }
            set { UWeb.SetVs(ViewState, "lid", value.ToString("d")); }
        }

        public RsOneItemTable TableName
        {
            get
            {
                if (ViewStateOnly)
                {
                    return (RsOneItemTable)UWeb.VsInt32(ViewState, "tn");
                }
                else
                {
                    return (RsOneItemTable)UWeb.QsInt32("lid", UWeb.VsInt32(ViewState, "tn"));
                }
            }
            set { UWeb.SetVs(ViewState, "tn", value.ToString("d")); }
        }
        #endregion

        #region Core Objects
        private Service service = null;
        public Service Service
        {
            get
            {
                if (service == null || service.IsNew)
                {
                    service = new Service(this.Cxt, ServiceID);
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
                if (category == null || category.IsNew)
                {
                    category = new Category(this.Cxt, CategoryID);
                }

                return category;
            }
            set { category = value; }
        }

        private _Item item = null;
        public _Item Item
        {
            get
            {
                if (item == null)
                {
                    item = new _Item(this.Cxt, ItemID);
                }

                return item;
            }
        }

        private ServiceCategory serviceCategory = null;
        public ServiceCategory ServiceCategory
        {
            get
            {
                if (serviceCategory == null || serviceCategory.IsNew)
                {
                    serviceCategory = new ServiceCategory(this.Cxt, ServiceID, CategoryID);
                }

                return serviceCategory;
            }
            set { serviceCategory = value; }
        }
        #endregion

        #region Calculated
        public virtual bool IsNew
        {
            get 
            {
                return ItemID == 0; 
            }
            set
            {
            }
        }
        #endregion
    }
}