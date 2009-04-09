<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentListItemLinks.ascx.cs" Inherits="Templates_ars_ContentListItemLinks" %>
<html:HyperLink runat="server" CssClass="FullStoryLink"><html:Image src="<%# FullStoryImage %>" alt="Full Story" runat="server" /></html:HyperLink>
<data:Discussion runat="server">
    <if:Group runat="server">
        <if:Link PathPattern="(/journals/([^/]+))|(/staff/([^/]+))" runat="server">
            <html:HyperLink runat="server" Anchor="Comments"><html:Image src="<%# DiscussionImage %>" alt="Discussion" runat="server" /></html:HyperLink>
        </if:Link>
        <html:HyperLink runat='server' CssClass="DiscussionLink" NavigateUrl="$DiscussionLink"><html:Image src="<%# DiscussionImage %>" alt="Discussion" runat="server" /></html:HyperLink>
    </if:Group>
</data:Discussion>
