using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Z_Wallet
{
    public partial class SiteMaster : MasterPage
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;

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

                    // Check if the login credentials match a user account
                    string userQuery = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
                    SqlCommand userCommand = new SqlCommand(userQuery, connection);
                    userCommand.Parameters.AddWithValue("@Email", email);
                    userCommand.Parameters.AddWithValue("@Password", password);

                    SqlDataReader userReader = userCommand.ExecuteReader();
                    if (userReader.HasRows)
                    {
                        bool isVerified = AccountStatus(email, password);

                        if (isVerified)
                        {
                            lblLoginErrorMessage.Visible = true;
                            lblLoginErrorMessage.Text = "Account is suspended plese contact help support.";

                            string alertScript = "<script>alert('Account is suspended plese contact help support.');</script>";
                            Response.Write(alertScript);
                        }
                        else
                        {
                            // User login success
                            // Store user information in session
                            userReader.Read();
                            int AccountNumber = (int)userReader["AccountNumber"]; // Update column name to "Id"
                            string firstName = (string)userReader["FirstName"];
                            string lastName = (string)userReader["LastName"];

                            Session["AccountNumber"] = AccountNumber;
                            Session["FirstName"] = firstName;
                            Session["LastName"] = lastName;

                            userReader.Close();
                            connection.Close();

                            // Redirect to the desired user page
                            Response.Redirect("Dashboard.aspx");
                        }
                    }
                    else
                    {
                        userReader.Close();

                        // Check if the login credentials match an admin account
                        string adminQuery = "SELECT * FROM Admins WHERE Email = @Email AND Password = @Password";
                        SqlCommand adminCommand = new SqlCommand(adminQuery, connection);
                        adminCommand.Parameters.AddWithValue("@Email", email);
                        adminCommand.Parameters.AddWithValue("@Password", password);

                        SqlDataReader adminReader = adminCommand.ExecuteReader();
                        if (adminReader.HasRows)
                        {
                            // Admin login success
                            // Store admin information in session
                            adminReader.Read();
                            string adminEmail = (string)adminReader["Email"];
                            string adminFirstName = (string)adminReader["FirstName"];
                            string adminLastName = (string)adminReader["LastName"];

                            Session["Email"] = adminEmail;
                            Session["FirstName"] = adminFirstName;
                            Session["LastName"] = adminLastName;

                            adminReader.Close();
                            connection.Close();

                            // Redirect to the desired admin page
                            Response.Redirect("Admin.aspx");
                        }
                        else
                        {
                            adminReader.Close();

                            // Login failed
                            // Show error message or take appropriate action
                            lblLoginErrorMessage.Text = "Invalid email or password!";
                            lblLoginErrorMessage.Visible = true; // Make the error label visible

                            string alertScript = "<script>alert('Invalid email or password!');</script>";
                            Response.Write(alertScript);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during login
                loginErrorLabel.Text = "An error occurred during login";
                loginErrorLabel.Visible = true; // Make the error label visible

                // Log the exception for troubleshooting
                Console.WriteLine("Exception during login: " + ex.Message);

                // Display the specific exception message to provide more information to the user
                string alertScript = $"<script>alert('An error occurred during login: {ex.Message}');</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginErrorAlert", alertScript, false);
            }
        }

        private bool AccountStatus(string email, string password)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AccountStatus FROM Users WHERE Email = @Email AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string accountStatus = result.ToString();
                    return accountStatus == "Suspended";
                }
                else
                {
                    throw new Exception("Account not found for the specified account number.");
                }
            }
        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            string firstName = signup_first_name.Text;
            string lastName = signup_last_name.Text;
            string email = signup_email.Text;
            string phone = signup_phone.Text;
            string password = signup_password.Text;
            DateTime signUpDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Global.CustomTimeZone);

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
                    if (existingUserCount > 0 || email.Equals("superadmin@zwallet.ph", StringComparison.OrdinalIgnoreCase))
                    {
                        // User with the same email or phone number already exists
                        // Show error message or take appropriate action
                        signupErrorLabel.Text = "Email or phone number already exists!";
                        AddAdminAccount(connection);
                        return; // Exit the method, do not proceed with registration
                    }

                    // Proceed with user registration
                    string query = "INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, Password, SignUpDateTime, CurrentBalance, TotalSendMoney, TotalReceiveMoney, TotalCashIn, TotalCashOut, Avatar, isDeactivated, AccountStatus) " +
                                    "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @SignUpDateTime, 0.00, 0.00, 0.00, 0.00, 0.00, NULL, 'Active', 'Unverified')";
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
                        signup_phone.Text = "+63";
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

        private void AddAdminAccount(SqlConnection connection)
        {
            string adminEmail = "superadmin@zwallet.ph";

            // Check if admin account already exists
            string checkQuery = "SELECT COUNT(*) FROM Admins WHERE Email = @Email";
            SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@Email", adminEmail);

            int existingAdminCount = (int)checkCommand.ExecuteScalar();
            if (existingAdminCount > 0)
            {
                // Admin account with the same email already exists
                // You can handle this case or skip adding a new row
                return; // Exit the method, no need to add a new admin account
            }

            // Admin account does not exist, proceed with adding a new row
            string adminFirstName = "Zildjan Leenor";
            string adminLastName = "Luvindino";
            string adminPassword = "admin";
            DateTime createdDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Global.CustomTimeZone);

            string query = "INSERT INTO Admins (FirstName, LastName, Email, Password, CreatedDateTime, Avatar) " +
                           "VALUES (@FirstName, @LastName, @Email, @Password, @CreatedDateTime, NULL)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", adminFirstName);
            command.Parameters.AddWithValue("@LastName", adminLastName);
            command.Parameters.AddWithValue("@Email", adminEmail);
            command.Parameters.AddWithValue("@Password", adminPassword);
            command.Parameters.AddWithValue("@CreatedDateTime", createdDateTime);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                // Admin account added successfully
            }
            else
            {
                // Failed to add admin account
            }
        }
        protected void termsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = termsCheckbox.Checked;
        }
    }
}
