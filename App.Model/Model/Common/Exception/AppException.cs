// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace App.Model
{
    public class AppException
    {
        public static string Throw(Exception ex)
        {
            throw ex;
        }

        public static void Throw(string message)
        {
            throw new Exception(message);
        }

        public static string GetError(Exception ex)
        {
            return GetError(ex, "");
        }

        public static string GetError(Exception ex, string message)
        {
            return ex.Message.Replace(Environment.NewLine, "<br/>") + "<p/>" + ex.StackTrace.Replace(Environment.NewLine, "<br/>") + "<p/>" + message.Replace(Environment.NewLine, "<br/>");
        }

        public static void Throw(Exception ex, SqlCommand cmd)
        {
            Throw(new Exception(ex.Message + " - " + UStr.ToString(cmd), ex));
        }
    }
}
