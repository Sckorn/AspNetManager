using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration.Provider;
using FootballManager;
using FootballManager.Database;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

/// <summary>
/// Summary description for FMMembershipProvider
/// </summary>
/// 
namespace FootballManager
{
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
                if (!ValidateUsername(username, email, Guid.Empty))
                {
                    status = MembershipCreateStatus.InvalidUserName;
                    return null;
                }

                base.OnValidatingPassword(new ValidatePasswordEventArgs(username, password, true));

                if (!ValidatePassword(password))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                    return null;
                }

                FMUser user = new FMUser();
                user.UserID = Guid.NewGuid();
                user.UserName = username;
                string salt = string.Empty;
                user.Password = TransformPassword(password, ref salt);
                user.Email = email;
                user.PasswordQuestion = passwordQuestion;
                user.PasswordAnswer = passwordAnswer;
                user.CreationDate = DateTime.Now;
                user.LastLogInDate = DateTime.Now;
                user.LastPasswordChangeTime = DateTime.Now;

                var um = new UserModel();
                int ret = um.CreateUser(user.UserName, user.Password, user.Email, user.PasswordQuestion, user.PasswordAnswer, false, user.UserID);

                if (ret == 0)
                {
                    status = MembershipCreateStatus.ProviderError;
                    return null;
                }
                else
                {
                    status = MembershipCreateStatus.Success;
                    return CreateUserInternal(user);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(String.Format("Ошибка создания пользователя: {0}", ex.Message));
                throw;
            }
        }

        private MembershipUser CreateUserInternal(FMUser user)
        {
            MembershipUser muser = new MembershipUser(base.Name, user.UserName, user.UserID, user.Email, user.PasswordQuestion, string.Empty, false, false, user.CreationDate, user.LastLogInDate, DateTime.Now, user.LastPasswordChangeTime, DateTime.MaxValue);
            return muser;
        }

        private bool ValidateUsername(string userName, string email, Guid excludeKey)
        {
            var um = new UserModel();
            bool ret = um.ValidateUser(userName, email);

            return !ret;
        }

        private bool ValidatePassword(string password)
        {
            bool IsValid = true;
            Regex HelpExpression;

            IsValid = IsValid && (password.Length >= MinRequiredPasswordLength);

            HelpExpression = new Regex(@"\W");
            IsValid = IsValid && (HelpExpression.Matches(password).Count >= MinRequiredNonAlphanumericCharacters);

            HelpExpression = new Regex(PasswordStrengthRegularExpression);
            IsValid = IsValid && (HelpExpression.Matches(password).Count > 0);

            return IsValid;
        }

        private string TransformPassword(string password, ref string salt)
        {
            string ret = string.Empty;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    ret = password;
                    break;
                case MembershipPasswordFormat.Encrypted:
                    byte[] ClearBytes = Encoding.UTF8.GetBytes(password);
                    byte[] EncryptedBytes = base.EncryptPassword(ClearBytes);
                    ret = Convert.ToBase64String(EncryptedBytes);
                    break;
                case MembershipPasswordFormat.Hashed:
                    if (string.IsNullOrEmpty(salt))
                    {
                        byte[] saltBytes = new byte[16];
                        RandomNumberGenerator rng = RandomNumberGenerator.Create();
                        rng.GetBytes(saltBytes);
                        salt = Convert.ToBase64String(saltBytes);
                    }

                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = GetMD5Hash(md5Hash, password);
                        ret = hash;
                    }

                    break;
            }

            return ret;
        }

        private string GetMD5Hash(MD5 hash, string input)
        {
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();
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
            try
            {
                UserModel um = new UserModel();
                FMUser CurrentUser = um.GetUser(username, Guid.Empty, false);
                if (CurrentUser == null)
                    return false;

                if (ValidateUserInternal(CurrentUser, password))
                {
                    CurrentUser.LastLogInDate = DateTime.Now;
                    um.UpdateUser();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        private bool ValidateUserInternal(FMUser user, string password)
        {
            if (user != null)
            {
                string salt = string.Empty;
                string passwordValidate = TransformPassword(password, ref salt);
                if (string.Compare(passwordValidate, password) == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}