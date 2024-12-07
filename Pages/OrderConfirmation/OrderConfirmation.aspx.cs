using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ECommerce__Project.Pages.OrderConfirmation
{
    public partial class OrderConfirmation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderDetails();
            }
        }

        private void LoadOrderDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;
            string userId = Session["UserId"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("~/Pages/Login/Login.aspx");
                return;
            }

            // Fetch order details for the current user
            List<object> orderDetails = new List<object>();
            decimal totalPrice = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
            SELECT p.labelP AS ProductName, 
                   p.priceP AS Price
            FROM [Order] o
            JOIN product p ON o.idP = p.idP
            WHERE o.idU = @UserId
            ORDER BY o.idO DESC"; 

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderDetails.Add(new
                            {
                                ProductName = reader["ProductName"],
                                Price = reader["Price"]
                            });

                            totalPrice += Convert.ToDecimal(reader["Price"]);
                        }
                    }
                }
            }

            rptOrderDetails.DataSource = orderDetails;
            rptOrderDetails.DataBind();

            lblTotalPrice.Text = totalPrice.ToString("0.00");
        }
    }
}