using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IDocumentService
    {
        Task AddDocumentAsync(Document document);
        Task DeleteDocumentAsync(int documentId);
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task<List<Document>> GetDocumentsByProgramIdAsync(int programId);
        Task<IEnumerable<Document>> GetAllDocumentsByProgramIdAsync(int programId);
    }
}
