<%@ Control AutoEventWireup="false" %>
<%@ Register Src="..\PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
    <div class="Bubble FromTheJournals">
        <data:List runat="server" MaxResults="1">
            <Data>
                <data:Index Group="Listings">
                    <Filters>
                        <data:IndexField Name="ActiveObjectsType" Value="Mubble.Models.Post" Mode="Require" />
                        <data:IndexField Name="Path" Value="/news" Mode="Exclude" />
                    </Filters>
                </data:Index>
            </Data>
            <ItemTemplate>
                <h2><html:HyperLink Path="/journals" runat="server"><html:Image runat="server" src="images/bubble-from-the-journals.gif" alt="From the Journals" /></html:HyperLink></h2>
                <h3><ars:PublishStatus runat="server" /><mbl:HyperLink runat="server" QueryString="bub"><data:Field name="Title" runat="server" /></mbl:Hyperlink></h3>
                <p><data:Field Name="Excerpt" runat="server" /></p>
                <mbl:HyperLink runat="server" QueryString="bub"><html:Image runat="server" src="images/bubble-read-more.gif" alt="Read More" /></mbl:HyperLink>
            </ItemTemplate>
        </data:List>
    </div>