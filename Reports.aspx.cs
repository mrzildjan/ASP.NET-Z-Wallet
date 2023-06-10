using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Reports : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    fromDate.Attributes["max"] = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Global.CustomTimeZone).ToString("yyyy-MM-dd");
                    toDate.Attributes["max"] = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Global.CustomTimeZone).ToString("yyyy-MM-dd");
                    BindTransactions();
                }
            }
        }

        protected void BindTransactions(DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                int accountNumber = (int)Session["AccountNumber"];
                DataTable transactions;

                if (fromDate.HasValue && toDate.HasValue)
                {
                    transactions = GetFilteredTransactions(accountNumber, fromDate.Value, toDate.Value);
                }
                else
                {
                    transactions = GetTransactions(accountNumber);
                }

                if (transactions.Rows.Count > 0)
                {
                    DataView sortedView = transactions.DefaultView;
                    sortedView.Sort = "TransactionDate DESC";
                    rptTransactions.DataSource = sortedView;
                    rptTransactions.DataBind();
                }
                else
                {
                    rptTransactions.DataSource = null;
                    rptTransactions.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during data retrieval
                Response.Write("<script>window.alert('An error occurred while retrieving transactions.')</script>");
            }
        }


        private DataTable GetTransactions(int accountNumber)
        {
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Transactions WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable transactions = new DataTable();

                connection.Open();
                adapter.Fill(transactions);
                connection.Close();

                // Update transaction ID to previous value - 1 for type "Received Money"
                foreach (DataRow row in transactions.Rows)
                {
                    if (row["TransactionType"].ToString() == "Received Money")
                    {
                        int transactionID = int.Parse(row["TransactionID"].ToString());
                        row["TransactionID"] = transactionID - 1;
                    }
                }

                return transactions;
            }
        }

        private DataTable GetFilteredTransactions(int accountNumber, DateTime fromDate, DateTime toDate)
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Transactions WHERE AccountNumber = @AccountNumber AND TransactionDate >= @FromDate AND TransactionDate <= DATEADD(day, 1, @ToDate)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@FromDate", fromDate);
                command.Parameters.AddWithValue("@ToDate", toDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable transactions = new DataTable();

                connection.Open();
                adapter.Fill(transactions);
                connection.Close();

                return transactions;
            }
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            DateTime fromDateValue;
            DateTime toDateValue;

            if (DateTime.TryParse(fromDate.Text, out fromDateValue) && DateTime.TryParse(toDate.Text, out toDateValue))
            {
                BindTransactions(fromDateValue, toDateValue);

                lblFilterSuccessMessage.Text = "Successfully filtered dates.";
                lblFilterSuccessMessage.Visible = true;

                lblFilterErrorMessage.Visible = false;
            }
            else
            {
                lblFilterErrorMessage.Text = "Please filter a valid dates.";
                lblFilterErrorMessage.Visible = true;

                lblFilterSuccessMessage.Visible = false;
            }
        }

        protected void btnResetFilter_Click(object sender, EventArgs e)
        {
            fromDate.Text = "";
            toDate.Text = "";

            lblFilterErrorMessage.Visible = false;
            lblFilterSuccessMessage.Visible = false;

            BindTransactions();
        }
    }
}
