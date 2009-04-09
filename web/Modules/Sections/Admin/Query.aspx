<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/Admin/Default.master"  
CodeFile="Query.aspx.cs" Inherits="Mubble.Web.Admin.Query" ValidateRequest="false" %>
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
            Visible="<%# Controller.Status == Mubble.Models.PublishStatus.Draft %>"></asp:LinkButton>
    </div>
    <div id="AdvancedOptions" class="Expando Expanded">
        <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Query</h2>
        <div class="OptionsContainer">
        <p>Enter the content query here:</p>
        <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
        <h2>Publishing</h2>
        <div class="OptionsContainer">
        <p><asp:CheckBox runat="server" ID="chckAllowPublish" Text="Allow child content" /></p>
        </div>
    </div>
</form>
</div>
</asp:Content>
