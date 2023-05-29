using System;
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
                        // Prepare the database query to save or update the verification information
                        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand("IF EXISTS (SELECT * FROM [User-Verification] WHERE AccountNumber = @AccountNumber) " +
                                                                "UPDATE [User-Verification] SET " +
                                                                "VerificationStatus = @VerificationStatus, FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Birthdate = @Birthdate, " +
                                                                "Gender = @Gender, Nationality = @Nationality, PlaceOfBirth = @PlaceOfBirth, AddressLineOne = @AddressLineOne, AddressLineTwo = @AddressLineTwo, " +
                                                                "City = @City, Province = @Province, PostalCode = @PostalCode, Country = @Country, IDType = @IDType, " +
                                                                "FrontIDPicture = @FrontIDPicture, BackIDPicture = @BackIDPicture " +
                                                                "WHERE AccountNumber = @AccountNumber " +
                                                                "ELSE " +
                                                                "INSERT INTO [User-Verification] (AccountNumber, VerificationStatus, FirstName, LastName, MiddleName, Birthdate, Gender, Nationality, PlaceOfBirth, " +
                                                                "AddressLineOne, AddressLineTwo, City, Province, PostalCode, Country, IDType, FrontIDPicture, BackIDPicture) " +
                                                                "VALUES (@AccountNumber, @VerificationStatus, @FirstName, @LastName, @MiddleName, @Birthdate, @Gender, @Nationality, @PlaceOfBirth, " +
                                                                "@AddressLineOne, @AddressLineTwo, @City, @Province, @PostalCode, @Country, @IDType, @FrontIDPicture, @BackIDPicture)", connection);

                            command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                            command.Parameters.AddWithValue("@VerificationStatus", "Pending");
                            command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                            command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                            command.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
                            command.Parameters.AddWithValue("@Birthdate", Convert.ToDateTime(txtBirthDate.Text));
                            command.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                            command.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                            command.Parameters.AddWithValue("@PlaceOfBirth", txtPlaceOfBirth.Text);
                            command.Parameters.AddWithValue("@AddressLineOne", txtAddressLine1.Text);
                            command.Parameters.AddWithValue("@AddressLineTwo", txtAddressLine2.Text);
                            command.Parameters.AddWithValue("@City", txtCityAddress.Text);
                            command.Parameters.AddWithValue("@Province", ProvinceAddress.Text);
                            command.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                            command.Parameters.AddWithValue("@Country", ddlCountries.SelectedValue);
                            command.Parameters.AddWithValue("@IDType", ddlIDType.SelectedValue);

                            // Save the front ID picture
                            string fileName1 = Path.GetFileName(fileUpload1.PostedFile.FileName);
                            byte[] imageData1 = fileUpload1.FileBytes;
                            command.Parameters.AddWithValue("@FrontIDPicture", imageData1);

                            // Save the back ID picture
                            string fileName2 = Path.GetFileName(fileUpload2.PostedFile.FileName);
                            byte[] imageData2 = fileUpload2.FileBytes;
                            command.Parameters.AddWithValue("@BackIDPicture", imageData2);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();

                            // Display success message or perform other actions
                            lblSuccessMessage.Visible = true;
                            lblSuccessMessage.Text = "Form submitted successfully!";
                            lblErrorMessage.Visible = false;
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

        private bool IsAccountVerified(int accountNumber)
        {
            // Check if the account is already verified in the database
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT VerificationStatus FROM [User-Verification] WHERE AccountNumber = @AccountNumber", connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string verificationStatus = reader["VerificationStatus"].ToString();
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
            Response.Redirect("~/Default.aspx");
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
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [User-Verification] WHERE AccountNumber = @AccountNumber", connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lblAccountStatus.Text = reader["VerificationStatus"].ToString();
                    if (lblAccountStatus.Text == "Pending")
                    {
                        lblAccountStatus.CssClass = "status-text status-pending";
                    }
                    else if (lblAccountStatus.Text == "Verified")
                    {
                        lblAccountStatus.CssClass = "status-text status-verified";
                        cardfileUpload1.Visible = false; // Hide front ID picture file upload
                        cardfileUpload2.Visible = false; // Hide back ID picture file upload
                    }
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
                connection.Close();
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
