using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IUniversityService
    {
        Task AddUniversityAsync(University university);
        Task DeleteUniversityAsync(int universityId);
        Task<University> GetUniversitiesByIdAsync(int universityId);
        Task<IEnumerable<University>> GetAllUniversitiesAsync();
        Task UpdateUniversityAsync(University university);
    }
}
