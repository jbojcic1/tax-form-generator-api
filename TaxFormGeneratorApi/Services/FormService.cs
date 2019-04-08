using System.Collections.Generic;
using System.Linq;
using TaxFormGeneratorApi.Dal;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Dtos;

namespace TaxFormGeneratorApi.Services
{
    public interface IFormService
    {
        IEnumerable<FormResponseDto> GetAll(int userId);
        void SaveForm(int userId, FormDto form);
    }

    public class FormService : IFormService
    {
        // TODO: reconsider once EF supports TPC for handling hierarchies
        
        private readonly IRepository<DividendJOPPD> dividendJOPPDRepository;
        private readonly IRepository<SalaryJOPPD> salaryJOPPDRepository;
        
        public FormService(
            IRepository<DividendJOPPD> dividendJOPPDRepository, 
            IRepository<SalaryJOPPD> salaryJOPPDRepository
        )
        {
            this.dividendJOPPDRepository = dividendJOPPDRepository;
            this.salaryJOPPDRepository = salaryJOPPDRepository;
        }

        public IEnumerable<FormResponseDto> GetAll(int userId)
        {
            var dividendJOPPDs = this.dividendJOPPDRepository.GetBy(x => x.UserId == userId)
                .Select(x => new FormResponseDto
                {
                    FormDate = x.FormDate,
                    Amount = x.Amount,
                    FormType = "DIVIDEND_JOPPD"
                })
                .ToList();
            
            var salaryJOPPDs = this.salaryJOPPDRepository.GetBy(x => x.UserId == userId)
                .Select(x => new FormResponseDto
                {
                    FormDate = x.FormDate,
                    Amount = x.Amount,
                    FormType = "SALARY_JOPPD"
                })
                .ToList();

            return salaryJOPPDs.Concat(dividendJOPPDs);
        }

        public void SaveForm(int userId, FormDto form)
        {
            switch (form)
            {
                case SalaryJOPPDDto salaryJOPPD:
                    this.SaveSalaryJOPPD(userId, salaryJOPPD);
                    break;
                case DividendJOPPDDto dividendJOPPD:
                    this.SaveDividendJOPPD(userId, dividendJOPPD);
                    break;
            }
        }

        private void SaveSalaryJOPPD(int userId, SalaryJOPPDDto form)
        {
            var salaryJOPPD = new SalaryJOPPD
            {
                UserId = userId,
                FormDate = form.FormDate,
                PaymentDate = form.PaymentDate,
                Amount = form.Amount,
                Currency = form.Currency,
                SalaryMonth = form.SalaryMonth
            };
            
            this.salaryJOPPDRepository.Insert(salaryJOPPD);
        }
        
        private void SaveDividendJOPPD(int userId, DividendJOPPDDto form)
        {
            var dividendJOPPD = new DividendJOPPD
            {
                UserId = userId,
                FormDate = form.FormDate,
                PaymentDate = form.PaymentDate,
                Amount = form.Amount,
                Currency = form.Currency,
                StartDate = form.StartDate,
                EndDate = form.EndDate
            };
            
            this.dividendJOPPDRepository.Insert(dividendJOPPD);
        }
    }
}