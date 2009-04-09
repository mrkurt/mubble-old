<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Application.aspx.cs" Inherits="Admin_Metrics_Application" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Application Metrics</title>
    <script type="text/javascript" src="<%= ResolveUrl("~/Templates/shared/jscripts/jquery.js") %>"></script>
    <script type="text/javascript">
        var Queries = {
            Setup : function(){
                $('td.QueryText a').click(Queries.Click);   
            },
            Click : function(e){
                $(this).next('div').slideToggle();
                return false;
            }
        }; 
        $(Queries.Setup);
    </script>
    <style type="text/css">
        body{
            font-size: 10pt;
            font-family: Verdana, Tahoma, arial, sans-serif;
        }
        table{
            border: 1px solid #cccccc;
            border-collapse: collapse;
            margin-bottom: 10px;
            width: 400px;
            float: left;
            margin-right: 10px;
        }
        table.DoubleWide{
            width: 99%;
            margin-right: 0px;
        }
        table th, td{
            border: 1px solid #cccccc;
            padding: 3px;
            vertical-align: top;
        }
        table thead th{
            background-color: #999999;
            color: white;
        }
        table tbody th{
            background-color: #eeeeee;
            text-align: left;
            width: 200px;
        }
        table td.QueryText{
            width: 50%;
        }
        table td.QueryText div{
            font-family: Courier New;
            border: 1px solid #dddddd;
            background-color: #eeeeee;
            margin: 3px;
            padding: 3px;
            display: none;
        }
    </style>
</head>
<body>
    <table>
        <thead>
            <tr><th colspan="2">Lucene Numbers</th></tr>
            <tr><th>Counter</th><th>Value</th></tr>
        </thead>
        <tbody>
            <%
                Mubble.Models.QueryEngine.Engines.LuceneMetrics lucene = Mubble.Models.QueryEngine.Engines.Lucene.Metrics;
            %>
            <tr><th>Total Searchers</th><td><%= lucene.TotalSearchersOpened %></td></tr>
            <tr><th>Current Searchers</th><td><%= lucene.CurrentSearchersOpened %></td></tr>
            <tr><th>Total Searches Run</th><td><%= lucene.SearchesRun %></td></tr>
        </tbody>
    </table>

    <table>
       <thead>
            <tr><th colspan="2">Worker Pool Numbers</th></tr>
            <tr><th>Counter</th><th>Value</th></tr>
        </thead>
        <tbody>
            <tr><th>Items Queued</th><td><%= Mubble.Worker.QueueLength %></td></tr>
            <tr><th>Total Threads</th><td><%= Mubble.Worker.TotalThreads %></td></tr>
            <tr><th>Max Threads</th><td><%= Mubble.Worker.MaxThreads %></td></tr>
        </tbody>
    </table>
            
    <table>
        <thead>
            <tr><th colspan="2">Cache</th></tr>
            <tr><th>Setting</th><th>Value</th></tr>
        </thead>
        <tbody>
            <%
                Mubble.Config.Caching cache = Mubble.Config.Caching.Current;
            %>
            <tr>
                <tr><th>Shared Object Cache Time</th><td><%= cache.GetSlidingExpiration() %></td></tr>
                <tr><th>Output Caching</th><td><%= cache.EnableOutputCaching %></td></tr>
                <tr><th>Static Cache Host</th><td><%= cache.StaticHost %></td></tr>
                <tr><th>Effective Byte Limit</th><td><%= (Cache.EffectivePrivateBytesLimit / 1024).ToString("#,###") %>KB</td></tr>
                <tr><th>Total Items</th><td><%= Cache.Count %></td></tr>
            </tr>
        </tbody>
    </table>
    
    <table>
        <thead>
            <tr><th colspan="4">Index Duplicates</th></tr>
            <tr><th>ID</th><th>Title</th></tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="Duplicates">
                <ItemTemplate>
                <tr>
                    <td class="QueryText"><%# Eval("Key") %></td>
                    <td><%# Eval("Value") %></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</body>
</html>
