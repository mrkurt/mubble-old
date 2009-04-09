<%@ Control AutoEventWireup="false" %>
<%@ Register Src="..\PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
    <div class="Bubble DontMiss">
        <h2><mbl:Hyperlink runat='server' ContentPath="/search" QueryString="Tag=dont+miss"><html:Image runat="server" src="images/bubble-dont-miss.gif" alt="Don't Miss" /></mbl:Hyperlink></h2>
        <ul class="Headlines">
        <data:List runat="server" MaxResults="3">
            <Data>
                <data:Index Group="Listings">
                    <Filters>
                        <data:IndexField Name="Tag" Value="dont miss" Mode="Require" />
                    </Filters>
                </data:Index>
            </Data>
            <ItemTemplate><li><ars:PublishStatus runat="server" /><mbl:HyperLink runat='server' QueryString="bub" /></li></ItemTemplate>
        </data:List>
        </ul>
    </div>