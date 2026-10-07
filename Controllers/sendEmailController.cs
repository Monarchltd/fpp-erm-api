using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotNet8.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class sendEmailController(ILogger<sendEmailController> logger, IConfiguration configuration): ControllerBase
    {
        [HttpGet]
        [Route("sndEmail")]// Matches GET requests to /Tender
        public IActionResult sndEmail(string foldername, string subjectline, string mailto, string tenderid, string username)
        {
            sendEmail.sndEmail(foldername, subjectline, mailto, tenderid, username, configuration);

            return Ok();
        }
    }
}
