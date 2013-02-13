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
    [ToolboxData("<{0}:RsRadioButtonList runat=server />")]
    public class RsRadioButtonList : RadioButtonList, IRsControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.OnPreRender(e);
            }
        }

        #region InitControl
        private void InitControl(LayoutE layout, ItemAttribute ia)
        {
            Enabled = layout != LayoutE.View;
            Visible = true;
            DataValueField = "AttributeID";
            DataTextField = "Name";
            DataSource = ia.Attribute.Children.DataTable;
            DataBind();
        }
        #endregion

        #region IRsControl Members

        public virtual ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            ia.Value1 = SelectedValue;

            list.Add(ia);

            return ia;
        }

        public virtual void SetValue(LayoutE layout, ItemAttribute ia)
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

        public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            RsDropDownList.GetFilter(q, ia, SelectedValue);
        }

        public virtual string GetText(ItemAttribute ia)
        {
            return RsDropDownList.GetTextControl(ia);
        }

        #endregion
    }
}
