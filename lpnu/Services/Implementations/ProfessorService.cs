using Microsoft.EntityFrameworkCore;
using lpnu.Data;
using lpnu.Models;
using System.Linq;
using System.Threading.Tasks;
using lpnu.Services.Interfaces;

public class ProfessorService : IProfessorService
{
	private readonly LpnuContext _context;

	public ProfessorService(LpnuContext context)
	{
		_context = context;
	}

	public async Task<PaginatedList<Professor>> GetProfessorsPageAsync(int pageIndex, int pageSize)
	{
		var source = _context.Teachers.AsQueryable();
		var count = await source.CountAsync();
		var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
		return new PaginatedList<Professor>(items, count, pageIndex, pageSize);
	}

	public async Task AddProfessorAsync(Professor professor)
	{
		_context.Teachers.Add(professor);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteProfessorAsync(int professorId)
	{
		var professor = await _context.Teachers.FindAsync(professorId);
		if (professor != null)
		{
			_context.Teachers.Remove(professor);
			await _context.SaveChangesAsync();
		}
	}

	public async Task<Professor> GetProfessorByIdAsync(int id)
	{
		return await _context.Teachers.FindAsync(id);
	}

	public async Task<IEnumerable<Professor>> GetAllProfessorsAsync()
	{
		return await _context.Teachers.ToListAsync();
	}

	public async Task<Professor> GetProfessorByFullNameAsync(string firstName, string lastName, string patronymic)
	{
		return await _context.Teachers
                             .FirstOrDefaultAsync(p =>
                             EF.Functions.Like(p.FirstName, firstName) &&
                             EF.Functions.Like(p.LastName, lastName) &&
                             EF.Functions.Like(p.Patronymic, patronymic));
    }

    public async Task UpdateProfessorAsync(Professor professor)
    {
        _context.Entry(professor).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProfessorExists(professor.Id))
            {
                throw new InvalidOperationException("Professor not found");
            }
            else
            {
                throw;
            }
        }
    }

    private bool ProfessorExists(int id)
    {
        return _context.Teachers.Any(e => e.Id == id);
    }
}