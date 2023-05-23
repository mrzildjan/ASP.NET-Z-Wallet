﻿using System;
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
                        // Store user information in session
                        reader.Read();
                        int userId = (int)reader["UserId"];
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];

                        Session["UserId"] = userId;
                        Session["FirstName"] = firstName;
                        Session["LastName"] = lastName;

                        reader.Close();
                        connection.Close();

                        // Redirect to the desired page
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        // User login failed
                        // Show error message or take appropriate action
                        loginErrorLabel.Text = "Invalid email or password!";
                        loginErrorLabel.Visible = true; // Make the error label visible

                        string alertScript = "<script>alert('Invalid email or password. Please try again');</script>";
                        Response.Write(alertScript);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during login
                loginErrorLabel.Text = "An error occurred during login";
                loginErrorLabel.Visible = true; // Make the error label visible

                // Log the exception for troubleshooting

                string alertScript = "<script>alert('An error occurred during login');</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginErrorAlert", alertScript, false);
            }
        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
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

                    // Check if email or phone number already exist in the database
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email OR PhoneNumber = @PhoneNumber";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Email", email);
                    checkCommand.Parameters.AddWithValue("@PhoneNumber", phone);

                    int existingUserCount = (int)checkCommand.ExecuteScalar();
                    if (existingUserCount > 0)
                    {
                        // Email or phone number already exists
                        string alertScript = "<script>alert('Email or phone number already exists.');</script>";
                        Response.Write(alertScript);
                        signupErrorLabel.Text = "Email or phone number already exists.";
                        return; // Exit the method without executing the insert query
                    }

                    // Proceed with user registration
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
