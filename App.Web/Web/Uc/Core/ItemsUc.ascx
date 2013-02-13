<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsUc.ascx.cs" Inherits="ItemsUc" %>
<%@ Register Src="~/Web/Uc/Core/AttributesUc.ascx" TagName="AttributesUc" TagPrefix="uc1" %>
<table class="tfw">
  <tr>
    <td valign="top">
      <table class="tfw">
        <tr>
          <td>
            <asp:Label ID="lc" CssClass="lc" runat="server" Visible="False" />
          </td>
          <td align="right">
            <asp:Label ID="lp" runat="server" Font-Italic="true" Visible="False" />
          </td>
        </tr>
      </table>
      <p />
      <asp:GridView ID="gv1" runat="server" Width="100%" ShowHeader="False" AutoGenerateColumns="False"
        GridLines="None" OnRowDataBound="gv1_RowDataBound" BackColor="#fefefe" AllowPaging="True"
        OnPageIndexChanging="gv1_PageIndexChanging" PageSize="10">
        <PagerStyle HorizontalAlign="Center" Font-Size="16" BackColor="#F0F0F0" />
        <AlternatingRowStyle BackColor="#F7FBED" />
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
          NextPageText=" Next &gt;" PreviousPageText="&lt; Previous " />
        <Columns>
          <asp:TemplateField>
            <ItemTemplate>
              <uc1:AttributesUc ID="asuc1" runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </td>
  </tr>
</table>
