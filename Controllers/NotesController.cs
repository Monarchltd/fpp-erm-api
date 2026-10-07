using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography.Pkcs;

namespace DotNet8.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController(ILogger<NotesController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost] [Route("InsertNotes")]
        public IActionResult InsertNotes([FromBody]TrnERMTenderNotes_1 model)
        {
            insertSql.InsertTrnERMTenderNotes(model, configuration);

            return Ok(model);
        }

        [HttpGet][Route("GetNotes")]
        public IActionResult GetNotes([FromQuery]string id)
        {

            string excpMessage = String.Empty;

            List<TrnERMTenderNotes_1> notes = new List<TrnERMTenderNotes_1>();

            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    String sql = "SELECT * FROM [dbo].[TrnERMTenderNotes_1] WHERE isactive=1 and [MasErmTender_id] = '"+id+"' order by id desc";

                    int fieldPlcHolder = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fieldPlcHolder = 0;

                                notes.Add(new TrnERMTenderNotes_1()
                                {
                                    id =                Convert.ToInt32(reader.GetInt64(fieldPlcHolder)),
                                    MasErmTender_id =   reader.GetInt64(++fieldPlcHolder),
                                    tenderref =         reader.GetString(++fieldPlcHolder),
                                    notes =             reader.GetString(++fieldPlcHolder),
                                    cusr =              reader.GetString(++fieldPlcHolder),
                                    cdte =              !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    uusr =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                    udte =              !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    isActive =          Convert.ToInt32(reader.GetInt32(++fieldPlcHolder)),
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
