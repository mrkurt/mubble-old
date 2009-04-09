<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tags.ascx.cs" Inherits="Templates_ars_Tags" %>
	<div class="Tags">
        <p>Filed under: <data:Tags runat="Server" Field="Tag" ID="Tags">
            <TagTemplate><html:HyperLink From="Tag" rel="tag" runat="server" /></TagTemplate>
            <SeparatorTemplate><span>, </span></SeparatorTemplate>
        </data:Tags></p>
	</div>