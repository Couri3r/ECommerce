<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/MasterPage/Master.master" CodeBehind="shopCart.aspx.cs" Inherits="ECommerce__Project.Pages.shopCart.shopCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Your Shopping Cart</h2>
        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <img src='<%# Eval("ImagePath") %>' alt="Product Image" style="width: 100px; height: auto;" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />

                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <div class="mt-3">
            <asp:Label ID="lblTotal" runat="server" CssClass="h4 text-success"></asp:Label>
        </div>
        <asp:Button ID="btnCheckout" runat="server" CssClass="btn btn-primary mt-3" Text="Checkout" OnClick="btnCheckout_Click" />
        <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3" Visible="False"></asp:Label>
    </div>
</asp:Content>

