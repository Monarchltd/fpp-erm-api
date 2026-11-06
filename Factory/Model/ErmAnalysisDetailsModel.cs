using DotNet8.WebApi.Factory.Model.sse;

namespace DotNet8.WebApi.Factory.Model
{
    public class ErmAnalysisDetailsModel
    {
        public List<ERMMultiDetails>  multianalyze { get; set; }
    }

    public class ERMMultiDetails
    {
        public ErmAnalysisHeader Header { get; set; }
        public List<ErmAnalysisSummaryModel> Summary { get; set; }
        public List<TrnERMTenderDetailsNHH> electric { get; set; }
        public List<TrnERMTenderDetailsGAS> gas { get; set; }

    }
}
