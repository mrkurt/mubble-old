<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="Mubble.Web.Admin.Blog"
MasterPageFile="~/Templates/Admin/Default.master" ValidateRequest="false"%>
<%@ Register Src="~/Admin/UserControls/Metadata/MultiValueRankedString.ascx" TagPrefix="mbl" TagName="MetadataMultiValue" %>
<%@ Register Src="~/Admin/UserControls/Metadata/BooleanValue.ascx" TagPrefix="mbl" TagName="MetadataBooleanValue" %>
<%@ Register Src="~/Admin/UserControls/Metadata/FeaturedContent.ascx" TagPrefix="mbl" TagName="FeaturedContent" %>
<%@ Register Src="~/Admin/UserControls/DiscussionOptions.ascx" TagPrefix="mbl" TagName="DiscussionOptions" %>
<%@ Import Namespace="Mubble" %>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
    <asp:PlaceHolder ID="Post" runat="server" Visible='<%# (Url.GetPathItem(1, "posts") == "post") %>'>
        <form runat="server" enctype="multipart/form-data">
        <div id="PostForm">
        <asp:ValidationSummary CssClass="FormErrors" runat="server" DisplayMode="BulletList" EnableClientScript="true" HeaderText="Problems!  Please correct the following errors, oversights, and omissions." />
            <label for="title" class="Title">Title:<asp:RequiredFieldValidator Text="*" ControlToValidate="txtTitle" ErrorMessage="The content title cannot be blank."  runat="Server"></asp:RequiredFieldValidator>
            </label>
            <asp:TextBox runat="server" ID="txtTitle" CssClass="Title"></asp:TextBox>
            <mbl:RichTextBox ID="txtBody" runat="server" CssClass="wysiwyg"></mbl:RichTextBox>
            <div class="TextEditorOptions">
            (<mbl:RichTextBoxToggle runat="server"></mbl:RichTextBoxToggle>)
            </div>
            <div class="ButtonBarRight">
                <asp:LinkButton CssClass="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save"></asp:LinkButton>
                <asp:LinkButton
                    ID="btnDraftSave"
                    CommandArgument="Draft"
                    CssClass="DraftButton" 
                    runat="server" 
                    OnClick="SaveButton_Click"
                    Text="Revert to Draft"
                    Visible="false"></asp:LinkButton>
                <asp:LinkButton
                    ID="btnPublishSave"
                    CommandArgument="Publish"
                    CssClass="PublishButton" 
                    runat="server" 
                    OnClick="SaveButton_Click"
                    Text="Publish Now"
                    Visible="true"></asp:LinkButton>
            </div>          
            <div id="AdvancedOptions" class="Expando Expanded">
                <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Advanced Options</h2>
                <div class="OptionsContainer">
                    <p>Slug:<br />
                    <asp:TextBox ID="txtSlug" CssClass="TextBox" runat="server"></asp:TextBox>
                    </p>
                </div>
            </div>

        </div>
        <div id="SideBar">
            <asp:PlaceHolder ID="AuthorSelector" runat="server">
            <h2>Author Username</h2>
            <p><asp:TextBox runat="server" ID="txtUserName" /></p>
            </asp:PlaceHolder>
            <mbl:FeaturedContent runat="server" Title="Featured Content" />
            <h2>Excerpt</h2>
            <p><asp:TextBox ID="txtExcerpt" runat="server" TextMode="MultiLine"></asp:TextBox></p>
            <mbl:MetaDataBooleanValue ID="chckElevated" runat="server" Title="Frontpage" Field="Elevated" Description="Include this post on front page?" />
            <mbl:MetadataMultiValue runat="server" Title="Tags" Field="Tag" Description="One tag per line, spaces and punctuation allowed" ID="Tags" />
            <mbl:DiscussionOptions runat="server" Title="Discussion Link" Description="" />
            <h2>Publish Time</h2>
            <p class="Center">
                <asp:DropDownList ID="lstMonth" runat="Server">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="lstDay" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="lstYear" runat="server"></asp:DropDownList>
            </p>
            <p class="Center">
                <asp:DropDownList ID="lstHour" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="lstMinute" runat="Server"></asp:DropDownList>
                <asp:DropDownList ID="lstAmPm" runat="server">
                    <asp:ListItem Value="AM">AM</asp:ListItem>
                    <asp:ListItem Value="PM">PM</asp:ListItem>
                </asp:DropDownList>
            </p>
        </div>
                </form>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="Listing" runat="server" Visible='<%# (Url.GetPathItem(1, "posts") == "posts") %>'>
        <form runat='server'>
        <table class="AdminListing">
        <col />
        <col />
        <col />
        <col />
        <col />
        <col />
        <thead>
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
                <th class="Title"><!--input type="checkbox" onclick="checkAll(this, 'blog_page_check');" /--> Title</th>
                <th>Author</th>
                <th>Post Date</th>
                <th>Time</th>
            </tr>
        </thead>
        <tbody>
        <blog:Posts runat="Server" ID="Posts" PageSize="20">
            <ItemTemplate>
                <mbl:RepeaterTableRow runat="server" AlternatingCssClass="Alternate">
                    <td class="IconList">
                    <mbl:PublishStatus runat="server" Status='<%# Eval("Status") %>' PublishDate='<%# Eval("PublishDate") %>' />
                    </td>                
                    <td>
                    <mbl:HyperLink runat="server" UrlPair='<%# Eval("Url") %>' runat="server"><mbl:Image src="Images/icon_view.gif" alt="Preview Post" runat="server" /></mbl:HyperLink>
                    <mbl:HyperLink runat="Server" 
                        ContentPath='<%# Controller.Path %>'
                        ContentPathExtra='<%# Eval("Slug", "/m-blog/post/{0}") %>'
                        Handler="AdminHandler"
                        ToolTip="Edit this post"
                        >
                    <%# Server.HtmlEncode(Eval("Title").ToString()) %>
                    </mbl:HyperLink>
                    </td>
                    <td><%# Eval("UserName") %></td>
                    <td><%# Eval("PublishDate", "{0:MM/dd/yyyy}") %></td>
                    <td><%# Eval("PublishDate", "{0:HH:mm}") %></td>
                </mbl:RepeaterTableRow>
            </ItemTemplate>
        </blog:Posts>
        </tbody>
    </table>
    <div id="SideBar">
        <h2><%# Controller.Title %></h2>
        <ul>
            <li><mbl:HyperLink runat="Server" ContentPathExtra="/m-blog/post" Handler="AdminHandler"><mbl:Image src="Images/icon_new_content.gif" runat="server" /></mbl:HyperLink> 
            <mbl:HyperLink runat="Server" ContentPathExtra="/m-blog/post" Handler="AdminHandler">Create a post</mbl:HyperLink></li>
        </ul>
        <h2>Browse Posts</h2>
        <ul>
            <li class="Center"><mbl:PagingLink runat="server" PageLinkFor="Posts" Mode="PreviousPage">&lt; Prev Page</mbl:PagingLink> <mbl:PagingLink runat="server" PageLinkFor="Posts" Mode="NextPage">Next Page &gt;</mbl:PagingLink></li>
            <li class="Center">Page: <mbl:PagingDropDownList runat="server" PageLinkFor="Posts" /></li>
        </ul>
    </div>
    </form>
    </asp:PlaceHolder>
</asp:Content>