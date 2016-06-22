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
            fmu.IsApproved = bool.Parse(dt.Rows[0]["approved"].ToString());

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

    public bool ValidateUser(string username, string email)
    {
        try
        {
            string[] parameters = {
                username,
                email
            };

            int res;
            int.TryParse(bc.ExecuteQueryScalar("f_user_validate", parameters).ToString(), out res);

            if (res == 0) return false;
            else return true;
        }
        catch (Exception ex)
        {
            Logger.WriteToLog(String.Format("Ошибка валидации пользователя: {0}", ex.Message));
            return false;
        }
    }

    public int CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
    {
        try
        {
            int intBoolApproved = (isApproved ? 1 : 0);

            string[] parameters = {
                username,
                password,
                email,
                passwordQuestion,
                passwordAnswer,
                intBoolApproved.ToString(),
                providerUserKey.ToString()
            };

            int ret;
            int.TryParse(bc.ExecuteQueryScalar("f_user_iu", parameters).ToString(), out ret);

            return ret;
        }
        catch (Exception ex)
        {
            Logger.WriteToLog(String.Format("Ошибка регистрации пользователя: {0}", ex.Message));
            return 0;
        }
    }

    public int UpdateUser()
    {
        try
        {
            int intBoolApproved = (_CurrentUser.IsApproved ? 1 : 0);

            string[] parameters = {
                _CurrentUser.UserName,
                _CurrentUser.Password,
                _CurrentUser.Email,
                _CurrentUser.PasswordQuestion,
                _CurrentUser.PasswordAnswer,
                intBoolApproved.ToString(),
                _CurrentUser.LastLogInDate.ToString(),
                _CurrentUser.UserID.ToString()
            };

            int res;
            int.TryParse(bc.ExecuteQueryScalar("f_user_iu", parameters).ToString(), out res);
            return res;
        }
        catch (Exception ex)
        {
            Logger.WriteToLog(String.Format("Ошибка обновления пользователя: {0}", ex.Message));
            return 0;
        }
    }

    public int UpdateUser(FMUser user)
    {
        try
        {
            if (_UserReceived)
            {
                if (!user.UserID.Equals(_CurrentUser.UserID))
                {
                    throw new Exception("Нельзя обновить данные пользователя отличного от текущего!");
                }
                else
                {
                    return UpdateUser();
                }
            }

            int intBoolApproved = (user.IsApproved ? 1 : 0);

            string[] parameters = {
                user.UserName,
                user.Password,
                user.Email,
                user.PasswordQuestion,
                user.PasswordAnswer,
                intBoolApproved.ToString(),
                user.LastLogInDate.ToString(),
                user.UserID.ToString()
            };

            int res;
            int.TryParse(bc.ExecuteQueryScalar("f_user_iu", parameters).ToString(), out res);
            return res;
        }
        catch (Exception ex)
        {
            Logger.WriteToLog(String.Format("Ошибка обновления пользователя: {0}", ex.Message));
            return 0;
        }
    }

    public int UpdateOnLogin(FMUser user)
    {
        try
        {
            string[] parameters = {
                
            };

            int ret;
            int.TryParse(bc.ExecuteQueryScalar("f_user_iu", parameters).ToString(), out ret);

            return ret;
        }
        catch (Exception ex)
        {
            Logger.WriteToLog(String.Format("Ошибка обновления пользователя: {0}", ex.Message));
            return 0;
        }
    }
}