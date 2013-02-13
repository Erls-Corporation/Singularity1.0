// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Model
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RsYearCombo runat=server />")]
    public class RsYearCombo : RsDropDownList, IRsControl
    {
        #region Properties

        [Category("RafeySoft")]
        public virtual int StartYear
        {
            get { return UWeb.VsInt32(ViewState, "StartYear", 0); }
            set { ViewState["StartYear"] = value; }
        }

        [Category("RafeySoft")]
        public virtual int EndYear
        {
            get { return UWeb.VsInt32(ViewState, "EndYear", 0); }
            set { ViewState["EndYear"] = value; }
        }

        [Category("RafeySoft")]
        public virtual bool StartYearCurrent
        {
            get { return UWeb.VsBool(ViewState, "StartYearCurrent", false); }
            set { ViewState["StartYearCurrent"] = value; }
        }

        [Category("RafeySoft")]
        public virtual bool EndYearCurrent
        {
            get { return UWeb.VsBool(ViewState, "EndYearCurrent", false); }
            set { ViewState["EndYearCurrent"] = value; }
        }

        [Category("RafeySoft")]
        public virtual int StartYearFromCurrent
        {
            get { return UWeb.VsInt32(ViewState, "StartYearFromCurrent", -40); }
            set { ViewState["StartYearFromCurrent"] = value; }
        }

        [Category("RafeySoft")]
        public virtual int EndYearFromCurrent
        {
            get { return UWeb.VsInt32(ViewState, "EndYearFromCurrent", 0); }
            set { ViewState["EndYearFromCurrent"] = value; }
        }

        [Category("RafeySoft")]
        public virtual bool SelectCurrentYear
        {
            get { return UWeb.VsBool(ViewState, "SelectCurrent", false); }
            set { ViewState["SelectCurrent"] = value; }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        #region InitControl

        private void InitControl(LayoutE layout, ItemAttribute ia)
        {
            Items.Clear();

            if (StartYear == 0)
            {
                StartYear = DateTime.Now.Year + StartYearFromCurrent;

                if (StartYearCurrent)
                {
                    StartYear = DateTime.Now.Year;
                }
            }

            if (EndYear == 0)
            {
                EndYear = DateTime.Now.Year + EndYearFromCurrent;

                if (EndYearCurrent)
                {
                    EndYear = DateTime.Now.Year;
                }
            }

            if (EndYear < StartYear)
            {
                throw new Exception("EndYear [" + EndYear + "] can not be less than StartYear [" + StartYear + "]");
            }

            for (int i = StartYear; i <= EndYear; i++)
            {
                base.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            if (SelectCurrentYear)
            {
                ListItem item = new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString());

                if (Items.Contains(item))
                {
                    SelectedIndex = Items.IndexOf(item);
                }
            }

            DefaultItemText = "Select Year";
            DefaultItemValue = "";
        }

        #endregion

        #region IRsControl Members

        public override ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            return base.GetValue(list, ia);
        }

        public override void SetValue(LayoutE layout, ItemAttribute ia)
        {
            if (!Page.IsPostBack)
            {
                InitControl(layout, ia);
            }

            ListItem li = Items.FindByValue(ia.Value1);

            if (li != null)
            {
                li.Selected = true;
            }
        }

        public override void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            base.GetFilter(q, ia);
        }

        public override string GetText(ItemAttribute ia)
        {
            return base.GetText(ia);
        }
        #endregion
    }
}
