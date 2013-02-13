<%@ Page Language="c#" MasterPageFile="~/Web/Page/Default.master" CodeFile="Default.aspx.cs"
  EnableEventValidation="false" Inherits="_Default" %>

<%@ Register Src="~/Web/Uc/Core/ServiceListUc.ascx" TagName="ServiceListUc" TagPrefix="uc5" %>
<%@ Register Src="~/Web/Uc/Search/SearchLatestUc.ascx" TagName="SearchLatestUc" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <table class="tfw">
      <tr>
        <td align="left" valign="top">
          <h1 class="h1m">
            Latest Articles</h1>
        </td>
        <td style="width: 35%;" align="left" valign="top">
          <h1 class="h1m">
            Services</h1>
        </td>
      </tr>
      <tr>
        <td align="center" valign="top">
          <uc1:SearchLatestUc ID="SearchLatestUc1" runat="server" />
        </td>
        <td style="border-left: 1px solid #e9e9e9; width: 35%;" align="center" valign="top">
          <uc5:ServiceListUc ID="ServiceListUc1" runat="server" />
        </td>
      </tr>
    </table>
  </div>
</asp:Content>
