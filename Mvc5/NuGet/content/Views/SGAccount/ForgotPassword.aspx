<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SecurityGuard.ViewModels.ForgotPasswordViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Forgot Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Forgot Password</h2>

<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/jquery-1.6.1.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/modernizr-1.7.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary() %>
    <fieldset>
        <legend>Reset your password</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Email) %>
            <%: Html.ValidationMessageFor(model => model.Email) %>
        </div>

        <p>
            <%: Html.HiddenFor(model => model.RequireSecretQuestionAndAnswer) %>
            <input type="submit" value="Submit" />
        </p>
    </fieldset>
<% } %>

</asp:Content>

<script type="text/javascript">
    $("#Email").focus();
</script>
