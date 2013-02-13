<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskUc.ascx.cs" Inherits="TaskUc" %>
<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label><p />
<table>
  <tr>
    <td valign="top">
      Select Task:
    </td>
    <td>
      <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Selected="True" Value="0">Update Stats</asp:ListItem>
        <asp:ListItem Value="1">Send Activate Account Email To Inactive Users</asp:ListItem>
        <asp:ListItem Value="2">Reload Config</asp:ListItem>
      </asp:DropDownList>
      &nbsp;<asp:Button ID="btnRun" runat="server" CssClass="btn" Text="Run Task" Width="75px"
        OnClick="btnRun_Click" />
    </td>
  </tr>
</table>
