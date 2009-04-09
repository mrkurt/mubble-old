<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cache.aspx.cs" Inherits="Admin_Metrics_Cache" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <div>
        <% System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache; %>   
        <p>Total Items: <%= cache.Count %></p>
        <asp:Repeater ID="CacheItems" runat="server">
            <ItemTemplate>
                <li><%# Eval("Key") %> - <%# Eval("Value") %></li>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</body>
</html>
