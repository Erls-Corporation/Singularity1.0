 <%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryTaskUc.ascx.cs"
  Inherits="CategoryTaskUc" %>
<table class="tfw">
  <tr>
    <td align="left" valign="top">
      <asp:GridView ID="gv1" runat="server" Width="100%" ShowHeader="False" AutoGenerateColumns="False"
        GridLines="None" OnRowDataBound="gv1_RowDataBound">
        <Columns>
          <asp:TemplateField ItemStyle-VerticalAlign="Top">
            <ItemTemplate>
              <asp:HyperLink ID="lnkt" runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </td>
  </tr>
</table>
