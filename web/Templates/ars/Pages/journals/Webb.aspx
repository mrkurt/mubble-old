<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False"  PostAllowed="false" %>
<%@ OutputCache VaryByParam="none" VaryByCustom="($groups)" Duration="600" %>
<%@ Register Src="../../ShareLink.ascx" TagName="Share" TagPrefix="mbl" %>
<%@ Register Src="../../Tags.ascx" TagName="Tags" TagPrefix="ars" %>
<%@ Register Src="../../JournalHeader.ascx" TagName="JournalHeader" TagPrefix="ars" %>
<%@ Register Src="../../PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
	<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Content Journal" runat="server">
        <html:AddClassToParent runat="server" ClassName="$FileName" />
        <ars:JournalHeader runat="server" />
			<data:Posts runat="server" ID="Posts">
				<Filters>
					<data:UrlParameters />
					<data:PageNumber ParameterName="Page" PageSize="20" />
				</Filters>
				<PostTemplate>
				    <if:FirstPost runat='server'>
					    <if:SinglePost runat="server">
						    <html:AddClassToParent runat="server" ClassName="Entry" />
					    </if:SinglePost>
					    <if:Post Is="!SinglePost,NewDate" runat="server">
						    <h3 class="Date"><data:Field runat='server' Name="PublishDate" Format="{0:MMMM dd, yyyy}" /> CT</h3>
					    </if:Post>
					    <h3><ars:PublishStatus runat="server" /><html:HyperLink runat="server" From="Post" /></h3>
					    <p class="Tag Full">By <data:Authors runat="server"><AuthorTemplate><html:HyperLink From="Author" runat="server" /></AuthorTemplate></data:Authors>
					     | Published: <data:Field runat="Server" Name="PublishDate" Format="{0:MMMM dd, yyyy - hh:mmtt}" /> CT
					    </p>
					    <div class="Body">
						    <if:SinglePost runat="server">
						        <html:PageTitle Value="$Title" runat="server" />
							    <data:Field Name="Body" runat="server" DisplayMode="Full" />
							    <ars:Tags runat="server" />
						    </if:SinglePost>
						    <if:NotSinglePost runat="server">
							    <data:Field Name="Body" runat="server" DisplayMode='Short' />
							    <if:Post Is="ExtendedPost" runat="server">
							    <html:HyperLink runat="server" From="Post">Read story links...</html:HyperLink>
							    </if:Post>
						    </if:NotSinglePost>
					    </div>
					    <div class="PostOptions">
						    <mbl:Share runat="server" />
						    <data:Discussion runat="server">
							    <if:SinglePost runat="server">
							    <html:DiscussionLink runat="server" CssClass="RespondLink"><html:Image src="images/post-your-comment.gif" alt="Post your comment" runat='server' /></html:DiscussionLink>
							    </if:SinglePost>
							    <if:NotSinglePost runat="server">
							    <html:HyperLink From="Post" runat="server" Anchor="Comments" CssClass="DiscussionLink"><html:Image runat="server" src="images/journal-post-discuss.gif" alt="Discuss" /></html:HyperLink>
							    </if:NotSinglePost>
						    </data:Discussion>
						    <if:Age Days="3" runat="server" Visible="false">
						    <a name="RSS--" href="http://blogprint2.smartwebprinting.com/blogPrint/servlet?action=printDialog&pbID=556" onclick="window.open('http://blogprint2.smartwebprinting.com/blogPrint/servlet?action=printDialog&pbID=556', 'melvinPrint', 'width=480,height=300,top=300,left=500,toolbars=0,location=0,menubar=0,resizable=1,scrollbars=1').focus(); return false"><html:Image runat="server" src="images/print_btn.gif" alt="Print" class="Print" /></a>
						    </if:Age>
					    </div>
					    <if:Post Is="SinglePost,DiscussionExists" runat="server">
						    <div class="Comments">
							    <h3 class="ReaderComments">Reader comments</h3>
							    <div class="Pages">Page: <a class="Inactive">1</a> <a>2</a> <a>3</a></div>
							    <div class="Pages">Page: <a class="Inactive">1</a> <a>2</a> <a>3</a></div>
						    </div>
					    </if:Post>
					    <if:Post Is="!SinglePost" runat="server">
					        <h4>Previous Alerts</h4>
					        <ul>
					    </if:Post>
					</if:FirstPost>
					<if:Post Is="!First" runat="server">
					    <li><html:HyperLink runat="server" /></li>
					    <if:Post Is="Last" runat="server">
					    	</ul>
					    </if:Post>
					</if:Post>
				</PostTemplate>
			</data:Posts>
		</div><%-- closes the div.ContentBody from JournalHeader.aspx --%>
		<div class="ContentFooter">
			<if:NotSinglePost runat="server" UseControl="Posts">
			<p class="Paging">
				<html:PagingLink runat="server" NoPageClass="Inactive" Mode="PreviousPage" PageLinkFor="Posts">&lt; Previous Page</html:PagingLink>
				| <html:PagingLink runat="server" NoPageClass="Inactive" Mode="NextPage" PageLinkFor="Posts">Next Page &gt;</span></html:PagingLink>
			</p>
			</if:NotSinglePost>
		</div>
	</div>
</asp:Content>