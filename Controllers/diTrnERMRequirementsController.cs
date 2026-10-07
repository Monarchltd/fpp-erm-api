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
    public class diTrnERMRequirementsController(ILogger<DataInsertController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("InsertRequirements")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult InsertRequirements([FromBody] TrnERMRequirements model)
        {
            double inserted_id = insertSql.InsertTrnErmRequirements(model, configuration);

            model.id = inserted_id;

            return model != null ? Ok(model) : NotFound(model);
        }
    }
}
