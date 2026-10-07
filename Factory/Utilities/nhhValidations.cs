namespace DotNet8.WebApi.Factory.Utilities
{
    using ClosedXML.Excel;
    using DotNet8.WebApi.Factory.Model;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;

    public class nhhValidations
    {
  
        public bool check_for_existing_cnb_no(string cnb, IConfiguration configuration)
        {
            bool resflag = false;

            try
            {
                string sql = "SELECT * FROM [MasERMTenderHeader] WHERE cnbno = '"+ cnb + "'";

                if (run_sql(sql, configuration) > 0)
                {
                    resflag = true;
                }

            }
            catch(Exception ex)
            {

            }

            return resflag;
        }

        public static int run_sql(string sql, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var reader = command.ExecuteReader();
                        if (reader != null)
                        {
                            if (reader.HasRows == true)
                            {
                                retval = 1;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return retval;
        }
    }
}
