<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" 
MasterPageFile="~/Templates/Admin/Default.master"
Inherits="Mubble.Web.Admin.Settings" %>
<%@ Register TagPrefix="admin" TagName="ModulePicker" Src="~/Admin/UserControls/ModulePicker.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="SubMenuLinks">
    <mbl:HyperLink 
        runat="Server" 
        ContentPath="<%# Controller.Path %>" 
        ContentPathExtra="/settings"
        Handler="AdminHandler"
        Visible="true"
        >Basic Settings</mbl:HyperLink>
    <mbl:HyperLink 
        runat="Server" 
        ContentPath="<%# Controller.Path %>" 
        ContentPathExtra="/settings/redirects"
        Handler="AdminHandler"
        Visible="true"
        >URL Mappings</mbl:HyperLink>
    <mbl:HyperLink 
        runat="Server" 
        ContentPath="<%# Controller.Path %>" 
        ContentPathExtra="/settings/rss"
        Handler="AdminHandler"
        >RSS Setup</mbl:HyperLink>
    <mbl:HyperLink CssClass="Clean"
        runat="Server" 
        ContentPath="<%# Controller.Path %>" 
        ContentPathExtra="/settings/permissions"
        Handler="AdminHandler"
        >Permissions</mbl:HyperLink>
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
<div id="PostForm">
<form runat="server">
    <asp:PlaceHolder id="Basic" runat="server" visible='<%# (Url.GetPathItem(1,"settings") == "settings") %>'>
        <h2 class="FormTitle">"<%# Controller.FileName %>" Settings</h2>
        <div class="Expando Expanded">
            <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Behavior</h2>
            <div class="OptionsContainer">
                <p><label><strong>Title:</strong></label><br />
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="TextBox"></asp:TextBox>
                </p>
                <p><label><strong>File Name:</strong></label><br />
                    <asp:TextBox ID="txtFileName" runat="server" CssClass="TextBox"></asp:TextBox>
                </p>
                <admin:ModulePicker runat="server" ID="modulePicker" />
            </div>
        </div>
        <div class="Expando Expanded">
            <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Template</h2>
            <div class="OptionsContainer">
            <p><label><strong>Template Set:</strong></label><br />
            <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="lstTemplates_Changed" ID="lstTemplates" runat="server"><asp:ListItem>Default</asp:ListItem><asp:ListItem>Audioholics</asp:ListItem></asp:DropDownList>
            </p>
            <p><label><strong>Template:</strong></label><br />
            <asp:DropDownList runat="server" ID="lstTemplateControls"><asp:ListItem>Main</asp:ListItem></asp:DropDownList>
            </p>
            </div>        
        </div>
        <div class="ButtonBarRight">
            <asp:LinkButton CssClass="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save"></asp:LinkButton>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder id="Redirects" runat="server" visible='<%# (Url.GetPathItem(1) == "redirects") %>'>
        <h2 class="FormTitle">Redirects to "<%# Controller.FileName %>"</h2>
        <div id="PageSelector" class="Expando Expanded">
            <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Behavior</h2>
            <table>
            <col class="Delete" />
            <col />
            <col />
                <thead>
                    <tr>
                        <th>&nbsp;</th>
                        <th>Original Url</th>
                        <th>Resulting content</th>
                    </tr>
                </thead>
            <asp:Repeater runat="server" ID="RedirectList" DataSource='<%# this.Controller.UrlRedirects %>'>
                <ItemTemplate>
                   <tr>
                        <td>
                        <asp:ImageButton
                            runat="server"
                            OnClick="RedirectDelete_Click" 
                            ImageUrl='<%# AdminTemplateBase + "images/icon_trashcan.gif" %>' 
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Url", "Remove \"{0}\"")%>' />
                        </td>                      
                        <td class="FlowRight">
                            <%# DataBinder.Eval(Container.DataItem, "Url") %>                               
                        </td>
                        <td>
                            <mbl:HyperLink 
                                runat="Server" 
                                ContentPath="<%# Controller.Path %>" 
                                ContentPathExtra='<%# Eval("PathExtra") %>'
                                Handler='<%# Eval("Handler") %>'
                                Visible="true"
                                ><%# this.Url.ToString(Eval("Handler") as string, Eval("PathExtra") as string) %>
                                </mbl:HyperLink>                             
                        </td>                     
                   </tr>
                </ItemTemplate>
            </asp:Repeater>
                <tfoot>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:TextBox runat="server" ID="UrlRedirect_Url"></asp:TextBox></td>
                        <td><%# this.Controller.Path %><mbl:HandlerDropDownList runat="server" ID="UrlRedirect_Handler" />/<asp:TextBox runat="Server" ID="UrlRedirect_PathExtra"></asp:TextBox>
                        &nbsp;<asp:LinkButton CssClass="SaveButton" runat="server" OnClick="RedirectSaveButton_Click" Text="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="Permissions" runat="server" Visible='<%# (Url.GetPathItem(1,"settings") == "permissions") %>'>
        <select class="permissions-group-selector">
            <asp:Repeater runat="server" ID="lstGroups">
                <ItemTemplate>
                <option value='<%# Container.DataItem %>' runat="server"><%# Container.DataItem %></option>
                </ItemTemplate>
            </asp:Repeater>
        </select>        
        <ul>
            <asp:Repeater runat="server" ID="lstPermissionsOptions">
                <ItemTemplate>
                    <li>
                        <label><input id="flag" type="checkbox" class="permissions-checkbox" value='<%# Eval("Key") %>' runat='server' />
                        <%# Eval("Value") %>
                        </label>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" Visible='<%# (Url.GetPathItem(1,"settings") == "rss") %>'>
        <h2 class="FormTitle">"<%# Controller.FileName %>" RSS Settings</h2>
        <div class="Expando Expanded">
            <h2 onclick="toggleParent(this, 'Expando', 'Expando Expanded');return false;">Feed Properties</h2>
            <div class="OptionsContainer">
                <p><label><strong>Title:</strong></label><br />
                    <asp:TextBox ID="txtRssTitle" runat="server" CssClass="TextBox"></asp:TextBox>
                </p>
                <p><label><strong>Content URL:</strong></label><br />
                    <asp:TextBox ID="txtRssLink" runat="server" CssClass="TextBox"></asp:TextBox>
                </p>
                <p><label><strong>Description:</strong></label><br />
                    <asp:TextBox ID="txtRssDescription" runat="server" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
                </p>
                <p><label><strong>Managing Editor:</strong></label><br />
                    <asp:TextBox ID="txtRssManagingEditor" runat="server" CssClass="TextBox"></asp:TextBox>
                </p>
                <p><label><strong>Item Template:</strong></label><br />
                    <asp:TextBox ID="txtRssItemTemplate" runat="server" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
                </p>
                <p><label><strong>Feed Redirect Url:</strong></label><br />
                    <asp:TextBox ID="txtRssRedirectUrl" runat="server" CssClass="TextBox"></asp:TextBox>
                </p>
                <p><label><strong>Redirect Exceptions:</strong></label><br />
                    <asp:TextBox ID="txtRssRedirectExceptions" runat="server" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
                </p>               
            </div>
        </div>
        <div class="ButtonBarRight">
            <asp:LinkButton CssClass="SaveButton" runat="server" OnClick="RssSaveButton_Click" Text="Save"></asp:LinkButton>
        </div>
    </asp:PlaceHolder>    
</form>
</div>
</asp:Content>