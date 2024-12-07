<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/MasterPage/Master.master" CodeBehind="Checkout.aspx.cs" Inherits="ECommerce__Project.Pages.Checkout.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Checkout</h2>
        
        <!-- Order Summary -->
        <div class="card mb-4">
            <div class="card-header bg-primary text-white">
                Order Summary
            </div>
            <div class="card-body">
                <asp:Repeater ID="rptOrderSummary" runat="server">
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
                            <td>$<%# Eval("SubTotal") %></td>
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
            <h4 class="text-success">Total: $<asp:Label ID="lblTotal" runat="server"></asp:Label></h4>
        </div>

        <div class="card">
            <div class="card-header bg-secondary text-white">
                Payment Method
            </div>
            <div class="card-body">
                <asp:RadioButtonList ID="rblPaymentMethod" runat="server" CssClass="form-check">
                    <asp:ListItem Text="Credit Card" Value="CreditCard" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="PayPal" Value="PayPal"></asp:ListItem>
                </asp:RadioButtonList>

                <div id="creditCardForm" class="mt-4">
                    <h5>Credit Card Details</h5>
                    <div class="form-group">
                        <label for="txtCardNumber">Card Number</label>
                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control" MaxLength="16" placeholder="1234 5678 9012 3456"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtExpiryDate">Expiry Date</label>
                        <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" MaxLength="5" placeholder="MM/YY"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtCVV">CVV</label>
                        <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" MaxLength="3" placeholder="123"></asp:TextBox>
                    </div>
                </div>

                
            </div>
        </div>

        <div class="mt-4">
            <asp:Button ID="btnPlaceOrder" runat="server" CssClass="btn btn-success" Text="Place Order" OnClick="btnPlaceOrder_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const rblPaymentMethod = document.getElementById('<%= rblPaymentMethod.ClientID %>');
            const creditCardForm = document.getElementById('creditCardForm');
            const paypalMessage = document.getElementById('paypalMessage');

            rblPaymentMethod.addEventListener("change", function () {
                const selectedValue = rblPaymentMethod.querySelector("input:checked").value;

                if (selectedValue === "CreditCard") {
                    creditCardForm.style.display = "block";
                    paypalMessage.style.display = "none";
                } else if (selectedValue === "PayPal") {
                    creditCardForm.style.display = "none";
                    paypalMessage.style.display = "block";
                }
            });
        });
    </script>
</asp:Content>
