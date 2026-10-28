

namespace DotNet8.WebApi.Factory.Model.sse
{
    public class sseGasModel
    {
        public double? id { get; set; }
        public string? QuotationID { get; set; }
        public string? CommercialAgreementID { get; set; }
        public string? DeliveryAgreementID { get; set; }
        public string? ContractStartDate { get; set; }
        public string? ContractEndDate { get; set; }
        public string? Term { get; set; }
        public string? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? SiteAddress { get; set; }
        public string? MPAN { get; set; }
        public string? ProductType { get; set; }
        public string? EnergyType { get; set; }
        public string? MeterType { get; set; }
        public string? PrimaryCommissionRebatepkWh { get; set; }
        public string? PrimaryCommissionRebatepday { get; set; }
        public string? SecondaryCommissionRebatepkWh { get; set; }
        public string? SecondaryCommissionRebatepday { get; set; }
        public string? DirectDebitDiscountpkWh { get; set; }
        public string? EUCBand { get; set; }
        public string? StandingChargepday { get; set; }
        public string? UnitRatepkWh { get; set; }
        public string? TermConsumptionkWh { get; set; }
        public string? AnnualConsumptionkWh { get; set; }
        public string? TotalCost { get; set; }
        public int? noofMonths { get; set; }
    }
}
