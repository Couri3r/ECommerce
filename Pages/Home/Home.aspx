<%@ Page Title="Home Page - E-Commerce" Language="C#" MasterPageFile="~/Pages/MasterPage/Master.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Ecomm_project.Home" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page - E-Commerce
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <asp:Repeater ID="ProductRepeater" runat="server"  OnItemCommand="ProductRepeater_ItemCommand">
            <HeaderTemplate>
                <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="praoduct-card p-3 border rounded shadow">
                        <img class="products img-fluid border-bottom mb-3" src='<%# Eval("ImagePath") %>' alt='<%# Eval("Name") %>' />
                        <p class="product-name"><%# Eval("Name") %></p>
                        <strong><p class="price">$<%# Eval("Price", "{0:0.00}") %></p></strong>
                        <p class="description"><%# Eval("Description") %></p>            
<asp:Button 
    ID="ViewButton" 
    runat="server" 
    Text="View" 
    CommandName="View" 
    CommandArgument='<%# Eval("ProductId") %>' 
    CssClass="btn btn-primary" />
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
