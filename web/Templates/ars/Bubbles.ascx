<%@ Control Language="VB" AutoEventWireup="false" %>
<%@ Register TagPrefix="ars" Src="Bubbles/TopStories.ascx" TagName="TopStories" %>
<%@ Register TagPrefix="ars" Src="Bubbles/OpenForum.ascx" TagName="OpenForum" %>
<%@ Register TagPrefix="ars" Src="Bubbles/DontMiss.ascx" TagName="DontMiss" %>
<%@ Register TagPrefix="ars" Src="Bubbles/Poll.ascx" TagName="Poll" %>
<%@ Register TagPrefix="ars" Src="Bubbles/FromTheJournals.ascx" TagName="FromTheJournals" %>
<%@ Register TagPrefix="ars" Src="Bubbles/Jobs.ascx" TagName="Jobs" %>
<%@ Register TagPrefix="ars" Src="Bubbles/Whitepapers.ascx" TagName="Whitepapers" %>
<%@ Register TagPrefix="ars" Src="Bubbles/DirectionsForum.ascx" TagName="DirectionsForum" %>
<%@ Register TagPrefix="ars" Src="Bubbles/SmbResources.ascx" TagName="SmbResources" %>

<div class="Bubbles Wide" runat="server">
    <if:Path StartsWith="!/index" runat='server'>
        <ars:SmbResources runat="server" />
    </if:Path>
	<if:Path StartsWith="!/index" runat='server'>
		<ars:FromTheJournals runat="server" />
	</if:Path>
	<ars:Jobs runat="server" />
	<ars:Whitepapers runat="server" />
	<ars:TopStories runat="server" />
	<ars:OpenForum runat="server" />
</div>