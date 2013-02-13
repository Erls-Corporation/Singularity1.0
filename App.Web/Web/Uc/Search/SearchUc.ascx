<%@ Control Language="C#" ClassName="SearchUc" CodeFile="SearchUc.ascx.cs" Inherits="SearchUc" %>
<%@ Register Src="SearchDbUc.ascx" TagName="SearchDbUc" TagPrefix="uc2" %>
<%@ Register Src="SearchGoogleUc.ascx" TagName="SearchGoogleUc" TagPrefix="uc5" %>
<uc5:SearchGoogleUc ID="SearchGoogleUc1" Visible="false" runat="server" />
<uc2:SearchDbUc ID="SearchDbUc1" runat="server" />
