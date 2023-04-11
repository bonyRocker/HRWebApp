using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public static class DataAccessLayer
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static DataSet ds;
        static SqlDataAdapter da;

        

        public static DataSet ExecuteDataSet(CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            try
            {
                con = new SqlConnection(GetConnectionString());
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                if (parameters == null)
                {
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
                else
                {
                    foreach (SqlParameter p in parameters)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                        {
                        }
                        cmd.Parameters.Add(p);
                    }
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PNGConStr"].ConnectionString;
        }
    }
}
