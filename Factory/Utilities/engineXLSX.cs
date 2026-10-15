namespace DotNet8.WebApi.Factory.Utilities
{
    using ClosedXML.Excel;
    using DotNet8.WebApi.Factory.Model;
    using DotNet8.WebApi.Factory.Model.Edf;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Aspose.Cells;

    public class engineXLSX
    {
  
        public static MasErmTenderModel_1 ReadExcelFile(string filefullpath, string tabname)
        {
            string errMessage                       = String.Empty;
            MasErmTenderModel_1 retval              = new MasErmTenderModel_1();

            retval.MasErmClientModel                = new MasErmClientModel();
            retval.MasErmNetworkModel               = new MasErmNetworkModel();
            retval.MasErmTenderHeaderModel          = new MasErmTenderHeaderModel_1();
            retval.MasErmTenderMsnModel_s           = new List<MasErmTenderMsnModel_1>();
            retval.MasErmTenderMpanModel_s          = new List<MasErmTenderMpanModel_1>();
            retval.MasErmTenderConsumptionModel_s   = new List<MasErmTenderConsumptionModel_1>();
            retval.MasErmTenderContractModel_s      = new List<MasErmTenderContractModel_1>();
            retval.MasErmTenderCurrentRatesModel_s  = new List<MasErmTenderCurrentRatesModel_1>();


            retval.MasErmClientModel.name           = "THE MONARCH PARTNERSHIP";
            retval.MasErmClientModel.cusr           = "System";
            retval.MasErmClientModel.cdte           = DateTime.Now;
            retval.MasErmClientModel.uusr           = "System";
            retval.MasErmClientModel.udte           = DateTime.MinValue;
            retval.MasErmClientModel.isActive       = 1;

            retval.MasErmNetworkModel.cusr          = "System";
            retval.MasErmNetworkModel.cdte          = DateTime.Now;
            retval.MasErmNetworkModel.uusr          = String.Empty;
            retval.MasErmNetworkModel.udte          = DateTime.Now;

            try
            {
                using (var workbook = new XLWorkbook(filefullpath))
                {

                    var worksheet = workbook.Worksheet(tabname);

                    var rows = worksheet.RangeUsed().RowsUsed();

                    foreach (var row in rows)
                    {
                        switch (row.RowNumber())
                        {
                            case 3: retval.MasErmNetworkModel.name =            row.Cell(2).GetValue<string>(); break;
                            case 4: retval.MasErmNetworkModel.headaddress1 =    row.Cell(2).GetValue<string>(); break;
                            case 5: retval.consultantname =                     row.Cell(2).GetValue<string>(); break;
                            case 6: retval.notes =                              row.Cell(2).GetValue<string>(); break;
                            case 7: retval.MasErmNetworkModel.address1 =        row.Cell(1).GetValue<string>(); break;
                            case 8:                             break;
                            case 9:                             break;
                            case 10:                            break;
                            default:                            break;
                        }

                        if (row.RowNumber() > 10)
                        {
                            MasErmTenderMsnModel_1 model_msn =  new MasErmTenderMsnModel_1();
                            model_msn.id =                      0;
                            model_msn.MasErmTender =            "0";
                            model_msn.locref =                  row.Cell(1).GetValue<string>();
                            if (model_msn.locref.Length > 0)
                            {
                                model_msn.clientref =   row.Cell(2).GetValue<string>();
                                model_msn.address1 =    row.Cell(3).GetValue<string>();
                                model_msn.address2 =    row.Cell(4).GetValue<string>();
                                model_msn.address3 =    row.Cell(5).GetValue<string>();
                                model_msn.address4 =    row.Cell(6).GetValue<string>();
                                model_msn.postcode =    row.Cell(8).GetValue<string>();
                                model_msn.msn =         row.Cell(9).GetValue<string>();
                                model_msn.cusr =        "";
                                model_msn.cdte =        Convert.ToDateTime("2019-01-06T17:16:40");
                                model_msn.uusr =        "";
                                model_msn.udte =        Convert.ToDateTime("2019-01-06T17:16:40");
                                model_msn.isActive =    1;
                                retval.MasErmTenderMsnModel_s.Add(model_msn);

                                MasErmTenderMpanModel_1 model_mpan =            new MasErmTenderMpanModel_1();
                                model_mpan.TrnERMTenderMsn =                    "";
                                model_mpan.mpan =                               row.Cell(10).GetValue<string>();
                                model_mpan.profile =                            model_mpan.mpan.Substring(0, 2);
                                model_mpan.mtc =                                model_mpan.mpan.Substring(2, 3);
                                model_mpan.llf =                                model_mpan.mpan.Substring(5, 3);
                                model_mpan.distributorid =                      model_mpan.mpan.Substring(8, 2);
                                model_mpan.mpan_core =                          model_mpan.mpan.Substring(8, (model_mpan.mpan.Length - 8));
                                model_mpan.mpan_core_valid =                    mpanCore_validation(model_mpan.mpan_core) == true ? 1 : 0;
                                model_mpan.id =                                 0;
                                model_mpan.MasErmTender =                       "0";
                                model_mpan.cusr =                               "";
                                model_mpan.cdte =                               DateTime.Now;
                                model_mpan.uusr =                               "";
                                model_mpan.udte =                               DateTime.MinValue;
                                model_mpan.isActive =                           1;
                                retval.MasErmTenderMpanModel_s.Add(model_mpan);

                                MasErmTenderConsumptionModel_1 model_cons =     new MasErmTenderConsumptionModel_1();
                                model_cons.id =                                 0;
                                model_cons.MasErmTender =                       "";
                                model_cons.TrnERMTenderMsn =                    "0";
                                model_cons.TrnERMTenderMpan =                   "0";
                                model_cons.day =                                row.Cell(33).GetValue<string>().Length > 0 ? Math.Round(row.Cell(33).GetValue<decimal>(), 2) : 0;
                                model_cons.night =                              row.Cell(34).GetValue<string>().Length > 0 ? Math.Round(row.Cell(34).GetValue<decimal>(), 2) : 0;
                                model_cons.evening =                            row.Cell(35).GetValue<string>().Length > 0 ? Math.Round(row.Cell(35).GetValue<decimal>(), 2) : 0;
                                model_cons.total =                              row.Cell(36).GetValue<string>().Length > 0 ? Math.Round(row.Cell(36).GetValue<decimal>(), 2) : 0;
                                model_cons.cusr =                               "";
                                model_cons.cdte =                               DateTime.Now;
                                model_cons.uusr =                               "";
                                model_cons.udte =                               DateTime.MinValue;
                                model_cons.isActive =                           1;
                                retval.MasErmTenderConsumptionModel_s.Add(model_cons);

                                MasErmTenderContractModel_1 model_contract =    new MasErmTenderContractModel_1();
                                model_contract.id =                             0;
                                model_contract.MasErmTender =                   "";
                                model_contract.TrnERMTenderMsn =                "";
                                model_contract.TrnERMTenderMpan =               "";
                                model_contract.TrnERMTenderConsumption =        "";
                                model_contract.startdate =                      Convert.ToDateTime(row.Cell(37).GetValue<string>());
                                model_contract.payment_method =                 row.Cell(38).GetValue<string>();
                                model_contract.payment_duration =               row.Cell(39).GetValue<string>();
                                model_contract.cusr =                           "";
                                model_contract.cdte =                           DateTime.Now;
                                model_contract.uusr =                           "";
                                model_contract.udte =                           DateTime.MinValue;
                                model_contract.isActive =                       1;
                                retval.MasErmTenderContractModel_s.Add(model_contract);

                                MasErmTenderCurrentRatesModel_1 model_rates =   new MasErmTenderCurrentRatesModel_1();
                                model_rates.id =                                0;
                                model_rates.MasErmTender =                      "";
                                model_rates.TrnERMTenderMsn =                   "";
                                model_rates.TrnERMTenderMpan =                  "";
                                model_rates.TrnERMTenderConsumption =           "";
                                model_rates.TrnERMTenderContract =              "";
                                model_rates.startdate =                         DateTime.MinValue;
                                model_rates.standingchargeperiod =              row.Cell(40).GetValue<string>();
                                decimal dValue;
                                bool isdecimal =                                decimal.TryParse(row.Cell(41).GetValue<string>(), out dValue);
                                model_rates.standing_charge =                   isdecimal == true ? Math.Round(row.Cell(41).GetValue<decimal>(), 2) : 0;
                                isdecimal =                                     decimal.TryParse(row.Cell(42).GetValue<string>(), out dValue);
                                model_rates.day_rate =                          isdecimal == true ? Math.Round(row.Cell(42).GetValue<decimal>(), 2) : 0;
                                isdecimal =                                     decimal.TryParse(row.Cell(43).GetValue<string>(), out dValue);
                                model_rates.night_rate =                        isdecimal == true ? Math.Round(row.Cell(43).GetValue<decimal>(), 2) : 0;
                                model_rates.cusr =                              "";
                                model_rates.cdte =                              DateTime.Now;
                                model_rates.uusr =                              "";
                                model_rates.udte =                              DateTime.MinValue;
                                model_rates.isActive =                          1;

                                retval.MasErmTenderCurrentRatesModel_s.Add(model_rates);
                            }
                        }
                    }

                    retval.successMessage = "Sucessfully Loaded.";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Function name: engineXLSX -> ReadExcelFile ");
                Console.WriteLine(ex.ToString());
                errMessage              = ex.Message.ToString();
                retval.successMessage   = null;
                retval.errMessage       = errMessage;

            }
            return (retval.successMessage != null ? cleanseData(retval) : retval);
        }
        
        public static MasErmTenderModel_1 cleanseData(MasErmTenderModel_1 data)
        {
            if (data != null)
            {
                string[] strTmp =                       data.MasErmNetworkModel.name.Trim().Split(",");
                data.consultantname =                   (strTmp.Length >0 ? strTmp[0] : String.Empty) + "," + (strTmp.Length > 1 ? strTmp[1] : String.Empty);
                data.consultantemail =                  (strTmp.Length > 1 ? strTmp[2] : String.Empty);

                data.MasErmNetworkModel.address1 =  data.MasErmNetworkModel.address1.Replace("Correspondance Address : ", "");
            }

            return data;
        }

        public static MasErmTenderModel_3 cleanseData_3(MasErmTenderModel_3 data)
        {
            if (data != null)
            {
                string[] strTmp =       data.MasErmNetworkModel.consultant.Trim().Split(",");
                data.consultantname =   (strTmp.Length > 0 ? strTmp[0] : String.Empty) + "," + (strTmp.Length > 1 ? strTmp[1] : String.Empty);
                data.consultantemail =  (strTmp.Length > 1 ? strTmp[1] : String.Empty);

                int i = 0;
                string[] strTmp1 =      data.MasErmNetworkModel.corraddress1.Split(",");
                data.MasErmNetworkModel.corraddress1 = "";
                foreach (string str in strTmp1)
                {
                    if (i < (strTmp1.Length - 1))
                    {
                        data.MasErmNetworkModel.corraddress1 += strTmp1[i] + ",";
                    }
                    i++;
                }
                data.MasErmNetworkModel.corrpostcode = strTmp1[strTmp1.Length - 1];

                string[] strTmp2 =      data.MasErmNetworkModel.headaddress1.Split(",");
                data.MasErmNetworkModel.headaddress1 = "";
                i = 0;
                foreach(string str in strTmp2)
                {
                    if (i < (strTmp2.Length-1))
                    {
                        data.MasErmNetworkModel.headaddress1 += strTmp2[i] + ",";
                    }
                    i++;
                }
                data.MasErmNetworkModel.headpostcode = strTmp2[strTmp2.Length - 1];

            }

            return data;
        }

        public static bool mpanCore_validation(string mpan)
        {
            bool retval = false;

            if (!string.IsNullOrEmpty(mpan))
            {
                int mpanDig1 = Convert.ToInt16(mpan.Substring(0,1));
                int mpanDig2 = Convert.ToInt16(mpan.Substring(1,1));
                int mpanDig3 = Convert.ToInt16(mpan.Substring(2,1));
                int mpanDig4 = Convert.ToInt16(mpan.Substring(3,1));
                int mpanDig5 = Convert.ToInt16(mpan.Substring(4,1));
                int mpanDig6 = Convert.ToInt16(mpan.Substring(5,1));
                int mpanDig7 = Convert.ToInt16(mpan.Substring(6,1));
                int mpanDig8 = Convert.ToInt16(mpan.Substring(7,1));
                int mpanDig9 = Convert.ToInt16(mpan.Substring(8,1));
                int mpanDig10= Convert.ToInt16(mpan.Substring(9,1));
                int mpanDig11= Convert.ToInt16(mpan.Substring(10,1));
                int mpanDig12= Convert.ToInt16(mpan.Substring(11,1));

                // P11*3 +Q11*5 +R11*7 +S11*13 +T11*17 +U11*19
                //       +V11*23 +W11*29 +X11*31 +Y11*37 +Z11*41 +AA11*43
                int source = (mpanDig1 * 3 +
                            mpanDig2* 5 +
                            mpanDig3* 7 +
                            mpanDig4* 13 +
                            mpanDig5* 17 +
                            mpanDig6* 19 +
                            mpanDig7* 23 +
                            mpanDig8* 29 +
                            mpanDig9* 31 +
                            mpanDig10* 37 +
                            mpanDig11 * 41 +
                            mpanDig12 * 43) % 11;

                int digit13  = Convert.ToInt16(mpan.Substring(12, 1));


                int mod10_11 = (source) % 10;

                if (digit13 == mod10_11)
                    retval = true;

            }

            return retval;
        }

        public static MasErmTenderModel_3 ReadExcelFileNHH(string filefullpath, string tabname)
        {
            string errMessage =                     String.Empty;
            MasErmTenderModel_3 retval =            new MasErmTenderModel_3();

            retval.MasErmClientModel =              new MasErmClientModel();
            retval.MasErmNetworkModel =             new MasErmNetworkModelNHH();
            retval.MasErmTenderHeaderModel =        new MasErmTenderHeaderModel_1();
            retval.TrnERMTenderDetails =            new List<TrnERMTenderDetailsNHH>();

            retval.MasErmClientModel.name =         "THE MONARCH PARTNERSHIP";
            retval.MasErmClientModel.cusr =         "System";
            retval.MasErmClientModel.cdte =         DateTime.Now;
            retval.MasErmClientModel.uusr =         "System";
            retval.MasErmClientModel.udte =         DateTime.MinValue;
            retval.MasErmClientModel.isActive =     1;

            retval.MasErmNetworkModel.cusr =        "System";
            retval.MasErmNetworkModel.cdte =        DateTime.Now;
            retval.MasErmNetworkModel.uusr =        String.Empty;
            retval.MasErmNetworkModel.udte =        DateTime.Now;

            try
            {
                using (var workbook = new XLWorkbook(filefullpath))
                {

                    var worksheet = workbook.Worksheet(tabname);

                    var rows = worksheet.RangeUsed().RowsUsed();

                    List<string> err = new List<string>();

                    foreach (var row in rows)
                    {
                        TrnERMTenderDetailsNHH tdnhh = new TrnERMTenderDetailsNHH();

                        switch (row.RowNumber())
                        {
                            case 1: retval.MasErmClientModel.name =             row.Cell(4).GetValue<string>(); break;
                            case 3: retval.MasErmNetworkModel.name =            row.Cell(2).GetValue<string>(); break;
                            case 4: retval.MasErmNetworkModel.registration_no = row.Cell(2).GetValue<string>(); break;
                            case 5: retval.MasErmNetworkModel.headaddress1 =    row.Cell(2).GetValue<string>(); break;
                            case 6: retval.MasErmNetworkModel.consultant =      row.Cell(2).GetValue<string>(); break;
                            case 7: retval.MasErmNetworkModel.comments =        row.Cell(1).GetValue<string>(); break;
                            case 8: retval.MasErmNetworkModel.corraddress1 =    row.Cell(2).GetValue<string>(); break;
                            case 9: break;
                            case 10:break;
                            case 11:int idx = 1;
                                    string headerValue = String.Empty;
                                    while (idx <= 62)
                                    {
                                        headerValue += row.Cell(idx).GetValue<string>() + ",";
                                        idx++;
                                    }
                                    err = nhhHeaderValidation(headerValue);
                                    break;
                            default:break;
                        }

                        if (row.RowNumber() == 11)
                        {
                            if (err.Count > 0)
                            {
                                retval.successMessage = "Error";

                                foreach (string e in err)
                                {
                                    retval.errMessage += e;
                                }
                                return (retval);
                            }
                        }

                        if (row.RowNumber() > 11)
                        {
                            tdnhh.id = 0;
                            tdnhh.MasErmTender = 0;
                            tdnhh.Location_Ref = row.Cell(1).GetValue<string>();

                            if (tdnhh.Location_Ref.Length > 0)
                            {
                                tdnhh.Location_Ref =                            row.Cell(1).GetValue<string>().Replace("'", " ");
                                tdnhh.Client_Ref =                              row.Cell(2).GetValue<string>().Replace("'", " ");
                                tdnhh.Address1 =                                row.Cell(3).GetValue<string>().Replace("'", " ");
                                tdnhh.Address2 =                                row.Cell(4).GetValue<string>().Replace("'", " ");
                                tdnhh.Address3 =                                row.Cell(5).GetValue<string>().Replace("'", " ");
                                tdnhh.Address4 =                                row.Cell(6).GetValue<string>().Replace("'", " ");
                                tdnhh.Address5 =                                row.Cell(7).GetValue<string>().Replace("'", " ");
                                tdnhh.Postcode =                                row.Cell(8).GetValue<string>().Replace("'", " ");
                                tdnhh.Msn =                                     row.Cell(9).GetValue<string>().Replace("'", " ");
                                tdnhh.Mpanfull =                                row.Cell(10).GetValue<string>().Replace("'", " ");
                                tdnhh.Profile =                                 row.Cell(11).GetValue<string>().Replace("'", " ");
                                tdnhh.Mtc =                                     row.Cell(12).GetValue<string>().Replace("'", " ");
                                tdnhh.Llf =                                     row.Cell(13).GetValue<string>().Replace("'", " ");
                                tdnhh.Distributor_ID =                          row.Cell(14).GetValue<string>().Replace("'", " ");
                                tdnhh.Mpan_Core =                               row.Cell(15).GetValue<string>().Replace("'", " ");
                                tdnhh.Mpan_Status =                             row.Cell(16).GetValue<string>().Replace("'", " ");
                                tdnhh.Day_kWh =                                 row.Cell(17).GetValue<string>().Replace("'", " ");
                                tdnhh.Night_kWh =                               row.Cell(18).GetValue<string>().Replace("'", " ");
                                tdnhh.Evening_kWh =                             row.Cell(19).GetValue<string>().Replace("'", " ");
                                tdnhh.Peak_kWh =                                row.Cell(20).GetValue<string>().Replace("'", " ");
                                tdnhh.OffPeak_kWh =                             row.Cell(21).GetValue<string>().Replace("'", " ");
                                tdnhh.Total_kWh =                               row.Cell(22).GetValue<string>().Replace("'", " ");
                                tdnhh.kVa =                                     row.Cell(23).GetValue<string>().Replace("'", " ");
                                tdnhh.Start_Date =                              row.Cell(24).GetValue<string>().Replace("'", " ");
                                tdnhh.Payment_Method =                          row.Cell(25).GetValue<string>().Replace("'", " ");
                                tdnhh.Payment_Duration =                        row.Cell(26).GetValue<string>().Replace("'", " ");
                                tdnhh.Supplier =                                row.Cell(27).GetValue<string>().Replace("'", " ");
                                tdnhh.LastBill_Date =                           row.Cell(28).GetValue<string>().Replace("'", " ");
                                tdnhh.LastBill_Date_Supplier =                  row.Cell(29).GetValue<string>().Replace("'", " ");
                                tdnhh.StandingCharge_penceorday =               row.Cell(30).GetValue<string>().Replace("'", " ");
                                tdnhh.DayRate =                                 row.Cell(31).GetValue<string>().Replace("'", " ");
                                tdnhh.NightRate =                               row.Cell(32).GetValue<string>().Replace("'", " ");
                                tdnhh.EveningRate =                             row.Cell(33).GetValue<string>().Replace("'", " ");
                                tdnhh.AvailRate =                               row.Cell(34).GetValue<string>().Replace("'", " ");
                                tdnhh.PeakDecjan =                              row.Cell(35).GetValue<string>().Replace("'", " ");
                                tdnhh.PeakNovfeb =                              row.Cell(36).GetValue<string>().Replace("'", " ");
                                tdnhh.PeakOther =                               row.Cell(37).GetValue<string>().Replace("'", " ");
                                tdnhh.OffPeak =                                 row.Cell(38).GetValue<string>().Replace("'", " ");
                                tdnhh.Other =                                   row.Cell(39).GetValue<string>().Replace("'", " ");
                                tdnhh.MeteringCharge_penceorday =               row.Cell(40).GetValue<string>().Replace("'", " ");
                                tdnhh.COMMSCharge_penceorday =                  row.Cell(41).GetValue<string>().Replace("'", " ");
                                tdnhh.SettlementAgencyCharge_penceorday =       row.Cell(42).GetValue<string>().Replace("'", " ");
                                tdnhh.MeterAdministratorAgentCharge_penceorday =row.Cell(43).GetValue<string>().Replace("'", " ");
                                tdnhh.NonEnergy_penceorday =                    row.Cell(44).GetValue<string>().Replace("'", " ");
                                tdnhh.UMSCharge_penceorday =                    row.Cell(45).GetValue<string>().Replace("'", " ");
                                tdnhh.TNUosFixedDailyCharge_penceorday =        row.Cell(46).GetValue<string>().Replace("'", " ");
                                tdnhh.IntelligentAnalytics_penceorday =         row.Cell(47).GetValue<string>().Replace("'", " ");
                                tdnhh.RenewableObligation_penceorday =          row.Cell(48).GetValue<string>().Replace("'", " ");
                                tdnhh.NuclearRABLevy_penceorday =               row.Cell(49).GetValue<string>().Replace("'", " ");
                                tdnhh.AMRCharge_penceorday =                    row.Cell(50).GetValue<string>().Replace("'", " ");
                                tdnhh.MeterAssetProviderMAPCharge_penceorday =  row.Cell(51).GetValue<string>().Replace("'", " ");
                                tdnhh.DataCollectorDataAggregator_penceorday =  row.Cell(52).GetValue<string>().Replace("'", " ");
                                tdnhh.MSPUnitCharge_penceorday =                row.Cell(53).GetValue<string>().Replace("'", " ");
                                tdnhh.MeterServiceAdvancedCharge_penceorday =   row.Cell(54).GetValue<string>().Replace("'", " ");
                                tdnhh.SiteFee_penceorday =                      row.Cell(55).GetValue<string>().Replace("'", " ");
                                tdnhh.DayAheadPremium_penceorday =              row.Cell(56).GetValue<string>().Replace("'", " ");
                                tdnhh.REGO_penceorday =                         row.Cell(57).GetValue<string>().Replace("'", " ");
                                tdnhh.CfdCharge_penceorday =                    row.Cell(58).GetValue<string>().Replace("'", " ");
                                tdnhh.GreengasLevyCharge_penceorday =           row.Cell(59).GetValue<string>().Replace("'", " ");
                                tdnhh.DataServicesAdvanced_penceorday =         row.Cell(60).GetValue<string>().Replace("'", " ");
                                tdnhh.Remarks =                                 row.Cell(61).GetValue<string>().Replace("'", " ");
                                tdnhh.ERMRemarks =                              row.Cell(62).GetValue<string>().Replace("'", " ");

                                retval.TrnERMTenderDetails.Add(tdnhh);

                            }
                        }

                    }

                    retval.successMessage = "Sucessfully Loaded.";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Function name: engineXLSX -> ReadExcelFile ");
                Console.WriteLine(ex.ToString());
                errMessage = ex.Message.ToString();
                retval.successMessage = null;
                retval.errMessage = errMessage;

            }
            return (retval.successMessage != null ? cleanseData_3(retval) : retval);
        }

        public static List<string> nhhHeaderValidation(string headerValue)
        {
            List<string> res = new List<string>();

            nhh nhh = new nhh();

            string[] tobeValidated = headerValue.Split(",");

            int idx = 0;
            foreach (string s in nhh.nhhHeaders)
            {
                if (s == tobeValidated[idx])
                {

                }
                else
                {
                    res.Add("| Uploaded files has Error in column " + s);
                }

                idx++;
            }

            return res;
        }

        public static MasErmTenderModel_3 ReadExcelFileGAS(string filefullpath, string tabname)
        {
            string errMessage =                 String.Empty;
            MasErmTenderModel_3 retval =        new MasErmTenderModel_3();

            retval.MasErmClientModel =          new MasErmClientModel();
            retval.MasErmNetworkModel =         new MasErmNetworkModelNHH();
            retval.MasErmTenderHeaderModel =    new MasErmTenderHeaderModel_1();
            retval.TrnERMTenderDetails =        new List<TrnERMTenderDetailsNHH>();
            retval.TrnERMTenderDetailsGAS =     new List<TrnERMTenderDetailsGAS>();

            retval.MasErmClientModel.name =     "THE MONARCH PARTNERSHIP";
            retval.MasErmClientModel.cusr =     "System";
            retval.MasErmClientModel.cdte =     DateTime.Now;
            retval.MasErmClientModel.uusr =     "System";
            retval.MasErmClientModel.udte =     DateTime.MinValue;
            retval.MasErmClientModel.isActive = 1;

            retval.MasErmNetworkModel.cusr =    "System";
            retval.MasErmNetworkModel.cdte =    DateTime.Now;
            retval.MasErmNetworkModel.uusr =    String.Empty;
            retval.MasErmNetworkModel.udte =    DateTime.Now;

            try
            {
                using (var workbook = new XLWorkbook(filefullpath))
                {

                    var worksheet = workbook.Worksheet(tabname);

                    var rows = worksheet.RangeUsed().RowsUsed();

                    List<string> err = new List<string>();

                    foreach (var row in rows)
                    {
                        TrnERMTenderDetailsGAS tdgas = new TrnERMTenderDetailsGAS();

                        switch (row.RowNumber())
                        {
                            case 1: retval.MasErmClientModel.name =             row.Cell(4).GetValue<string>(); break;
                            case 3: retval.MasErmNetworkModel.name =            row.Cell(2).GetValue<string>(); break;
                            case 4: retval.MasErmNetworkModel.registration_no = row.Cell(2).GetValue<string>(); break;
                            case 5: retval.MasErmNetworkModel.headaddress1 =    row.Cell(2).GetValue<string>(); break;
                            case 6: retval.MasErmNetworkModel.consultant =      row.Cell(2).GetValue<string>(); break;
                            case 7: retval.MasErmNetworkModel.comments =        row.Cell(1).GetValue<string>(); break;
                            case 8: retval.MasErmNetworkModel.corraddress1 =    row.Cell(2).GetValue<string>(); break;
                            case 9: break;
                            case 10: break;
                            case 11:
                                        int idx = 1;
                                        string headerValue = String.Empty;
                                        while (idx <= 36)
                                        {
                                            headerValue += row.Cell(idx).GetValue<string>() + ",";
                                            idx++;
                                        }
                                        err = gasHeaderValidation(headerValue);
                                        break;
                            default: break;
                        }

                        if (row.RowNumber() == 11)
                        {
                            if (err.Count > 0)
                            {
                                retval.successMessage = "Error";

                                foreach (string e in err)
                                {
                                    retval.errMessage += e;
                                }
                                return (retval);
                            }
                        }

                        if (row.RowNumber() > 11)
                        {
                            tdgas.id =              0;
                            tdgas.MasErmTender =    0;
                            tdgas.Location_Ref =    row.Cell(1).GetValue<string>();

                            if (tdgas.Location_Ref.Length > 0)
                            {
                                tdgas.Location_Ref =    row.Cell(1).GetValue<string>().Replace("'", " ");
                                tdgas.Client_Ref =      row.Cell(2).GetValue<string>().Replace("'", " ");
                                tdgas.Address1 =        row.Cell(3).GetValue<string>().Replace("'", " ");
                                tdgas.Address2 =        row.Cell(4).GetValue<string>().Replace("'", " ");
                                tdgas.Address3 =        row.Cell(5).GetValue<string>().Replace("'", " ");
                                tdgas.Address4 =        row.Cell(6).GetValue<string>().Replace("'", " ");
                                tdgas.Address5 =        row.Cell(7).GetValue<string>().Replace("'", " ");
                                tdgas.Postcode =        row.Cell(8).GetValue<string>().Replace("'", " ");
                                tdgas.Msn =             row.Cell(9).GetValue<string>().Replace("'", " ");
                                tdgas.Mprn =            row.Cell(10).GetValue<string>().Replace("'", " ");
                                tdgas.kWh =             row.Cell(11).GetValue<string>().Replace("'", " ");
                                tdgas.Startdate =       row.Cell(12).GetValue<string>().Replace("'", " ");
                                tdgas.Payment_method =  row.Cell(13).GetValue<string>().Replace("'", " ");
                                tdgas.Payment_duration =    row.Cell(14).GetValue<string>().Replace("'", " ");
                                tdgas.SuppId =              row.Cell(15).GetValue<string>().Replace("'", " ");
                                tdgas.Last_bill_date =      row.Cell(16).GetValue<string>().Replace("'", " ");
                                tdgas.Last_bill_date_supp = row.Cell(17).GetValue<string>().Replace("'", " ");
                                tdgas.Standing_charge_penceperday = row.Cell(18).GetValue<string>().Replace("'", " ");
                                tdgas.Day_Rate =            row.Cell(19).GetValue<string>().Replace("'", " ");
                                tdgas.Peak_Decjan =         row.Cell(20).GetValue<string>().Replace("'", " ");
                                tdgas.Peak_Novfeb =         row.Cell(21).GetValue<string>().Replace("'", " ");
                                tdgas.Peak_Other =          row.Cell(22).GetValue<string>().Replace("'", " ");
                                tdgas.Offpeak =             row.Cell(23).GetValue<string>().Replace("'", " ");
                                tdgas.Other =               row.Cell(24).GetValue<string>().Replace("'", " ");
                                tdgas.DistCommodity_penceperday =   row.Cell(25).GetValue<string>().Replace("'", " ");
                                tdgas.MeteringCharge_penceperday =  row.Cell(26).GetValue<string>().Replace("'", " ");
                                tdgas.DistributionSystemCapacity_penceperday = row.Cell(27).GetValue<string>().Replace("'", " ");
                                tdgas.ManagementFee_penceprday =    row.Cell(28).GetValue<string>().Replace("'", " ");
                                tdgas.DistFixed_penceperday =       row.Cell(29).GetValue<string>().Replace("'", " ");
                                tdgas.Unidentifiedgas_penceperday = row.Cell(30).GetValue<string>().Replace("'", " ");
                                tdgas.TransportFee_penceperday =    row.Cell(31).GetValue<string>().Replace("'", " ");
                                tdgas.AMRCharge_penceperday =       row.Cell(32).GetValue<string>().Replace("'", " ");
                                tdgas.DistCustomerCapacity_penceprday = row.Cell(33).GetValue<string>().Replace("'", " ");
                                tdgas.SiteFee_penceprday =          row.Cell(34).GetValue<string>().Replace("'", " ");
                                tdgas.Remarks =                     row.Cell(35).GetValue<string>().Replace("'", " ");
                                tdgas.ERMRemarks =                  row.Cell(36).GetValue<string>().Replace("'", " ");

                                retval.TrnERMTenderDetailsGAS.Add(tdgas);

                            }
                        }

                    }

                    retval.successMessage = "Sucessfully Loaded.";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Function name: engineXLSX -> ReadExcelFile ");
                Console.WriteLine(ex.ToString());
                errMessage =            ex.Message.ToString();
                retval.successMessage = null;
                retval.errMessage =     errMessage;

            }
            return (retval.successMessage != null ? cleanseData_3(retval) : retval);
        }

        public static List<string> gasHeaderValidation(string headerValue)
        {
            List<string> res = new List<string>();

            gas gas = new gas();

            string[] tobeValidated = headerValue.Split(",");

            int idx = 0;
            foreach (string s in gas.gasHeaders)
            {
                if (s == tobeValidated[idx])
                {

                }
                else
                {
                    res.Add("| Uploaded files has Error in column " + s);
                }

                idx++;
            }

            return res;
        }

        public static List<edfModel> ReadExcelFileEDF(string filefullpath)
        {
            string errMessage =     String.Empty;
            List<edfModel> retval = new List<edfModel>();

            try
            {
                using (var workbook = new XLWorkbook(filefullpath))
                {

                    var worksheet = workbook.Worksheet(1);

                    var rows = worksheet.RangeUsed().RowsUsed();

                    List<string> err = new List<string>();

                    foreach (var row in rows)
                    {
                        if (row.RowNumber() > 1)
                        {
                            retval.Add(new edfModel {
                                QuoteID = row.Cell(1).GetValue<string>().Replace("'", " "),
                                Revision = row.Cell(2).GetValue<string>().Replace("'", " "),
                                PrimaryRequestor = row.Cell(3).GetValue<string>().Replace("'", " "),
                                SecondaryRequestor = row.Cell(4).GetValue<string>().Replace("'", " "),
                                TertiaryRequestor = row.Cell(5).GetValue<string>().Replace("'", " "),
                                EarliestSupplyStartDate = row.Cell(6).GetValue<string>().Replace("'", " "),
                                EarliestTerminationDate = row.Cell(7).GetValue<string>().Replace("'", " "),
                                YourReference = row.Cell(8).GetValue<string>().Replace("'", " "),
                                CR = row.Cell(9).GetValue<string>().Replace("'", " "),
                                CustomerName = row.Cell(10).GetValue<string>().Replace("'", " "),
                                ContactName = row.Cell(11).GetValue<string>().Replace("'", " "),
                                CompanyRegisteredNumber = row.Cell(12).GetValue<string>().Replace("'", " "),
                                RegisteredAddress1 = row.Cell(13).GetValue<string>().Replace("'", " "),
                                RegisteredAddress2 = row.Cell(14).GetValue<string>().Replace("'", " "),
                                RegisteredAddress3 = row.Cell(15).GetValue<string>().Replace("'", " "),
                                RegisteredAddressTown = row.Cell(16).GetValue<string>().Replace("'", " "),
                                RegisteredCounty = row.Cell(17).GetValue<string>().Replace("'", " "),
                                RegisteredPostCode = row.Cell(18).GetValue<string>().Replace("'", " "),
                                BillingAddress1 = row.Cell(19).GetValue<string>().Replace("'", " "),
                                BillingAddress2 = row.Cell(20).GetValue<string>().Replace("'", " "),
                                BillingAddress3 = row.Cell(21).GetValue<string>().Replace("'", " "),
                                BillingAddressTown = row.Cell(22).GetValue<string>().Replace("'", " "),
                                BillingCounty = row.Cell(23).GetValue<string>().Replace("'", " "),
                                BillingPostCode = row.Cell(24).GetValue<string>().Replace("'", " "),
                                SiteAddress1 = row.Cell(25).GetValue<string>().Replace("'", " "),
                                SiteAddress2 = row.Cell(26).GetValue<string>().Replace("'", " "),
                                SiteAddress3 = row.Cell(27).GetValue<string>().Replace("'", " "),
                                SiteAddressTown = row.Cell(28).GetValue<string>().Replace("'", " "),
                                SiteCounty = row.Cell(29).GetValue<string>().Replace("'", " "),
                                SitePostCode = row.Cell(30).GetValue<string>().Replace("'", " "),
                                EDFEnergyGroupReference = row.Cell(31).GetValue<string>().Replace("'", " "),
                                PricePoint = row.Cell(32).GetValue<string>().Replace("'", " "),
                                BillingCycle = row.Cell(33).GetValue<string>().Replace("'", " "),
                                PaymentMethod = row.Cell(34).GetValue<string>().Replace("'", " "),
                                PaymentPeriod = row.Cell(35).GetValue<string>().Replace("'", " "),
                                PremiseID = row.Cell(36).GetValue<string>().Replace("'", " "),
                                MPAN = row.Cell(37).GetValue<string>().Replace("'", " "),
                                MeterType = row.Cell(38).GetValue<string>().Replace("'", " "),
                                AMR = row.Cell(39).GetValue<string>().Replace("'", " "),
                                ProfileClass = row.Cell(40).GetValue<string>().Replace("'", " "),
                                MTC = row.Cell(41).GetValue<string>().Replace("'", " "),
                                LLF = row.Cell(42).GetValue<string>().Replace("'", " "),
                                EnergisationStatus = row.Cell(43).GetValue<string>().Replace("'", " "),
                                Voltage = row.Cell(44).GetValue<string>().Replace("'", " "),
                                ElectricitySource = row.Cell(45).GetValue<string>().Replace("'", " "),
                                ElectricitySourceCCLExemption = row.Cell(46).GetValue<string>().Replace("'", " "),
                                PriceStructureDescription = row.Cell(47).GetValue<string>().Replace("'", " "),
                                TriadChargingMethod = row.Cell(48).GetValue<string>().Replace("'", " "),
                                DNO = row.Cell(49).GetValue<string>().Replace("'", " "),
                                PriceStructureCode = row.Cell(50).GetValue<string>().Replace("'", " "),
                                ReactivePowerCharge = row.Cell(51).GetValue<string>().Replace("'", " "),
                                ReactivePowerUOM = row.Cell(52).GetValue<string>().Replace("'", " "),
                                StandingChargeRate = row.Cell(53).GetValue<string>().Replace("'", " "),
                                StandingChargeUOM = row.Cell(54).GetValue<string>().Replace("'", " "),
                                AvailabilityCapacity = row.Cell(55).GetValue<string>().Replace("'", " "),
                                AvailabilityChargeRate = row.Cell(56).GetValue<string>().Replace("'", " "),
                                AvailabilityChargeUOM = row.Cell(57).GetValue<string>().Replace("'", " "),
                                ExcessCapacityChargeRate = row.Cell(58).GetValue<string>().Replace("'", " "),
                                ExcessCapacityChargeUOM = row.Cell(59).GetValue<string>().Replace("'", " "),
                                RateCount = row.Cell(60).GetValue<string>().Replace("'", " "),
                                Rate1Description = row.Cell(61).GetValue<string>().Replace("'", " "),
                                Rate1Value = row.Cell(62).GetValue<string>().Replace("'", " "),
                                Rate1UOM = row.Cell(63).GetValue<string>().Replace("'", " "),
                                Rate1UsagekWh = row.Cell(64).GetValue<string>().Replace("'", " "),
                                Rate1CostPence = row.Cell(65).GetValue<string>().Replace("'", " "),
                                Rate2Description = row.Cell(66).GetValue<string>().Replace("'", " "),
                                Rate2Value = row.Cell(67).GetValue<string>().Replace("'", " "),
                                Rate2UOM = row.Cell(68).GetValue<string>().Replace("'", " "),
                                Rate2UsagekWh = row.Cell(69).GetValue<string>().Replace("'", " "),
                                Rate2CostPence = row.Cell(70).GetValue<string>().Replace("'", " "),
                                Rate3Description = row.Cell(71).GetValue<string>().Replace("'", " "),
                                Rate3Value = row.Cell(72).GetValue<string>().Replace("'", " "),
                                Rate3UOM = row.Cell(73).GetValue<string>().Replace("'", " "),
                                Rate3UsagekWh = row.Cell(74).GetValue<string>().Replace("'", " "),
                                Rate3CostPence = row.Cell(75).GetValue<string>().Replace("'", " "),
                                Rate4Description = row.Cell(76).GetValue<string>().Replace("'", " "),
                                Rate4Value = row.Cell(77).GetValue<string>().Replace("'", " "),
                                Rate4UOM = row.Cell(78).GetValue<string>().Replace("'", " "),
                                Rate4UsagekWh = row.Cell(79).GetValue<string>().Replace("'", " "),
                                Rate4CostPence = row.Cell(80).GetValue<string>().Replace("'", " "),
                                Rate5Description = row.Cell(81).GetValue<string>().Replace("'", " "),
                                Rate5Value = row.Cell(82).GetValue<string>().Replace("'", " "),
                                Rate5UOM = row.Cell(83).GetValue<string>().Replace("'", " "),
                                Rate5UsagekWh = row.Cell(84).GetValue<string>().Replace("'", " "),
                                Rate5CostPence = row.Cell(85).GetValue<string>().Replace("'", " "),
                                Rate6Description = row.Cell(86).GetValue<string>().Replace("'", " "),
                                Rate6Value = row.Cell(87).GetValue<string>().Replace("'", " "),
                                Rate6UOM = row.Cell(88).GetValue<string>().Replace("'", " "),
                                Rate6UsagekWh = row.Cell(89).GetValue<string>().Replace("'", " "),
                                Rate6CostPence = row.Cell(90).GetValue<string>().Replace("'", " "),
                                Rate7Description = row.Cell(91).GetValue<string>().Replace("'", " "),
                                Rate7Value = row.Cell(92).GetValue<string>().Replace("'", " "),
                                Rate7UOM = row.Cell(93).GetValue<string>().Replace("'", " "),
                                Rate7UsagekWh = row.Cell(94).GetValue<string>().Replace("'", " "),
                                Rate7CostPence = row.Cell(95).GetValue<string>().Replace("'", " "),
                                Rate8Description = row.Cell(96).GetValue<string>().Replace("'", " "),
                                Rate8Value = row.Cell(97).GetValue<string>().Replace("'", " "),
                                Rate8UOM = row.Cell(98).GetValue<string>().Replace("'", " "),
                                Rate8UsagekWh = row.Cell(99).GetValue<string>().Replace("'", " "),
                                Rate8CostPence = row.Cell(100).GetValue<string>().Replace("'", " "),
                                Rate9Description = row.Cell(101).GetValue<string>().Replace("'", " "),
                                Rate9Value = row.Cell(102).GetValue<string>().Replace("'", " "),
                                Rate9UOM = row.Cell(103).GetValue<string>().Replace("'", " "),
                                Rate9UsagekWh = row.Cell(104).GetValue<string>().Replace("'", " "),
                                Rate9CostPence = row.Cell(105).GetValue<string>().Replace("'", " "),
                                Rate10Description = row.Cell(106).GetValue<string>().Replace("'", " "),
                                Rate10Value = row.Cell(107).GetValue<string>().Replace("'", " "),
                                Rate10UOM = row.Cell(108).GetValue<string>().Replace("'", " "),
                                Rate10UsagekWh = row.Cell(109).GetValue<string>().Replace("'", " "),
                                Rate10CostPence = row.Cell(110).GetValue<string>().Replace("'", " "),
                                Rate11Description = row.Cell(111).GetValue<string>().Replace("'", " "),
                                Rate11Value = row.Cell(112).GetValue<string>().Replace("'", " "),
                                Rate11UOM = row.Cell(113).GetValue<string>().Replace("'", " "),
                                Rate11UsagekWh = row.Cell(114).GetValue<string>().Replace("'", " "),
                                Rate11CostPence = row.Cell(115).GetValue<string>().Replace("'", " "),
                                Rate12Description = row.Cell(116).GetValue<string>().Replace("'", " "),
                                Rate12Value = row.Cell(117).GetValue<string>().Replace("'", " "),
                                Rate12UOM = row.Cell(118).GetValue<string>().Replace("'", " "),
                                Rate12UsagekWh = row.Cell(119).GetValue<string>().Replace("'", " "),
                                Rate12CostPence = row.Cell(120).GetValue<string>().Replace("'", " "),
                                Rate13Description = row.Cell(121).GetValue<string>().Replace("'", " "),
                                Rate13Value = row.Cell(122).GetValue<string>().Replace("'", " "),
                                Rate13UOM = row.Cell(123).GetValue<string>().Replace("'", " "),
                                Rate13UsagekWh = row.Cell(124).GetValue<string>().Replace("'", " "),
                                Rate13CostPence = row.Cell(125).GetValue<string>().Replace("'", " "),
                                Rate14Description = row.Cell(126).GetValue<string>().Replace("'", " "),
                                Rate14Value = row.Cell(127).GetValue<string>().Replace("'", " "),
                                Rate14UOM = row.Cell(128).GetValue<string>().Replace("'", " "),
                                Rate14UsagekWh = row.Cell(129).GetValue<string>().Replace("'", " "),
                                Rate14CostPence = row.Cell(130).GetValue<string>().Replace("'", " "),
                                Rate15Description = row.Cell(131).GetValue<string>().Replace("'", " "),
                                Rate15Value = row.Cell(132).GetValue<string>().Replace("'", " "),
                                Rate15UOM = row.Cell(133).GetValue<string>().Replace("'", " "),
                                Rate15UsagekWh = row.Cell(134).GetValue<string>().Replace("'", " "),
                                Rate15CostPence = row.Cell(135).GetValue<string>().Replace("'", " "),
                                Rate16Description = row.Cell(136).GetValue<string>().Replace("'", " "),
                                Rate16Value = row.Cell(137).GetValue<string>().Replace("'", " "),
                                Rate16UOM = row.Cell(138).GetValue<string>().Replace("'", " "),
                                Rate16UsagekWh = row.Cell(139).GetValue<string>().Replace("'", " "),
                                Rate16CostPence = row.Cell(140).GetValue<string>().Replace("'", " "),
                                Rate17Description = row.Cell(141).GetValue<string>().Replace("'", " "),
                                Rate17Value = row.Cell(142).GetValue<string>().Replace("'", " "),
                                Rate17UOM = row.Cell(143).GetValue<string>().Replace("'", " "),
                                Rate17UsagekWh = row.Cell(144).GetValue<string>().Replace("'", " "),
                                Rate17CostPence = row.Cell(145).GetValue<string>().Replace("'", " "),
                                Rate18Description = row.Cell(146).GetValue<string>().Replace("'", " "),
                                Rate18Value = row.Cell(147).GetValue<string>().Replace("'", " "),
                                Rate18UOM = row.Cell(148).GetValue<string>().Replace("'", " "),
                                Rate18UsagekWh = row.Cell(149).GetValue<string>().Replace("'", " "),
                                Rate18CostPence = row.Cell(150).GetValue<string>().Replace("'", " "),
                                Rate19Description = row.Cell(151).GetValue<string>().Replace("'", " "),
                                Rate19Value = row.Cell(152).GetValue<string>().Replace("'", " "),
                                Rate19UOM = row.Cell(153).GetValue<string>().Replace("'", " "),
                                Rate19UsagekWh = row.Cell(154).GetValue<string>().Replace("'", " "),
                                Rate19CostPence = row.Cell(155).GetValue<string>().Replace("'", " "),
                                Rate20Description = row.Cell(156).GetValue<string>().Replace("'", " "),
                                Rate20Value = row.Cell(157).GetValue<string>().Replace("'", " "),
                                Rate20UOM = row.Cell(158).GetValue<string>().Replace("'", " "),
                                Rate20UsagekWh = row.Cell(159).GetValue<string>().Replace("'", " "),
                                Rate20CostPence = row.Cell(160).GetValue<string>().Replace("'", " "),
                                QuoteDate = row.Cell(161).GetValue<string>().Replace("'", " "),
                                SiteFeeRate = row.Cell(162).GetValue<string>().Replace("'", " "),
                                SiteFeeUOM = row.Cell(163).GetValue<string>().Replace("'", " "),
                                SmartMeterFlag = row.Cell(164).GetValue<string>().Replace("'", " "),
                                DUoSTariffID = row.Cell(165).GetValue<string>().Replace("'", " "),
                                MarketSegment = row.Cell(166).GetValue<string>().Replace("'", " "),
                                ConnectionType = row.Cell(167).GetValue<string>().Replace("'", " "),
                                MeterGroup = row.Cell(168).GetValue<string>().Replace("'", " "),
                                SSC = row.Cell(169).GetValue<string>().Replace("'", " "),
                                TCRBand = row.Cell(170).GetValue<string>().Replace("'", " "),
                            });
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Function name: engineXLSX -> ReadExcelFile ");
                Console.WriteLine(ex.ToString());
                errMessage = ex.Message.ToString();
            }

            return retval;
        }


        public static int updatepriceloader(string filepath, List<edfModel> model)
        {
            int irow = 0;
            try
            {
                string source_path = filepath.Replace("Analysis", "Analysis\\AnalysisTemplate");
                source_path = source_path.Substring(0, source_path.LastIndexOf('\\')) + "\\PriceLoaderV10.xlsx";

                string destin_path = filepath.Replace("Analysis", "Analysis\\Temp");
                destin_path = destin_path.Substring(0, destin_path.LastIndexOf('\\')) + "\\PriceLoaderV10.xlsx";


                using (var workbook = new XLWorkbook(source_path))
                {

                    var worksheet = workbook.Worksheet("EDFInput");

                    worksheet.Cells().Style.Protection.SetLocked(false);
                    worksheet.Unprotect();

                    irow = 2;
                    foreach (var data in model)
                    {
                        worksheet.Row(irow).Cell(1).Value = data.AMR;
                        worksheet.Row(irow).Cell(3).Value = data.MPAN;
                        worksheet.Row(irow).Cell(8).Value = data.StandingChargeRate;
                        irow++;
                    }

                    workbook.SaveAs(destin_path);
                }
            }
            catch (System.Exception ex)
            {
                
            }

            return irow;
        }

        public static List<scModel> extract_sc_priceloader(string filepath, int stoprun)
        {

            List<scModel> scModel = new List<scModel>();

            string destin_path = filepath.Replace("Analysis", "Analysis\\Temp");
            destin_path = destin_path.Substring(0, destin_path.LastIndexOf('\\')) + "\\PriceLoaderV10.xlsx";

            try
            {

                // Load workbook
                Aspose.Cells.Workbook book = new Aspose.Cells.Workbook(destin_path);

                // Get first worksheet
                Aspose.Cells.Worksheet sheet = book.Worksheets["EDFInput"];

                book.CalculateFormula();

                for (int row = 2; row <= (stoprun-1); row++)
                {
                    // Read a cell
                    Aspose.Cells.Cell valueA1 = sheet.Cells["C" + row];
                    Aspose.Cells.Cell valueA2 = sheet.Cells["K" + row];

                    scModel.Add(new scModel
                    {
                        mpan = valueA1.Value.ToString(),
                        charge = Math.Round(Convert.ToDecimal(valueA2.Value), 2).ToString()
                    });
                }


            }
            catch (System.Exception ex)
            {

            }

            return scModel;

        }

    }
}
