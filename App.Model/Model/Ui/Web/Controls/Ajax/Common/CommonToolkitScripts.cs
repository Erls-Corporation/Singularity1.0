// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;


#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("App.Model.Model.Ui.Web.Controls.Ajax.Common.Common.js", "text/javascript")]
#endregion


namespace AjaxControlToolkit
{    
    /// <summary>
    /// This class just exists as a type to get common scripts loaded.  For further info
    /// see Common.js in this folder.
    /// </summary>
    [ClientScriptResource(null, "App.Model.Model.Ui.Web.Controls.Ajax.Common.Common.js")]
    public static class CommonToolkitScripts
    {        
    }
}


