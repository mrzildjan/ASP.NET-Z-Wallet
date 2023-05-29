using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Z_Wallet
{
    public partial class Admin : System.Web.UI.Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the admin is logged in
            if (Session["Email"] == null && Session["FirstName"] == null && Session["LastName"] == null)
            {
                // Admin is not logged in, redirect to login page
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Admin is logged in
                string adminEmail = Session["Email"].ToString();
                DisplayAdminInfo(adminEmail);
                DisplayStatistics();
            }
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

                    // Get total users count
                    string usersQuery = "SELECT COUNT(*) FROM Users";
                    SqlCommand usersCommand = new SqlCommand(usersQuery, connection);
                    int totalUsers = (int)usersCommand.ExecuteScalar();
                    totalUsersLabel.Text = totalUsers.ToString();

                    // Get active members count
                    string activeMembersQuery = "SELECT COUNT(*) FROM Users WHERE isDeactivated = 'Active'";
                    SqlCommand activeMembersCommand = new SqlCommand(activeMembersQuery, connection);
                    int activeMembers = (int)activeMembersCommand.ExecuteScalar();
                    activeMembersLabel.Text = activeMembers.ToString();

                    // Get inactive members count
                    string inactiveMembersQuery = "SELECT COUNT(*) FROM Users WHERE isDeactivated = 'Inactive'";
                    SqlCommand inactiveMembersCommand = new SqlCommand(inactiveMembersQuery, connection);
                    int inactiveMembers = (int)inactiveMembersCommand.ExecuteScalar();
                    inactiveMembersLabel.Text = inactiveMembers.ToString();

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
