using DataTier;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace SGA.App_Code
{
    public class SGARoleProvider : RoleProvider
    {
        private string _appName;

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

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            System.Collections.Generic.List<string> roles = new System.Collections.Generic.List<string>();
            if (SGACommon.LoginUserInfo != null)
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, "select distinct r.rolename,r.roleId from roles r  inner join usersinroles u on u.roleId=r.roleId where u.userId=" + SGACommon.LoginUserInfo.userId);
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        roles.Add(dr["rolename"].ToString());
                    }
                }
            }
            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
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
                name = "SGARoleManagerSqlProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Roles Provider");
            }
            base.Initialize(name, config);
            this._appName = config["applicationName"];
            if (string.IsNullOrEmpty(this._appName))
            {
                this._appName = "QldGov";
            }
            if (this._appName.Length > 256)
            {
                throw new ProviderException("Provider application name too long");
            }
            config.Remove("applicationName");
            string connectionStringName = config["connectionStringName"];
            if (string.IsNullOrEmpty(connectionStringName))
            {
                this._appName = "QldGov";
            }
            config.Remove("connectionStringName");
            if (config.Count > 0)
            {
                string text2 = config.GetKey(0);
                if (!string.IsNullOrEmpty(text2))
                {
                    throw new ProviderException(string.Format("Provider unrecognized attribute {0}", text2));
                }
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }
    }
}
