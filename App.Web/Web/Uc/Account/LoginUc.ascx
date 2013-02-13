<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginUc.ascx.cs" Inherits="LoginUc" %>
<asp:LoginView ID="lv1" runat="server">
  <AnonymousTemplate>
    <h2>
      Welcome to RafeySoft Public Services</h2>
    <div class="ddl">
    </div>
    <table class="tfw">
      <tr>
        <td valign="top">
          <table class="tfw">
            <tr>
              <td valign="top">
                <h2>
                  New User! Register here</h2>
              </td>
            </tr>
            <tr>
              <td valign="top">
                <p>
                </p>
                <asp:Button ID="Register" runat="server" CommandName="Register" Text="Register" CssClass="btn"
                  Width="100px" OnClick="Register_Click" />
              </td>
            </tr>
          </table>
        </td>
        <td style="width: 30%; border-color: #CCCCCC; border-right-style: dotted; border-width: 1px">
          &nbsp;
        </td>
        <td style="width: 5%;">
          &nbsp;
        </td>
        <td align="right">
          <table class="tfw">
            <tr>
              <td td align="left">
                <h2>
                  Sign In</h2>
              </td>
            </tr>
            <tr>
              <td align="left">
                <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" 
                  onloginerror="Login1_LoginError">
                  <LayoutTemplate>
                    <fieldset>
                      <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Email:</asp:Label>
                      <br />
                      <asp:TextBox ID="UserName" runat="server" Columns="25"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                      <p>
                      </p>
                      <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                      <br />
                      <asp:TextBox ID="Password" runat="server" TextMode="Password" Columns="25"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                      <p>
                      </p>
                      <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                      <p>
                      </p>
                      <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Sign In" Width="100px"
                        ValidationGroup="Login1" CssClass="btn" />
                      <p>
                      </p>
                      <asp:LinkButton ID="ForgotPassword" runat="server" CommandName="ForgotPassword" Text="Forgot Password"
                        OnClick="ForgotPassword_Click" /><p />
                      <asp:Label ID="FailureText" runat="server" EnableViewState="False" ForeColor="Red" />
                    </fieldset>
                  </LayoutTemplate>
                </asp:Login>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </AnonymousTemplate>
  <LoggedInTemplate>
    <h2>
      Welcome back!
      <asp:LoginName ID="LoginName1" runat="server" />
    </h2>
    <p />
    <asp:Button ID="logout" CssClass="btn" runat="server" OnClick="Logout_Click" Text="Log off" />
  </LoggedInTemplate>
</asp:LoginView>
