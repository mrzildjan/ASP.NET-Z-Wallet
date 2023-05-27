using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class User_Dashboard : MasterPage
    {
        private string firstName;
        private string lastName;
        private string avatarUrl;

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
                    LoadUserInfo();
                    SetUserFullName();
                    SetUserAvatar();
                }
            }
        }

        private void LoadUserInfo()
        {
            if (Session["AccountNumber"] != null)
            {
                int accountNumber = Convert.ToInt32(Session["AccountNumber"]);
                DataTable userInfo = GetUserInfo(accountNumber);

                if (userInfo.Rows.Count > 0)
                {
                    DataRow row = userInfo.Rows[0];
                    firstName = row["FirstName"].ToString();
                    lastName = row["LastName"].ToString();
                    byte[] avatarData = row["Avatar"] as byte[];
                    if (avatarData != null && avatarData.Length > 0)
                    {
                        avatarUrl = "data:image/jpeg;base64," + Convert.ToBase64String(avatarData);
                    }
                    else
                    {
                        // Set the default image URL if avatar is null
                        avatarUrl = "/Content/assets/images/user.png";
                    }
                }
            }
        }

        protected void SetUserAvatar()
        {
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                userAvatarImage.Src = avatarUrl;
            }
            else
            {
                // Set the default image URL if avatar URL is null or empty
                userAvatarImage.Src = "/Content/assets/images/user.png";
            }
        }


        private DataTable GetUserInfo(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT FirstName, LastName, Avatar FROM Users WHERE AccountNumber = @AccountNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable userInfo = new DataTable();
                adapter.Fill(userInfo);

                return userInfo;
            }
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
