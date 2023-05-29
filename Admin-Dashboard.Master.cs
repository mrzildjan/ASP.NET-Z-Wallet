using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Admin_Dashboard : System.Web.UI.MasterPage
    {
        private string firstName;
        private string lastName;
        private string avatarUrl;

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
                    LoadUserInfo();
                    SetUserFullName();
                    SetUserAvatar();
                }
            }
        }

        private void LoadUserInfo()
        {
            if (Session["FirstName"] != null && Session["LastName"] != null)
            {
                firstName = Session["FirstName"].ToString();
                lastName = Session["LastName"].ToString();
            }
        }

        protected void SetUserAvatar()
        {
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                userAvatarImage.Src = avatarUrl;
            }
            else
            {
                // Set the default image URL if avatar URL is null or empty
                userAvatarImage.Src = "/Content/assets/images/user.png";
            }
        }

        protected void SetUserFullName()
        {
            userFullNameLabel.Text = $"{firstName} {lastName}";
            LoginNameLabel.Text = $"{firstName} {lastName}";
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Default.aspx");
        }
    }
}
