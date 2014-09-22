<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SecurityGuard.ViewModels.ChangePasswordViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Change Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>
    Change Password</h2>
<p>
    Use the form below to change your password.
</p>
<p>Password Requirements:</p>
<ul>
    <li>To be a minimum of @Membership.MinRequiredPasswordLength characters in length.</li>
    <% if (Membership.MinRequiredNonAlphanumericCharacters > 0)
    {%>
        <li>To have a minimum of @Membership.MinRequiredNonAlphanumericCharacters non-alpha numeric characters included, such as &quot;<span style="font-family: Courier New; font-size: 16px;">(){}-_*&^%$#@@!</span>&quot;.</li>
    <%}%>
</ul>

<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/jquery-1.6.1.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/modernizr-1.7.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Account Information</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.OldPassword) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.OldPassword) %>
            <%: Html.ValidationMessageFor(model => model.OldPassword) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.NewPassword) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.NewPassword) %>
            <%: Html.ValidationMessageFor(model => model.NewPassword) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.ConfirmPassword) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ConfirmPassword) %>
            <%: Html.ValidationMessageFor(model => model.ConfirmPassword) %>
        </div>

        <p>
            <input type="submit" value="Change Password" />
        </p>
    </fieldset>
<% } %>


</asp:Content>
