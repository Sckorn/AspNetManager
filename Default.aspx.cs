using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FootballManager.Database;
using System.Data;
using System.Data.Common;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PGConnector db = new PGConnector(System.Configuration.ConfigurationManager.ConnectionStrings["MembershipConnectionPgsql"].ToString());
            DataSet ds = db.ExecuteQuery("f_country_g", new string[] { "AFG" });

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}