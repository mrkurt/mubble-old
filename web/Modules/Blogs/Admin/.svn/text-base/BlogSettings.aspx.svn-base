<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/Admin/Default.master"  
CodeFile="BlogSettings.aspx.cs" Inherits="Mubble.Web.Admin.BlogSettings" ValidateRequest="false" %>
<%@ Register TagPrefix="admin" TagName="ModulePicker" Src="~/Admin/UserControls/ModulePicker.ascx" %>
<%@ Import Namespace="Mubble" %>
<%@ Import Namespace="Mubble.Models" %>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
<div id="PostForm">
<form runat="server"><asp:ValidationSummary CssClass="FormErrors" runat="server" DisplayMode="BulletList" EnableClientScript="true" HeaderText="Problems!  Please correct the following errors, oversights, and omissions." />
    <label for="title" class="Title">Title:<asp:RequiredFieldValidator Text="*" ControlToValidate="txtTitle" ErrorMessage="The content title cannot be blank."  runat="Server"></asp:RequiredFieldValidator>
    </label>
    <asp:TextBox runat="server" ID="txtTitle" CssClass="Title"></asp:TextBox>
    <mbl:RichTextBox ID="txtBody" style="height: 120px;" runat="server" CssClass="wysiwyg"></mbl:RichTextBox>
    <div class="TextEditorOptions">
    (<mbl:RichTextBoxToggle runat="server"></mbl:RichTextBoxToggle>)
    </div>
    <div class="ButtonBarRight">
        <asp:LinkButton CssClass="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save"></asp:LinkButton>
        <asp:LinkButton
            CommandArgument="Draft"
            CssClass="DraftButton" 
            runat="server" 
            OnClick="SaveButton_Click"
            Text="Hide"
            Visible="<%# Controller.Status == Mubble.Models.PublishStatus.Published %>"></asp:LinkButton>
        <asp:LinkButton 
            CommandArgument="Publish"
            CssClass="PublishButton" 
            runat="server" 
            OnClick="SaveButton_Click"
            Text="Make Visible"
            Visible="<%# Controller.Status ==  Mubble.Models.PublishStatus.Draft %>"></asp:LinkButton>
    </div>
    <div id="AdvancedOptions" class="Expando Expanded">
        <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Discussion Options</h2>
        <div class="OptionsContainer">
            <p>Discussion Area:<br />
            <asp:DropDownList runat="server" ID="lstDiscussionProvider" />
            </p>
            <p><asp:CheckBox runat="server" ID="chckInlineDiscussions" Text="Inline Discussions?" /></p>
        </div>
    </div>
</form>
</div>
</asp:Content>
