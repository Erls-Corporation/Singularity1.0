<%@ Page Language="c#" MasterPageFile="~/Web/Page/Default.master" CodeFile="ItemSearch.aspx.cs"
  Inherits="ItemSearch" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register src="~/Web/Uc/Search/SearchMainUc.ascx" tagname="SearchMainUc" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
  <uc1:SearchMainUc ID="SearchMainUc1" runat="server" />
</asp:Content>
