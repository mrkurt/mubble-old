<%@ Page ContentType="text/html" Language="C#" AutoEventWireup="true" CodeFile="Subscribe.aspx.cs" Inherits="Arstechnica.Custom.Subscribe" %>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="Content Page Site <%# Controller.FileName %>">
        <div class="ContentHeader"><span class="Replace"><data:Field runat="server" Name="Title" /></span></div>
        <div class="ContentBody">
        <data:Pages runat="server" ID="Pages">
            <Filters>
                <data:PageNumber ParameterName="PageNumber" />
            </Filters>
            <PageTemplate>
                <h3><data:Field Name="Name" runat="server" /></h3>
                <data:Field Name="Body" runat="server" />
            </PageTemplate>
        </data:Pages>
        </div>
        <div class="ContentFooter"></div>
    </div>
</asp:Content>
