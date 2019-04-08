using System;

namespace TaxFormGeneratorApi.Dtos
{
    public class DividendJOPPDDto : FormDto
    {
        public decimal Amount { get; set; }
        
        public string Currency { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}