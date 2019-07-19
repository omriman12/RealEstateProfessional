using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SouthRealEstate.DAL
{
    public class ADOHelper
    {
        private string m_ConnectionString = null;

        /* constants */
        private const int MAX_XOMMAND_EXECUTION_TIMEOUT_SEC = 180;

        public ADOHelper(string connectionStr)
        {
            m_ConnectionString = connectionStr;
        }

        public async Task ExecuteQueryAsync(string query, int timeout = MAX_XOMMAND_EXECUTION_TIMEOUT_SEC)
        {
            try
            {
                using (var connection = new MySqlConnection(m_ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandTimeout = timeout;
                        cmd.CommandText = query;
                        //execute query
                        await cmd.ExecuteNonQueryAsync();
                    }

                    //returns the connection to pool
                    connection.Close();
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary> 
        /// Set the connection, command, and then execute the command with non query. 
        /// </summary> 
        public async Task<Int32> ExecuteNonQueryAsync(String commandText, MySqlParameter[] parameters, int timeout = MAX_XOMMAND_EXECUTION_TIMEOUT_SEC)
        {
            int retVal = 0;
            using (MySqlConnection conn = new MySqlConnection(m_ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
                {
                    cmd.CommandTimeout = timeout;
                    cmd.CommandType = CommandType.Text;
                    if (parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    await conn.OpenAsync();
                    retVal = await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
            }

            return retVal;
        }

        /// <summary> 
        /// Set the connection, command, and then execute the command and only return one value. 
        /// </summary> 
        public async Task<Object> ExecuteScalarAsync(String commandText, MySqlParameter[] parameters, int timeout = MAX_XOMMAND_EXECUTION_TIMEOUT_SEC)
        {
            Object retVal = null;
            using (MySqlConnection conn = new MySqlConnection(m_ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
                {
                    cmd.CommandTimeout = timeout;
                    cmd.CommandType = CommandType.Text;
                    if (parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    await conn.OpenAsync();
                    retVal = await cmd.ExecuteScalarAsync();
                    conn.Close();
                }
            }

            return retVal;
        }
    }
}
