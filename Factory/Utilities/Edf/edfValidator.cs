using ClosedXML.Excel;
using DotNet8.WebApi.Factory.Model;
using DotNet8.WebApi.Factory.Model.Edf;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DotNet8.WebApi.Factory.Utilities
{
    public class edfValidator
    {
        public static int fileValidation(string filepath, List<edfModel> model, string tenderid, IConfiguration configuration)
        {
            int retval = 0;

            try
            {
                #region read_details
                viewERMTenderModel ermDetails = new viewERMTenderModel();
                getSql getSql = new getSql();

                try
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = configuration.GetConnectionString("DevDatabase");
                        connection.Open();

                        ermDetails = getSql.getTenderAll(tenderid, configuration);

                    }
                }
                catch (Exception e)
                {
                }
                finally
                {

                }
                #endregion

                string fname = filepath.Substring(filepath.LastIndexOf('\\'), (filepath.Length- filepath.LastIndexOf('\\')));

                string destin_path =    filepath.Replace("Analysis", "Uploads\\" + ermDetails.ermHeader.TenderRef);
                destin_path =           destin_path.Substring(0, destin_path.LastIndexOf('\\')) + fname.Replace("\\", "\\Error_");

                File.Copy(filepath, destin_path);

                //using (var workbook = new XLWorkbook())
                //{
                //    var worksheet =                     workbook.Worksheets.Add("Error Report");
                //    worksheet.Cell("A1").Value =        "Hello World!";
                //    worksheet.Cell("A2").FormulaA1 =    "=MID(A1, 7, 5)";
                //    workbook.SaveAs(destin_path);
                //}
            }
            catch(Exception ex)
            {

            }

            return retval;
        }
    }
}
