
using DotNet8.WebApi.Factory.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DotNet8.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TenderController(ILogger<TenderController> logger, IConfiguration configuration) : ControllerBase
    {

        [HttpGet] [Route("")]// Matches GET requests to /Tender
        public IActionResult GetTender()
        {
            string excpMessage = String.Empty;

            List<MasErmTenderHeaderModel> ermtender = new List<MasErmTenderHeaderModel>();

            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    String sql =    "SELECT A.*, B.*, C.*, D.*, E.*, F.* FROM [dbo].[MasERMTenderHeader] A " +
                                    "JOIN [dbo].[MasERMTender_1] B on CAST(B.id as varchar)= A.tenderid " + 
                                    "JOIN [dbo].[MasERMSupplier] D on D.id = B.supplier " + 
                                    "JOIN [dbo].[MasERMNetwork] E on E.id = B.network_id " +
                                    "JOIN [dbo].[MasERMClient] C on C.id = E.clientid " +
                                    "JOIN [dbo].[TrnERMTenderStatus_1] F on F.id = A.statusid " +
                                    "WHERE A.isActive=1 ORDER BY F.cdte desc";

                    int fieldPlcHolder = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fieldPlcHolder = 0;

                                ermtender.Add(new MasErmTenderHeaderModel()
                                {
                                    id =                    Convert.ToInt32(reader.GetInt64(fieldPlcHolder)),
                                    CnbNo =                 reader.GetString(++fieldPlcHolder).Trim(),
                                    AccountId =             reader.GetString(++fieldPlcHolder),
                                    TenderId =              reader.GetString(++fieldPlcHolder),
                                    TenderRef=              reader.GetString(++fieldPlcHolder),
                                    StatusId =              reader.GetString(++fieldPlcHolder),
                                    VersionId =             reader.GetString(++fieldPlcHolder),
                                    CreatedBy =             reader.GetString(++fieldPlcHolder),
                                    CreatedDate =           !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    uusr =                  !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) :String.Empty,
                                    udte =                  !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    isActive =              Convert.ToInt32(reader.GetInt32(++fieldPlcHolder)),
                                    MasErmTenderModel_s =   new MasErmTenderModel_3()
                                    {
                                        id =                reader.GetInt64(++fieldPlcHolder),
                                        network_id =        reader.GetInt64(++fieldPlcHolder),
                                        consultantname =    reader.GetString(++fieldPlcHolder),
                                        consultantemail =   reader.GetString(++fieldPlcHolder),
                                        supplytype =        reader.GetInt32(++fieldPlcHolder).ToString(),
                                        notes =             reader.GetString(++fieldPlcHolder),
                                        commission =        Math.Round(reader.GetDecimal(++fieldPlcHolder),2),
                                        commission_type =   reader.GetString(++fieldPlcHolder),
                                        supplier =          reader.GetString(++fieldPlcHolder),
                                        comp_reg =          reader.GetString(++fieldPlcHolder),
                                        newlondps =         reader.GetString(++fieldPlcHolder),
                                        fueltype =          reader.GetString(++fieldPlcHolder),
                                        enddatelength =     reader.GetString(++fieldPlcHolder),
                                        analysisaq =        reader.GetString(++fieldPlcHolder),
                                        fillers =           reader.GetString(++fieldPlcHolder),
                                        mopagreement =      reader.GetString(++fieldPlcHolder),
                                        status =            reader.GetInt32(++fieldPlcHolder),
                                        version =           reader.GetInt32(++fieldPlcHolder),
                                        cusr =              reader.GetString(++fieldPlcHolder),
                                        cdte =              reader.GetDateTime(++fieldPlcHolder),
                                        uusr =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive =          reader.GetInt32(++fieldPlcHolder),
                                    },
                                    MasErmClientModel_s =   new MasErmClientModel()
                                    {
                                        id =                reader.GetInt64(++fieldPlcHolder),
                                        name =              reader.GetString(++fieldPlcHolder),
                                        address1 =          reader.GetString(++fieldPlcHolder),
                                        address2 =          reader.GetString(++fieldPlcHolder),
                                        postcode =          reader.GetString(++fieldPlcHolder),
                                        email =             reader.GetString(++fieldPlcHolder),
                                        cusr =              reader.GetString(++fieldPlcHolder),
                                        cdte =              reader.GetDateTime(++fieldPlcHolder),
                                        uusr =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive =          reader.GetInt32(++fieldPlcHolder)
                                    },
                                    MasErmSupplierModel_s = new MasErmSupplierModel()
                                    {
                                        id =                reader.GetInt64(++fieldPlcHolder),
                                        name =              reader.GetString(++fieldPlcHolder),
                                        address1 =          reader.GetString(++fieldPlcHolder),
                                        address2 =          reader.GetString(++fieldPlcHolder),
                                        postcode =          reader.GetString(++fieldPlcHolder),
                                        email =             reader.GetString(++fieldPlcHolder),
                                        type =              reader.GetInt32(++fieldPlcHolder),
                                        dps =               reader.GetString(++fieldPlcHolder),
                                        comment1 =          reader.GetString(++fieldPlcHolder),
                                        comment2 =          reader.GetString(++fieldPlcHolder),
                                        comment3 =          reader.GetString(++fieldPlcHolder),
                                        comment4 =          reader.GetString(++fieldPlcHolder),
                                        comment5 =          reader.GetString(++fieldPlcHolder),
                                        cusr =              reader.GetString(++fieldPlcHolder),
                                        cdte =              reader.GetDateTime(++fieldPlcHolder),
                                        uusr =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) :DateTime.MinValue,
                                        isActive =          reader.GetInt32(++fieldPlcHolder),
                                    },
                                    MasErmNetworkModel_s = new MasErmNetworkModelNHH()
                                    {
                                        id = reader.GetInt64(++fieldPlcHolder),
                                        clientid = reader.GetInt64(++fieldPlcHolder),
                                        clientref = reader.GetString(++fieldPlcHolder),
                                        name = reader.GetString(++fieldPlcHolder),
                                        registration_no = reader.GetString(++fieldPlcHolder),
                                        headaddress1 = reader.GetString(++fieldPlcHolder),
                                        headaddress2 = reader.GetString(++fieldPlcHolder),
                                        headpostcode = reader.GetString(++fieldPlcHolder),
                                        heademail = reader.GetString(++fieldPlcHolder),
                                        consultant = reader.GetString(++fieldPlcHolder),
                                        comments = reader.GetString(++fieldPlcHolder),
                                        corraddress1 = reader.GetString(++fieldPlcHolder),
                                        corraddress2 = reader.GetString(++fieldPlcHolder),
                                        corrpostcode = reader.GetString(++fieldPlcHolder),
                                        corremail = reader.GetString(++fieldPlcHolder),
                                        cusr = reader.GetString(++fieldPlcHolder),
                                        cdte = reader.GetDateTime(++fieldPlcHolder),
                                        uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive = reader.GetInt32(++fieldPlcHolder)
                                    },
                                    TrnERMTenderStatus_s = new TrnERMTenderStatus_1()
                                    {
                                        id =                reader.GetInt64(++fieldPlcHolder),
                                        MasErmTender =      reader.GetInt64(++fieldPlcHolder),
                                        tenderref =         reader.GetString(++fieldPlcHolder),
                                        StatusId =          reader.GetInt32(++fieldPlcHolder),
                                        StatusDesc =        reader.GetString(++fieldPlcHolder),
                                        SubStatusId =       reader.GetInt32(++fieldPlcHolder),
                                        SubStatusDesc =     reader.GetString(++fieldPlcHolder),
                                        CurrentState =      reader.GetInt32(++fieldPlcHolder),
                                        cusr =              reader.GetString(++fieldPlcHolder),
                                        cdte =              reader.GetDateTime(++fieldPlcHolder),
                                        uusr =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte =              !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive =          reader.GetInt32(++fieldPlcHolder)
                                    }
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

            return ermtender != null ? (ermtender.Count > 0 ? Ok(ermtender) : NotFound(excpMessage)) : NotFound();
        }

        [HttpGet]
        [Route("SearchTender")]// Matches GET requests to /Tender
        public IActionResult SearchTender(string tenderid, string cnbno)
        {
            string excpMessage = String.Empty;

            List<MasErmTenderHeaderModel> ermtender = new List<MasErmTenderHeaderModel>();

            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                    connection.Open();

                    String sql = "SELECT A.*, B.*, C.*, D.*, E.*, F.* FROM [dbo].[MasERMTenderHeader] A " +
                                    "JOIN [dbo].[MasERMTender_1] B on CAST(B.id as varchar)= A.tenderid " +
                                    "JOIN [dbo].[MasERMSupplier] D on D.id = B.supplier " +
                                    "JOIN [dbo].[MasERMNetwork] E on E.id = B.network_id " +
                                    "JOIN [dbo].[MasERMClient] C on C.id = E.clientid " +
                                    "JOIN [dbo].[TrnERMTenderStatus_1] F on F.id = A.statusid " +
                                    "WHERE A.isActive=1 and (A.tenderid='"+ tenderid + "' or A.cnbno='"+ cnbno + "') ORDER BY F.cdte desc";

                    int fieldPlcHolder = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fieldPlcHolder = 0;

                                ermtender.Add(new MasErmTenderHeaderModel()
                                {
                                    id = Convert.ToInt32(reader.GetInt64(fieldPlcHolder)),
                                    CnbNo = reader.GetString(++fieldPlcHolder).Trim(),
                                    AccountId = reader.GetString(++fieldPlcHolder),
                                    TenderId = reader.GetString(++fieldPlcHolder),
                                    TenderRef = reader.GetString(++fieldPlcHolder),
                                    StatusId = reader.GetString(++fieldPlcHolder),
                                    VersionId = reader.GetString(++fieldPlcHolder),
                                    CreatedBy = reader.GetString(++fieldPlcHolder),
                                    CreatedDate = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                    udte = !reader.IsDBNull(++fieldPlcHolder) ? Convert.ToDateTime(reader.GetSqlDateTime(fieldPlcHolder).ToString()) : DateTime.MinValue,
                                    isActive = Convert.ToInt32(reader.GetInt32(++fieldPlcHolder)),
                                    MasErmTenderModel_s = new MasErmTenderModel_3()
                                    {
                                        id = reader.GetInt64(++fieldPlcHolder),
                                        network_id = reader.GetInt64(++fieldPlcHolder),
                                        consultantname = reader.GetString(++fieldPlcHolder),
                                        consultantemail = reader.GetString(++fieldPlcHolder),
                                        supplytype = reader.GetInt32(++fieldPlcHolder).ToString(),
                                        notes = reader.GetString(++fieldPlcHolder),
                                        commission = Math.Round(reader.GetDecimal(++fieldPlcHolder), 2),
                                        commission_type = reader.GetString(++fieldPlcHolder),
                                        supplier = reader.GetString(++fieldPlcHolder),
                                        comp_reg = reader.GetString(++fieldPlcHolder),
                                        newlondps = reader.GetString(++fieldPlcHolder),
                                        fueltype = reader.GetString(++fieldPlcHolder),
                                        enddatelength = reader.GetString(++fieldPlcHolder),
                                        analysisaq = reader.GetString(++fieldPlcHolder),
                                        fillers = reader.GetString(++fieldPlcHolder),
                                        mopagreement = reader.GetString(++fieldPlcHolder),
                                        status = reader.GetInt32(++fieldPlcHolder),
                                        version = reader.GetInt32(++fieldPlcHolder),
                                        cusr = reader.GetString(++fieldPlcHolder),
                                        cdte = reader.GetDateTime(++fieldPlcHolder),
                                        uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive = reader.GetInt32(++fieldPlcHolder),
                                    },
                                    MasErmClientModel_s = new MasErmClientModel()
                                    {
                                        id = reader.GetInt64(++fieldPlcHolder),
                                        name = reader.GetString(++fieldPlcHolder),
                                        address1 = reader.GetString(++fieldPlcHolder),
                                        address2 = reader.GetString(++fieldPlcHolder),
                                        postcode = reader.GetString(++fieldPlcHolder),
                                        email = reader.GetString(++fieldPlcHolder),
                                        cusr = reader.GetString(++fieldPlcHolder),
                                        cdte = reader.GetDateTime(++fieldPlcHolder),
                                        uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive = reader.GetInt32(++fieldPlcHolder)
                                    },
                                    MasErmSupplierModel_s = new MasErmSupplierModel()
                                    {
                                        id = reader.GetInt64(++fieldPlcHolder),
                                        name = reader.GetString(++fieldPlcHolder),
                                        address1 = reader.GetString(++fieldPlcHolder),
                                        address2 = reader.GetString(++fieldPlcHolder),
                                        postcode = reader.GetString(++fieldPlcHolder),
                                        email = reader.GetString(++fieldPlcHolder),
                                        type = reader.GetInt32(++fieldPlcHolder),
                                        dps = reader.GetString(++fieldPlcHolder),
                                        comment1 = reader.GetString(++fieldPlcHolder),
                                        comment2 = reader.GetString(++fieldPlcHolder),
                                        comment3 = reader.GetString(++fieldPlcHolder),
                                        comment4 = reader.GetString(++fieldPlcHolder),
                                        comment5 = reader.GetString(++fieldPlcHolder),
                                        cusr = reader.GetString(++fieldPlcHolder),
                                        cdte = reader.GetDateTime(++fieldPlcHolder),
                                        uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive = reader.GetInt32(++fieldPlcHolder),
                                    },
                                    MasErmNetworkModel_s = new MasErmNetworkModelNHH()
                                    {
                                        id = reader.GetInt64(++fieldPlcHolder),
                                        clientid = reader.GetInt64(++fieldPlcHolder),
                                        clientref = reader.GetString(++fieldPlcHolder),
                                        name = reader.GetString(++fieldPlcHolder),
                                        registration_no = reader.GetString(++fieldPlcHolder),
                                        headaddress1 = reader.GetString(++fieldPlcHolder),
                                        headaddress2 = reader.GetString(++fieldPlcHolder),
                                        headpostcode = reader.GetString(++fieldPlcHolder),
                                        heademail = reader.GetString(++fieldPlcHolder),
                                        consultant = reader.GetString(++fieldPlcHolder),
                                        comments = reader.GetString(++fieldPlcHolder),
                                        corraddress1 = reader.GetString(++fieldPlcHolder),
                                        corraddress2 = reader.GetString(++fieldPlcHolder),
                                        corrpostcode = reader.GetString(++fieldPlcHolder),
                                        corremail = reader.GetString(++fieldPlcHolder),
                                        cusr = reader.GetString(++fieldPlcHolder),
                                        cdte = reader.GetDateTime(++fieldPlcHolder),
                                        uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive = reader.GetInt32(++fieldPlcHolder)
                                    },
                                    TrnERMTenderStatus_s = new TrnERMTenderStatus_1()
                                    {
                                        id = reader.GetInt64(++fieldPlcHolder),
                                        MasErmTender = reader.GetInt64(++fieldPlcHolder),
                                        tenderref = reader.GetString(++fieldPlcHolder),
                                        StatusId = reader.GetInt32(++fieldPlcHolder),
                                        StatusDesc = reader.GetString(++fieldPlcHolder),
                                        SubStatusId = reader.GetInt32(++fieldPlcHolder),
                                        SubStatusDesc = reader.GetString(++fieldPlcHolder),
                                        CurrentState = reader.GetInt32(++fieldPlcHolder),
                                        cusr = reader.GetString(++fieldPlcHolder),
                                        cdte = reader.GetDateTime(++fieldPlcHolder),
                                        uusr = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetString(fieldPlcHolder) : String.Empty,
                                        udte = !reader.IsDBNull(++fieldPlcHolder) ? reader.GetDateTime(fieldPlcHolder) : DateTime.MinValue,
                                        isActive = reader.GetInt32(++fieldPlcHolder)
                                    }
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

            return ermtender != null ? (ermtender.Count > 0 ? Ok(ermtender) : NotFound(excpMessage)) : NotFound();
        }

    }
}
