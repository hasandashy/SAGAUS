using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace SGA.App_Code
{
    public class SGAMembershipProvider : MembershipProvider
    {
        private string _appName;

        private bool _enablePasswordReset;

        private bool _enablePasswordRetrieval;

        private int _maxInvalidPasswordAttempts;

        private int _minRequiredNonalphanumericCharacters;

        private int _minRequiredPasswordLength;

        private int _passwordAttemptWindow;

        private string _passwordStrengthRegularExpression;

        private bool _requiresQuestionAndAnswer;

        private bool _requiresUniqueEmail;

        public override string ApplicationName
        {
            get
            {
                return this._appName;
            }
            set
            {
                this._appName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return this._enablePasswordReset;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return this._maxInvalidPasswordAttempts;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return this._minRequiredNonalphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return this._minRequiredPasswordLength;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return this._passwordAttemptWindow;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return this._passwordStrengthRegularExpression;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return this._requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return this._requiresUniqueEmail;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return this._enablePasswordRetrieval;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return MembershipPasswordFormat.Hashed;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string EmailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new System.ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "SGAMembershipSqlProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Membership Provider for Comprara");
            }
            base.Initialize(name, config);
            this._enablePasswordReset = SGACommon.ConfigGetBooleanValue(config, "enablePasswordReset", true);
            this._enablePasswordRetrieval = SGACommon.ConfigGetBooleanValue(config, "enablePasswordRetrieval", true);
            this._requiresQuestionAndAnswer = SGACommon.ConfigGetBooleanValue(config, "requiresQuestionAndAnswer", true);
            this._requiresUniqueEmail = SGACommon.ConfigGetBooleanValue(config, "requiresUniqueEmail", true);
            this._maxInvalidPasswordAttempts = SGACommon.ConfigGetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            this._passwordAttemptWindow = SGACommon.ConfigGetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            this._minRequiredPasswordLength = SGACommon.ConfigGetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
            this._minRequiredNonalphanumericCharacters = SGACommon.ConfigGetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);
            this._passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (this._passwordStrengthRegularExpression != null)
            {
                this._passwordStrengthRegularExpression = this._passwordStrengthRegularExpression.Trim();
                if (this._passwordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        new Regex(this._passwordStrengthRegularExpression);
                    }
                    catch (System.ArgumentException ex)
                    {
                        throw new ProviderException(ex.Message, ex);
                    }
                }
            }
            this._passwordStrengthRegularExpression = string.Empty;
            if (this._minRequiredNonalphanumericCharacters > this._minRequiredPasswordLength)
            {
                throw new HttpException("MinRequiredNonalphanumericCharacters can not be more than MinRequiredPasswordLength");
            }
            this._appName = config["applicationName"];
            if (string.IsNullOrEmpty(this._appName))
            {
                this._appName = "QldGov";
            }
            if (this._appName.Length > 256)
            {
                throw new ProviderException("Provider application name too long");
            }
            string connectionStringName = config["connectionStringName"];
            if (string.IsNullOrEmpty(connectionStringName))
            {
                this._appName = "QldGov";
            }
            config.Remove("enablePasswordReset");
            config.Remove("enablePasswordRetrieval");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("commandTimeout");
            config.Remove("name");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("connectionStringName");
            if (config.Count > 0)
            {
                string key = config.GetKey(0);
                if (!string.IsNullOrEmpty(key))
                {
                    throw new ProviderException(string.Format("Provider unrecognized attribute {0}", key));
                }
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
