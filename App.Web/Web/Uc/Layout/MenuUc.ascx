<%@ Control Language="C#" ClassName="MenuUc" CodeFile="MenuUc.ascx.cs" Inherits="MenuUc" %>
<asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
<asp:Repeater ID="TopNavRepeat" runat="server" DataSourceID="SiteMapDataSource1">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("Url")%>'
                ToolTip='<%# Eval("Description") %>' />
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>
