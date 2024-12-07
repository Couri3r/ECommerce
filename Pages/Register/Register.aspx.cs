using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
namespace Ecomm_project
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString);
        int temp = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                con.Open();

                string checkUser = "select count(*) from [User] where uName = @uName";
                SqlCommand com = new SqlCommand(checkUser, con);
                com.Parameters.AddWithValue("@uName", uname.Text);
                temp = Convert.ToInt32(com.ExecuteScalar());

                if (temp == 1)
                {
                    Response.Write("User already exists!!!!");
                }

                string checkEmail = "select count(*) from [User] where email = @Email";
                com = new SqlCommand(checkEmail, con);
                com.Parameters.AddWithValue("@Email", email.Text);
                int emailExists = Convert.ToInt32(com.ExecuteScalar());

                if (emailExists == 1)
                {
                    Response.Write("Email already exists!!!!");
                }

                con.Close();
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            if (temp != 1) 
            {
                try
                {
                    string hashedPassword = hashPassword(pass.Text); 

                    con.Open();
                    string insertUser = "insert into [User] (uName, uPass, email) values (@uname, @pwd, @Email)";
                    SqlCommand com = new SqlCommand(insertUser, con);
                    com.Parameters.AddWithValue("@uname", uname.Text);
                    com.Parameters.AddWithValue("@pwd", hashedPassword);
                    com.Parameters.AddWithValue("@Email", email.Text); 
                    com.ExecuteNonQuery();

                    con.Close();

                    Response.Redirect("~/Pages/Login/Login.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.ToString());
                }
            }
        }

        private string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }


    }
}