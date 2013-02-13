// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System.Web.UI;
using System.ComponentModel;
using System.Drawing;

[assembly: System.Web.UI.WebResource("App.Model.Model.Ui.Web.Controls.Ajax.MutuallyExclusiveCheckBox.MutuallyExclusiveCheckBoxBehavior.js", "text/javascript")]

namespace AjaxControlToolkit
{
    [TargetControlType(typeof(ICheckBoxControl))]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [ClientScriptResource("AjaxControlToolkit.MutuallyExclusiveCheckBoxBehavior", "App.Model.Model.Ui.Web.Controls.Ajax.MutuallyExclusiveCheckBox.MutuallyExclusiveCheckBoxBehavior.js")]
    [Designer(typeof(AjaxControlToolkit.MutuallyExclusiveCheckBoxDesigner))]
    [ToolboxBitmap(typeof(MutuallyExclusiveCheckBoxExtender), "MutuallyExclusiveCheckBox.MutuallyExclusiveCheckBox.ico")]
    public class MutuallyExclusiveCheckBoxExtender : ExtenderControlBase
    {
        /// <summary>
        /// The unique key to use to associate checkboxes. This key does
        /// not respect INamingContainer renaming.
        /// </summary>
        [ExtenderControlProperty]
        [RequiredProperty]
        public string Key
        {
            get { return GetPropertyValue("Key", string.Empty); }
            set { SetPropertyValue("Key", value); }
        }
    }
}