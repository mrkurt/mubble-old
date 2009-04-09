<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" PostAllowed="false" %>
<%@ Register Src="../../ShareLink.ascx" TagName="Share" TagPrefix="mbl" %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<%@ Register Src="../../Tags.ascx" TagName="Tags" TagPrefix="ars" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Content Article">
        <div class="ContentHeader"><p>Ars File: <strong><%#Controller.Parent.Title%></strong></p></div>
        <div class="ContentBody">
			<div class="Options">
				<html:PagingDropDownList runat="server" PageLinkFor="Pages"></html:PagingDropDownList>
				<html:LinkFromMetadata runat="server" Field="PdfLink"><html:Image src="images/download-pdf.gif" alt="Download the PDF" runat="server" /></html:LinkFromMetadata>
			</div>
			<h1><data:Field runat="server" Name="Title" /></h1>
			<p class="Tag Full">
			<data:Authors runat="server">
			    <HeaderTemplate>By </HeaderTemplate>
			    <AuthorTemplate><html:HyperLink runat="server" /></AuthorTemplate>
			    <SeparatorTemplate>, </SeparatorTemplate>
			    <FooterTemplate> | </FooterTemplate>
			</data:Authors>
			Published: <data:Field runat="Server" Name="PublishDate" Format="{0:MMMM dd, yyyy - hh:mmtt}" /> CT
			</p>
			<data:Pages runat="server" ID="Pages">
				<Filters>
					<data:PageNumber ParameterName="PageNumber" />
				</Filters>
				<PageTemplate>
					<h2><data:Field Name="Name" runat="server" /></h2>
					<div class="Body">
					<data:Field Name="Body" runat="server" />
					</div>
				</PageTemplate>
			</data:Pages>
        </div>
		<div class="ContentFooter">
			<div class="PostOptions">
				<p class="Paging">
					<html:PagingLink runat="server" Mode="PreviousPage" NoPageClass="Inactive" PageLinkFor="Pages">&lt; Previous Page</html:PagingLink> |
					<html:PagingLink runat="server" Mode="NextPage" NoPageClass="Inactive" PageLinkFor="Pages">Next Page &gt;</html:PagingLink>
				</p>
				<mbl:Share runat="server" />
				<html:DiscussionLink runat="server"><html:Image src="images/journal-post-discuss.gif" alt="Discuss" runat="server" /></html:DiscussionLink>
			</div>
			<ars:Tags runat="server" />
		</div>
    </div>
</asp:Content>