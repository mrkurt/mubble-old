<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Media.aspx.cs" 
    Inherits="Mubble.Web.Admin.Media" 
    MasterPageFile="~/Templates/Admin/Default.master"
    Title="Content media"    
    %>
<%@ Import Namespace="Mubble.Tools" %>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <mbl:StyleSheet runat="server" href='<%#ResolveUrl(AdminTemplateBase + "styles/media.css")%>' />
    <form runat="server">
    <div id="TableContainer">
        <div class="UploadFile">
        File: <asp:FileUpload runat="server" ID="file" />
        <asp:Button ID="btnUpload" runat="server" class="Button" Text="Upload" OnClick="btnUpload_Click" />
        </div>
        <table class="MediaListing">
            <col class="Icon" />
            <col class="Title" />
            <col class="Details" />
            <col class="Options" />
            <thead>
                <tr><th>&nbsp;</th><th>Title</th><th>Details</th><th>Options</th></tr>
            </thead>
            <tbody>
                <data:List ID="Files" runat="server" EnableViewState="false">
                    <Pager PageSize="100" />
                    <Data><data:Files /></Data>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Hyperlink
                                runat="server"
                                ImageUrl='<%# MediaUrl + "/w{50}/h{50}/" + Eval("FileName") %>'
                                NavigateUrl='<%# MediaUrl + "/" + Eval("FileName") %>'
                                />
                            </td>
                            <td>
                                <asp:Hyperlink 
                                runat="server"
                                NavigateUrl='<%# MediaUrl + "/" + Eval("FileName") %>'
                                >
                                <%# Eval("FileName") %>
                                </asp:Hyperlink>
                            </td>
                            <td>
                            <span class="MinorDetails">                            
                            <%# Eval("MediaType") %>/<%# Eval("MediaSubType") %>
                            </span>
                            </td>
                            <td class="Options">
                                [<mbl:HyperLink 
                                    runat="server"
                                    Handler="AdminHandler"
                                    ContentPath='<%# Controller.Path %>'
                                    ContentPathExtra='<%# DataBinder.Eval(Container.DataItem, "ID", "/media/delete/{0}") %>'
                                    QueryString='<%# string.Format("source={0}", Server.UrlEncode(Request.RawUrl)) %>'
                                    >Delete</mbl:HyperLink>]
                            </td>
                        </tr>
                    </ItemTemplate>
                </data:List>
            </tbody>
        </table>
    </div>
    <div id="SideBar">
    <h2><%# Controller.Title %></h2>
    <h2>Browse Media</h2>
    <ul>
        <li class="Center"><html:PagingLink runat="server" PageLinkFor="Files" Mode="PreviousPage" Handler="AdminHandler">&lt; Prev Page</html:PagingLink>
        <html:PagingLink runat="server" PageLinkFor="Files" Mode="NextPage" Handler="AdminHandler">Next Page &gt;</html:PagingLink></li>
        <li class="Center">Page: <html:PagingDropDownList runat="server" PageLinkFor="Files" Handler="AdminHandler" /></li>
    </ul>
    </div>
    </form>
</asp:Content>
