using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ECommerce__Project.Pages.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT idu, uPass FROM [User] WHERE uName = @uName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@uName", txtUsername.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashedPassword = reader["uPass"].ToString();

                                string enteredHashedPassword = hashPassword(txtPassword.Text.Trim());

                                if (enteredHashedPassword == storedHashedPassword)
                                {
                                    Session["UserId"] = Convert.ToInt32(reader["idu"]);
                                    Response.Redirect("~/Pages/Home/Home.aspx");
                                }
                                else
                                {
                                    lblError.Text = HttpUtility.HtmlEncode("Invalid username or password.");
                                    lblError.Visible = true;
                                }
                            }
                            else
                            {
                                lblError.Text = HttpUtility.HtmlEncode("Invalid username or password.");
                                lblError.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred while processing your request.";
                lblError.Visible = true;
                Console.WriteLine(ex.Message);
            }
        }

        private string hashPassword(string password)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
                byte[] encryptedBytes = sha1.ComputeHash(passwordBytes);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }
}
