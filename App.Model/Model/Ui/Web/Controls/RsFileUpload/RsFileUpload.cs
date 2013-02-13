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

[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileUpload.Res.bc.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileUpload.Res.be.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileUpload.Res.bs.ico", "image/x-icon")]
[assembly: WebResource("App.Model.Model.Ui.Web.Controls.RsFileUpload.Res.bd.ico", "image/x-icon")]

#endregion

namespace App.Model
{
    [ToolboxData("<{0}:RsFileUpload runat=server />")]
    public class RsFileUpload : WebControl, IRsControl
    {
        #region Data Members
        private RsFileViewer v = null;
        private RsImageButton bd = null;
        private RsImageButton be = null;
        private RsImageButton bs = null;
        private RsImageButton bc = null;
        private FileUpload fu = null;
        public event EventHandler BeforeDelete = null;
        public event EventHandler AfterDelete = null;
        public event EventHandler BeforeEdit = null;
        public event EventHandler AfterEdit = null;
        public event EventHandler BeforeSave = null;
        public event EventHandler AfterSave = null;
        public event EventHandler BeforeCancel = null;
        public event EventHandler AfterCancel = null;
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

        public string UrlFolder
        {
            get
            {
                return UWeb.Vs(ViewState, "UrlFolder");
            }
            set
            {
                ViewState["UrlFolder"] = value;
            }
        }

        [DefaultValue(true)]
        public bool RetainFileNameInEdit
        {
            get
            {
                return UWeb.VsBool(ViewState, "RetainFileNameInEdit", true);
            }
            set
            {
                ViewState["RetainFileNameInEdit"] = value;
            }
        }

        [DefaultValue(false)]
        public bool ShowDeleteButton
        {
            get
            {
                return UWeb.VsBool(ViewState, "ShowDeleteButton", false);
            }
            set
            {
                ViewState["ShowDeleteButton"] = value;
            }
        }

        [DefaultValue(false)]
        public bool ShowEditButton
        {
            get
            {
                return UWeb.VsBool(ViewState, "ShowEditButton", false);
            }
            set
            {
                ViewState["ShowEditButton"] = value;
            }
        } 
        #endregion

        #region Delegated

        public string Url
        {
            get
            {
                EnsureChildControls();
                return v.Url;
            }
            set
            {
                EnsureChildControls();
                v.Url = value;
            }
        }
        
        public override string ToolTip
        {
            get
            {
                EnsureChildControls();
                return v.ToolTip;
            }
            set
            {
                EnsureChildControls();
                v.ToolTip = value;
            }
        }

        public string NoImageUrl
        {
            get
            {
                EnsureChildControls();
                return v.NoImageUrl;
            }
            set
            {
                EnsureChildControls();
                v.NoImageUrl = value;
            }
        }

        public bool ShowNoImage
        {
            get
            {
                EnsureChildControls();
                return v.ShowNoImage;
            }
            set
            {
                EnsureChildControls();
                v.ShowNoImage = value;
            }
        }
        #endregion

        #region Controls
        public RsFileViewer FileViewer { get { return v; } }
        public RsImageButton ButtonDelete { get { return bd; } }
        public RsImageButton ButtonEdit { get { return bd; } }
        public FileUpload FileUpload { get { return fu; } } 
        #endregion

        #region Calculated
        
        public string PostedFileName
        {
            get
            {
                if (fu.PostedFile != null && fu.PostedFile.ContentLength > 0)
                {
                    return UFile.GetFileName(fu.PostedFile.FileName);
                }

                return "";
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

            v.RenderControl(w);
            fu.RenderControl(w);
            bd.RenderControl(w);
            be.RenderControl(w);
            bs.RenderControl(w);
            bc.RenderControl(w);
        }

        protected override void CreateChildControls()
        {
            v = new RsFileViewer();
            v.ID = UWeb.GetUniqueName(this, "v");
            Controls.Add(v);

            be = new RsImageButton();
            be.ID = UWeb.GetUniqueName(this, "be");
            be.CommandName = be.ToolTip = "Edit";
            be.ImageUrl = UWeb.ResUrl(this, "be.ico");
            be.Click += new ImageClickEventHandler(be_Click);
            Controls.Add(be);

            bd = new RsImageButton();
            bd.ID = UWeb.GetUniqueName(this, "bd");
            bd.CommandName = bd.ToolTip = "Delete";
            bd.ImageUrl = UWeb.ResUrl(this, "bd.ico");
            bd.Click += new ImageClickEventHandler(bd_Click);
            bd.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');");
            Controls.Add(bd);

            bs = new RsImageButton();
            bs.ID = UWeb.GetUniqueName(this, "bs");
            bs.CommandName = bs.ToolTip = "Save";
            bs.ImageUrl = UWeb.ResUrl(this, "bs.ico");
            bs.Click += new ImageClickEventHandler(bs_Click);
            bs.Visible = false;
            Controls.Add(bs);

            bc = new RsImageButton();
            bc.ID = UWeb.GetUniqueName(this, "bc");
            bc.CommandName = bc.ToolTip = "Cancel";
            bc.ImageUrl = UWeb.ResUrl(this, "bc.ico");
            bc.Click += new ImageClickEventHandler(bc_Click);
            bc.Visible = false;
            Controls.Add(bc);

            fu = new FileUpload();
            fu.Width = new Unit(200, UnitType.Pixel);
            fu.ID = UWeb.GetUniqueName(this, "fu");
            fu.Visible = false;
            Controls.Add(fu);
        }

        void bc_Click(object sender, ImageClickEventArgs e)
        {
            Cancel();
        }

        void bs_Click(object sender, ImageClickEventArgs e)
        {
            Save();
        }

        void bd_Click(object sender, ImageClickEventArgs e)
        {
            Delete();
        }

        void be_Click(object sender, ImageClickEventArgs e)
        {
            Edit();
        }

        #endregion

        #region IRsControl Members

        public virtual ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            SaveAs(ia.FilePath());

            list.Add(ia);

            return ia;
        }

        public virtual void SetValue(LayoutE layoutID, ItemAttribute ia)
        {
            if (layoutID == LayoutE.Edit)
            {
                ShowEditButton = true;
                ShowDeleteButton = true;
            }

            SetValue(ia.UrlFile());
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

        public virtual void SaveAs(string filePath)
        {
            if (fu.PostedFile != null && fu.PostedFile.ContentLength > 0)
            {
                UFile.RemoveReadOnly(filePath);

                UFile.CreateFolderFromFilePath(filePath);

                fu.PostedFile.SaveAs(filePath);
            }
        }

        public virtual void SetValue(string url)
        {
            SetValue(url, UWeb.GetFileName(url));
        }

        public virtual void SetValue(string url, string toolTip)
        {
            EnsureChildControls();

            v.SetValue(url, toolTip);

            ShowToolBar();
        } 
        #endregion

        #region Helpers

        #region ToolBar
        private void ShowToolBar()
        {
            if (bs.Visible)
            {
                return;
            }

            bd.Visible = ShowDeleteButton && UWeb.UrlExists(Url);
            be.Visible = ShowEditButton;
        }

        private void Cancel()
        {
            if (BeforeCancel != null)
            {
                BeforeCancel(this, EventArgs.Empty);
            }

            bs.Visible = bc.Visible = false;
            be.Visible = bd.Visible = true;
            fu.Visible = false;

            SetValue(Url, ToolTip);

            if (AfterCancel != null)
            {
                AfterCancel(this, EventArgs.Empty);
            }
        }

        private void Delete()
        {
            if (BeforeDelete != null)
            {
                BeforeDelete(this, EventArgs.Empty);
            }

            UFile.Delete(UWeb.MapPath(Url));

            SetValue(Url, ToolTip);

            if (AfterDelete != null)
            {
                AfterDelete(this, EventArgs.Empty);
            }
        }

        private void Edit()
        {
            if (BeforeEdit != null)
            {
                BeforeEdit(this, EventArgs.Empty);
            }

            bs.Visible = bc.Visible = true;
            be.Visible = bd.Visible = false;
            fu.Visible = true;

            if (AfterEdit != null)
            {
                AfterEdit(this, EventArgs.Empty);
            }
        }

        private void Save()
        {
            if (BeforeSave != null)
            {
                BeforeSave(this, EventArgs.Empty);
            }

            bs.Visible = bc.Visible = false;
            be.Visible = bd.Visible = true;
            fu.Visible = false;

            if (PostedFileName != "")
            {
                string path = UWeb.MapPath(Url);

                if (path == "")
                {
                    path = UrlFolder;
                }
                {
                    UFile.Delete(path);
                }

                if (!RetainFileNameInEdit)
                {
                    path = UFile.GetFolder(path) + PostedFileName;
                }

                SaveAs(path);
            }

            SetValue(Url, ToolTip);

            if (AfterSave != null)
            {
                AfterSave(this, EventArgs.Empty);
            }
        } 
        #endregion

        
        #endregion
    }
}
