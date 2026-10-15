using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Model.Edf;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection;

namespace DotNet8.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EdfController(ILogger<EdfController> logger, IConfiguration configuration) : ControllerBase
    {

        [HttpGet]
        [Route("readData")]// Matches GET requests to /Tender
        public IActionResult readData([FromQuery] string filepath)
        {

            engineXLSX engineXLSX =     new engineXLSX();
            var res =                   engineXLSX.ReadExcelFileEDF(filepath);

            int stoprun =               engineXLSX.updatepriceloader(filepath, res);

            List<scModel> sc_res =      engineXLSX.extract_sc_priceloader(filepath, stoprun);

            List<edfModel> edf =        new List<edfModel>();

            foreach (var item in res)
            {
                edfModel em =   new edfModel();
                em =            item;
                var v =         sc_res.Where(x => x.mpan.Equals(item.MPAN)).ToList();

                if (v != null)
                {
                    em.calc_sccharge = v.Count>0 ? v[0].charge : "";
                }

                edf.Add(em);
            }

            return Ok(edf);
        }

    }
}
