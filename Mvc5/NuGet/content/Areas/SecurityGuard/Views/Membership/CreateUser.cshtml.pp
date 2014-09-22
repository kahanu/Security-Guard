@model $rootnamespace$.Areas.SecurityGuard.ViewModels.RegisterViewModel

@{
    ViewBag.Title = "CreateUser";
    Layout = "~/Areas/SecurityGuard/Views/Shared/_SecurityGuardLayoutPage.cshtml";
}

<div id="breadcrumb">
    @Html.ActionLink("Dashboard", "Index", "Dashboard") > 
    @Html.ActionLink("Manage Users", "Index", "Membership") > 
    Create User
</div>

<script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>

<h2>Create User</h2>

<ul>
    <li>To be a minimum of @Membership.MinRequiredPasswordLength characters in length.</li>
    @if (Membership.MinRequiredNonAlphanumericCharacters > 0)
    {
        <li>To have a minimum of @Membership.MinRequiredNonAlphanumericCharacters non-alpha numeric characters included, such as &quot;<span style="font-family: Courier New; font-size: 16px;">(){}-_*&^%$#@@!</span>&quot;.</li>
    }
</ul>
@using (Html.BeginForm())
{
    @Html.ValidationSummary(true)
    <fieldset>
        <legend>Enter User Credentials</legend>

        <div class="row">
            <label>@Html.LabelFor(model => model.UserName)</label>
            <div class="inputs">
                <span class="input_wrapper">@Html.TextBoxFor(model => model.UserName, new { @class = "text" })</span>
                <span class="system negative" id="username-message">@Html.ValidationMessageFor(model => model.UserName)</span>
            </div>
        </div>

        <div class="row">
            <label>@Html.LabelFor(model => model.Email)</label>
            <div class="inputs">
                <span class="input_wrapper">@Html.TextBoxFor(model => model.Email, new { @class = "text" })</span>
                <span class="system negative">@Html.ValidationMessageFor(model => model.Email)</span>
            </div>
        </div>

        <div class="row">
            <label>@Html.LabelFor(model => model.Password)</label>
            <div class="inputs">
                <span class="input_wrapper">@Html.PasswordFor(model => model.Password, new { @class = "text" })</span>
                <span class="system negative">@Html.ValidationMessageFor(model => model.Password)</span>
            </div>
        </div>

        <div class="row">
            <label>@Html.LabelFor(model => model.ConfirmPassword)</label>
            <div class="inputs">
                <span class="input_wrapper">@Html.PasswordFor(model => model.ConfirmPassword, new { @class = "text" })</span>
                <span class="system negative">@Html.ValidationMessageFor(model => model.ConfirmPassword)</span>
            </div>
        </div>

        @if (Model.RequireSecretQuestionAndAnswer)
        {
        <div class="row">
            <label>@Html.LabelFor(model => model.SecretQuestion)</label>
            <div class="inputs">
                <span class="input_wrapper">@Html.TextBoxFor(model => model.SecretQuestion, new { @class = "text" })</span>
                <span class="system negative">@Html.ValidationMessageFor(model => model.SecretQuestion)</span>
            </div>
        </div>

        <div class="row">
            <label>@Html.LabelFor(model => model.SecretAnswer)</label>
            <div class="inputs">
                <span class="input_wrapper">@Html.TextBoxFor(model => model.SecretAnswer, new { @class = "text" })</span>
                <span class="system negative">@Html.ValidationMessageFor(model => model.SecretAnswer)</span>
            </div>
        </div>
        }

        <div class="row">
            <label>@Html.LabelFor(model => model.Approve)</label>
            <div class="inputs">
                <span class="input_wrapper">@Html.EditorFor(model => model.Approve, new { @class = "text" })</span>
                <span class="system">@Html.ValidationMessageFor(model => model.Approve)</span>
            </div>
        </div>


        @Html.HiddenFor(model => model.RequireSecretQuestionAndAnswer)
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
}

<div>
    @Html.ActionLink("Back to Manage Users", "Index")
</div>


<script type="text/javascript">

    $(function () {

        var msg = $("#username-message");
        var btn = $("input[type='submit']");

        $("#UserName").focus();

        $("#UserName").blur(function () {

            var username = $(this).val();

            if (username.length == 0) {
                alert("No username.");
                return;
            }

            $.ajax({
                url: '@Url.Action("CheckForUniqueUser", "Membership")',
                dataType: 'json',
                type: 'GET',
                data: { userName: username },
                success: OnCheckForUniqueUserSuccess,
                error: OnCheckForUniqueUserError
            });
        });

        function OnCheckForUniqueUserSuccess(data) {
            if (data.Exists) {
                msg.text("This username already exists.  Please enter a new one.");
                btn.attr("disabled", "disabled");
            } else {
                msg.text("");
                btn.removeAttr("disabled");
            }
        }

        function OnCheckForUniqueUserError(xhr, status, error) {
            msg.text("There was an error checking uniqueness.");
        }
    });

</script>