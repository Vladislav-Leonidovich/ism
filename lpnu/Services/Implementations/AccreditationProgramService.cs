using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace lpnu.Services.Implementations
{
    public class AccreditationProgramService : IAccreditationProgramService
    {
        private readonly LpnuContext _context;
        private readonly IDocumentService _documentService;

        public AccreditationProgramService(LpnuContext context, IDocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }

        public async Task<IEnumerable<AccreditationProgram>> GetAllProgramsAsync()
        {
            return await _context.AccreditationPrograms
                .Include(p => p.AccreditationDocuments)
                .Include(p => p.OtherDocuments)
                .ToListAsync();
        }

        public async Task<AccreditationProgram> GetProgramByIdAsync(int programId)
        {
            return await _context.AccreditationPrograms
                .Include(p => p.AccreditationDocuments)
                .Include(p => p.OtherDocuments)
                .FirstOrDefaultAsync(p => p.Id == programId);
        }

        public async Task<AccreditationProgram> GetProgramOtherDocumentsByIdAsync(int programId)
        {
            return await _context.AccreditationPrograms
                .Include(p => p.OtherDocuments)
                .FirstOrDefaultAsync(p => p.Id == programId);
        }

        public async Task<AccreditationProgram> GetProgramAccreditationDocumentsByIdAsync(int programId)
        {
            return await _context.AccreditationPrograms
                .Include(p => p.AccreditationDocuments)
                .FirstOrDefaultAsync(p => p.Id == programId);
        }

        public async Task AddProgramAsync(AccreditationProgram program)
        {
            _context.AccreditationPrograms.Add(program);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProgramAsync(AccreditationProgram program)
        {
            _context.Entry(program).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramExists(program.Id))
                {
                    throw new InvalidOperationException("Program not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteProgramAsync(int id)
        {
            var program = await _context.AccreditationPrograms.FindAsync(id);
            if (program == null)
            {
                throw new Exception("Program not found");
            }

            // Удаляем связанные документы
            var documents = await _documentService.GetAllDocumentsByProgramIdAsync(id);
            foreach (var document in documents)
            {
                _context.Documents.Remove(document);
                if (File.Exists(document.FilePath))
                {
                    File.Delete(document.FilePath);
                }
            }

            _context.AccreditationPrograms.Remove(program);
            await _context.SaveChangesAsync();
        }

        private bool ProgramExists(int id)
        {
            return _context.AccreditationPrograms.Any(e => e.Id == id);
        }
    }
}
