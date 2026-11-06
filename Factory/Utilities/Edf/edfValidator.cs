using ClosedXML.Excel;
using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Model.Edf;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DotNet8.WebApi.Factory.Utilities
{
    class errorDetails
    {
        public int id { get; set; }
        public string mpan { get; set; }
        public string errormessage { get; set; }
    }

    public class edfValidator
    {
        public static int fileValidation(string filepath, List<edfModel> model, string tenderid, IConfiguration configuration)
        {
            int retval = 0;

            try
            {
                #region read_details
                viewERMTenderModel ermDetails = new viewERMTenderModel();
                getSql getSql =                 new getSql();

                try
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                        connection.Open();

                        ermDetails =                  getSql.getTenderAll(tenderid, configuration);

                    }
                }
                catch (Exception e)
                {
                }
                finally
                {

                }
                #endregion

                List<errorDetails> err = new List<errorDetails>();

                foreach (var itm in model)
                {
                    string errmessage = String.Empty;

                    //Check for Payment Method.
                    string paymethod =  ermDetails.ermRequirements.PaymentTerms == "1" ? "Direct Debit" : "BACS";
                    errmessage +=       itm.PaymentMethod == paymethod ? "" : "Payment Method: Fail " + itm.PaymentMethod+ " -ne- " + paymethod + ",";

                    //Check for Payment Period.
                    errmessage +=       itm.PaymentPeriod == ermDetails.ermRequirements.PaymentDuration ? "" : "Payment Period: Fail " + itm.PaymentPeriod + " -ne- " + ermDetails.ermRequirements.PaymentDuration + ",";

                    if (errmessage.Trim().Length > 0)
                        err.Add(new errorDetails { mpan = itm.MPAN, errormessage = errmessage });

                }

                string fname =          filepath.Substring(filepath.LastIndexOf('\\'), (filepath.Length- filepath.LastIndexOf('\\')));
                fname =                 fname.Replace(".xlsx", DateTime.Now.ToString("ddMMyyyyHHMMSS")+".csv");

                string destin_path =    filepath.Replace("Analysis", "Uploads\\" + ermDetails.ermHeader.TenderRef);
                destin_path =           destin_path.Substring(0, destin_path.LastIndexOf('\\')) + fname.Replace("\\", "\\Error_");
                
                StreamWriter sw =       new StreamWriter(destin_path);

                sw.WriteLine("MPAN, Error Message");

                foreach (var itm in err)
                {
                    sw.WriteLine("m"+itm.mpan.ToString() + "," + itm.errormessage);
                }
                sw.Close();

                retval = err.Count();

            }
            catch(Exception ex)
            {

            }

            return retval;
        }
    }
}
