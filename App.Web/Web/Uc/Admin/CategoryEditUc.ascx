<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryEditUc.ascx.cs"
  Inherits="CategoryEditUc" %>
<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<table class="tfw">
  <tr>
    <td class="dtc1" valign="top">
      <b>Category Image:</b>
    </td>
    <td>
      <cc1:RsFileUpload ID="RsFileUpload1" runat="server" />
    </td>
  </tr>
  <tr>
    <td class="dtc1" valign="top">
      <b>Category Attribute:</b>
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
                <asp:TemplateField HeaderText="Attribute">
                  <ItemTemplate>
                    <cc1:RsLabel ID="lblAttributeID" Visible="false" runat="server" />
                    <cc1:RsHyperLink ID="lnkName" runat="server" />
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
