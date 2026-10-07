
using DotNet8.WebApi.Factory.Utilities;
using fppErm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class diTrnERMRequirementsContractdurationController(ILogger<diTrnERMRequirementsContractdurationController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("InsertDuration")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult InsertDuration([FromBody] List<TrnERMRequirementsContractDuration> model)
        {
            double inserted_id = insertSql.TrnERMRequirementsContractdurationInsert(model, configuration);

            return model != null ? Ok(model) : NotFound(model);
        }
    }
}
