using System;

namespace TaxFormGeneratorApi.Domain
{
    public class SalaryJOPPD: IEntity
    {
        public int Id { get; set; }
        
        public DateTime FormDate { get; set; }
        
        public DateTime PaymentDate { get; set; }
        
        public decimal Amount { get; set; }
        
        public string Currency { get; set; }

        public string SalaryMonth { get; set; }
        
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}