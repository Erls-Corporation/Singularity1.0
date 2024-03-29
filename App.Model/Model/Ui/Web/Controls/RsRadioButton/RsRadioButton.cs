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
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Model
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RsRadioButton runat=server />")]
    public class RsRadioButton : RadioButton, IRsControl
    {
        public string Value
        {
            get { return Checked ? "1" : "0"; }
            set { Checked = (value == "1" ? true : false); }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        #region Properties

        [Category("RafeySoft")]
        public virtual bool ShowDefaultItem
        {
            get { return UWeb.VsBool(ViewState, "ShowDefaultItem", false); }
            set { ViewState["ShowDefaultItem"] = value; }
        }

        [Category("RafeySoft")]
        public virtual bool SelectDefaultItem
        {
            get { return UWeb.VsBool(ViewState, "SelectDefaultItem", false); }
            set { ViewState["SelectDefaultItem"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string DefaultItemText
        {
            get { return UWeb.Vs(ViewState, "DefaultItemText", ""); }
            set { ViewState["DefaultItemText"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string DefaultItemValue
        {
            get { return UWeb.Vs(ViewState, "DefaultItemValue", ""); }
            set { ViewState["DefaultItemValue"] = value; }
        } 
        #endregion

        #region InitControl
        private void InitControl(LayoutE layout, ItemAttribute ia)
        {
            Enabled = layout != LayoutE.View;
            Visible = true;

            ia.Attribute.Kvs.SetProperties(this);
        }
        #endregion

        #region IRsControl Members

        public ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            ia.Value1 = Value;

            list.Add(ia);

            return ia;
        }

        public virtual void SetValue(LayoutE layout, ItemAttribute ia)
        {
            if (layout == LayoutE.Edit && ia.IsNew)
            {
                Value = ia.DefaultValue;
            }
            else
            {
                Value = ia.Value1;
            }
        }

        public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            RsTextBox.GetFilter(q, ia, Value);
        }

        public virtual string GetText(ItemAttribute ia)
        {
            return ia.Value1 == "1" ? "(x)" : "( )";
        }
        #endregion        
    }
}
