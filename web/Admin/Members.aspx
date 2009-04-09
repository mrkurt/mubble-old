<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="Admin_Members" %>
<%@ Register Assembly="QualityData.Membership" Namespace="QualityData.Web.UI.WebControls" TagPrefix="members" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <members:MembershipManager runat="server" MembershipRole="Administrators" />
        </div>
    </form>
</body>
</html>
