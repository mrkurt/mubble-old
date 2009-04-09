<%@ Page ContentType="text/html" Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>" PostAllowed="false" %>
<%@ OutputCache VaryByParam="*" VaryByCustom="($groups)" Duration="600" %>
<%@ Import Namespace="Mubble.Tools.Http" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="Content Listing SearchResults">
		<div class="ContentHeader" runat="server">
		    <p>Ars File: <strong><data:Field runat='server' Name="Title" /></p></strong>
		    <if:Path StartsWith="/apple/iphone/apps" runat="server">
		        <html:AddClassToParent runat="server" ClassName="iphone-apps" />
		    </if:Path>
		</div>
		<div class="ContentBody">
			<data:List runat="server" ItemTemplatePath="../../ContentListitem.ascx" ID="MainContentList">
				<Data>
					<data:Index>
						<Filters>
							<data:ControllerQuery />
						</Filters>
					</data:Index>
				</Data>
				<HeaderTemplate>
					<data:Field runat="server" Name="Body" />
				</HeaderTemplate>
			</data:List>
		</div>
		<div class="ContentFooter"></div>
    </div>
</asp:Content>