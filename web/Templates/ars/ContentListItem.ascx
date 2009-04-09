<%@ Register Src="ContentListItemLinks.ascx" TagName="ItemLinks" TagPrefix="ars" %>
<%@ Register Src="PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
<h3 class="Headline" runat="server">
    <ars:PublishStatus runat="server" />
    <if:Group runat="server">
        <if:Path StartsWith="/news" runat="server"><html:AddClassToParent runat="server" ClassName="News" /></if:Path>
        <if:Path StartsWith="/articles" runat="server"><html:AddClassToParent runat="server" ClassName="Article" /></if:Path>
        <if:Path StartsWith="/guides" runat="server"><html:AddClassToParent runat="server" ClassName="Guide" /></if:Path>
        <if:Path StartsWith="/reviews" runat="server"><html:AddClassToParent runat="server" ClassName="Article" /></if:Path>
        <if:Post runat="server"><html:AddClassToParent runat="server" ClassName="Journal" /></if:Post>
        <if:Path StartsWith="/journals" runat="server"><html:AddClassToParent runat="server" ClassName="Journal" /></if:Path>
        <if:Path StartsWith="/staff" runat="server"><html:AddClassToParent runat="server" ClassName="Journal" /></if:Path>
    </if:Group>
    <html:HyperLink runat='server' />
</h3>
<p class="Excerpt"><data:Field runat="server" Name="Excerpt" /></p>
<p class="Tag"><data:Field runat="server" Name="PublishDate" Format="{0:MMMM dd, yyyy - hh:mmtt}" /> CT
<data:Authors runat="server">
    <HeaderTemplate> - by </HeaderTemplate>
    <AuthorTemplate><mbl:HyperLink runat="server" /></AuthorTemplate>
    <SeparatorTemplate>, </SeparatorTemplate>
</data:Authors>
</p>
<p class="Options">
<ars:ItemLinks runat="server" />
</p>