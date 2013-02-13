<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadUc.ascx.cs" Inherits="UploadUc" %>
<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label><p />
<asp:DropDownList ID="cmbTable" runat="server">
</asp:DropDownList>
<p />
<asp:FileUpload ID="FileUpload1" Width="498px" runat="server" />
<br />
<asp:TextBox ID="txtXml" runat="server" Columns="75" Rows="10" TextMode="MultiLine"></asp:TextBox>
<br />
<asp:Button ID="btnUpload" CssClass="btn" Width="75px" runat="server" Text="Upload"
  OnClick="btnUpload_Click" />
