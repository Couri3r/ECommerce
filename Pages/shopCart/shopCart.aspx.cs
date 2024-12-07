using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerce__Project.Pages.shopCart
{
    public partial class shopCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    LoadCartItems();
                }
                else
                {
                    Response.Redirect("~/Pages/Login/Login.aspx");
                }
            }
        }

        private void LoadCartItems()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;
            int userId = Convert.ToInt32(Session["UserId"]); // Retrieve the user ID from the session

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Query to retrieve cart items and product details for the logged-in user
                    string query = @"SELECT p.labelP AS ProductName, p.priceP AS Price, p.photopath AS ImagePath,  sc.idp AS ProductID FROM shopcart sc JOIN product p ON sc.idp = p.idP WHERE sc.idu = @UserId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            gvCart.DataSource = dt;
                            gvCart.DataBind();

                            // Calculate the total
                            decimal total = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                total += Convert.ToDecimal(row["Price"]);
                            }

                            lblTotal.Text = $"Total: ${total:0.00}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = $"Error loading cart items: {ex.Message}";
                    lblError.Visible = true;
                }
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Checkout/Checkout.aspx");
        }


    }
}