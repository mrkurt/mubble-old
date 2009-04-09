<%@ Page Language="VB" Inherits="Mubble.UI.Page" Explicit="False" Title="<%# Controller.Title %>"  %>
<%@ Import Namespace="Mubble.Tools.Http" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <h3><%#Controller.Title%></h3>
    <form runat="server">
        <asp:LoginView runat="server">
            <AnonymousTemplate>
                <asp:Login Class="Login" DisplayRememberMe="true" RememberMeSet="true" runat="server" DestinationPageUrl='<%# Request.QueryString("origin") %>'>
                </asp:Login>
                <html:HyperLink runat="server" Path="/register">Register Here</html:HyperLink>
                <asp:PasswordRecovery CssClass="Login" runat="server"></asp:PasswordRecovery>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <p>You are already logged in as "<asp:LoginName runat="server" />".</p>
                <p><asp:LoginStatus runat="server" /></p>
                <p><asp:ChangePassword runat="server"></asp:ChangePassword></p>
            </LoggedInTemplate>
        </asp:LoginView>
    </form>
</asp:Content>    