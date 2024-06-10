using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace lpnu.Services.Implementations
{
    public class EducationProgramService : IEducationProgramService
    {
        readonly LpnuContext _context;

        public EducationProgramService(LpnuContext context)
        {
            _context = context;
        }

        public async Task AddEducationProgramAsync(EducationProgram program)
        {
            _context.EducationPrograms.Add(program);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEducationProgramAsync(int programId)
        {
            var program = await _context.EducationPrograms.FindAsync(programId);
            if (program != null)
            {
                _context.EducationPrograms.Remove(program);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EducationProgram>> GetAllBachelorsProgramsAsync()
        {
            return await _context.EducationPrograms
            .Where(p => p.Level == EducationLevel.Bachelors)
            .ToListAsync();
        }

        public async Task<IEnumerable<EducationProgram>> GetAllEducationProgramsAsync()
        {
            return await _context.EducationPrograms.ToListAsync();
        }

        public async Task<IEnumerable<EducationProgram>> GetAllMastersProgramsAsync()
        {
            return await _context.EducationPrograms
            .Where(p => p.Level == EducationLevel.Masters)
            .ToListAsync();
        }

        public async Task<IEnumerable<EducationProgram>> GetAllPhdProgramsAsync()
        {
            return await _context.EducationPrograms
            .Where(p => p.Level == EducationLevel.PhD)
            .ToListAsync();

        }

        public async Task<EducationProgram> GetEducationProgramByIdAsync(int id)
        {
            return await _context.EducationPrograms.FindAsync(id);
        }

        public async Task UpdateEducationProgramAsync(EducationProgram educationProgram)
        {
            _context.Entry(educationProgram).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationProgramExists(educationProgram.Id))
                {
                    throw new InvalidOperationException("Education Program not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool EducationProgramExists(int id)
        {
            return _context.EducationPrograms.Any(e => e.Id == id);
        }
    }
}
