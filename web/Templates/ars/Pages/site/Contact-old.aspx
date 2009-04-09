<%@ Page ContentType="text/html" Strict="true" Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Arstechnica.Custom.Contact" MasterPageFile="../../Default.master" %>
<%@ OutputCache VaryByParam="none" VaryByCustom="($groups)" Duration="600" %>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="Content Page Site <%# Controller.FileName %>">
        <div class="ContentHeader"><span class="Replace"><data:Field runat="server" Name="Title" /></span></div>
        <div class="ContentBody">
            <p>Welcome to the Ars Technica "Contact Us" form. Use this form to send us news tips, corrections, media inquiries, and more. </p>
            <div class="Contact">
            <form runat="server" id="ContactForm">
            <asp:Literal runat="server" ID="ErrorMessage" Visible="false">
                <p class="ErrorMessage">Please be sure to fill in all required fields.</p>
            </asp:Literal>
            <p>Select an option:
            <span class="Error"> *</span><asp:RequiredFieldValidator EnableClientScript="false" runat="server" ControlToValidate="Destination" Display="None"> *</asp:RequiredFieldValidator>
            <br />
            <asp:DropDownList runat="server" ID="Destination">
                <asp:ListItem Value="" Selected="true"></asp:ListItem>
                <asp:ListItem Value="news">News tips</asp:ListItem>
                <asp:ListItem Value="press">Press/Media enquiries</asp:ListItem>
                <asp:ListItem Value="editors">Corrections</asp:ListItem>
                <asp:ListItem Value="comments">Comments/Other</asp:ListItem>
            </asp:DropDownList></p>
            <p>Your Name: <span class="Error">*</span><asp:RequiredFieldValidator EnableClientScript="false" runat="server" ControlToValidate="Name" Display="None"></asp:RequiredFieldValidator><br /><asp:TextBox runat="server" ID="Name" CssClass="Text" /></p>
            <p>Your Email: <span class="Error">*</span><asp:RequiredFieldValidator EnableClientScript="false" runat="server" ControlToValidate="Email" Display="None"> *</asp:RequiredFieldValidator><br /><asp:TextBox runat="server" ID="Email" CssClass="Text" /></p>
            <p>Subject: <br /><asp:TextBox runat="server" ID="Subject" CssClass="Text" /></p>
            <p>Related URL:<br /><asp:TextBox runat="server" ID="Url" CssClass="Text" /></p>
            <p>Message: <span class="Error">*</span><asp:RequiredFieldValidator EnableClientScript="false" runat="server" ControlToValidate="Message" Display="None"> *</asp:RequiredFieldValidator><br /><asp:TextBox runat="server" ID="Message" TextMode="MultiLine" /></p>
            <p><asp:Button runat="server" Text="Send" OnClick="SendMessage_Click" /></p>
            </form>
            <asp:Literal runat="server" ID="Confirmation" Visible='false'>
                <h4>Thanks for contacting Ars Technica.  We will get back to you shortly.</h4>
            </asp:Literal>
            </div>
            <p>Please note:</p>
                <ul>
                <li>If you are looking for Subscription support, visit <a href="subscriber-faq.ars">our subscriber support page</a> for more information</li>
                <li>Information on contacting our moderation team <a href="contact-mods.ars">is available here</a>. Please note that we receive many moderation requests,
				often stemming from the same incident or post. We do not always reply to reports, but we do investigate all of them.</li>
                </ul>
				<p>Thanks for contacting Ars Technica!</p>
        </div>
        <div class="ContentFooter"></div>
    </div>
</asp:Content>