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
                        previewImage.ImageUrl = "/Content/assets/images/2.jpg";
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
            string password = txtPassword.Text;

            if (!IsNewPasswordSameAsCurrent(accountNumber, password))
            {
                // Update the user's profile data in the database
                UpdateUserProfile(accountNumber, firstName, lastName, email, contact, password);

                // Reload the profile data
                LoadProfileData();

                // Display a success message
                lblSuccessMessage.Text = "Profile updated successfully.";
                lblSuccessMessage.Visible = true;
            }
            else
            {
                // Display an error message indicating that the new password is the same as the current password
                lblErrorMessage.Text = "New password must be different from the current password.";
                lblErrorMessage.Visible = true;
            }
        }

        private bool IsNewPasswordSameAsCurrent(int accountNumber, string newPassword)
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

                return matchingCount > 0;
            }
        }

        private void UpdateUserProfile(int accountNumber, string firstName, string lastName, string email, string contact, string password)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @Contact, Password = @Password WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contact", contact);
                command.Parameters.AddWithValue("@Password", password);
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
            }
        }
    }
}
