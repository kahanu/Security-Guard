@model $rootnamespace$.Areas.SecurityGuard.ViewModels.RegisterViewModel
@{
    ViewBag.Title = "Register";
}
<h2>
    Create a New Account</h2>
<p>
    Use the form below to create a new account.
</p>
<p>Password Requirements:</p>
<ul>
    <li>To be a minimum of @Membership.MinRequiredPasswordLength characters in length.</li>
    @if (Membership.MinRequiredNonAlphanumericCharacters > 0)
    {
        <li>To have a minimum of @Membership.MinRequiredNonAlphanumericCharacters non-alpha numeric characters included, such as &quot;<span style="font-family: Courier New; font-size: 16px;">(){}-_*&^%$#@@!</span>&quot;.</li>
    }
</ul>
<script src="@Url.Content("~/Content/SecurityGuard/scripts/jquery-1.6.1.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Content/SecurityGuard/scripts/modernizr-1.7.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Content/SecurityGuard/scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Content/SecurityGuard/scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
@using (Html.BeginForm((string)ViewBag.FormAction, "SGAccount"))
{
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(true)
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <div class="editor-label">
                @Html.LabelFor(m => m.UserName)
            </div>
            <div class="editor-field">
                @Html.TextBoxFor(m => m.UserName)
                @Html.ValidationMessageFor(m => m.UserName)
            </div>
            <div class="editor-label">
                @Html.LabelFor(m => m.Email)
            </div>
            <div class="editor-field">
                @Html.TextBoxFor(m => m.Email)
                @Html.ValidationMessageFor(m => m.Email)
            </div>
            <div class="editor-label">
                @Html.LabelFor(m => m.Password)
            </div>
            <div class="editor-field">
                @Html.PasswordFor(m => m.Password)
                @Html.ValidationMessageFor(m => m.Password)
            </div>
            <div class="editor-label">
                @Html.LabelFor(m => m.ConfirmPassword)
            </div>
            <div class="editor-field">
                @Html.PasswordFor(m => m.ConfirmPassword)
                @Html.ValidationMessageFor(m => m.ConfirmPassword)
            </div>
            @if (Model.RequireSecretQuestionAndAnswer)
            {
                <div class="editor-label">
                    @Html.LabelFor(model => model.SecretQuestion)
                </div>
                <div class="editor-field">
                    @Html.TextBoxFor(model => model.SecretQuestion)
                    @Html.ValidationMessageFor(model => model.SecretQuestion)
                </div>
                <div class="editor-label">
                    @Html.LabelFor(model => model.SecretAnswer)
                </div>
                <div class="editor-field">
                    @Html.TextBoxFor(model => model.SecretAnswer)
                    @Html.ValidationMessageFor(model => model.SecretAnswer)
                </div>
            }
            <p>
                <input type="submit" value="Register" />
            </p>
        </fieldset>
    </div>
}
