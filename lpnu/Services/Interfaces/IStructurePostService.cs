using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IStructurePostService
    {
        Task AddStructurePostAsync(StructurePost post);
        Task DeleteStructurePostAsync(int postId);
        Task<StructurePost> GetStructureByIdAsync(int id);
        Task<IEnumerable<StructurePost>> GetAllStructurePostsAsync();
        Task UpdateStructurePostAsync(StructurePost post);
    }
}
