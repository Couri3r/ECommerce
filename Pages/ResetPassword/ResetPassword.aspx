<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/MasterPage/Master.master" CodeBehind="ResetPassword.aspx.cs" Inherits="ECommerce__Project.Pages.ResetPassword.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container mt-5">
    <h2 class="text-center">Reset Password</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3"></asp:Label>
    <div class="form-group">
        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Placeholder="New Password" CssClass="form-control" />
    </div>
    <asp:Button ID="btnReset" runat="server" Text="Reset Password" OnClick="btnReset_Click" CssClass="btn btn-primary btn-block" />
</div>
        </asp:Content>
