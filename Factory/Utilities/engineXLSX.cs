namespace DotNet8.WebApi.Factory.Utilities
{
    using ClosedXML.Excel;
    using DotNet8.WebApi.Factory.Model;
    using System;
    using System.Collections.Generic;

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
            catch (Exception ex)
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
            catch (Exception ex)
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
    }
}
