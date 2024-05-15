using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class DBContext
    {
        public static bool ExecuteNonQuery(string commandName, SqlParameter[] parameters)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnect"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = commandName;
                    command.Parameters.AddRange(parameters);

                    try
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        result = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return (result > 0);
        }

        public static DataTable GetParamatizedQuery(string commandName, SqlParameter[] parameters)
        {

            DataTable table = null;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnect"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        try
                        {

                            if (connection.State != ConnectionState.Open)
                            {
                                connection.Open();
                            }

                            table = new DataTable();
                            dataAdapter.Fill(table);

                        }
                        catch (SqlException ex)
                        {
                        }
                        finally
                        {
                            connection.Close();

                        }
                    }

                }
            }
            return table;
        }

        public static DataTable GetSelectQuery(string commandName)
        {
            DataTable table = null;


            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnect"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandName;
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }
                        try
                        {
                            table = new DataTable();
                            dataAdapter.Fill(table);
                        }
                        catch (SqlException ex)
                        {
                        }
                        finally
                        {
                            connection.Close();

                        }
                    }
                }
                return table;
            }

        }
    }
}
