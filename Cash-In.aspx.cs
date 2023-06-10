using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Z_Wallet
{
    public partial class Cash_In : Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;
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
                lblCurrentBalance.Text = string.Format("₱ {0:N2}", Convert.ToDecimal(row["CurrentBalance"]));
            }
        }

        private DataTable GetUserAccountData(int AccountNumber)
        {

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

        private void StoreTransaction(int accountNumber, string transactionType, string transactionSender, string transactionReceiver, decimal transactionAmount)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Transactions (AccountNumber, TransactionType, TransactionSender, TransactionReceiver, TransactionAmount, TransactionDate) " +
                               "VALUES (@AccountNumber, @TransactionType, @TransactionSender, @TransactionReceiver, @TransactionAmount, @TransactionDate)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@TransactionType", transactionType);
                command.Parameters.AddWithValue("@TransactionSender", transactionSender);
                command.Parameters.AddWithValue("@TransactionReceiver", transactionReceiver);
                command.Parameters.AddWithValue("@TransactionAmount", transactionAmount);
                command.Parameters.AddWithValue("@TransactionDate", TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Global.CustomTimeZone));

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void btnCashin_Click(object sender, EventArgs e)
        {
            // Get the account number from the session
            int accountNumber = (int)Session["AccountNumber"];

            // Check if the account is deactivated
            bool isDeactivated = IsAccountDeactivated(accountNumber);
            bool isVerified = AccountStatus(accountNumber);

            // Get the cash in amount entered by the user
            decimal cashInAmount = Convert.ToDecimal(depositAmount.Text);

            if (cashInAmount % 100 != 0)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Amount should divisible by 100.00";
                lblSuccessMessage.Visible = false;
            }
            else
            {
                if (isVerified)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Account is not Verified. Please complete Verification Form.";

                    lblSuccessMessage.Visible = false;
                }
                else
                {
                    if (isDeactivated)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Account is deactivated. Cash-out is not allowed.";

                        lblSuccessMessage.Visible = false;
                    }
                    else
                    {
                        // Check if the cash-in amount is greater than zero
                        if (cashInAmount > 0)
                        {
                            // Update the current balance in the database
                            bool success = UpdateCurrentBalance(accountNumber, cashInAmount);

                            if (success)
                            {
                                // Store the transaction information in the database
                                StoreTransaction(accountNumber, "Cash-In", "", "", cashInAmount);

                                // Display the updated balance and total cash-in
                                DisplayAccountInformation(accountNumber);

                                lblSuccessMessage.Visible = true;
                                lblSuccessMessage.Text = "Cash-in was successfully added to your account.";
                                lblErrorMessage.Visible = false;
                            }
                            else
                            {
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = "Account credit amount cannot exceed 50,000 PHP.";

                                lblSuccessMessage.Visible = false;
                            }
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "Invalid cash-in amount. Please enter a positive value.";

                            lblSuccessMessage.Visible = false;
                        }
                    }
                }
            }
        }

        private bool AccountStatus(int accountNumber)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AccountStatus FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string accountStatus = result.ToString();
                    return accountStatus == "Unverified" || accountStatus == "Pending" || accountStatus == "Denied" || accountStatus == "Suspended";
                }
                else
                {
                    throw new Exception("Account not found for the specified account number.");
                }
            }
        }

        private bool IsAccountDeactivated(int accountNumber)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT isDeactivated FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string accountStatus = result.ToString();
                    return accountStatus == "Inactive" || accountStatus == "Deactivated";
                }
                else
                {
                    throw new Exception("Account not found for the specified account number.");
                }
            }
        }
        private bool UpdateCurrentBalance(int accountNumber, decimal cashInAmount)
        {

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

                    lblSuccessMessage.Visible = false;
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
