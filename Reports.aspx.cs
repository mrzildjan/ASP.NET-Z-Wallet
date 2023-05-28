using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Z_Wallet
{
    public partial class Reports : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    BindTransactions();
                }
            }
        }

        protected void BindTransactions()
        {
            try
            {
                int accountNumber = (int)Session["AccountNumber"];
                DataTable transactions = GetTransactions(accountNumber);

                if (transactions.Rows.Count > 0)
                {
                    DataView sortedView = transactions.DefaultView;
                    sortedView.Sort = "TransactionDate DESC";
                    rptTransactions.DataSource = sortedView;
                    rptTransactions.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during data retrieval
                Response.Write("<script>window.alert('An error occurred while retrieving transactions.')<script/>");
            }
        }

        private DataTable GetTransactions(int accountNumber)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Transactions WHERE AccountNumber = @AccountNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable transactions = new DataTable();
                adapter.Fill(transactions);

                return transactions;
            }
        }
    }
}
