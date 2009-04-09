<%@ Page Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>"  %>
<%@ Import Namespace="Mubble" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
<h3><%#Controller.Title%></h3>
<%#Controller.Body%>
<div>
    <mbl:Controllers
        runat="Server"
        ExcludeDrafts="true"
        OrderBy="Title"
        ID="Sections"
        >
        <ItemTemplate>
            <h4><mbl:HyperLink runat="server" Content="<%# Container.DataItem %>"><%# Container.DataItem.Title %></mbl:HyperLink>
            </h4>
            <ul>
                <mbl:ContentList
                    runat="Server"
                    ExcludeDrafts="true"
                    OrderBy="Title"
                    ID="SectionArticles"
                    RootContent="<%# Container.DataItem %>"
                    ModuleId='<%# Controller.Settings("ModuleId") %>'
                    ModuleControl='<%# Controller.Settings("Control") %>'
                    >
                    <ItemTemplate>
                        <li><%#Page.ConditionalWrite(Mubble.Models.PublishStatus.Draft, Container.DataItem.Status, "(Draft)")%>
                        <mbl:HyperLink Content='<%# Container.DataItem %>' runat="server"><%# Container.DataItem.Title %></mbl:HyperLink>
                        <br />
                        <%#Container.DataItem.PublishDate.ToLongDateString()%>
                        </li>
                    </ItemTemplate>                        
                    </mbl:ContentList>
            </ul>
        </ItemTemplate>
        </mbl:Controllers>
</div>
</asp:Content>