using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JudgeLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Accounts.checkAccount(tbAccountName.Text, tbPassword.Text, 1) == 0)
        {
            lResult.Text = "Yeah";
            this.Session["encryptedname"] = tbAccountName.Text;
            this.Session["password"] = tbPassword.Text;
            Response.Redirect("~/Judge/Brain.aspx");
        }
        else
        {
            lResult.Text = "No";
        }
    }
}