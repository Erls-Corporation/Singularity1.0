// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace App.Model
{
    public class MessageForm
    {
        public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, Config.ProductName, buttons, icon);
        }

        public static DialogResult Show(string text)
        {
            return Show(text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Error(string text)
        {
            return Show(text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Show(Exception ex)
        {
            return Show(ex, "");
        }

        public static DialogResult Show(Exception ex, string text)
        {
            return Error(text + "\n\n" + ex.Message + "\n\n" + ex.StackTrace);
        }

        public static DialogResult Confirm(string text)
        {
            return MessageBox.Show(text, Config.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
