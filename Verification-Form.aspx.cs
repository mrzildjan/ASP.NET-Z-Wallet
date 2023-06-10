using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Verification : Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;
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
                    PopulateCountries();
                    int accountNumber;
                    if (int.TryParse(Session["AccountNumber"].ToString(), out accountNumber))
                    {
                        // Load the user's verification information if available
                        LoadVerificationInfo(accountNumber);
                    }
                    else
                    {
                        // Handle invalid account number in session
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate the form fields
            if (ValidateForm())
            {
                // Get the account number from the session
                int accountNumber = Convert.ToInt32(Session["AccountNumber"]);

                // Check if the account is already verified
                if (IsAccountVerified(accountNumber))
                {
                    // Display message that the account is already verified
                    lblErrorMessage.Text = "Account is already verified.";
                    lblErrorMessage.Visible = true;
                    lblSuccessMessage.Visible = false;
                }
                else
                {
                    // Check if both front and back ID pictures are chosen
                    if (fileUpload1.HasFile && fileUpload2.HasFile)
                    {
                        // Check file extensions
                        string fileExtension1 = Path.GetExtension(fileUpload1.FileName).ToLower();
                        string fileExtension2 = Path.GetExtension(fileUpload2.FileName).ToLower();

                        if (IsImageExtension(fileExtension1) && IsImageExtension(fileExtension2))
                        {
                            // Prepare the database query to save or update the verification information
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                SqlCommand command = new SqlCommand("IF EXISTS (SELECT * FROM [User-Verification] WHERE AccountNumber = @AccountNumber) " +
                                                                    "UPDATE [User-Verification] SET " +
                                                                    "FrontIDPicture = @FrontIDPicture, BackIDPicture = @BackIDPicture " +
                                                                    "WHERE AccountNumber = @AccountNumber " +
                                                                    "ELSE " +
                                                                    "INSERT INTO [User-Verification] (AccountNumber, FrontIDPicture, BackIDPicture) " +
                                                                    "VALUES (@AccountNumber, @FrontIDPicture, @BackIDPicture)", connection);

                                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                                // Save the front ID picture
                                byte[] imageData1 = fileUpload1.FileBytes;
                                command.Parameters.AddWithValue("@FrontIDPicture", imageData1);

                                // Save the back ID picture
                                byte[] imageData2 = fileUpload2.FileBytes;
                                command.Parameters.AddWithValue("@BackIDPicture", imageData2);

                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();

                                // Update the account status in the Users table to "Pending"
                                UpdateAccountStatus(accountNumber, "Pending");

                                // Display success message or perform other actions
                                lblSuccessMessage.Visible = true;
                                lblSuccessMessage.Text = "Form submitted successfully!";
                                lblErrorMessage.Visible = false;
                            }
                        }
                        else
                        {
                            // Display error message for invalid file extensions
                            lblErrorMessage.Text = "Please choose image files only.";
                            lblErrorMessage.Visible = true;
                            lblSuccessMessage.Visible = false;
                        }
                    }
                    else
                    {
                        // Display error message for not choosing both front and back ID pictures
                        lblErrorMessage.Text = "Please choose both front and back ID pictures.";
                        lblErrorMessage.Visible = true;
                        lblSuccessMessage.Visible = false;
                    }
                }
            }
        }

        private bool IsImageExtension(string fileExtension)
        {
            // Define the allowed image file extensions
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

            // Check if the file extension is in the allowed extensions list
            return allowedExtensions.Contains(fileExtension);
        }

        private void UpdateAccountStatus(int accountNumber, string status)
        {
            // Update the account status in the Users table        
            string query = "UPDATE [Users] SET [AccountStatus] = @AccountStatus WHERE [AccountNumber] = @AccountNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountStatus", status);
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


        private bool IsAccountVerified(int accountNumber)
        {
            // Check if the account is already verified in the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT AccountStatus FROM Users WHERE AccountNumber = @AccountNumber", connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string verificationStatus = reader["AccountStatus"].ToString();
                    if (verificationStatus == "Verified")
                    {
                        reader.Close();
                        connection.Close();
                        return true;
                    }
                }
                reader.Close();
                connection.Close();
            }

            return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Clear the form fields and redirect the user
            ClearForm();
            Response.Redirect("~/Dashboard.aspx");
        }

        private void PopulateCountries()
        {
            // Clear existing items in the countries dropdown list
            ddlCountries.Items.Clear();

            // Add a default 'Select Country' option to the countries dropdown list
            ddlCountries.Items.Add(new ListItem("Select Country", ""));

            // Get a list of RegionInfo objects representing all the specific cultures
            var countries = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures)
                            .Select(x => new System.Globalization.RegionInfo(x.LCID))
                            .Distinct()
                            .OrderBy(x => x.DisplayName);

            // Loop through the countries list and add each country to the countries dropdown list
            foreach (var country in countries)
            {
                ddlCountries.Items.Add(new ListItem(country.DisplayName, country.EnglishName));
            }
        }

        private void LoadVerificationInfo(int accountNumber)
        {
            // Prepare the database query to load the user's verification information
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Load the verification information from the User-Verification table
                SqlCommand command = new SqlCommand("SELECT * FROM [User-Verification] WHERE AccountNumber = @AccountNumber", connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtMiddleName.Text = reader["MiddleName"].ToString();
                    txtBirthDate.Text = Convert.ToDateTime(reader["Birthdate"]).ToString("yyyy-MM-dd");
                    ddlGender.SelectedValue = reader["Gender"].ToString();
                    txtNationality.Text = reader["Nationality"].ToString();
                    txtPlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    txtAddressLine1.Text = reader["AddressLineOne"].ToString();
                    txtAddressLine2.Text = reader["AddressLineTwo"].ToString();
                    txtCityAddress.Text = reader["City"].ToString();
                    ProvinceAddress.Text = reader["Province"].ToString();
                    txtPostalCode.Text = reader["PostalCode"].ToString();
                    ddlCountries.SelectedValue = reader["Country"].ToString();
                    ddlIDType.SelectedValue = reader["IDType"].ToString();
                }
                reader.Close();

                // Load the verification status from the Users table
                command = new SqlCommand("SELECT AccountStatus FROM [Users] WHERE AccountNumber = @AccountNumber", connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                SqlDataReader statusReader = command.ExecuteReader();
                if (statusReader.Read())
                {
                    string accountStatus = statusReader["AccountStatus"].ToString();
                    lblAccountStatus.Text = accountStatus;
                    lblAccountStatus.CssClass = "status-badge " + GetStatusBadgeClass(accountStatus);

                    if (accountStatus == "Pending" || accountStatus == "Unverified" || accountStatus == "Denied")
                    {
                        cardfileUpload1.Visible = true; // Show front ID picture file upload
                        cardfileUpload2.Visible = true; // Show back ID picture file upload
                    }
                    else if (accountStatus == "Verified")
                    {
                        cardfileUpload1.Visible = false; // Hide front ID picture file upload
                        cardfileUpload2.Visible = false; // Hide back ID picture file upload
                    }
                }
                statusReader.Close();

                connection.Close();
            }
        }


        protected string GetStatusBadgeClass(string status)
        {
            switch (status)
            {
                case "Verified":
                    return "bg-success";
                case "Pending":
                    return "bg-info";
                case "Denied":
                    return "bg-danger";
                case "Suspended":
                    return "bg-warning";
                case "Unverified":
                    return "bg-secondary";
                default:
                    return "bg-secondary";
            }
        }


        private bool ValidateForm()
        {
            // Perform validation for the required fields
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                lblErrorMessage.Text = "First name is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                lblErrorMessage.Text = "Last name is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (ddlIDType.SelectedIndex == 0)
            {
                lblErrorMessage.Text = "ID type is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtBirthDate.Text))
            {
                lblErrorMessage.Text = "Birth date is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (ddlGender.SelectedIndex == -1)
            {
                lblErrorMessage.Text = "Gender is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtNationality.Text))
            {
                lblErrorMessage.Text = "Nationality is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPlaceOfBirth.Text))
            {
                lblErrorMessage.Text = "Place of birth is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtAddressLine1.Text))
            {
                lblErrorMessage.Text = "Address Line 1 is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCityAddress.Text))
            {
                lblErrorMessage.Text = "City is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(ProvinceAddress.Text))
            {
                lblErrorMessage.Text = "Province is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPostalCode.Text))
            {
                lblErrorMessage.Text = "Postal code is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (ddlCountries.SelectedIndex == 0)
            {
                lblErrorMessage.Text = "Country is required.";
                lblErrorMessage.Visible = true;
                return false;
            }

            lblErrorMessage.Visible = false;
            return true;
        }

        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtBirthDate.Text = string.Empty;
            ddlGender.SelectedIndex = -1;
            txtNationality.Text = string.Empty;
            txtPlaceOfBirth.Text = string.Empty;
            txtAddressLine1.Text = string.Empty;
            txtAddressLine2.Text = string.Empty;
            txtCityAddress.Text = string.Empty;
            ProvinceAddress.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            ddlCountries.SelectedIndex = 0;
            ddlIDType.SelectedIndex = 0;
            lblErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;
        }
    }
}
