using Microsoft.EntityFrameworkCore;
using lpnu.Models;

namespace lpnu.Data
{
	public class LpnuContext : DbContext
	{
		public LpnuContext(DbContextOptions<LpnuContext> options) : base(options)
		{

		}

		public DbSet<Professor> Teachers { get; set; }
		public DbSet<StructurePost> StructurePosts { get; set; }
		public DbSet<Photo> Gallery { get; set; }
		public DbSet<Partner> Partners { get; set; }
		public DbSet<EducationProgram> EducationPrograms { get; set; }
		public DbSet<AccreditationProgram> AccreditationPrograms { get; set; }
		public DbSet<Document> Documents { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<University> Universities { get; set; }
		public DbSet<Conference> Conferences { get; set; }
		public DbSet<Contact> Contacts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Document>()
				.HasDiscriminator<string>("DocumentType")
				.HasValue<AccreditationDocument>("AccreditationDocument")
				.HasValue<OtherDocument>("OtherDocument");

			modelBuilder.Entity<AccreditationProgram>()
				.HasMany(p => p.AccreditationDocuments)
				.WithOne(d => d.AccreditationProgram)
				.HasForeignKey(d => d.AccreditationProgramId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<AccreditationProgram>()
				.HasMany(p => p.OtherDocuments)
				.WithOne(d => d.AccreditationProgram)
				.HasForeignKey(d => d.AccreditationProgramId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
