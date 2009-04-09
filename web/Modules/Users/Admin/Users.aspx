<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Mubble.Web.Admin.Users"
MasterPageFile="~/Templates/Admin/Default.master" ValidateRequest="false"%>
<%@ Import Namespace="Mubble" %>
<asp:Content runat="Server" ContentPlaceHolderID="Content">
    <asp:PlaceHolder ID="UserDetails" runat="Server" Visible='<%# (Url.GetPathItem(0, "list") == "user") %>'>
        <form id="UserDetailsForm" runat="Server">
        <p><strong>Username:</strong><br />
            <asp:TextBox runat="server" ID="txtUserName" ReadOnly="true"></asp:TextBox>
        </p>
        <p>
            <strong>Full Name:</strong><br />
            <asp:TextBox runat="Server" ID="txtFullName"></asp:TextBox>
        </p>
        <p>
            <asp:Button runat="Server" OnClick="btnSaveUser_Click" Text="Save User" />
        </p>
        </form>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="UserList" runat="Server" Visible='<%# (Url.GetPathItem(0, "list") == "list") %>'>
        <table class="AdminListing">
            <tr>
                <th>User</th><th>Full Name</th>
            </tr>
            <asp:Repeater runat="Server" ID="AllUsers">
              <ItemTemplate>
              <tr><td>
              <mbl:HyperLink runat="Server" ContentPathExtra='<%# Eval("UserName", "/user/{0}") %>'
                        Handler="AdminHandler"
                        ToolTip="Edit this user"
                        >
                            <%# Eval("UserName") %>
                            </mbl:HyperLink>
              </td><td><%# Eval("UserName") %></td></tr>
              </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:PlaceHolder>
</asp:Content>