using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AccountNumber"] == null)
            {
                Response.Redirect("Default.aspx"); // Redirect to the login page
            }
            else
            {
                if (!IsPostBack)
                {
                    LoadProfileData();
                }
            }
        }

        private void LoadProfileData()
        {
            if (Session["AccountNumber"] != null)
            {
                int accountNumber = Convert.ToInt32(Session["AccountNumber"]);

                DataTable accountData = GetUserAccountData(accountNumber);

                if (accountData.Rows.Count > 0)
                {
                    DataRow row = accountData.Rows[0];
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    txtContact.Text = row["PhoneNumber"].ToString();

                    // Load and display the profile picture
                    byte[] imageData = row["Avatar"] as byte[];
                    if (imageData != null && imageData.Length > 0)
                    {
                        string base64Image = Convert.ToBase64String(imageData);
                        string imageUrl = "data:image/jpeg;base64," + base64Image;
                        previewImage.ImageUrl = imageUrl;
                    }
                    else
                    {
                        // Set the default profile picture path
                        previewImage.ImageUrl = "/Content/assets/images/user.png";
                    }

                    // Display the account status
                    string accountStatus = row["isDeactivated"].ToString();
                    if (accountStatus == "Active")
                    {
                        lblAccountStatus.Text = "Active";
                        lblAccountStatus.CssClass = "status-text status-active";
                    }
                    else
                    {
                        lblAccountStatus.Text = "Inactive";
                        lblAccountStatus.CssClass = "status-text status-inactive d-flex";
                    }
                }
            }
        }

        private DataTable GetUserAccountData(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            DataTable accountData = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(accountData);
            }

            return accountData;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int accountNumber = Convert.ToInt32(Session["AccountNumber"]);
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string contact = txtContact.Text;

            // Update the user's profile data in the database
            UpdateUserProfile(accountNumber, firstName, lastName, email, contact);

            // Reload the profile data
            LoadProfileData();

            // Display a success message
            lblSuccessMessage.Text = "Profile updated successfully.";
            lblSuccessMessage.Visible = true;

            // Hide error message
            lblErrorMessage.Visible = false;

            lblPasswordSuccessMessage.Visible = false;
            lblPasswordErrorMessage.Visible = false;
            lblAvatarSuccessMessage.Visible = false;
            lblStatusErrorMessage.Visible = false;
            lblStatusSuccessMessage.Visible = false;
        }

        private void UpdateUserProfile(int accountNumber, string firstName, string lastName, string email, string contact)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @Contact WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contact", contact);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                int accountNumber = Convert.ToInt32(Session["AccountNumber"]);

                // Get the uploaded file data as byte array
                byte[] imageData = fileUpload.FileBytes;

                // Update the user's profile picture in the database
                UpdateUserAvatar(accountNumber, imageData);

                // Reload the profile data
                LoadProfileData();
            }
        }

        private void UpdateUserAvatar(int accountNumber, byte[] imageData)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Avatar = @Avatar WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Avatar", imageData);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                lblAvatarSuccessMessage.Text = "Avatar changed successfully.";
                lblAvatarSuccessMessage.Visible = true;

                lblPasswordSuccessMessage.Visible = false;
                lblPasswordErrorMessage.Visible = false;
                lblSuccessMessage.Visible = false;
                lblErrorMessage.Visible = false;
                lblStatusErrorMessage.Visible = false;
                lblStatusSuccessMessage.Visible = false;
                lblStatusErrorMessage.Visible = false;
                lblStatusSuccessMessage.Visible = false;
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            int accountNumber = Convert.ToInt32(Session["AccountNumber"]);
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;

            // Verify if the old password matches the current password
            if (IsOldPasswordValid(accountNumber, oldPassword))
            {
                // Check if the new password is different from the current password
                if (IsNewPasswordDifferent(accountNumber, newPassword))
                {
                    // Update the user's password in the database
                    UpdateUserPassword(accountNumber, newPassword);

                    // Display a success message
                    lblPasswordSuccessMessage.Text = "Password changed successfully.";
                    lblPasswordSuccessMessage.Visible = true;

                    // Hide error message
                    lblPasswordErrorMessage.Visible = false;

                    lblSuccessMessage.Visible = false;
                    lblErrorMessage.Visible = false;
                    lblAvatarSuccessMessage.Visible = false;
                    lblStatusErrorMessage.Visible = false;
                    lblStatusSuccessMessage.Visible = false;
                }
                else
                {
                    // Display an error message indicating that the new password must be different
                    lblPasswordErrorMessage.Text = "New password must be different from the current password.";
                    lblPasswordErrorMessage.Visible = true;

                    // Hide error message
                    lblPasswordSuccessMessage.Visible = false;

                    lblSuccessMessage.Visible = false;
                    lblErrorMessage.Visible = false;
                    lblAvatarSuccessMessage.Visible = false;
                    lblStatusErrorMessage.Visible = false;
                    lblStatusSuccessMessage.Visible = false;
                }
            }
            else
            {
                // Display an error message indicating the invalid old password
                lblPasswordErrorMessage.Text = "Invalid old password.";
                lblPasswordErrorMessage.Visible = true;

                lblPasswordSuccessMessage.Visible = false;

                lblSuccessMessage.Visible = false;
                lblErrorMessage.Visible = false;
                lblAvatarSuccessMessage.Visible = false;
                lblStatusErrorMessage.Visible = false;
                lblStatusSuccessMessage.Visible = false;
            }
        }

        private bool IsNewPasswordDifferent(int accountNumber, string newPassword)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE AccountNumber = @AccountNumber AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@Password", newPassword);

                connection.Open();
                int matchingCount = (int)command.ExecuteScalar();
                connection.Close();

                return matchingCount == 0;
            }
        }

        private bool IsOldPasswordValid(int accountNumber, string oldPassword)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE AccountNumber = @AccountNumber AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@Password", oldPassword);

                connection.Open();
                int matchingCount = (int)command.ExecuteScalar();
                connection.Close();

                return matchingCount > 0;
            }
        }

        private void UpdateUserPassword(int accountNumber, string newPassword)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Password = @Password WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblPasswordSuccessMessage.Visible = false;
            lblPasswordErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;
            lblAvatarSuccessMessage.Visible = false;
            lblStatusErrorMessage.Visible = false;
            lblStatusSuccessMessage.Visible = false;
        }

        protected void btnCancelPassword_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";

            lblPasswordSuccessMessage.Visible = false;
            lblPasswordErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;
            lblAvatarSuccessMessage.Visible = false;
            lblStatusErrorMessage.Visible = false;
            lblStatusSuccessMessage.Visible = false;
        }

        protected void btnReactivate_Click(object sender, EventArgs e)
        {
            int accountNumber = Convert.ToInt32(Session["AccountNumber"]);

            // Get the current account status from the database
            string currentStatus = GetAccountStatus(accountNumber);

            if (currentStatus == "Active")
            {
                // Account is already active, display a message
                lblStatusErrorMessage.Text = "Account is already active.";
                lblStatusErrorMessage.Visible = true;
                lblStatusSuccessMessage.Visible = false;
            }
            else
            {
                // Update the user's account status to "Active" in the database
                UpdateAccountStatus(accountNumber, "Active");

                // Set the status label text
                lblAccountStatus.Text = "Active";
                // Add the appropriate CSS class
                lblAccountStatus.CssClass = "status-text status-active";

                // Display a success message
                lblStatusSuccessMessage.Text = "Account reactivated successfully.";
                lblStatusSuccessMessage.Visible = true;
                lblStatusErrorMessage.Visible = false;
            }
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            int accountNumber = Convert.ToInt32(Session["AccountNumber"]);

            // Get the current account status from the database
            string currentStatus = GetAccountStatus(accountNumber);

            if (currentStatus == "Inactive")
            {
                // Account is already deactivated, display a message
                lblStatusErrorMessage.Text = "Account is already deactivated.";
                lblStatusErrorMessage.Visible = true;
                lblStatusSuccessMessage.Visible = false;
            }
            else
            {
                // Update the user's account status to "Inactive" in the database
                UpdateAccountStatus(accountNumber, "Inactive");

                // Set the status label text
                lblAccountStatus.Text = "Inactive";
                // Add the appropriate CSS class
                lblAccountStatus.CssClass = "status-text status-inactive d-flex";

                // Display a success message
                lblStatusSuccessMessage.Text = "Account deactivated successfully.";
                lblStatusSuccessMessage.Visible = true;
                lblStatusErrorMessage.Visible = false;
            }
        }

        private string GetAccountStatus(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT isDeactivated FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                string accountStatus = (string)command.ExecuteScalar();
                connection.Close();

                return accountStatus;
            }
        }

        private void UpdateAccountStatus(int accountNumber, string status)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET isDeactivated = @Status WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    lblStatusSuccessMessage.Text = "Account status updated successfully.";
                    lblStatusSuccessMessage.Visible = true;
                    lblStatusErrorMessage.Visible = false;
                }
                else
                {
                    lblStatusErrorMessage.Text = "Failed to update account status.";
                    lblStatusErrorMessage.Visible = true;
                    lblStatusSuccessMessage.Visible = false;
                }
            }
        }
    }
}
