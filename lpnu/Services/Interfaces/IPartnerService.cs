using lpnu.Models;
namespace lpnu.Services.Interfaces
{
    public interface IPartnerService
    {
        Task AddPartnerAsync(Partner partner);
        Task<IEnumerable<Partner>> GetAllPartnersAsync();
        Task DeletePatrnerAsync(int partnerId);
        Task<Partner> GetPartnerByIdAsync(int id);
        Task UpdatePartnerAsync(Partner partner);
    }
}
