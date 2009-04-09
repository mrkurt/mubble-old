<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/Admin/Default.master"  
CodeFile="Register.aspx.cs" Inherits="Mubble.Web.Client.Register" ValidateRequest="false" %>
<%@ Import Namespace="Mubble" %>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
<div id="PostForm">
<form runat="server">
    <asp:CreateUserWizard runat="server" FinishDestinationPageUrl='<%# Page.ResolveUrl("~/default.admin") %>'></asp:CreateUserWizard>
</form>
</div>
</asp:Content>