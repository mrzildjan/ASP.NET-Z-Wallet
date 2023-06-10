using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Z_Wallet
{
    public partial class Admin_Profile : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null && Session["FirstName"] == null && Session["LastName"] == null)
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
            if (Session["Email"] != null)
            {
                string email = Session["Email"].ToString();

                DataTable accountData = GetAdminAccountData(email);

                if (accountData.Rows.Count > 0)
                {
                    DataRow row = accountData.Rows[0];
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    txtEmail.Text = row["Email"].ToString();

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
                }
            }
        }

        private DataTable GetAdminAccountData(string email) // Changed parameter type to string
        {
            DataTable accountData = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Admins WHERE Email = @Email";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(accountData);
            }

            return accountData;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string email = Session["Email"].ToString();
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string updatedEmail = txtEmail.Text; // Changed variable name to avoid conflicts

            // Update the user's profile data in the database
            UpdateAdminProfile(email, firstName, lastName, updatedEmail); // Updated parameter name

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
        }

        private void UpdateAdminProfile(string email, string firstName, string lastName, string updatedEmail) // Updated parameter name
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Admins SET FirstName = @FirstName, LastName = @LastName, Email = @UpdatedEmail WHERE Email = @Email"; // Updated column name

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@UpdatedEmail", updatedEmail); // Updated parameter name
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string email = Session["Email"].ToString(); // Changed variable name to avoid conflicts

                // Get the uploaded file data as byte array
                byte[] imageData = fileUpload.FileBytes;

                // Update the user's profile picture in the database
                UpdateAdminAvatar(email, imageData); // Updated parameter name

                // Reload the profile data
                LoadProfileData();
            }
        }

        private void UpdateAdminAvatar(string email, byte[] imageData) // Updated parameter name
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Admins SET Avatar = @Avatar WHERE Email = @Email";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Avatar", imageData);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                lblAvatarSuccessMessage.Text = "Avatar changed successfully.";
                lblAvatarSuccessMessage.Visible = true;

                lblPasswordSuccessMessage.Visible = false;
                lblPasswordErrorMessage.Visible = false;
                lblSuccessMessage.Visible = false;
                lblErrorMessage.Visible = false;
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string email = Session["Email"].ToString(); // Changed variable name to avoid conflicts
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;

            // Verify if the old password matches the current password
            if (IsOldPasswordValid(email, oldPassword)) // Updated parameter name
            {
                // Check if the new password is different from the current password
                if (IsNewPasswordDifferent(email, newPassword)) // Updated parameter name
                {
                    // Update the user's password in the database
                    UpdateAdminPassword(email, newPassword); // Updated parameter name

                    // Display a success message
                    lblPasswordSuccessMessage.Text = "Password changed successfully.";
                    lblPasswordSuccessMessage.Visible = true;

                    // Hide error message
                    lblPasswordErrorMessage.Visible = false;

                    lblSuccessMessage.Visible = false;
                    lblErrorMessage.Visible = false;
                    lblAvatarSuccessMessage.Visible = false;
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
            }
        }

        private bool IsNewPasswordDifferent(string email, string newPassword) // Updated parameter name
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admins WHERE Email = @Email AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", newPassword);

                connection.Open();
                int matchingCount = (int)command.ExecuteScalar();
                connection.Close();

                return matchingCount == 0;
            }
        }

        private bool IsOldPasswordValid(string email, string oldPassword) // Updated parameter name
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admins WHERE Email = @Email AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", oldPassword);

                connection.Open();
                int matchingCount = (int)command.ExecuteScalar();
                connection.Close();

                return matchingCount > 0;
            }
        }

        private void UpdateAdminPassword(string email, string newPassword) // Updated parameter name
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Admins SET Password = @Password WHERE Email = @Email";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@Email", email);

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
        }
    }
}
