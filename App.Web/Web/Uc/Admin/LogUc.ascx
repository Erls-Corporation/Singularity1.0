<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogUc.ascx.cs" Inherits="LogUc" %>
<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn" Width="75px"
  onclick="btnRefresh_Click" />
&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" Width="75px"
  onclick="btnClear_Click" />
<p />
<asp:GridView ID="GridView1" runat="server" Width="100%" ShowHeader="False"
  OnRowDataBound="GridView1_RowDataBound" GridLines="None" AllowPaging="True" 
  AllowSorting="True" onselectedindexchanging="GridView1_SelectedIndexChanging">
</asp:GridView>


