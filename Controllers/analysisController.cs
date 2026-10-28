using DocumentFormat.OpenXml.Drawing;
using DotNet8.WebApi.Factory.Model.Edf;
using DotNet8.WebApi.Factory.Model.sse;
using DotNet8.WebApi.Factory.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;

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
            List<sseSCModel> scModel =  new List<sseSCModel>();

            if (supplier.Equals("SSE"))
            {
                List<sseElec1Model> sse1 = new List<sseElec1Model>();
                List<sseElec2Model> sse2 = new List<sseElec2Model>();
                List<sseElec3Model> sse3 = new List<sseElec3Model>();
                List<sseGasModel> sseGas = new List<sseGasModel>();

                if (type == "HH" || type == "NHH")
                {
                    sse1 = engineXLSX.ReadExcelFileSSEElec1(filepath);

                    if (sse1 != null)
                    {
                        if (sse1.Count > 0)
                        {
                            string[] chkval = sse1[0] != null ? sse1[0].Tariff.Split('_') : [];

                            if (chkval.Count() > 0)
                            {
                                if (!type.Trim().Equals(chkval[0].Trim()))
                                {
                                    return Ok(scModel);
                                }
                            }
                        }
                    }

                    if (sse1.Count == 0)
                    {
                        sse2 = engineXLSX.ReadExcelFileSSEElec2(filepath);
                        if (sse2 != null)
                        {
                            if (sse2.Count > 0)
                            {
                                string[] chkval = sse2[0] != null ? sse2[0].Tariff.Split('_') : [];

                                if (chkval.Count() > 0)
                                {
                                    if (!type.Trim().Equals(chkval[0].Trim()))
                                    {
                                        return Ok(scModel);
                                    }
                                }
                            }
                        }

                    }

                    if (sse2.Count == 0)
                    {
                        sse3 = engineXLSX.ReadExcelFileSSEElec3(filepath);
                        if (sse3 != null)
                        {
                            if (sse3.Count > 0)
                            {
                                string[] chkval = sse3[0] != null ? sse3[0].Tariff.Split('_') : [];

                                if (chkval.Count() > 0)
                                {
                                    if (!type.Trim().Equals(chkval[0].Trim()))
                                    {
                                        return Ok(scModel);
                                    }
                                }
                            }
                        }

                    }
                }
                else if (type == "Gas")
                {
                    sseGas = engineXLSX.ReadExcelFileSSEGas(filepath);
                }




                if (sse1 != null)
                {
                    foreach (var itm in sse1)
                    {
                        scModel.Add(new sseSCModel
                        {
                            mpan =              itm.MPAN,
                            day =               itm.UnrestrictedpkWh.Trim().Length > 0 ? itm.UnrestrictedpkWh : "0",
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
                            evening =           itm.EveningweekendandnightpkWh.Trim().Length > 0 ? itm.EveningweekendandnightpkWh : "0",
                            night =             itm.NightpkWh.Trim().Length > 0 ? itm.NightpkWh : "0",
                            standing_charge =   itm.StandingChargepday.Trim().Length > 0 ? itm.StandingChargepday : "0",
                            noof_months =       itm.noofMonths.ToString(),
                            avail_charge =      "0"
                        });
                    }
                }

                if (sse3 != null)
                {
                    foreach (var itm in sse3)
                    {
                        scModel.Add(new sseSCModel
                        {
                            mpan =              itm.MPAN,
                            day =               (itm.DaypkWh.Length <= 0 ? (itm.DaypkWh.Length <= 0 ? itm.DaypkWh : itm.DaypkWh) : itm.DaypkWh),
                            evening =           itm.NightpkWh.Trim().Length > 0 ? itm.DaypkWh : "0",
                            night =             itm.NightpkWh.Trim().Length > 0 ? itm.NightpkWh : "0",
                            standing_charge =   itm.StandingChargepday.Trim().Length > 0 ? itm.StandingChargepday : "0",
                            noof_months =       itm.noofMonths.ToString(),
                            avail_charge =      "0"
                        });
                    }
                }

                if (sseGas != null)
                {
                    foreach (var itm in sseGas)
                    {
                        string brownorgreen = string.Empty;
                        string[] splitbrownorgreen = itm.EnergyType.Split(' ');

                        if (splitbrownorgreen != null)
                        {
                            if (splitbrownorgreen.Length > 0)
                            {
                                brownorgreen = splitbrownorgreen[0].Trim() == "Standard" ? "Brown" :
                                                splitbrownorgreen[0].Trim() == "Green" ? "Green" :
                                                 splitbrownorgreen[0].Trim() == "Select" ? "Select Green" : "";
                            }
                        }

                        scModel.Add(new sseSCModel
                        {
                            mpan =              itm.MPAN,
                            day =               (itm.TermConsumptionkWh.Length > 0 ? itm.TermConsumptionkWh : "0"),
                            evening =           "0",
                            night =             "0",
                            standing_charge =   itm.StandingChargepday.Trim().Length > 0 ? itm.StandingChargepday : "0",
                            noof_months =       itm.noofMonths.ToString(),
                            avail_charge =      itm.UnitRatepkWh.ToString(),
                            category =          brownorgreen
                        });
                    }
                }

                int retval = engineXLSX.updateAnalysisTemplateGas(filepath, tenderid, scModel, type, supplier, configuration);

            }

            if (supplier.Equals("EDF Energy"))
            {
                if (type == "Gas")
                {
                    var res = engineXLSX.ReadExcelFileEDFGas(filepath);

                    if (res != null)
                    {
                        if (!type.Trim().Equals(res[0].MeterType.Trim() == "Synthetic Diaphragm" ? "Gas": ""))
                        {
                            return Ok(scModel);
                        }
                    }

                    foreach (var item in res)
                    {
                        string brownorgreen =           string.Empty;
                        string[] splitbrownorgreen =    item.GasSource.Split(' ');

                        if (splitbrownorgreen != null)
                        {
                            if (splitbrownorgreen.Length > 0)
                            {
                                brownorgreen = splitbrownorgreen[0].Trim() == "Standard" ? "Brown" :
                                                splitbrownorgreen[0].Trim() == "UK" ? "Green" :
                                                 splitbrownorgreen[0].Trim() == "Select" ? "Select Green" : "";
                            }
                        }

                        scModel.Add(new sseSCModel
                        {
                            mpan =          item.MPRN,
                            day =           item.Rate1Value.Length > 0 ? item.Rate1Value : "0",
                            evening =       item.Rate2Value.Length > 0 ? item.Rate2Value : "0",
                            night =         item.Rate3Value.Length > 0 ? item.Rate3Value : "0",
                            standing_charge=item.StandingChargeRate,
                            noof_months =   item.noofMonths.ToString(),
                            avail_charge =  item.Rate1Value,
                            category =      brownorgreen
                        });
                    }

                    int retval = engineXLSX.updateAnalysisTemplateGas(filepath, tenderid, scModel, type, supplier, configuration);

                }
                else
                { 
                    var res = engineXLSX.ReadExcelFileEDF(filepath);

                    if (res != null)
                    {
                        if (!type.Trim().Equals(res[0].MeterType.Trim()))
                        {
                            return Ok(scModel);
                        }
                    }

                    #region file_validation
                    int valid_res = edfValidator.fileValidation(filepath, res, tenderid, configuration);
                    #endregion file_validation

                    int stoprun =           engineXLSX.updatepriceloader(filepath, res);

                    List<scModel> sc_res =  engineXLSX.extract_sc_priceloader(filepath, stoprun);

                    foreach (var item in res)
                    {
                        edfModel em =   new edfModel();
                        em =            item;
                        var v =         sc_res.Where(x => x.mpan.Equals(item.MPAN)).ToList();

                        if (v != null)
                        {
                            string brownorgreen =           string.Empty;
                            string[] splitbrownorgreen =    item.ElectricitySource.Split(' ');
                            if (splitbrownorgreen != null)
                            {
                                if (splitbrownorgreen.Length > 0)
                                {
                                    brownorgreen = splitbrownorgreen[0].Trim() == "Standard" ? "Brown" : 
                                                    splitbrownorgreen[0].Trim() == "UK" ? "Green" :
                                                     splitbrownorgreen[0].Trim() == "Select" ? "Select Green" : "";
                                }
                            }

                            scModel.Add(new sseSCModel
                            {
                                mpan =              item.MPAN,
                                day =               item.Rate1Value.Length > 0 ? item.Rate1Value : "0",
                                evening =           item.Rate2Value.Length > 0 ? item.Rate2Value : "0",
                                night =             item.Rate3Value.Length > 0 ? item.Rate3Value : "0",
                                standing_charge =   em.calc_sccharge = v.Count > 0 ? v[0].charge : "",
                                noof_months =       item.noofMonths.ToString(),
                                avail_charge =      "0",
                                category =          brownorgreen
                            });

                            em.calc_sccharge =      v.Count > 0 ? v[0].charge : "";
                        }
                    }

                    int retval =            engineXLSX.updateAnalysisTemplateElec(filepath, tenderid, scModel, type, supplier, configuration);

                    //engineXLSX.updatePivot(filepath);

                }
            }

            return Ok(scModel);
        }

    }
}
