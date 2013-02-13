<%@ Control Language="C#" ClassName="SearchMainUc" CodeFile="SearchMainUc.ascx.cs"
  Inherits="SearchMainUc" %>
<%@ Register Src="~/Web/Uc/Core/CategoryListUc.ascx" TagName="CategoryListUc" TagPrefix="uc2" %>
<%@ Register Src="~/Web/Uc/Core/ItemsUc.ascx" TagName="ItemsUc" TagPrefix="uc1" %>
<%@ Register Src="~/Web/Uc/Search/SearchUc.ascx" TagName="SearchUc" TagPrefix="uc1" %>
<%@ Register Src="~/Web/Uc/Gadgets/GoogleAdUc.ascx" TagName="GoogleAdUc" TagPrefix="uc4" %>
<asp:PlaceHolder ID="ph2" runat="server">
  <div class="dfw">
    <table class="tfw">
      <tr>
        <td align="left" valign="top">
          <table>
            <tr>
              <td align="left" valign="top">
                <h2>
                  <%=Cxt.Service.Get(App.Model.AttributeE.ServiceName)%></h2>
              </td>
            </tr>
            <tr>
              <td align="left" valign="top">
                <%= Cxt.Service.Get(App.Model.AttributeE.ServiceAbout) %>
              </td>
            </tr>
          </table>
        </td>
        <td style="padding-left: 20px; width: 35%" valign="top" align="right">
          <uc2:CategoryListUc ID="cluc1" runat="server" />
        </td>
      </tr>
    </table>
  </div>
</asp:PlaceHolder>
<uc4:GoogleAdUc ID="guc11" runat="server" AdType="One728x15" />
<asp:PlaceHolder ID="ph1" runat="server">
  <div class="dfw">
    <uc1:SearchUc ID="suc1" runat="server" />
    <uc1:ItemsUc ID="isuc1" runat="server" />
  </div>
</asp:PlaceHolder>
