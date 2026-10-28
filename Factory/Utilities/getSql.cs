
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DotNet8.WebApi.Factory.Model;
using fppErm.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace DotNet8.WebApi.Factory.Utilities
{
    public class getSql
    {
        public static viewERMTenderModel getTenderAll(string id, IConfiguration configuration)
        {
            viewERMTenderModel erm = new viewERMTenderModel();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();
                    erm.ermTender = new MasErmTenderModel_1();

                    string sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[MasERMTender_1] WHERE ID = '" + id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                erm.ermTender.id = reader.GetInt64(0);
                                erm.ermTender.network_id = reader.GetInt64(1);
                                erm.ermTender.consultantname = reader.GetString(2);
                                erm.ermTender.consultantemail = reader.GetString(3);
                                erm.ermTender.supplytype = reader.GetInt32(4).ToString();
                                erm.ermTender.notes = reader.GetString(5);
                                erm.ermTender.commission = reader.GetDecimal(6);
                                erm.ermTender.commission_type = reader.GetString(7);
                                erm.ermTender.supplier = reader.GetString(8);
                                erm.ermTender.comp_reg = reader.GetString(9);
                                erm.ermTender.newlondps = reader.GetString(10);
                                erm.ermTender.fueltype = reader.GetString(11);
                                erm.ermTender.enddatelength = reader.GetString(12);
                                erm.ermTender.analysisaq = reader.GetString(13);
                                erm.ermTender.fillers = reader.GetString(14);
                                erm.ermTender.mopagreement = reader.GetString(15);
                                erm.ermTender.status = reader.GetInt32(16);
                                erm.ermTender.version = reader.GetInt32(17);
                                erm.ermTender.cusr = reader.GetString(18);
                                erm.ermTender.cdte = reader.GetDateTime(19);
                                erm.ermTender.uusr = reader.GetString(20);
                                erm.ermTender.udte = reader.GetDateTime(21);
                                erm.ermTender.isActive = reader.GetInt32(22);

                            }
                        }
                    }

                    erm.ermNetwork = new MasErmNetworkModelNHH();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[MasERMNetwork] WHERE ID = '" + erm.ermTender.network_id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                erm.ermNetwork.id = reader.GetInt64(0);
                                erm.ermNetwork.clientid = reader.GetInt64(1);
                                erm.ermNetwork.clientref = reader.GetString(2);
                                erm.ermNetwork.name = reader.GetString(3);
                                erm.ermNetwork.registration_no = reader.GetString(4);
                                erm.ermNetwork.headaddress1 = reader.GetString(5);
                                erm.ermNetwork.headaddress2 = reader.GetString(6);
                                erm.ermNetwork.headpostcode = reader.GetString(7);
                                erm.ermNetwork.heademail = reader.GetString(8);
                                erm.ermNetwork.consultant = reader.GetString(9);
                                erm.ermNetwork.comments = reader.GetString(10);
                                erm.ermNetwork.corraddress1 = reader.GetString(11);
                                erm.ermNetwork.corraddress2 = reader.GetString(12);
                                erm.ermNetwork.corrpostcode = reader.GetString(13);
                                erm.ermNetwork.corremail = reader.GetString(14);
                                erm.ermNetwork.cusr = reader.GetString(15);
                                erm.ermNetwork.cdte = reader.GetDateTime(16);
                                erm.ermNetwork.uusr = reader.GetString(17);
                                erm.ermNetwork.udte = reader.GetDateTime(18);
                                erm.ermNetwork.isActive = reader.GetInt32(19);

                            }
                        }
                    }

                    erm.ermHeader = new MasErmTenderHeaderModel_1();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[MasERMTenderHeader] WHERE TENDERID = '" + erm.ermTender.id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                erm.ermHeader.id = reader.GetInt64(0);
                                erm.ermHeader.CnbNo = reader.GetString(1);
                                erm.ermHeader.AccountId = reader.GetString(2);
                                erm.ermHeader.TenderId = reader.GetString(3);
                                erm.ermHeader.TenderRef = reader.GetString(4);
                                erm.ermHeader.StatusId = reader.GetString(5);
                                erm.ermHeader.VersionId = reader.GetString(6);
                                erm.ermHeader.CreatedBy = reader.GetString(7);
                                erm.ermHeader.CreatedDate = reader.GetDateTime(8);
                                erm.ermHeader.uusr = reader.GetString(9);
                                erm.ermHeader.udte = reader.GetDateTime(10);
                                erm.ermHeader.isActive = reader.GetInt32(11);

                            }
                        }
                    }

                    erm.ermDetailNHH = new List<TrnERMTenderDetailsNHH>();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[TrnERMTenderDetailsNHH] WHERE [MasErmTender] = '" + erm.ermTender.id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrnERMTenderDetailsNHH itm = new TrnERMTenderDetailsNHH();
                                itm.id = reader.GetInt64(0);
                                itm.MasErmTender = reader.GetInt64(1);
                                itm.Location_Ref = reader.GetString(2);
                                itm.Client_Ref = reader.GetString(3);
                                itm.Address1 = reader.GetString(4);
                                itm.Address2 = reader.GetString(5);
                                itm.Address3 = reader.GetString(6);
                                itm.Address4 = reader.GetString(7);
                                itm.Address5 = reader.GetString(8);
                                itm.Postcode = reader.GetString(9);
                                itm.Msn = reader.GetString(10);
                                itm.Mpanfull = reader.GetString(11);
                                itm.Profile = reader.GetString(12);
                                itm.Mtc = reader.GetString(13);
                                itm.Llf = reader.GetString(14);
                                itm.Distributor_ID = reader.GetString(15);
                                itm.Mpan_Core = reader.GetString(16);
                                itm.Mpan_Status = reader.GetString(17);
                                itm.Day_kWh = reader.GetString(18);
                                itm.Night_kWh = reader.GetString(19);
                                itm.Evening_kWh = reader.GetString(20);
                                itm.Peak_kWh = reader.GetString(21);
                                itm.OffPeak_kWh = reader.GetString(22);
                                itm.Total_kWh = reader.GetString(23);
                                itm.kVa = reader.GetString(24);
                                itm.Start_Date = reader.GetString(25);
                                itm.Payment_Method = reader.GetString(26);
                                itm.Payment_Duration = reader.GetString(27);
                                itm.Supplier = reader.GetString(28);
                                itm.LastBill_Date = reader.GetString(29);
                                itm.LastBill_Date_Supplier = reader.GetString(30);
                                itm.StandingCharge_penceorday = reader.GetString(31);
                                itm.DayRate = reader.GetString(32);
                                itm.NightRate = reader.GetString(33);
                                itm.EveningRate = reader.GetString(34);
                                itm.AvailRate = reader.GetString(35);
                                itm.PeakDecjan = reader.GetString(36);
                                itm.PeakNovfeb = reader.GetString(37);
                                itm.PeakOther = reader.GetString(38);
                                itm.OffPeak = reader.GetString(39);
                                itm.Other = reader.GetString(40);
                                itm.MeteringCharge_penceorday = reader.GetString(41);
                                itm.COMMSCharge_penceorday = reader.GetString(42);
                                itm.SettlementAgencyCharge_penceorday = reader.GetString(43);
                                itm.MeterAdministratorAgentCharge_penceorday = reader.GetString(44);
                                itm.NonEnergy_penceorday = reader.GetString(45);
                                itm.UMSCharge_penceorday = reader.GetString(46);
                                itm.TNUosFixedDailyCharge_penceorday = reader.GetString(47);
                                itm.IntelligentAnalytics_penceorday = reader.GetString(48);
                                itm.RenewableObligation_penceorday = reader.GetString(49);
                                itm.NuclearRABLevy_penceorday = reader.GetString(50);
                                itm.AMRCharge_penceorday = reader.GetString(51);
                                itm.MeterAssetProviderMAPCharge_penceorday = reader.GetString(52);
                                itm.DataCollectorDataAggregator_penceorday = reader.GetString(53);
                                itm.MSPUnitCharge_penceorday = reader.GetString(54);
                                itm.MeterServiceAdvancedCharge_penceorday = reader.GetString(55);
                                itm.SiteFee_penceorday = reader.GetString(56);
                                itm.DayAheadPremium_penceorday = reader.GetString(57);
                                itm.REGO_penceorday = reader.GetString(58);
                                itm.CfdCharge_penceorday = reader.GetString(59);
                                itm.GreengasLevyCharge_penceorday = reader.GetString(60);
                                itm.DataServicesAdvanced_penceorday = reader.GetString(61);
                                itm.Remarks = reader.GetString(62);
                                itm.ERMRemarks = reader.GetString(63);
                                itm.cusr = reader.GetString(64);
                                itm.cdte = reader.GetDateTime(65);
                                itm.uusr = reader.GetString(66);
                                itm.udte = reader.GetDateTime(67);
                                itm.isActive = reader.GetInt32(68);

                                erm.ermDetailNHH.Add(itm);
                            }
                        }
                    }

                    erm.ermDetailGAS = new List<TrnERMTenderDetailsGAS>();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[TrnERMTenderDetailsGAS] WHERE [MasErmTender] = '" + erm.ermTender.id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrnERMTenderDetailsGAS itm = new TrnERMTenderDetailsGAS();
                                itm.id = reader.GetInt64(0);
                                itm.MasErmTender = reader.GetInt64(1);
                                itm.Location_Ref = reader.GetString(2);
                                itm.Client_Ref = reader.GetString(3);
                                itm.Address1 = reader.GetString(4);
                                itm.Address2 = reader.GetString(5);
                                itm.Address3 = reader.GetString(6);
                                itm.Address4 = reader.GetString(7);
                                itm.Address5 = reader.GetString(8);
                                itm.Postcode = reader.GetString(9);
                                itm.Msn = reader.GetString(10);
                                itm.Mprn = reader.GetString(11);
                                itm.kWh = reader.GetString(12);
                                itm.Startdate = reader.GetString(13);
                                itm.Payment_method = reader.GetString(14);
                                itm.Payment_duration = reader.GetString(15);
                                itm.SuppId = reader.GetString(16);
                                itm.Last_bill_date = reader.GetString(17);
                                itm.Last_bill_date_supp = reader.GetString(18);
                                itm.Standing_charge_penceperday = reader.GetString(19);
                                itm.Day_Rate = reader.GetString(20);
                                itm.Peak_Decjan = reader.GetString(21);
                                itm.Peak_Novfeb = reader.GetString(22);
                                itm.Peak_Other = reader.GetString(23);
                                itm.Offpeak = reader.GetString(24);
                                itm.Other = reader.GetString(25);
                                itm.DistCommodity_penceperday = reader.GetString(26);
                                itm.MeteringCharge_penceperday = reader.GetString(27);
                                itm.DistributionSystemCapacity_penceperday = reader.GetString(28);
                                itm.ManagementFee_penceprday = reader.GetString(29);
                                itm.DistFixed_penceperday = reader.GetString(30);
                                itm.Unidentifiedgas_penceperday = reader.GetString(31);
                                itm.TransportFee_penceperday = reader.GetString(32);
                                itm.AMRCharge_penceperday = reader.GetString(33);
                                itm.DistCustomerCapacity_penceprday = reader.GetString(34);
                                itm.SiteFee_penceprday = reader.GetString(35);
                                itm.Remarks = reader.GetString(36);
                                itm.ERMRemarks = reader.GetString(37);
                                itm.cusr = reader.GetString(38);
                                itm.cdte = reader.GetDateTime(39);
                                itm.uusr = reader.GetString(40);
                                itm.udte = reader.GetDateTime(41);
                                itm.isActive = reader.GetInt32(42);

                                erm.ermDetailGAS.Add(itm);
                            }
                        }
                    }

                    erm.ermRequirements = new TrnERMRequirements();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[TrnERMRequirements_1] WHERE [MasErmTender] = '" + erm.ermTender.id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                erm.ermRequirements.id = reader.GetInt64(0);
                                erm.ermRequirements.MasErmTender = reader.GetInt64(1);
                                erm.ermRequirements.EnergyType = reader.GetString(2);
                                erm.ermRequirements.ContractDuration = reader.GetString(3);
                                erm.ermRequirements.PaymentTerms = reader.GetString(4);
                                erm.ermRequirements.ContractType = reader.GetString(5);
                                erm.ermRequirements.IncludedCommission = reader.GetString(6);
                                erm.ermRequirements.PaymentDuration = reader.GetString(7);
                                erm.ermRequirements.Contract_startdate = reader.GetDateTime(8);
                                erm.ermRequirements.Contract_enddate = reader.GetDateTime(9);
                                erm.ermRequirements.Dcda_agent = reader.GetString(10);
                                erm.ermRequirements.Backbydate = reader.GetDateTime(11);
                                erm.ermRequirements.Billing = reader.GetString(12);
                                erm.ermRequirements.Submission_deadline = reader.GetDateTime(13);
                                erm.ermRequirements.cusr = reader.GetString(14);
                                erm.ermRequirements.cdte = reader.GetDateTime(15);
                                erm.ermRequirements.uusr = reader.GetString(16);
                                erm.ermRequirements.udte = reader.GetDateTime(17);
                                erm.ermRequirements.isActive = reader.GetInt32(18);

                            }
                        }
                    }

                    erm.ermSuppliers = new List<TrnERMRequirementsSuppliers>();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[TrnERMRequirementsSuppliers_1] WHERE [MasErmTender] = '" + erm.ermTender.id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrnERMRequirementsSuppliers itm = new TrnERMRequirementsSuppliers();

                                itm.id = reader.GetInt64(0);
                                itm.MasErmTender = reader.GetInt64(1);
                                itm.MasErmRequirements = reader.GetInt64(2);
                                itm.tenderref = reader.GetString(3);
                                itm.SupplierId = reader.GetInt64(4);
                                itm.InvitationToTender = reader.GetString(5);
                                itm.EmailIds = reader.GetString(6);
                                itm.cusr = reader.GetString(7);
                                itm.cdte = reader.GetDateTime(8);
                                itm.uusr = reader.GetString(9);
                                itm.udte = reader.GetDateTime(10);
                                itm.isActive = reader.GetInt32(11);

                                erm.ermSuppliers.Add(itm);

                            }
                        }
                    }

                    erm.ermUploads = new List<TrnERMRequirementsUploads>();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[TrnERMRequirementsUploads_1] WHERE [MasErmTender] = '" + erm.ermTender.id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrnERMRequirementsUploads itm = new TrnERMRequirementsUploads();

                                itm.id = reader.GetInt64(0);
                                itm.MasErmTender = reader.GetInt64(1);
                                itm.MasErmRequirements = reader.GetInt64(2);
                                itm.tenderref = reader.GetString(3);
                                itm.heading = reader.GetString(4);
                                itm.upload_file = reader.GetString(5);
                                itm.cusr = reader.GetString(6);
                                itm.cdte = reader.GetDateTime(7);
                                itm.uusr = reader.GetString(8);
                                itm.udte = reader.GetDateTime(9);
                                itm.isActive = reader.GetInt32(10);

                                erm.ermUploads.Add(itm);

                            }
                        }
                    }

                    erm.ermContractDuration = new List<TrnERMRequirementsContractDuration>();

                    sql = "SELECT * FROM [Fixed Pricing Portal].[dbo].[TrnERMRequirementsContractDuration_1] WHERE [MasErmTender] = '" + erm.ermTender.id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TrnERMRequirementsContractDuration itm = new TrnERMRequirementsContractDuration();

                                itm.id = reader.GetInt64(0);
                                itm.MasErmTender = reader.GetInt64(1);
                                itm.MasErmRequirements = reader.GetInt64(2);
                                itm.StartDate = reader.GetDateTime(3);
                                itm.EndDate = reader.GetDateTime(4);
                                itm.Duration = reader.GetInt32(5);
                                itm.cusr = reader.GetString(6);
                                itm.cdte = reader.GetDateTime(7);
                                itm.uusr = reader.GetString(8);
                                itm.udte = reader.GetDateTime(9);
                                itm.isActive = reader.GetInt32(10);

                                erm.ermContractDuration.Add(itm);

                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
            }
            finally
            {

            }

            return erm;
        }
    
        public static string getSupplierName(string id, IConfiguration configuration)
        {
            string excpMessage = String.Empty;
            string sql = String.Empty;
            sql = "SELECT * FROM [dbo].[MasERMSupplier] WHERE id = '" + id + "' and [IsActive] = 1";
            MasErmSupplierModel supplier = new MasErmSupplierModel();

            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    int fieldPlcHolder = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fieldPlcHolder = 0;

                                supplier.id = reader.GetInt64(fieldPlcHolder);
                                supplier.name = reader.GetString(++fieldPlcHolder).Trim();
                                supplier.address1 = reader.GetString(++fieldPlcHolder);
                                supplier.address2 = reader.GetString(++fieldPlcHolder);
                                supplier.postcode = reader.GetString(++fieldPlcHolder);
                                supplier.email = reader.GetString(++fieldPlcHolder);
                                supplier.type = reader.GetInt32(++fieldPlcHolder);
                                supplier.dps = reader.GetString(++fieldPlcHolder);
                                supplier.comment1 = reader.GetString(++fieldPlcHolder);
                                supplier.comment2 = reader.GetString(++fieldPlcHolder);
                                supplier.comment3 = reader.GetString(++fieldPlcHolder);
                                supplier.comment4 = reader.GetString(++fieldPlcHolder);
                                supplier.comment5 = reader.GetString(++fieldPlcHolder);
                                supplier.cusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty;
                                supplier.cdte = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue;
                                supplier.uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty;
                                supplier.udte = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue;
                                supplier.isActive = reader.GetInt32(++fieldPlcHolder);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                excpMessage = e.Message;
            }
            finally
            {

            }

            return supplier.name;
        }

        public static List<ErmAnalysisHeader> getAnalysisHeader(string id, IConfiguration configuration)
        {
            string excpMessage = String.Empty;
            string sql =         String.Empty;
            sql =                "SELECT * FROM [dbo].[ErmAnalysisHeader] WHERE MasErmTender = '" + id + "' and [IsActive] = 1";
            List<ErmAnalysisHeader> data = new List<ErmAnalysisHeader>();

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    int fieldPlcHolder = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fieldPlcHolder = 0;

                                data.Add(new ErmAnalysisHeader {
                                    id = reader.GetInt64(fieldPlcHolder),
                                    MasErmTender = reader.GetInt64(++fieldPlcHolder).ToString(),
                                    tenderref = reader.GetString(++fieldPlcHolder),
                                    version = reader.GetInt32(++fieldPlcHolder).ToString(),
                                    reportTyoe = reader.GetInt32(++fieldPlcHolder).ToString(),
                                    filename = reader.GetString(++fieldPlcHolder),
                                    cusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                    cdte = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                    udte = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    isActive = reader.GetInt32(++fieldPlcHolder)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                excpMessage = e.Message;
            }
            finally
            {

            }

            return data;
        }

    }
}
