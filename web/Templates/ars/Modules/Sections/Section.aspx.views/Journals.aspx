<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false" %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<%@ Import Namespace="Mubble" %>
<%@ Register Src="../../../JournalHeader.ascx" TagName="JournalHeader" TagPrefix="ars" %>
<%@ Register Src="../../../ShareLink.ascx" TagName="Share" TagPrefix="mbl" %>
<%@ Register Src="../../../PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Content Journal" runat="server">
        <html:AddClassToParent runat="server" ClassName="$FileName" />
        <ars:JournalHeader runat="server" />
			<data:List runat="server" MaxResults="20">
				<Data>
					<data:Index>
						<Filters>
							<data:ChildContent />
							<data:IndexField Mode="Require" Name="IsContent" Value="true" />
						</Filters>
					</data:Index>
				</Data>
				<ItemTemplate>
					<h3><ars:PublishStatus runat="server" /><html:HyperLink runat="server" From="Content" /></h3>
					<p class="Tag Full">By <data:Authors runat="server"><AuthorTemplate><html:HyperLink runat="server" /></AuthorTemplate></data:Authors>
					 | Published: <data:Field runat="Server" Name="PublishDate" Format="{0:MMMM dd, yyyy - hh:mmtt}" /> CT
					</p>
					<div class="Body">
					<data:Field Name="Body" runat="server" DisplayMode='Short' />
					<if:Post Is="ExtendedPost" runat="server">
						<html:HyperLink runat="server" From="Post">More...</html:HyperLink>
					</if:Post>
					</div>
					<div class="PostOptions">
						<mbl:Share runat="server" />
						<html:HyperLink From="Content" runat="server" Anchor="Comments" CssClass="DiscussionLink"><html:Image runat="server" src="images/journal-post-discuss.gif" alt="Discuss" /></html:HyperLink>
						<if:Age Days="3" runat="server">
						<a name="RSS--" href="http://blogprint2.smartwebprinting.com/blogPrint/servlet?action=printDialog&pbID=556" onclick="window.open('http://blogprint2.smartwebprinting.com/blogPrint/servlet?action=printDialog&pbID=556', 'melvinPrint', 'width=480,height=300,top=300,left=500,toolbars=0,location=0,menubar=0,resizable=1,scrollbars=1').focus(); return false"><html:Image runat="server" src="images/print_btn.gif" alt="Print" class="Print" /></a>
						</if:Age>
					</div>
				</ItemTemplate>
			</data:List>
		</div><%-- closes the div.ContentBody opened in JournalHeaders.aspx --%>
		<div class="ContentFooter"></div>
    </div>
</asp:Content>