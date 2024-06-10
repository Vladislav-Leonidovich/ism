using System.ComponentModel.DataAnnotations;

namespace lpnu.Models
{
	public class EducationProgram
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public EducationLevel Level { get; set; }
	}

	public enum EducationLevel
	{
		Bachelors,
		Masters,
		PhD
	}
}
