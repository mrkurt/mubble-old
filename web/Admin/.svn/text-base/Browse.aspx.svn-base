<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse.aspx.cs" 
MasterPageFile="~/Templates/Admin/Default.master"
Inherits="Mubble.Web.Admin.Browse" Title="Browsing Content" %>
<%@ Register Src="~/Admin/UserControls/SectionPicker.ascx" TagPrefix="admin" TagName="SectionPicker" %>
<%@ Register Assembly="MubbleCore" Namespace="Mubble.UI.Data" TagPrefix="data" %>
<asp:Content runat="server" ContentPlaceHolderID="SubMenuLinks">
    <mbl:HyperLink 
        runat="Server" 
        ContentPath="<%# Controller.Path %>" 
        ContentPathExtra="/browse/add"
        Handler="AdminHandler"
        >Add Content to "<%# Controller.FileName %>"</mbl:HyperLink>
    <mbl:HyperLink CssClass="Clean"
        runat="Server" 
        ContentPath="<%# Controller.Path %>" 
        ContentPathExtra="/browse/search"
        Handler="AdminHandler"
        >Search</mbl:HyperLink>
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
    <asp:PlaceHolder ID="MoveToList" runat="server" Visible='<%# (Url.GetPathItem(1,"browse") == "move") %>'>
        <admin:SectionPicker onSectionClick="NewLocation_Click" ID="MoveToSection" runat="server" />
        <h2 runat="server" style="color: Green; text-align: center; color: #9999FF;" id="MoveToResult" visible="false"></h2>
    </asp:PlaceHolder>
    <asp:PlaceHolder id="Listing" runat="server" visible='<%# (Url.GetPathItem(1,"browse") == "browse") %>'>
    <table class="AdminListing">
        <col />
        <col class="Title" />
        <col />
        <col />
        <col />
        <thead>
            <tr>
                <th colspan="6">
                    <mbl:BreadCrumbTrail runat="server" Content="<%# Page.Controller %>">
                        <ItemTemplate>
                            <mbl:HyperLink runat="server" 
                            ContentPath='<%# DataBinder.Eval(Container.DataItem, "Path") %>'
                            ContentPathExtra="/browse" 
                            Handler="AdminHandler"
                            Visible='<%# (Page.Controller.Path != DataBinder.Eval(Container.DataItem, "Path").ToString() ) %>'>
                                <%#DataBinder.Eval(Container.DataItem, "Title")%>
                            </mbl:HyperLink>
                            <asp:Label Visible='<%# (Page.Controller.Path == DataBinder.Eval(Container.DataItem, "Path").ToString()) %>' runat="server">
                                <%# DataBinder.Eval(Container.DataItem, "Title") %>
                            </asp:Label>
                        </ItemTemplate>
                        <SeparatorTemplate> &raquo; </SeparatorTemplate>
                    </mbl:BreadCrumbTrail>
                </th>
            </tr>
            <tr class="Alternate">
                <th class="Images">
                <mbl:HyperLink runat="server" 
                    Content="<%# Controller %>" 
                    ContentPathExtra="/browse/add"
                    ImageUrl='<%# (AdminTemplateBase + "Images/icon_new_content.gif") %>'
                    Handler="AdminHandler"
                    ToolTip="Add new content"
                    ></mbl:HyperLink>
                </th>
                <th class="Title"><!--input type="checkbox" onclick="checkAll(this, 'blog_page_check');" /--> Title</th>
                <th>Options</th>
                <th>Type</th>
                <th>Function</th>
                <th>Published</th>
            </tr>
        </thead>
        <tbody>
        <asp:Repeater runat="Server" DataSource='<%# Controller.Controllers.Sort("Title") %>'>
            <ItemTemplate>
                <mbl:RepeaterTableRow runat="server" AlternatingCssClass="Alternate">
                    <td class="IconList">
                    <mbl:HyperLink runat="Server" 
                        ContentPath='<%# DataBinder.Eval(Container.DataItem, "Path") %>'
                        ImageUrl='<%# (AdminTemplateBase + "Images/icon_browse.gif") %>'
                        ToolTip="Keep Browsing"
                        ContentPathExtra="/browse"
                        Handler="AdminHandler">
                        </mbl:HyperLink>
                    </td>                
                    <td>
                    <mbl:HyperLink runat="Server" 
                        ContentPath='<%# DataBinder.Eval(Container.DataItem, "Path") %>'
                        Handler="AdminHandler"
                        ToolTip="Edit this content"
                        >
                    <%# DataBinder.Eval(Container.DataItem, "Title") %>
                    </mbl:HyperLink>
                    </td>
                    <td class="ContentOptions">
                    [<mbl:HyperLink runat="Server" 
                        ContentPath='<%# DataBinder.Eval(Container.DataItem, "Path") %>'
                        ToolTip="View content">View</mbl:HyperLink>]
                    [<mbl:HyperLink runat="Server" 
                        ContentPath='<%# DataBinder.Eval(Container.DataItem, "Path") %>'
                        ToolTip="Keep browsing."
                        ContentPathExtra="/browse"
                        Handler="AdminHandler">Browse</mbl:HyperLink>]
                    [<mbl:HyperLink runat="Server" 
                        ContentPath='<%# DataBinder.Eval(Container.DataItem, "Path") %>'
                        ToolTip='<%# Eval("FileName", "Move \"{0}\" to a new location") %>'
                        ContentPathExtra="/browse/move"
                        Handler="AdminHandler">Move</mbl:HyperLink>]                        
                    </td>
                    <td><%# ((Mubble.Models.Controller)Container.DataItem).ModuleControl.Type %></td>
                    <td><%# ((Mubble.Models.Controller)Container.DataItem).ModuleControl.Name%></td>
                    <td><mbl:PublishStatus runat="server" PublishDate='<%# Eval("PublishDate") %>' Status='<%# Eval("Status") %>' />
                    <%# DataBinder.Eval(Container.DataItem, "PublishDate", "{0:MM/dd/yyyy}") %></td>
                </mbl:RepeaterTableRow>
            </ItemTemplate>
        </asp:Repeater>
            <tr runat="server" visible='<%# Controller.Controllers.Count <= 0 %>'>
                <td colspan="5" style="text-align: center; padding: 15px; font-style: italic; font-weight: bold;">
                No additional content for "<%# Controller.FileName %>".  You may 
                <mbl:HyperLink 
                    runat="Server" 
                    ContentPath="<%# Controller.Path %>" 
                    ContentPathExtra="/browse/add"
                    Handler="AdminHandler"
                    >add some</mbl:HyperLink>
                    
                     if this is a good place for it.
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <th class="Images">
                <mbl:HyperLink runat="server" 
                    Content="<%# Controller %>" 
                    ContentPathExtra="/browse/add"
                    ImageUrl='<%# (AdminTemplateBase + "Images/icon_new_content.gif") %>'
                    Handler="AdminHandler"
                    ToolTip="Add new content"
                    ></mbl:HyperLink>
                </th>
                <th colspan="4">
                    <mbl:HyperLink 
                    runat="Server" 
                    ContentPath="<%# Controller.Path %>" 
                    ContentPathExtra="/browse/add"
                    Handler="AdminHandler"
                    >Add new content</mbl:HyperLink>
                </th>
            </tr>
        </tfoot>
    </table>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="Add" runat="server" visible='<%# (Url.GetPathItem(1) == "add") %>'>
        <form class="AddContent" runat="server" id="AddContentForm">
        <h2 class="Blue">Add Content</h2>
        <asp:ValidationSummary CssClass="FormErrors" runat="server" DisplayMode="BulletList" EnableClientScript="true" HeaderText="Oops! Please fix the following:" />
            <p><label for="title">Title:<asp:RequiredFieldValidator Text="*" ControlToValidate="txtTitle" ErrorMessage="The content title cannot be blank."  runat="Server"></asp:RequiredFieldValidator>
            </label>
            <asp:TextBox runat="server" ID="txtTitle" CssClass="TextBox"></asp:TextBox></p>
            <p><label class="TextBox">Filename:</label>
            <asp:TextBox runat="server" ID="txtFileName" CssClass="TextBox"></asp:TextBox></p>           
            <p><label>Module:</label>
                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="lstModules_Changed" id="lstModules" runat="server">
                </asp:DropDownList>
            </p>
            <p><label>View:</label>
                <asp:DropDownList ID="lstControls" runat="server">
                </asp:DropDownList>
            </p>
            <div class="ButtonBarRight" style="float: none; width: auto; padding: 6px;">
                <asp:LinkButton OnClick="SaveButton_Click" CssClass="SaveButton" runat="server" Text="Continue"></asp:LinkButton>
            </div>                 
        </form>
    </asp:PlaceHolder>
</asp:Content>