<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="Sorry, but the page you are looking for is not found"
  CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
      <asp:Label ID="lcd" ForeColor="Red" runat="server" Text="An error occured while full filling your request. Please retry after some time. We apologize for any inconvienece this may has caused."></asp:Label>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="false">

      <script type="text/javascript">
        var GOOG_FIXURL_LANG = 'en';
        var GOOG_FIXURL_SITE = 'http://rafeysoft.com/';
      </script>

      <script type="text/javascript" src="http://linkhelp.clients.google.com/tbproxy/lh/wm/fixurl.js"></script>

    </asp:PlaceHolder>
  </div>
</asp:Content>
