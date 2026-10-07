
using DotNet8.WebApi.Factory.Utilities;
using fppErm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class diTrnERMRequirementsUploadController(ILogger<diTrnERMRequirementsUploadController> logger, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("InsertUploads")]// Matches GET requests to /HelloWorld
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult InsertUploads([FromBody] List<TrnERMRequirementsUploads> model)
        {
            double inserted_id = insertSql.TrnERMRequirementsUpload(model, configuration);

            return model != null ? Ok(model) : NotFound(model);
        }
    }
}
