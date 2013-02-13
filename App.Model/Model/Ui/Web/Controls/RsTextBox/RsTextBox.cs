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

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("App.Model.Model.Ui.Web.Controls.RsTextBox.Res.Script.js", "text/javascript")]
#endregion

namespace App.Model
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RsTextBox runat=server />")]
    public class RsTextBox : TextBox, IRsControl
    {
        #region Properties

        [Category("RafeySoft")]
        public virtual bool ShowWaterMarkText
        {
            get { return UWeb.VsBool(ViewState, "ShowWaterMarkText", false); }
            set { ViewState["ShowWaterMarkText"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string WaterMarkText
        {
            get { return UWeb.Vs(ViewState, "WaterMarkText", ""); }
            set { ViewState["WaterMarkText"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string CssClassWaterMarkTextFocus
        {
            get { return UWeb.Vs(ViewState, "CssClassWaterMarkTextFocus", ""); }
            set { ViewState["CssClassWaterMarkTextFocus"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string CssClassWaterMarkTextBlur
        {
            get { return UWeb.Vs(ViewState, "CssClassWaterMarkTextBlur", ""); }
            set { ViewState["CssClassWaterMarkTextBlur"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string ClickOnEnterButtonClientID
        {
            get { return UWeb.Vs(ViewState, "ClickOnEnterButtonClientID", ""); }
            set { ViewState["ClickOnEnterButtonClientID"] = value; }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitWaterMarkText();
            }
        }

        #region IRsControl Members


        public virtual ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            ia.Value1 = Text;

            list.Add(ia);

            return ia;
        }

        public virtual void SetValue(LayoutE layout, ItemAttribute ia)
        {
            CausesValidation = true;

            Width = new Unit(99, UnitType.Percentage);

            MaxLength = ia.Attribute.ConfigAttributes.ToInt32(AttributeE.AttributeMaxLength);

            if (layout == LayoutE.Edit && ia.IsNew)
            {
                Text = ia.DefaultValue;
            }
            else
            {
                Text = ia.Value1;
            }
        }

        public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            GetFilter(q, ia, Text);
        }

        public virtual string GetText(ItemAttribute ia)
        {
            return ia.Value1;
        }

        #endregion

        #region Helpers

        private void InitWaterMarkText()
        {
            if (!ShowWaterMarkText)
            {
                return;
            }

            Page.ClientScript.RegisterClientScriptInclude(this.GetType().ToString(), UWeb.ResUrl(this, "Script.js"));

            Text = WaterMarkText;

            CssClass = CssClassWaterMarkTextBlur;

            Attributes.Add("onfocus", "RsTextBox_onfocus(this, " + UStr.Quote(CssClassWaterMarkTextFocus) + ", " + UStr.Quote(WaterMarkText) + ");");
            Attributes.Add("onblur", "RsTextBox_onblur(this, " + UStr.Quote(CssClassWaterMarkTextBlur) + ", " + UStr.Quote(WaterMarkText) + ");");
            Attributes.Add("onkeypress", "return RsTextBox_onkeypress(event, " + UStr.Quote(ClickOnEnterButtonClientID) + ")");
        }

        public static void GetFilter(SearchQuery q, ItemAttribute ia, string text)
        {
            string filter = "";
            string pid = "";

            if (String.IsNullOrEmpty(text))
            {
                return;
            }

            pid = q.NextParamId;
            filter = BaseItem.FilterAnd(filter, UStr.FilterInt32("AttributeID", pid));
            q.SetParam(pid, ia.AttributeID);
            
            pid = q.NextParamId;
            filter = BaseItem.FilterOr(filter, UStr.FilterParam("Value", pid));
            q.SetParam(pid, UStr.Percent(text));

            if (String.IsNullOrEmpty(filter))
            {
                return;
            }

            filter = BaseItem.TrimOr(filter);

            q.AppendFilter(filter + "\n");
        } 
        #endregion
    }
}
