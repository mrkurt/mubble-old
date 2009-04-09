<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false"  %>
<%@ Register Src="../Bubbles.ascx" TagName="Bubbles" TagPrefix="ars" %>
<%@ OutputCache VaryByParam="none" VaryByCustom="($groups)" Duration="600" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Featured">
		<data:List runat="server" MaxResults="3">
			<Data>
				<data:Index>
					<Filters>
						<data:IndexField Name="IsFeatured" Value="true" Mode="Require" />
					</Filters>
				</data:Index>
			</Data>
			<ItemTemplate>
				<html:HyperLink runat="server"><html:PublishStatus class="PublishStatus" runat="server" DraftIcon="images/pencil.png" ScheduledIcon="images/clock.png" /><html:Image FileName="$Metadata[FeaturedIcon]" runat="server" /></html:HyperLink>
			</ItemTemplate>
		</data:List>
    </div>
    <ars:Bubbles runat="server" />
    <div class="Content Listing Home">
		<div class="ContentHeader"><span class="Replace"><data:Field runat="server" Name="Title" /></span></div>
		<div class="ContentBody">
			<a href="/events/CES2009.ars"><img src="http://media.arstechnica.com/ars.static/images/ces_pimage.png" alt="CES Coverage" style="margin-bottom: 15px;" /></a>
			<data:List runat='server' MaxResults="18" ItemTemplatePath="../ContentListItem.ascx">
				<Data>
					<data:Index>
						<Filters><data:ControllerQuery /></Filters>
					</data:Index>
				</Data>
			</data:List>
		</div>
		<div class="ContentFooter"></div>
    </div>
</asp:Content>