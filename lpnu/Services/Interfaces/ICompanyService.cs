using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface ICompanyService
    {
        Task AddCompanyAsync(Company company);
        Task DeleteCompanyAsync(int companyId);
        Task<Company> GetCompanyByIdAsync(int companyId);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task UpdateCompanyAsync(Company company);
    }
}
