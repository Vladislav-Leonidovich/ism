using lpnu.Models;

namespace lpnu.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<PaginatedList<Photo>> GetGalleryPageAsync(int pageIndex, int pageSize);
        Task AddPhotoAsync(Photo photo);
        Task DeletePhotoAsync(int photoId);
        Task<Photo> GetPhotoByIdAsync(int id);
    }
}
