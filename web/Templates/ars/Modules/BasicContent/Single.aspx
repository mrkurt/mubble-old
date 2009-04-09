<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false" %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Content Page Site <%# Controller.FileName %>">
        <div class="ContentHeader"><span class="Replace"><data:Field runat="server" Name="Title" /></span></div>
        <div class="ContentBody">
            <data:Field runat="server" Name="Body" />
        </div>
        <div class="ContentFooter"></div>
    </div>
</asp:Content>