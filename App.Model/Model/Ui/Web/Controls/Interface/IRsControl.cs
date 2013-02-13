// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace App.Model
{
    public interface IRsControl
    {
        ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia);
        void SetValue(LayoutE layout, ItemAttribute ia);
        void GetFilter(SearchQuery q, ItemAttribute ia);
        string GetText(ItemAttribute ia);
    }
}
