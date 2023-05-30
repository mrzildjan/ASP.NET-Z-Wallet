using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Manage_Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMembers();
            }
        }

        private void BindMembers()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZILD\OneDrive\Documents\GitHub\Z-Wallet\App_Data\Z-Wallet.mdf;Integrated Security=True";
            string query = "SELECT FirstName, LastName, Email, PhoneNumber, AccountStatus FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Member> membersList = new List<Member>();

                    while (reader.Read())
                    {
                        Member member = new Member();
                        member.FullName = $"{reader["FirstName"]} {reader["LastName"]}";
                        member.Email = reader["Email"].ToString();
                        member.PhoneNumber = reader["PhoneNumber"].ToString();
                        member.Status = reader["AccountStatus"].ToString();
                        member.StatusBadgeClass = GetStatusBadgeClass(member.Status);

                        membersList.Add(member);
                    }

                    reader.Close();
                    connection.Close();

                    MembersList = membersList;
                }
            }
        }

        protected List<Member> MembersList;

        private string GetStatusBadgeClass(string status)
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

    public class Member
    {
        public string FullName;
        public string Email;
        public string PhoneNumber;
        public string Status;
        public string StatusBadgeClass;
    }
}
