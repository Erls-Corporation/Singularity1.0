<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttributeUc.ascx.cs" Inherits="AttributeUc" %>
<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<%@ Register Assembly="Fluent.MultiLineTextBoxValidator" Namespace="Fluent" TagPrefix="cc2" %>
<table class="tfw">
  <tr>
    <td valign="top" align="right">
      <asp:PlaceHolder ID="ph1" runat="server">
        <div class="dtc1">
          <cc1:RsLabel ID="ln" runat="server" Font-Bold="True" />
        </div>
      </asp:PlaceHolder>
    </td>
    <td style="width: 100%;" valign="top" align="left">
      <div class="dtc2">
        <asp:PlaceHolder ID="ph2" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="ph4" Visible="false" runat="server">
          <asp:TextBox CssClass="cc" ID="lo" runat="server" /><span class="cc">
            characters remaining</span></asp:PlaceHolder>
        <asp:PlaceHolder ID="ph5" Visible="false" runat="server">
          <asp:RegularExpressionValidator ID="val1" runat="server" Enabled="false" Visible="false"
            Display="Dynamic" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="ph6" Visible="false" runat="server">
          <asp:RequiredFieldValidator ID="val2" runat="server" Enabled="false" Visible="false"
            Display="Dynamic" />
        </asp:PlaceHolder>
      </div>
    </td>
    <td valign="top">
      <asp:PlaceHolder ID="ph3" runat="server">
        <div class="dtc3">
          <asp:Panel ID="pnl1" runat="server" BackColor="#ffffe0" Width="100%" Height="100%">
            <cc1:RsLabel ID="lhlp" runat="server" /></asp:Panel>
        </div>
      </asp:PlaceHolder>
    </td>
  </tr>
</table>
