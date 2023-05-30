using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class User_Members : System.Web.UI.Page
    {
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
                    // Set the initial status badge class based on the selected status
                    string selectedStatus = ddlAccountStatus.SelectedValue;
                    string statusBadgeClass = GetStatusBadgeClass(selectedStatus);
                    ddlAccountStatus.CssClass = "status-text " + statusBadgeClass;
                }

                if (Request.QueryString["accountNumber"] != null)
                {
                    string accountNumber = Request.QueryString["accountNumber"];
                    // Use the accountNumber as needed, e.g., fetch member details based on the accountNumber
                }
            }
        }
        protected void ddlAccountStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = ddlAccountStatus.SelectedValue;
            ddlAccountStatus.Text = selectedStatus;

            string statusBadgeClass = GetStatusBadgeClass(selectedStatus);
            ddlAccountStatus.CssClass = "status-text " + statusBadgeClass;
        }

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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve the selected value from the dropdown
            string selectedStatus = ddlAccountStatus.SelectedValue;

            // Perform any necessary actions with the selected status
            // For example, you can update the database or perform some other logic


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Manage-Members.aspx");
        }
    }
}