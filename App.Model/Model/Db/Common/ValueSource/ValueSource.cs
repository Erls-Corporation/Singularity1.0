// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public class ValueSource1
    {
        public static string Value(Cxt cxt, string valueSource)
        {
            switch (GetName(valueSource))
            {
                case "User.ID":
                    return cxt.User.ID.ToString();

                case "User.Name":
                    return cxt.User.Name;

                case "User.UserName":
                    return cxt.User.UserName;

                case "DateTime.Now":
                    return UStr.Timestamp;

                default:
                    throw new Exception("Invalid value name:" + valueSource);
            }
        }

        public static string Text(Cxt cxt, string valueSource, string value)
        {
            switch (GetName(valueSource))
            {
                case "User.ID":
                    return User.GetUser(cxt, value).Name;

                case "User.Name":
                    return User.GetUser(cxt, value).Name;

                case "User.UserName":
                    return User.GetUser(cxt, value).UserName;

                case "DateTime.Now":
                    return value;

                default:
                    throw new Exception("Invalid value name:" + valueSource);
            }
        }

        private static string GetName(string valueSource)
        {
            string [] sc = UStr.Split(valueSource, "|");

            if (sc.Length < 2)
            {
                return "";
            }

            return sc.GetValue(1).ToString();
        }
    }
}
