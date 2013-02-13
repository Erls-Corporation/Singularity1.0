<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TableComboUc.ascx.cs"
  Inherits="TableComboUc" %>
<table>
  <tr>
    <td valign="top">
      Select Table:
    </td>
    <td>
      <asp:DropDownList ID="DropDownList1" runat="server"/>
        &nbsp;<asp:Button ID="btnSelect" runat="server" CssClass="btn" Text="Go" Width="75px"
          OnClick="btnSelect_Click" /> &nbsp;<asp:Button ID="btnAdd" runat="server" 
        CssClass="btn" Text="Add New" Width="75px" onclick="btnAdd_Click"
          />
    </td>
  </tr>
</table>
