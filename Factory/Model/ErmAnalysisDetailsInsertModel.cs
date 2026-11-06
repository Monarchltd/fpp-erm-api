using DotNet8.WebApi.Factory.Model.sse;

namespace DotNet8.WebApi.Factory.Model
{
    public class ErmAnalysisDetailsInsertModel
    {
        public string               header_id { get; set; }
        public ErmAnalysisHeader    analysisHeader = new ErmAnalysisHeader();
        public List<string>         suppref = new List<string>();
        public List<sseSCModel>     scmodel = new List<sseSCModel>();
    }
}
