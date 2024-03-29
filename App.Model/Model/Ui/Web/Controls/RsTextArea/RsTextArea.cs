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
    [ToolboxData("<{0}:RsTextArea runat=server />")]
    public class RsTextArea : RsTextBox, IRsControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        #region IRsControl Members

        public override ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            return base.GetValue(list, ia);
        }

        public override void SetValue(LayoutE layout, ItemAttribute ia)
        {
            TextMode = TextBoxMode.MultiLine;
            Height = 200;

            base.SetValue(layout, ia);
        }

        public override void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            base.GetFilter(q, ia);
        }

        public override string GetText(ItemAttribute ia)
        {
            return base.GetText(ia).Replace(Environment.NewLine, "<br/>");
        }

        #endregion
    }
}
