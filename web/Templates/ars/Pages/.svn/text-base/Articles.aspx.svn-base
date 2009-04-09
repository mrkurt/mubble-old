<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false"  %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<%@ Import Namespace="Mubble" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Content Listing Section">
		<div class="ContentHeader"><p>Ars File: <strong><data:Field runat='server' Name="Title" /></strong></p></div>
		<div class="ContentBody">
			<data:Field runat="server" Name="Body" />
			<%-- Begin Sections List --%>
			<if:ListHasItems runat="server" ListIs="Child">
			<div class="Inset Sections">
				<h3><span class="Replace">Related Stories</span></h3>
				<ul>
					<data:List runat="server">
						<Data>
							<data:Index>
								<Filters>
									<data:XmlQuery>
									    <query required="true" orderBy="SortableTitle">
									        <term field="Path" value="/articles/*" />
									        <term field="Path" value="/reviews/*" />
									    </query>
									</data:XmlQuery>
									<data:IndexField Name="IsContentContainer" Value="true" Mode="Require" />
								</Filters>
							</data:Index>
						</Data>
						<ItemTemplate>
							<li><html:HyperLink runat="server" /></li>
						</ItemTemplate>
					</data:List>
				</ul>
		        <h4 class="Footer"></h4>
			</div>
			</if:ListHasItems>
			<%-- End Sections List --%>

			<%-- Show Articles --%>
			<data:List runat="server" id="SearchResults" ItemTemplatePath="../ContentListitem.ascx">
				<Pager PageSize="20" />
				<Data>
					<data:Index>
						<Filters>
							<data:XmlQuery>
							    <query required="true">
							        <term field="Path" value="/articles/*" />
							        <term field="Path" value="/reviews/*" />
							    </query>
							</data:XmlQuery>
							<data:IndexField Name="IsContent" Value="true" Mode="Require" />
						</Filters>
					</data:Index>
				</Data>
			</data:List>
		</div>
		<div class="ContentFooter">
			<p class="Paging"><html:StupidPageLink runat="server" NoPageClass="Inactive" HideWhenNoPage="false" Mode="PreviousPage" PageLinkFor="SearchResults">&lt; Previous Page</html:StupidPageLink> | <html:StupidPageLink runat="server" NoPageClass="Inactive" HideWhenNoPage="false" Mode="NextPage" PageLinkFor="SearchResults">Next Page &gt;</html:StupidPageLink></p>
		</div>
	</div>
</asp:Content>
