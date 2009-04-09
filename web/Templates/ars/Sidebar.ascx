<%@ Control Language="VB" %>
<%@ Register Src="ads/Panel.ascx" TagName="Panel" TagPrefix="ads" %>
<%@ Register Src="ads/Tower.ascx" TagName="Tower" TagPrefix="ads" %>
<%@ Register Src="Bubbles.ascx" TagName="Bubbles" TagPrefix="ars" %>
<%@ Register Src="PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
    <div id="Sidebar">
        <div class="Ad Panel iframe">
		<script type="text/javascript">
			var ppanel = cnp.ad.create(cnp.ad.refreshable, false);
			ppanel.addParameter({'sz':'300x250'});
			ppanel.load();
		</script>
	</div>

        <%--Front page stuff--%>
        <mbl:PathConditional runat="server" PathPattern="^/index">
        <div class="JournalsBox">
            <h2><span>Ars Journals</span></h2><ul class="Tabs Menu">
                <li id="AllJournalTab" class="Tab on"><mbl:HyperLink runat="server" ContentPath="/journals"><span class="Replace">All Journal Posts</span></mbl:HyperLink></li>
                <li id="ThumbsJournalTab" class="Tab"><mbl:HyperLink runat="server" ContentPath="/journals/thumbs"><span class="Replace">Opposable Thumbs Posts</span></mbl:HyperLink></li>
                <li id="AppleJournalTab" class="Tab"><mbl:HyperLink runat="server" ContentPath="/journals/apple"><span class="Replace">Infinite Loop Posts</span></mbl:HyperLink></li>
                <li id="MicrosoftJournalTab" class="Tab"><mbl:HyperLink runat="server" ContentPath="/journals/microsoft"><span class="Replace">One Microsoft Way Posts</span></mbl:HyperLink></li>
                <li id="LinuxJournalTab" class="Tab"><mbl:HyperLink runat="server" ContentPath="/journals/linux"><span class="Replace">#open.ended</span></mbl:HyperLink></li>
                <li id="HardwareJournalTab" class="Tab"><mbl:HyperLink runat="server" ContentPath="/journals/hardware"><span class="Replace">Kit</span></mbl:HyperLink></li>
                <li id="ScienceJournalTab" class="Tab"><mbl:HyperLink runat="server" ContentPath="/journals/science"><span class="Replace">Nobel Intent Posts</span></mbl:HyperLink></li>
                <li id="StaffJournalTab" class="Tab"><mbl:HyperLink runat="server" ContentPath="/staff"><span class="Replace">Staff.Ars Posts</span></mbl:HyperLink></li>
            </ul>
            <ul class="Headlines">
            <data:List runat="server" MaxResults="17">
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
            <h3><mbl:HyperLink runat="server" ContentPath="/journals"><html:Image runat="server" src="images/view-more.gif" alt="View More" /></mbl:HyperLink></h3></div>

        <data:List runat="server" Offset="1" MaxResults="1">
            <Data>
                <data:Index>
                    <Filters>
                        <data:IndexField Name="ActiveObjectsType" Value="Mubble.Models.Post" Mode="Require" />
                        <data:IndexField Name="Path" Value="/news" Mode="Exclude" />
                    </Filters>
                </data:Index>
            </Data>
            <ItemTemplate>
                <div class="FromTheJournalsBox <%# Container.DataItem.Controller.FileName %>">
                    <h2><span>From the Journals</span></h2>
                    <h3 class="Age3"><ars:PublishStatus runat="server" /><mbl:HyperLink runat="server" /></h3>
                    <p><data:Field runat="server" Name="Excerpt" /></p>
                    <p class="Tag">Posted <data:Field runat="server" Name="PublishDate" Format="{0:MMMM dd, yyyy @ h:mmtt}" /> -
                    <data:Authors runat="server">
                        <AuthorTemplate><mbl:HyperLink runat="server" /></AuthorTemplate>
                        <SeparatorTemplate>, </SeparatorTemplate>
                    </data:Authors></p>
                    <p class="Options">
                        <mbl:HyperLink runat="server"><html:Image runat="server" src="images/read-more.gif" alt="Read More" /></mbl:HyperLink>
                    </p>
                </div>
            </ItemTemplate>
        </data:List>
        <div class="NewsArchiveBox">
            <h2><span>Recent News Archive</span></h2>
            <ul class="Headlines">
            <data:List runat="server" Offset="20" MaxResults="14">
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
            <h3><mbl:HyperLink runat="server" ContentPath="/news"><html:Image runat="server" src="images/news-archive.gif" alt="News Archive" /></mbl:HyperLink></h3>
        </div>
        <html:Image runat="server" src="images/ars_emblem.jpg" alt="Serving the PC enthusiast for 8x10^2 centuries" />
        </mbl:PathConditional>

        <%--Content pages--%>
                <mbl:PathConditional PathPattern="\/(?!index).+" runat="server" Group="Sidebar" OnlyOneInGroup="true">
                <ars:Bubbles runat="server" />
                </mbl:PathConditional>
    </div>