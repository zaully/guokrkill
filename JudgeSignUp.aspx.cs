using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JudgeSignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Accounts newAccount = new Accounts();
        if (tbAccountName.Text == "" || tbPassword.Text == "")
        {
            return;
        }
        int result = newAccount.registerAccount(this.tbAccountName.Text, this.tbPassword.Text, 1);
        if (result == 0)
        {
            lResult.Text = "Successfully signed up!";
            btnSignUp.Enabled = false;
        }
        else if (result == 1)
        {
            lResult.Text = "We already have the same account name.";
        }
        else
        {
            lResult.Text = "Failed!";
        }
    }
}