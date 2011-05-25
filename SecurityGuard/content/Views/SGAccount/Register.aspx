<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SecurityGuard.ViewModels.RegisterViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>
    Create a New Account</h2>
<p>
    Use the form below to create a new account.
</p>
<p>
    Passwords are required to be a minimum of <%:Membership.MinRequiredPasswordLength %>
    characters in length.
</p>

<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/jquery-1.5.1.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/modernizr-1.7.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>RegisterViewModel</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.UserName) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.UserName) %>
            <%: Html.ValidationMessageFor(model => model.UserName) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Email) %>
            <%: Html.ValidationMessageFor(model => model.Email) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Password) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Password) %>
            <%: Html.ValidationMessageFor(model => model.Password) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.ConfirmPassword) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ConfirmPassword) %>
            <%: Html.ValidationMessageFor(model => model.ConfirmPassword) %>
        </div>

        <% if (Model.RequireSecretQuestionAndAnswer)
           {%>
            <div class="editor-label">
                <%: Html.LabelFor(model => model.SecretQuestion) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.SecretQuestion) %>
                <%: Html.ValidationMessageFor(model => model.SecretQuestion) %>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.SecretAnswer) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.SecretAnswer) %>
                <%: Html.ValidationMessageFor(model => model.SecretAnswer) %>
            </div>
           <%} %>

        <p>
            <input type="submit" value="Register" />
        </p>
    </fieldset>
<% } %>

</asp:Content>
