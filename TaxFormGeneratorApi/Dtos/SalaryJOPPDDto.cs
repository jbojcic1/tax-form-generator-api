namespace TaxFormGeneratorApi.Dtos
{
    public class SalaryJOPPDDto : FormDto
    {      
        public decimal Amount { get; set; }
        
        public string Currency { get; set; }

        public string SalaryMonth { get; set; }
    }
}