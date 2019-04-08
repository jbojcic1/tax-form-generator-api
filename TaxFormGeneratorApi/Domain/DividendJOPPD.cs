using System;

namespace TaxFormGeneratorApi.Domain
{
    public class DividendJOPPD: IEntity
    {
        public int Id { get; set; }

        public DateTime FormDate { get; set; }
        
        public DateTime PaymentDate { get; set; }
        
        public decimal Amount { get; set; }
        
        public string Currency { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}