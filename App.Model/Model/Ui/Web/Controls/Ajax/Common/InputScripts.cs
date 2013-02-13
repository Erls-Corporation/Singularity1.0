using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

[assembly: System.Web.UI.WebResource("App.Model.Model.Ui.Web.Controls.Ajax.Common.Input.js", "application/x-javascript")]

namespace AjaxControlToolkit
{
    [RequiredScript(typeof(CommonToolkitScripts))]
    [ClientScriptResource(null, "App.Model.Model.Ui.Web.Controls.Ajax.Common.Input.js")]
    public static class InputScripts
    {
    }
}
