<%@ Control Language="C#" ClassName="SearchDbUc" CodeFile="SearchDbUc.ascx.cs" Inherits="SearchDbUc" %>
<%@ Register Src="~/Web/Uc/Core/AttributesUc.ascx" TagName="AttributesUc" TagPrefix="uc1" %>
<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<table class="tfw">
  <tr>
    <td valign="top" align="left">
      <asp:PlaceHolder ID="ph0" runat="server">
        <h2>
          <asp:Label ID="ln" runat="server"></asp:Label>
        </h2>
      </asp:PlaceHolder>
    </td>
  </tr>
  <tr>
    <td valign="top" align="left">
      <asp:PlaceHolder ID="ph1" Visible="false" runat="server">
        <asp:HyperLink ID="lnkSearchType" runat="server">Advanced Search</asp:HyperLink></asp:PlaceHolder>
    </td>
  </tr>
  <tr>
    <td valign="middle" align="left">
      <asp:PlaceHolder ID="ph2" Visible="false" runat="server">
        <table cellpadding="0" cellspacing="0">
          <tr>
            <td valign="top" align="left">
              <cc1:RsTextBox ID="txtSearch" CssClass="stb" runat="server" Width="400px" CssClassWaterMarkTextFocus="stb"
                CssClassWaterMarkTextBlur="stbb" WaterMarkText="Search" ShowWaterMarkText="false" />
              &nbsp;<asp:Button ID="btnSearch1" CssClass="btn" runat="server" Text="Search" OnClick="btnSearch_Click"
                Width="75px" />
            </td>
          </tr>
          <tr>
            <td>
              <asp:Label ID="lse" CssClass="sg" runat="server"></asp:Label>
            </td>
          </tr>
        </table>
      </asp:PlaceHolder>
      <asp:PlaceHolder ID="ph3" Visible="false" runat="server">
        <uc1:AttributesUc ID="asuc1" runat="server" />
      </asp:PlaceHolder>
    </td>
  </tr>
  <tr>
    <td align="center" valign="middle">
      <asp:PlaceHolder ID="ph4" Visible="false" runat="server">
        <asp:Button ID="btnSearch2" CssClass="btn" runat="server" Text="Search" OnClick="btnSearch_Click"
          Width="75px" />
      </asp:PlaceHolder>
    </td>
  </tr>
</table>
