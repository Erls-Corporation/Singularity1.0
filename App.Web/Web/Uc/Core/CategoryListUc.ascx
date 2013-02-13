<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryListUc.ascx.cs"
  Inherits="CategoryListUc" %>
<%@ Register Src="CategoryTaskUc.ascx" TagName="CategoryTaskUc" TagPrefix="uc1" %>
<table class="tfw">
  <tr>
    <td align="left" valign="top">
      <asp:GridView ID="gv1" runat="server" Width="100%" ShowHeader="False" AutoGenerateColumns="False"
        GridLines="None" OnRowDataBound="gv1_RowDataBound">
        <Columns>
          <asp:TemplateField ItemStyle-VerticalAlign="Top">
            <ItemTemplate>
              <table cellpadding="3" class="tfw">
                <tr>
                  <td style="width: 10px;" rowspan="5" valign="top">
                    <h2>
                      <asp:Image ID="imgc" runat="server" Height="16px" Width="16px" /></h2>
                  </td>
                  <td colspan="2">
                    <h2>
                      <asp:HyperLink ID="lnkm" runat="server" />
                    </h2>
                    <uc1:CategoryTaskUc ID="ctuc1" runat="server" />
                  </td>
                </tr>
              </table>
            </ItemTemplate>
            <ItemStyle VerticalAlign="Top"></ItemStyle>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </td>
  </tr>
</table>
