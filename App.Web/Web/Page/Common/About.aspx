<%@ Page Language="C#" MasterPageFile="~/Web/Page/Default.master" Title="About" CodeFile="About.aspx.cs"
    Inherits="About" %>

<%@ Register Src="~/Web/Uc/Layout/AboutUc.ascx" TagName="AboutUc" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <div id="body">
        <div class="dfw">
            <uc1:AboutUc ID="AboutUc1" runat="server" />
        </div>
    </div>
</asp:Content>
