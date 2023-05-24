using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Z_Wallet
{
    public partial class Cash_In : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["AccountNumber"] == null)
            {
                Response.Redirect("Default.aspx"); // Redirect to the login page
            }
            else
            {
                // Retrieve and display the user's account information
                if (!IsPostBack)
                {
                    int accountNumber = (int)Session["AccountNumber"];
                    DisplayAccountInformation(accountNumber);
                }
            }
        }

        private void DisplayAccountInformation(int accountNumber)
        {
            DataTable accountData = GetUserAccountData(accountNumber);

            if (accountData.Rows.Count > 0)
            {
                DataRow row = accountData.Rows[0];
                lblCurrentBalance.Text = row["CurrentBalance"].ToString();
            }
        }

        private DataTable GetUserAccountData(int AccountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            DataTable accountData = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(accountData);
            }

            return accountData;
        }

        protected void btnCashin_Click(object sender, EventArgs e)
        {
            // Get the account number from the session
            int accountNumber = (int)Session["AccountNumber"];

            // Get the cash in amount entered by the user
            decimal cashInAmount = Convert.ToDecimal(depositAmount.Text);

            // Update the current balance in the database
            bool success = UpdateCurrentBalance(accountNumber, cashInAmount);

            if (success)
            {
                // Display the updated balance and total cash-in
                DisplayAccountInformation(accountNumber);

                lblSuccessMessage.Visible = true;
                lblSuccessMessage.Text = "Cash-in was successfully added to your account.";
                lblErrorMessage.Visible = false;
            }
        }

        private bool UpdateCurrentBalance(int accountNumber, decimal cashInAmount)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Retrieve the current balance and total cash-in amount
                decimal currentBalance = GetCurrentBalance(accountNumber);
                decimal totalCashIn = GetTotalCashIn(accountNumber);

                // Check if the cash-in amount exceeds the credit limit
                if (currentBalance + cashInAmount > 50000)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Account credit amount cannot exceed 50,000 PHP.";
                    return false; // Cash-in exceeds the credit limit
                }

                // Update the current balance and total cash-in amount in the database
                string query = "UPDATE Users SET CurrentBalance = CurrentBalance + @CashInAmount, TotalCashIn = @TotalCashInAmount WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CashInAmount", cashInAmount);
                command.Parameters.AddWithValue("@TotalCashInAmount", totalCashIn + cashInAmount);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        private decimal GetCurrentBalance(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CurrentBalance FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    throw new Exception("Current balance not found for the specified account.");
                }
            }
        }

        private decimal GetTotalCashIn(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TotalCashIn FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    throw new Exception("Total cash-in not found for the specified account.");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Clear the cash-in amount textbox
            depositAmount.Text = "";

            // Hide success and error messages
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;
        }
    }
}
