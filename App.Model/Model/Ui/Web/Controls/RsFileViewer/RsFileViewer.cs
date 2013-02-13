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

#region Res

[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res._f.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.txt.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.bmp.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.doc.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.docx.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.exe.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.gif.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.hlp.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.img.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.jpg.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.lnk.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.mdb.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.mdf.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.msg.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.msi.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.pdf.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.pic.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.png.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.ppt.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.ra.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.rar.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.reg.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.rtf.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.tif.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.wmv.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.xls.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.ni.jpg", "image/jpeg")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileViewer.Res.nis.jpg", "image/jpeg")]

#endregion

namespace App.Model
{
    [ToolboxData("<{0}:RsFileViewer runat=server />")]
    public class RsFileViewer : WebControl, IRsControl
    {
        #region Data Members
        private RsImageButton img = null;
        private RsHyperLink lnk = null;

        #endregion

        #region Properties

        #region Settings
        public string Tag
        {
            get
            {
                return UWeb.Vs(ViewState, "Tag");
            }
            set
            {
                ViewState["Tag"] = value;
            }
        }

        [DefaultValue(true)]
        public bool ShowNoImage
        {
            get
            {
                return UWeb.VsBool(ViewState, "ShowNoImage", true);
            }
            set
            {
                ViewState["ShowNoImage"] = value;
            }
        }

        public string NoImageUrl
        {
            get
            {
                return UWeb.Vs(ViewState, "NoImageUrl");
            }
            set
            {
                ViewState["NoImageUrl"] = value;
            }
        }

        #endregion

        #region Controls
        public RsImageButton Image { get { return img; } }

        #endregion

        #region Calculated
        public string Url
        {
            get
            {
                EnsureChildControls();
                return lnk.NavigateUrl;
            }
            set
            {
                EnsureChildControls();
                lnk.NavigateUrl = value;
            }
        }

        public override string ToolTip
        {
            get
            {
                EnsureChildControls();
                return lnk.ToolTip;
            }
            set
            {
                EnsureChildControls();
                lnk.ToolTip = value;
            }
        }
        #endregion
        #endregion

        #region Overrides

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter w)
        {
            EnsureChildControls();

            Table t = new Table();
            t.BorderWidth = 0;
            t.CellPadding = 0;
            t.CellSpacing = 0;

            TableRow r = null; 
            TableCell c = null;

            r = new TableRow();
            c = new TableCell();
            c.Controls.Add(img);
            r.Cells.Add(c);
            t.Rows.Add(r);

            r = new TableRow();
            c = new TableCell();
            c.Controls.Add(lnk);
            r.Cells.Add(c);
            t.Rows.Add(r);

            t.RenderControl(w);
        }

        protected override void CreateChildControls()
        {
            img = new RsImageButton();
            img.ID = UWeb.GetUniqueName(this, "img");
            img.ImageUrl = UWeb.ResUrl(this, "img.ico");
            Controls.Add(img);

            lnk = new RsHyperLink();
            lnk.ID = UWeb.GetUniqueName(this, "lnk");
            lnk.NavigateUrl = UWeb.ResUrl(this, "lnk.ico");
            Controls.Add(img);
        }

        #endregion

        #region IRsControl Members

        public virtual ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            list.Add(ia);

            return ia;
        }

        public virtual void SetValue(LayoutE layoutID, ItemAttribute ia)
        {
            SetValue(ia.UrlFile(ShowNoImage));
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

        #region Methods

        public virtual void SetValue(string url)
        {
            SetValue(url, UWeb.GetFileName(url));
        }

        public virtual void SetValue(string url, string toolTip)
        {
            EnsureChildControls();

            Url = url;
            ToolTip = toolTip;

            if (!UWeb.UrlExists(url) && !ShowNoImage)
            {
                img.Visible = false;
            }
            else
            {
                img.Visible = true;
                img.ToolTip = toolTip;
                SetUi(url, toolTip);
            }

            ShowToolBar();
        }

        private void SetUi(string url, string toolTip)
        {
            img.ImageUrl = UrlFile(url);

            if (img.ImageUrl == "" && url != "")
            {
                if (toolTip == "")
                {
                    toolTip = UWeb.GetFileName(url);
                }

                img.ImageUrl = UWeb.ResUrl(this, GetIconFileName(url));
                img.ToolTip = toolTip;
            }

            lnk.Text = toolTip;
            lnk.NavigateUrl = url;
            lnk.ToolTip = toolTip;
        }

        private string GetIconFileName(string url)
        {
            switch (UWeb.GetExtension(url))
            {
                case ".bmp": return "bmp.ico";
                case ".doc": return "doc.ico";
                case ".docx": return "docx.ico";
                case ".exe": return "exe.ico";
                case ".gif": return "gif.ico";
                case ".hlp": return "hlp.ico";
                case ".img": return "img.ico";
                case ".jpg": return "jpg.ico";
                case ".lnk": return "lnk.ico";
                case ".mdb": return "mdb.ico";
                case ".mdf": return "mdf.ico";
                case ".msg": return "msg.ico";
                case ".msi": return "msi.ico";
                case ".pdf": return "pdf.ico";
                case ".pic": return "pic.ico";
                case ".png": return "png.ico";
                case ".ppt": return "ppt.ico";
                case ".ra": return "ra.ico";
                case ".rar": return "rar.ico";
                case ".reg": return "reg.ico";
                case ".rtf": return "rtf.ico";
                case ".tif": return "tif.ico";
                case ".wmv": return "wmv.ico";
                case ".xls": return "xls.ico";
                case ".txt": return "txt.ico";
                default:
                    return "_f.ico";
            }
        } 
        #endregion

        #region Helpers

        #region ToolBar
        private void ShowToolBar()
        {

        }

        #endregion 
        
        #region UrlFile

        private string UrlFile(string url)
        {
            if (!UWeb.UrlExists(url) && ShowNoImage)
            {
                if (NoImageUrl == "")
                {
                    return UWeb.ResUrl(this, "nis.jpg");
                }

                return NoImageUrl;
            }

            if (UWeb.IsImage(url))
            {
                return url;
            }

            return "";
        }

        #endregion

        #endregion
    }
}
