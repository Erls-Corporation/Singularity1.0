<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="RafeySoft.com SignIn"
    CodeFile="SignIn.aspx.cs" Inherits="SignIn" %>

<%@ Register Src="~/Web/Uc/Account/LoginUc.ascx" TagName="LoginUc" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <div id="body">
        <div class="dfw">
            <uc1:LoginUc ID="LoginUc1" runat="server" />
        </div>
    </div>
</asp:Content>
