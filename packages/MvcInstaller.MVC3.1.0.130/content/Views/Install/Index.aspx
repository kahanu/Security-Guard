<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcInstaller.Settings.InstallerConfig>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
.installer-label
{
    float: left; width: 180px; font-weight: bold;
}

.fieldwrapper { margin-bottom: 0.5em; clear: both; }
.fieldwrapperindented { margin-bottom: 0.5em; clear: both; margin-left: 20px; }
</style>
    <h2>
        MVC Site Installer</h2>
    <p>For more information on this installer, please visit <a href="http://mvcinstaller.codeplex.com" target="_blank">http://mvcinstaller.codeplex.com</a>.</p>
    <p>The data below is the information from your installer.config file that will be used to install your application.</p>
    <p style="color:Red;"><b>Important!</b> Make sure this path on the server has <b>write permissions</b>, at least for the installation process.  You can disable it afterward.</p>
    <fieldset>
        <legend>Application</legend>
        <div class="fieldwrapper">
            <div class="installer-label">Name:</div>
            <div class="display-field"><%= Model.ApplicationName %></div>
        </div>
    </fieldset>

    <fieldset>
        <legend>Database settings</legend>
        <p>The database and db user needs to be created prior to running the installer!</p>
        
        <div class="fieldwrapper">
            <div class="installer-label">
                Database:</div>
            <div class="display-field">
                <%= Model.Database.InitialCatalog %></div>
        </div>
        <div class="fieldwrapper">
            <div class="installer-label">
                Data Source:</div>
            <div class="display-field">
                <%= Model.Database.DataSource %></div>
        </div>
        <% if (Model.Database.UseTrustedConnection)
           { %>
        <div class="fieldwrapper">
            <div class="installer-label">Trusted Connection:</div>
            <div class="display-field">True</div>
        </div>
        <%}
           else
           { %>
        <div class="fieldwrapper">
            <div class="installer-label">
                Username:</div>
            <div class="display-field">
                <%= Model.Database.UserName%></div>
        </div>
        <%} %>
    </fieldset>
    <fieldset>
        <legend>Membership settings</legend>
        <% if (Model.Membership.Create)
           { %>
        <p>These are the Roles and Users to be created by the installer.</p>
        <% foreach (var role in Model.RoleManager.Roles)
           {%>
        <div class="fieldwrapper">
            <div class="installer-label">
                Role:</div>
            <div class="display-field">
                <%= role.Name%></div>
        </div>
        <% foreach (var user in role.Users)
           {%>
        <div class="fieldwrapperindented">
            <div class="installer-label">
                User in role:</div>
            <div class="display-field">
                <%= user.UserName%></div>
        </div>
        <%} %>
        <%} %>
        <%}
           else
           { %>
           <p>The Asp.Net Membership system will not be created.</p>
        <%} %>
    </fieldset>
    <div id="response"></div>
    <button id="runprocess">Install</button>
    <span id="loader" style="display: none;">
        <img src="/MvcInstaller/ajax-loader.gif" alt="Processing... please wait!" />&nbsp;Processing... please wait!
    </span>

    <script type="text/javascript">

        $(function() {
            var loader = $("#loader");
            var button = $("#runprocess");
            var resp = $("#response");

            $("#runprocess").live("click", function() {
                button.hide();
                loader.show();

                $.ajax(
                {
                    url: '<%= Url.Action("Run", "Install") %>',
                    datatype: 'json',
                    type: 'POST',
                    success: OnSuccess,
                    error: OnError
                });
            });

            function OnSuccess(data) {
                loader.hide();
                var msg = data.Message;
                if (msg == "The installer completed successfully!") {
                    msg += " <a href=\"/Account/LogOn\">Log On</a>";
                } else {
                    button.show();
                }
                resp.html(msg);
            }

            function OnError(data) {
                loader.hide();
                resp.html(data.Message);
                button.show();
            }
        });
    
    </script>

</asp:Content>
