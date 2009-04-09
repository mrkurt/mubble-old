<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Jobs.ascx.cs" Inherits="Templates_ars_Jobs"  %>
    <div class="Bubble Jobs">
        <h2><a href="http://jobs.arstechnica.com"><html:Image runat="server" src="images/bubble-jobs.gif" alt="Jobs!" /></a></h2>
        <ul class="Headlines">
        <mbl:RssFeed ID="Feed" MaxItems="5" runat="server">
        <ItemTemplate><li><asp:Hyperlink runat="server" NavigateURL='<%# Eval("Link") %>'>
            <strong><%# FormatTitle(Eval("Title")) %></strong>
            <%# FormatLocation(Eval("Description")) %>
            </asp:Hyperlink></li></ItemTemplate>
        </mbl:RssFeed>
        </ul>
        <a href="http://jobs.arstechnica.com"><html:Image runat="server" src="images/more-jobs.gif" alt="More Jobs" /></a>
    </div>