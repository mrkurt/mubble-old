<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenForum.ascx.cs" Inherits="Templates_ars_OpenForum"  %>
    <if:Path StartsWith="!/site" runat='server'>
    <div class="Bubble OpenForum">
        <h2><a href="http://episteme.arstechnica.com"><html:Image runat="server" src="images/bubble-openforum.gif" alt="OpenForum" /></a></h2>
        <ul class="Headlines">
        <mbl:RssFeed ID="Feed" MaxItems="5" runat="server">
            <ItemTemplate><li><asp:Hyperlink runat="server" NavigateURL='<%# Eval("Link") %>'>
            <%# Eval("Title") %>
            </asp:Hyperlink></li></ItemTemplate>
        </mbl:RssFeed>
        </ul>
    </div>
    </if:Path>