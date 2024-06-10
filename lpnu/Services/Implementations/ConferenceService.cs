using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lpnu.Services.Implementations
{
    public class ConferenceService : IConferenceService
    {
        private readonly LpnuContext _context;

        public ConferenceService(LpnuContext context)
        {
            _context = context;
        }

        public async Task AddConferenceAsync(Conference conference)
        {
            _context.Conferences.Add(conference);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteConferenceAsync(int conferenceId)
        {
            var conference = await _context.Conferences.FindAsync(conferenceId);
            if (conference != null)
            {
                _context.Conferences.Remove(conference);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Conference>> GetAllConferencesAsync()
        {
            return await _context.Conferences.ToListAsync();
        }

        public async Task<Conference> GetConferencesByIdAsync(int conferenceId)
        {
            return await _context.Conferences.FindAsync(conferenceId);
        }

        public async Task UpdateConferenceAsync(Conference conference)
        {
            _context.Entry(conference).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConferenceExists(conference.Id))
                {
                    throw new InvalidOperationException("Conference not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ConferenceExists(int id)
        {
            return _context.Conferences.Any(e => e.Id == id);
        }
    }
}
