<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false" %>
<%@ Register Src="../../../Bubbles.ascx" TagName="Bubbles" TagPrefix="ars" %>
<%@ OutputCache VaryByParam="none" VaryByCustom="($groups)" Duration="600" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Featured Full">
        <data:List runat="server" MaxResults="39">
            <Data>
                <data:Index>
                    <Filters>
                        <data:IndexField Name="IsFeatured" Value="true" Mode="Require" />
                    </Filters>
                </data:Index>
            </Data>
            <ItemTemplate>
                <html:HyperLink runat="server"><html:PublishStatus class="PublishStatus" runat="server" DraftIcon="images/pencil.png" ScheduledIcon="images/clock.png" /><html:Image runat="server" FileName="$Metadata[FeaturedIcon]" /></html:HyperLink>
            </ItemTemplate>
        </data:List>
    <div style="clear: both;">&nbsp;</div>
    </div>
</asp:Content>