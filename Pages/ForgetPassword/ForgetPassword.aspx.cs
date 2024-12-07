using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

namespace ECommerce__Project.Pages.ForgetPassword
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TechStore"].ConnectionString);

        protected void btnSendPassword_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string token = Guid.NewGuid().ToString(); 
                DateTime expiration = DateTime.Now.AddHours(1); 

                string query = "UPDATE [User] SET ResetToken = @Token, TokenExpiration = @Expiration WHERE email = @Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Token", token);
                cmd.Parameters.AddWithValue("@Expiration", expiration);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    string resetLink = "https://localhost:44369/Pages/ResetPassword/ResetPassword.aspx?token=" + token; 
                    SendResetEmail(txtEmail.Text, resetLink);
                    lblResult.Text = "Password reset link has been sent to your email!";
                    lblResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblResult.Text = "Email not found!";
                    lblResult.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error: " + ex.Message;
            }
        }

        private void SendResetEmail(string toEmail, string resetLink)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(""); 
            mail.To.Add(toEmail);
            mail.Subject = "Password Reset Request";
            mail.Body = "Reset your password by clicking here: " + resetLink;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("", "#");
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }


    }
}
