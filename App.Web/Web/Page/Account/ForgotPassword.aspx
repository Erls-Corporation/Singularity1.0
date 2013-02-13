<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="Forgot Password"
  CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <h2>
      Forgot Password?
    </h2>
    <div class="ddl">
    </div>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
      <asp:Label ForeColor="Red" ID="ErrorMessage" runat="server" EnableViewState="False" /></asp:PlaceHolder>
    <asp:PlaceHolder ID="PlaceHolder2" runat="server">
      <asp:Literal EnableViewState="False" ID="FailureText" runat="server" />
      <p>
        Please enter email address to send a password reminder email.</p>
      <table border="0" cellpadding="0">
        <tr>
          <td align="right" valign="middle">
            <p>
              Email Address:</p>
          </td>
          <td valign="middle">
            <asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Email Address is required."
              ID="EmailRequired" runat="server" ToolTip="">Email Address is required.</asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td colspan="2" align="left" valign="middle">
            <asp:Button CommandName="Submit" ID="SubmitButton" runat="server" Text="Send Me Password"
              CssClass="btn" OnClick="SubmitButton_Click" />
          </td>
        </tr>
      </table>
    </asp:PlaceHolder>
  </div>
</asp:Content>
