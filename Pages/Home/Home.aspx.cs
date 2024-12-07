using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Ecomm_project
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT idP AS ProductId, labelP AS Name, desP AS Description, priceP AS Price, photopath AS ImagePath FROM product";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        ProductRepeater.DataSource = dt;
                        ProductRepeater.DataBind();
                    }
                    else
                    {
                        Response.Write("<p>No products found in the database.</p>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<p>Error: {ex.Message}</p>");
                }
            }
        }
        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string productId = e.CommandArgument.ToString();

                Response.Redirect($"~/Pages/ProductDetails/ProductDetails.aspx?ProductId={productId}");
            }
        }



    }

}
