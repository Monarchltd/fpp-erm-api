
using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNet8.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController(ILogger<UsersController> logger, IConfiguration configuration) : ControllerBase
    {

        [HttpGet]
        [Route("")]// Matches GET requests to /Tender
        public IActionResult LoginUser([FromQuery] string username, string password)
        {
            bool res = false;
            try
            {
                Login login = new Login();
                res = login.getDetails(username, "", "CD14", password);
            }
            catch (Exception e)
            {

            }
            finally
            {

            }

            return Ok(res);
        }

    }
}
