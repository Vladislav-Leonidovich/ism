using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace lpnu.Services.Implementations
{
    public class UniversityService : IUniversityService
    {
        private readonly LpnuContext _context;

        public UniversityService(LpnuContext context)
        {
            _context = context;
        }
        public async Task AddUniversityAsync(University university)
        {
            _context.Universities.Add(university);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUniversityAsync(int universityId)
        {
            var university = await _context.Universities.FindAsync(universityId);
            if (university != null)
            {
                _context.Universities.Remove(university);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<University>> GetAllUniversitiesAsync()
        {
            return await _context.Universities.ToListAsync();
        }

        public async Task<University> GetUniversitiesByIdAsync(int universityId)
        {
            return await _context.Universities.FindAsync(universityId);
        }

        public async Task UpdateUniversityAsync(University university)
        {
            _context.Entry(university).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityExists(university.Id))
                {
                    throw new InvalidOperationException("University not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool UniversityExists(int id)
        {
            return _context.Universities.Any(e => e.Id == id);
        }
    }
}
