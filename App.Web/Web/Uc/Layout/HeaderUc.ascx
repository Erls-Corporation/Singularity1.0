<%@ Control Language="C#" ClassName="HeaderUc" CodeFile="HeaderUc.ascx.cs" Inherits="HeaderUc" %>
<%@ Register Src="~/Web/Uc/Account/LoginBannerUc.ascx" TagName="LoginBannerUc" TagPrefix="uc7" %>
<%@ Register Src="~/Web/Uc/Search/SearchBoxUc.ascx" TagName="SearchBoxUc" TagPrefix="uc2" %>
<%@ Register Src="~/Web/Uc/Gadgets/AddThis.ascx" TagName="AddThis" TagPrefix="uc1" %>
<table class="tfw">
  <tr>
    <td colspan="2" align="right" valign="top">
      <table class="tfw">
        <tr>
          <td align="right" valign="middle">
            <uc7:LoginBannerUc ID="LoginBannerUc1" runat="server" />
          </td>
          <td style="width: 150px" align="right" valign="middle">
            <uc2:SearchBoxUc ID="SearchBoxUc1" runat="server" />
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td>
      <h1>
        <asp:Label ID="lblName" runat="server" ForeColor="#33CC33"></asp:Label>
      </h1>
      <h2>
        <asp:Label ID="lblTitle" runat="server" ForeColor="#666666"></asp:Label>
      </h2>
      <h3>
        <asp:Label ID="lblTitle2" runat="server" ForeColor="#999999"></asp:Label>
      </h3>
    </td>
    <td style="padding-right:25px;" align="right" valign="top">
      <uc1:AddThis ID="AddThis1" runat="server" />
    </td>
  </tr>
  <tr>
    <td colspan="2" align="right" valign="bottom">
      <asp:Label ID="lou" runat="server" CssClass="sg">
      <%= App.Model.UStr.Plural(Application["OnlineUserCount"], "User")%>
      Online | Site Powered By</asp:Label>
      <asp:HyperLink ID="HyperLink1" CssClass="sg" NavigateUrl="http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=27&lid=4&cid=23&iid=2124"
        Text="Singularity" runat="server"></asp:HyperLink>
    </td>
  </tr>
</table>
