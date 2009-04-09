<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false" %>
<%@ Register Src="../../../ContentListItemLinks.ascx" TagName="ItemLinks" TagPrefix="ars" %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<div class="Featured">
		 <data:List runat='server' MaxResults="3">
			<Data>
				<data:Index>
					<Filters>
						<data:ControllerQuery />
						<data:IndexField Name="IsFeatured" Value="true" Mode="Require" />
					</Filters>
				</data:Index>
			</Data>
			<ItemTemplate>
				<html:HyperLink runat="server"><html:PublishStatus class="PublishStatus" runat="server" DraftIcon="images/pencil.png" ScheduledIcon="images/clock.png" /><html:Image runat="server" FileName="$Metadata[FeaturedIcon]" /></html:HyperLink>
			</ItemTemplate>
		</data:List>
	</div>
	<div class="Content Listing Section <%# Controller.FileName %>">
		<div class="ContentHeader"><span class="Replace"><data:Field runat="server" Name="Title" /></span></div>
		<div class="ContentBody">
			<div class="Inset FeaturedList">
				<h3><span class="Replace">Featured Articles</span></h3>
				<data:List runat='server' Offset="3" MaxResults="10">
					<Data>
						<data:Index>
							<Filters>
								<data:ControllerQuery />
								<data:IndexField Name="IsFeatured" Value="true" Mode="Require" />
							</Filters>
						</data:Index>
					</Data>
					<ItemTemplate>
						<h4><html:HyperLink runat="server" /></h4>
						<p class="Excerpt"><data:Field runat="server" Name="Excerpt" /></p>
						<p class="Options">
							<ars:ItemLinks runat='server' DiscussionImage="images/discussion-dark.gif" FullStoryImage="images/full-story-dark.gif" />
						</p>
					</ItemTemplate>
				</data:List>
				<h4 class="Footer"></h4>
			</div>
			<data:List runat="server" ItemTemplatePath="../../../ContentListItem.ascx">
				<Data><data:Index><Filters><data:ControllerQuery /></Filters></data:Index></Data>
			</data:List>
		</div>
		<div class="ContentFooter"></div>
	</div>
</asp:Content>
