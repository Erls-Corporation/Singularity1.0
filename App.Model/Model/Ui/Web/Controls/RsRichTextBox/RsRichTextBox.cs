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
    [ToolboxData("<{0}:RsRichTextBox runat=server />")]
    public class RsRichTextBox : FredCK.FCKeditorV2.FCKeditor, IRsControl
    {
        public string Text
        {
            get { return base.Value; }
            set { base.Value = Text; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.BasePath = "~/Web/Js/fckeditor/";
            base.OnPreRender(e);
        }

        #region IRsControl Members

        public virtual ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            ia.Value1 = UStr.StripHtml(base.Value);

            ia.ValueText = base.Value;

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
                Value = ia.ValueText;
            }

            Visible = true;
            Height = 400;
            Width = new Unit(100, UnitType.Percentage);
        }

        public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            RsTextBox.GetFilter(q, ia, Value);
        }

        public virtual string GetText(ItemAttribute ia)
        {
            return ia.ValueText;
        }
        #endregion
    }
}
