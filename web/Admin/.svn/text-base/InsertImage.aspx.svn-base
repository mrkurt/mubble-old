<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="InsertImage.aspx.cs" 
    MasterPageFile="~/Templates/Admin/Popup.master"
    Inherits="Mubble.Web.Admin.InsertImage" 
    Title="{$lang_mubbleimage_title}"
    %>
<%@ Import Namespace="Mubble.Tools" %>
<asp:Content ContentPlaceHolderID="PageHead" runat="server">
    
	<script language="javascript" type="text/javascript">
	<!--
		function init() {
			
		}
		
		function imageSelected(link){
		    var src = link.href;
		    var popUrl = '<%# AdminUrl + "/InsertImage?template=Popup&src=" %>' + escape(src);
		    if(src.indexOf('src=') < 0){
		        link.href = popUrl;
		    }
		}
	//-->
	</script>
	<% if(SectionImageOptions.Visible){ %>
	<script type="text/javascript" src="<%# Page.ResolveUrl("~/jscripts/tiny_mce/utils/form_utils.js") %>"></script>	
	<script type="text/javascript" src="<%#Page.ResolveUrl("~/jscripts/tiny_mce/plugins/mubbleimage/jscripts/functions.js") %>"></script>
	<% } %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <mbl:StyleSheet runat="server" href='<%#ResolveUrl(AdminTemplateBase + "styles/media.css")%>' />
 <!-- {$lang_mubbleimage_title} -->
    <asp:PlaceHolder runat="server" ID="SectionSourceSelection" Visible="false">
        <h1>Choose a Media Source</h1>
        <div class="MediaSourceSelection">
            <h2>Upload media</h2>
            <form runat="server">
            <p class="FormField">
            <label>Local File:</label>
            <asp:FileUpload runat="server" ID="fileUpload" /></p>
            <p class="Button"><asp:Button runat="server" Text="Save" OnClick="UploadButton_click" /></p>
            </form>
        </div>
        <div class="MediaSourceSelection">
            <form action="<%# Request.RawUrl %>" name="formFilter" method="get">
            <input type="hidden" name="template" value="<%# Server.HtmlEncode(Request.QueryString["template"]) %>" />
            <input type="hidden" name="view" value="list" />
            <h2>Search "<%# Controller.FileName %>" media</h2>
            <p class="FormField">
            <label>Search Text:</label>
            <input type="text" id="filter" class="TextBox" name="filter" />
            </p>
            <p class="Button"><input type="submit" value="Submit">
            <br />
            <mbl:HyperLink runat="server" 
                Handler="AdminHandler"
                Content="<%# Controller %>"
                ContentPathExtra="/InsertImage" 
                QueryString='<%# "view=list&template=" + Server.HtmlEncode(Request.QueryString["template"]) %>'
                >All Images</mbl:HyperLink>
            </p>
            </form>
        </div>
        <div class="MediaSourceSelection" <%# ConditionalWrite(Controller.Path, Mubble.Models.Controller.RootContent.Path, "style=\"display:none;\"") %>>
            <form id="sharedImagesForm" action="<%# Request.RawUrl %>" name="urlForm" method="get">
            <input type="hidden" name="template" value="<%# Server.HtmlEncode(Request.QueryString["template"]) %>" />
            <h2>Choose from Shared Image Pool</h2>
            <p class="FormField">
            <label>Image:</label>
            <select name="src" onchange="if(this.selectedIndex > 0) document.getElementById('sharedImagesForm').submit();">
            <option></option>
            <asp:Repeater ID="SharedImageList" runat="server">
                <ItemTemplate>
                    <option value="<%# Eval("FileName") %>"><%# Eval("FileName") %></option>
                </ItemTemplate>
            </asp:Repeater>
            </select>
            </p>
            <p class="Button"><input type="submit" value="Next" /></p>
            </form>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="Server" ID="SectionImageOptions" Visible="false">
        <script type="text/javascript">
            var width = <%# MediaSize.Width %>;
            var host = '<%# MediaHost %>';
            var height = <%# MediaSize.Height %>;
            var mediaFile = '<%# MediaFileName %>';
            var baseMediaUrl = '<%# BaseMediaUrl %>';
            var rawMediaUrl = '<%# RawMediaUrl %>';
            var isMubbleMedia = <%# IsMubbleMedia.ToString().ToLower() %>;
        </script>
        <form>
        <table class="ImageEditor">
            <tr>
                <th colspan="2">Options</th>
                <th>Preview</th>
            </tr>
            <tr>
                <td colspan="2" style="width: 135px;"></td>
                <td class="Preview" style="width: 315px; text-align: center;" rowspan="7">
                    <div style="width: 295px; height: 200px; overflow: auto; text-align: center;vertical-align:middle;">
                    <img id="PreviewImage" src="<%# RawMediaUrl %>" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;Title:</td><td><input type="text" name="alt" id="alt" onblur="titleFieldBlur();" />&nbsp;</td>
            </tr>       
            <tr>
                <td>&nbsp;W:</td><td><input onblur="sizeFieldBlur(this)" type="text" value="<%# (MediaSize.Width > 0) ? MediaSize.Width.ToString() : null %>" id="txtImageWidth" <%# (!this.IsMubbleMedia) ? "disabled=\"disabled\"" : null %> /></td>
            </tr>
            <tr>
                <td>&nbsp;H:</td><td><input onblur="sizeFieldBlur(this)" type="text" value="<%# (MediaSize.Height > 0) ? MediaSize.Height.ToString() : null %>" id="txtImageHeight" <%# (!this.IsMubbleMedia) ? "disabled=\"disabled\"" : null %>  /></td>
            </tr>
            <tr>
                <td>&nbsp;Class:</td><td><select id="classlist" name="classlist"></select></td>
            </tr>
            <tr>
                <td style="text-align: right;">&nbsp;<input <%# (!this.IsMubbleMedia) ? "disabled=\"disabled\"" : null %> type="checkbox" name="popup" style="border: 0px; background-color:Transparent;" /></td>
                <td><label for="popup">Open full image on click.</label></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right;"><input type="button" value="Insert" onclick="insertAction();" />&nbsp;</td>
            </tr>
        </table>
        </form>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="SectionImageList" Visible="false">
        <table class="MediaListing">
            <col class="Icon" />
            <col class="Title" />
            <col class="Details" />
            <col class="Options" />
            <thead>
                <tr><th>&nbsp;</th><th>Title</th><th>Details</th></tr>
            </thead>
            <tbody>
                <asp:Repeater 
                    runat="Server" 
                    id="MediaList"
                    DataSource='<%# Controller.Files %>'
                >
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Hyperlink
                                runat="server"
                                ImageUrl='<%# MediaUrl + "/50/50/" + DataBinder.Eval(Container.DataItem, "FileName") %>'
                                NavigateUrl='<%# MediaUrl + "/" + DataBinder.Eval(Container.DataItem, "FileName") %>'
                                onclick="imageSelected(this);"
                                />
                            </td>
                            <td>
                                <asp:Hyperlink
                                runat="server"
                                NavigateUrl='<%# MediaUrl + "/" + DataBinder.Eval(Container.DataItem, "FileName") %>'
                                onclick="imageSelected(this);"
                                >
                                <%# DataBinder.Eval(Container.DataItem, "FileName") %>
                                </asp:Hyperlink>
                            </td>
                            <td>
                            <span class="MinorDetails">                            
                            <%# DataBinder.Eval(Container.DataItem, "MediaType") %>/<%# DataBinder.Eval(Container.DataItem, "MediaSubType") %>
                            </span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="3">
                    
                    </td>
                </tr>
            </tfoot>
        </table>
    </asp:PlaceHolder>
</asp:Content>

