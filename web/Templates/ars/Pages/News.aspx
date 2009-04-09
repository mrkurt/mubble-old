<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" PostAllowed="false" %>
<%@ OutputCache VaryByParam="comments" VaryByCustom="($groups)" Duration="600" %>
<%@ Register Src="../ShareLink.ascx" TagName="Share" TagPrefix="mbl" %>
<%@ Register Src="../Tags.ascx" TagName="Tags" TagPrefix="ars" %>
<%@ Register Src="../ContentListItem.ascx" TagName="ContentListing" TagPrefix="ars" %>
<%@ Register Src="../PublishStatus.ascx" TagPrefix="ars" TagName="PublishStatus" %>

<%@ Register Src="FullNewsDisplay.ascx" TagPrefix="ars" TagName="FullNews" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <%-- Container div, runat="server" so the AddClass controls will work --%>
    <div class="Content" runat="server">
        <div class="ContentHeader"><span class="Replace">From the Newsdesk</span></div>
        <div class="ContentBody">
	        <data:Posts runat="server" ID="Posts">
		        <Filters>
			        <data:UrlParameters />
			        <data:PageNumber ParameterName="Page" PageSize="30" />
		        </Filters>
		        <PostTemplate>
			        <if:SinglePost runat="server">
			            <ars:FullNews runat="server" />
			        </if:SinglePost>
			        <if:NotSinglePost runat="server">
			            <if:FirstPost runat="server">
			            	<html:AddClassToParent runat='server' ClassName="Listing News" />
			            </if:FirstPost>
				        <ars:ContentListing runat="server" />
			        </if:NotSinglePost>
		        </PostTemplate>
	        </data:Posts>
	    </div>


		<%-- div.ContentBody --%>
		<div class="ContentFooter">
			<p class="Paging">
			<if:SinglePost UseControl="Posts" runat="server">
				<html:PagingLink MaxTitleLength="65" runat="server" HideWhenNoPage="true" Mode="NextPage" PageLinkFor="Posts">{0} <span class="Hint">:Next Post</span></html:PagingLink>
				<html:PagingLink MaxTitleLength="65" runat="server" HideWhenNoPage="true" Mode="PreviousPage" PageLinkFor="Posts">{0} <span class="Hint">:Prev Post</span></html:PagingLink>
			</if:SinglePost>
			<if:NotSinglePost runat="server" UseControl="Posts">
				<html:PagingLink runat="server" HideWhenNoPage="true" Mode="NextPage" PageLinkFor="Posts">{0} <span class="Hint">:Next</span></html:PagingLink>
				<html:PagingLink runat="server" HideWhenNoPage="true" Mode="PreviousPage" PageLinkFor="Posts">{0} <span class="Hint">:Prev</span></html:PagingLink>
			</if:NotSinglePost>
			</p>
		</div>
	</div>

	<if:SinglePost UseControl="Posts" runat="server">
	<table id="LatestPosts">
		<thead>
			<tr>
				<th class="LatestNewsPosts"><span class="Replace">Latest News Posts</span></td>
				<th class="LatestJournalPosts"><span class="Replace">Latest Journal Posts</span></td>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td class="LatestNewsPosts">
					<ul class="Headlines">
					<data:List runat="server" MaxResults="8">
						<Data>
							<data:Index>
								<Filters>
									<data:IndexField Mode="Require" Name="Path" Value="/news" />
									<data:IndexField Mode="Require" Name="ActiveObjectsType" Value="Mubble.Models.Post" />
								</Filters>
							</data:Index>
						</Data>
						<ItemTemplate>
							<li><ars:PublishStatus runat="server" /><html:HyperLink runat="server" /></li>
						</ItemTemplate>
					</data:List>
					</ul>
				</td>
				<td class="LatestJournalPosts">
					<ul class="Headlines">
					<data:List runat="server" MaxResults="8">
						<Data>
							<data:Index>
								<Filters>
									<data:IndexField Mode="Require" Name="ActiveObjectsType" Value="Mubble.Models.Post" />
									<data:IndexField Mode="Exclude" Name="Path" Value="/news" />
								</Filters>
							</data:Index>
						</Data>
						<ItemTemplate>
							<li runat="server"><ars:PublishStatus runat="server" /><data:Container runat="server"><html:AddClassToParent runat="server" ClassName="$FileName" /></data:Container><html:HyperLink runat="server" /></li>
						</ItemTemplate>
					</data:List>
					</ul>
				</td>
			</tr>
		</tbody>
		<tfoot>
			<tr>
				<td class="LatestNewsPosts"><html:HyperLink runat="server" Path="/news"><html:Image runat="server" src="images/view-more.gif" alt="View More" /></html:HyperLink></td>
				<td class="LatestJournalPosts"><html:HyperLink runat="server" Path="/journals"><html:Image runat="server" src="images/view-more.gif" alt="View More" /></html:HyperLink></td>
			</tr>
		</tfoot>
	</table>
	</if:SinglePost>

</asp:Content>