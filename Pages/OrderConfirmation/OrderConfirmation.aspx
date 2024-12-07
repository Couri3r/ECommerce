<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" MasterPageFile="~/Pages/MasterPage/Master.master" Inherits="ECommerce__Project.Pages.OrderConfirmation.OrderConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-success">Order Confirmation</h2>

        <div class="card mb-4">
            <div class="card-header bg-primary text-white">
                Order Details
            </div>
            <div class="card-body">
                <asp:Repeater ID="rptOrderDetails" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Product</th>
                                    <th>Price</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ProductName") %></td>
                            <td>$<%# Eval("Price") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="mb-4">
            <h4 class="text-success">Total: $<asp:Label ID="lblTotalPrice" runat="server"></asp:Label></h4>
        </div>

        <div class="alert alert-success" role="alert">
            Thank you for your order! Your order has been placed successfully.
        </div>
        
      
    </div>
</asp:Content>
