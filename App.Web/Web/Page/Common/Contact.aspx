<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="Contact Information"
  CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<%@ Register Src="~/Web/Uc/Layout/ContactUc.ascx" TagName="ContactUc" TagPrefix="uc2" %>
<%@ Register Src="~/Web/Uc/Gadgets/LiveMessengerUc.ascx" TagName="LiveMessengerUc"
  TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <uc2:ContactUc ID="ContactUc1" runat="server" />
  </div>
  <div class="dfw">
    <h1>
      Chat</h1>
    <uc1:LiveMessengerUc ID="LiveMessengerUc1" runat="server" />
  </div>
</asp:Content>
