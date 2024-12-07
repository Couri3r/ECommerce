using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerce__Project.Pages.ResetPassword
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                {
                    lblMessage.Text = "Invalid token.";
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            if (string.IsNullOrEmpty(token))
            {
                lblMessage.Text = "Invalid token.";
                return;
            }

            try
            {
                con.Open();
                string query = "SELECT uName, TokenExpiration FROM [User] WHERE ResetToken = @Token";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Token", token);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    DateTime tokenExpiration = (DateTime)reader["TokenExpiration"];
                    if (tokenExpiration < DateTime.Now)
                    {
                        lblMessage.Text = "Token has expired.";
                        return;
                    }

                    string username = reader["uName"].ToString();
                    reader.Close();

                    string hashedPassword = hashPassword(txtNewPassword.Text);

                    string updateQuery = "UPDATE [User] SET uPass = @NewPassword, ResetToken = NULL, TokenExpiration = NULL WHERE uName = @UserName";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@NewPassword", hashedPassword);
                    updateCmd.Parameters.AddWithValue("@UserName", username);
                    updateCmd.ExecuteNonQuery();

                    lblMessage.Text = "Password has been reset successfully!";
                }
                else
                {
                    lblMessage.Text = "Invalid token.";
                }

                con.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
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