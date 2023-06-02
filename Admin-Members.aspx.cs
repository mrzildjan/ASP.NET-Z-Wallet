using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Admin_Members : System.Web.UI.Page
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
                    BindMembers();
                }
            }
        }

        private void BindMembers()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
            string query = "SELECT AdminID, FirstName, LastName, Email, CreatedDateTime FROM Admins ORDER BY AdminID DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<AdminMember> adminList = new List<AdminMember>();

                    while (reader.Read())
                    {
                        AdminMember adminMember = new AdminMember();
                        adminMember.AdminID = Convert.ToInt32(reader["AdminID"]);
                        adminMember.FullName = $"{reader["FirstName"]} {reader["LastName"]}";
                        adminMember.Email = reader["Email"].ToString();
                        adminMember.DateCreated = ((DateTime)reader["CreatedDateTime"]).ToString("MMM dd, yyyy hh:mm:ss tt");

                        adminList.Add(adminMember);
                    }

                    reader.Close();
                    connection.Close();

                    AdminList = adminList;
                }
            }
        }

        protected List<AdminMember> AdminList;
    }

    public class AdminMember
    {
        public string FullName;
        public string Email;
        public string DateCreated;
        public int AdminID;
    }
}
