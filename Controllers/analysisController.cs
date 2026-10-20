using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Model.Edf;
using DotNet8.WebApi.Factory.Model.sse;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DotNet8.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class analysisController(ILogger<analysisController> logger, IConfiguration configuration) : ControllerBase
    {

        [HttpGet]
        [Route("readData")]// Matches GET requests to /Tender
        public IActionResult readData([FromQuery] string filepath, string tenderid, string type, string supplier)
        {

            engineXLSX engineXLSX =     new engineXLSX();
            List<sseSCModel> scModel = new List<sseSCModel>();

            if (supplier.Equals("SSE"))
            {
                List<sseElec1Model> sse1 = new List<sseElec1Model>();
                List<sseElec2Model> sse2 = new List<sseElec2Model>();

                if (type == "HH" || type == "NHH")
                {
                    sse1 = engineXLSX.ReadExcelFileSSEElec1(filepath);

                    if (sse1.Count == 0)
                    {
                        sse2 = engineXLSX.ReadExcelFileSSEElec2(filepath);
                    }
                }
                else if (type == "Gas")
                {
                }

                if (sse1 != null)
                {
                    foreach (var itm in sse1)
                    {
                        scModel.Add(new sseSCModel
                        {
                            mpan =              itm.MPAN,
                            day =               itm.UnrestrictedpkWh,
                            night =             "0",
                            evening =           "0",
                            standing_charge =   itm.StandingChargepday,
                            noof_months =       itm.noofMonths.ToString(),
                            avail_charge =      "0"
                        });
                    }
                }

                if (sse2 != null)
                {
                    foreach (var itm in sse2)
                    {
                        scModel.Add(new sseSCModel
                        {
                            mpan =              itm.MPAN,
                            day =               (itm.DaypkWh.Length <= 0 ? (itm.UnrestrictedpkWh.Length <= 0 ? itm.WeekdaypkWh : itm.UnrestrictedpkWh) : itm.DaypkWh),
                            evening =           itm.EveningweekendandnightpkWh,
                            night =             itm.NightpkWh,
                            standing_charge =   itm.StandingChargepday,
                            noof_months =       itm.noofMonths.ToString(),
                            avail_charge =      "0"
                        });
                    }
                }

                int retval = engineXLSX.updateAnalysisTemplateNew(filepath, tenderid, scModel, type, supplier, configuration);

            }

            if (supplier.Equals("EDF Energy"))
            {
                var res =               engineXLSX.ReadExcelFileEDF(filepath);

                int stoprun =           engineXLSX.updatepriceloader(filepath, res);

                List<scModel> sc_res =  engineXLSX.extract_sc_priceloader(filepath, stoprun);

                foreach (var item in res)
                {
                    edfModel em = new edfModel();
                    em = item;
                    var v = sc_res.Where(x => x.mpan.Equals(item.MPAN)).ToList();

                    if (v != null)
                    {
                        scModel.Add(new sseSCModel
                        {
                            mpan =              item.MPAN,
                            day =               item.Rate1Value.Length >= 0 ? item.Rate1Value: "0",
                            evening =           item.Rate2Value.Length >= 0 ? item.Rate2Value : "0",
                            night =             item.Rate3Value.Length >= 0 ? item.Rate3Value : "0",
                            standing_charge =   em.calc_sccharge = v.Count > 0 ? v[0].charge : "",
                            noof_months =       item.noofMonths.ToString(),
                            avail_charge =      "0"
                        });

                        em.calc_sccharge = v.Count > 0 ? v[0].charge : "";
                    }
                }

                int retval = engineXLSX.updateAnalysisTemplateNew(filepath, tenderid, scModel, type, supplier, configuration);
            }

            return Ok(scModel);
        }

    }
}
