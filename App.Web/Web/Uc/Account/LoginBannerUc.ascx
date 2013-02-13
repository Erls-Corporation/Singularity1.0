<%@ Control Language="C#" ClassName="LoginBannerUc" CodeFile="LoginBannerUc.ascx.cs"
  Inherits="LoginBannerUc" %>
<asp:LoginView ID="LoginView1" runat="server">
  <LoggedInTemplate>
    Welcome&nbsp;<b><asp:LoginName ID="LoginName1" runat="server" />
    </b>&nbsp;|&nbsp;
<%--    <asp:HyperLink ID="Account" NavigateUrl="~/Web/Page/Account/Account.aspx" runat="server"
      Text="My Account" />
    &nbsp;|&nbsp;--%>
    <asp:HyperLink ID="Logout" NavigateUrl="~/Web/Page/Account/SignOut.aspx" runat="server"
      Text="Sign Out" />
  </LoggedInTemplate>
  <AnonymousTemplate>
    <asp:HyperLink ID="Login" NavigateUrl="~/Web/Page/Account/SignIn.aspx" runat="server"
      Text="Sign In" />&nbsp;|&nbsp;
    <asp:HyperLink ID="Register" NavigateUrl="~/Web/Page/Account/Register.aspx" runat="server"
      Text="Register" />&nbsp;
  </AnonymousTemplate>
</asp:LoginView>
<asp:PlaceHolder ID="PlaceHolder1" runat="server">&nbsp;|&nbsp;<asp:HyperLink ID="lnkAdmin"
  NavigateUrl="~/Web/Page/Admin/Admin.aspx" runat="server" Text="Admin" /></asp:PlaceHolder>
