// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using App.Model;

public partial class UploadUc : BaseUc
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cmbTable.DataValueField = "AttributeTableID";
            cmbTable.DataTextField = "Name";
            cmbTable.DataSource = BaseCollection.ExecuteSql("select * from [AttributeTable]");
            cmbTable.DataBind();
        }
    }

    public void InitControl()
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Upload();
    }

    public void Upload()
    {
        try
        {
            lblMsg.Text = "";

            UploadZip();

            UploadXml();

            Category.UpdateStats(Cxt);
        }
        catch (Exception ex)
        {
            lblMsg.Text = AppException.GetError(ex);
        }
    }

    private void UploadZip()
    {
        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0)
        {
            string fileZip = Config.FolderUploadsZip + FileUpload1.PostedFile.FileName;

            UFile.Delete(fileZip);

            FileUpload1.PostedFile.SaveAs(fileZip);

            ItemAttributes.UploadZip(Cxt, AttributeTable.GetRsOneItemTable(BaseItem.ToInt32(cmbTable.SelectedValue)), fileZip);

            lblMsg.Text = "File uploaded successfully";
        }
    }

    private void UploadXml()
    {
        if (txtXml.Text != "")
        {
            ItemAttributes.UploadXml(Cxt, AttributeTable.GetRsOneItemTable(BaseItem.ToInt32(cmbTable.SelectedValue)), txtXml.Text);

            lblMsg.Text = "Xml uploaded successfully";
        }
    }
}
