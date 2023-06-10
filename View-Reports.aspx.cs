using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class View_Reports : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Z-WalletConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null && Session["FirstName"] == null && Session["LastName"] == null)
            {
                Response.Redirect("Default.aspx"); // Redirect to the login page
            }
            else
            {
                if (!IsPostBack)
                {
                    BindReports();
                }
            }
        }

        private void BindReports()
        {
            string query = "SELECT AccountNumber, FirstName, LastName, Email, PhoneNumber, AccountStatus FROM Users ORDER BY AccountNumber DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Report> reportsList = new List<Report>();

                    while (reader.Read())
                    {
                        Report report = new Report();
                        report.AccountNumber = Convert.ToInt32(reader["AccountNumber"]);
                        report.FullName = $"{reader["FirstName"]} {reader["LastName"]}";
                        report.Email = reader["Email"].ToString();
                        report.PhoneNumber = reader["PhoneNumber"].ToString();
                        report.Status = reader["AccountStatus"].ToString();
                        report.StatusBadgeClass = GetStatusBadgeClass(report.Status);

                        reportsList.Add(report);
                    }

                    reader.Close();
                    connection.Close();

                    ReportsList = reportsList;
                }
            }
        }

        protected List<Report> ReportsList;

        protected string GetStatusBadgeClass(string status)
        {
            switch (status)
            {
                case "Verified":
                    return "bg-success";
                case "Pending":
                    return "bg-info";
                case "Denied":
                    return "bg-danger";
                case "Suspended":
                    return "bg-warning";
                case "Unverified":
                    return "bg-secondary";
                default:
                    return "bg-secondary";
            }
        }
    }
    public class Report
    {
        public string FullName;
        public string Email;
        public string PhoneNumber;
        public string Status;
        public string StatusBadgeClass;
        public int AccountNumber;
    }
}
