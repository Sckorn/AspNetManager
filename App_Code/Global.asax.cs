using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.IO;

/// <summary>
/// Summary description for Global
/// </summary>
/// 
namespace FootballManager
{
    public class Global : HttpApplication
    {
        public static string DefaultFactory
        {
            get; set;
        }

        public static string DefaultConnectionString
        {
            get; set;
        }

        public static PGConnector[] ConnectorsPool = new PGConnector[1];

        protected void Application_Start(object sender, EventArgs e)
        {
            Global.DefaultFactory = System.Configuration.ConfigurationManager.AppSettings["defaultFactory"];
            Global.DefaultConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[System.Configuration.ConfigurationManager.AppSettings["defaultConnectionString"]].ToString();
            Logger.Init();
            // Code that runs on application startup
            Global.ConnectorsPool[0] = new PGConnector(Global.DefaultConnectionString);
            RoutesAdd(RouteTable.Routes);
            Logger.WriteToLog("Приложение запущено!");
        }

        private static void RoutesAdd(RouteCollection routes)
        {
            //routes.Add();
            routes.MapPageRoute("", "Login/", "~/Accounts/Login.aspx");
            routes.MapPageRoute("", "Register/", "~/Accounts/Register.aspx");
            Logger.WriteToLog("Маршруты УРЛ добавлены!");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
    }
}