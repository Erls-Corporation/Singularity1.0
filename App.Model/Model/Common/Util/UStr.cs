// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;
using System.Data.SqlClient;

namespace App.Model
{
    //[System.Diagnostics.DebuggerStepThrough]
    public class UStr
    {
        #region ToString
        public static string ToString(DataRow row)
        {
            if (row == null)
            {
                return "";
            }

            StringBuilder s = new StringBuilder();

            s.AppendLine("Table Name = " + UStr.Bracket(row.Table.TableName));

            foreach (DataColumn col in row.Table.Columns)
            {
                s.AppendLine(UStr.Bracket(col.ColumnName) + " = " + UStr.Bracket(row[col.ColumnName]));
            }

            return s.ToString();
        }

        public static string ToString(SqlCommand cmd)
        {
            StringBuilder s = new StringBuilder();

            s.AppendLine(cmd.CommandText);

            foreach (SqlParameter p in cmd.Parameters)
            {
                s.AppendLine(UStr.Bracket(p.ParameterName) + " = " + UStr.Bracket(p.Value));
            }

            return s.ToString();
        } 
        #endregion

        public static string GetString(params object[] o)
        {
            StringBuilder s = new StringBuilder();

            foreach (object ox in o)
            {
                if (ox == null)
                {
                    s.Append("null,");
                }
                else
                {
                    s.Append(ox.ToString() + ",");
                }
            }

            return TrimEnd(s.ToString(), ",");
        }

        #region Trim
        public static string TrimStart(object s, object trim)
        {
            if (s == null || trim == null)
            {
                return null;
            }

            string sx = s.ToString();
            string st = trim.ToString();
            if (!sx.StartsWith(st))
            {
                return sx;
            }

            int len = st.Length;
            if (len > sx.Length)
            {
                return sx;
            }

            return sx.Remove(0, len);
        }

        public static string TrimEnd(object s, object trim)
        {
            if (s == null || trim == null)
            {
                return null;
            }

            string sx = s.ToString();
            string st = trim.ToString();
            if (!sx.EndsWith(st))
            {
                return sx;
            }

            int len = st.Length;
            if (len > sx.Length)
            {
                return sx;
            }

            return sx.Remove(sx.Length - len, len);
        } 
        #endregion

        public static string Join(string sep1, string sep2, params object[] s)
        {
            string ret = "";
            bool s1 = false;

            if (s == null)
            {
                return "";
            }

            foreach (object v in s)
            {
                s1 = !s1;

                if (v != null)
                {
                    ret += v.ToString() + (s1 ? sep1 : sep2);
                }
            }

            return UStr.TrimEnd(ret, s1 ? sep1 : sep2);
        }

        #region Surrounds

        public static string Percent(object s)
        {
            return "%" + s.ToString() + "%";
        }
        
        public static string Quote(object s)
        {
            return "'" + s.ToString() + "'";
        }

        public static string DQuote(object s)
        {
            return "\"" + s.ToString() + "\"";
        }

        public static string Bracket(object o)
        {
            return "[" + (o == null ? "" : o.ToString()) + "]";
        }

        public static string PBracket(object o)
        {
            return "(" + (o == null ? "" : o.ToString()) + ")";
        }

        public static string B(object o)
        {
            return H("b", o);
        }

        public static string H(string tag, object o)
        {
            if (o == null)
            {
                return "";
            }

            return "<" + tag + ">" + o.ToString() + "</" + tag + ">";
        }
        #endregion

        #region Filter
        public static string Filter(string columnName, object value)
        {
            return "LOWER(" + UStr.Bracket(columnName) + ") LIKE LOWER('%" + value + "%')";
        }

        public static string FilterParam(string columnName, object value)
        {
            return "LOWER(" + UStr.Bracket(columnName) + ") LIKE LOWER(" + value + ")";
        }

        public static string FilterExact(string columnName, object value)
        {
            return UStr.Bracket(columnName) + "='" + value + "'";
        }

        public static string FilterInt32(string columnName, object value)
        {
            return UStr.Bracket(columnName) + "=" + value;
        } 
        #endregion

        public static string Delimited(string delimiter, params object[] vals)
        {
            string s = "";

            foreach (object val in vals)
            {
                s += val.ToString() + delimiter;
            }

            return UStr.TrimEnd(s, delimiter);
        }

        public static string Timestamp
        {
            get { return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"); }
        }

        public static string[] Split(object s, string delimiter)
        {
            if (s == null)
            {
                return null;
            }

            return s.ToString().Split(delimiter.ToCharArray());
        }

        public static byte[] ToBytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string TrimNewLine(string s)
        {
            return s.Replace(Environment.NewLine, "");
        }

        public static string FilterAnd(params object[] pv)
        {
            string filter = "";

            if (pv == null || pv.Length == 0)
            {
                return "";
            }
            
            if (pv.Length % 2 != 0)
            {
                throw new Exception("Length of parameter-value collection should be an even number. Incorrect parameter-value collection [" + UStr.GetString(pv) + "]");
            }

            for (int i = 0; i < pv.Length; i += 2)
            {
                if (pv.GetValue(i) != null)
                {
                    string val = "";

                    if (pv.GetValue(i + 1) != null)
                    {
                        val = pv.GetValue(i + 1).ToString();
                    }
 
                    filter = BaseItem.FilterAnd(filter, UStr.FilterExact(pv.GetValue(i).ToString(), val));
                }
            }

            filter = BaseItem.TrimAnd(filter);

            return filter;
        
        }

        #region Substring
        public static string Substring(string s, int maxChar, bool removeNewLine)
        {
            return UStr.Substring(removeNewLine ? s.Replace(Environment.NewLine, "") : s, maxChar);
        }

        public static string Substring(string s, int maxChar)
        {
            int len = s.Length;

            return s.Substring(0, len <= maxChar ? len : maxChar);
        } 
        #endregion

        public static string More(string s, int maxChar, string readMoreUrl)
        {
            int len = s.Length;

            if (len <= maxChar)
            {
                return s;
            }

            return s.Substring(0, maxChar) + " <a href=" + UStr.DQuote(readMoreUrl) + ">Read More...</a>";
        }

        #region Duration
        public static string Duration(DateTime dx)
        {
            string s = "";

            if (dx == null)
            {
                return s;
            }

            DateTime du = DateTime.MinValue + (DateTime.Now - dx);

            int y = du.Year - 1;
            int m = du.Month - 1;

            s = Duration(y, "year", m, "month");
            if (s != "")
            {
                return s;
            }

            int d = du.Day - 1;

            s = Duration(m, "month", d, "day");
            if (s != "")
            {
                return s;
            }

            s = Duration(d, "day", du.Hour, "hour");
            if (s != "")
            {
                return s;
            }

            s = Duration(du.Hour, "hour", du.Minute, "minute");
            if (s != "")
            {
                return s;
            }

            s = Duration(du.Minute, "minute", 0, "");
            if (s != "")
            {
                return s;
            }

            return "A moment ago";
        }

        public static string Duration(int p1, string s1, int p2, string s2)
        {
            string s = "";

            if (p1 != 0)
            {
                s += Plural(p1, s1);

                if (p2 != 0)
                {
                    s += ", " + Plural(p2, s2);
                }

                s += " ago";
            }

            return s;
        }

        public static string Plural(object count, string s)
        {
            int i = BaseItem.ToInt32(count);

            return i + (i == 1 ? " " + s : " " + s + "s");
        } 
        #endregion

        public static string StripHtml(string s)
        {
            s = Regex.Replace(s, @"<(.|\n)*?>", string.Empty);

            s = s.Replace(Environment.NewLine, "");
            s = s.Replace("&nbsp;", " ");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");

            return s;
        }

        public static bool IsSpaces(string s)
        {
            if (s == null)
            {
                return true;
            }

            return new Regex(@"\w|\d", RegexOptions.Multiline).Match(s).Length == 0;
        }

        #region Quotes
        public static bool InQuotes(string s)
        {
            return s.StartsWith("\"") && s.EndsWith("\"");
        }

        public static string RemoveQuotes(string s)
        {
            if (s.StartsWith("\""))
            {
                s = s.TrimStart("\"".ToCharArray());
            }

            if (s.EndsWith("\""))
            {
                s = s.TrimEnd("\"".ToCharArray());
            }

            return s;
        } 
        #endregion

        // Returns true if string 's' is found in delimitedText
        public static bool Contains(string text, object s)
        {
            return text.IndexOf(s.ToString()) != -1;
        }
    }
}
