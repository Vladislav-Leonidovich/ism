using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IAccreditationProgramService
    {
        Task<IEnumerable<AccreditationProgram>> GetAllProgramsAsync();
        Task<AccreditationProgram> GetProgramByIdAsync(int programId);
        Task<AccreditationProgram> GetProgramOtherDocumentsByIdAsync(int programId);
        Task<AccreditationProgram> GetProgramAccreditationDocumentsByIdAsync(int programId);
        Task AddProgramAsync(AccreditationProgram program);
        Task UpdateProgramAsync(AccreditationProgram program);
        Task DeleteProgramAsync(int id);
    }
}
