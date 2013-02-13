<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailUc.ascx.cs" Inherits="EmailUc" %>
<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<style type="text/css">
  .style1
  {
    width: 45px;
  }
</style>
<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label><p />
<table class="tfw">
  <tr>
    <td valign="top" align="right" class="style1">
      <b>To:</b>
    </td>
    <td>
      <cc1:RsTextArea ID="RsTextArea1" Width="100%" Height="50px" runat="server"></cc1:RsTextArea>
    </td>
  </tr>
  <tr>
    <td valign="top" align="right" class="style1">
      <b>Cc:</b>
    </td>
    <td>
      <cc1:RsTextArea ID="RsTextArea2" Width="100%" Height="50px" runat="server"></cc1:RsTextArea>
    </td>
  </tr>
  <tr>
    <td valign="top" align="right" class="style1">
      <b>Bcc:</b>
    </td>
    <td>
      <cc1:RsTextArea ID="RsTextArea3" Width="100%" Height="50px" runat="server"></cc1:RsTextArea>
    </td>
  </tr>
  <tr>
    <td valign="top" align="right" class="style1">
      <b>Subject:</b>
    </td>
    <td>
      <cc1:RsTextBox ID="RsTextBox1" Width="100%" runat="server" />
    </td>
  </tr>
  <tr>
    <td valign="top" align="right" class="style1">
      <b>Body:</b>
    </td>
    <td>
      <cc1:RsRichTextBox ID="RsRichTextBox1" Width="100%" Height="400px" runat="server" />
    </td>
  </tr>
  <tr>
    <td valign="top" align="right" class="style1">
    </td>
    <td>
      <asp:Button ID="btnSend" CssClass="btn" Width="75px" runat="server" Text="Send" OnClick="btnSend_Click" />
    </td>
  </tr>
</table>
