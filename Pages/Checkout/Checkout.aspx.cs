using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ECommerce__Project.Pages.Checkout
{
    public partial class Checkout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderSummary();
            }
        }

        private void LoadOrderSummary()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;
            string userId = Session["UserId"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("~/Pages/Login/Login.aspx");
                return;
            }

            List<object> cartItems = new List<object>();
            decimal total = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
    SELECT p.labelP AS ProductName, 
           p.priceP AS Price,
           p.priceP AS SubTotal
    FROM shopcart sc
    JOIN product p ON sc.idp = p.idP
    WHERE sc.idu = @UserId";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cartItems.Add(new
                            {
                                ProductName = reader["ProductName"],
                                SubTotal = reader["SubTotal"]
                            });

                            total += Convert.ToDecimal(reader["SubTotal"]);
                        }
                    }
                }
            }

            rptOrderSummary.DataSource = cartItems;
            rptOrderSummary.DataBind();

            lblTotal.Text = total.ToString("0.00");
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            string paymentMethod = rblPaymentMethod.SelectedValue;
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;
            string userId = Session["UserId"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("~/Pages/Login/Login.aspx");
                return;
            }

            if (paymentMethod == "CreditCard")
            {
                string cardNumber = txtCardNumber.Text;
                string expiryDate = txtExpiryDate.Text;
                string cvv = txtCVV.Text;

                if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(cvv))
                {
                    Response.Write("<p class='text-danger'>Please fill in all credit card details.</p>");
                    return;
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string selectCartItemsQuery = @"
            SELECT sc.idP, p.priceP 
            FROM shopcart sc
            JOIN product p ON sc.idP = p.idP
            WHERE sc.idU = @UserId";

                List<Tuple<int, decimal>> cartItems = new List<Tuple<int, decimal>>();
                decimal totalPrice = 0;

                using (SqlCommand cmd = new SqlCommand(selectCartItemsQuery, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productId = Convert.ToInt32(reader["idP"]);
                            decimal price = Convert.ToDecimal(reader["priceP"]);
                            cartItems.Add(new Tuple<int, decimal>(productId, price));
                            totalPrice += price;
                        }
                    }
                }

                foreach (var item in cartItems)
                {
                    string insertOrderQuery = @"
                INSERT INTO [Order] (idP, idU, totalPrice)
                VALUES (@ProductId, @UserId, @TotalPrice)";

                    using (SqlCommand cmd = new SqlCommand(insertOrderQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", item.Item1);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        cmd.ExecuteNonQuery();
                    }
                }

                string clearCartQuery = "DELETE FROM shopcart WHERE idU = @UserId";

                using (SqlCommand cmd = new SqlCommand(clearCartQuery, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("~/Pages/OrderConfirmation/OrderConfirmation.aspx");
        }
    }
}
