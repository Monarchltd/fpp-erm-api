using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataInsertController(ILogger<DataInsertController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Insert([FromBody]MasErmTenderModel_2 model) //MasErmClientModel model =null)
        {
            int inserted_id = insertSql.InsertMasErmTender_1(model, configuration);

            model.id = inserted_id;

            return Ok(model);
        }
    }
}
