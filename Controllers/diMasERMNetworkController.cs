using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class diMasERMNetworkController(ILogger<DataInsertController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Insert([FromBody] MasErmNetworkModelNHH model) //MasErmClientModel model =null)
        {
            int inserted_id = insertSql.InsertMasErmNetwork(model, configuration);

            model.id = inserted_id;

            return Ok(model);
        }

    }
}
