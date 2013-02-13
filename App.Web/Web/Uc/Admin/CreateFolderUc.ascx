<%@  Control Language="C#" AutoEventWireup="true" CodeFile="CreateFolderUc.ascx.cs"
  Inherits="CreateFolderUc" %>
<table class="tfw">
  <tr class="st1">
    <td>
      <asp:label id="lblName" runat="server"></asp:label>
    </td>
  </tr>
  <tr>
    <td>
      <asp:label id="lblMsg" runat="server" font-bold="true" text="" forecolor="Red" font-size="Larger"></asp:label>
      <p />
      <h2>
        1. Upload file in folder ~\Web\u\z\</h2>
      <asp:fileupload id="FileUpload1" width="498px" runat="server" />
      <br />
      <asp:button id="btnUpload" cssclass="btn" width="75px" runat="server" text="Upload File"
        onclick="btnUpload_Click" />
      <p />
      <p />
      <h2>
        2. Create sub-folder under ~\Web\u\</h2>
      Enter sub-folder name: &nbsp;<asp:textbox id="txtFolder" runat="server" text="test123"></asp:textbox>
      &nbsp;<asp:button id="btnCreate" cssclass="btn" runat="server" text="Create Sub-Folder"
        onclick="btnCreate_Click" />
  </tr>
</table>
