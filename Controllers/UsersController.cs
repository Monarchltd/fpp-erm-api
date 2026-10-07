
using DotNet8.WebApi.Factory.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNet8.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController(ILogger<UsersController> logger, IConfiguration configuration) : ControllerBase
    {

        [HttpGet]
        [Route("")]// Matches GET requests to /Tender
        public IActionResult LoginUser()
        {

            logger.LogInformation("User endpoint was hit.");


            try
            {

            }
            catch (Exception e)
            {

            }
            finally
            {

            }

            return Ok();
        }

    }
}
