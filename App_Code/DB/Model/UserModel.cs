using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballManager;
using FootballManager.Database;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for UserModel
/// </summary>
public class UserModel
{
    private BaseConnector bc = null;
    private static bool _UserReceived = false;
    public static FMUser _CurrentUser = null;

    public UserModel()
    {
        bc = new PGConnector(Global.DefaultConnectionString);

        //
        // TODO: Add constructor logic here
        //
    }

    public UserModel(BaseConnector _connector)
    {
        bc = _connector;
    }

    public FMUser GetUser(string userName, Guid userID, bool isOnline)
    {
        try
        {
            string[] parameters = {
                userName,
                userID.ToString(),
                isOnline.ToString()
            };
            DataSet ds = bc.ExecuteQuery("f_user_g", parameters);

            DataTable dt = ds.Tables[0];

            FMUser fmu = new FMUser();
            fmu.UserID = Guid.Parse(dt.Rows[0]["userID"].ToString());
            fmu.UserName = dt.Rows[0]["userName"].ToString();
            fmu.Email = dt.Rows[0]["email"].ToString();
            fmu.Password = dt.Rows[0]["password"].ToString();
            fmu.PasswordAnswer = dt.Rows[0]["passwordAnswer"].ToString();
            fmu.PasswordQuestion = dt.Rows[0]["passwordQuestion"].ToString();
            fmu.CreationDate = DateTime.Parse(dt.Rows[0]["dtcreate"].ToString());
            fmu.LastLogInDate = DateTime.Parse(dt.Rows[0]["dtlastlogin"].ToString());
            fmu.LastPasswordChangeTime = DateTime.Parse(dt.Rows[0]["dtlastpasswordchange"].ToString());

            _CurrentUser = fmu;

            _UserReceived = true;

            return _CurrentUser;
        }
        catch (Exception ex)
        {
            _UserReceived = false;
            _CurrentUser = null;
            Logger.WriteToLog(String.Format("Ошибка получения пользователя {0}", ex.Message));
            return _CurrentUser;
        }
    }

    public int CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Logger.WriteToLog(String.Format("Ошибка регистрации пользователя: {0}", ex.Message));
        }
        throw new NotImplementedException("Work in progress");
    }
}