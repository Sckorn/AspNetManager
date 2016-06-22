using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounts_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
        {
            Path = "~/Script/jquery-2.2.4.min.js",

        });
    }

    protected void MainLoginForm_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (Membership.ValidateUser(MainLoginForm.UserName, MainLoginForm.Password))
        {
            e.Authenticated = true;
        }
        else
        {
            e.Authenticated = false;
        }
    }
}