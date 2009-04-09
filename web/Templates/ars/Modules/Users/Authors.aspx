<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false" %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<%@ Import Namespace="Mubble" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<div class="Content Listing Authors SearchResults">
	<data:Authors runat="server">
		<Filters>
			<data:UrlParameters />
		</Filters>
		<AuthorTemplate>
		<div class="ContentHeader"><p>Content by: <strong><html:HyperLink From="Author" runat="server" /></strong></p></div>
		<div class="ContentBody">
			<data:Field 
			    runat="server" 
			    name="Bio" 
			    Format="<div class=&quot;AuthorInformation&quot;><div class=&quot;AuthorBio&quot;>{0}</div></div>" 
			    />
			<data:List ID="SearchResults" runat="server" ItemTemplatePath="../../ContentListitem.ascx">
				<Pager PageSize="20" />
				<Data>
					<data:Index AggressiveCaching="false" Asynchronous="false" CacheAgainstQuery="true">
						<Filters>
							<data:AuthorFilter />
						</Filters>
					</data:Index>
				</Data>
			</data:List>
	    </div>
		<div class="ContentFooter">
			<p class="Paging"><html:StupidPageLink runat="server" NoPageClass="Inactive" HideWhenNoPage="false" Mode="PreviousPage" PageLinkFor="SearchResults">&lt; Previous Page</html:StupidPageLink> | <html:StupidPageLink runat="server" NoPageClass="Inactive" HideWhenNoPage="false" Mode="NextPage" PageLinkFor="SearchResults">Next Page &gt;</html:StupidPageLink></p>
		</div>
		</AuthorTemplate>
	</data:Authors>
	</div>
</asp:Content>