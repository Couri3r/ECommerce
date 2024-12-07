using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerce__Project.Pages.ProductDetails
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productId = Request.QueryString["ProductId"];
                if (!string.IsNullOrEmpty(productId))
                {
                    LoadProductDetails(productId);
                }
                else
                {
                    Response.Write("<p>Invalid product ID.</p>");
                }
            }
        }

        private void LoadProductDetails(string productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT labelP AS Name, desP AS Description, priceP AS Price, photopath AS ImagePath FROM product WHERE idP = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Bind the product details to controls on the page
                            lblProductName.Text = reader["Name"].ToString();
                            imgProduct.ImageUrl = reader["ImagePath"].ToString();
                            lblDescription.Text = reader["Description"].ToString();
                            lblPrice.Text = $"${Convert.ToDecimal(reader["Price"]):0.00}";
                        }
                        else
                        {
                            Response.Write("<p>Product not found.</p>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<p>Error: {ex.Message}</p>");
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"];
            if (!string.IsNullOrEmpty(productId) && Session["UserId"] != null)
            {
                AddToCart(Convert.ToInt32(Session["UserId"]), productId);
            }
            else
            {
                Response.Redirect("~/Pages/Login/Login.aspx");
            }
        }

        private void AddToCart(int userId, string productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO shopcart (idu, idp) VALUES (@UserId, @ProductId)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        cmd.ExecuteNonQuery();
                        Response.Write("<p>Product added to cart successfully.</p>");
                    }
                }

                catch (Exception ex)
                {
                    Response.Write($"<p>Error: {ex.Message}</p>");
                }
            }
        }

    }
}