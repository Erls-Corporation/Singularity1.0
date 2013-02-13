<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="New Member Registration"
  CodeFile="Register.aspx.cs" Inherits="Register" EnableEventValidation="false" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <h2>
      New Member Registration
    </h2>
    <div class="ddl">
    </div>
    <p style="color: Red;">
      <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal></p>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
      <p>
        Please fill up following information:</p>
      <table class="tfw" cellspacing="10">
        <tr>
          <td colspan="2" style="color: red">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPassword"
              ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."></asp:CompareValidator>
          </td>
        </tr>
        <tr class="ar">
          <td align="right">
            <b>Your Name: </b>
          </td>
          <td>
            <asp:TextBox runat="server" ID="txtName" Width="250px" />
            <asp:RequiredFieldValidator ControlToValidate="txtName" ErrorMessage="Name is required."
              ID="RequiredFieldValidator1" runat="server" ToolTip="Name is required.">*</asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td align="right">
            <b>E-mail: </b>
          </td>
          <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
              ErrorMessage="E-mail is required." ToolTip="E-mail is required.">*</asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
              ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
              ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
          </td>
        </tr>
        <tr class="ar">
          <td align="right">
            <b>Password</b>:
          </td>
          <td>
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="150px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
              ErrorMessage="Password is required." ToolTip="Password is required.">*</asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td style="width: 30%" align="right">
            <b>Confirm Password: </b>
          </td>
          <td>
            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" Width="150px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtConfirmPassword"
              ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required.">*</asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr class="ar">
          <td align="right">
            <b>Service Terms:</b>
          </td>
          <td>
            By clicking on <b>'I accept'</b> below you are agreeing that:<ul>
              <li>You will not post any material on this website which is against generally accepted human morals. Failing to do so will disable your account.
              </li>
              <li>You will help us to remove, if any such material is found on this website.</li>
            </ul>
          </td>
        </tr>
        <tr>
          <td align="right">
            <b>Enter Code: </b>
          </td>
          <td>
            <cc1:CaptchaControl ID="CaptchaControl1" runat="server"></cc1:CaptchaControl>
            &nbsp; &nbsp;
          </td>
        </tr>
        <tr>
          <td align="center" colspan="2" style="color: red">
            <asp:Button ID="btnCreate" runat="server" CssClass="btn" Text="I accept. Create my account"
              OnClick="btnCreate_Click" />
          </td>
        </tr>
      </table>
    </asp:PlaceHolder>
  </div>
</asp:Content>
