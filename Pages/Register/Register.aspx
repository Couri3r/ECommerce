<%@ Page Title="Home Page - E-Commerce" Language="C#" MasterPageFile="~/Pages/MasterPage/Master.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Ecomm_project.Register" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Register Page - E-Commerce
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-8">
                    <div class="card">
                        <div class="card-header text-center">
                            <h2>Registration</h2>
                        </div>
                        <div class="card-body">
                            <h4 class="text-center mb-4">Username</h4>
                            <div class="form-group">
                                <label for="uname">Username</label>
                                <asp:TextBox ID="uname" runat="server" class="form-control" placeholder="Username"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Reqname" runat="server" ErrorMessage="Username Required" ForeColor="DeepPink" ControlToValidate="uname" />
                            </div>                       
                            <h4 class="text-center mb-4">Password</h4>
                            <div class="form-group">
                                <label for="pass">Password</label>
                                <asp:TextBox ID="pass" runat="server" TextMode="Password" class="form-control" placeholder="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Reqpass" runat="server" ErrorMessage="Password Required" ForeColor="DeepPink" ControlToValidate="pass" />
                                <asp:RegularExpressionValidator ID="Regpass" runat="server" ErrorMessage="Enter at least 8 characters, one uppercase, one lowercase, and one special character" ForeColor="Brown" ControlToValidate="pass" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$" />
                            </div>
                            <div class="form-group">
                                <label for="confpass">Confirm Password</label>
                                <asp:TextBox ID="confpass" runat="server" TextMode="Password" class="form-control" placeholder="Confirm Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqconfpass" runat="server" ErrorMessage="Re-type Password Required" ForeColor="DeepPink" ControlToValidate="confpass" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Both passwords must be the same" ForeColor="Blue" ControlToCompare="pass" ControlToValidate="confpass" />
                            </div>
                            <div class="form-group">
    <h4 class="text-center mb-4">Email</h4>
    <label for="email">Email Address</label>
    <asp:TextBox ID="email" runat="server" class="form-control" placeholder="Email Address"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ReqEmail" runat="server" ErrorMessage="Email Required" ForeColor="DeepPink" ControlToValidate="email" />
    <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ErrorMessage="Invalid Email Format" ForeColor="Brown" ControlToValidate="email" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
</div>
                            <div class="form-group text-center">
                                <asp:Button ID="btn" runat="server" Text="Register" class="btn btn-primary" OnClick="btn_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



</asp:Content>
