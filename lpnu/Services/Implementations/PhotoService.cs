using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lpnu.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly LpnuContext _context;

        public PhotoService(LpnuContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Photo>> GetGalleryPageAsync(int pageIndex, int pageSize)
        {
            var source = _context.Gallery.AsQueryable();
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<Photo>(items, count, pageIndex, pageSize);
        }

        public async Task AddPhotoAsync(Photo photo)
        {
            _context.Gallery.Add(photo);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(int photoId)
        {
            var photo = await _context.Gallery.FindAsync(photoId);
            if (photoId != null)
            {
                _context.Gallery.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Photo> GetPhotoByIdAsync(int id)
        {
            return await _context.Gallery.FindAsync(id);
        }
    }
}
