using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class diMasERMTenderHeaderController(ILogger<DataInsertController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Insert([FromBody]MasErmTenderHeaderModel_1 model) //MasErmClientModel model =null)
        {
            //int inserted_id = insertSql.InsertMasErmTenderHeaderModel_1(model, configuration);

            //model.id = inserted_id;

            MasErmTenderHeaderModel_1 headModel = insertSql.InsertMasErmTenderHeaderModel_1(model, configuration);

            //model.id = inserted_id;

            return Ok(headModel);
        }

    }
}
