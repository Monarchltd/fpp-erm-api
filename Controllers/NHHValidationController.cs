
using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNet8.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NHHValidationController(ILogger<NHHValidationController> logger, IConfiguration configuration) : ControllerBase
    {

        [HttpGet]
        [Route("chekforcnb")]// Matches GET requests to /Tender
        public IActionResult chekforcnb(string cnb, IConfiguration configuration)
        {
            bool result = false;
            try
            {
                nhhValidations nhh =    new nhhValidations();
                result = nhh.check_for_existing_cnb_no(cnb.Trim(), configuration);
            }
            catch (Exception e)
            {

            }
            finally
            {

            }

            return Ok(result);
        }

    }
}
