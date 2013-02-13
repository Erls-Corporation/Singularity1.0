<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttributesUc.ascx.cs"
  Inherits="AttributesUc" %>
<%@ Register Src="~/Web/Uc/Core/AttributeUc.ascx" TagName="AttributeUc" TagPrefix="uc1" %>
<%@ Register Src="~/Web/Uc/Core/ItemStatUc.ascx" TagName="ItemStatUc" TagPrefix="uc2" %>
<table class="tfw">
  <tr>
    <td colspan="2" align="right" valign="top">
      <asp:PlaceHolder ID="ph1" runat="server" Visible="false">
        <asp:HyperLink ID="be" runat="server" Text="Edit" /><asp:LinkButton ID="bd" ToolTip="Delete"
          runat="server" Text=" | Delete" OnCommand="bd_Command" /></asp:PlaceHolder>
    </td>
  </tr>
  <tr>
    <td colspan="2" valign="top">
      <asp:GridView ID="gv1" runat="server" Width="100%" ShowHeader="False" AutoGenerateColumns="False"
        GridLines="None" OnRowDataBound="gv1_RowDataBound">
        <Columns>
          <asp:TemplateField>
            <ItemTemplate>
              <asp:Label ID="laid" runat="server" Text='<%# Eval("AttributeID") %>' Visible="False" />
              <uc1:AttributeUc ID="auc1" runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </td>
  </tr>
  <tr>
    <td align="left" valign="top">
      <asp:PlaceHolder ID="ph3" runat="server" Visible="false">
        <table cellpadding="3" class="sg">
          <tr>
            <td valign="top">
              <asp:Image ID="imgc" Visible="false" runat="server" Height="16px" Width="16px" />
            </td>
            <td valign="top">
              <asp:HyperLink ID="lnks" runat="server" CssClass="sg" />
              &gt;
              <asp:HyperLink ID="lnkc" runat="server" CssClass="sg" />
            </td>
          </tr>
        </table>
      </asp:PlaceHolder>
    </td>
    <td align="right" valign="top">
      <asp:PlaceHolder ID="ph2" runat="server">
        <uc2:ItemStatUc ID="ituc1" runat="server" />
      </asp:PlaceHolder>
    </td>
  </tr>
</table>
