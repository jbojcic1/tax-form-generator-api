using System;

namespace TaxFormGeneratorApi.Dtos
{
    public class FormResponseDto
    {
        public DateTime FormDate { get; set; }
        public string FormType { get; set; }
        public decimal Amount { get; set; }
    }
}