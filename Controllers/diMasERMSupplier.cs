using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class diMasERMSupplierController(ILogger<DataInsertController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        [Route("getSupplier")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult getSupplier(string id)
        {
            string excpMessage = String.Empty;
            string sql = String.Empty;
            sql = Convert.ToDouble(id) > 0 ? "SELECT * FROM [dbo].[MasERMSupplier] WHERE id = '"+id+"' and [IsActive] = 1" : 
                                             "SELECT * FROM [dbo].[MasERMSupplier] WHERE [IsActive] = 1";
            List<MasErmSupplierModel> supplier = new List<MasErmSupplierModel>();

            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    int fieldPlcHolder = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fieldPlcHolder = 0;

                                supplier.Add(new MasErmSupplierModel()
                                {
                                    id =        reader.GetInt64(fieldPlcHolder),
                                    name =      reader.GetString(++fieldPlcHolder).Trim(),
                                    address1 =  reader.GetString(++fieldPlcHolder),
                                    address2 =  reader.GetString(++fieldPlcHolder),
                                    postcode =  reader.GetString(++fieldPlcHolder),
                                    email =     reader.GetString(++fieldPlcHolder),
                                    type =      reader.GetInt32(++fieldPlcHolder),
                                    dps =       reader.GetString(++fieldPlcHolder),
                                    comment1 =  reader.GetString(++fieldPlcHolder),
                                    comment2 =  reader.GetString(++fieldPlcHolder),
                                    comment3 =  reader.GetString(++fieldPlcHolder),
                                    comment4 =  reader.GetString(++fieldPlcHolder),
                                    comment5 =  reader.GetString(++fieldPlcHolder),
                                    cusr =      !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                    cdte =      !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    uusr =      !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                    udte =      !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    isActive =  reader.GetInt32(++fieldPlcHolder),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                excpMessage = e.Message;
            }
            finally
            {

            }

            return supplier != null ? (supplier.Count > 0 ? Ok(supplier) : NotFound(supplier)) : NotFound();
        }

    }
}
