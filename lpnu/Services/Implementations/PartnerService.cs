using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lpnu.Services.Implementations
{
    public class PartnerService : IPartnerService
    {
        private readonly LpnuContext _context;
        public PartnerService(LpnuContext context)
        {
            _context = context;
        }
        public async Task AddPartnerAsync(Partner partner)
        {
            _context.Partners.Add(partner);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatrnerAsync(int partnerId)
        {
            var partner = await _context.Partners.FindAsync(partnerId);
            if (partner != null)
            {
                _context.Partners.Remove(partner);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Partner>> GetAllPartnersAsync()
        {
            return await _context.Partners.ToListAsync();
        }

        public async Task<Partner> GetPartnerByIdAsync(int id)
        {
            return await _context.Partners.FindAsync(id);
        }

        public async Task UpdatePartnerAsync(Partner partner)
        {
            _context.Entry(partner).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnerExists(partner.Id))
                {
                    throw new InvalidOperationException("Partner not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PartnerExists(int id)
        {
            return _context.Partners.Any(e => e.Id == id);
        }
    }
}
