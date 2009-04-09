<%@ Control ClassName="TopStories" AutoEventWireup="false" %>
<%@ Register Src="..\PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
    <div class="Bubble TopStories">
        <h2><html:HyperLink runat="server" Path="/features"><html:Image runat="server" src="images/bubble-recent-features.gif" alt="Recent Features" /></html:HyperLink></h2>
        <ul class="Headlines">
        <data:List runat="server" Offset="3" MaxResults="5">
            <Data>
                <data:Index Group="Listings">
                    <Filters>
                        <data:IndexField Name="IsFeatured" Value="true" Mode="Require" />
                    </Filters>
                </data:Index>
            </Data>
            <ItemTemplate><li><ars:PublishStatus runat="server" /><mbl:HyperLink runat="server" QueryString="bub" /></li></ItemTemplate>
        </data:List>
        </ul>
    </div>