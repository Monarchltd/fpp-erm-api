using System.ComponentModel.DataAnnotations;

namespace DotNet8.WebApi.Factory.Model
{
    public class TrnERMTenderDetails
    {
        public double id { get; set; }
        public double MasErmTender { get; set; }
        public string msn { get; set; }
        public string locref { get; set; }
        public string clientref { get; set; }

        public string address1 { get; set; }

        public string address2 { get; set; }

        public string address3 { get; set; }

        public string address4 { get; set; }

        public string postcode { get; set; }

        public string mpan { get; set; }

        public string profile { get; set; }

        public string mtc { get; set; }

        public string llf { get; set; }

        public string distributorid { get; set; }

        public string mpan_core { get; set; }

        public decimal day { get; set; }

        public decimal night { get; set; }

        public decimal evening { get; set; }

        public decimal total { get; set; }

        public DateTime startdate { get; set; }

        public string payment_method { get; set; }

        public string payment_duration { get; set; }

        public string standingchargeperiod { get; set; }

        public decimal standing_charge { get; set; }

        public decimal day_rate { get; set; }

        public decimal night_rate { get; set; }

        public string? cusr { get; set; }
        public DateTime cdte { get; set; }
        public string uusr { get; set; }
        public DateTime udte { get; set; }
        public int isActive { get; set; }
    }
}
