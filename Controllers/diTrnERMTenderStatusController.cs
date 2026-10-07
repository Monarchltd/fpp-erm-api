
using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using fppErm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class diTrnERMTenderStatusController(ILogger<diTrnERMTenderStatusController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("InsertStatus")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult InsertStatus([FromBody] TrnERMTenderStatus_1 model)
        {
            double inserted_id = insertSql.TrnERMTenderStatusInsert(model, configuration);

            return model != null ? Ok(model) : NotFound(model);
        }


        [HttpGet]
        [Route("GetStatus")]
        public IActionResult GetStatus([FromQuery] string id)
        {

            string excpMessage = String.Empty;

            List<TrnERMTenderStatus_1> notes = new List<TrnERMTenderStatus_1>();

            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    String sql = "SELECT * FROM [dbo].[TrnERMTenderStatus_1] WHERE isactive=1 and [MasErmTender] = '" + id + "' order by id desc";

                    int fieldPlcHolder = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fieldPlcHolder = 0;

                                notes.Add(new TrnERMTenderStatus_1()
                                {
                                    id = reader.GetInt64(fieldPlcHolder),
                                    MasErmTender = reader.GetInt64(++fieldPlcHolder),
                                    tenderref = reader.GetString(++fieldPlcHolder),
                                    StatusId = reader.GetInt32(++fieldPlcHolder),
                                    StatusDesc = reader.GetString(++fieldPlcHolder),
                                    SubStatusId = reader.GetInt32(++fieldPlcHolder),
                                    SubStatusDesc = reader.GetString(++fieldPlcHolder),
                                    CurrentState = reader.GetInt32(++fieldPlcHolder),
                                    cusr = reader.GetString(++fieldPlcHolder),
                                    cdte = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                    udte = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    isActive = Convert.ToInt32(reader.GetInt32(++fieldPlcHolder)),
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

            return notes != null ? Ok(notes) : NotFound(excpMessage);
        }

    }
}
