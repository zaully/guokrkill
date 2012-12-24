using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Judge_BrainTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["encryptedname"] != null && Session["password"] != null)
            {
                if (Accounts.checkAccount(Session["encryptedname"].ToString(), Session["password"].ToString(), 1) != 0)
                {
                    Response.Redirect("~/JudgeLogin.aspx");
                }
            }
            else
            {
                Response.Redirect("~/JudgeLogin.aspx");
            }
        }
    }
    protected void btn_click(object sender, EventArgs e)
    {
        TextBox1.Text = DateTime.Now.ToShortTimeString();
    }
}