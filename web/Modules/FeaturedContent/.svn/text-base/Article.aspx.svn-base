<%@ Page Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <h3><%#Controller.Title%></h3>
    <p><asp:Repeater runat="Server">
        <ItemTemplate><%# Container.DataItem.FullName %></ItemTemplate>
        <SeparatorTemplate>, </SeparatorTemplate>
    </asp:Repeater></p>
    <mbl:ContentPage runat="Server" PageNumber='<%#Url.GetPathItem(0,1)%>' ID="ContentDisplay">
        <h4><%#Container.SelectedPage.Name%></h4>
        <%#Container.SelectedPage.Body%>
    </mbl:ContentPage>
    <p><mbl:PagingLink Mode="PreviousPage" runat="Server" PageLinkFor="ContentDisplay">&lt; Previous</mbl:PagingLink></p>
    <p><mbl:PagingDropDownList runat="server" PageLinkFor="ContentDisplay"></mbl:PagingDropDownList></p>
    <p><mbl:PagingLink Mode="NextPage" runat="Server" PageLinkFor="ContentDisplay">Next &gt;</mbl:PagingLink></p>
</asp:Content>    