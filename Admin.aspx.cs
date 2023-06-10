using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Z_Wallet
{
    public partial class Admin : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the admin is logged in
            if (Session["Email"] == null && Session["FirstName"] == null && Session["LastName"] == null)
            {
                // Admin is not logged in, redirect to login page
                Response.Redirect("Default.aspx");
            }
            // Admin is logged in
            string adminEmail = Session["Email"].ToString();
            DisplayAdminInfo(adminEmail);
            DisplayStatistics();
        }

        private void DisplayAdminInfo(string adminEmail)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Admins WHERE Email = @Email";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", adminEmail);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string adminFirstName = reader["FirstName"].ToString();
                        string adminLastName = reader["LastName"].ToString();
                        int adminID = Convert.ToInt32(reader["AdminID"]);

                        adminNameLabel.Text = $"{adminFirstName} {adminLastName}";
                        adminIDLabel.Text = adminID.ToString();
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred
                Console.WriteLine("Exception during admin info retrieval: " + ex.Message);
            }
        }

        private void DisplayStatistics()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get admin members count
                    string adminMembersQuery = "SELECT COUNT(*) FROM Admins";
                    SqlCommand adminMembersCommand = new SqlCommand(adminMembersQuery, connection);
                    int adminMembers = (int)adminMembersCommand.ExecuteScalar();
                    adminMembersLabel.Text = adminMembers.ToString();

                    // Get total users count
                    string usersQuery = "SELECT COUNT(*) FROM Users";
                    SqlCommand usersCommand = new SqlCommand(usersQuery, connection);
                    int totalUsers = (int)usersCommand.ExecuteScalar();
                    totalUsersLabel.Text = totalUsers.ToString();

                    // Get verified members count
                    string verifiedMembersQuery = "SELECT COUNT(*) FROM Users WHERE AccountStatus = 'Verified'";
                    SqlCommand verifiedMembersCommand = new SqlCommand(verifiedMembersQuery, connection);
                    int activeMembers = (int)verifiedMembersCommand.ExecuteScalar();
                    verifiedMembersLabel.Text = activeMembers.ToString();

                    // Get unverified members count
                    string unverfiedMembersQuery = "SELECT COUNT(*) FROM Users WHERE AccountStatus = 'Unverified'";
                    SqlCommand unverfiedMembersCommand = new SqlCommand(unverfiedMembersQuery, connection);
                    int unverifiedMembers = (int)unverfiedMembersCommand.ExecuteScalar();
                    unverfiedMembersLabel.Text = unverifiedMembers.ToString();

                    // Get pending members count
                    string pendingMembersQuery = "SELECT COUNT(*) FROM Users WHERE AccountStatus = 'Pending'";
                    SqlCommand pendingMembersCommand = new SqlCommand(pendingMembersQuery, connection);
                    int pendingMembers = (int)pendingMembersCommand.ExecuteScalar();
                    pendingMembersLabel.Text = pendingMembers.ToString();

                    // Get daily transactions count
                    DateTime today = DateTime.Today;
                    string dailyTransactionsQuery = "SELECT COUNT(*) FROM Transactions WHERE CAST(TransactionDate AS DATE) = @Today";
                    SqlCommand dailyTransactionsCommand = new SqlCommand(dailyTransactionsQuery, connection);
                    dailyTransactionsCommand.Parameters.AddWithValue("@Today", today);
                    int dailyTransactions = (int)dailyTransactionsCommand.ExecuteScalar();
                    dailyTransactionsLabel.Text = dailyTransactions.ToString();

                    // Get total transactions count
                    string totalTransactionsQuery = "SELECT COUNT(*) FROM Transactions";
                    SqlCommand totalTransactionsCommand = new SqlCommand(totalTransactionsQuery, connection);
                    int totalTransactions = (int)totalTransactionsCommand.ExecuteScalar();
                    totalTransactionsLabel.Text = totalTransactions.ToString();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred
                Console.WriteLine("Exception during statistics retrieval: " + ex.Message);
            }
        }
    }
}
