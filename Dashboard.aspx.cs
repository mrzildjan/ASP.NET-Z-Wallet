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
    public partial class Dashboard : System.Web.UI.Page
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
                    // Retrieve and display the user's account information
                    int AccountNumber = (int)Session["AccountNumber"];
                    DataTable accountData = GetUserAccountData(AccountNumber);

                    if (accountData.Rows.Count > 0)
                    {
                        DataRow row = accountData.Rows[0];
                        lblAccountNumber.Text = row["AccountNumber"].ToString();
                        lblName.Text = string.Format("{0} {1}", row["FirstName"].ToString(), row["LastName"].ToString());
                        lblDateRegistered.Text = ((DateTime)row["SignUpDateTime"]).ToString("MMM dd, yyyy");
                        lblCurrentBalance.Text = row["CurrentBalance"].ToString();
                        lblTotalSendMoney.Text = row["TotalSendMoney"].ToString();
                        lblTotalCashIn.Text = row["TotalCashIn"].ToString();
                        lblTotalCashout.Text = row["TotalCashOut"].ToString();
                    }
                }
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
    }
}
