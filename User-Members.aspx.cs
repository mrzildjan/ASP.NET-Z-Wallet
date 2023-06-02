using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class User_Members : System.Web.UI.Page
    {
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
                    PopulateCountries();
                    // Set the initial status badge class based on the selected status
                    string selectedStatus = ddlAccountStatus.SelectedValue;
                    string statusBadgeClass = GetStatusBadgeClass(selectedStatus);
                    ddlAccountStatus.CssClass = "status-text " + statusBadgeClass;

                    // Fetch member details from the database based on the accountNumber
                    if (Request.QueryString["accountNumber"] != null)
                    {
                        int accountNumber = Convert.ToInt32(Request.QueryString["accountNumber"]);
                        FetchMemberDetails(accountNumber);
                    }
                }
                else
                {
                    lblSuccessVerificationStatus.Visible = false;
                    lblErrorMessage.Visible = false;
                    lblSuccessMessage.Visible = false;
                }
            }
        }

        private void FetchMemberDetails(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
            string query = "SELECT * FROM [User-Verification] WHERE AccountNumber = @AccountNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Retrieve the member details from the reader and populate the form controls
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

                        // Retrieve and display the front ID picture
                        if (!reader.IsDBNull(reader.GetOrdinal("FrontIDPicture")))
                        {
                            byte[] frontIDPicture = (byte[])reader["FrontIDPicture"];
                            previewIDImage1.ImageUrl = "data:image;base64," + Convert.ToBase64String(frontIDPicture);
                        }

                        // Retrieve and display the back ID picture
                        if (!reader.IsDBNull(reader.GetOrdinal("BackIDPicture")))
                        {
                            byte[] backIDPicture = (byte[])reader["BackIDPicture"];
                            previewIDImage2.ImageUrl = "data:image;base64," + Convert.ToBase64String(backIDPicture);
                        }
                    }

                    reader.Close();
                    connection.Close();
                }
            }

            // Fetch the account status from the Users table
            query = "SELECT AccountStatus FROM [Users] WHERE AccountNumber = @AccountNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Set the account status dropdown list
                        string accountStatus = reader["AccountStatus"].ToString();
                        ddlAccountStatus.SelectedValue = accountStatus;
                        ddlAccountStatus.CssClass = "status-text " + GetStatusBadgeClass(accountStatus);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
        }

        protected void ddlAccountStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = ddlAccountStatus.SelectedValue;
            ddlAccountStatus.Text = selectedStatus;

            string statusBadgeClass = GetStatusBadgeClass(selectedStatus);
            ddlAccountStatus.CssClass = "status-text " + statusBadgeClass;
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate the form fields
            if (ValidateForm())
            {
                // Get the account number from the query string parameter
                if (Request.QueryString["accountNumber"] != null)
                {
                    int accountNumber = Convert.ToInt32(Request.QueryString["accountNumber"]);

                    // Prepare the database query to save or update the verification information
                    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("IF EXISTS (SELECT * FROM [User-Verification] WHERE AccountNumber = @AccountNumber) " +
                                                            "UPDATE [User-Verification] SET " +
                                                            "FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Birthdate = @Birthdate, " +
                                                            "Gender = @Gender, Nationality = @Nationality, PlaceOfBirth = @PlaceOfBirth, AddressLineOne = @AddressLineOne, AddressLineTwo = @AddressLineTwo, " +
                                                            "City = @City, Province = @Province, PostalCode = @PostalCode, Country = @Country, IDType = @IDType " +
                                                            (fileUpload1.HasFile ? ", FrontIDPicture = @FrontIDPicture " : "") +
                                                            (fileUpload2.HasFile ? ", BackIDPicture = @BackIDPicture " : "") +
                                                            "WHERE AccountNumber = @AccountNumber " +
                                                            "ELSE " +
                                                            "INSERT INTO [User-Verification] (AccountNumber, FirstName, LastName, MiddleName, Birthdate, Gender, Nationality, PlaceOfBirth, " +
                                                            "AddressLineOne, AddressLineTwo, City, Province, PostalCode, Country, IDType" +
                                                            (fileUpload1.HasFile ? ", FrontIDPicture" : "") +
                                                            (fileUpload2.HasFile ? ", BackIDPicture" : "") +
                                                            ") " +
                                                            "VALUES (@AccountNumber, @FirstName, @LastName, @MiddleName, @Birthdate, @Gender, @Nationality, @PlaceOfBirth, " +
                                                            "@AddressLineOne, @AddressLineTwo, @City, @Province, @PostalCode, @Country, @IDType" +
                                                            (fileUpload1.HasFile ? ", @FrontIDPicture" : "") +
                                                            (fileUpload2.HasFile ? ", @BackIDPicture" : "") +
                                                            ")", connection);

                        command.Parameters.AddWithValue("@AccountNumber", accountNumber);
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

                        if (fileUpload1.HasFile)
                        {
                            string fileName1 = Path.GetFileName(fileUpload1.PostedFile.FileName);
                            byte[] imageData1 = fileUpload1.FileBytes;
                            command.Parameters.AddWithValue("@FrontIDPicture", imageData1);
                        }

                        if (fileUpload2.HasFile)
                        {
                            string fileName2 = Path.GetFileName(fileUpload2.PostedFile.FileName);
                            byte[] imageData2 = fileUpload2.FileBytes;
                            command.Parameters.AddWithValue("@BackIDPicture", imageData2);
                        }

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        // Display success message or perform other actions
                        lblSuccessMessage.Visible = true;
                        lblSuccessMessage.Text = "Form submitted successfully!";
                        lblErrorMessage.Visible = false;
                    }
                }
            }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Manage-Members.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Retrieve the selected value from the dropdown
            string selectedStatus = ddlAccountStatus.SelectedValue;

            // Get the account number from the query string parameter
            if (Request.QueryString["accountNumber"] != null)
            {
                int accountNumber = Convert.ToInt32(Request.QueryString["accountNumber"]);

                // Update the account status in the database
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
                string query = "UPDATE [Users] SET [AccountStatus] = @AccountStatus WHERE [AccountNumber] = @AccountNumber";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AccountStatus", selectedStatus);
                        command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        // Display success message or perform other actions
                        lblSuccessVerificationStatus.Visible = true;
                        lblSuccessVerificationStatus.Text = "Successfully Change Status.";

                        lblErrorMessage.Visible = false;
                        lblSuccessMessage.Visible = false;
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Manage-Members.aspx");
        }
    }
}