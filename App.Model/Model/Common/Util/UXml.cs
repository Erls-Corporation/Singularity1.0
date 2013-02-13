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
using System.IO;

namespace App.Model
{
    [System.Diagnostics.DebuggerStepThrough]
    public class UXml
    {
        public static string Indent(string xml)
        {
            try
            {
                string outXml = string.Empty;
                MemoryStream ms = new MemoryStream();
                // Create a XMLTextWriter that will send its output to a memory stream (file)
                XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.Unicode);
                XmlDocument doc = new XmlDocument();

                // Load the unformatted XML text string into an instance
                // of the XML Document Object Model (DOM)
                doc.LoadXml(xml);

                // Set the formatting property of the XML Text Writer to indented
                // the text writer is where the indenting will be performed
                xtw.Formatting = Formatting.Indented;

                // write dom xml to the xmltextwriter
                doc.WriteContentTo(xtw);
                // Flush the contents of the text writer
                // to the memory stream, which is simply a memory file
                xtw.Flush();

                // set to start of the memory stream (file)
                ms.Seek(0, SeekOrigin.Begin);
                // create a reader to read the contents of
                // the memory stream (file)
                StreamReader sr = new StreamReader(ms);
                // return the formatted string to caller
                return sr.ReadToEnd();
            }
            catch
            {
                return xml;
            }

        }

        public static string ToString(XmlNodeList nodes)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                foreach (XmlNode node in nodes)
                {
                    sb.AppendLine(node.OuterXml);
                }

                string s = "<Root>" + sb.ToString() + "</Root>";

                s = Indent(s);

                s = UStr.TrimStart(s, "<Root>");

                s = UStr.TrimEnd(s, "</Root>");

                return s;
            }
            catch
            {
                return "";
            }
        }
    }
}
