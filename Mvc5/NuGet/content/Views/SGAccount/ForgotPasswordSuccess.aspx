<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SecurityGuard.ViewModels.ForgotPasswordViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Forgot Password Success
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Password Reset Success</h2>

<p>Your new password has been emailed to you.</p>

<p><%: Html.ActionLink("Login", "Login", "SGAccount")%></p>
</asp:Content>
