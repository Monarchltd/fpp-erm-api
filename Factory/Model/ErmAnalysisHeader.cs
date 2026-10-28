using System;

namespace DotNet8.WebApi.Factory.Model
{
    public class ErmAnalysisHeader
    {
        public double? id { get; set; }
        public string MasErmTender { get; set; }
        public string tenderref { get; set; }
        public string version { get; set; }
        public string reportTyoe { get; set; }
        public string filename { get; set; }
        public string cusr { get; set; }
        public DateTime cdte { get; set; }
        public string uusr { get; set; }
        public DateTime udte { get; set; }
        public int isActive { get; set; }
    }
}
