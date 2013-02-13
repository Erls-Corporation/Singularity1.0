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
    [ToolboxData("<{0}:RsImageButton runat=server />")]
    public class RsImageButton : ImageButton, IRsControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        #region IRsControl Members
        public virtual ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            list.Add(ia);

            return ia;
        }

        public virtual void SetValue(LayoutE layout, ItemAttribute ia)
        {
            ImageUrl = ia.Attribute.UrlAttributeImage;
        }

        public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            return;
        }

        public virtual string GetText(ItemAttribute ia)
        {
            return ia.Name;
        }

        #endregion
    }
}
