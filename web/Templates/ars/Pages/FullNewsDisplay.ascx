<%@ Control Language="VB" ClassName="FullNewsDisplay" Explicit="False" %>
<%@ Register Src="../ShareLink.ascx" TagName="Share" TagPrefix="mbl" %>
<%@ Register Src="../Tags.ascx" TagName="Tags" TagPrefix="ars" %>
<%@ Register Src="../ContentListItem.ascx" TagName="ContentListing" TagPrefix="ars" %>
<%@ Register Src="../PublishStatus.ascx" TagPrefix="ars" TagName="PublishStatus" %>

<html:PageTitle ID="PageTitle1" Value="$Title" runat="server" />
<html:AddClassToParent runat='server' ClassName="NewsItem" />
<h1><ars:PublishStatus runat="server" /><html:HyperLink runat="server" From="Post" /></h1>
<p class="Tag Full">By <data:Authors runat="server"><AuthorTemplate><html:HyperLink From="Author" runat="server" /></AuthorTemplate></data:Authors>
 | Published: <data:Field runat="Server" Name="PublishDate" Format="{0:MMMM dd, yyyy - hh:mmtt}" /> CT
</p>
<% If (String.IsNullOrEmpty(Request.QueryString("comments"))) Then%>
<div class="Body">
    <if:ListHasItems runat='server' ListIs="Child">
	<div class="Inset RelatedStories">
		<h3><span class="Replace">Related Stories</span></h3>
		<ul>
		<data:List runat="server" MaxResults="5">
			<Data>
				<data:Index Asynchronous="true" AggressiveCaching="false">
					<Filters>
						<data:RelatedContent Relevance=".3" />
					</Filters>
				</data:Index>
			</Data>
			<ItemTemplate>
				<li><ars:PublishStatus runat="server" /><mbl:HyperLink runat='server' /></li>
			</ItemTemplate>
		</data:List>
		</ul>
		<h4 class="Footer"></h4>
	</div>
	</if:ListHasItems>
	<data:Field Name="Body" runat="server" />
</div>
<div class="PostOptions flat">
	<mbl:Share runat="server" />
	<html:DiscussionLink CssClass="DiscussionLink RespondLink" runat='server'><html:Image src="images/journal-post-discuss.gif" alt="Discuss" runat="server" /></html:DiscussionLink>
	<a href="#" onclick="window.print();return false"><html:Image runat="server" src="images/print_btn.gif" alt="Print" class="Print" /></a>
</div>
<% Else %>
<div class="Body">
    <p class="Excerpt"><data:Field runat="server" Name="Excerpt" /></p>
    <p><html:HyperLink runat="server">Read Full Story</html:HyperLink></p>
</div>
<div class="PostOptions flat">
	<html:DiscussionLink CssClass="RespondLink" runat='server'><html:Image src="images/post-your-comment.gif" alt="Discuss" runat="server" /></html:DiscussionLink>
</div>
<div class="Comments">
	<div style="height: 35px;" id="topcomments">
		<h3 class="ReaderComments"><html:DiscussionLink runat="server">Reader comments</html:DiscussionLink></h3>
		<div class="Pages">Page: <a class="Inactive">1</a> <a>2</a> <a>3</a></div>
	</div>
	<div class="Pages">Page: <a class="Inactive">1</a> <a>2</a> <a>3</a></div>
</div>
<% End If%>
<ars:Tags runat="server" />