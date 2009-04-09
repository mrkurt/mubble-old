<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionPicker.ascx.cs" Inherits="Mubble.Web.Admin.SectionPicker" %>
<form runat="server" id="MoveToForm">
<h2>Move To</h2>
<ul>
<asp:Repeater runat="server" DataSource='<%# Mubble.Models.Controller.RootContent.GetAllChildren("IsContentContainer", true) %>'>
    <ItemTemplate>
        <li>
        <asp:LinkButton runat="Server" OnClick="SectionButton_Click" CommandArgument='<%# Eval("Id") %>'>
        <%# Eval("Path") %>
        </asp:LinkButton>
        </li>
    </ItemTemplate>
</asp:Repeater>
</ul>
</form>