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

public partial class CreateFolderUc : BaseUc
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0)
            {
                string fileZip = Config.FolderUploadsZip + FileUpload1.PostedFile.FileName;

                UFile.Delete(fileZip);

                FileUpload1.PostedFile.SaveAs(fileZip);

                lblMsg.Text = "File uploaded successfully at " + UStr.Bracket(fileZip);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = AppException.GetError(ex);
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Config.FolderUploads + txtFolder.Text;

            UFile.CreateFolder(path);

            lblMsg.Text = "Folder [" + path + " ] created successfully";
        }
        catch (Exception ex)
        {
            lblMsg.Text = AppException.GetError(ex);
        }
    }
}
