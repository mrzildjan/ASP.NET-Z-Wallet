using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Verification : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCountries();
            }
        }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            // Handle upload button click for the first file
            if (fileUpload1.PostedFile != null && fileUpload1.PostedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(fileUpload1.PostedFile.FileName);
                // Save the file to the desired location
                fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
                // Display success message or perform other actions
                lblFrontIDSuccessMessage.Visible = true;
                lblFrontIDSuccessMessage.Text = "File uploaded successfully!";
            }
        }

        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            // Handle upload button click for the second file
            if (fileUpload2.PostedFile != null && fileUpload2.PostedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(fileUpload2.PostedFile.FileName);
                // Save the file to the desired location
                fileUpload2.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
                // Display success message or perform other actions
                lblBackIDSuccessMessage.Visible = true;
                lblBackIDSuccessMessage.Text = "File uploaded successfully!";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Handle submit button click
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            // Get other form field values and perform necessary actions

            // Display success message or perform other actions
            lblSuccessMessage.Visible = true;
            lblSuccessMessage.Text = "Form submitted successfully!";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Handle cancel button click
            // Clear form fields or perform other necessary actions
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
    }
}
