<%@ Page Title="Login Page - E-Commerce" Language="C#" MasterPageFile="~/Pages/MasterPage/Master.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ECommerce__Project.Pages.Login.Login" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Login Page - E-Commerce
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
            <div class="card-header">
                Login
            </div>
            <div class="card-body">
                <div class="form-group">
                    <label for="username">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" class="form-control" placeholder="Username" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="password">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password" required="true"></asp:TextBox>
                </div>
                <asp:Button ID="loginbtn" runat="server" Text="Login" class="btn btn-primary" OnClick="btnLogin_Click" />
                
                <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="text-center mt-3">
            <p>Don't have an account? <a href="/Pages/Register/Register.aspx">Register here</a></p>
        </div>
        <div class="text-center mt-3">
    <p>Forgot your password? <a href="/Pages/ForgetPassword/ForgetPassword.aspx">Reset it here</a></p>
    </div>

</asp:Content>
