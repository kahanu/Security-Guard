<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SecurityGuard.ViewModels.ForgotPasswordViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Enter Secret Answer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>
    Enter Secret Answer</h2>

<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/jquery-1.6.1.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Content/SecurityGuard/scripts/modernizr-1.7.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm("ForgotPassword", "SGAccount", FormMethod.Post)) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary() %>

    <fieldset>
        <legend>Reset your password</legend>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email) %>
        </div>
        <div class="editor-field">
            <%: Model.Email %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.SecretQuestion) %>
        </div>
        <div class="editor-field">
            <%: Model.SecretQuestion %>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.SecretAnswer) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.SecretAnswer) %>
            <%: Html.ValidationMessageFor(model => model.SecretAnswer) %>
        </div>
        <p>
            <%: Html.HiddenFor(model => model.Checked) %>
            <%: Html.HiddenFor(model => model.Email) %>
            <%: Html.HiddenFor(model => model.RequireSecretQuestionAndAnswer) %>
            <%: Html.HiddenFor(model => model.SecretQuestion) %>
            <input type="submit" value="Submit" />
        </p>
    </fieldset>
<% } %>
</asp:Content>


<script type="text/javascript">
    $("#SecretAnswer").focus();
</script>