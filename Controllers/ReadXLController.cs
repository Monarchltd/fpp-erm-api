using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection;

namespace DotNet8.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadXLController(ILogger<HelloWorldController> logger) : ControllerBase
    {
        
        [HttpGet]
        [Route("")]// Matches GET requests to /HelloWorld
        public IActionResult GetReadXL([FromQuery] string filepath, string tabname)
        {
            logger.LogInformation($"Echo endpoint was hit with message: {filepath}");

            engineXLSX engineXLSX = new engineXLSX();

            //MasErmTenderModel_1 tender = new MasErmTenderModel_1();
            MasErmTenderModel_3 tender = new MasErmTenderModel_3();

            if (tabname.Equals("Ele-NHH") ||
                tabname.Equals("Ele-HH"))
            {
                //tender = engineXLSX.ReadExcelFile(filepath, tabname);
                tender = engineXLSX.ReadExcelFileNHH(filepath, tabname);
            }
            else
            {
                APIEnum.Enum myEnum = APIEnum.Enum.ERR_CODE1;

                tender.errMessage = APIEnum.getDescription(myEnum);
            }

           return Ok(tender);
        }
    }
}
