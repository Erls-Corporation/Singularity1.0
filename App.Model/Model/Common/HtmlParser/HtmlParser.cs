// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;

namespace App.Model
{
    public class HtmlParser
    {
        #region Data Members
        StringBuilder pageHtml = new StringBuilder();
        string url = "";
        Exception error = null;
        public delegate void TextExtractedEventHandler(object sender, string text);
        public event TextExtractedEventHandler TextExtracted = null;
        protected ArrayList garbage = new ArrayList();
        #endregion

        #region Properties
        public string PageHtml
        {
            get { return pageHtml.ToString(); }
        }

        public string Url
        {
            get { return url; }
        }

        public Exception Error
        {
            get { return error; }
        }

        protected virtual string ItemRegex
        {
            get { return ""; }
        }

        protected virtual string LastItemEndTag
        {
            get { return ""; }
        }


        #endregion

        #region Parse
        public virtual bool Parse(string pageUrls)
        {
            bool result = true;

            string[] urls = UStr.Split(pageUrls, Environment.NewLine);

            foreach (string urlx in urls)
            {
                result = ParseUrl(urlx);
            }

            return result;
        }

        public virtual bool ParseUrl(string pageUrl)
        {
            try
            {
                if (UStr.IsSpaces(pageUrl))
                {
                    return true;
                }

                if (DoParse(pageUrl))
                {
                    Extract();
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex;

                return false;
            }
        }

        protected virtual void Extract()
        {
            try
            {
                Regex r = new Regex(ItemRegex, RegexOptions.Multiline);

                MatchCollection mc = r.Matches(PageHtml);

                if (mc.Count == 0)
                {
                    return;
                }

                string text = "";

                for (int i = 0; i < mc.Count; i++)
                {
                    if (i < mc.Count - 2)
                    {
                        text = PageHtml.Substring(mc[i].Index, mc[i + 1].Index - mc[i].Index);
                    }
                    else
                    {
                        text = PageHtml.Substring(mc[i].Index, PageHtml.IndexOf(LastItemEndTag, mc[i].Index) - mc[i].Index);
                    }

                    text = RemoveGarbage(text);

                    OnExtract(text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string RemoveGarbage(string text)
        {
            foreach (string g in garbage)
            {
                text = text.Replace(g, "");
            }

            return text;
        }

        protected virtual void OnExtract(string text)
        {
            if (TextExtracted != null && !UStr.IsSpaces(text))
            {
                TextExtracted(this, text.Trim());
            }
        }

        private bool DoParse(string pageUrl)
        {
            url = pageUrl;

            pageHtml.Length = 0;

            // used on each read operation
            byte[] buf = new byte[8192];

            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // continue building the string
                    pageHtml.Append(Encoding.ASCII.GetString(buf, 0, count));
                }
            }
            while (count > 0); // any more data to read?

            return true;
        }
        #endregion
    }
}
