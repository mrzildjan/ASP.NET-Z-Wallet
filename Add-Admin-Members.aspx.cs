using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace Z_Wallet
{
    public partial class Add_Admin_Members : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null && Session["FirstName"] == null && Session["LastName"] == null)
            {
                Response.Redirect("Default.aspx"); // Redirect to the login page
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            DateTime createdDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Global.CustomTimeZone); // Set the default value for CreatedDateTime

            // Validate input data
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // Check if the email address already exists in the Users or Admins table
                if (EmailExists(email))
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Email address already exists.";
                    return;
                }

                // Insert data into the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Admins (FirstName, LastName, Email, Password, CreatedDateTime, Avatar) VALUES (@FirstName, @LastName, @Email, @Password, @CreatedDateTime, @Avatar)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@CreatedDateTime", createdDateTime);

                        // Convert and store the image file as a byte array
                        byte[] imageData = null;
                        if (fileUpload.HasFile)
                        {
                            imageData = fileUpload.FileBytes;
                        }

                        // Handle the case when no image is uploaded
                        if (imageData != null)
                        {
                            SqlParameter avatarParameter = new SqlParameter("@Avatar", SqlDbType.Image);
                            avatarParameter.Value = imageData;
                            command.Parameters.Add(avatarParameter);
                        }
                        else
                        {
                            command.Parameters.Add("@Avatar", SqlDbType.Image).Value = DBNull.Value;
                        }

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    // Display success message
                    lblSuccessMessage.Visible = true;
                    lblSuccessMessage.Text = "Admin member created successfully.";
                }
            }
            else
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please fill in all the fields.";
            }
        }


        // Check if the email address already exists in the Users or Admins table
        private bool EmailExists(string email)
        {
            bool exists = false;

            // Check Users table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    if (userCount > 0)
                    {
                        exists = true;
                    }
                }
            }

            if (exists) return true;

            // Check Admins table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admins WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int adminCount = Convert.ToInt32(command.ExecuteScalar());
                    if (adminCount > 0)
                    {
                        exists = true;
                    }
                }
            }

            return exists;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin-Members.aspx");
        }
    }
}