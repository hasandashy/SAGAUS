using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace DataTier
{
    public sealed class SqlHelper
    {
        private enum SqlConnectionOwnership
        {
            Internal,
            External
        }

        public static string GetConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Conn"].ToString();
            }
        }

        public static bool isValidConnection
        {
            get
            {
                return SqlHelper.CheckDatabaseConnectivity();
            }
        }

        private SqlHelper()
        {
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
            {
                throw new System.ArgumentNullException("command");
            }
            if (commandParameters != null)
            {
                for (int i = 0; i < commandParameters.Length; i++)
                {
                    SqlParameter p = commandParameters[i];
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                        {
                            p.Value = System.DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if (commandParameters != null && dataRow != null)
            {
                int i = 0;
                for (int j = 0; j < commandParameters.Length; j++)
                {
                    SqlParameter commandParameter = commandParameters[j];
                    if (commandParameter.ParameterName == null || commandParameter.ParameterName.Length <= 1)
                    {
                        throw new System.Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName));
                    }
                    if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                    {
                        commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                    }
                    i++;
                }
            }
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if (commandParameters != null && parameterValues != null)
            {
                if (commandParameters.Length != parameterValues.Length)
                {
                    throw new System.ArgumentException("Parameter count does not match Parameter Value count.");
                }
                int i = 0;
                int j = commandParameters.Length;
                while (i < j)
                {
                    if (parameterValues[i] is IDbDataParameter)
                    {
                        IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                        if (paramInstance.Value == null)
                        {
                            commandParameters[i].Value = System.DBNull.Value;
                        }
                        else
                        {
                            commandParameters[i].Value = paramInstance.Value;
                        }
                    }
                    else if (parameterValues[i] == null)
                    {
                        commandParameters[i].Value = System.DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = parameterValues[i];
                    }
                    i++;
                }
            }
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
            {
                throw new System.ArgumentNullException("command");
            }
            if (commandText == null || commandText.Length == 0)
            {
                throw new System.ArgumentNullException("commandText");
            }
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandTimeout = 600;
            if (transaction != null)
            {
                if (transaction.Connection == null)
                {
                    throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                SqlHelper.AttachParameters(command, commandParameters);
            }
        }

        public static int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteNonQuery(commandType, commandText, null);
        }

        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = SqlHelper.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
            return result;
        }

        public static int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            int result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteNonQuery(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteNonQuery(connection, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            int result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return retval;
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            int result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteDataset(commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            DataSet result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = SqlHelper.ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
            return result;
        }

        public static DataSet ExecuteDataset(string spName, params object[] parameterValues)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            DataSet result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteDataset(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteDataset(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteDataset(connection, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            DataSet result;
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                result = ds;
            }
            return result;
        }

        public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            DataSet result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteDataset(transaction, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            DataSet result;
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                result = ds;
            }
            return result;
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            DataSet result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, SqlHelper.SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader result;
            try
            {
                SqlHelper.PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                SqlDataReader dataReader;
                if (connectionOwnership == SqlHelper.SqlConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                    }
                }
                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                result = dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw;
            }
            return result;
        }

        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteReader(commandType, commandText, null);
        }

        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            SqlConnection connection = null;
            SqlDataReader result;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                result = SqlHelper.ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlHelper.SqlConnectionOwnership.Internal);
            }
            catch
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }
            return result;
        }

        public static SqlDataReader ExecuteReader(string spName, params object[] parameterValues)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlDataReader result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteReader(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteReader(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteReader(connection, commandType, commandText, null);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlHelper.SqlConnectionOwnership.External);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlDataReader result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteReader(transaction, commandType, commandText, null);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return SqlHelper.ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlHelper.SqlConnectionOwnership.External);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlDataReader result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static object ExecuteScalar(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteScalar(commandType, commandText, null);
        }

        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            object result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = SqlHelper.ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
            return result;
        }

        public static object ExecuteScalar(string spName, params object[] parameterValues)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            object result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteScalar(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteScalar(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteScalar(connection, commandType, commandText, null);
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            object retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            object result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteScalar(transaction, commandType, commandText, null);
        }

        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            object retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            object result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteXmlReader(connection, commandType, commandText, null);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();
            XmlReader result;
            try
            {
                SqlHelper.PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
                XmlReader retval = cmd.ExecuteXmlReader();
                cmd.Parameters.Clear();
                result = retval;
            }
            catch
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw;
            }
            return result;
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            XmlReader result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteXmlReader(transaction, commandType, commandText, null);
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            XmlReader retval = cmd.ExecuteXmlReader();
            cmd.Parameters.Clear();
            return retval;
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            XmlReader result;
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                result = SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new System.ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlHelper.FillDataset(connection, commandType, commandText, dataSet, tableNames);
            }
        }

        public static void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new System.ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlHelper.FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
            }
        }

        public static void FillDataset(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new System.ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlHelper.FillDataset(connection, spName, dataSet, tableNames, parameterValues);
            }
        }

        public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            SqlHelper.FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
        }

        public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            SqlHelper.FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public static void FillDataset(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (dataSet == null)
            {
                throw new System.ArgumentNullException("dataSet");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                SqlHelper.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                SqlHelper.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            SqlHelper.FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            SqlHelper.FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public static void FillDataset(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (dataSet == null)
            {
                throw new System.ArgumentNullException("dataSet");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                SqlHelper.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                SqlHelper.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        private static void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (dataSet == null)
            {
                throw new System.ArgumentNullException("dataSet");
            }
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                if (tableNames != null && tableNames.Length > 0)
                {
                    string tableName = "Table";
                    for (int index = 0; index < tableNames.Length; index++)
                    {
                        if (tableNames[index] == null || tableNames[index].Length == 0)
                        {
                            throw new System.ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        }
                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        tableName += (index + 1).ToString();
                    }
                }
                dataAdapter.Fill(dataSet);
                command.Parameters.Clear();
            }
            if (mustCloseConnection)
            {
                connection.Close();
            }
        }

        public static void FillDataTable(CommandType commandType, string commandText, DataTable dataTable)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (dataTable == null)
            {
                throw new System.ArgumentNullException("dataTable");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlHelper.FillDataTable(connection, commandType, commandText, dataTable);
            }
        }

        public static void FillDataTable(CommandType commandType, string commandText, DataTable dataTable, params SqlParameter[] commandParameters)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (dataTable == null)
            {
                throw new System.ArgumentNullException("dataTable");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlHelper.FillDataTable(connection, commandType, commandText, dataTable, commandParameters);
            }
        }

        public static void FillDataTable(string spName, DataTable dataTable, params object[] parameterValues)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (dataTable == null)
            {
                throw new System.ArgumentNullException("dataTable");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlHelper.FillDataTable(connection, spName, dataTable, parameterValues);
            }
        }

        public static void FillDataTable(SqlConnection connection, CommandType commandType, string commandText, DataTable dataTable)
        {
            SqlHelper.FillDataTable(connection, commandType, commandText, dataTable, null);
        }

        public static void FillDataTable(SqlConnection connection, CommandType commandType, string commandText, DataTable dataTable, params SqlParameter[] commandParameters)
        {
            SqlHelper.FillDataTable(connection, null, commandType, commandText, dataTable, commandParameters);
        }

        public static void FillDataTable(SqlConnection connection, string spName, DataTable dataTable, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (dataTable == null)
            {
                throw new System.ArgumentNullException("dataTable");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                SqlHelper.FillDataTable(connection, CommandType.StoredProcedure, spName, dataTable, commandParameters);
            }
            else
            {
                SqlHelper.FillDataTable(connection, CommandType.StoredProcedure, spName, dataTable);
            }
        }

        public static void FillDataTable(SqlTransaction transaction, CommandType commandType, string commandText, DataTable dataTable)
        {
            SqlHelper.FillDataTable(transaction, commandType, commandText, dataTable, null);
        }

        public static void FillDataTable(SqlTransaction transaction, CommandType commandType, string commandText, DataTable dataTable, params SqlParameter[] commandParameters)
        {
            SqlHelper.FillDataTable(transaction.Connection, transaction, commandType, commandText, dataTable, commandParameters);
        }

        public static void FillDataTable(SqlTransaction transaction, string spName, DataTable dataTable, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (dataTable == null)
            {
                throw new System.ArgumentNullException("dataTable");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            if (parameterValues != null && parameterValues.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, parameterValues);
                SqlHelper.FillDataTable(transaction, CommandType.StoredProcedure, spName, dataTable, commandParameters);
            }
            else
            {
                SqlHelper.FillDataTable(transaction, CommandType.StoredProcedure, spName, dataTable);
            }
        }

        private static void FillDataTable(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataTable dataTable, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (dataTable == null)
            {
                throw new System.ArgumentNullException("dataTable");
            }
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            SqlHelper.PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                dataAdapter.Fill(dataTable);
                command.Parameters.Clear();
            }
            if (mustCloseConnection)
            {
                connection.Close();
            }
        }

        public static void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null)
            {
                throw new System.ArgumentNullException("insertCommand");
            }
            if (deleteCommand == null)
            {
                throw new System.ArgumentNullException("deleteCommand");
            }
            if (updateCommand == null)
            {
                throw new System.ArgumentNullException("updateCommand");
            }
            if (tableName == null || tableName.Length == 0)
            {
                throw new System.ArgumentNullException("tableName");
            }
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataSet, tableName);
                dataSet.AcceptChanges();
            }
        }

        public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sourceColumns != null && sourceColumns.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                for (int index = 0; index < sourceColumns.Length; index++)
                {
                    commandParameters[index].SourceColumn = sourceColumns[index];
                }
                SqlHelper.AttachParameters(cmd, commandParameters);
            }
            return cmd;
        }

        public static int ExecuteNonQueryTypedParams(string spName, DataRow dataRow)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            int result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteNonQuery(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static int ExecuteNonQueryTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            int result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static int ExecuteNonQueryTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            int result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static DataSet ExecuteDatasetTypedParams(string spName, DataRow dataRow)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            DataSet result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteDataset(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteDataset(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static DataSet ExecuteDatasetTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            DataSet result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            DataSet result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static SqlDataReader ExecuteReaderTypedParams(string spName, DataRow dataRow)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlDataReader result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteReader(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteReader(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlDataReader result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlDataReader result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static object ExecuteScalarTypedParams(string spName, DataRow dataRow)
        {
            string connectionString = SqlHelper.GetConnectionString;
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            object result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteScalar(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName,
					commandParameters
				});
            }
            else
            {
                result = SqlHelper.ExecuteScalar(connectionString, new object[]
				{
					CommandType.StoredProcedure,
					spName
				});
            }
            return result;
        }

        public static object ExecuteScalarTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            object result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static object ExecuteScalarTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            object result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            XmlReader result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new System.ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            XmlReader result;
            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                SqlHelper.AssignParameterValues(commandParameters, dataRow);
                result = SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                result = SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
            return result;
        }

        private static bool CheckDatabaseConnectivity()
        {
            SqlConnection cnn = new SqlConnection();
            bool retVal;
            try
            {
                cnn.ConnectionString = SqlHelper.GetConnectionString;
                cnn.Open();
                retVal = System.Convert.ToBoolean("True");
            }
            catch (System.Exception ex)
            {
                string strErrorText = ex.Message.ToString();
                retVal = System.Convert.ToBoolean("False");
            }
            finally
            {
                cnn.Close();
            }
            return retVal;
        }
    }

    public sealed class SqlHelperParameterCache
    {
        private static System.Collections.Hashtable paramCache = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());

        private SqlHelperParameterCache()
        {
        }

        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();
            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }
            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(discoveredParameters, 0);
            SqlParameter[] array = discoveredParameters;
            for (int i = 0; i < array.Length; i++)
            {
                SqlParameter discoveredParameter = array[i];
                discoveredParameter.Value = System.DBNull.Value;
            }
            return discoveredParameters;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];
            int i = 0;
            int j = originalParameters.Length;
            while (i < j)
            {
                clonedParameters[i] = (SqlParameter)((System.ICloneable)originalParameters[i]).Clone();
                i++;
            }
            return clonedParameters;
        }

        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (commandText == null || commandText.Length == 0)
            {
                throw new System.ArgumentNullException("commandText");
            }
            string hashKey = connectionString + ":" + commandText;
            SqlHelperParameterCache.paramCache[hashKey] = commandParameters;
        }

        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (commandText == null || commandText.Length == 0)
            {
                throw new System.ArgumentNullException("commandText");
            }
            string hashKey = connectionString + ":" + commandText;
            SqlParameter[] cachedParameters = SqlHelperParameterCache.paramCache[hashKey] as SqlParameter[];
            SqlParameter[] result;
            if (cachedParameters == null)
            {
                result = null;
            }
            else
            {
                result = SqlHelperParameterCache.CloneParameters(cachedParameters);
            }
            return result;
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return SqlHelperParameterCache.GetSpParameterSet(connectionString, spName, false);
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new System.ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            SqlParameter[] spParameterSetInternal;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                spParameterSetInternal = SqlHelperParameterCache.GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
            return spParameterSetInternal;
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return SqlHelperParameterCache.GetSpParameterSet(connection, spName, false);
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            SqlParameter[] spParameterSetInternal;
            using (SqlConnection clonedConnection = (SqlConnection)((System.ICloneable)connection).Clone())
            {
                spParameterSetInternal = SqlHelperParameterCache.GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
            return spParameterSetInternal;
        }

        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new System.ArgumentNullException("spName");
            }
            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            SqlParameter[] cachedParameters = SqlHelperParameterCache.paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                SqlParameter[] spParameters = SqlHelperParameterCache.DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                SqlHelperParameterCache.paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }
            return SqlHelperParameterCache.CloneParameters(cachedParameters);
        }
    }
}