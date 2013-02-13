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

public partial class SearchUc : BaseUc
{    
    #region Data Members
   
    public event SearchClickEventHandler SearchClick = null; 
   
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchDbUc1.SearchClick += new SearchClickEventHandler(SearchDbUc1_SearchClick);
    }

    public void InitControl()
    {
        if (Uc.CategoryID == 0)
        {
            Visible = false;
        }
        else
        {
            SearchDbUc1.Uc.ServiceID = Uc.ServiceID;
            SearchDbUc1.Uc.CategoryID = Uc.CategoryID;
            SearchDbUc1.InitControl();
        }
    }

    void SearchDbUc1_SearchClick(object sender, SearchQuery q)
    {
        if (SearchClick != null)
        {
            SearchClick(this, q);
        }
    }

}