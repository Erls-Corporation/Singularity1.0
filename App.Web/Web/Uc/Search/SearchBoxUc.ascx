<%@ Control Language="C#" ClassName="SearchBoxUc" CodeFile="SearchBoxUc.ascx.cs"
  Inherits="SearchBoxUc" %>
<%@ Register Assembly="App.Model" Namespace="App.Model" TagPrefix="cc1" %>
<%@ Register Src="~/Web/Uc/Core/AttributesUc.ascx" TagName="AttributesUc" TagPrefix="uc1" %>
<table cellpadding="0" cellspacing="0">
  <tr>
    <td valign="top" align="right">
      <cc1:RsTextBox ID="SearchTextBox" CssClassWaterMarkTextFocus="stbh" CssClassWaterMarkTextBlur="stbhb"
        WaterMarkText="Search" ShowWaterMarkText="true" runat="server"/>
    </td>
    <td valign="top" align="right">
      <asp:ImageButton ID="SearchImageButton" AlternateText="Search" ToolTip="Search Website" ImageUrl="~/Web/Img/tb/find.jpg"
        Height="19" Width="20" runat="server" OnClick="SearchImageButton_Click"></asp:ImageButton>
    </td>
  </tr>
</table>
