using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IEducationProgramService
    {
        Task AddEducationProgramAsync(EducationProgram program);
        Task DeleteEducationProgramAsync(int programId);
        Task<EducationProgram> GetEducationProgramByIdAsync(int id);
        Task<IEnumerable<EducationProgram>> GetAllEducationProgramsAsync();
        Task<IEnumerable<EducationProgram>> GetAllBachelorsProgramsAsync();
        Task<IEnumerable<EducationProgram>> GetAllMastersProgramsAsync();
        Task<IEnumerable<EducationProgram>> GetAllPhdProgramsAsync();
        Task UpdateEducationProgramAsync(EducationProgram educationProgram);
    }
}
