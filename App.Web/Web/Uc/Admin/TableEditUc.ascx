<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TableEditUc.ascx.cs" Inherits="TableEditUc" %>
<%@ Register Src="~/Web/Uc/Core/AttributesUc.ascx" TagName="AttributesUc" TagPrefix="uc1" %>
<%@ Register Src="~/Web/Uc/Admin/ServiceEditUc.ascx" TagName="ServiceEditUc" TagPrefix="uc2" %>
<%@ Register Src="~/Web/Uc/Admin/CategoryEditUc.ascx" TagName="CategoryEditUc" TagPrefix="uc3" %>
<uc1:AttributesUc ID="asuc1" runat="server" />
<uc2:ServiceEditUc ID="seuc1" Visible="false" runat="server" />
<uc3:CategoryEditUc ID="ceuc1" Visible="false" runat="server" />
