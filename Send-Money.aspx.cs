using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Send_Money : System.Web.UI.Page
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
                lblCurrentBalance.Text = string.Format("₱ {0:N2}", Convert.ToDecimal(row["CurrentBalance"]));
            }
        }

        private DataTable GetUserAccountData(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            DataTable accountData = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(accountData);
            }

            return accountData;
        }

        protected void btnSendMoney_Click(object sender, EventArgs e)
        {
            // Get the account number from the session
            int senderAccountNumber = (int)Session["AccountNumber"];

            // Get the receiver's account number and send amount entered by the user
            int receiverAccountNumberValue;
            if (!int.TryParse(receiverAccountNumber.Text, out receiverAccountNumberValue))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid receiver account number. Please enter a valid account number.";

                lblSuccessMessage.Visible = false;
                return;
            }

            decimal sendAmountValue;
            if (!decimal.TryParse(sendAmount.Text, out sendAmountValue))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid send amount. Please enter a valid amount.";

                lblSuccessMessage.Visible = false;
                return;
            }

            // Check if the receiver account number exists
            bool receiverAccountExists = CheckAccountExists(receiverAccountNumberValue);

            if (!receiverAccountExists)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "The receiver's account was not found.";

                lblSuccessMessage.Visible = false;
                return;
            }

            // Verify the password (You can replace this with your own password verification logic)
            string enteredPassword = password.Text;
            bool passwordVerified = VerifyPassword(senderAccountNumber, enteredPassword);

            if (!passwordVerified)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid password. Please try again.";

                lblSuccessMessage.Visible = false;
                return;
            }

            // Check if the receiver account number is the same as the sender's account number
            if (senderAccountNumber == receiverAccountNumberValue)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "You cannot send money to your own account.";

                lblSuccessMessage.Visible = false;
                return;
            }

            // Perform the money sending operation
            int updateResult = UpdateCurrentBalance(senderAccountNumber, receiverAccountNumberValue, sendAmountValue);

            switch (updateResult)
            {
                case 0: // success
                        // Update the total send money and total receive money in the database
                    bool updateSendSuccess = UpdateTotalSendMoney(senderAccountNumber, sendAmountValue);
                    bool updateReceiveSuccess = UpdateTotalReceiveMoney(receiverAccountNumberValue, sendAmountValue);

                    if (updateSendSuccess && updateReceiveSuccess)
                    {
                        // Display the updated balance
                        DisplayAccountInformation(senderAccountNumber);

                        lblSuccessMessage.Visible = true;
                        lblSuccessMessage.Text = "Money was successfully sent.";
                        lblErrorMessage.Visible = false;
                    }
                    else
                    {
                        // Update failed. Display an error message.
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Money was sent successfully.";
                        lblSuccessMessage.Visible = false;
                    }
                    break;
                case 1: // insufficient funds
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Insufficient funds.";

                    lblSuccessMessage.Visible = false;
                    break;
                case 2: // receiver's credit limit would be exceeded
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "The receiver's credit amount would exceed the limit.";

                    lblSuccessMessage.Visible = false;
                    break;
            }
        }

        private bool CheckAccountExists(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                return result > 0;
            }
        }

        protected void btnCancelPassword_Click(object sender, EventArgs e)
        {
            // Clear the password input
            password.Text = "";

            // Hide the password verification modal using JavaScript/jQuery
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePasswordModal", "$('#passwordModal').modal('hide');", true);
        }

        protected void btnVerifyPassword_Click(object sender, EventArgs e)
        {
            // Get the account number from the session
            int senderAccountNumber = (int)Session["AccountNumber"];

            // Get the receiver's account number and send amount entered by the user
            int receiverAccountNumberValue;
            if (!int.TryParse(receiverAccountNumber.Text, out receiverAccountNumberValue))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid receiver account number. Please enter a valid account number.";

                lblSuccessMessage.Visible = false;
                return;
            }

            decimal sendAmountValue;
            if (!decimal.TryParse(sendAmount.Text, out sendAmountValue))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid send amount. Please enter a valid amount.";

                lblSuccessMessage.Visible = false;
                return;
            }

            // Verify the password (You can replace this with your own password verification logic)
            string enteredPassword = password.Text;
            bool passwordVerified = VerifyPassword(senderAccountNumber, enteredPassword);

            if (passwordVerified)
            {
                bool isVerified = AccountStatus(senderAccountNumber);

                if (isVerified)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Account is not Verified. Please complete Verification Form.";

                    lblSuccessMessage.Visible = false;
                }
                else
                {

                    // Check if the receiver account is verified
                    bool receiverSuspended = AccountSuspended(receiverAccountNumberValue);

                    if (receiverSuspended)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Cannot sent money to a suspended account.";

                        lblSuccessMessage.Visible = false;
                        return;
                    }
                    // Check if the receiver account is verified
                    bool receiverVerified = AccountStatus(receiverAccountNumberValue);

                    if (receiverVerified)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Cannot sent money to a unverified account.";

                        lblSuccessMessage.Visible = false;
                        return;
                    }

                    // Check if the account is deactivated or inactive
                    bool isDeactivated = IsAccountDeactivated(senderAccountNumber);

                    if (isDeactivated)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Account is deactivated. Send-money is not allowed.";

                        lblSuccessMessage.Visible = false;
                    }
                    else
                    {
                        // Check if the receiver account is deactivated
                        bool receiverDeactivated = IsAccountDeactivated(receiverAccountNumberValue);

                        if (receiverDeactivated)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "Cannot send money to a deactivated account.";

                            lblSuccessMessage.Visible = false;
                            return;
                        }

                        // Check if the receiver account number is the same as the sender's account number
                        if (senderAccountNumber == receiverAccountNumberValue)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "You cannot send money to your own account.";

                            lblSuccessMessage.Visible = false;
                            return;
                        }

                        // Check if the receiver account number exists
                        bool receiverAccountExists = CheckAccountExists(receiverAccountNumberValue);

                        if (!receiverAccountExists)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "Invalid receiver account number. Please enter a valid account number.";

                            lblSuccessMessage.Visible = false;
                            return;
                        }

                        // Update the current balance in the database
                        int updateResult = UpdateCurrentBalance(senderAccountNumber, receiverAccountNumberValue, sendAmountValue);

                        switch (updateResult)
                        {
                            case 0:
                                // Update the total send money and total receive money in the database
                                bool updateSendSuccess = UpdateTotalSendMoney(senderAccountNumber, sendAmountValue);
                                bool updateReceiveSuccess = UpdateTotalReceiveMoney(receiverAccountNumberValue, sendAmountValue);

                                if (updateSendSuccess && updateReceiveSuccess)
                                {
                                    // Display the updated balance
                                    DisplayAccountInformation(senderAccountNumber);

                                    lblSuccessMessage.Visible = true;
                                    lblSuccessMessage.Text = "Money was successfully sent.";
                                    lblErrorMessage.Visible = false;
                                }
                                else
                                {
                                    // Update failed. Display an error message.
                                    lblErrorMessage.Visible = true;
                                    lblErrorMessage.Text = "Money was sent successfully.";
                                    lblSuccessMessage.Visible = false;
                                }
                                break;
                            case 1:
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = "Insufficient funds. Please enter a valid amount.";
                                lblSuccessMessage.Visible = false;
                                break;
                            case 2:
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = "The transaction would exceed the receiver's credit limit.";
                                lblSuccessMessage.Visible = false;
                                break;
                        }
                    }
                }
            }
            else
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid password. Please try again.";
                lblSuccessMessage.Visible = false;
            }

            // Hide the password verification modal using JavaScript/jQuery
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePasswordModal", "$('#passwordModal').modal('hide');", true);
        }

        private bool AccountSuspended(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

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
                    return accountStatus == "Suspended";
                }
                else
                {
                    throw new Exception("Account not found for the specified account number.");
                }
            }
        }
        private bool AccountStatus(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

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
                    return accountStatus == "Unverified" || accountStatus == "Pending" || accountStatus == "Denied";
                }
                else
                {
                    throw new Exception("Account not found for the specified account number.");
                }
            }
        }

        private bool IsAccountDeactivated(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

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
                    // Instead of throwing an exception, set a label text to indicate that the account was not found.
                    lblErrorMessage.Text = "Account not found for the specified account number.";
                    lblErrorMessage.Visible = true;
                    return false;
                }
            }
        }

        private bool VerifyPassword(int accountNumber, string password)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE AccountNumber = @AccountNumber AND Password = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                return result > 0;
            }
        }

        private void StoreTransaction(int senderAccountNumber, int receiverAccountNumber, decimal sendAmount)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Transactions (AccountNumber, TransactionType, TransactionSender, TransactionReceiver, TransactionAmount, TransactionDate) " +
                               "VALUES (@AccountNumber, @TransactionType, @TransactionSender, @TransactionReceiver, @TransactionAmount, @TransactionDate);" +
                               "INSERT INTO Transactions (AccountNumber, TransactionType, TransactionSender, TransactionReceiver, TransactionAmount, TransactionDate) " +
                               "VALUES (@ReceiverAccountNumber, @ReceivedTransactionType, @ReceivedTransactionSender, @ReceivedTransactionReceiver, @TransactionAmount, @TransactionDate)";

                string senderName = GetAccountName(senderAccountNumber);
                string receiverName = GetAccountName(receiverAccountNumber);

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", senderAccountNumber);
                command.Parameters.AddWithValue("@TransactionType", "Send Money");
                command.Parameters.AddWithValue("@TransactionSender", senderName);
                command.Parameters.AddWithValue("@TransactionReceiver", receiverName);
                command.Parameters.AddWithValue("@ReceiverAccountNumber", receiverAccountNumber);
                command.Parameters.AddWithValue("@ReceivedTransactionType", "Received Money");
                command.Parameters.AddWithValue("@ReceivedTransactionSender", senderName);
                command.Parameters.AddWithValue("@ReceivedTransactionReceiver", receiverName);
                command.Parameters.AddWithValue("@TransactionAmount", sendAmount);
                command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private string GetAccountName(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT FirstName, LastName FROM Users WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    return firstName + " " + lastName;
                }
                else
                {
                    throw new Exception("Account not found.");
                }
            }
        }

        private int UpdateCurrentBalance(int senderAccountNumber, int receiverAccountNumber, decimal sendAmount)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Retrieve the current balance of the sender
                decimal senderCurrentBalance = GetCurrentBalance(senderAccountNumber);

                // Check if the sender has sufficient funds
                if (sendAmount > senderCurrentBalance)
                {
                    return 1; // Insufficient funds
                }

                // Retrieve the current balance of the receiver
                decimal receiverCurrentBalance = GetCurrentBalance(receiverAccountNumber);

                // Check if receiver's balance exceeds limit after transaction
                if (receiverCurrentBalance + sendAmount > 50000)
                {
                    return 2; // Receiver's balance will exceed limit
                }

                // Update the current balances in the database
                string updateSenderQuery = "UPDATE Users SET CurrentBalance = CurrentBalance - @SendAmount WHERE AccountNumber = @SenderAccountNumber";
                string updateReceiverQuery = "UPDATE Users SET CurrentBalance = CurrentBalance + @SendAmount WHERE AccountNumber = @ReceiverAccountNumber";

                SqlCommand updateSenderCommand = new SqlCommand(updateSenderQuery, connection);
                updateSenderCommand.Parameters.AddWithValue("@SendAmount", sendAmount);
                updateSenderCommand.Parameters.AddWithValue("@SenderAccountNumber", senderAccountNumber);

                SqlCommand updateReceiverCommand = new SqlCommand(updateReceiverQuery, connection);
                updateReceiverCommand.Parameters.AddWithValue("@SendAmount", sendAmount);
                updateReceiverCommand.Parameters.AddWithValue("@ReceiverAccountNumber", receiverAccountNumber);

                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    updateSenderCommand.Transaction = transaction;
                    updateSenderCommand.ExecuteNonQuery();

                    updateReceiverCommand.Transaction = transaction;
                    updateReceiverCommand.ExecuteNonQuery();

                    transaction.Commit();

                    // Store the transaction information in the database
                    StoreTransaction(senderAccountNumber, receiverAccountNumber, sendAmount);
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("An error occurred while sending money.", ex);
                }
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

        private bool UpdateTotalSendMoney(int accountNumber, decimal sendAmount)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET TotalSendMoney = ISNULL(TotalSendMoney, 0) + @SendAmount WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SendAmount", sendAmount);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        private bool UpdateTotalReceiveMoney(int accountNumber, decimal receiveAmount)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET TotalReceiveMoney = ISNULL(TotalReceiveMoney, 0) + @ReceiveAmount WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ReceiveAmount", receiveAmount);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Clear the input fields
            receiverAccountNumber.Text = "";
            sendAmount.Text = "";

            // Hide success and error messages
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;
        }
    }
}
