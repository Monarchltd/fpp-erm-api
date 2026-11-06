
using Microsoft.Data.SqlClient;

namespace DotNet8.WebApi.Factory.Utilities
{
    public class deleteSql
    {
        public static int deleteTender(string id, IConfiguration configuration)
        {
            int retval = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    string sql1= "UPDATE [Fixed Pricing Portal].[dbo].[MasErmTender_1] SET [isActive] = 0 where id = '" + id + "'";
                    string sql2= "UPDATE [Fixed Pricing Portal].[dbo].[MasErmTenderHeader] SET [isActive] = 0 where tenderid = '" + id + "'";
                    string sql3= "UPDATE [Fixed Pricing Portal].[dbo].[TrnERMTenderDetailsNHH] SET [isActive] = 0 where masermtender = '" + id + "'";
                    string sql4= "UPDATE [Fixed Pricing Portal].[dbo].[TrnErmRequirements_1] SET [isActive] = 0 where masermtender = '" + id + "'";
                    string sql5= "UPDATE [Fixed Pricing Portal].[dbo].[TrnERMRequirementsSuppliers_1] SET [isActive] = 0 where masermtender = '" + id + "'";
                    string sql6= "UPDATE [Fixed Pricing Portal].[dbo].[TrnErmRequirementsUploads_1] SET [isActive] = 0 where masermtender = '" + id + "'";
                    string sql7= "UPDATE [Fixed Pricing Portal].[dbo].[TrnErmTenderNotes_1] SET [isActive] = 0 where masermtender_id = '" + id + "'";
                    string sql8= "UPDATE [Fixed Pricing Portal].[dbo].[TrnErmTenderStatus_1] SET [isActive] = 0 where masermtender = '" + id + "'";
                    string sql9= "UPDATE [Fixed Pricing Portal].[dbo].[TrnERMRequirementsContractDuration_1] SET [isActive] = 0 where masermtender = '" + id + "'";

                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql3, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql4, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql5, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql6, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql7, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql8, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(sql9, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception e)
            {
            }
            finally
            {

            }

            return retval;
        }

    }
}
