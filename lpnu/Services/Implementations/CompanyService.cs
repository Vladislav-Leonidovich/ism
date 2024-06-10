using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace lpnu.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly LpnuContext _context;

        public CompanyService(LpnuContext context)
        {
            _context = context;
        }
        public async Task AddCompanyAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompanyAsync(int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int companyId)
        {
            return await _context.Companies.FindAsync(companyId);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(company.Id))
                {
                    throw new InvalidOperationException("Company not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
