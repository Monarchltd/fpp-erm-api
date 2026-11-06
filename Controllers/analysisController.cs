using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Model.Edf;
using DotNet8.WebApi.Factory.Model.sse;
using DotNet8.WebApi.Factory.Utilities;
using fppErm.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;

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

            #region SSE
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
                            mpan = itm.MPAN,
                            day = itm.UnrestrictedpkWh.Trim().Length > 0 ? itm.UnrestrictedpkWh : "0",
                            night = "0",
                            evening = "0",
                            standing_charge = itm.StandingChargepday,
                            noof_months = itm.noofMonths.ToString(),
                            avail_charge = "0"
                        });
                    }
                }

                if (sse2 != null)
                {
                    foreach (var itm in sse2)
                    {
                        scModel.Add(new sseSCModel
                        {
                            mpan = itm.MPAN,
                            day = (itm.DaypkWh.Length <= 0 ? (itm.UnrestrictedpkWh.Length <= 0 ? itm.WeekdaypkWh : itm.UnrestrictedpkWh) : itm.DaypkWh),
                            evening = itm.EveningweekendandnightpkWh.Trim().Length > 0 ? itm.EveningweekendandnightpkWh : "0",
                            night = itm.NightpkWh.Trim().Length > 0 ? itm.NightpkWh : "0",
                            standing_charge = itm.StandingChargepday.Trim().Length > 0 ? itm.StandingChargepday : "0",
                            noof_months = itm.noofMonths.ToString(),
                            avail_charge = "0"
                        });
                    }
                }

                if (sse3 != null)
                {
                    foreach (var itm in sse3)
                    {
                        scModel.Add(new sseSCModel
                        {
                            mpan = itm.MPAN,
                            day = (itm.DaypkWh.Length <= 0 ? (itm.DaypkWh.Length <= 0 ? itm.DaypkWh : itm.DaypkWh) : itm.DaypkWh),
                            evening = itm.NightpkWh.Trim().Length > 0 ? itm.DaypkWh : "0",
                            night = itm.NightpkWh.Trim().Length > 0 ? itm.NightpkWh : "0",
                            standing_charge = itm.StandingChargepday.Trim().Length > 0 ? itm.StandingChargepday : "0",
                            noof_months = itm.noofMonths.ToString(),
                            avail_charge = "0"
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
                            mpan = itm.MPAN,
                            day = (itm.TermConsumptionkWh.Length > 0 ? itm.TermConsumptionkWh : "0"),
                            evening = "0",
                            night = "0",
                            standing_charge = itm.StandingChargepday.Trim().Length > 0 ? itm.StandingChargepday : "0",
                            noof_months = itm.noofMonths.ToString(),
                            avail_charge = itm.UnitRatepkWh.ToString(),
                            category = brownorgreen
                        });
                    }
                }

                int retval = engineXLSX.updateAnalysisTemplateGas(filepath, tenderid, scModel, type, supplier, configuration);

            }
            #endregion SSE

            #region EDFENERGY
            if (supplier.Equals("EDF Energy"))
            {
                if (type == "Gas")
                {
                    var res = engineXLSX.ReadExcelFileEDFGas(filepath);

                    if (res != null)
                    {
                        if (!type.Trim().Equals(res[0].MeterType.Trim() == "Synthetic Diaphragm" ? "Gas" : ""))
                        {
                            return Ok(scModel);
                        }
                    }

                    foreach (var item in res)
                    {
                        string brownorgreen = string.Empty;
                        string[] splitbrownorgreen = item.GasSource.Split(' ');

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
                            mpan = item.MPRN,
                            day = item.Rate1UsagekWh.Length > 0 ? item.Rate1UsagekWh : "0",
                            evening = item.Rate2UsagekWh.Length > 0 ? item.Rate2UsagekWh : "0",
                            night = item.Rate3UsagekWh.Length > 0 ? item.Rate3UsagekWh : "0",
                            standing_charge = item.StandingChargeRate,
                            noof_months = item.noofMonths.ToString(),
                            avail_charge = item.Rate1Value,
                            category = brownorgreen
                        });
                    }

                    int retval = engineXLSX.updateAnalysisTemplateGas(filepath, tenderid, scModel, type, supplier, configuration);

                }
                else
                {
                    var res= filepath.Length > 0 ? engineXLSX.ReadExcelFileEDF(filepath): null;

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

                    int stoprun = engineXLSX.updatepriceloader(filepath, res);

                    List<scModel> sc_res = engineXLSX.extract_sc_priceloader(filepath, stoprun);

                    foreach (var item in res)
                    {
                        edfModel em = new edfModel();
                        em = item;
                        var v = sc_res.Where(x => x.mpan.Equals(item.MPAN)).ToList();

                        if (v != null)
                        {
                            string brownorgreen = string.Empty;
                            string[] splitbrownorgreen = item.ElectricitySource.Split(' ');
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
                                mpan = item.MPAN,
                                day = engineXLSX.getDayvalue(item, "EDF Energy"),
                                evening = engineXLSX.getEveningvalue(item, "EDF Energy"),
                                night = engineXLSX.getNightvalue(item, "EDF Energy"),
                                standing_charge = em.calc_sccharge = v.Count > 0 ? v[0].charge : "",
                                noof_months = item.noofMonths.ToString(),
                                avail_charge = "0",
                                category = brownorgreen
                            });

                            em.calc_sccharge = v.Count > 0 ? v[0].charge : "";
                        }
                    }

                    int retval = engineXLSX.updateAnalysisTemplateElec(filepath, tenderid, scModel, type, supplier, configuration);

                    //engineXLSX.updatePivot(filepath);

                }
            }
            #endregion EDFENERGY

            return Ok(scModel);
        }

        [HttpPost]
        [Route("multiAnalysis")]
        public IActionResult multiAnalysis([FromBody]List<MultiAnalysisModel> idList)
        {
            try
            {
                engineXLSX.createMultiAnalysisElec(idList, configuration);
            }
            catch(Exception ex)
            {

            }
            return Ok();
        }


        [HttpGet]
        [Route("readDatav1")]// Matches GET requests to /Tender
        public IActionResult readDatav1([FromQuery] string filepath1,
            string filepath2,
            string filepath3,
            string filepath4,
            string filepath5,
            string filepath6,
            string tenderid, string type, string supplier)
        {

            string filepath = filepath1.Length > 1 ? filepath1 :
                              filepath2.Length > 1 ? filepath2 :
                              filepath3.Length > 1 ? filepath3 :
                              filepath4.Length > 1 ? filepath4 :
                              filepath5.Length > 1 ? filepath5 :
                              filepath6.Length > 1 ? filepath6 : "";

            engineXLSX engineXLSX =     new engineXLSX();
            List<sseSCModel> scModel =  new List<sseSCModel>();

            #region SSE
            if (supplier.Equals("SSE"))
            {
                List<sseElec1Model> sse1 = new List<sseElec1Model>();
                List<sseElec2Model> sse2 = new List<sseElec2Model>();
                List<sseElec3Model> sse3 = new List<sseElec3Model>();
                List<sseGasModel> sseGas = new List<sseGasModel>();

                if (type == "HH" || type == "NHH")
                {
                    //sse1 = engineXLSX.ReadExcelFileSSEElec1(filepath);
                    var res1 = filepath1.Length > 1 ? engineXLSX.ReadExcelFileSSEElec1(filepath1) : null;
                    var res2 = filepath2.Length > 1 ? engineXLSX.ReadExcelFileSSEElec1(filepath2) : null;
                    var res3 = filepath3.Length > 1 ? engineXLSX.ReadExcelFileSSEElec1(filepath3) : null;
                    var res4 = filepath4.Length > 1 ? engineXLSX.ReadExcelFileSSEElec1(filepath4) : null;
                    var res5 = filepath5.Length > 1 ? engineXLSX.ReadExcelFileSSEElec1(filepath5) : null;
                    var res6 = filepath6.Length > 1 ? engineXLSX.ReadExcelFileSSEElec1(filepath6) : null;

                    sse1 = engineXLSX.Merge_ReadExcelFileSSEElec1(res1, res2, res3, res4, res5, res6);

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
                        //sse2 = engineXLSX.ReadExcelFileSSEElec2(filepath);

                        var res7 = filepath1.Length > 1 ? engineXLSX.ReadExcelFileSSEElec2(filepath1) : null;
                        var res8 = filepath2.Length > 1 ? engineXLSX.ReadExcelFileSSEElec2(filepath2) : null;
                        var res9 = filepath3.Length > 1 ? engineXLSX.ReadExcelFileSSEElec2(filepath3) : null;
                        var res10 = filepath4.Length > 1 ? engineXLSX.ReadExcelFileSSEElec2(filepath4) : null;
                        var res11 = filepath5.Length > 1 ? engineXLSX.ReadExcelFileSSEElec2(filepath5) : null;
                        var res12 = filepath6.Length > 1 ? engineXLSX.ReadExcelFileSSEElec2(filepath6) : null;

                        sse2 = engineXLSX.Merge_ReadExcelFileSSEElec2(res7, res8, res9, res10, res11, res12);


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
                        //sse3 = engineXLSX.ReadExcelFileSSEElec3(filepath);

                        var res13 = filepath1.Length > 1 ? engineXLSX.ReadExcelFileSSEElec3(filepath1) : null;
                        var res14 = filepath2.Length > 1 ? engineXLSX.ReadExcelFileSSEElec3(filepath2) : null;
                        var res15 = filepath3.Length > 1 ? engineXLSX.ReadExcelFileSSEElec3(filepath3) : null;
                        var res16 = filepath4.Length > 1 ? engineXLSX.ReadExcelFileSSEElec3(filepath4) : null;
                        var res17 = filepath5.Length > 1 ? engineXLSX.ReadExcelFileSSEElec3(filepath5) : null;
                        var res18 = filepath6.Length > 1 ? engineXLSX.ReadExcelFileSSEElec3(filepath6) : null;

                        sse3 = engineXLSX.Merge_ReadExcelFileSSEElec3(res13, res14, res15, res16, res17, res18);


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
                    //sseGas = engineXLSX.ReadExcelFileSSEGas(filepath);

                    var res19 = filepath1.Length > 1 ? engineXLSX.ReadExcelFileSSEGas(filepath1) : null;
                    var res20 = filepath2.Length > 1 ? engineXLSX.ReadExcelFileSSEGas(filepath2) : null;
                    var res21 = filepath3.Length > 1 ? engineXLSX.ReadExcelFileSSEGas(filepath3) : null;
                    var res22 = filepath4.Length > 1 ? engineXLSX.ReadExcelFileSSEGas(filepath4) : null;
                    var res23 = filepath5.Length > 1 ? engineXLSX.ReadExcelFileSSEGas(filepath5) : null;
                    var res24 = filepath6.Length > 1 ? engineXLSX.ReadExcelFileSSEGas(filepath6) : null;

                    sseGas = engineXLSX.Merge_ReadExcelFileSSEGas(res19, res20, res21, res22, res23, res24);

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
                        string brownorgreen =   string.Empty;
                        brownorgreen =          itm.EnergyType.Substring(0, 2).ToLower() == "st" ? "Brown" :
                                                itm.EnergyType.Substring(0, 2).ToLower() == "uk" ? "Green" :
                                                itm.EnergyType.Substring(0, 2).ToLower() == "se" ? "Select Green" : "";

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
            #endregion SSE

            #region EDFENERGY
            try
            {
                if (supplier.Equals("EDF Energy"))
                {
                    if (type == "Gas")
                    {
                        //var res = engineXLSX.ReadExcelFileEDFGas(filepath);
                        var res1 = filepath1.Length > 1 ? engineXLSX.ReadExcelFileEDFGas(filepath1) : null;
                        var res2 = filepath2.Length > 1 ? engineXLSX.ReadExcelFileEDFGas(filepath2) : null;
                        var res3 = filepath3.Length > 1 ? engineXLSX.ReadExcelFileEDFGas(filepath3) : null;
                        var res4 = filepath4.Length > 1 ? engineXLSX.ReadExcelFileEDFGas(filepath4) : null;
                        var res5 = filepath5.Length > 1 ? engineXLSX.ReadExcelFileEDFGas(filepath5) : null;
                        var res6 = filepath6.Length > 1 ? engineXLSX.ReadExcelFileEDFGas(filepath6) : null;

                        var res = engineXLSX.Merge_ReadExcelFileEDFGas(res1, res2, res3, res4, res5, res6);


                        if (res != null)
                        {
                            if (!type.Trim().Equals(res.Count > 0 ? res[0].MeterType.Trim() == "Synthetic Diaphragm" ? "Gas" : "" : ""))
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
                                mpan =              item.MPRN,
                                day =               item.Rate1Value.Length > 0 ? item.Rate1UsagekWh : "0",
                                evening =           item.Rate2Value.Length > 0 ? item.Rate2UsagekWh : "0",
                                night =             item.Rate3Value.Length > 0 ? item.Rate3UsagekWh : "0",
                                standing_charge =   item.StandingChargeRate,
                                noof_months =       item.noofMonths.ToString(),
                                avail_charge =      item.Rate1Value,
                                category =          brownorgreen
                            });
                        }

                        int retval = engineXLSX.updateAnalysisTemplateGas(filepath, tenderid, scModel, type, supplier, configuration);

                    }
                    else
                    {

                        var res1 = filepath1.Length > 1 ? engineXLSX.ReadExcelFileEDF(filepath1) : null;
                        var res2 = filepath2.Length > 1 ? engineXLSX.ReadExcelFileEDF(filepath2) : null;
                        var res3 = filepath3.Length > 1 ? engineXLSX.ReadExcelFileEDF(filepath3) : null;
                        var res4 = filepath4.Length > 1 ? engineXLSX.ReadExcelFileEDF(filepath4) : null;
                        var res5 = filepath5.Length > 1 ? engineXLSX.ReadExcelFileEDF(filepath5) : null;
                        var res6 = filepath6.Length > 1 ? engineXLSX.ReadExcelFileEDF(filepath6) : null;

                        var res =  engineXLSX.Merge_ReadExcelFileEDF(res1, res2, res3, res4, res5, res6);

                        if (res != null)
                        {
                            if (!type.Trim().Equals(res.Count >0 ? res[0].MeterType.Trim() : ""))
                            {
                                return Ok(scModel);
                            }
                        }
                        
                        #region file_validation
                        int valid_res =         edfValidator.fileValidation(filepath, res, tenderid, configuration);
                        #endregion file_validation

                        int stoprun =           engineXLSX.updatepriceloader(filepath, res);
                        List<scModel> sc_res =  engineXLSX.extract_sc_priceloader(filepath, stoprun);

                        try
                        {
                            //req04112026: Yes we can do that like - if we have same end in the uploaded files they will be
                            //categorized as 0-12 months.
                            var req041120261_0 = res.Select(x => x.EarliestTerminationDate).Distinct().ToList();

                            foreach (var item in res)
                            {
                                edfModel em =   new edfModel();
                                em =            item;
                                var v =         sc_res.Where(x => x.mpan.Equals(item.MPAN) &&
                                                                  x.source.Equals(item.ElectricitySource) &&
                                                                  x.noof_months == item.noofMonths).ToList();

                                if (v != null)
                                {
                                    string brownorgreen = string.Empty;

                                    brownorgreen = item.ElectricitySource.Substring(0, 2).ToLower().Trim() == "st" ? "Brown" :
                                                   item.ElectricitySource.Substring(0, 2).ToLower().Trim() == "uk" ? "Green" :
                                                   item.ElectricitySource.Substring(0, 2).ToLower().Trim() == "se" ? "Select Green" : "";

                                    scModel.Add(new sseSCModel
                                    {
                                        mpan =              item.MPAN,
                                        day =               engineXLSX.getDayvalue(item, "EDF Energy"),
                                        evening =           engineXLSX.getEveningvalue(item, "EDF Energy"),
                                        night =             engineXLSX.getNightvalue(item, "EDF Energy"),
                                        standing_charge =   em.calc_sccharge = v != null ? v[0].charge : "",
                                        noof_months =       req041120261_0 != null ? (req041120261_0.Count() > 1 ? item.noofMonths.ToString() : "12") : item.noofMonths.ToString(),
                                        avail_charge =      "0",
                                        category =          brownorgreen
                                    });

                                    em.calc_sccharge = v != null ? v[0].charge : "";
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        int retval =            engineXLSX.updateAnalysisTemplateElec(filepath, tenderid, scModel, type, supplier, configuration);
                    }
                }
            }
            catch(TaskCanceledException ex)
            {

            }
            #endregion EDFENERGY

            return Ok(scModel);
        }

    }
}
