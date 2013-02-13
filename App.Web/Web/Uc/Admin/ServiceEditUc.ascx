<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceEditUc.ascx.cs"
  Inherits="ServiceEditUc" %>
<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<table class="tfw">
  <tr>
    <td class="dtc1" valign="top">
      <b>Service Image:</b>
    </td>
    <td>
      <cc1:RsFileUpload ID="RsFileUpload1" runat="server" />
    </td>
  </tr>
  <tr>
    <td class="dtc1" valign="top">
      <b>Service Categories:</b>
    </td>
    <td>
      <table>
        <tr>
          <td>
            <p />
            <b>
              <asp:HyperLink ID="lnkAdd" Visible="false" runat="server">Add New</asp:HyperLink></b><p />
          </td>
        </tr>
        <tr>
          <td>
            <cc1:RsGridView ID="RsGridView1" GridLines="None" BackColor="#fefefe" AutoGenerateColumns="False"
              OnRowDataBound="RsGridView1_RowDataBound" runat="server">
              <AlternatingRowStyle BackColor="#F7FBED" />
              <Columns>
                <asp:TemplateField HeaderText="Category">
                  <ItemTemplate>
                    <cc1:RsLabel ID="lblCategoryID" Visible="false" runat="server" />
                    <cc1:RsHyperLink ID="lnkName" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sequence">
                  <ItemTemplate>
                    <cc1:RsTextBox ID="txtSequence" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Default">
                  <ItemTemplate>
                    <cc1:RsRadioButton ID="rdoDefault" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </cc1:RsGridView>
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
