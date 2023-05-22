using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Z_Wallet
{
    public partial class SiteMaster : MasterPage
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = login_email.Text;
            string password = login_password.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // User login success
                        // Redirect to the desired page
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        // User login failed
                        // Show error message or take appropriate action
                        loginErrorLabel.Text = "Invalid email or password";
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during login
                loginErrorLabel.Text = "An error occurred during login";
                // Log the exception for troubleshooting
            }
        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            Response.Write("Button Clicked");

            string firstName = signup_first_name.Text;
            string lastName = signup_last_name.Text;
            string email = signup_email.Text;
            string phone = signup_phone.Text;
            string password = signup_password.Text;
            DateTime signUpDateTime = DateTime.Now;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, Password, SignUpDateTime) " +
                                   "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @SignUpDateTime)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PhoneNumber", phone);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@SignUpDateTime", signUpDateTime);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        if (rowsAffected > 0)
                        {
                            // User registration success
                            // Show success message or take appropriate action
                            signupSuccessLabel.Text = "Congratulations! You have successfully registered.";
                            signupErrorLabel.Text = "";

                            // Clear the form fields
                            signup_first_name.Text = "";
                            signup_last_name.Text = "";
                            signup_email.Text = "";
                            signup_phone.Text = "";
                            signup_password.Text = "";
                            signup_confirm_password.Text = "";

                            // Display window alert
                            string alertScript = "<script>alert('Congratulations! You have successfully registered.');</script>";
                            Response.Write(alertScript);
                        }
                        else
                        {
                            // User registration failed
                            // Show error message or take appropriate action
                            signupErrorLabel.Text = "Registration failed";
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during registration
                signupErrorLabel.Text = "An error occurred during registration";
                // Log the exception for troubleshooting
            }
        }
    }
}
