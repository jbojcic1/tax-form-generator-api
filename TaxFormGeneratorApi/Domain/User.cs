using System;
using System.Collections.Generic;

namespace TaxFormGeneratorApi.Domain
{
    public class User : IEntity
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Password { get; set; }
        
        public string Salt { get; set; }
        
        public string RefreshToken { get; set; }
        
        public virtual UserSettings UserSettings { get; set; }
        
        public virtual List<SalaryJOPPD> SalaryJOPPDs { get; set; }
        
        public virtual List<DividendJOPPD> DividendJOPPDs { get; set; }
    }
}