<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false" %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<div class="Content Listing SearchResults">
		<div class="ContentHeader"><p>Ars File: <strong>Site Search</strong></p></div>
		<div class="ContentBody">
			<div id="AdvancedSearch">
				<html:Form runat="server" Path="/search">
					<div id="searchmain">
					<table border="0" cellspacing="0" cellpadding="0">
					<tr><th>Keywords</th><td><mbl:TextBox runat="server" FieldName="search" /></td></tr>
					<tr><th>Featured only</th><td><mbl:Checkbox runat="server" FieldName="featured" value="true" /></td></tr>
					<tr><th>Section</th><td> <mbl:Select runat="server" FieldName="path">
						<option value="/" selected="selected">Everything</option>
						<option value="/news">News</option>
						<option value="/articles">Articles</option>
						<option value="/reviews">Reviews</option>
						<option value="/journals">Journals.Ars</option>
						<option value="/journals/apple">Infinite Loop</option>
						<option value="/journals/microsoft">One Microsoft Way</option>
						<option value="/journals/thumbs">Opposable Thumbs</option>
						<option value="/journals/science">Nobel Intent</option>
						<option value="/journals/linux">Open Ended</option>
						<option value="/guides">Guides</option>
					</mbl:Select></td></tr>
					<tr><th>Author</th><td> <mbl:AuthorSelector runat="server" FieldName="author" /></td></tr>
					<tr><th>Sort By</th><td><mbl:Select runat="server" FieldName="sort"><option value="">Date</option><option value="score">Score</option></mbl:Select></td></tr>
					<tr>
						<td colspan="2" style="text-align: right;" id="advSearchCell">
							<input type="submit" value="Search" id="advSearch" />
						</td>
					</tr>
					</table>
					</div>
				</html:Form>
			</div>
			<data:List ID="SearchResults" runat="server" ItemTemplatePath="../ContentListitem.ascx">
				<Pager PageSize="20" />
				<Data>
					<data:Index AggressiveCaching="false" Asynchronous="false">
						<Filters>
							<data:UserSearch />
						</Filters>
					</data:Index>
				</Data>
				<HeaderTemplate>
					<data:Field Name="Body" runat='server' />
				</HeaderTemplate>
			</data:List>
		</div>
		<div class="ContentFooter">
			<p class="Paging"><html:StupidPageLink runat="server" NoPageClass="Inactive" HideWhenNoPage="false" Mode="PreviousPage" PageLinkFor="SearchResults">&lt; Previous Page</html:StupidPageLink> | <html:StupidPageLink runat="server" NoPageClass="Inactive" HideWhenNoPage="false" Mode="NextPage" PageLinkFor="SearchResults">Next Page &gt;</html:StupidPageLink></p>
		</div>
	</div>
</asp:Content>