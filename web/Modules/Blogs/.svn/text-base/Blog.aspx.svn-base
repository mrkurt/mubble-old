<%@ Page Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>"  %>
<%@ OutputCache VaryByParam="none" VaryByCustom="($groups)" Duration="600" %>
<%@ Import Namespace="Mubble" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
   <blog:Posts runat="server" ID="Posts" PageSize="20">
        <HeaderTemplate><h1>Posts</h1></HeaderTemplate>
        <ItemTemplate>
            <h2><mbl:PublishStatus runat="server" PublishedIcon="" Status='<%# Container.DataItem.Status %>' PublishDate='<%# Container.DataItem.PublishDate %>' />
            <mbl:HyperLink runat="server" UrlPair='<%# Container.DataItem.Url %>'><%# Container.DataItem.Title %></mbl:HyperLink></h2>
            <%#Container.DataItem.Body%>
        </ItemTemplate>
        <SeparatorTemplate><hr /></SeparatorTemplate>
   </blog:Posts>
   <p><mbl:PagingLink runat="server" PageLinkFor="Posts" Mode="PreviousPage">&lt; Prev Page</mbl:PagingLink></p>
   <p><mbl:PagingLink runat="server" PageLinkFor="Posts" Mode="NextPage">Next Page &gt;</mbl:PagingLink></p>
   <h2><%# DateTime.Now %></h2>
</asp:Content>