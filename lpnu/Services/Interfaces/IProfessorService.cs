using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IProfessorService
    {
        Task<PaginatedList<Professor>> GetProfessorsPageAsync(int pageIndex, int pageSize);
        Task AddProfessorAsync(Professor professor);
        Task DeleteProfessorAsync(int professorId);
        Task<Professor> GetProfessorByIdAsync(int id);
        Task<Professor> GetProfessorByFullNameAsync(string firstName, string lastName, string patronymic);
        Task<IEnumerable<Professor>> GetAllProfessorsAsync();
        Task UpdateProfessorAsync(Professor professor);

    }
}
