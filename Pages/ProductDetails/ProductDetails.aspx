<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" MasterPageFile="~/Pages/MasterPage/Master.master" Inherits="ECommerce__Project.Pages.ProductDetails.ProductDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="container mt-5">
            <div class="card shadow-lg">
                <div class="row g-0">
                    <!-- Product Image -->
                    <div class="col-md-6">
                        <asp:Image id="imgProduct" runat="server" CssClass="img-fluid rounded-start" alt="Product Image" />
                    </div>
                    <!-- Product Details -->
                    <div class="col-md-6">
                        <div class="card-body">
                            <asp:Label id="lblProductName" runat="server" CssClass="card-title h3"></asp:Label>
                            <asp:Label id="lblDescription" runat="server" CssClass="card-text text-muted"></asp:Label>
                            <br />
                            <asp:Label id="lblPrice" runat="server" CssClass="text-success h4"></asp:Label>
    <div class="mt-3 d-flex">
    <!-- Add to Cart button -->
<asp:Button ID="btnAddToCart" runat="server" CssClass="btn btn-primary me-3" Text="Add to Cart" OnClick="btnAddToCart_Click" />
    
</div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Content>

