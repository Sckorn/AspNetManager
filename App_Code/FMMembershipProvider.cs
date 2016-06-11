using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration.Provider;
using FootballManager;
using FootballManager.Database;
using System.Collections.Specialized;

/// <summary>
/// Summary description for FMMembershipProvider
/// </summary>
public class FMMembershipProvider : MembershipProvider
{
    private string _Name;
    private string _FileName;
    private string _ApplicationName;
    private bool _EnablePasswordReset;
    private bool _RequiresQuestionAndAnswer;
    private string _PasswordStrengthRegEx;
    private int _MaxInvalidPasswordAttempts;
    private int _MinRequiredNonAlphanumericChars;
    private int _MinRequiredPasswordLength;
    private MembershipPasswordFormat _PasswordFormat;
    private BaseConnector dbConnection = null;

    public FMMembershipProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override string ApplicationName
    {
        get
        {
            return _ApplicationName;
        }

        set
        {
            _ApplicationName = value;
            dbConnection = null;
        }
    }

    public override bool EnablePasswordReset
    {
        get
        {
            return _EnablePasswordReset;
        }
    }

    public override bool EnablePasswordRetrieval
    {
        get
        {
            if (this._PasswordFormat == MembershipPasswordFormat.Hashed)
                return false;
            else
                return true;
        }
    }

    public override int MaxInvalidPasswordAttempts
    {
        get
        {
            return _MaxInvalidPasswordAttempts;
        }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
        get
        {
            return _MinRequiredNonAlphanumericChars;
        }
    }

    public override int MinRequiredPasswordLength
    {
        get
        {
            return _MinRequiredPasswordLength;
        }
    }

    public override int PasswordAttemptWindow
    {
        get
        {
            return 20;
        }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
        get
        {
            return _PasswordFormat;
        }
    }

    public override string PasswordStrengthRegularExpression
    {
        get
        {
            return _PasswordStrengthRegEx;
        }
    }

    public override bool RequiresQuestionAndAnswer
    {
        get
        {
            return _RequiresQuestionAndAnswer;
        }
    }

    public override bool RequiresUniqueEmail
    {
        get
        {
            return true;
        }
    }

    public override void Initialize(string name, NameValueCollection config)
    {
        if (config == null)
        {
            throw new ArgumentNullException("Config cannot be null");
        }

        if (string.IsNullOrEmpty(name))
        {
            name = "FMMembershipProvider";
        }

        if (string.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "Db membership provider");
        }

        base.Initialize(name, config);

        _ApplicationName = "Football Manager";
        _EnablePasswordReset = true;
        _PasswordStrengthRegEx = @"[\w| !§$%&/()=\-?\*]*";
        _MaxInvalidPasswordAttempts = 3;
        _MinRequiredNonAlphanumericChars = 1;
        _MinRequiredPasswordLength = 5;
        _RequiresQuestionAndAnswer = true;
        _PasswordFormat = MembershipPasswordFormat.Hashed;

        foreach (string key in config.Keys)
        {
            switch (key.ToLower())
            {
                case "name":
                    _Name = config[key];
                    break;
                case "applicationname":
                    _ApplicationName = config[key];
                    break;
                case "enablepasswordreset":
                    _EnablePasswordReset = bool.Parse(config[key]);
                    break;
                case "passwordstrengthregex":
                    _PasswordStrengthRegEx = config[key];
                    break;
                case "maxinvalidpasswordattempts":
                    _MaxInvalidPasswordAttempts = int.Parse(config[key]);
                    break;
                case "minrequirednonalphanumericchars":
                    _MinRequiredNonAlphanumericChars = int.Parse(config[key]);
                    break;
                case "minrequiredpasswordlength":
                    _MinRequiredPasswordLength = int.Parse(config[key]);
                    break;
                case "passwordformat":
                    _PasswordFormat = (MembershipPasswordFormat)Enum.Parse(typeof(MembershipPasswordFormat), config[key]);
                    break;
                case "requiresquestionandanswer":
                    _RequiresQuestionAndAnswer = bool.Parse(config[key]);
                    break;
            }
        }
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Logger.WriteToLog(String.Format("Ошибка создания пользователя: {0}", ex.Message));
            throw;
        }
        throw new NotImplementedException();
    }

    private bool ValidateUser(string userName, string email, Guid excludeKey)
    {
        bool isValid = true;

        throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }

    public override bool ValidateUser(string username, string password)
    {
        throw new NotImplementedException();
    }
}