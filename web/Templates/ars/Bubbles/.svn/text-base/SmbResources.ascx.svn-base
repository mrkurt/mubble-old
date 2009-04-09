<%@ Control ClassName="SmbResources" AutoEventWireup="false" %>
<%@ Register Src="..\PublishStatus.ascx" TagName="PublishStatus" TagPrefix="ars" %>
    <div class="Bubble SmbResources">
        <h2><html:HyperLink runat="server" Path="/business/smb-resources"><html:Image runat="server" src="images/small-business-resources.png" alt="Small Business Resources" /></html:HyperLink></h2>
        <p>This article is part of an editorial series focused on technology solutions for small business professionals, brought to you by 
        Comcast.  
        Find more original Ars stories on small business here:</p>
        <ul class="Headlines">
        <data:List runat="server" Offset="3" MaxResults="5">
            <Data>
                <data:Index Group="Listings">
                    <Filters>
                        <data:IndexField Name="ad-cat" Value="itbiz_smb" Mode="Require" />
                    </Filters>
                </data:Index>
            </Data>
            <ItemTemplate><li><ars:PublishStatus runat="server" /><mbl:HyperLink runat="server" QueryString="bub" /></li></ItemTemplate>
        </data:List>
        </ul>
    </div>