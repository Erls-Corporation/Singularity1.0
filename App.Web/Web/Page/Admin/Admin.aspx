<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="Admin Page"
  CodeFile="Admin.aspx.cs" Inherits="Admin" ValidateRequest="false" %>

<%@ Register Src="~/Web/Uc/Admin/UploadUc.ascx" TagName="UploadUc" TagPrefix="uc1" %>
<%@ Register Src="~/Web/Uc/Admin/LogUc.ascx" TagName="LogUc" TagPrefix="uc2" %>
<%@ Register Src="~/Web/Uc/Admin/EmailUc.ascx" TagName="EmailUc" TagPrefix="uc4" %>
<%@ Register Src="~/Web/Uc/Admin/TaskUc.ascx" TagName="TaskUc" TagPrefix="uc5" %>
<%@ Register Src="~/Web/Uc/Admin/TableComboUc.ascx" TagName="TableComboUc" TagPrefix="uc3" %>
<%@ Register Src="~/Web/Uc/Admin/CreateFolderUc.ascx" TagName="CreateFolderUc" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <table class="tfw">
      <tr class="st1">
        <td>
          General Tasks
        </td>
      </tr>
      <tr>
        <td>
          <uc3:TableComboUc ID="TableComboUc1" runat="server" />
        </td>
      </tr>
      <tr class="st1">
        <td>
          Run Tasks
        </td>
      </tr>
      <tr>
        <td>
          <uc5:TaskUc ID="TaskUc1" runat="server" />
          <p />
        </td>
      </tr>
      <tr class="st1">
        <td>
          Upload
        </td>
      </tr>
      <tr>
        <td>
          <uc1:UploadUc ID="UploadUc1" runat="server" />
          <p />
        </td>
      </tr>
      <tr class="st1">
        <td>
          Email
        </td>
      </tr>
      <tr>
        <td>
          <uc4:EmailUc ID="EmailUc1" runat="server" />
          <p />
        </td>
      </tr>
      <tr class="st1">
        <td>
          Create Folders & Upload Files
        </td>
      </tr>
      <tr>
        <td>
          <uc6:CreateFolderUc ID="CreateFolderUc1" runat="server" />
        </td>
      </tr>
      <tr class="st1">
        <td>
          Log
        </td>
      </tr>
      <tr>
        <td>
          <uc2:LogUc ID="LogUc1" runat="server" />
        </td>
      </tr>
    </table>
  </div>
</asp:Content>
