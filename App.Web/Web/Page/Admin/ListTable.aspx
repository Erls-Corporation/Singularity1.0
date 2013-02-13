<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="List Table"
  CodeFile="ListTable.aspx.cs" Inherits="ListTable" ValidateRequest="false" %>

<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <table class="tfw">
      <tr class="st1">
        <td>
          <asp:Label ID="lblName" runat="server"></asp:Label>
        </td>
      </tr>
      <tr>
        <td>
          <p />
          
            <asp:HyperLink ID="lnkAdd" runat="server">Add New</asp:HyperLink> |
          <asp:LinkButton ID="lbGoBack" runat="server" OnClick="lbGoBack_Click">Cancel</asp:LinkButton>
          <asp:LinkButton ID="lbClone" Visible="false" runat="server" OnClick="lbClone_Click">Copy</asp:LinkButton>
          <p />
        </td>
      </tr>
      <tr>
        <td>
          <cc1:RsGridView ID="RsGridView1" GridLines="None" BackColor="#fefefe" AutoGenerateColumns="False"
            OnRowDataBound="RsGridView1_RowDataBound" ShowCheckBox="false" runat="server">
            <AlternatingRowStyle BackColor="#F7FBED" />
            <Columns>
              <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                  <cc1:RsLabel ID="lblID" Visible="false" runat="server" />
                  <cc1:RsHyperLink ID="lnkName" runat="server" />
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </cc1:RsGridView>
        </td>
      </tr>
    </table>
  </div>
</asp:Content>
