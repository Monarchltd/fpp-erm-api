
using DotNet8.WebApi.Factory.Utilities;
using fppErm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class diTrnERMRequirementsSuppliersController(ILogger<diTrnERMRequirementsUploadController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("InsertSuppliers")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult InsertSuppliers([FromBody] TrnERMRequirementsSuppliers model)
        {
            double inserted_id = insertSql.TrnERMRequirementsSuppliers(model, configuration);

            return model != null ? Ok(model) : NotFound(model);
        }
    }
}
