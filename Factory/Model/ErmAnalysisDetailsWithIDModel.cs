using DotNet8.WebApi.Factory.Model.sse;

namespace DotNet8.WebApi.Factory.Model
{
    public class ErmAnalysisDetailsWithIDModel
    {
        public string summary_id { get; set; }
        public string mpan { get; set; }
        public string day { get; set; }
        public string night { get; set; }
        public string evening { get; set; }
        public string avail_charge { get; set; }
        public string standing_charge { get; set; }
        public string noof_months { get; set; }
        public string category { get; set; }
    }
}
