using System;

namespace TaxFormGeneratorApi.Dtos
{
    public class FormDto
    {
        public DateTime FormDate { get; set; }
        
        public DateTime PaymentDate { get; set; }
    }
}