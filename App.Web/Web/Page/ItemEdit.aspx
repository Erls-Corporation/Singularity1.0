<%@ Page Language="c#" MasterPageFile="~/Web/Page/Default.master" CodeFile="ItemEdit.aspx.cs"
  Inherits="ItemEdit" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Src="~/Web/Uc/Core/AttributesUc.ascx" TagName="AttributesUc" TagPrefix="uc1" %>
<%@ Register Src="~/Web/Uc/Gadgets/GoogleAdUc.ascx" TagName="GoogleAdUc" TagPrefix="uc4" %>
<%@ Register Src="~/Web/Uc/Admin/TableEditUc.ascx" TagName="TableEditUc" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <div class="dfw">
    <asp:PlaceHolder ID="ph4" Visible="false" runat="server">
      <asp:Label ID="lcd" ForeColor="Green" runat="server"></asp:Label><p />
      <asp:Button ID="btnOk" runat="server" CssClass="btn" Text="OK" OnClick="btnOK_Click"
        Width="75px" /></asp:PlaceHolder>
    <asp:PlaceHolder ID="ph5" runat="server">
      <h2>
        <asp:Label ID="ln" runat="server"></asp:Label>
      </h2>
      <br />
      <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
      <table class="tfw">
        <tr>
          <td>
            <asp:PlaceHolder ID="ph1" runat="server">
              <p>
                <asp:Label ID="lblInfo" runat="server" Text="Please fill in following information:"></asp:Label>
              </p>
              <asp:Button ID="btnSave1" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click"
                Width="75px" />&nbsp; </asp:PlaceHolder>
            <asp:Button ID="btnCancel1" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click"
              Width="75px" />
            <p />
            <p />
          </td>
        </tr>
        <tr>
          <td align="center">
            <uc4:GoogleAdUc ID="guc11" runat="server" AdType="One468x15" />
          </td>
        </tr>
        <tr>
          <td>
            <table class="tfw">
              <tr>
                <td>
                  <table class="tfw">
                    <tr align="left">
                      <td>
                        <uc1:AttributesUc ID="asuc1" runat="server" />
                        <uc2:TableEditUc ID="teuc1" runat="server" />
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <asp:PlaceHolder ID="ph3" runat="server">
                          <table>
                            <tr>
                              <td>
                                <b>Enter Code:</b>
                              </td>
                              <td>
                                <cc1:CaptchaControl ID="CaptchaControl1" runat="server" />
                              </td>
                            </tr>
                          </table>
                        </asp:PlaceHolder>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <uc4:GoogleAdUc ID="guc12" runat="server" />
          </td>
        </tr>
        <tr>
          <td>
            <p />
            <p />
            <asp:PlaceHolder ID="ph2" runat="server">
              <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click"
                Width="75px" />&nbsp; </asp:PlaceHolder>
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click"
              Width="75px" />
            <p />
          </td>
        </tr>
      </table>
    </asp:PlaceHolder>
  </div>
</asp:Content>
