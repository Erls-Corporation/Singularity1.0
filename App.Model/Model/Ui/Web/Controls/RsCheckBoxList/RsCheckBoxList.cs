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
using System.Collections;

namespace App.Model
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RsCheckBoxList runat=server />")]
    public class RsCheckBoxList : CheckBoxList, IRsControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
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
            ItemAttributes list2 = new ItemAttributes(ia.Cxt, ia.TableName, ia.ItemID, ia.AttributeID);

            foreach (ListItem li in Items)
            {
                bool contains = list2.Contains(ia.AttributeID, li.Value);

                if (li.Selected && !contains) // selected but no in db
                {
                    ItemAttribute.AddToList(list, ia.ItemID, ia.AttributeID, li.Value);
                }
                else if (!li.Selected && contains) // not selected but in db
                {
                    ItemAttribute iax = list2.Get(ia.AttributeID, li.Value);

                    list.DataTable.ImportRow(iax.DataRow);

                    DataRow row = list.DataTable.Rows[list.DataTable.Rows.Count - 1];

                    row.AcceptChanges();

                    row.Delete(); // mark deleted
                }
            }

            return null;
        }

        public virtual void SetValue(LayoutE layout, ItemAttribute ia)
        {
            if (!Page.IsPostBack)
            {
                InitControl(layout, ia);
            }

            ItemAttributes list = new ItemAttributes(ia.Cxt, ia.TableName, ia.ItemID, ia.AttributeID);

            for (int i = 0; i < list.Count; i++)
            {
                ItemAttribute iax = new ItemAttribute(ia.Cxt, list[i]);

                ListItem li = Items.FindByValue(iax.Value1);

                if (li != null)
                {
                    li.Selected = true;
                }
            }
        }

        public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            GetFilter(q, ia, Items);
        }

        public virtual string GetText(ItemAttribute ia)
        {
            StringBuilder s = new StringBuilder();

            if (!Page.IsPostBack)
            {
                InitControl(LayoutE.Edit, ia);
            }

            ItemAttributes list = new ItemAttributes(ia.Cxt, ia.TableName, ia.ItemID, ia.AttributeID);

            for (int i = 0; i < list.Count; i++)
            {
                ItemAttribute iax = new ItemAttribute(ia.Cxt, list[i]);

                ListItem li = Items.FindByValue(iax.Value1); // TODO: don't use ListItem here

                s.AppendLine("[" + (li != null ? "√" : " ") + "] " + li.Text);
            }

            return s.ToString();
        }
        #endregion

        #region Helper
        public static void GetFilter(SearchQuery q, ItemAttribute ia, ListItemCollection items)
        {
            string filter = "";
            string pid = "";

            foreach (ListItem item in items)
            {
                if (item.Selected)
                {
                    pid = q.NextParamId;
                    filter = BaseItem.FilterOr(filter, UStr.FilterInt32("AttributeID", pid));
                    q.SetParam(pid, item.Value);
                }
            }

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
