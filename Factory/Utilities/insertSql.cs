
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DotNet8.WebApi.Factory.Model;
using fppErm.Models;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace DotNet8.WebApi.Factory.Utilities
{
    public class insertSql
    {
        //1
        public static int InsertMasErmNetwork(MasErmNetworkModelNHH model, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                string sql = String.Format("INSERT INTO [dbo].[MasERMNetwork] " +
                                                      " ([clientid]" +
                                                      ", [clientref]" +
                                                      ", [name]" +
                                                      ", [registration_no]" +
                                                      ", [headaddress1]" +
                                                      ", [headaddress2]" +
                                                      ", [headpostcode]" +
                                                      ", [heademail]" +
                                                      ", [consultant]" +
                                                      ", [comments]" +
                                                      ", [corraddress1]" +
                                                      ", [corraddress2]" +
                                                      ", [corrpostcode]" +
                                                      ", [corremail]" +
                                                      ", [cusr]" +
                                                      ", [cdte]" +
                                                      ", [uusr]" +
                                                      ", [udte]" +
                                                      ", [isActive]) " +
                                                      "VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}', {18} )",
                                                      model.clientid,
                                                      model.clientref,
                                                      model.name,
                                                      model.registration_no,
                                                      model.headaddress1,
                                                      model.headaddress2,
                                                      model.headpostcode,
                                                      model.heademail,
                                                      model.consultant,
                                                      model.comments,
                                                      model.corraddress1,
                                                      model.corraddress2,
                                                      model.corrpostcode,
                                                      model.corremail,
                                                      model.cusr,
                                                      model.cdte.ToString("yyyy-MM-dd HH:MM"),
                                                      model.uusr,
                                                      "",
                                                      model.isActive
                                                      );

                sql += ";" + "SELECT Scope_Identity();";

                retval = insert_to_db(sql, configuration);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return retval;
        }

        //2
        public static int InsertMasErmTender_1(MasErmTenderModel_2 model, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                string sql = String.Format("INSERT INTO [dbo].[MasERMTender_1]" +
                                            " ([network_id]" +
                                            ", [consultantname]" +
                                            ", [consultantemail]" +
                                            ", [supplytype]" +
                                            ", [notes]" +
                                            ", [commission]" +
                                            ", [commission_type]" +
                                            ", [supplier]" +
                                            ", [comp_reg]" +
                                            ", [newlondps]" +
                                            ", [fueltype]" +
                                            ", [enddatelength]" +
                                            ", [analysisaq]" +
                                            ", [fillers]" +
                                            ", [mopagreement]" +
                                            ", [status]" +
                                            ", [version]" +
                                            ", [cusr]" +
                                            ", [cdte]" +
                                            ", [uusr]" +
                                            ", [udte]" +
                                            ", [isActive]) " +
                                            "VALUES ({0},'{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},{16},'{17}','{18}','{19}','{20}',{21} )",
                                            model.network_id,
                                            model.consultantname,
                                            model.consultantemail,
                                            model.supplytype,
                                            model.notes,
                                            model.commission,
                                            model.commission_type,
                                            model.supplier,
                                            model.comp_reg,
                                            model.newlondps,
                                            model.fueltype,
                                            model.enddatelength,
                                            model.analysisaq,
                                            model.fillers,
                                            model.mopagreement,
                                            model.status,
                                            model.version,
                                            model.cusr,
                                            model.cdte.ToString("yyyy-MM-dd HH:MM"),
                                            model.uusr,
                                            "",
                                            model.isActive
                                            );
                sql += ";" + "SELECT Scope_Identity();";

                retval = insert_to_db(sql, configuration);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return retval;
        }

        //3
        public static MasErmTenderHeaderModel_1 InsertMasErmTenderHeaderModel_1(MasErmTenderHeaderModel_1 model, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                string sql = String.Format("INSERT INTO [dbo].[MasERMTenderHeader]" +
                                            " ([cnbno]" +
                                            ", [accountid]" +
                                            ", [tenderid]" +
                                            ", [tenderref]" +
                                            ", [statusid]" +
                                            ", [versionid]" +
                                            ", [cusr]" +
                                            ", [cdte]" +
                                            ", [uusr]" +
                                            ", [udte]" +
                                            ", [isActive]) " +
                                            "VALUES ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}',{10} )",
                                            model.CnbNo,
                                            model.AccountId,
                                            model.TenderId,
                                            model.TenderRef,
                                            model.StatusId,
                                            model.VersionId,
                                            model.CreatedBy,
                                            model.CreatedDate.ToString("yyyy-MM-dd HH:MM"),
                                            "",
                                            "",
                                            model.isActive
                                            );

                model.id = insert_to_db(sql, configuration);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            //return retval;
            return (model);
        }

        public static bool InsertMasErmTender(MasErmTenderModel_1 model, IConfiguration configuration)
        {
            bool retval = false;

            try
            {
                string MasERMTender_1 = String.Format("INSERT INTO [dbo].[MasERMTender_1] +" +
                                                      " ([network_id]" +
                                                      ", [consultantname]" +
                                                      ", [consultantemail]" +
                                                      ", [supplytype]" +
                                                      ", [notes]" +
                                                      ", [commission]" +
                                                      ", [commission_type]" +
                                                      ", [supplier]" +
                                                      ", [comp_reg]" +
                                                      ", [newlondps]" +
                                                      ", [fueltype]" +
                                                      ", [enddatelength]" +
                                                      ", [analysisaq]" +
                                                      ", [fillers]" +
                                                      ", [mopagreement]" +
                                                      ", [status]" +
                                                      ", [version]" +
                                                      ", [cusr]" +
                                                      ", [cdte]" +
                                                      ", [uusr]" +
                                                      ", [udte]" +
                                                      ", [isActive])" +
                                                      "VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{20} )",
                                                      model.MasErmNetworkModel.id,
                                                      model.consultantname,
                                                      model.consultantemail,
                                                      model.supplytype,
                                                      model.notes,
                                                      model.commission,
                                                      model.commission_type,
                                                      model.supplier,
                                                      model.comp_reg,
                                                      model.newlondps,
                                                      model.fueltype,
                                                      model.enddatelength,
                                                      model.analysisaq,
                                                      model.fillers,
                                                      model.mopagreement,
                                                      model.status,
                                                      model.version,
                                                      model.cusr,
                                                      model.cdte,
                                                      model.uusr,
                                                      model.udte,
                                                      1
                                                      );
                /*
                   ""(< network_id, bigint,>
                   ,< consultantname, varchar(30),>
                   ,< consultantemail, varchar(50),>
                   ,< supplytype, int,>
                   ,< notes, varchar(500),>
                   ,< commission, decimal(18, 9),>
                   ,< commission_type, varchar(12),>
                   ,< supplier, varchar(12),>
                   ,< comp_reg, varchar(50),>
                   ,< newlondps, varchar(2),>
                   ,< fueltype, varchar(2),>
                   ,< enddatelength, varchar(50),>
                   ,< analysisaq, varchar(2),>
                   ,< fillers, varchar(2),>
                   ,< mopagreement, varchar(2),>
                   ,< status, int,>
                   ,< version, int,>
                   ,< cusr, varchar(12),>
                   ,< cdte, datetime,>
                   ,< uusr, varchar(12),>
                   ,< udte, datetime,>
                   ,< isActive, int,>)

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    string sql = "SELECT A.*, B.*, C.*, D.*, E.* FROM [dbo].[MasERMTenderHeader] A JOIN [dbo].[MasERMTender_1] B on CAST(B.id as varchar)= A.tenderid JOIN [dbo].[MasERMSupplier] D on D.id = B.supplier\r\nJOIN [dbo].[MasERMNetwork] E on E.id = B.network_id JOIN [dbo].[MasERMClient] C on C.id = E.clientid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                    }
                }
                */

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return retval;
        }


        public static bool InsertTrnERMTenderNotes(TrnERMTenderNotes_1 model, IConfiguration configuration)
        {
            bool retval = false;

            try
            {
                string sql = String.Format("INSERT INTO [dbo].[TrnERMTenderNotes_1] " +
                                                      " ([MasErmTender_id]" +
                                                      ", [tenderref]" +
                                                      ", [notes]" +
                                                      ", [cusr]" +
                                                      ", [cdte]" +
                                                      ", [uusr]" +
                                                      ", [udte]" +
                                                      ", [isActive]) " +
                                                      "VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7} )",
                                                      model.MasErmTender_id,
                                                      model.tenderref,
                                                      model.notes,
                                                      model.cusr,
                                                      model.cdte.ToString("yyyy-MM-dd HH:MM"),
                                                      model.uusr,
                                                      "",
                                                      1
                                                      );

                retval = insert_to_db(sql, configuration) >= 0 ? true : false;

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return retval;
        }

        public static double InsertTrnErmRequirements(TrnERMRequirements model, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                /*
                System.IO.FileStream fs =   new System.IO.FileStream(@filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                byte[] rawData =            br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();

                string sql = String.Format("INSERT INTO [dbo].[TrnERMRequirements_1] ([MasErmTender],[TermsAndCondition_doc],[cusr],[cdte],[uusr],[udte],[isActive]) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})");
                */

                string sql = String.Format("INSERT INTO [dbo].[TrnERMRequirements_1] ([MasErmTender]" +
                                           ",[EnergyType], [ContractDuration],[PaymentTerms],[ContractType]," +
                                           "[IncludedCommission],[PaymentDuration],[Contract_startdate],[Contract_enddate]," +
                                           "[Dcda_agent],[Backbydate],[Billing],[Submission_deadline],[cusr],[cdte]," +
                                           "[uusr],[udte],[isActive]) values (" +
                                           "{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',{17}" +
                                           ");",
                                           model.MasErmTender,
                                           model.EnergyType,
                                           model.ContractDuration,
                                           model.PaymentTerms,
                                           model.ContractType,
                                           model.IncludedCommission,
                                           model.PaymentDuration,
                                           model.Contract_startdate.ToString("yyyy-MM-dd HH:MM"),
                                           model.Contract_enddate.ToString("yyyy-MM-dd HH:MM"),
                                           model.Dcda_agent,
                                           "", //model.Backbydate.ToString("yyyy-MM-dd HH:MM"),
                                           model.Billing,
                                           model.Submission_deadline.ToString("yyyy-MM-dd HH:MM"),
                                           model.cusr,
                                           model.cdte.ToString("yyyy-MM-dd HH:MM"),
                                           "",
                                           "",
                                           model.isActive
                                           );

                sql += "SELECT Scope_Identity();";

                retval = insert_to_db(sql, configuration);

            }
            catch (Exception ex)
            {
            }
            return retval;

        }

        public static double TrnERMRequirementsUpload(List<TrnERMRequirementsUploads> model, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                foreach (var m in model)
                {
                    string sql = String.Format("INSERT INTO [dbo].[TrnERMRequirementsUploads_1] ([MasErmTender]" +
                                               ", [MasErmRequirements], [tenderref], [heading], [upload_file]," +
                                               "[cusr],[cdte],[uusr],[udte],[isActive]) values (" +
                                               "{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}', '{8}', {9}" +
                                               ");",
                                               m.MasErmTender,
                                               m.MasErmRequirements,
                                               m.tenderref,
                                               m.heading,
                                               m.upload_file,
                                               m.cusr,
                                               m.cdte.ToString("yyyy-MM-dd HH:MM"),
                                               "",
                                               "",
                                               m.isActive
                                               );


                    retval += insert_to_db(sql, configuration);
                }

            }
            catch (Exception ex)
            {
            }
            return retval;

        }

        public static double TrnERMRequirementsSuppliers(TrnERMRequirementsSuppliers model, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                string sql = String.Format("INSERT INTO [dbo].[TrnERMRequirementsSuppliers_1] ([MasErmTender]" +
                                            ", [MasErmRequirements], [tenderref], [SupplierId], [InvitationToTender],[EmailIds]," +
                                            "[cusr],[cdte],[uusr],[udte],[isActive]) values (" +
                                            "{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}', '{8}', '{9}', {10}" +
                                            ");",
                                            model.MasErmTender,
                                            model.MasErmRequirements,
                                            model.tenderref,
                                            model.SupplierId,
                                            model.InvitationToTender,
                                            model.EmailIds,
                                            model.cusr,
                                            model.cdte.ToString("yyyy-MM-dd HH:MM"),
                                            "",
                                            "",
                                            model.isActive
                                            );


                retval += insert_to_db(sql, configuration);

            }
            catch (Exception ex)
            {
            }
            return retval;

        }

        public static double TrnERMTenderStatusInsert(TrnERMTenderStatus_1 model, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                string sql = String.Format("INSERT INTO [dbo].[TrnERMTenderStatus_1] ([MasErmTender]" +
                                            ", [tenderref], [StatusId], [StatusDesc],[SubStatusId]," +
                                            "[SubStatusDesc], [CurrentState], [cusr],[cdte],[uusr],[udte],[isActive]) values (" +
                                            "{0},'{1}',{2},'{3}',{4},'{5}',{6},'{7}', '{8}', '{9}', '{10}', {11}" +
                                            ");",
                                            model.MasErmTender,
                                            model.tenderref,
                                            model.StatusId,
                                            model.StatusDesc,
                                            model.SubStatusId,
                                            model.SubStatusDesc,
                                            model.CurrentState,
                                            model.cusr,
                                            model.cdte.ToString("yyyy-MM-dd HH:MM"),
                                            "",
                                            "",
                                            model.isActive
                                            );

                sql +=      "SELECT SCOPE_IDENTITY();";
                retval =   insert_to_db(sql, configuration);

                //Updated MasERMTenderHeader table with status id
                if (retval > 0)
                {
                    sql = String.Format("UPDATE [dbo].[MasERMTenderHeader] SET [statusid] = '{0}',[uusr] = '{1}',[udte] = '{2}'WHERE id = '{3}'",
                                        retval, 
                                        model.cusr, 
                                        model.cdte.ToString("yyyy-MM-dd HH:MM"), 
                                        model.MasErmTender);
                    retval += insert_to_db(sql, configuration);
                }

            }
            catch (Exception ex)
            {
            }
            return retval;

        }

        public static double TrnERMTenderDetailsUpload(List<TrnERMTenderDetailsNHH> model, IConfiguration configuration)
        {
            int retval = -1;
            string currDate = DateTime.Now.ToString("yyyy-MM-dd HH:MM");
            List<TrnERMTenderDetailsNHH> trnERMTenderDetails = new List<TrnERMTenderDetailsNHH>();

            try
            {
                foreach (var m in model)
                {
                    TrnERMTenderDetailsNHH trnDetails = new TrnERMTenderDetailsNHH();

                    /*
                    trnDetails.MasErmTender =   m.MasErmTender;
                    trnDetails.msn =            m.msn;
                    trnDetails.locref =         m.locref;
                    trnDetails.clientref =      m.clientref;
                    trnDetails.address1 =       m.address1;
                    trnDetails.address2 =       m.address2;
                    trnDetails.address3 =       m.address3;
                    trnDetails.postcode =       m.postcode;

                    foreach (var j in model.MasErmTenderMpanModel_s)
                    {
                        trnDetails.mpan =           j.mpan;
                        trnDetails.profile =        j.profile;
                        trnDetails.mtc =            j.mtc;
                        trnDetails.llf =            j.llf;
                        trnDetails.distributorid =  j.distributorid;
                        trnDetails.mpan_core =      j.mpan_core;
                    }

                    foreach (var k in model.MasErmTenderConsumptionModel_s)
                    {
                        trnDetails.day =            k.day;
                        trnDetails.night =          k.night;
                        trnDetails.evening =        k.evening;
                        trnDetails.total =          k.total;
                    }

                    foreach (var l in model.MasErmTenderContractModel_s)
                    {
                        trnDetails.startdate =          l.startdate;
                        trnDetails.payment_method =     l.payment_method;
                        trnDetails.payment_duration =   l.payment_duration;
                    }

                    foreach (var n in model.MasErmTenderCurrentRatesModel_s)
                    {
                        trnDetails.standing_charge =    n.standing_charge;
                        trnDetails.day_rate =           n.day_rate;
                        trnDetails.night_rate =         n.night_rate;
                        trnDetails.cusr =               n.cusr;
                        trnDetails.cdte =               Convert.ToDateTime(n.cdte.ToString("yyyy-MM-dd HH:MM"));
                        trnDetails.isActive =           n.isActive;
                    }
                    */

                    trnDetails.MasErmTender =   m.MasErmTender;
                    trnDetails.Location_Ref =   m.Location_Ref;
                    trnDetails.Client_Ref =     m.Client_Ref;
                    trnDetails.Address1 =       m.Address1;
                    trnDetails.Address2 =       m.Address2;
                    trnDetails.Address3 =       m.Address3;
                    trnDetails.Address4 =       m.Address4;
                    trnDetails.Address5 =       m.Address5;
                    trnDetails.Msn =            m.Msn;
                    trnDetails.Mpanfull =       m.Mpanfull;
                    trnDetails.Profile =        m.Profile;
                    trnDetails.Mtc =            m.Mtc;
                    trnDetails.Llf =            m.Llf;
                    trnDetails.Distributor_ID = m.Distributor_ID;
                    trnDetails.Mpan_Core =      m.Mpan_Core;
                    trnDetails.Mpan_Status =    m.Mpan_Status;
                    trnDetails.Day_kWh =        m.Day_kWh;
                    trnDetails.Night_kWh =      m.Night_kWh;
                    trnDetails.Evening_kWh =    m.Evening_kWh;
                    trnDetails.Peak_kWh =       m.Peak_kWh;
                    trnDetails.OffPeak_kWh =    m.OffPeak_kWh;
                    trnDetails.Total_kWh =      m.Total_kWh;
                    trnDetails.kVa =            m.kVa;
                    trnDetails.Start_Date =     m.Start_Date;
                    trnDetails.Payment_Method = m.Payment_Method;
                    trnDetails.Payment_Duration=m.Payment_Duration;
                    trnDetails.Supplier =       m.Supplier;
                    trnDetails.LastBill_Date =  m.LastBill_Date;
                    trnDetails.LastBill_Date_Supplier =     m.LastBill_Date_Supplier;
                    trnDetails.StandingCharge_penceorday =  m.StandingCharge_penceorday;
                    trnDetails.DayRate =        m.DayRate;
                    trnDetails.NightRate =      m.NightRate;
                    trnDetails.EveningRate =    m.EveningRate;
                    trnDetails.AvailRate =      m.AvailRate;
                    trnDetails.PeakDecjan =     m.PeakDecjan;
                    trnDetails.PeakNovfeb =     m.PeakNovfeb;
                    trnDetails.PeakOther =      m.PeakOther;
                    trnDetails.OffPeak =        m.OffPeak;
                    trnDetails.Other =          m.Other;
                    trnDetails.MeteringCharge_penceorday =              m.MeteringCharge_penceorday;
                    trnDetails.COMMSCharge_penceorday =                 m.COMMSCharge_penceorday;
                    trnDetails.SettlementAgencyCharge_penceorday =      m.SettlementAgencyCharge_penceorday;
                    trnDetails.MeterAdministratorAgentCharge_penceorday = m.MeterAdministratorAgentCharge_penceorday;
                    trnDetails.NonEnergy_penceorday =                   m.NonEnergy_penceorday;
                    trnDetails.UMSCharge_penceorday =                   m.UMSCharge_penceorday;
                    trnDetails.TNUosFixedDailyCharge_penceorday =       m.TNUosFixedDailyCharge_penceorday;
                    trnDetails.IntelligentAnalytics_penceorday =        m.IntelligentAnalytics_penceorday;
                    trnDetails.RenewableObligation_penceorday =         m.RenewableObligation_penceorday;
                    trnDetails.NuclearRABLevy_penceorday =              m.NuclearRABLevy_penceorday;
                    trnDetails.AMRCharge_penceorday =                   m.AMRCharge_penceorday;
                    trnDetails.MeterAssetProviderMAPCharge_penceorday = m.MeterAssetProviderMAPCharge_penceorday;
                    trnDetails.DataCollectorDataAggregator_penceorday = m.DataCollectorDataAggregator_penceorday;
                    trnDetails.MSPUnitCharge_penceorday =               m.MSPUnitCharge_penceorday;
                    trnDetails.MeterServiceAdvancedCharge_penceorday =  m.MeterServiceAdvancedCharge_penceorday;
                    trnDetails.SiteFee_penceorday =                     m.SiteFee_penceorday;
                    trnDetails.DayAheadPremium_penceorday =             m.DayAheadPremium_penceorday;
                    trnDetails.REGO_penceorday =                        m.REGO_penceorday;
                    trnDetails.CfdCharge_penceorday =                   m.CfdCharge_penceorday;
                    trnDetails.GreengasLevyCharge_penceorday =          m.GreengasLevyCharge_penceorday;
                    trnDetails.DataServicesAdvanced_penceorday =        m.DataServicesAdvanced_penceorday;
                    trnDetails.Remarks =                                m.Remarks;
                    trnDetails.ERMRemarks =                             m.ERMRemarks;
                    trnDetails.cusr =                                   m.cusr;
                    trnDetails.isActive =                               m.isActive;


                    trnERMTenderDetails.Add(trnDetails);


                    string sql = String.Empty;
                    sql = String.Format("INSERT INTO [dbo].[TrnERMTenderDetailsNHH] " +
                                        "([MasErmTender] " +
                                        ", [Location_Ref] " +
                                        ", [Client_Ref] " +
                                        ", [Address1] " +
                                        ", [Address2] " +
                                        ", [Address3] " +
                                        ", [Address4] " +
                                        ", [Address5] " +
                                        ", [Postcode] " +
                                        ", [Msn] " +
                                        ", [Mpanfull] " +
                                        ", [Profile] " +
                                        ", [Mtc] " +
                                        ", [Llf] " +
                                        ", [Distributor_ID] " +
                                        ", [Mpan_Core] " +
                                        ", [Mpan_Status] " +
                                        ", [Day_kWh] " +
                                        ", [Night_kWh] " +
                                        ", [Evening_kWh] " +
                                        ", [Peak_kWh] " +
                                        ", [OffPeak_kWh] " +
                                        ", [Total_kWh] " +
                                        ", [kVa] " +
                                        ", [Start_Date] " +
                                        ", [Payment_Method] " +
                                        ", [Payment_Duration] " +
                                        ", [Supplier] " +
                                        ", [LastBill_Date] " +
                                        ", [LastBill_Date_Supplier] " +
                                        ", [StandingCharge_penceorday] " +
                                        ", [DayRate] " +
                                        ", [NightRate] " +
                                        ", [EveningRate] " +
                                        ", [AvailRate] " +
                                        ", [PeakDecjan] " +
                                        ", [PeakNovfeb] " +
                                        ", [PeakOther] " +
                                        ", [OffPeak] " +
                                        ", [Other] " +
                                        ", [MeteringCharge_penceorday] " +
                                        ", [COMMSCharge_penceorday] " +
                                        ", [SettlementAgencyCharge_penceorday] " +
                                        ", [MeterAdministratorAgentCharge_penceorday] " +
                                        ", [NonEnergy_penceorday] " +
                                        ", [UMSCharge_penceorday] " +
                                        ", [TNUosFixedDailyCharge_penceorday] " +
                                        ", [IntelligentAnalytics_penceorday] " +
                                        ", [RenewableObligation_penceorday] " +
                                        ", [NuclearRABLevy_penceorday] " +
                                        ", [AMRCharge_penceorday] " +
                                        ", [MeterAssetProviderMAPCharge_penceorday] " +
                                        ", [DataCollectorDataAggregator_penceorday] " +
                                        ", [MSPUnitCharge_penceorday] " +
                                        ", [MeterServiceAdvancedCharge_penceorday] " +
                                        ", [SiteFee_penceorday] " +
                                        ", [DayAheadPremium_penceorday] " +
                                        ", [REGO_penceorday] " +
                                        ", [CfdCharge_penceorday] " +
                                        ", [GreengasLevyCharge_penceorday] " +
                                        ", [DataServicesAdvanced_penceorday] " +
                                        ", [Remarks] " +
                                        ", [ERMRemarks] " +
                                        ",[cusr]" +
                                        ",[cdte]" +
                                        ",[uusr]" +
                                        ",[udte]" +
                                        ",[isActive]) VALUES (" +
                                        " {0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'," +
                                        "'{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}'," +
                                        "'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}'," +
                                        "'{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}'," +
                                        "'{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}'," +
                                        "'{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}','{60}'," +
                                        "'{61}','{62}','{63}','{64}','{65}','{66}', {67})",
                                        trnDetails.MasErmTender,
                                        trnDetails.Location_Ref,
                                        trnDetails.Client_Ref,
                                        trnDetails.Address1,
                                        trnDetails.Address2,
                                        trnDetails.Address3,
                                        trnDetails.Address4,
                                        trnDetails.Address5,
                                        trnDetails.Postcode,
                                        trnDetails.Msn,
                                        trnDetails.Mpanfull,
                                        trnDetails.Profile,
                                        trnDetails.Mtc,
                                        trnDetails.Llf,
                                        trnDetails.Distributor_ID,
                                        trnDetails.Mpan_Core,
                                        trnDetails.Mpan_Status,
                                        trnDetails.Day_kWh,
                                        trnDetails.Night_kWh,
                                        trnDetails.Evening_kWh,
                                        trnDetails.Peak_kWh,
                                        trnDetails.OffPeak_kWh,
                                        trnDetails.Total_kWh,
                                        trnDetails.kVa.Trim().Length ==0 ? 0: trnDetails.kVa.Trim(),
                                        Convert.ToDateTime(trnDetails.Start_Date).ToString("yyyy-MM-dd"),
                                        trnDetails.Payment_Method,
                                        trnDetails.Payment_Duration,
                                        trnDetails.Supplier.Trim(),
                                        Convert.ToDateTime(trnDetails.LastBill_Date).ToString("yyyy-MM-dd"),
                                        trnDetails.LastBill_Date_Supplier.Trim(),
                                        trnDetails.StandingCharge_penceorday,
                                        trnDetails.DayRate,
                                        trnDetails.NightRate,
                                        trnDetails.EveningRate,
                                        trnDetails.AvailRate.Trim().Length == 0 ? 0 : trnDetails.AvailRate.Trim(),
                                        trnDetails.PeakDecjan,
                                        trnDetails.PeakNovfeb,
                                        trnDetails.PeakOther,
                                        trnDetails.OffPeak,
                                        trnDetails.Other,
                                        trnDetails.MeteringCharge_penceorday,
                                        trnDetails.COMMSCharge_penceorday,
                                        trnDetails.SettlementAgencyCharge_penceorday,
                                        trnDetails.MeterAdministratorAgentCharge_penceorday,
                                        trnDetails.NonEnergy_penceorday,
                                        trnDetails.UMSCharge_penceorday,
                                        trnDetails.TNUosFixedDailyCharge_penceorday,
                                        trnDetails.IntelligentAnalytics_penceorday,
                                        trnDetails.RenewableObligation_penceorday,
                                        trnDetails.NuclearRABLevy_penceorday,
                                        trnDetails.AMRCharge_penceorday,
                                        trnDetails.MeterAssetProviderMAPCharge_penceorday,
                                        trnDetails.DataCollectorDataAggregator_penceorday,
                                        trnDetails.MSPUnitCharge_penceorday,
                                        trnDetails.MeterServiceAdvancedCharge_penceorday,
                                        trnDetails.SiteFee_penceorday,
                                        trnDetails.DayAheadPremium_penceorday,
                                        trnDetails.REGO_penceorday,
                                        trnDetails.CfdCharge_penceorday,
                                        trnDetails.GreengasLevyCharge_penceorday,
                                        trnDetails.DataServicesAdvanced_penceorday,
                                        trnDetails.Remarks,
                                        trnDetails.ERMRemarks,
                                        trnDetails.cusr,
                                        currDate,
                                        "",
                                        "",
                                        trnDetails.isActive);

                    //replace characters

                    retval += insert_to_db(sql, configuration);
                }

            }
            catch (Exception ex)
            {
            }
            return retval;

        }

        public static double TrnERMRequirementsContractdurationInsert(List<TrnERMRequirementsContractDuration> model, IConfiguration configuration)
        {
            int retval = 0;

            try
            {
                foreach (var m in model)
                {
                    string sql = String.Format("INSERT INTO [dbo].[TrnERMRequirementsContractDuration_1] ([MasErmTender]" +
                                                ", [MasErmRequirements], [StartDate], [EndDate], [Duration]," +
                                                "[cusr],[cdte],[uusr],[udte],[isActive]) values (" +
                                                "{0},{1},'{2}','{3}',{4},'{5}','{6}','{7}', '{8}', {9}" +
                                                ");",
                                                m.MasErmTender,
                                                m.MasErmRequirements,
                                                m.StartDate.ToString("yyyy-MM-dd HH:MM"),
                                                m.EndDate.ToString("yyyy-MM-dd HH:MM"),
                                                m.Duration,
                                                m.cusr,
                                                m.cdte.ToString("yyyy-MM-dd HH:MM"),
                                                "",
                                                "",
                                                m.isActive
                                                );


                    retval += insert_to_db(sql, configuration);
                }

            }
            catch (Exception ex)
            {
            }
            return retval;

        }

        public static int insert_to_db(string sql, IConfiguration configuration)
        {
            int retval = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        retval = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return retval;
        }
    }
}
