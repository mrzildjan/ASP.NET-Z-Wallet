using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Admin_Dashboard : System.Web.UI.MasterPage
    {
        private string firstName;
        private string lastName;
        private string avatarUrl;

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
                    LoadUserInfo();
                    SetUserFullName();
                    SetAdminAvatar();
                }
            }
        }

        private void LoadUserInfo()
        {
            if (Session["FirstName"] != null && Session["LastName"] != null)
            {
                firstName = Session["FirstName"].ToString();
                lastName = Session["LastName"].ToString();
            }
        }

        protected void SetAdminAvatar()
        {
            string email = Session["Email"].ToString(); // Get the user's email from the session

            // Fetch the avatar image data from the database
            byte[] imageData = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Avatar FROM Admins WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        imageData = (byte[])reader["Avatar"];
                    }
                }
                reader.Close();
            }

            if (imageData != null && imageData.Length > 0)
            {
                // Convert the image data to a base64 string
                string base64Image = Convert.ToBase64String(imageData);

                // Create the image URL with the base64 string
                avatarUrl = "data:image/jpeg;base64," + base64Image;
            }
            else
            {
                // Set the default image URL if avatar image data is null or empty
                avatarUrl = "/Content/assets/images/user.png";
            }

            userAvatarImage.Src = avatarUrl;
        }

        protected void SetUserFullName()
        {
            userFullNameLabel.Text = $"{firstName} {lastName}";
            LoginNameLabel.Text = $"{firstName} {lastName}";
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Default.aspx");
        }
    }
}
