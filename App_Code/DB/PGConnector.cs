using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballManager.Database;

/// <summary>
/// Summary description for PGConnector
/// </summary>
public class PGConnector : BaseConnector
{
    public PGConnector() : base()
    {
        _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MembershipConnectionPgsql"].ToString();
    }

    public PGConnector(string _connection) : base(_connection)
    {
        
    }
}