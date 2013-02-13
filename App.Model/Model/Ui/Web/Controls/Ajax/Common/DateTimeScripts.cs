using System.Web.UI;
using System.Web;
using System.Web.Script.Services;
using AjaxControlToolkit;
using System.Globalization;

[assembly: System.Web.UI.WebResource("App.Model.Model.Ui.Web.Controls.Ajax.Common.DateTime.js", "application/x-javascript")]

namespace AjaxControlToolkit
{
    [RequiredScript(typeof(CommonToolkitScripts))]
    [ClientScriptResource(null, "App.Model.Model.Ui.Web.Controls.Ajax.Common.DateTime.js")]
    public static class DateTimeScripts
    {
    }
}
