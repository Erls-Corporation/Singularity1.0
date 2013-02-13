<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceListUc.ascx.cs"
  Inherits="ServiceListUc" %>
<%--<%@ Register Src="~/Web/Uc/Search/SearchUc.ascx" TagName="SearchUc" TagPrefix="uc2" %>
--%><table>
  <tr>
    <td align="center">
      &nbsp;
     <%-- <uc2:SearchUc ID="SearchUc1" runat="server" />--%>
    </td>
  </tr>
  <tr>
    <td colspan="2" valign="middle" align="right">
      <p />
    </td>
  </tr>
  <tr>
    <td colspan="2" valign="middle" align="left">
      <asp:GridView ID="gv1" runat="server" Width="100%" ShowHeader="False" AutoGenerateColumns="False"
        GridLines="None" OnRowDataBound="gv1_RowDataBound">
        <AlternatingRowStyle BackColor="#F7FBED" />
        <Columns>
          <asp:TemplateField>
            <ItemTemplate>
              <table>
                <tr>
                  <td align="left" valign="middle">
                    <div class="dc1">
                      <asp:HyperLink ID="lnks1" runat="server">
                        <asp:ImageButton ID="imgs" runat="server" Height="50px" Width="50px" />
                      </asp:HyperLink>
                    </div>
                  </td>
                  <td align="left" valign="top">
                    <h2><asp:HyperLink ID="lnks2" runat="server" /></h2>
                    <p>
                      <asp:Label ID="ld" runat="server" Text="Label"></asp:Label></p>
                  </td>
                </tr>
              </table>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </td>
  </tr>
</table>
