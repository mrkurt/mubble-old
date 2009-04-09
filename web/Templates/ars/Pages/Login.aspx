<%@ Page Language="C#" Title="Subscriber Login" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Arstechnica.Custom.Login" MasterPageFile="../Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="Content Page Site <%# Controller.FileName %>">
        <div class="ContentHeader"><span class="Replace"><data:Field runat="server" Name="Title" /></span></div>
        <div class="ContentBody">
            <div class="Contact">
            <form runat="server" class="Login">
                <div class="Login">
                <asp:Login 
                    runat="server" 
                    ID="LoginForm" 
                    OnAuthenticate="UserAuthenticate" 
                    VisibleWhenLoggedIn="false"
                    >                    
                </asp:Login>                
                <asp:LoginView runat="server">
                    <LoggedInTemplate>
                        <p>You are already logged in.</p>
                        <p><asp:LoginStatus runat="server" /></p>
                    </LoggedInTemplate>
                </asp:LoginView>
                </div>
            </form>
            </div>
        </div>
        <div class="ContentFooter"></div>
    </div>
</asp:Content>
