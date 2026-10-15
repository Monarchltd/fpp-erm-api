
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
    public class diTrnERMTenderDetailsController(ILogger<diTrnERMTenderDetailsController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("InsertAnalysis")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        //ublic IActionResult InsertAnalysis([FromBody] MasErmTenderAnalysis model)
        public IActionResult InsertAnalysis([FromBody] List<TrnERMTenderDetailsNHH> model)
        {
            double inserted_id = insertSql.TrnERMTenderDetailsUpload(model, configuration);

            return model != null ? Ok(model) : NotFound(model);
        }

        [HttpPost]
        [Route("InsertGasAnalysis")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        //ublic IActionResult InsertAnalysis([FromBody] MasErmTenderAnalysis model)
        public IActionResult InsertGAsAnalysis([FromBody] List<TrnERMTenderDetailsGAS> model)
        {
            double inserted_id = insertSql.TrnERMTenderDetailsGASUpload(model, configuration);

            return model != null ? Ok(model) : NotFound(model);
        }

    }
}
