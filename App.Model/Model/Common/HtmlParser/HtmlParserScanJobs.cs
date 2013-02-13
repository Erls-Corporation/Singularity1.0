// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace App.Model
{
    public class HtmlParserScanJobs : HtmlParser
    {
        public HtmlParserScanJobs()
        {
            garbage.Add("&nbsp;<script type=\"text/javascript\"><!--" + Environment.NewLine);
            garbage.Add("google_ad_client = \"pub-5660335780111525\";" + Environment.NewLine);
            garbage.Add("/* 468x15, created 3/6/09 */" + Environment.NewLine);
            garbage.Add("google_ad_slot = \"7164639388\";" + Environment.NewLine);
            garbage.Add("google_ad_width = 468;" + Environment.NewLine);
            garbage.Add("google_ad_height = 15;" + Environment.NewLine);
            garbage.Add("//-->" + Environment.NewLine);
            garbage.Add("</script>" + Environment.NewLine);
            garbage.Add("<script type=\"text/javascript\"" + Environment.NewLine);
            garbage.Add("src=\"http://pagead2.googlesyndication.com/pagead/show_ads.js\">" + Environment.NewLine);
            garbage.Add("</script>");
            garbage.Add("<p>***** ***** *****</p>");
            garbage.Add("/* 468x60, created 3/6/09 */" + Environment.NewLine);
            garbage.Add("google_ad_slot = \"2780990405\";" + Environment.NewLine);
            garbage.Add("google_ad_height = 60;" + Environment.NewLine);
            garbage.Add("<p>");
            garbage.Add("</p>");
            garbage.Add("<b>");
            garbage.Add("</b>");
            garbage.Add("<div>");
            garbage.Add("</div>");
            garbage.Add("<font>");
            garbage.Add("</font>");
            garbage.Add("**************************" + Environment.NewLine);
        }

        #region Properties
        protected override string ItemRegex
        {
            get { return @"(<p>[\*{5}])"; }
        }

        protected override string LastItemEndTag
        {
            get { return @"<div id=""FOOTER"">"; }
        }

        protected override void OnExtract(string text)
        {
            if (!UStr.IsSpaces(text))
            {
                NameValueCollection c = UWeb.ParseQs(Url);

                text += Environment.NewLine + Environment.NewLine + c["paper"] + " " + UStr.PBracket(c["link"]);

                base.OnExtract(text);
            }
        }
        #endregion

    }
}
