<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" MasterPageFile="~/Pages/MasterPage/Master.master" Inherits="ECommerce__Project.Pages.ForgetPassword.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container mt-5">
    <h2 class="text-center">Password Recovery</h2>
    <table class="table">
        <tr>
            <td class="col-sm-3"><label for="txtEmail">Enter Your Email:</label></td>
            <td class="col-sm-9">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator 
                    ID="rfvEmail" 
                    runat="server" 
                    ControlToValidate="txtEmail" 
                    ErrorMessage="Email is required." 
                    CssClass="text-danger" 
                    Display="Dynamic" 
                    ValidationGroup="PasswordRecovery" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="btnSendPassword" runat="server" Text="Send Password" CssClass="btn btn-primary" OnClick="btnSendPassword_Click" ValidationGroup="PasswordRecovery" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Label ID="lblResult" runat="server" CssClass="text-success"></asp:Label>
            </td>
        </tr>
    </table>
</div>

    </asp:Content>
