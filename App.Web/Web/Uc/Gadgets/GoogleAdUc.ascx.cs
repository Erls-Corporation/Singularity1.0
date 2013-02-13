// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using App.Model; using Attribute = App.Model.Attribute;

public partial class GoogleAdUc : BaseUc
{
    private AdTypeE adType = AdTypeE.One728x90;

    public AdTypeE AdType
    {
        get { return adType; }
        set { adType = value; }
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void InitControl()
    {
        Visible = !Cxt.Service.IsNew;
        if (!Visible)
        {
            return;
        }

        Visible = Config.EnableGoogleAds;
        if (!Visible)
        {
            return;
        }

        Visible = Cxt.Service.ConfigAttributes.ToBool(AttributeE.ServiceEnableGoogleAds);
        if (!Visible)
        {
            return;
        }

        switch (AdType)
        {
            case AdTypeE.One728x90:
                One728x90.Visible = true;
                break;
            case AdTypeE.Two250x250:
                Two250x250.Visible = true;
                break;
            case AdTypeE.One728x15:
                One728x15.Visible = true;
                break;
            case AdTypeE.Two728x15:
                Two728x15.Visible = true;
                break;
            case AdTypeE.Three728x15:
                Three728x15.Visible = true;
                break;
            case AdTypeE.One468x15:
                One468x15.Visible = true;
                break;
            case AdTypeE.Two468x15:
                Two468x15.Visible = true;
                break;
            case AdTypeE.Three468x15:
                Three468x15.Visible = true;
                break;
            case AdTypeE.One120x600:
                One120x600.Visible = true;
                break;
            default:
                throw new Exception("Unknown AdType" + AdType.ToString());
        }
    }
}
