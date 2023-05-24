using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Z_Wallet
{
    public partial class Cash_Out : Page
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

        protected void btnCashOut_Click(object sender, EventArgs e)
        {
            // Get the account number from the session
            int accountNumber = (int)Session["AccountNumber"];

            // Get the cash out amount entered by the user
            decimal cashOutAmount = Convert.ToDecimal(withdrawAmount.Text);

            // Show the password verification modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showPasswordModal", "$('#passwordModal').modal('show');", true);
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

            // Get the cash out amount entered by the user
            decimal cashOutAmount = Convert.ToDecimal(withdrawAmount.Text);

            // Verify the password (You can replace this with your own password verification logic)
            string enteredPassword = password.Text;
            bool passwordVerified = VerifyPassword(accountNumber, enteredPassword);

            if (passwordVerified)
            {
                // Update the current balance and total cash-out in the database
                bool success = UpdateCurrentBalance(accountNumber, cashOutAmount);

                if (success)
                {
                    // Display the updated balance and total cash-out
                    DisplayAccountInformation(accountNumber);

                    lblSuccessMessage.Visible = true;
                    lblSuccessMessage.Text = "Cash-out was successfully deducted from your account.";
                    lblErrorMessage.Visible = false;
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Insufficient funds. Please enter a valid cash-out amount.";
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
        private bool UpdateCurrentBalance(int accountNumber, decimal cashOutAmount)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Retrieve the current balance and total cash-out amount
                decimal currentBalance = GetCurrentBalance(accountNumber);
                decimal totalCashOut = GetTotalCashOut(accountNumber);

                // Check if the cash-out amount exceeds the current balance
                if (cashOutAmount > currentBalance)
                {
                    return false; // Insufficient funds
                }

                // Update the current balance and total cash-out amount in the database
                string query = "UPDATE Users SET CurrentBalance = CurrentBalance - @CashOutAmount, TotalCashOut = @TotalCashOutAmount WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CashOutAmount", cashOutAmount);
                command.Parameters.AddWithValue("@TotalCashOutAmount", totalCashOut + cashOutAmount);
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

        private decimal GetTotalCashOut(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TotalCashOut FROM Users WHERE AccountNumber = @AccountNumber";

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
                    throw new Exception("Total cash-out not found for the specified account.");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Clear the cash-out amount textbox
            withdrawAmount.Text = "";

            // Hide success and error messages
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;
        }
    }
}
