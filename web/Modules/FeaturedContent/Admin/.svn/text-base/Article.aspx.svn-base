<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/Admin/Default.master"  
CodeFile="Article.aspx.cs" Inherits="Mubble.Web.Admin.Article" ValidateRequest="false" %>
<%@ Register Src="~/Admin/UserControls/Metadata/MultiValueRankedString.ascx" TagPrefix="mbl" TagName="MetadataMultiValue" %>
<%@ Register Src="~/Admin/UserControls/Metadata/FeaturedContent.ascx" TagPrefix="mbl" TagName="FeaturedContent" %>
<%@ Register Src="~/Admin/UserControls/Metadata/StringValue.ascx" TagPrefix="mbl" TagName="MetadataValue" %>
<%@ Register Src="~/Admin/UserControls/DiscussionOptions.ascx" TagPrefix="mbl" TagName="DiscussionOptions" %>
<%@ Import Namespace="Mubble" %>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
<form runat="server" enctype="multipart/form-data">
<div id="PostForm">
    <asp:ValidationSummary CssClass="FormErrors" runat="server" DisplayMode="BulletList" EnableClientScript="true" HeaderText="Problems!  Please correct the following errors, oversights, and omissions." />
    <label for="title" class="Title">Title:<asp:RequiredFieldValidator Text="*" ControlToValidate="txtTitle" ErrorMessage="The content title cannot be blank."  runat="Server"></asp:RequiredFieldValidator>
    </label>
    <asp:TextBox runat="server" ID="txtTitle" CssClass="Title"></asp:TextBox>
    <div class="Expando">
        <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Blurb</h2>
        <div class="OptionsContainer">
        <asp:TextBox ID="txtContentBody" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div id="PageSelector" class="Expando Expanded">
        <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Article Pages</h2>
        <table>
            <col class="PageNumbers" />
            <col class="IconOptions" />
            <col class="Names" />
            <col class="Delete" />
        <asp:Repeater runat="server" ID="PageList">
            <ItemTemplate>
                <tr class="<%# ConditionalWrite(
                                CurrentPageNumber.ToString(), 
                                Eval("PageNumber").ToString(), 
                                "Selected") 
                            %>">
                    <td class="PageNumber">
                        <asp:LinkButton 
                            runat="server" 
                            OnClick="PageSelector_Click" 
                            CommandArgument='<%# Eval("PageNumber") %>'
                            ><%# Eval("PageNumber") %></asp:LinkButton>
                        <asp:Label
                                runat="server" 
                                ToolTip="This page has changed since the last save" 
                                Visible='false'>*</asp:Label>
                    </td>
                    <td>
                        <asp:Image
                            runat="server"
                            ImageUrl='<%# AdminTemplateBase + "Images/icon_up_inactive.gif" %>'
                            Visible='<%# (int)Eval("PageNumber") <= 1 %>'
                            ToolTip="" />
                        <asp:LinkButton 
                            runat="server" 
                            OnClick="PageUp_Click"
                            CommandArgument='<%# Eval("PageNumber") %>'
                            Visible='<%# (int)Eval("PageNumber") > 1 %>'
                        ><img src="<%# AdminTemplateBase + "Images/icon_up.gif" %>" alt="Move Up" /></asp:LinkButton>
                        <asp:Image
                            runat="server"
                            ImageUrl='<%# AdminTemplateBase + "Images/icon_down_inactive.gif" %>'
                            Visible='<%# (int)Eval("PageNumber") >= FeaturedContent.Pages.Count %>'
                            ToolTip="" />
                        <asp:LinkButton
                            runat="server" 
                            OnClick="PageDown_Click"
                            CommandArgument='<%# Eval("PageNumber") %>'
                            Visible='<%# (int)Eval("PageNumber") < FeaturedContent.Pages.Count %>'
                        ><img src="<%# AdminTemplateBase + "Images/icon_down.gif" %>" alt="Move Down" /></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton 
                            runat="server" 
                            OnClick="PageSelector_Click" 
                            CommandArgument='<%# Eval("PageNumber") %>'
                            Visible = '<%# (int)Eval("PageNumber") != CurrentPageNumber  %>'
                            ><%# Eval("Name") %></asp:LinkButton>
                        <asp:TextBox
                            runat="server"
                            id="txtPageName"
                            Text='<%# Eval("Name") %>'
                            visible = '<%# (int)Eval("PageNumber") == CurrentPageNumber  %>'
                            cssclass="TextBox"
                            ></asp:TextBox>
                        <asp:HiddenField runat="server" ID="txtPageNumber" Value='<%# Eval("PageNumber") %>' />
                    </td>
                    <td>
                        <asp:ImageButton 
                            runat="server" 
                            OnClick="PageDelete_Click"
                            Enabled='<%# (FeaturedContent.Pages.Count > 1) %>'
                            ImageUrl='<%# AdminTemplateBase + "images/icon_trashcan.gif" %>' 
                            CommandArgument='<%# Eval("PageNumber") %>'
                            ToolTip='<%# Eval("Name", "Remove \"{0}\"")%>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
            <tr>
                <td colspan="4" class="AddPageCell"><asp:LinkButton OnClick="AddPage_Click" runat="server">Add page</asp:LinkButton></td>
            </tr>
        </table>
    </div>
    <input type="hidden" runat="server" id="txtCurrentPage" value="<%# CurrentPageNumber %>" />
    <input type="hidden" runat="server" id="txtNewPageName" />
    <mbl:RichTextBox ID="txtBody" runat="server" CssClass="wysiwyg"></mbl:RichTextBox>
    <div class="ButtonBarRight">
        <asp:LinkButton CssClass="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save"></asp:LinkButton>
        <asp:LinkButton
            CommandArgument="Draft"
            CssClass="DraftButton" 
            runat="server" 
            OnClick="SaveButton_Click"
            Text="Revert to Draft"
            Visible="<%# Controller.Status == Mubble.Models.PublishStatus.Published %>"></asp:LinkButton>
        <asp:LinkButton 
            CommandArgument="Publish"
            CssClass="PublishButton" 
            runat="server" 
            OnClick="SaveButton_Click"
            Text="Publish Now"
            Visible="<%# Controller.Status == Mubble.Models.PublishStatus.Draft %>"></asp:LinkButton>
    </div>
    <div class="TextEditorOptions">
    (<mbl:RichTextBoxToggle runat="server"></mbl:RichTextBoxToggle>)
    </div>
</div>
<div id="SideBar">
    <mbl:FeaturedContent runat="server" Title="Featured Article" />
    <mbl:MetadataValue runat='server' Title="URL to PDF" Field="PdfLink" />
    <mbl:MetadataMultiValue runat="server" Title="Authors" Field="Author" Description="One username per line" ID="Authors" />
    <h2>Excerpt</h2>
    <p><asp:TextBox ID="txtExcerpt" runat="server" TextMode="MultiLine"></asp:TextBox></p>
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
</asp:Content>