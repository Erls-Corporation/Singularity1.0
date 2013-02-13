<%@ Control Language="C#" ClassName="SearchLatestUc" CodeFile="SearchLatestUc.ascx.cs"
  Inherits="SearchLatestUc" %>
<%@ Register Src="~/Web/Uc/Core/ItemsUc.ascx" TagName="ItemsUc" TagPrefix="uc1" %>
<table class="tfw">
  <tr>
    <td valign="top" align="left" valign="top">
      <uc1:ItemsUc ID="isuc1" runat="server" />
    </td>
  </tr>
</table>
