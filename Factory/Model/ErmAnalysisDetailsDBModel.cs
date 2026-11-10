using DotNet8.WebApi.Factory.Model.sse;

namespace DotNet8.WebApi.Factory.Model
{
    public class ErmAnalysisDetailsDBModel
    {
        public string id { get; set; }
        public string ErmAnalysisSummary { get; set; }
        public string mpan { get; set; }
        public string day { get; set; }
        public string night { get; set; }
        public string evening { get; set; }
        public string avail_charge { get; set; }
        public string standing_charge { get; set; }
        public string noof_months { get; set; }
        public string category { get; set; }
        public string cusr { get; set; }
        public DateTime cdte { get; set; }
        public string uusr { get; set; }
        public DateTime udte { get; set; }
        public int isActive { get; set; }
    }
}
