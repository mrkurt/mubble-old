<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/Admin/Default.master"  
CodeFile="Content.aspx.cs" Inherits="Mubble.Web.Admin.Content" ValidateRequest="false" %>
<%@ Import Namespace="Mubble" %>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
<div id="PostForm">
<form runat="server"><asp:ValidationSummary CssClass="FormErrors" runat="server" DisplayMode="BulletList" EnableClientScript="true" HeaderText="Problems!  Please correct the following errors, oversights, and omissions." />
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
    <div id="AdvancedOptions" class="Expando">
        <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Advanced Options</h2>
        <div class="OptionsContainer">
            <p>Publish Date:<br />
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
                &nbsp;
                <asp:DropDownList ID="lstHour" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="lstMinute" runat="Server"></asp:DropDownList>
                <asp:DropDownList ID="lstAmPm" runat="server">
                    <asp:ListItem Value="AM">AM</asp:ListItem>
                    <asp:ListItem Value="PM">PM</asp:ListItem>
                </asp:DropDownList>
            </p>
        </div>
    </div>
</form>
</div>
</asp:Content>
