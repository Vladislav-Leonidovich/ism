using lpnu.Data;
using lpnu.Models;
using lpnu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace lpnu.Services.Implementations
{
    public class StructurePostService : IStructurePostService
    {
        private readonly LpnuContext _context;

        public StructurePostService(LpnuContext context)
        {
            _context = context;
        }

        public async Task DeleteStructurePostAsync(int postId)
        {
            var post = await _context.StructurePosts.FindAsync(postId);
            if (post != null)
            {
                _context.StructurePosts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddStructurePostAsync(StructurePost post)
        {
            _context.StructurePosts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StructurePost>> GetAllStructurePostsAsync()
        {
            return await _context.StructurePosts.ToListAsync();
        }

        public async Task<StructurePost> GetStructureByIdAsync(int id)
        {
            return await _context.StructurePosts.FindAsync(id);
        }

        public async Task UpdateStructurePostAsync(StructurePost post)
        {
            _context.Entry(post).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StructurePostExists(post.Id))
                {
                    throw new InvalidOperationException("Post not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool StructurePostExists(int id)
        {
            return _context.StructurePosts.Any(e => e.Id == id);
        }
    }
}
