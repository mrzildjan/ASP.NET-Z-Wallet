using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
                lblCurrentBalance.Text = row["CurrentBalance"].ToString();
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
            int accountNumber = (int)Session["AccountNumber"];

            // Get the receiver's account number and send amount entered by the user
            int receiverAccountNumberValue;
            if (!int.TryParse(receiverAccountNumber.Text, out receiverAccountNumberValue))
            {
                // Display an error message or handle the invalid receiver account number input
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid receiver account number. Please enter a valid account number.";
                return;
            }

            decimal sendAmountValue;
            if (!decimal.TryParse(sendAmount.Text, out sendAmountValue))
            {
                // Display an error message or handle the invalid send amount input
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid send amount. Please enter a valid amount.";
                return;
            }

            // Check if the receiver account number exists
            bool receiverAccountExists = CheckAccountExists(receiverAccountNumberValue);

            if (!receiverAccountExists)
            {
                // Display an error message indicating that the receiver's account was not found
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "The receiver's account was not found.";
                return;
            }

            // Verify the password (You can replace this with your own password verification logic)
            string enteredPassword = password.Text;
            bool passwordVerified = VerifyPassword(accountNumber, enteredPassword);

            if (!passwordVerified)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid password. Please try again.";
                return;
            }

            // Check if the receiver account number is the same as the sender's account number
            if (accountNumber == receiverAccountNumberValue)
            {
                // Display an error message indicating that sending money to own account is not allowed
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "You cannot send money to your own account.";
                return;
            }

            bool success = UpdateCurrentBalance(accountNumber, receiverAccountNumberValue, sendAmountValue);

            if (success)
            {
                // Display the updated balance and success message
                lblErrorMessage.Visible = false;
                lblSuccessMessage.Visible = true;
                lblSuccessMessage.Text = "Money successfully sent.";
                DisplayAccountInformation(accountNumber);
            }
            else
            {
                // Display an error message indicating insufficient funds or that the receiver's new balance would exceed the limit
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Insufficient funds or the receiver's credit amount would exceed 50,000 PHP. Please enter valid details.";
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
            int accountNumber = (int)Session["AccountNumber"];

            // Get the receiver's account number and send amount entered by the user
            int receiverAccountNumberValue;
            if (!int.TryParse(receiverAccountNumber.Text, out receiverAccountNumberValue))
            {
                // Display an error message or handle the invalid receiver account number input
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid receiver account number. Please enter a valid account number.";
                return;
            }

            decimal sendAmountValue;
            if (!decimal.TryParse(sendAmount.Text, out sendAmountValue))
            {
                // Display an error message or handle the invalid send amount input
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid send amount. Please enter a valid amount.";
                return;
            }

            // Verify the password (You can replace this with your own password verification logic)
            string enteredPassword = password.Text;
            bool passwordVerified = VerifyPassword(accountNumber, enteredPassword);

            if (passwordVerified)
            {
                // Check if the receiver account number is the same as the sender's account number
                if (accountNumber == receiverAccountNumberValue)
                {
                    // Display an error message indicating that sending money to own account is not allowed
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "You cannot send money to your own account.";
                    return;
                }

                // Check if the receiver account number exists
                bool receiverAccountExists = CheckAccountExists(receiverAccountNumberValue);

                if (!receiverAccountExists)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Invalid receiver account number. Please enter a valid account number.";
                    return;
                }

                // Update the current balance in the database
                bool success = UpdateCurrentBalance(accountNumber, receiverAccountNumberValue, sendAmountValue);

                if (success)
                {
                    // Display the updated balance
                    DisplayAccountInformation(accountNumber);

                    lblSuccessMessage.Visible = true;
                    lblSuccessMessage.Text = "Money was successfully sent.";
                    lblErrorMessage.Visible = false;
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Insufficient funds or invalid receiver account number. Please enter valid details.";
                }
            }
            else
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid password. Please try again.";
            }

            // Hide the password verification modal using JavaScript/jQuery
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePasswordModal", "$('#passwordModal').modal('hide');", true);
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

        private bool UpdateCurrentBalance(int senderAccountNumber, int receiverAccountNumber, decimal sendAmount)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Retrieve the current balance of the sender
                decimal senderCurrentBalance = GetCurrentBalance(senderAccountNumber);

                // Check if the sender has sufficient funds
                if (sendAmount > senderCurrentBalance)
                {
                    return false; // Insufficient funds
                }

                // Retrieve the current balance of the receiver
                decimal receiverCurrentBalance = GetCurrentBalance(receiverAccountNumber);

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
                    // Execute the update commands within a transaction
                    updateSenderCommand.Transaction = transaction;
                    updateSenderCommand.ExecuteNonQuery();

                    updateReceiverCommand.Transaction = transaction;
                    updateReceiverCommand.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
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
