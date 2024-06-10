using lpnu.Data;
using Microsoft.EntityFrameworkCore;
using lpnu.Models;
using System.Linq;
using System.Threading.Tasks;
using lpnu.Services.Interfaces;
namespace lpnu.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly LpnuContext _context;

        public DocumentService(LpnuContext context)
        {
            _context = context;
        }

        public async Task AddDocumentAsync(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            return await _context.Documents
                .Include(d => d.AccreditationProgram)
                .FirstOrDefaultAsync(d => d.Id == documentId);
        }

        public async Task<List<Document>> GetDocumentsByProgramIdAsync(int programId)
        {
            return await _context.Documents
                                 .Where(d => d.AccreditationProgramId == programId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsByProgramIdAsync(int programId)
        {
            return await _context.Documents
                .Where(d => d.AccreditationProgramId == programId)
                .ToListAsync();
        }
    }
}
