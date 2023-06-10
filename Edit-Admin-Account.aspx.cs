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
    public partial class Edit_Admin_Account : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null || Session["FirstName"] == null || Session["LastName"] == null)
            {
                Response.Redirect("Default.aspx"); // Redirect to the login page
            }
            else
            {
                if (!IsPostBack)
                {
                    // Fetch member details from the database based on the AdminID
                    if (Request.QueryString["AdminID"] != null)
                    {
                        int adminID = Convert.ToInt32(Request.QueryString["AdminID"]);
                        LoadAdminProfile(adminID);
                    }
                }
            }
        }

        private void LoadAdminProfile(int adminID)
        {
            DataTable adminData = GetAdminAccountData(adminID);

            if (adminData.Rows.Count > 0)
            {
                DataRow row = adminData.Rows[0];
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

        private DataTable GetAdminAccountData(int adminID)
        {

            DataTable accountData = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Admins WHERE AdminID = @AdminID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdminID", adminID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(accountData);
            }

            return accountData;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int adminID = Convert.ToInt32(Request.QueryString["AdminID"]);
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string updatedEmail = txtEmail.Text;

            // Update the admin's profile data in the database
            UpdateAdminProfile(adminID, firstName, lastName, updatedEmail);

            // Reload the profile data
            LoadAdminProfile(adminID);

            // Display a success message
            lblSuccessMessage.Text = "Profile updated successfully.";
            lblSuccessMessage.Visible = true;

            // Hide error message
            lblErrorMessage.Visible = false;

            lblPasswordSuccessMessage.Visible = false;
            lblPasswordErrorMessage.Visible = false;
            lblAvatarSuccessMessage.Visible = false;
        }

        private void UpdateAdminProfile(int adminID, string firstName, string lastName, string updatedEmail)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Admins SET FirstName = @FirstName, LastName = @LastName, Email = @UpdatedEmail WHERE AdminID = @AdminID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@UpdatedEmail", updatedEmail);
                command.Parameters.AddWithValue("@AdminID", adminID);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int adminID = Convert.ToInt32(Request.QueryString["AdminID"]);

            if (fileUpload.HasFile)
            {
                // Get the uploaded file data as byte array
                byte[] imageData = fileUpload.FileBytes;

                // Update the admin's profile picture in the database
                UpdateAdminAvatar(adminID, imageData);

                // Reload the profile data
                LoadAdminProfile(adminID);
            }
        }

        private void UpdateAdminAvatar(int adminID, byte[] imageData)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Admins SET Avatar = @Avatar WHERE AdminID = @AdminID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Avatar", imageData);
                command.Parameters.AddWithValue("@AdminID", adminID);

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
            int adminID = Convert.ToInt32(Request.QueryString["AdminID"]);
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;

            // Verify if the old password matches the current password
            if (IsOldPasswordValid(adminID, oldPassword))
            {
                // Check if the new password is different from the current password
                if (IsNewPasswordDifferent(adminID, newPassword))
                {
                    // Update the admin's password in the database
                    UpdateAdminPassword(adminID, newPassword);

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

        private bool IsNewPasswordDifferent(int adminID, string newPassword)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admins WHERE AdminID = @AdminID AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdminID", adminID);
                command.Parameters.AddWithValue("@Password", newPassword);

                connection.Open();
                int matchingCount = (int)command.ExecuteScalar();
                connection.Close();

                return matchingCount == 0;
            }
        }

        private bool IsOldPasswordValid(int adminID, string oldPassword)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admins WHERE AdminID = @AdminID AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdminID", adminID);
                command.Parameters.AddWithValue("@Password", oldPassword);

                connection.Open();
                int matchingCount = (int)command.ExecuteScalar();
                connection.Close();

                return matchingCount > 0;
            }
        }

        private void UpdateAdminPassword(int adminID, string newPassword)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Admins SET Password = @Password WHERE AdminID = @AdminID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@AdminID", adminID);

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
