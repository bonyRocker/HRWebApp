using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRWebApp
{
    public partial class LoginUI1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            alertLogin.Visible = false;
            if (!(string.IsNullOrEmpty(txtUserId.Text) || string.IsNullOrEmpty(txtPassword.Text)))
            {
                if (txtUserId.Text == "makasoft" && txtPassword.Text == "makasoft")
                {
                    Session["CurrentUserId"] = txtUserId.Text;
                    Session["CurrentUserName"] = txtUserId.Text;
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                lblMessage.Text = "Enter User Name or Password";
                alertLogin.Visible = true;
            }
        }
    }
}